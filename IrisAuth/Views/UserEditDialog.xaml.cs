using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using IrisAuth.ViewModels;

namespace IrisAuth.Views
{
    /// <summary>
    /// Interaction logic for UserEditDialog.xaml
    /// </summary>
    public partial class UserEditDialog : Window
    {
        //public UserEditDialog()
        //{
        //    InitializeComponent();

        //}
        public UserEditDialog(UserEditDialogViewModel vm)
        {
            InitializeComponent();

            DataContext = vm;
            vm.CloseAction = Close;
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            if (DataContext is UserEditDialogViewModel vm)
            {
                vm.Password = PwdBox.Password;
                vm.SaveCommand.Execute(null);
            }
        }
    }
}
