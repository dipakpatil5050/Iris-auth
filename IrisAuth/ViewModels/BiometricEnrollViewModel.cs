//using IrisAuth.Repositories;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using System.Windows.Input;
//using System.Windows;
//using System.Windows.Media.Imaging;
//using System.IO;
//using IrisAuth.IrisSdk.Services;

//namespace IrisAuth.ViewModels
//{
//    //public class BiometricEnrollViewModel : ViewModelBase
//    //{
//    //    private readonly UserAccountRepository _repo;
//    //    private readonly IrisCaptureService _irisService;
//    //    private readonly int _userId;

//    //    public int? LeftQuality { get; private set; }
//    //    public int? RightQuality { get; private set; }

//    //    public string LeftImageBase64 { get; private set; }
//    //    public string RightImageBase64 { get; private set; }

//    //    private BitmapSource _leftPreview;
//    //    public BitmapSource LeftPreview
//    //    {
//    //        get => _leftPreview;
//    //        set { _leftPreview = value; RaisePropertyChanged(nameof(LeftPreview)); }
//    //    }

//    //    private BitmapSource _rightPreview;
//    //    public BitmapSource RightPreview
//    //    {
//    //        get => _rightPreview;
//    //        set { _rightPreview = value; RaisePropertyChanged(nameof(RightPreview)); }
//    //    }

//    //    private string _errorMessage;
//    //    public string ErrorMessage
//    //    {
//    //        get => _errorMessage;
//    //        set { _errorMessage = value; RaisePropertyChanged(nameof(ErrorMessage)); }
//    //    }

//    //    public string LeftQualityText =>
//    //        LeftQuality.HasValue ? $"Left Eye Quality: {LeftQuality}" : "Left eye not captured";

//    //    public string RightQualityText =>
//    //        RightQuality.HasValue ? $"Right Eye Quality: {RightQuality}" : "Right eye not captured";

//    //    public bool CanSave => LeftQuality.HasValue && RightQuality.HasValue;

//    //    public ICommand CaptureLeftCommand { get; }
//    //    public ICommand CaptureRightCommand { get; }
//    //    public ICommand SaveCommand { get; }
//    //    public ICommand CancelCommand { get; }   // ✅ ADD
//    //    public Action CloseAction { get; set; }

//    //    public BiometricEnrollViewModel(int userId)
//    //    {
//    //        _userId = userId;
//    //        _repo = new UserAccountRepository();
//    //        _irisService = new IrisCaptureService();

//    //        CaptureLeftCommand = new ViewModelCommand(async _ => await CaptureLeft());
//    //        CaptureRightCommand = new ViewModelCommand(async _ => await CaptureRight());
//    //        SaveCommand = new ViewModelCommand(_ => Save(), _ => CanSave);
//    //    }

//    //    private async Task CaptureLeft()
//    //    {
//    //        ErrorMessage = null;

//    //        var result = await _irisService.CaptureAsync(5000, 40);

//    //        if (!result.Success)
//    //        {
//    //            ErrorMessage = result.Error;
//    //            return;
//    //        }

//    //        LeftQuality = result.Quality;
//    //        LeftImageBase64 = result.ImageBase64;
//    //        LeftPreview = Base64ToBitmap(result.ImageBase64);

//    //        RaisePropertyChanged(nameof(LeftQualityText));
//    //        RaisePropertyChanged(nameof(CanSave));
//    //    }

//    //    private async Task CaptureRight()
//    //    {
//    //        ErrorMessage = null;

//    //        var result = await _irisService.CaptureAsync(5000, 40);

//    //        if (!result.Success)
//    //        {
//    //            ErrorMessage = result.Error;
//    //            return;
//    //        }

//    //        RightQuality = result.Quality;
//    //        RightImageBase64 = result.ImageBase64;
//    //        RightPreview = Base64ToBitmap(result.ImageBase64);

//    //        RaisePropertyChanged(nameof(RightQualityText));
//    //        RaisePropertyChanged(nameof(CanSave));
//    //    }

//    //    private void Save()
//    //    {
//    //        _repo.SaveBiometric(
//    //            _userId,
//    //            LeftImageBase64,
//    //            RightImageBase64,
//    //            LeftQuality.Value,
//    //            RightQuality.Value
//    //        );

