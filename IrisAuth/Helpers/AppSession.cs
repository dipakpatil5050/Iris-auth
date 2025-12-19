using IrisAuth.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IrisAuth.Helpers
{
    public static class AppSession
    {
        public static event Action UserChanged;
        public static event Action SettingsChanged;
        public static AppSettingsModel CurrentSettings { get; private set; }
        private static UserModel _currentUser;
        public static UserModel CurrentUser
        {
            get => _currentUser;
            set
            {
                _currentUser = value;
                UserChanged?.Invoke();   // 🔥 notify UI
            }
        }

        public static bool IsSuperUser =>
            CurrentUser != null &&
            CurrentUser.Roles == "SuperAdmin";
        public static void RefreshSettings(AppSettingsModel settings)
        {
            CurrentSettings = settings;
            SettingsChanged?.Invoke(); // 🔥 live refresh
        }
    }
}
