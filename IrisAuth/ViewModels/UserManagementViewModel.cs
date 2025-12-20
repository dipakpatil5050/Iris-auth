
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
using IrisAuth.ViewModels;
namespace IrisAuth.ViewModels
{
    public class UserManagementViewModel : ViewModelBase
    {
        private readonly UserAccountRepository _repo = new UserAccountRepository();

        // MASTER LIST: Holds ALL data from database
        private List<UserAccountModel> _allUsers = new List<UserAccountModel>();

        public ObservableCollection<UserAccountModel> Users { get; }
        public ICollectionView UsersView { get; }

        // BUTTONS LIST: Dynamic "1 2 3..." buttons
        public ObservableCollection<PaginationButton> PaginationButtons { get; }
            = new ObservableCollection<PaginationButton>();

        private UserAccountModel _selectedUser;
        public UserAccountModel SelectedUser
        {
            get => _selectedUser;
            set
            {
                _selectedUser = value;
                OnPropertyChanged(nameof(SelectedUser));
                //CommandManager.InvalidateRequerySuggested();
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
                //UsersView.Refresh();

                // Reset to Page 1 when searching
                CurrentPage = 1;
                RefreshData();
            }
        }

        /* ================= PAGINATION ================= */
        private int _pageSize = 10;
        private int _currentPage = 1;
        private int _totalPages = 1;

        public int CurrentPage
        {
            get => _currentPage;
            set
            {
                _currentPage = value;
                OnPropertyChanged(nameof(CurrentPage));
                //UsersView.Refresh();
                RefreshData();
            }
        }

        //public int TotalPages =>
        //    (int)Math.Ceiling((double)Users.Count / _pageSize);

        public int TotalPages
        {
            get => _totalPages;
            set
            {
                _totalPages = value;
                OnPropertyChanged(nameof(TotalPages));
            }
        }





        /* ================= COMMANDS ================= */
        public ICommand AddUserCommand { get; }
        public ICommand EditCommand { get; }
        public ICommand ToggleBlockCommand { get; }
        public ICommand EnrollBiometricCommand { get; }
        public ICommand NextPageCommand { get; }
        public ICommand PrevPageCommand { get; }

        public ICommand GoToPageCommand { get; } // New: Click on "1", "2"

        public UserManagementViewModel()
        {
            Users = new ObservableCollection<UserAccountModel>();

            //UsersView = CollectionViewSource.GetDefaultView(Users);
            //UsersView.Filter = FilterUsers;

            AddUserCommand = new ViewModelCommand(_ => AddUser());
            EditCommand = new ViewModelCommand(u => EditUser(u as UserAccountModel));
            ToggleBlockCommand = new ViewModelCommand(u => ToggleBlock(u as UserAccountModel));
            EnrollBiometricCommand = new ViewModelCommand(u => EnrollBiometric(u as UserAccountModel));

            //NextPageCommand = new ViewModelCommand(_ =>
            //{
            //    if (CurrentPage < TotalPages)
            //        CurrentPage++;
            //});

            NextPageCommand = new ViewModelCommand(_ => { if (CurrentPage < TotalPages) CurrentPage++; });
            PrevPageCommand = new ViewModelCommand(_ => { if (CurrentPage > 1) CurrentPage--; });



            GoToPageCommand = new ViewModelCommand(page =>
            {
                if (CurrentPage > 1)
                    CurrentPage--;
                if (page is int p) CurrentPage = p;
            });

            LoadUsers();
        }

        /* ================= FILTER ================= */
        private void RefreshData()
        {
            // 1. Filter the Master List based on Search Text
            IEnumerable<UserAccountModel> query = _allUsers;

            if (!string.IsNullOrWhiteSpace(SearchText))
            {
                string s = SearchText.ToLower();
                query = query.Where(user =>
                    (user.Username != null && user.Username.ToLower().Contains(s)) ||
                    (user.GroupName != null && user.GroupName.ToLower().Contains(s)) ||
                    user.UserId.ToString().Contains(s)
                );
            }

            var filteredList = query.ToList();

            // 2. Calculate Total Pages
            TotalPages = (int)Math.Ceiling((double)filteredList.Count / _pageSize);
            if (TotalPages == 0) TotalPages = 1;

            if (CurrentPage > TotalPages) CurrentPage = TotalPages;
            if (CurrentPage < 1) CurrentPage = 1;

            // 3. Get ONLY the rows for the Current Page
            var pageRows = filteredList
                            .Skip((CurrentPage - 1) * _pageSize)
                            .Take(_pageSize)
                            .ToList();

            // 4. Update the UI Collection
            Users.Clear();
            foreach (var item in pageRows) Users.Add(item);

            // 5. Update the "1 2 3" Buttons
            UpdatePaginationButtons();
        }

      private void UpdatePaginationButtons()
                        { PaginationButtons.Clear();

                            // Logic to determine which buttons to show (e.g. 1 ... 4 5 6 ... 10)
                            int start = Math.Max(1, CurrentPage - 2);
                    int end = Math.Min(TotalPages, CurrentPage + 2);

                            // First Page
                            if (start > 1)
                            {
                                PaginationButtons.Add(new PaginationButton { Content = "1", PageNumber = 1, Command = GoToPageCommand
                });
                if (start > 2) PaginationButtons.Add(new PaginationButton { Content = "...", IsEllipsis = true });
                            }

                            // Middle Pages
                            for (int i = start; i <= end; i++)
                {
                    PaginationButtons.Add(new PaginationButton
                    {
                        Content = i.ToString(),
                        PageNumber = i,
                        IsSelected = (i == CurrentPage),
                        Command = GoToPageCommand
                    });
                }

                    // Last Page
                    if (end < TotalPages)
                    {
                        if (end < TotalPages - 1) PaginationButtons.Add(new PaginationButton { Content = "...", IsEllipsis = true });
                        PaginationButtons.Add(new PaginationButton { Content = TotalPages.ToString(), PageNumber = TotalPages, Command = GoToPageCommand });
                    }
      }





        /* ================= DATA ================= */
        private void LoadUsers()
        {
            var dbUsers = _repo.GetUsers();
            _allUsers.Clear();
            _allUsers.AddRange(dbUsers);

            CurrentPage = 1;
            //UsersView.Refresh();
            OnPropertyChanged(nameof(TotalPages));
            RefreshData();
        }

        /* ================= ACTIONS ================= */
        private void AddUser()
        {
            OpenDialog(null);
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
