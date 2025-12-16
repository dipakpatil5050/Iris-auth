using IrisAuth.Models;
using IrisAuth.Repositories;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IrisAuth.Helpers;
using System.Windows.Input;
using IrisAuth.Services;
namespace IrisAuth.ViewModels
{

    public class UserEditDialogViewModel : ViewModelBase
    {
        private readonly UserAccountRepository _userRepo = new UserAccountRepository();

        public string DialogTitle { get; }
        public bool IsEditMode { get; }

        public int? UserId { get; }

        private string _username;
        public string Username
        {
            get => _username;
            set
            {
                _username = value;
                OnPropertyChanged(nameof(Username));
                CommandManager.InvalidateRequerySuggested();
            }
        }

        private int _groupId;
        public int GroupId
        {
            get => _groupId;
            set
            {
                _groupId = value;
                OnPropertyChanged(nameof(GroupId));
            }
        }

        private bool _isBiometricEnabled;
        public bool IsBiometricEnabled
        {
            get => _isBiometricEnabled;
            set
            {
                _isBiometricEnabled = value;
                OnPropertyChanged(nameof(IsBiometricEnabled));
            }
        }

        public string Password { get; set; }

        public ObservableCollection<GroupPermissionsModel> Groups { get; }

        private string _errorMessage;
        public string ErrorMessage
        {
            get => _errorMessage;
            set
            {
                _errorMessage = value;
                OnPropertyChanged(nameof(ErrorMessage));
            }
        }

        public ICommand SaveCommand { get; }
        public ICommand CancelCommand { get; }

        public Action CloseAction { get; set; }

        public UserEditDialogViewModel(UserAccountModel user = null)
        {
            Groups = new ObservableCollection<GroupPermissionsModel>(_userRepo.GetGroups());

            if (user == null)
            {
                DialogTitle = "Add User";
                IsEditMode = false;
            }
            else
            {
                DialogTitle = "Edit User";
                IsEditMode = true;

                UserId = user.UserId;
                Username = user.Username;
                GroupId = user.GroupId;
                IsBiometricEnabled = user.IsBiometricEnabled;
            }

            SaveCommand = new ViewModelCommand(
                _ => Save(),
                _ => CanSave());

            CancelCommand = new ViewModelCommand(_ => CloseAction?.Invoke());
        }

        private bool CanSave()
        {
            if (string.IsNullOrWhiteSpace(Username))
                return false;

            if (!IsEditMode && string.IsNullOrWhiteSpace(Password))
                return false;

            return true;
        }

        private void Save()
        {
            ErrorMessage = null;

            if (!CanSave())
            {
                ErrorMessage = "Please fill all required fields.";
                return;
            }

            if (IsEditMode)
            {
                _userRepo.UpdateUser(UserId.Value, GroupId, IsBiometricEnabled);
            }
            else
            {
                var hash = PasswordHasher.Hash(Password);
                _userRepo.CreateUser(Username, hash, GroupId);
            }

            CloseAction?.Invoke();
        }
    }
}
