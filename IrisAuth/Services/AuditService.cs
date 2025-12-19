using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Net;
using IrisAuth.Models;
using IrisAuth.Repositories;
namespace IrisAuth.Services
{
    public class AuditService
    {
        private readonly AuditTrailRepository _repo = new AuditTrailRepository();

        public void Log(
            string username,
            string action,
            string details = null,
            string oldValue = null,
            string newValue = null,
            string reason = null)
        {
            var audit = new AuditTrailModel
            {
                Username = username,
                Action = action,
                Details = details,
                OldValue = oldValue,
                NewValue = newValue,
                Reason = reason,
                IpAddress = GetIp(),
                HostName = Environment.MachineName
            };

            _repo.Insert(audit);

            WriteWindowsEventLog(audit);
        }

        private void WriteWindowsEventLog(AuditTrailModel audit)
        {
            const string source = "IrisAuth";

            if (!EventLog.SourceExists(source))
            {
                EventLog.CreateEventSource(source, "Application");
            }

            EventLog.WriteEntry(
                source,
                $"{audit.Username} | {audit.Action} | {audit.Details}",
                EventLogEntryType.Information
            );
        }

        private string GetIp()
        {
            try
            {
                return Dns.GetHostEntry(Dns.GetHostName())
                          .AddressList
                          .FirstOrDefault(a => a.AddressFamily ==
                              System.Net.Sockets.AddressFamily.InterNetwork)?
                          .ToString();
            }
            catch
            {
                return null;
            }
        }
    }
}
