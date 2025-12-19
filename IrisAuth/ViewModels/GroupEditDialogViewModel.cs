using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IrisAuth.Models;
using IrisAuth.Repositories;
using System.Windows.Input;
using System.Collections.ObjectModel;
using IrisAuth.Services;
using System.Windows;
namespace IrisAuth.ViewModels
{

  
    public class GroupEditDialogViewModel : ViewModelBase
    {
        private readonly GroupRepository _repo = new GroupRepository();
        private readonly WindowsGroupService _windowsGroupService
            = new WindowsGroupService();
        private readonly AuditService _audit = new AuditService();
        public string DialogTitle { get; }
        public bool IsEditMode { get; }

        public int? GroupId { get; }

        public string GroupName { get; set; }
        private readonly string _originalGroupName;
        // ================= LOGIN TYPE =================
        public ObservableCollection<LoginTypeItem> LoginTypes { get; }

        private int _loginType;
        public int LoginType
        {
            get => _loginType;
            set
            {
                _loginType = value;
                OnPropertyChanged(nameof(LoginType));
                OnPropertyChanged(nameof(IsLoginTimeoutVisible));
            }
        }

        // ================= LOGIN TIMEOUT (MINUTES - UI) =================
        private int? _loginTimeoutMinutes;
        public int? LoginTimeoutMinutes
        {
            get => _loginTimeoutMinutes;
            set
            {
                _loginTimeoutMinutes = value;
                OnPropertyChanged(nameof(LoginTimeoutMinutes));
            }
        }

        public bool IsLoginTimeoutVisible =>
            LoginType == 2 || LoginType == 3;

        // ================= PERMISSIONS =================
        public bool Permission1 { get; set; }
        public bool Permission2 { get; set; }
        public bool Permission3 { get; set; }
        public bool Permission4 { get; set; }
        public bool Permission5 { get; set; }
        public bool Permission6 { get; set; }

        private string _errorMessage;
        public string ErrorMessage
        {
            get => _errorMessage;
            set { _errorMessage = value; OnPropertyChanged(nameof(ErrorMessage)); }
        }

        public ICommand SaveCommand { get; }
        public ICommand CancelCommand { get; }

        public Action CloseAction { get; set; }

        public GroupEditDialogViewModel(GroupPermissionsModel group = null)
        {
            LoginTypes = new ObservableCollection<LoginTypeItem>
            {
                new LoginTypeItem(1, "Off"),
                new LoginTypeItem(2, "Absolute"),
                new LoginTypeItem(3, "Inactive")
            };

            if (group == null)
            {
                DialogTitle = "Add Group";
                LoginType = 1;
                _originalGroupName = null;   // ✅ SAFE
            }
            else
            {
                DialogTitle = "Edit Group";
                IsEditMode = true;

                GroupId = group.GroupId;
                GroupName = group.GroupName;
                _originalGroupName = group.GroupName;  // ✅ SAFE
                LoginType = group.LoginType ?? 1;

                // Convert SECONDS → MINUTES
                LoginTimeoutMinutes = group.LoginTimeout.HasValue
                    ? group.LoginTimeout / 60
                    : null;

                Permission1 = group.Permission1;
                Permission2 = group.Permission2;
                Permission3 = group.Permission3;
                Permission4 = group.Permission4;
                Permission5 = group.Permission5;
                Permission6 = group.Permission6;
            }

            SaveCommand = new ViewModelCommand(_ => Save());
            CancelCommand = new ViewModelCommand(_ => CloseAction?.Invoke());
           // _originalGroupName = group.GroupName;
        }

        //private void Save()
        //{
        //    if (string.IsNullOrWhiteSpace(GroupName))
        //    {
        //        ErrorMessage = "Group name is required.";
        //        return;
        //    }

        //    if (IsLoginTimeoutVisible &&
        //        (!LoginTimeoutMinutes.HasValue || LoginTimeoutMinutes <= 0))
        //    {
        //        ErrorMessage = "Login timeout must be greater than 0 minutes.";
        //        return;
        //    }
        //    // ✅ 1. Create Windows Group ONLY for new group
        //    if (!IsEditMode)
        //    {
        //        _windowsGroupService.CreateLocalGroup(GroupName);
        //    }

        //    // ✅ 2. Save in application database
        //    var model = new GroupPermissionsModel
        //    {
        //        GroupId = GroupId ?? 0,
        //        GroupName = GroupName,
        //        LoginType = LoginType,

        //        // Convert MINUTES → SECONDS
        //        LoginTimeout = IsLoginTimeoutVisible
        //            ? LoginTimeoutMinutes * 60
        //            : null,

        //        Permission1 = Permission1,
        //        Permission2 = Permission2,
        //        Permission3 = Permission3,
        //        Permission4 = Permission4,
        //        Permission5 = Permission5,
        //        Permission6 = Permission6
        //    };

        //    if (IsEditMode)
        //        _repo.UpdateGroup(model);
        //    else
        //        _repo.CreateGroup(model);

        //    CloseAction?.Invoke();
        //}
        private void Save()
        {
            ErrorMessage = null;

            if (string.IsNullOrWhiteSpace(GroupName))
            {
                ErrorMessage = "Group name is required.";
                return;
            }

            if (IsLoginTimeoutVisible &&
                (!LoginTimeoutMinutes.HasValue || LoginTimeoutMinutes <= 0))
            {
                ErrorMessage = "Login timeout must be greater than 0 minutes.";
                return;
            }

            try
            {
                // ================= ADD GROUP =================
                if (!IsEditMode)
                {
                    // 1️⃣ Create Windows group
                    _windowsGroupService.CreateLocalGroup(GroupName);

                    // 2️⃣ Save to DB
                    _repo.CreateGroup(new GroupPermissionsModel
                    {
                        GroupName = GroupName,
                        LoginType = LoginType,
                        LoginTimeout = IsLoginTimeoutVisible
                            ? LoginTimeoutMinutes * 60
                            : null,
                        Permission1 = Permission1,
                        Permission2 = Permission2,
                        Permission3 = Permission3,
                        Permission4 = Permission4,
                        Permission5 = Permission5,
                        Permission6 = Permission6
                    });

                    // 3️⃣ AUDIT
                    _audit.Log(
                        "admin",
                        "GROUP_CREATE",
                        $"Group {GroupName} created",
                        null,
                        GroupName,
                        "New role creation"
                    );
                }
                // ================= EDIT GROUP =================
                else
                {
                    _repo.UpdateGroup(new GroupPermissionsModel
                    {
                        GroupId = GroupId ?? 0,
                        GroupName = GroupName,
                        LoginType = LoginType,
                        LoginTimeout = IsLoginTimeoutVisible
                            ? LoginTimeoutMinutes * 60
                            : null,
                        Permission1 = Permission1,
                        Permission2 = Permission2,
                        Permission3 = Permission3,
                        Permission4 = Permission4,
                        Permission5 = Permission5,
                        Permission6 = Permission6
                    });

                    // 🔹 AUDIT ONLY IF NAME CHANGED
                    if (_originalGroupName != GroupName)
                    {
                        _audit.Log(
                            "admin",
                            "GROUP_UPDATE",
                            $"Group renamed from {_originalGroupName} to {GroupName}",
                            _originalGroupName,
                            GroupName,
                            "Role name updated"
                        );
                    }
                    else
                    {
                        _audit.Log(
                            "admin",
                            "GROUP_UPDATE",
                            $"Group {GroupName} permissions updated",
                            null,
                            null,
                            "Permission change"
                        );
                    }
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
