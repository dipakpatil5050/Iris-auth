using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Threading;
using IrisAuth.IrisSdk.Services;
using IrisAuth.Repositories;
namespace IrisAuth.RuntimeService
{
    public partial class IrisAuthService : ServiceBase
    {
        private CancellationTokenSource _cts;

        // 🔐 Enrollment lock file (shared with WPF)
        private const string EnrollLockPath =
            @"C:\ProgramData\IrisAuth\enroll.lock";

        public IrisAuthService()
        {
            InitializeComponent();           // MUST exist (Designer file)
            ServiceName = "IrisAuthRuntime";
        }

        // =========================
        // SERVICE START
        // =========================
        protected override void OnStart(string[] args)
        {
            _cts = new CancellationTokenSource();

            Task.Run(() => RunLoop(_cts.Token));
        }

        // =========================
        // SERVICE STOP
        // =========================
        protected override void OnStop()
        {
            _cts.Cancel();
        }

        // =========================
        // MAIN LOOP
        // =========================
        private async Task RunLoop(CancellationToken token)
        {
            var iris = new IrisCaptureService();
            var repo = new UserAccountRepository();

            while (!token.IsCancellationRequested)
            {
                try
                {
                    // 🔐 DO NOT LOGIN DURING ENROLLMENT
                    if (File.Exists(EnrollLockPath))
                    {
                        await Task.Delay(1000, token);
                        continue;
                    }

                    // 🔍 Capture iris
                    var result = await iris.CaptureAsync(
                        timeoutMs: 3000,
                        minQuality: 40
                    );

                    if (!result.Success)
                    {
                        await Task.Delay(500, token);
                        continue;
                    }

                    // 🔎 Match against DB
                    var user = repo.MatchIris(result.ImageBase64);

                    if (user != null)
                    {
                        // 📝 Write login state for WinCC
                        LoginStateWriter.WriteLogin(
                            user.Username,
                            user.GroupName
                        );

                        // ⏳ Debounce (avoid repeated logins)
                        await Task.Delay(5000, token);
                    }
                }
                catch (OperationCanceledException)
                {
                    // Service stopping – ignore
                }
                catch (Exception ex)
                {
                    // OPTIONAL: log to file or EventLog
                    // File.AppendAllText("C:\\ProgramData\\IrisAuth\\service.log",
                    //     DateTime.Now + " " + ex + Environment.NewLine);
                }

                await Task.Delay(500, token);
            }
        }
    }
}
