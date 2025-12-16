using IrisAuth.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using System.Windows.Media.Imaging;
using System.IO;
using IrisAuth.IrisSdk.Services;

namespace IrisAuth.ViewModels
{
    public class BiometricEnrollViewModel : ViewModelBase
    {
        private readonly UserAccountRepository _repo;
        private readonly IrisCaptureService _irisService;
        private readonly int _userId;

        public int? LeftQuality { get; private set; }
        public int? RightQuality { get; private set; }

        public string LeftImageBase64 { get; private set; }
        public string RightImageBase64 { get; private set; }

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

        private string _errorMessage;
        public string ErrorMessage
        {
            get => _errorMessage;
            set { _errorMessage = value; RaisePropertyChanged(nameof(ErrorMessage)); }
        }

        public string LeftQualityText =>
            LeftQuality.HasValue ? $"Left Eye Quality: {LeftQuality}" : "Left eye not captured";

        public string RightQualityText =>
            RightQuality.HasValue ? $"Right Eye Quality: {RightQuality}" : "Right eye not captured";

        public bool CanSave => LeftQuality.HasValue && RightQuality.HasValue;

        public ICommand CaptureLeftCommand { get; }
        public ICommand CaptureRightCommand { get; }
        public ICommand SaveCommand { get; }

        public Action CloseAction { get; set; }

        public BiometricEnrollViewModel(int userId)
        {
            _userId = userId;
            _repo = new UserAccountRepository();
            _irisService = new IrisCaptureService();

            CaptureLeftCommand = new ViewModelCommand(async _ => await CaptureLeft());
            CaptureRightCommand = new ViewModelCommand(async _ => await CaptureRight());
            SaveCommand = new ViewModelCommand(_ => Save(), _ => CanSave);
        }

        private async Task CaptureLeft()
        {
            ErrorMessage = null;

            var result = await _irisService.CaptureAsync(5000, 40);

            if (!result.Success)
            {
                ErrorMessage = result.Error;
                return;
            }

            LeftQuality = result.Quality;
            LeftImageBase64 = result.ImageBase64;
            LeftPreview = Base64ToBitmap(result.ImageBase64);

            RaisePropertyChanged(nameof(LeftQualityText));
            RaisePropertyChanged(nameof(CanSave));
        }

        private async Task CaptureRight()
        {
            ErrorMessage = null;

            var result = await _irisService.CaptureAsync(5000, 40);

            if (!result.Success)
            {
                ErrorMessage = result.Error;
                return;
            }

            RightQuality = result.Quality;
            RightImageBase64 = result.ImageBase64;
            RightPreview = Base64ToBitmap(result.ImageBase64);

            RaisePropertyChanged(nameof(RightQualityText));
            RaisePropertyChanged(nameof(CanSave));
        }

        private void Save()
        {
            _repo.SaveBiometric(
                _userId,
                LeftImageBase64,
                RightImageBase64,
                LeftQuality.Value,
                RightQuality.Value
            );

            CloseAction?.Invoke();
        }

        private BitmapSource Base64ToBitmap(string base64)
        {
            byte[] bytes = Convert.FromBase64String(base64);
            using (var ms = new MemoryStream(bytes))
            {
                var bmp = new BitmapImage();
                bmp.BeginInit();
                bmp.CacheOption = BitmapCacheOption.OnLoad;
                bmp.StreamSource = ms;
                bmp.EndInit();
                bmp.Freeze();
                return bmp;
            }
        }
    }
}
