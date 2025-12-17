//using System;
//using System.Runtime.InteropServices;
//using System.Windows;
//using System.Windows.Input;
//using System.Windows.Interop;

//namespace IrisAuth.Views
//{
//    /// <summary>
//    /// Interaction logic for MainWindowView.xaml
//    /// </summary>
//    //public partial class MainView : Window
//    //{
//    //    public MainView()
//    //    {
//    //        InitializeComponent();
//    //        this.MaxHeight = SystemParameters.MaximizedPrimaryScreenHeight;
//    //    }

//    //    private void RadioButton_Checked(object sender, RoutedEventArgs e)
//    //    {

//    //    }

//    //    private void RadioButton_Checked_1(object sender, RoutedEventArgs e)
//    //    {

//    //    }

//    //    private void RadioButton_Checked_2()
//    //    {

//    //    }
//    //    [DllImport("user32.dll")]
//    //    public static extern IntPtr SendMessage(IntPtr hWnd, int wMsg, int wParam, int lParam);

//    //    private void pnlControlBar_MouseLeftButtonDown(Object sender, MouseButtonEventArgs e)
//    //    {
//    //        WindowInteropHelper helper = new WindowInteropHelper(this);
//    //        SendMessage(helper.Handle, 161, 2, 0);
//    //    }

//    //    private void pnlControlBar_MouseEnter(Object sender, MouseEventArgs e)
//    //    {
//    //        this.MaxHeight = SystemParameters.MaximizedPrimaryScreenHeight;
//    //    }

//    //    private void BtnClose_Click(object sender, RoutedEventArgs e)
//    //    {
//    //        Application.Current.Shutdown();
//    //    }

//    //    private void BtnMinimize_Click(object sender, RoutedEventArgs e)
//    //    {
//    //        this.WindowState = WindowState.Minimized;
//    //    }

//    //    private void BtnMaximum_Click(object sender, RoutedEventArgs e)
//    //    {
//    //        if (this.WindowState == WindowState.Normal)
//    //            this.WindowState = WindowState.Maximized;
//    //        else
//    //            this.WindowState = WindowState.Normal;
//    //    }

//    //    private void RadioButton_Checked_3(object sender, RoutedEventArgs e)
//    //    {

//    //    }



//    //    private bool IsMaximize = false;
//    //    private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
//    //    {
//    //        if (e.ClickCount == 2)
//    //        {
//    //            if (IsMaximize)
//    //            {
//    //                this.WindowState = WindowState.Normal;
//    //                this.Width = 1080;
//    //                this.Height = 720;

//    //                IsMaximize = false;
//    //            }
//    //            else
//    //            {
//    //                this.WindowState = WindowState.Maximized;

//    //                IsMaximize = true;
//    //            }
//    //        }
//    //    }

//    //    private void Border_MouseDown(object sender, MouseButtonEventArgs e)
//    //    {
//    //        if (e.ChangedButton == MouseButton.Left)
//    //        {
//    //            this.DragMove();
//    //        }
//    //    }

//    //    private void membersDataGrid_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
//    //    {

//    //    }
//    //}
//    public partial class MainView : Window
//    {
//        public MainView()
//        {
//            InitializeComponent();
//            MaxHeight = SystemParameters.MaximizedPrimaryScreenHeight;
//        }

//        // ===============================
//        // WINDOW DRAG SUPPORT (Custom Chrome)
//        // ===============================
//        [DllImport("user32.dll")]
//        private static extern IntPtr SendMessage(
//            IntPtr hWnd, int msg, int wParam, int lParam);

//        private void ControlBar_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
//        {
//            if (e.LeftButton == MouseButtonState.Pressed)
//            {
//                var helper = new WindowInteropHelper(this);
//                SendMessage(helper.Handle, 0xA1, 0x2, 0);
//            }
//        }

//        // ===============================
//        // WINDOW BUTTONS
//        // ===============================
//        private void BtnClose_Click(object sender, RoutedEventArgs e)
//        {
//            Application.Current.Shutdown();
//        }

//        private void BtnMinimize_Click(object sender, RoutedEventArgs e)
//        {
//            WindowState = WindowState.Minimized;
//        }

//        private void BtnMaximum_Click(object sender, RoutedEventArgs e)
//        {
//            WindowState = WindowState == WindowState.Maximized
//                ? WindowState.Normal
//                : WindowState.Maximized;
//        }

//        // ===============================
//        // DOUBLE CLICK TO MAXIMIZE
//        // ===============================
//        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
//        {
//            if (e.ClickCount == 2)
//            {
//                BtnMaximum_Click(sender, e);
//            }
//        }
//    }
//}
using System.Windows;
using System.Windows.Input;

namespace IrisAuth.Views
{
    public partial class MainView : Window
    {
        public MainView()
        {
            InitializeComponent();
        }

        private void Minimize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void Maximize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState == WindowState.Maximized
                ? WindowState.Normal
                : WindowState.Maximized;
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void Header_MouseDown(object sender, MouseButtonEventArgs e)
        {
            //if (e.LeftButton == MouseButtonState.Pressed)
            //    DragMove();
        }
    }
}


