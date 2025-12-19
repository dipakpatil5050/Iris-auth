
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
using System.ComponentModel;
using System.Windows.Data;
namespace IrisAuth.ViewModels
{
    public class UserManagementViewModel : ViewModelBase
    {
        private readonly UserAccountRepository _repo = new UserAccountRepository();

        public ObservableCollection<UserAccountModel> Users { get; }
        public ICollectionView UsersView { get; }

        private UserAccountModel _selectedUser;
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

        /* ================= SEARCH ================= */
        private string _searchText;
        public string SearchText
        {
            get => _searchText;
            set
            {
                _searchText = value;
                OnPropertyChanged(nameof(SearchText));
                UsersView.Refresh();
            }
        }

        /* ================= PAGINATION ================= */
        private int _pageSize = 8;
        private int _currentPage = 1;

        public int CurrentPage
        {
            get => _currentPage;
            set
            {
                _currentPage = value;
                OnPropertyChanged(nameof(CurrentPage));
                UsersView.Refresh();
            }
        }

        public int TotalPages =>
            (int)Math.Ceiling((double)Users.Count / _pageSize);

        /* ================= COMMANDS ================= */
        public ICommand AddUserCommand { get; }
        public ICommand EditCommand { get; }
        public ICommand ToggleBlockCommand { get; }
        public ICommand EnrollBiometricCommand { get; }
        public ICommand NextPageCommand { get; }
        public ICommand PrevPageCommand { get; }

        public UserManagementViewModel()
        {
            Users = new ObservableCollection<UserAccountModel>();

            UsersView = CollectionViewSource.GetDefaultView(Users);
            UsersView.Filter = FilterUsers;

            AddUserCommand = new ViewModelCommand(_ => AddUser());
            EditCommand = new ViewModelCommand(u => EditUser(u as UserAccountModel));
            ToggleBlockCommand = new ViewModelCommand(u => ToggleBlock(u as UserAccountModel));
            EnrollBiometricCommand = new ViewModelCommand(u => EnrollBiometric(u as UserAccountModel));

            NextPageCommand = new ViewModelCommand(_ =>
            {
                if (CurrentPage < TotalPages)
                    CurrentPage++;
            });

            PrevPageCommand = new ViewModelCommand(_ =>
            {
                if (CurrentPage > 1)
                    CurrentPage--;
            });

            LoadUsers();
        }

        /* ================= FILTER ================= */
        private bool FilterUsers(object obj)
        {
            var user = obj as UserAccountModel;
            if (user == null)
                return false;

            bool pageMatch = IsUserOnCurrentPage(user);

            if (string.IsNullOrWhiteSpace(SearchText))
                return pageMatch;

            string search = SearchText.ToLower();

            return pageMatch &&
                   (
                       (user.Username != null &&
                        user.Username.ToLower().Contains(search)) ||

                       (user.GroupName != null &&
                        user.GroupName.ToLower().Contains(search)) ||

                       user.UserId.ToString().Contains(search)
                   );
        }


        private bool IsUserOnCurrentPage(UserAccountModel user)
        {
            int index = Users.IndexOf(user);
            return index >= (_currentPage - 1) * _pageSize &&
                   index < _currentPage * _pageSize;
        }

        /* ================= DATA ================= */
        private void LoadUsers()
        {
            Users.Clear();
            foreach (var user in _repo.GetUsers())
                Users.Add(user);

            CurrentPage = 1;
            UsersView.Refresh();
            OnPropertyChanged(nameof(TotalPages));
        }

        /* ================= ACTIONS ================= */
        private void AddUser() => OpenDialog(null);

        private void EditUser(UserAccountModel user) => OpenDialog(user);

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
            LoadUsers();
        }

        private void OpenDialog(UserAccountModel user)
        {
            var vm = new UserEditDialogViewModel(user);
            var dlg = new UserEditDialog(vm)
            {
                Owner = Application.Current.MainWindow
            };

            dlg.ShowDialog();
            LoadUsers();
        }
    }
}
