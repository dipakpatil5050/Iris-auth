using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;
namespace IrisAuth.RuntimeService
{
    public static class LoginStateWriter
    {
        private static readonly string FilePath =
            @"C:\ProgramData\IrisAuth\login_state.json";

        public static void WriteLogin(string username, string group)
        {
            Directory.CreateDirectory(@"C:\ProgramData\IrisAuth");

            File.WriteAllText(FilePath,
                JsonConvert.SerializeObject(new
                {
                    Action = "LOGIN",
                    Username = username,
                    Group = group,
                    Time = DateTime.Now
                }));
        }

        public static void WriteLogout()
        {
            File.WriteAllText(FilePath,
                "{ \"Action\": \"LOGOUT\" }");
        }
    }
}
