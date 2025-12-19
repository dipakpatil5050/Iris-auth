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
        private readonly WindowsUserService _windowsUserService
            = new WindowsUserService();
        private readonly AuditService _audit = new AuditService();
        public string DialogTitle { get; }
        public bool IsEditMode { get; }

        public int? UserId { get; }
        private readonly int _originalGroupId;
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
        private string GetGroupNameById(int groupId)
        {
            var group = Groups.FirstOrDefault(g => g.GroupId == groupId);
            return group?.GroupName;
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
            _originalGroupId = user?.GroupId ?? 0;
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

        //private void Save()
        //{
        //    ErrorMessage = null;

        //    if (!CanSave())
        //    {
        //        ErrorMessage = "Please fill all required fields.";
        //        return;
        //    }

        //    if (IsEditMode)
        //    {
        //        _userRepo.UpdateUser(UserId.Value, GroupId, IsBiometricEnabled);
        //    }
        //    else
        //    {
        //        var hash = PasswordHasher.Hash(Password);
        //        _userRepo.CreateUser(Username, hash, GroupId);
        //    }

        //    CloseAction?.Invoke();
        //}
        private void Save()
        {
            ErrorMessage = null;

            if (!CanSave())
            {
                ErrorMessage = "Please fill all required fields.";
                return;
            }

            try
            {
                if (IsEditMode)
                {
                    var oldGroup = GetGroupNameById(_originalGroupId);
                    var newGroup = GetGroupNameById(GroupId);

                    if (oldGroup != newGroup)
                    {
                        _windowsUserService.SyncUserGroup(Username, oldGroup, newGroup);
                        _audit.Log(
                                "admin",
                                "USER_GROUP_CHANGE",
                                $"User {Username} group changed",
                                oldGroup,
                                newGroup,
                                "Role change approved"
                               );
                    }
                    // 🔹 Only DB update
                    _userRepo.UpdateUser(UserId.Value, GroupId, IsBiometricEnabled);

                }
                else
                {
                    // 🔹 Create Windows User + add to Windows Group
                    var groupName = GetGroupNameById(GroupId);

                    if (string.IsNullOrWhiteSpace(groupName))
                    {
                        ErrorMessage = "Invalid group selected.";
                        return;
                    }

                    _windowsUserService.CreateLocalUser(
                        Username,
                        groupName,
                        Password
                    );

                    // 🔹 Create IrisAuth DB user
                    var hash = PasswordHasher.Hash(Password);
                    _userRepo.CreateUser(Username, hash, GroupId);
                      _audit.Log(
                        "admin", // later: CurrentUser
                        "USER_CREATE",
                        $"User {Username} created",
                        null,
                        Username,
                        "New operator onboarding"
                    );
                }

                CloseAction?.Invoke();
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
            }
        }
    }
}
