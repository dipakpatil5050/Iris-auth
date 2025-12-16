
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using IrisAuth.Models;
using IrisAuth.Repositories;
using IrisAuth.Helpers;
using System.Windows;
using IrisAuth.Views;
namespace IrisAuth.ViewModels
{
    public class UserManagementViewModel : ViewModelBase
    {
        private readonly UserAccountRepository _repo = new UserAccountRepository();
        private UserAccountModel _selectedUser;

        public ObservableCollection<UserAccountModel> Users { get; }

        public UserAccountModel SelectedUser
        {
            get => _selectedUser;
            set
            {
                _selectedUser = value;
                OnPropertyChanged(nameof(SelectedUser));
                CommandManager.InvalidateRequerySuggested();
            }
        }

        public ICommand AddUserCommand { get; }
        public ICommand EditCommand { get; }
        public ICommand ToggleBlockCommand { get; }
        public ICommand EnrollBiometricCommand { get; }
        public UserManagementViewModel()
        {
            Users = new ObservableCollection<UserAccountModel>();

            AddUserCommand = new ViewModelCommand(_ => AddUser());

            EditCommand = new ViewModelCommand(
                u => EditUser(u as UserAccountModel),
                u => u is UserAccountModel);

            ToggleBlockCommand = new ViewModelCommand(
                u => ToggleBlock(u as UserAccountModel),
                u => u is UserAccountModel);
            EnrollBiometricCommand = new ViewModelCommand(
                u => EnrollBiometric(u as UserAccountModel),
                u => u is UserAccountModel
);

            LoadUsers();
        }

        private void LoadUsers()
        {
            Users.Clear();
            foreach (var user in _repo.GetUsers())
                Users.Add(user);
        }

        private void AddUser()
        {
            OpenDialog(null);
            // handled by View (dialog)
        }

        private void EditUser(UserAccountModel user)
        {
            OpenDialog(user);
        }

        private void ToggleBlock(UserAccountModel user)
        {
            if (user == null) return;

            if (user.IsBlocked)
                _repo.UnblockUser(user.UserId);
            else
                _repo.BlockUser(user.UserId);

            LoadUsers();
        }
        private void EnrollBiometric(UserAccountModel user)
        {
            if (user == null) return;

            var vm = new BiometricEnrollViewModel(user.UserId);
            var dlg = new BiometricEnrollDialog
            {
                DataContext = vm,
                Owner = Application.Current.MainWindow
            };

            vm.CloseAction = () => dlg.Close();
            dlg.ShowDialog();

            LoadUsers(); // refresh biometric status
        }
        /*
        private void OpenDialog(UserAccountModel user)
        {
            var vm = new UserEditDialogViewModel(user);
            var dlg = new UserEditDialog
            {
                DataContext = vm,
                Owner = Application.Current.MainWindow
            };

            vm.CloseAction = () => dlg.Close();

            dlg.ShowDialog();
            LoadUsers();
        } */
        private void OpenDialog(UserAccountModel user)
        {
            var vm = new UserEditDialogViewModel(user);

            var dlg = new UserEditDialog(vm)   // ✅ PASS VM HERE
            {
                Owner = Application.Current.MainWindow
            };

            dlg.ShowDialog();
            LoadUsers();
        }
    }
}
