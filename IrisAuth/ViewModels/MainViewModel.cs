using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using FontAwesome.Sharp;
using IrisAuth.Models;
using IrisAuth.Repositories;

namespace IrisAuth.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        //Fields
        private UserModel _currentUserAccount;
        private ViewModelBase _currentChildView;
        private string _caption;
        private IconChar _icon;


        private IUserRepository userRepository;

        // Properties
        public UserModel CurrentUserAccount
        {
            get
            {
                return _currentUserAccount;
            }

            set
            {
                _currentUserAccount = value;
                OnPropertyChanged(nameof(CurrentUserAccount));
            }
        }

        public ViewModelBase CurrentChildView { 
            get => _currentChildView;
            set
            {
                _currentChildView = value;
                OnPropertyChanged(nameof(CurrentChildView));
            }

        }
        public string Caption { get => _caption;
            set
            {
                _caption = value;
                OnPropertyChanged(nameof(Caption));

            }
        }
        public IconChar Icon { get => _icon;
            set 
            {
                _icon = value;
                OnPropertyChanged(nameof(Icon));
            }
        }

        //--> Commands
        public ICommand ShowHomeViewCommand { get; }
        public ICommand ShowUserManagementViewCommand { get; }
        public ICommand ShowUserLogsViewCommand { get; }
        public ICommand ShowUserGroupsViewCommand { get; }

        public MainViewModel()
        {
            userRepository = new UserRepository();
            CurrentUserAccount = new UserModel();

            //Initialize commands
            ShowHomeViewCommand = new ViewModelCommand(ExecuteShowHomeViewCommand);
            ShowUserManagementViewCommand = new ViewModelCommand(ExecuteShowUserManagementViewCommand);
            ShowUserLogsViewCommand = new ViewModelCommand(ExecuteShowUserLogsViewCommand);
            ShowUserGroupsViewCommand = new ViewModelCommand(ExecuteShowUserGroupsViewCommand);
            //Default view
            ExecuteShowHomeViewCommand(null);

            LoadCurrentUserData();
        }

        private void ExecuteShowUserLogsViewCommand(object obj)
        {
            CurrentChildView = new UserLogsViewModel();
            Caption = "User logs";
            Icon = IconChar.User;
        }
        private void ExecuteShowUserGroupsViewCommand(object obj)
        {
            CurrentChildView = new GroupManagementViewModel();
            Caption = "User Groups";
            Icon = IconChar.User;
        }

        private void ExecuteShowUserManagementViewCommand(object obj)
        {
            CurrentChildView = new UserManagementViewModel();
            Caption = "User Management";
            Icon = IconChar.UserGroup;
        }

        private void ExecuteShowHomeViewCommand(object obj)
        {
            CurrentChildView = new HomeViewModel();
            Caption = "Dashboard";
            Icon = IconChar.Home;
        }

        private void LoadCurrentUserData()
        {
            var user = userRepository.GetByUsername(Thread.CurrentPrincipal.Identity.Name);
            if (user != null)
            {
                CurrentUserAccount.Username = user.Username;
                //CurrentUserAccount.DisplayName = $"{user.Name} {user.LastName}";
               // CurrentUserAccount.ProfilePicture = null;               
            }
            else
            {
               // CurrentUserAccount.DisplayName="Invalid user, not logged in";
                //Hide child views.
            }
        }
    }
}
