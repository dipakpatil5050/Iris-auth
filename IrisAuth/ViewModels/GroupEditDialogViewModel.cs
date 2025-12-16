using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IrisAuth.Models;
using IrisAuth.Repositories;
using System.Windows.Input;
namespace IrisAuth.ViewModels
{
    public class GroupEditDialogViewModel : ViewModelBase
    {
        private readonly GroupRepository _repo = new GroupRepository();

        public string DialogTitle { get; }
        public bool IsEditMode { get; }

        public int? GroupId { get; }

        public string GroupName { get; set; }

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
            if (group == null)
            {
                DialogTitle = "Add Group";
                IsEditMode = false;
            }
            else
            {
                DialogTitle = "Edit Group";
                IsEditMode = true;

                GroupId = group.GroupId;
                GroupName = group.GroupName;

                Permission1 = group.Permission1;
                Permission2 = group.Permission2;
                Permission3 = group.Permission3;
                Permission4 = group.Permission4;
                Permission5 = group.Permission5;
                Permission6 = group.Permission6;
            }

            SaveCommand = new ViewModelCommand(_ => Save());
            CancelCommand = new ViewModelCommand(_ => CloseAction?.Invoke());
        }

        private void Save()
        {
            if (string.IsNullOrWhiteSpace(GroupName))
            {
                ErrorMessage = "Group name is required.";
                return;
            }

            var model = new GroupPermissionsModel
            {
                GroupId = GroupId ?? 0,
                GroupName = GroupName,
                Permission1 = Permission1,
                Permission2 = Permission2,
                Permission3 = Permission3,
                Permission4 = Permission4,
                Permission5 = Permission5,
                Permission6 = Permission6
            };

            if (IsEditMode)
                _repo.UpdateGroup(model);
            else
                _repo.CreateGroup(model);

            CloseAction?.Invoke();
        }
    }
}
