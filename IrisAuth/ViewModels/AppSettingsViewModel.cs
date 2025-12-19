using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using IrisAuth.Helpers;
using IrisAuth.Models;
using IrisAuth.Services;
namespace IrisAuth.ViewModels
{
    //public class AppSettingsViewModel : ViewModelBase
    //{
    //    private readonly AppSettingsService _service = new AppSettingsService();

    //    public AppSettingsModel Settings { get; }

    //    public bool CanEditSettings => AppSession.IsSuperUser;

    //    public ICommand SaveCommand { get; }

    //    public AppSettingsViewModel()
    //    {
    //        Settings = _service.Load();

    //        SaveCommand = new ViewModelCommand(
    //            _ => Save(),
    //            _ => CanEditSettings);
    //    }

    //    private void Save()
    //    {
    //        if (!CanEditSettings)
    //        {
    //            MessageBox.Show("Only Super Admin can modify settings.");
    //            return;
    //        }

    //        _service.Save(Settings);

    //        MessageBox.Show(
    //            "Settings saved successfully.",
    //            "Success",
    //            MessageBoxButton.OK,
    //            MessageBoxImage.Information);
    //    }
    //}
    //public class AppSettingsViewModel : ViewModelBase
    //{
    //    private readonly AppSettingsService _service = new AppSettingsService();

    //    public AppSettingsModel Settings { get; }

    //    public bool CanEditSettings => AppSession.IsSuperUser;

    //    public ICommand SaveCommand { get; }

    //    public AppSettingsViewModel()
    //    {
    //        Settings = _service.Load();

    //        SaveCommand = new ViewModelCommand(
    //            _ => Save(),
    //            _ => CanEditSettings
    //        );
    //    }

    //    private void Save()
    //    {
    //        if (!CanEditSettings)
    //            return;

    //        _service.Save(Settings);

    //        // ✅ Live apply (no restart)
    //        AppSession.RefreshSettings(Settings);

    //        MessageBox.Show(
    //            "Settings applied successfully.",
    //            "Application Settings",
    //            MessageBoxButton.OK,
    //            MessageBoxImage.Information);
    //    }
    //}
    public class AppSettingsViewModel : ViewModelBase
    {
        private readonly AppSettingsService _service = new AppSettingsService();

        public AppSettingsModel Settings { get; }

        public bool CanEditSettings => AppSession.IsSuperUser;

        public ICommand SaveCommand { get; }

        public AppSettingsViewModel()
        {
            Settings = _service.Load();

            SaveCommand = new ViewModelCommand(
                _ => Save(),
                _ => CanEditSettings
            );
        }

        private void Save()
        {
            if (!CanEditSettings)
                return;

            _service.Save(Settings);

            // 🔥 live apply (no restart)
            AppSession.RefreshSettings(Settings);

            MessageBox.Show(
                "Settings applied successfully.",
                "Application Settings",
                MessageBoxButton.OK,
                MessageBoxImage.Information);
        }
    }
}
