//using System;
//using System.Collections.ObjectModel;
//using System.Linq;
//using System.Windows.Input;
//using IrisAuth.Helpers;
//using IrisAuth.Models;
//using IrisAuth.Repositories;

//namespace IrisAuth.ViewModels
//{
//    public class UserLogsViewModel : ViewModelBase
//    {
//        private readonly AuditTrailRepository _repo =
//            new AuditTrailRepository();

//        public ObservableCollection<AuditTrailModel> Logs
//            = new ObservableCollection<AuditTrailModel>();

//        // ================= FILTER =================
//        private string _searchText;
//        public string SearchText
//        {
//            get { return _searchText; }
//            set
//            {
//                _searchText = value;
//                OnPropertyChanged(nameof(SearchText));
//            }
//        }

//        public DateTime? FromDate { get; set; }
//        public DateTime? ToDate { get; set; }

//        // ================= COMMAND =================
//        public ICommand SearchCommand { get; }

//        public UserLogsViewModel()
//        {
//            SearchCommand = new ViewModelCommand(
//                _ => Load());

//            Load();
//        }

//        private void Load()
//        {
//            Logs.Clear();

//            var data = _repo.GetAuditReport(
//                FromDate,
//                ToDate,
//                null,
//                null
//            );

//            if (!string.IsNullOrWhiteSpace(SearchText))
//            {
//                data = data.Where(a =>
//                    (a.Username != null &&
//                     a.Username.IndexOf(SearchText,
//                        StringComparison.OrdinalIgnoreCase) >= 0)

//                 || (a.Action != null &&
//                     a.Action.IndexOf(SearchText,
//                        StringComparison.OrdinalIgnoreCase) >= 0)

//                 || (a.Details != null &&
//                     a.Details.IndexOf(SearchText,
//                        StringComparison.OrdinalIgnoreCase) >= 0)

//                 || (a.Reason != null &&
//                     a.Reason.IndexOf(SearchText,
//                        StringComparison.OrdinalIgnoreCase) >= 0)
//                ).ToList();
//            }

//            foreach (var item in data)
//                Logs.Add(item);
//        }
//    }
//}
using IrisAuth.Helpers;
using IrisAuth.Models;
using IrisAuth.Repositories;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace IrisAuth.ViewModels
{
    public class UserLogsViewModel : ViewModelBase
    {
        private readonly AuditTrailRepository _repo =
            new AuditTrailRepository();

        public ObservableCollection<AuditTrailModel> Logs { get; }
            = new ObservableCollection<AuditTrailModel>();

        private AuditTrailModel _selectedLog;
        public AuditTrailModel SelectedLog
        {
            get { return _selectedLog; }
            set
            {
                _selectedLog = value;
                OnPropertyChanged(nameof(SelectedLog));
            }
        }

        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }

        private string _searchText;
        public string SearchText
        {
            get { return _searchText; }
            set
            {
                _searchText = value;
                OnPropertyChanged(nameof(SearchText));
            }
        }

        public ICommand SearchCommand { get; }
        public ICommand ExportExcelCommand { get; }
        public ICommand PrintCommand { get; }
        public ICommand ViewCommand { get; }

        public UserLogsViewModel()
        {
            SearchCommand = new ViewModelCommand(_ => Load());
            ExportExcelCommand = new ViewModelCommand(_ => ExportExcel());
            PrintCommand = new ViewModelCommand(_ => Print());
            ViewCommand = new ViewModelCommand(l => ViewDetails(l as AuditTrailModel));

            Load();
        }

        private void Load()
        {
            Logs.Clear();

            var data = _repo.GetAuditReport(
                FromDate,
                ToDate,
                null,
                null);

            if (!string.IsNullOrWhiteSpace(SearchText))
            {
                data = data.Where(a =>
                    (a.Username != null &&
                     a.Username.IndexOf(SearchText,
                        StringComparison.OrdinalIgnoreCase) >= 0)
                 || (a.Action != null &&
                     a.Action.IndexOf(SearchText,
                        StringComparison.OrdinalIgnoreCase) >= 0)
                 || (a.Reason != null &&
                     a.Reason.IndexOf(SearchText,
                        StringComparison.OrdinalIgnoreCase) >= 0)
                ).ToList();
            }

            foreach (var r in data)
                Logs.Add(r);
        }

        private void ExportExcel()
        {
            var sb = new StringBuilder();
            sb.AppendLine("DateTime,Username,Action,Reason");

            foreach (var l in Logs)
            {
                sb.AppendLine(
                    $"{l.EventTime:dd.MM.yyyy HH:mm:ss}," +
                    $"{l.Username},{l.Action},\"{l.Reason}\"");
            }

            File.WriteAllText("AuditLogs.csv", sb.ToString());
            MessageBox.Show("Exported to AuditLogs.csv");
        }

        private void Print()
        {
            var dlg = new PrintDialog();
            if (dlg.ShowDialog() == true)
            {
                var tb = new TextBlock
                {
                    Text = string.Join(Environment.NewLine,
                        Logs.Select(l =>
                            $"{l.EventTime:dd.MM.yyyy HH:mm:ss} | {l.Username} | {l.Action} | {l.Reason}")),
                    FontSize = 11,
                    Margin = new Thickness(20)
                };

                dlg.PrintVisual(tb, "Audit Logs");
            }
        }

        private void ViewDetails(AuditTrailModel log)
        {
            if (log == null) return;

            MessageBox.Show(
                $"User: {log.Username}\n" +
                $"Action: {log.Action}\n" +
                $"Old: {log.OldValue}\n" +
                $"New: {log.NewValue}\n" +
                $"Reason: {log.Reason}",
                "Audit Details",
                MessageBoxButton.OK,
                MessageBoxImage.Information);
        }
    }
}



