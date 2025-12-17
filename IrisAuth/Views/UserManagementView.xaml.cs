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
using IrisAuth.ViewModels;
namespace IrisAuth.Views
{
    public partial class UserManagementView : UserControl
    {
        public UserManagementView()
        {
            InitializeComponent();
/*            DataContext = new UserManagementViewModel();*/ // 🔑 REQUIRED

        }

    }
}

  
