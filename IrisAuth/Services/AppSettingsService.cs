using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using IrisAuth.Models;
namespace IrisAuth.Services
{
    public class AppSettingsService
    {
        public AppSettingsModel Load()
        {
            return new AppSettingsModel
            {
                AllowLocalBiometricUpload = GetBool("AllowLocalBiometricUpload"),

                EnableGroupCreation = GetBool("WindowsSecurity.EnableGroupCreation"),
                EnableUserCreation = GetBool("WindowsSecurity.EnableUserCreation"),

                GroupPrefix = Get("WindowsSecurity.GroupPrefix"),
                UserPrefix = Get("WindowsSecurity.UserPrefix"),

                GroupDescription = Get("WindowsSecurity.GroupDescription"),
                UserDescription = Get("WindowsSecurity.UserDescription"),

                DefaultUserPassword = Get("WindowsSecurity.DefaultUserPassword"),
                ForcePasswordChange = GetBool("WindowsSecurity.ForcePasswordChange"),

                AutoAddUserToGroup = GetBool("WindowsSecurity.AutoAddUserToGroup"),

                LogOperations = GetBool("WindowsSecurity.LogOperations"),

                Mode = Get("WindowsSecurity.Mode"),
                DomainName = Get("WindowsSecurity.DomainName")
            };
        }

        public void Save(AppSettingsModel s)
        {
            Set("AllowLocalBiometricUpload", s.AllowLocalBiometricUpload.ToString().ToLower());

            Set("WindowsSecurity.EnableGroupCreation", s.EnableGroupCreation.ToString().ToLower());
            Set("WindowsSecurity.EnableUserCreation", s.EnableUserCreation.ToString().ToLower());

            Set("WindowsSecurity.GroupPrefix", s.GroupPrefix ?? "");
            Set("WindowsSecurity.UserPrefix", s.UserPrefix ?? "");

            Set("WindowsSecurity.GroupDescription", s.GroupDescription);
            Set("WindowsSecurity.UserDescription", s.UserDescription);

            Set("WindowsSecurity.DefaultUserPassword", s.DefaultUserPassword);
            Set("WindowsSecurity.ForcePasswordChange", s.ForcePasswordChange.ToString().ToLower());

            Set("WindowsSecurity.AutoAddUserToGroup", s.AutoAddUserToGroup.ToString().ToLower());

            Set("WindowsSecurity.LogOperations", s.LogOperations.ToString().ToLower());

            Set("WindowsSecurity.Mode", s.Mode);
            Set("WindowsSecurity.DomainName", s.DomainName);

            ConfigurationManager.RefreshSection("appSettings");
        }

        // ---------- helpers ----------
        private string Get(string key)
            => ConfigurationManager.AppSettings[key];

        private bool GetBool(string key)
            => bool.TryParse(ConfigurationManager.AppSettings[key], out bool v) && v;

        private void Set(string key, string value)
        {
            var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

            if (config.AppSettings.Settings[key] == null)
                config.AppSettings.Settings.Add(key, value);
            else
                config.AppSettings.Settings[key].Value = value;

            config.Save(ConfigurationSaveMode.Modified);
        }
    }
}
