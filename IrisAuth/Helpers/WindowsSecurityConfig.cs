using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
namespace IrisAuth.Helpers
{
    public static class WindowsSecurityConfig
    {
        public static bool EnableGroupCreation =>
            bool.Parse(ConfigurationManager.AppSettings["WindowsSecurity.EnableGroupCreation"] ?? "false");

        public static bool EnableUserCreation =>
            bool.Parse(ConfigurationManager.AppSettings["WindowsSecurity.EnableUserCreation"] ?? "false");

        // ✔ NO PREFIX if empty or missing
        public static string GroupPrefix =>
            ConfigurationManager.AppSettings["WindowsSecurity.GroupPrefix"] ?? string.Empty;

        public static string UserPrefix =>
            ConfigurationManager.AppSettings["WindowsSecurity.UserPrefix"] ?? string.Empty;

        public static string GroupDescription =>
            ConfigurationManager.AppSettings["WindowsSecurity.GroupDescription"] ?? "IrisAuth group";

        public static string UserDescription =>
            ConfigurationManager.AppSettings["WindowsSecurity.UserDescription"] ?? "IrisAuth user";

        public static string DefaultUserPassword =>
            ConfigurationManager.AppSettings["WindowsSecurity.DefaultUserPassword"] ?? "ChangeMe@123";

        public static bool ForcePasswordChange =>
            bool.Parse(ConfigurationManager.AppSettings["WindowsSecurity.ForcePasswordChange"] ?? "true");

        public static bool AutoAddUserToGroup =>
            bool.Parse(ConfigurationManager.AppSettings["WindowsSecurity.AutoAddUserToGroup"] ?? "true");
        // ✅ ADD THESE TWO
        public static string SecurityMode =>
            ConfigurationManager.AppSettings["WindowsSecurity.Mode"] ?? "Local";
        // Local | Domain

        public static string DomainName =>
            ConfigurationManager.AppSettings["WindowsSecurity.DomainName"];
    }
}
