using IrisAuth.Models;              
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;

namespace IrisAuth.Views
{
    public partial class UserManagementView : UserControl
    {
        public UserManagementView()
        {
            InitializeComponent();
            // Set DataContext to the included ViewModel (no external files required)
            this.DataContext = new UserManagementViewModel();
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ComboBox_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {

        }
    }

    // ------------------------
    // RelayCommand (simple ICommand)
    // ------------------------
    public class RelayCommand : ICommand
    {
        private readonly Action<object> _execute;
        private readonly Predicate<object> _canExecute;

        public RelayCommand(Action<object> execute, Predicate<object> canExecute = null)
        {
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
            _canExecute = canExecute;
        }

        public bool CanExecute(object parameter) => _canExecute?.Invoke(parameter) ?? true;
        public void Execute(object parameter) => _execute(parameter);
        public event EventHandler CanExecuteChanged;
        public void RaiseCanExecuteChanged() => CanExecuteChanged?.Invoke(this, EventArgs.Empty);
    }

    // ------------------------
    // Converters
    // ------------------------
    public class RowIndexConverter : IValueConverter
    {
        // Converts AlternationIndex to 1-based row number
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is int idx) return (idx + 1).ToString();
            return "0";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => throw new NotSupportedException();
    }

    public class NameLastNameConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            var name = values.Length > 0 && values[0] != null ? values[0].ToString() : string.Empty;
            var last = values.Length > 1 && values[1] != null ? values[1].ToString() : string.Empty;
            var full = (name + " " + last).Trim();
            return string.IsNullOrEmpty(full) ? "-" : full;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture) => throw new NotSupportedException();
    }

    // ------------------------
    // ViewModel (contains sample data + commands)
    // ------------------------
    public class UserManagementViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<UserModel> Users { get; } = new ObservableCollection<UserModel>();
        public ICollectionView UsersView { get; }

        private string _filterText = "";
        public string FilterText
        {
            get => _filterText;
            set
            {
                if (_filterText == value) return;
                _filterText = value;
                OnPropertyChanged(nameof(FilterText));
                UsersView.Refresh();
            }
        }

        private string _selectedSort = "Username";
        public string SelectedSort
        {
            get => _selectedSort;
            set
            {
                if (_selectedSort == value) return;
                _selectedSort = value;
                OnPropertyChanged(nameof(SelectedSort));
                ApplySort();
            }
        }

        public ICommand AddCommand { get; }
        public ICommand SearchCommand { get; }
        public ICommand ViewCommand { get; }
        public ICommand EditCommand { get; }
        public ICommand DeleteCommand { get; }

        public UserManagementViewModel()
        {
            // ----- sample/dummy data (replace with real data)

            Users.Add(new UserModel { Id = "10101", Username = "admin", Password = "", Name = "Rogher", LastName = "Rice", Email = "rogher@rjcodeadvance.com", Roles = "Super Admin", AvatarUri = "https://i.pravatar.cc/48?img=1" });
            Users.Add(new UserModel { Id = "10109", Username = "briana", Password = "", Name = "Brianna", LastName = "Moss", Email = "briana@rjcodeadvance.com", Roles = "Billing Admin, Manager", AvatarUri = "https://i.pravatar.cc/48?img=2" });
            Users.Add(new UserModel { Id = "10107", Username = "jannet", Password = "", Name = "Janetta", LastName = "Aguirre", Email = "jannetta@gmail.com", Roles = "Manager", AvatarUri = "https://i.pravatar.cc/48?img=3" });
            Users.Add(new UserModel { Id = "10103", Username = "joseph", Password = "", Name = "Joshep", LastName = "Sykes", Email = "joseph@rjcodeadvance.com", Roles = "Standard", AvatarUri = "https://i.pravatar.cc/48?img=4" });
            Users.Add(new UserModel { Id = "10102", Username = "kathy", Password = "", Name = "Katherine", LastName = "Hays", Email = "kathy@rjcodeadvance.com", Roles = "User Admin, Billing Admin", AvatarUri = "https://i.pravatar.cc/48?img=5" });

            UsersView = CollectionViewSource.GetDefaultView(Users);
            UsersView.Filter = FilterUsers;
            ApplySort();

            AddCommand = new RelayCommand(_ => AddUser());
            SearchCommand = new RelayCommand(_ => UsersView.Refresh());
            ViewCommand = new RelayCommand(param => ViewUser(param as UserModel), param => param is UserModel);
            EditCommand = new RelayCommand(param => EditUser(param as UserModel), param => param is UserModel);
            DeleteCommand = new RelayCommand(param => DeleteUser(param as UserModel), param => param is UserModel);
        }

        private bool FilterUsers(object obj)
        {
            var u = obj as UserModel;
            if (u == null) return false;
            if (string.IsNullOrWhiteSpace(FilterText)) return true;

            var q = FilterText.Trim().ToLowerInvariant();
            return (u.Id?.ToLowerInvariant().Contains(q) ?? false)
                || (!string.IsNullOrEmpty(u.Username) && u.Username.ToLower().Contains(q))
                || (!string.IsNullOrEmpty(u.Email) && u.Email.ToLower().Contains(q))
                || (!string.IsNullOrEmpty(u.Name) && (u.Name + " " + u.LastName).ToLower().Contains(q))
                || (!string.IsNullOrEmpty(u.Roles) && u.Roles.ToLower().Contains(q));
        }

        private void ApplySort()
        {
            UsersView.SortDescriptions.Clear();
            switch (SelectedSort)
            {
                case "Username":
                    UsersView.SortDescriptions.Add(new SortDescription(nameof(UserModel.Username), ListSortDirection.Ascending));
                    break;
                case "Full name":
                    UsersView.SortDescriptions.Add(new SortDescription(nameof(UserModel.Name), ListSortDirection.Ascending));
                    UsersView.SortDescriptions.Add(new SortDescription(nameof(UserModel.LastName), ListSortDirection.Ascending));
                    break;
                case "ID":
                    UsersView.SortDescriptions.Add(new SortDescription(nameof(UserModel.Id), ListSortDirection.Ascending));
                    break;
                default:
                    UsersView.SortDescriptions.Add(new SortDescription(nameof(UserModel.Username), ListSortDirection.Ascending));
                    break;
            }
        }

        private void AddUser()
        {
            var nextId = (Users.Any() ? (int.TryParse(Users.Max(x => x.Id), out var max) ? (max + 1).ToString() : (Users.Count + 10100).ToString()) : "10100");
            Users.Insert(0, new UserModel
            {
                Id = nextId,
                Username = $"user{nextId}",
                Password = "",
                Name = "New",
                LastName = "User",
                Email = $"user{nextId}@example.com",
                Roles = "Standard",
                AvatarUri = "https://i.pravatar.cc/48"
            });
        }

        private void ViewUser(UserModel user)
        {
            if (user == null) return;
            MessageBox.Show($"Viewing user: {user.Name} {user.LastName} ({user.Username})", "View user", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void EditUser(UserModel user)
        {
            if (user == null) return;
            MessageBox.Show($"Edit user: {user.Name} {user.LastName}", "Edit user", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void DeleteUser(UserModel user)
        {
            if (user == null) return;
            var answer = MessageBox.Show($"Delete user {user.Name} {user.LastName} ({user.Username})?", "Confirm delete", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (answer == MessageBoxResult.Yes) Users.Remove(user);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string prop) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
    }
}
