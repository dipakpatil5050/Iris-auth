using IrisAuth.ViewModels;
using System;
using System.Windows.Controls;
using System.Windows.Threading;

namespace IrisAuth.Views
{
    public partial class HomeView : UserControl
    {
        private readonly DispatcherTimer _timer;

        public HomeView()
        {
            InitializeComponent();
            this.DataContext = new UserControl();

            _timer = new DispatcherTimer
            {
                Interval = TimeSpan.FromSeconds(1)
            };

            _timer.Tick += (s, e) =>
            {
                txtDate.Text = DateTime.Now.ToString("dddd, dd MMM yyyy");
                txtTime.Text = DateTime.Now.ToString("HH:mm:ss");
            };

            _timer.Start();
        }
    }
}