//    //        CloseAction?.Invoke();
//    //    }

//    //    private BitmapSource Base64ToBitmap(string base64)
//    //    {
//    //        byte[] bytes = Convert.FromBase64String(base64);
//    //        using (var ms = new MemoryStream(bytes))
//    //        {
//    //            var bmp = new BitmapImage();
//    //            bmp.BeginInit();
//    //            bmp.CacheOption = BitmapCacheOption.OnLoad;
//    //            bmp.StreamSource = ms;
//    //            bmp.EndInit();
//    //            bmp.Freeze();
//    //            return bmp;
//    //        }
//    //    }
//    //}
//    public class BiometricEnrollViewModel : ViewModelBase
//    {
//        private readonly UserAccountRepository _repo;
//        private readonly IrisCaptureService _irisService;
//        private readonly int _userId;

//        // =======================
//        // BIOMETRIC DATA
//        // =======================

//        public int? LeftQuality { get; private set; }
//        public int? RightQuality { get; private set; }

//        public string LeftImageBase64 { get; private set; }
//        public string RightImageBase64 { get; private set; }

//        // =======================
//        // IMAGE PREVIEW
//        // =======================

//        private BitmapSource _leftPreview;
//        public BitmapSource LeftPreview
//        {
//            get => _leftPreview;
//            set
//            {
//                _leftPreview = value;
//                RaisePropertyChanged(nameof(LeftPreview));
//            }
//        }

//        private BitmapSource _rightPreview;
//        public BitmapSource RightPreview
//        {
//            get => _rightPreview;
//            set
//            {
//                _rightPreview = value;
//                RaisePropertyChanged(nameof(RightPreview));
//            }
//        }

//        // =======================
//        // UI STATE
//        // =======================

//        private string _errorMessage;
//        public string ErrorMessage
//        {
//            get => _errorMessage;
//            set
//            {
//                _errorMessage = value;
//                RaisePropertyChanged(nameof(ErrorMessage));
//            }
//        }

//        public string LeftQualityText =>
//            LeftQuality.HasValue
//                ? $"Left Eye Quality: {LeftQuality}"
//                : "Left eye not captured";

//        public string RightQualityText =>
//            RightQuality.HasValue
//                ? $"Right Eye Quality: {RightQuality}"
//                : "Right eye not captured";

//        public bool CanSave =>
//            LeftQuality.HasValue && RightQuality.HasValue;

//        // =======================
//        // COMMANDS
//        // =======================

//        public ICommand CaptureLeftCommand { get; }
//        public ICommand CaptureRightCommand { get; }
//        public ICommand SaveCommand { get; }
//        public ICommand CancelCommand { get; }

//        // =======================
//        // DIALOG CLOSE HOOK
//        // =======================

//        public Action CloseAction { get; set; }

//        // =======================
//        // CONSTRUCTOR
//        // =======================

//        public BiometricEnrollViewModel(int userId)
//        {
//            _userId = userId;
//            _repo = new UserAccountRepository();
//            _irisService = new IrisCaptureService();

//            CaptureLeftCommand = new ViewModelCommand(async _ => await CaptureLeft());
//            CaptureRightCommand = new ViewModelCommand(async _ => await CaptureRight());
//            SaveCommand = new ViewModelCommand(_ => Save(), _ => CanSave);
//            CancelCommand = new ViewModelCommand(_ => CloseAction?.Invoke());
//        }

//        // =======================
//        // CAPTURE METHODS
//        // =======================

//        private async Task CaptureLeft()
//        {
//            ErrorMessage = null;

//            var result = await _irisService.CaptureAsync(timeoutMs: 5000, minQuality: 40);

//            if (!result.Success)
//            {
//                ErrorMessage = result.Error;
//                return;
//            }

//            LeftQuality = result.Quality;
//            LeftImageBase64 = result.ImageBase64;
//            LeftPreview = Base64ToBitmap(result.ImageBase64);

//            RaisePropertyChanged(nameof(LeftQualityText));
//            RaisePropertyChanged(nameof(CanSave));
//            CommandManager.InvalidateRequerySuggested();
//        }

