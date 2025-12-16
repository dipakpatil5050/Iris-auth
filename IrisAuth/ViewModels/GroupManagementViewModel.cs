using IrisAuth.Models;
using IrisAuth.Repositories;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

namespace IrisAuth.ViewModels
{
    public class GroupManagementViewModel : ViewModelBase
    {
        private readonly GroupRepository _repo = new GroupRepository();

        public ObservableCollection<GroupPermissionsModel> Groups { get; }
        public GroupPermissionsModel SelectedGroup { get; set; }

        public ICommand AddGroupCommand { get; }
        public ICommand EditGroupCommand { get; }
        public ICommand DeleteGroupCommand { get; }

        public GroupManagementViewModel()
        {
            Groups = new ObservableCollection<GroupPermissionsModel>();
            AddGroupCommand = new ViewModelCommand(_ => AddGroup());
            EditGroupCommand = new ViewModelCommand(g => EditGroup(g as GroupPermissionsModel));
            DeleteGroupCommand = new ViewModelCommand(g => DeleteGroup(g as GroupPermissionsModel));

            LoadGroups();
        }

        private void LoadGroups()
        {
            Groups.Clear();
            foreach (var g in _repo.GetGroups())
                Groups.Add(g);
        }

        private void AddGroup()
        {
            var vm = new GroupEditDialogViewModel();
            var dlg = new Views.GroupEditDialog
            {
                DataContext = vm,
                Owner = Application.Current.MainWindow
            };

            vm.CloseAction = () => dlg.Close();
            dlg.ShowDialog();
            LoadGroups();
        }

        private void EditGroup(GroupPermissionsModel group)
        {
            if (group == null) return;

            var vm = new GroupEditDialogViewModel(group);
            var dlg = new Views.GroupEditDialog
            {
                DataContext = vm,
                Owner = Application.Current.MainWindow
            };

            vm.CloseAction = () => dlg.Close();
            dlg.ShowDialog();
            LoadGroups();
        }

        private void DeleteGroup(GroupPermissionsModel group)
        {
            if (group == null) return;

            if (MessageBox.Show($"Delete group '{group.GroupName}'?",
                "Confirm", MessageBoxButton.YesNo) != MessageBoxResult.Yes)
                return;

            _repo.DeleteGroup(group.GroupId);
            LoadGroups();
        }
    }
}