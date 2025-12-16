using IrisAuth.ViewModels;
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

namespace IrisAuth.Views
{
    /// <summary>
    /// Interaction logic for BiometricEnrollDialog.xaml
    /// </summary>
    public partial class BiometricEnrollDialog : Window
    {
        public BiometricEnrollDialog()
        {
            InitializeComponent();

            Loaded += (_, __) =>
            {
                if (DataContext is BiometricEnrollViewModel vm)
                    vm.CloseAction = Close;
            };
        }
    }
}