//        private async Task CaptureRight()
//        {
//            ErrorMessage = null;

//            var result = await _irisService.CaptureAsync(timeoutMs: 5000, minQuality: 40);

//            if (!result.Success)
//            {
//                ErrorMessage = result.Error;
//                return;
//            }

//            RightQuality = result.Quality;
//            RightImageBase64 = result.ImageBase64;
//            RightPreview = Base64ToBitmap(result.ImageBase64);

//            RaisePropertyChanged(nameof(RightQualityText));
//            RaisePropertyChanged(nameof(CanSave));
//            CommandManager.InvalidateRequerySuggested();
//        }

//        // =======================
//        // SAVE
//        // =======================

//        private void Save()
//        {
//            _repo.SaveBiometric(
//                _userId,
//                LeftImageBase64,
//                RightImageBase64,
//                LeftQuality.Value,
//                RightQuality.Value
//            );

//            CloseAction?.Invoke();
//        }

//        // =======================
//        // HELPERS
//        // =======================

//        private BitmapSource Base64ToBitmap(string base64)
//        {
//            byte[] bytes = Convert.FromBase64String(base64);

//            using (var ms = new MemoryStream(bytes))
//            {
//                var bmp = new BitmapImage();
//                bmp.BeginInit();
//                bmp.CacheOption = BitmapCacheOption.OnLoad;
//                bmp.StreamSource = ms;
//                bmp.EndInit();
//                bmp.Freeze();
//                return bmp;
//            }
//        }
//    }
//}
using IrisAuth.Repositories;
using IrisAuth.IrisSdk.Services;
using System;
using System.Configuration;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace IrisAuth.ViewModels
{
    public class BiometricEnrollViewModel : ViewModelBase
    {
        private readonly UserAccountRepository _repo;
        private readonly IrisCaptureService _irisService;
        private readonly int _userId;

        // ================= ENROLL LOCK =================
        private const string EnrollLockPath = @"C:\ProgramData\IrisAuth\enroll.lock";

        private void CreateEnrollLock()
        {
            Directory.CreateDirectory(@"C:\ProgramData\IrisAuth");
            File.WriteAllText(EnrollLockPath, DateTime.Now.ToString("O"));
        }

        private void RemoveEnrollLock()
        {
            if (File.Exists(EnrollLockPath))
                File.Delete(EnrollLockPath);
        }

        // ================= CONFIG =================
        public bool AllowLocalUpload =>
            ConfigurationManager.AppSettings["AllowLocalBiometricUpload"] == "true";

        // ================= DEVICE STATUS =================
        private bool _deviceConnected;
        public bool DeviceConnected
        {
            get => _deviceConnected;
            set
            {
                _deviceConnected = value;
                RaisePropertyChanged(nameof(DeviceConnected));
                RaisePropertyChanged(nameof(DeviceStatusText));
            }
        }

        public string DeviceStatusText =>
            DeviceConnected ? "🟢 Iris Device Connected" : "🔴 Iris Device Not Found";

        // ================= SOURCE =================
        private bool _useIrisDevice = true;

        public bool UseIrisDevice
        {
            get => _useIrisDevice;
            set
            {
                _useIrisDevice = value;
                RaisePropertyChanged(nameof(UseIrisDevice));
                RaisePropertyChanged(nameof(UseLocalFile));
            }
        }

        public bool UseLocalFile
        {
            get => !_useIrisDevice;
            set
            {
                _useIrisDevice = !value;
                RaisePropertyChanged(nameof(UseLocalFile));
                RaisePropertyChanged(nameof(UseIrisDevice));
            }
        }

        // ================= BIOMETRIC =================
        public int? LeftQuality { get; private set; }
        public int? RightQuality { get; private set; }

        public string LeftImageBase64 { get; private set; }
        public string RightImageBase64 { get; private set; }
        private string _errorMessage;
        public string ErrorMessage
        {
            get { return _errorMessage; }
            set
            {
                _errorMessage = value;
                RaisePropertyChanged(nameof(ErrorMessage));
            }
        }
        private BitmapSource _leftPreview;
        public BitmapSource LeftPreview
        {
            get => _leftPreview;
            set { _leftPreview = value; RaisePropertyChanged(nameof(LeftPreview)); }
        }

        private BitmapSource _rightPreview;
        public BitmapSource RightPreview
        {
            get => _rightPreview;
            set { _rightPreview = value; RaisePropertyChanged(nameof(RightPreview)); }
        }

        public string LeftQualityText =>
            LeftQuality.HasValue ? $"Left Eye Quality: {LeftQuality}" : "Left eye not captured";

        public string RightQualityText =>
            RightQuality.HasValue ? $"Right Eye Quality: {RightQuality}" : "Right eye not captured";

        public bool CanSave => LeftQuality.HasValue && RightQuality.HasValue;

        // ================= COMMANDS =================
        public ICommand CaptureLeftCommand { get; }
        public ICommand CaptureRightCommand { get; }
        public ICommand SaveCommand { get; }
        public ICommand CancelCommand { get; }

        public Action CloseAction { get; set; }

        // ================= CTOR =================
        public BiometricEnrollViewModel(int userId)
        {
            _userId = userId;
            _repo = new UserAccountRepository();
            _irisService = new IrisCaptureService();

            CreateEnrollLock();

            DeviceConnected = _irisService.IsDeviceConnected();

            if (!AllowLocalUpload)
                UseIrisDevice = true;

            CaptureLeftCommand = new ViewModelCommand(async _ => await Capture(true));
            CaptureRightCommand = new ViewModelCommand(async _ => await Capture(false));
            SaveCommand = new ViewModelCommand(_ => Save(), _ => CanSave);
            CancelCommand = new ViewModelCommand(_ =>
            {
                RemoveEnrollLock();
                CloseAction?.Invoke();
            });
        }

        private async Task Capture(bool isLeft)
        {
            if (UseIrisDevice)
                await CaptureFromDevice(isLeft);
            else
                LoadFromFile(isLeft);
        }

        private async Task CaptureFromDevice(bool isLeft)
        {
            var r = await _irisService.CaptureAsync(5000, 40);
            if (!r.Success)
            {
                ErrorMessage = r.Error;
                return;
            }
            if (isLeft)
            {
                LeftImageBase64 = r.ImageBase64;
                LeftQuality = r.Quality;
                LeftPreview = Base64ToBitmap(r.ImageBase64);
            }
            else
            {
                RightImageBase64 = r.ImageBase64;
                RightQuality = r.Quality;
                RightPreview = Base64ToBitmap(r.ImageBase64);
            }

            RaisePropertyChanged(nameof(CanSave));
        }

        private void LoadFromFile(bool isLeft)
        {
            var dlg = new Microsoft.Win32.OpenFileDialog
            {
                Filter = "Image Files (*.png;*.jpg;*.bmp)|*.png;*.jpg;*.bmp"
            };

            if (dlg.ShowDialog() != true) return;

            byte[] b = File.ReadAllBytes(dlg.FileName);
            string base64 = Convert.ToBase64String(b);
            BitmapImage img = new BitmapImage();

            using (var ms = new MemoryStream(b))
            {
                img.BeginInit();
                img.CacheOption = BitmapCacheOption.OnLoad;
                img.StreamSource = ms;
                img.EndInit();
                img.Freeze();
            }

            if (isLeft)
            {
                LeftImageBase64 = base64;
                LeftPreview = img;
                LeftQuality = 80;
            }
            else
            {
                RightImageBase64 = base64;
                RightPreview = img;
                RightQuality = 82;
            }

            RaisePropertyChanged(nameof(CanSave));
        }

        private void Save()
        {
            _repo.SaveBiometric(
                _userId,
                LeftImageBase64,
                RightImageBase64,
                LeftQuality.Value,
                RightQuality.Value);

            RemoveEnrollLock();
            CloseAction?.Invoke();
        }

        private BitmapSource Base64ToBitmap(string base64)
        {
            byte[] b = Convert.FromBase64String(base64);
            using (var ms = new MemoryStream(b))
            {
                BitmapImage img = new BitmapImage();
                img.BeginInit();
                img.CacheOption = BitmapCacheOption.OnLoad;
                img.StreamSource = ms;
                img.EndInit();
                img.Freeze();
                return img;
            }
        }
    }
}

