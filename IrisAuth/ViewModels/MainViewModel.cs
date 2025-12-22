using FontAwesome.Sharp;
using IrisAuth.Helpers;
using IrisAuth.Models;
using IrisAuth.Repositories;
using IrisAuth.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace IrisAuth.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        //Fields
        private UserModel _currentUserAccount;
        private ViewModelBase _currentChildView;
        private string _caption;
        private IconChar _icon;
        public bool ShowSettingsMenu
        {
            get
            {
                return AppSession.CurrentUser != null &&
                       AppSession.CurrentUser.Roles == "SuperAdmin";
            }
        }
        public void RefreshPermissions()
        {
            OnPropertyChanged(nameof(ShowSettingsMenu));
        }

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
        public ICommand ShowUserGroupViewCommand { get; }
        public ICommand ShowSettingsViewCommand { get; }
        public ICommand LogoutCommand { get; }

        public MainViewModel()
        {
            userRepository = new UserRepository();
            CurrentUserAccount = new UserModel();
            LogoutCommand = new RelayCommand(ExecuteLogout);

            AppSession.UserChanged += OnUserChanged;   // ✅ ADD

            //Initialize commands
            ShowHomeViewCommand = new ViewModelCommand(ExecuteShowHomeViewCommand);
            ShowUserManagementViewCommand = new ViewModelCommand(ExecuteShowUserManagementViewCommand);
            ShowUserLogsViewCommand = new ViewModelCommand(ExecuteShowUserLogsViewCommand);
            ShowUserGroupViewCommand = new ViewModelCommand(ExecuteShowUserGroupsViewCommand);
            ShowSettingsViewCommand = new ViewModelCommand(ExecuteShowSettingsViewCommand);
            //Default view
            ExecuteShowHomeViewCommand(null);

            LoadCurrentUserData();
        }
        private void OnUserChanged()
        {
            OnPropertyChanged(nameof(ShowSettingsMenu));
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
            Caption = "Group Master";
            Icon = IconChar.Database;
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
        private void ExecuteShowSettingsViewCommand(object obj)
        {
            CurrentChildView = new AppSettingsViewModel();
            Caption = "Application Settings";
            Icon = IconChar.Cog;
        }
        private void LoadCurrentUserData()
        {
            var user = userRepository.GetByUsername(Thread.CurrentPrincipal.Identity.Name);

            if (user != null)
            {
                CurrentUserAccount = user;

                // 🔑 THIS LINE IS REQUIRED
                OnPropertyChanged(nameof(ShowSettingsMenu));
            }
        }

        private void ExecuteLogout(object obj)
        {
            // 1️⃣ Clear session
            AppSession.CurrentUser = null;
            Thread.CurrentPrincipal = null;

            Application.Current.Dispatcher.Invoke(() =>
            {
                // 2️⃣ Open LoginView
                var loginView = new LoginView();
                loginView.Show();

                // 3️⃣ Set LoginView as MainWindow
                Application.Current.MainWindow = loginView;

                // 4️⃣ Close MainView
                foreach (Window window in Application.Current.Windows)
                {
                    if (window is MainView)
                    {
                        window.Close();
                        break;
                    }
                }
            });
        }
    }
}
