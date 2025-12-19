using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Runtime.CompilerServices;
namespace IrisAuth.Models
{
    //public class AppSettingsModel
    //{
    //    public bool AllowLocalBiometricUpload { get; set; }

    //    public bool EnableGroupCreation { get; set; }
    //    public bool EnableUserCreation { get; set; }

    //    public string GroupPrefix { get; set; }
    //    public string UserPrefix { get; set; }

    //    public string GroupDescription { get; set; }
    //    public string UserDescription { get; set; }

    //    public string DefaultUserPassword { get; set; }
    //    public bool ForcePasswordChange { get; set; }
    //    public bool AutoAddUserToGroup { get; set; }

    //    public bool LogOperations { get; set; }

    //    /// <summary>
    //    /// Local | Domain
    //    /// </summary>
    //    public string Mode { get; set; }

    //    public string DomainName { get; set; }

    //    // ⭐ IMPORTANT for UI binding
    //   // public bool IsDomainMode => Mode == "Domain";
    //}
    //public class AppSettingsModel : INotifyPropertyChanged
    //{
    //    public event PropertyChangedEventHandler PropertyChanged;

    //    private string _mode;
    //    private string _domainName;

    //    public bool AllowLocalBiometricUpload { get; set; }
    //    public bool EnableGroupCreation { get; set; }
    //    public bool EnableUserCreation { get; set; }

    //    public string GroupPrefix { get; set; }
    //    public string UserPrefix { get; set; }
    //    public string GroupDescription { get; set; }
    //    public string UserDescription { get; set; }
    //    public string DefaultUserPassword { get; set; }
    //    public bool ForcePasswordChange { get; set; }
    //    public bool AutoAddUserToGroup { get; set; }

    //    public bool LogOperations { get; set; }

    //    /// <summary>Local | Domain</summary>
    //    public string Mode
    //    {
    //        get => _mode;
    //        set
    //        {
    //            _mode = value;
    //            OnPropertyChanged(nameof(Mode));
    //            OnPropertyChanged(nameof(IsDomainMode));
    //        }
    //    }

    //    public string DomainName
    //    {
    //        get => _domainName;
    //        set
    //        {
    //            _domainName = value;
    //            OnPropertyChanged(nameof(DomainName));
    //        }
    //    }

    //    public bool IsDomainMode => Mode == "Domain";

    //    protected void OnPropertyChanged(string prop)
    //        => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
    //}
    public class AppSettingsModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string prop = null)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));

        private bool _allowLocalBiometricUpload;
        public bool AllowLocalBiometricUpload
        {
            get => _allowLocalBiometricUpload;
            set { _allowLocalBiometricUpload = value; OnPropertyChanged(); }
        }

        private bool _enableGroupCreation;
        public bool EnableGroupCreation
        {
            get => _enableGroupCreation;
            set { _enableGroupCreation = value; OnPropertyChanged(); }
        }

        private bool _enableUserCreation;
        public bool EnableUserCreation
        {
            get => _enableUserCreation;
            set { _enableUserCreation = value; OnPropertyChanged(); }
        }

        public string GroupPrefix { get; set; }
        public string UserPrefix { get; set; }
        public string GroupDescription { get; set; }
        public string UserDescription { get; set; }
        public string DefaultUserPassword { get; set; }

        private bool _forcePasswordChange;
        public bool ForcePasswordChange
        {
            get => _forcePasswordChange;
            set { _forcePasswordChange = value; OnPropertyChanged(); }
        }

        private bool _autoAddUserToGroup;
        public bool AutoAddUserToGroup
        {
            get => _autoAddUserToGroup;
            set { _autoAddUserToGroup = value; OnPropertyChanged(); }
        }

        public bool LogOperations { get; set; }

        private string _mode;
        public string Mode
        {
            get => _mode;
            set
            {
                _mode = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(IsDomainMode));
            }
        }

        public string DomainName { get; set; }

        public bool IsDomainMode => Mode == "Domain";
    }
}
