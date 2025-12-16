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
    //public class UserEditDialogViewModel : ViewModelBase
    //{
    //    //    private readonly IUserRepository _userRepo;
    //    //    private readonly UserProvisioningService _provisioning;

    //    //    public string DialogTitle { get; }
    //    //    public bool IsEditMode { get; }

    //    //    public int? UserId { get; }
    //    //    public string Username { get; set; }
    //    //    public string Password { get; set; }
    //    //    public int GroupId { get; set; }
    //    //    public bool IsBiometricEnabled { get; set; }

    //    //    public ObservableCollection<GroupPermissionsModel> Groups { get; }

    //    //    public ICommand SaveCommand { get; }
    //    //    public ICommand CancelCommand { get; }

    //    //    public Action CloseAction { get; set; }

    //    //    //public UserEditDialogViewModel(UserAccountModel user = null)
    //    //    //{
    //    //    //    Groups = new ObservableCollection<GroupPermissionsModel>(
    //    //    //        _userRepo.GetGroups());

    //    //    //    if (user == null)
    //    //    //    {
    //    //    //        DialogTitle = "Add User";
    //    //    //        IsEditMode = false;
    //    //    //    }
    //    //    //    else
    //    //    //    {
    //    //    //        DialogTitle = "Edit User";
    //    //    //        IsEditMode = true;

    //    //    //        UserId = user.UserId;
    //    //    //        Username = user.Username;
    //    //    //        GroupId = user.GroupId;
    //    //    //        IsBiometricEnabled = user.IsBiometricEnabled;
    //    //    //    }

    //    //    //    SaveCommand = new ViewModelCommand(_ => Save());
    //    //    //    CancelCommand = new ViewModelCommand(_ => CloseAction?.Invoke());
    //    //    //}

    //    //    //private void Save()
    //    //    //{
    //    //    //    var selectedGroup = Groups.First(g => g.GroupId == GroupId);

    //    //    //    if (IsEditMode)
    //    //    //    {
    //    //    //        _provisioning.UpdateUser(
    //    //    //            UserId.Value,
    //    //    //            GroupId,
    //    //    //            IsBiometricEnabled
    //    //    //        );
    //    //    //    }
    //    //    //    else
    //    //    //    {
    //    //    //        _provisioning.CreateUser(
    //    //    //            Username,
    //    //    //            Password,               // plain password only here
    //    //    //            GroupId,
    //    //    //            selectedGroup.GroupName // Windows group name
    //    //    //        );
    //    //    //    }

    //    //    //    CloseAction?.Invoke();
    //    //    //}
    //    //    public UserEditDialogViewModel(IUserRepository userRepo,
    //    //UserProvisioningService provisioning,
    //    //UserAccountModel user = null))
    //    //    {
    //    //        Groups = new ObservableCollection<GroupPermissionsModel>(
    //    //            _userRepo.GetGroups());

    //    //        if (user == null)
    //    //        {
    //    //            DialogTitle = "Add User";
    //    //            IsEditMode = false;
    //    //        }
    //    //        else
    //    //        {
    //    //            DialogTitle = "Edit User";
    //    //            IsEditMode = true;

    //    //            UserId = user.UserId;
    //    //            Username = user.Username;
    //    //            GroupId = user.GroupId;
    //    //            IsBiometricEnabled = user.IsBiometricEnabled;
    //    //        }

    //    //        SaveCommand = new ViewModelCommand(_ => Save());
    //    //        CancelCommand = new ViewModelCommand(_ => CloseAction?.Invoke());
    //    //    }

    //    //    private void Save()
    //    //    {
    //    //        var selectedGroup = Groups.First(g => g.GroupId == GroupId);

    //    //        if (IsEditMode)
    //    //        {
    //    //            _provisioning.UpdateUser(
    //    //                UserId.Value,
    //    //                GroupId,
    //    //                IsBiometricEnabled
    //    //            );
    //    //        }
    //    //        else
    //    //        {
    //    //            _provisioning.CreateUser(
    //    //                Username,
    //    //                Password,
    //    //                GroupId,
    //    //                selectedGroup.GroupName
    //    //            );
    //    //        }

    //    //        CloseAction?.Invoke();
    //    //    }
    //    private readonly UserAccountRepository _userRepo = new UserAccountRepository();

    //    public string DialogTitle { get; }
    //    public bool IsEditMode { get; }

    //    public int? UserId { get; }
    //    public string Username { get; set; }
    //    public string Password { get; set; }
    //    public int GroupId { get; set; }
    //    public bool IsBiometricEnabled { get; set; }

    //    public ObservableCollection<GroupPermissionsModel> Groups { get; }

    //    public ICommand SaveCommand { get; }
    //    public ICommand CancelCommand { get; }

    //    public Action CloseAction { get; set; }

    //    public UserEditDialogViewModel(UserAccountModel user = null)
    //    {
    //        Groups = new ObservableCollection<GroupPermissionsModel>(
    //            _userRepo.GetGroups());

    //        if (user == null)
    //        {
    //            DialogTitle = "Add User";
    //            IsEditMode = false;
    //        }
    //        else
    //        {
    //            DialogTitle = "Edit User";
    //            IsEditMode = true;

    //            UserId = user.UserId;
    //            Username = user.Username;
    //            GroupId = user.GroupId;
    //            IsBiometricEnabled = user.IsBiometricEnabled;
    //        }

    //        SaveCommand = new ViewModelCommand(_ => Save());
    //        CancelCommand = new ViewModelCommand(_ => CloseAction?.Invoke());
    //    }

    //    private void Save()
    //    {
    //        if (IsEditMode)
    //        {
    //            _userRepo.UpdateUser(UserId.Value, GroupId, IsBiometricEnabled);
    //        }
    //        else
    //        {
    //            var hash = PasswordHasher.Hash(Password);
    //            _userRepo.CreateUser(Username, hash, GroupId);
    //        }

    //        CloseAction?.Invoke();
    //    }
    //}
    public class UserEditDialogViewModel : ViewModelBase
    {
        private readonly UserAccountRepository _userRepo = new UserAccountRepository();


        public string DialogTitle { get; }
        public bool IsEditMode { get; }

        public int? UserId { get; }
        public string Username { get; set; }
        public int GroupId { get; set; }
        public bool IsBiometricEnabled { get; set; }

        // Password is set from code-behind
        public string Password { get; set; }

        public ObservableCollection<GroupPermissionsModel> Groups { get; }

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

            SaveCommand = new ViewModelCommand(_ => Save());
            CancelCommand = new ViewModelCommand(_ => CloseAction?.Invoke());
        }

        private void Save()
        {
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
