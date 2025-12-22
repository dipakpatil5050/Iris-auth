//using IrisAuth.Helpers;
//using IrisAuth.Models;
//using IrisAuth.Repositories;
//using System;
//using System.Collections.Generic;
//using System.Collections.ObjectModel;
//using System.IO;
//using System.Linq;
//using System.Text;
//using System.Windows;
//using System.Windows.Controls;
//using System.Windows.Input;

//namespace IrisAuth.ViewModels
//{
//    public class UserLogsViewModel : ViewModelBase
//    {
//        private readonly AuditTrailRepository _repo =
//            new AuditTrailRepository();

//        // MASTER LIST: Holds ALL data from database
//        private List<AuditTrailModel> _allLogs = new List<AuditTrailModel>();

//        // ================= PAGINATION BUTTONS =================
//        public ObservableCollection<PaginationButton> PaginationButtons { get; }
//            = new ObservableCollection<PaginationButton>();

//        public ObservableCollection<AuditTrailModel> Logs { get; }
//            = new ObservableCollection<AuditTrailModel>();

//        private AuditTrailModel _selectedLog;
//        public AuditTrailModel SelectedLog
//        {
//            get { return _selectedLog; }
//            set
//            {
//                _selectedLog = value;
//                OnPropertyChanged(nameof(SelectedLog));
//            }
//        }

//        public DateTime? FromDate { get; set; }
//        public DateTime? ToDate { get; set; }

//        /* ================= SEARCH ================= */
//        private string _searchText;
//        public string SearchText
//        {
//            get => _searchText;
//            set
//            {
//                _searchText = value;
//                OnPropertyChanged(nameof(SearchText));

//                // Reset to Page 1 when searching
//                CurrentPage = 1;
//                RefreshData();
//            }
//        }

//        /* ================= PAGINATION ================= */
//        private int _pageSize = 10;
//        private int _currentPage = 1;
//        private int _totalPages = 1;

//        public int CurrentPage
//        {
//            get => _currentPage;
//            set
//            {
//                _currentPage = value;
//                OnPropertyChanged(nameof(CurrentPage));
//                RefreshData();
//            }
//        }

//        public int TotalPages
//        {
//            get => _totalPages;
//            set
//            {
//                _totalPages = value;
//                OnPropertyChanged(nameof(TotalPages));
//            }
//        }



//        public ICommand SearchCommand { get; }
//        public ICommand ExportExcelCommand { get; }
//        public ICommand PrintCommand { get; }
//        public ICommand ViewCommand { get; }
//        public ICommand NextPageCommand { get; }
//        public ICommand PrevPageCommand { get; }

//        public ICommand GoToPageCommand { get; }

//        public UserLogsViewModel()
//        {
//            SearchCommand = new ViewModelCommand(_ => Load());
//            ExportExcelCommand = new ViewModelCommand(_ => ExportExcel());
//            PrintCommand = new ViewModelCommand(_ => Print());
//            ViewCommand = new ViewModelCommand(l => ViewDetails(l as AuditTrailModel));

//            NextPageCommand = new ViewModelCommand(_ => { if (CurrentPage < TotalPages) CurrentPage++; });
//            PrevPageCommand = new ViewModelCommand(_ => { if (CurrentPage > 1) CurrentPage--; });
//            GoToPageCommand = new ViewModelCommand(p =>
//            {
//                if (p is int page)
//                    CurrentPage = page;
//            });
//            Load();
//        }

//        private void Load()
//        {
//            Logs.Clear();

//            var data = _repo.GetAuditReport(
//                FromDate,
//                ToDate,
//                null,
//                null);

//            _allLogs.Clear();
//            _allLogs.AddRange(data);

//            CurrentPage = 1;
//            RefreshData();

//            if (!string.IsNullOrWhiteSpace(SearchText))
//            {
//                data = data.Where(a =>
//                    (a.Username != null &&
//                     a.Username.IndexOf(SearchText,
//                        StringComparison.OrdinalIgnoreCase) >= 0)
//                 || (a.Action != null &&
//                     a.Action.IndexOf(SearchText,
//                        StringComparison.OrdinalIgnoreCase) >= 0)
//                 || (a.Reason != null &&
//                     a.Reason.IndexOf(SearchText,
//                        StringComparison.OrdinalIgnoreCase) >= 0)
//                ).ToList();
//            }

//            foreach (var r in data)
//                Logs.Add(r);
//        }

//        private void ExportExcel()
//        {
//            var sb = new StringBuilder();
//            sb.AppendLine("DateTime,Username,Action,Reason");

//            foreach (var l in Logs)
//            {
//                sb.AppendLine(
//                    $"{l.EventTime:dd.MM.yyyy HH:mm:ss}," +
//                    $"{l.Username},{l.Action},\"{l.Reason}\"");
//            }

//            File.WriteAllText("AuditLogs.csv", sb.ToString());
//            MessageBox.Show("Exported to AuditLogs.csv");
//        }

//        private void Print()
//        {
//            var dlg = new PrintDialog();
//            if (dlg.ShowDialog() == true)
//            {
//                var tb = new TextBlock
//                {
//                    Text = string.Join(Environment.NewLine,
//                        Logs.Select(l =>
//                            $"{l.EventTime:dd.MM.yyyy HH:mm:ss} | {l.Username} | {l.Action} | {l.Reason}")),
//                    FontSize = 11,
//                    Margin = new Thickness(20)
//                };

//                dlg.PrintVisual(tb, "Audit Logs");
//            }
//        }

//        private void ViewDetails(AuditTrailModel log)
//        {
//            if (log == null) return;

//            MessageBox.Show(
//                $"User: {log.Username}\n" +
//                $"Action: {log.Action}\n" +
//                $"Old: {log.OldValue}\n" +
//                $"New: {log.NewValue}\n" +
//                $"Reason: {log.Reason}",
//                "Audit Details",
//                MessageBoxButton.OK,
//                MessageBoxImage.Information);
//        }



//        private void RefreshData()
//        {
//            IEnumerable<AuditTrailModel> query = _allLogs;

//            if (!string.IsNullOrWhiteSpace(SearchText))
//            {
//                string s = SearchText.ToLower();

//                query = query.Where(a =>
//                    (a.Username != null && a.Username.ToLower().Contains(s)) ||
//                    (a.Action != null && a.Action.ToLower().Contains(s)) ||
//                    (a.Reason != null && a.Reason.ToLower().Contains(s))
//                );
//            }

//            var filteredList = query.ToList();

//            TotalPages = (int)Math.Ceiling((double)filteredList.Count / _pageSize);
//            if (TotalPages == 0) TotalPages = 1;

//            if (CurrentPage > TotalPages) CurrentPage = TotalPages;
//            if (CurrentPage < 1) CurrentPage = 1;

//            var pageRows = filteredList
//                .Skip((CurrentPage - 1) * _pageSize)
//                .Take(_pageSize)
//                .ToList();

//            Logs.Clear();
//            foreach (var item in pageRows)
//                Logs.Add(item);

//            UpdatePaginationButtons();
//        }

//        private void UpdatePaginationButtons()
//        {
//            PaginationButtons.Clear();

//            int start = Math.Max(1, CurrentPage - 2);
//            int end = Math.Min(TotalPages, CurrentPage + 2);

//            if (start > 1)
//            {
//                PaginationButtons.Add(new PaginationButton
//                {
//                    Content = "1",
//                    PageNumber = 1,
//                    Command = GoToPageCommand
//                });

//                if (start > 2)
//                    PaginationButtons.Add(new PaginationButton
//                    {
//                        Content = "...",
//                        IsEllipsis = true
//                    });
//            }

//            for (int i = start; i <= end; i++)
//            {
//                PaginationButtons.Add(new PaginationButton
//                {
//                    Content = i.ToString(),
//                    PageNumber = i,
//                    IsSelected = (i == CurrentPage),
//                    Command = GoToPageCommand
//                });
//            }

//            if (end < TotalPages)
//            {
//                if (end < TotalPages - 1)
//                    PaginationButtons.Add(new PaginationButton
//                    {
//                        Content = "...",
//                        IsEllipsis = true
//                    });

//                PaginationButtons.Add(new PaginationButton
//                {
//                    Content = TotalPages.ToString(),
//                    PageNumber = TotalPages,
//                    Command = GoToPageCommand
//                });
//            }
//        }

//    }
//}

using IrisAuth.Helpers;
using IrisAuth.Models;
using IrisAuth.Repositories;
using System;
using System.Collections.Generic;
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

        // ================= MASTER LIST =================
        private List<AuditTrailModel> _allLogs = new List<AuditTrailModel>();
        // ================= UI COLLECTION =================
        public ObservableCollection<AuditTrailModel> Logs { get; }
            = new ObservableCollection<AuditTrailModel>();

        // ================= PAGINATION BUTTONS =================
        public ObservableCollection<PaginationButton> PaginationButtons { get; }
            = new ObservableCollection<PaginationButton>();

        private AuditTrailModel _selectedLog;
        public AuditTrailModel SelectedLog
        {
            get => _selectedLog;
            set
            {
                _selectedLog = value;
                OnPropertyChanged(nameof(SelectedLog));
            }
        }

        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }

        // ================= SEARCH =================
        private string _searchText;
        public string SearchText
        {
            get => _searchText;
            set
            {
                _searchText = value;
                OnPropertyChanged(nameof(SearchText));
                CurrentPage = 1;
                RefreshData();
            }
        }

        // ================= PAGINATION =================
        private readonly int _pageSize = 10;
        private int _currentPage = 1;
        private int _totalPages = 1;

        public int CurrentPage
        {
            get => _currentPage;
            set
            {
                _currentPage = value;
                OnPropertyChanged(nameof(CurrentPage));
                RefreshData();
            }
        }

        public int TotalPages
        {
            get => _totalPages;
            set
            {
                _totalPages = value;
                OnPropertyChanged(nameof(TotalPages));
            }
        }

        // ================= COMMANDS =================
        public ICommand SearchCommand { get; }
        public ICommand ExportExcelCommand { get; }
        public ICommand PrintCommand { get; }
        public ICommand ViewCommand { get; }
        public ICommand NextPageCommand { get; }
        public ICommand PrevPageCommand { get; }
        public ICommand GoToPageCommand { get; }

        public UserLogsViewModel()
        {
            SearchCommand = new ViewModelCommand(_ => Load());
            ExportExcelCommand = new ViewModelCommand(_ => ExportExcel());
            PrintCommand = new ViewModelCommand(_ => Print());
            ViewCommand = new ViewModelCommand(l => ViewDetails(l as AuditTrailModel));

            NextPageCommand = new ViewModelCommand(_ =>
            {
                if (CurrentPage < TotalPages)
                    CurrentPage++;
            });

            PrevPageCommand = new ViewModelCommand(_ =>
            {
                if (CurrentPage > 1)
                    CurrentPage--;
            });

            GoToPageCommand = new ViewModelCommand(p =>
            {
                if (p is int page)
                    CurrentPage = page;
            });

            Load();
        }

        // ================= LOAD FROM DB =================
        private void Load()
        {
            var data = _repo.GetAuditReport(
                FromDate,
                ToDate,
                null,
                null);

            _allLogs.Clear();
            _allLogs.AddRange(data);

            CurrentPage = 1;
            RefreshData();
        }

        // ================= CORE PAGINATION LOGIC =================
        private void RefreshData()
        {
            IEnumerable<AuditTrailModel> query = _allLogs;

            if (!string.IsNullOrWhiteSpace(SearchText))
            {
                string s = SearchText.ToLower();
                query = query.Where(a =>
                    (a.Username != null && a.Username.ToLower().Contains(s)) ||
                    (a.Action != null && a.Action.ToLower().Contains(s)) ||
                    (a.Reason != null && a.Reason.ToLower().Contains(s))
                );
            }

            var filtered = query.ToList();

            TotalPages = (int)Math.Ceiling((double)filtered.Count / _pageSize);
            if (TotalPages == 0) TotalPages = 1;

            if (CurrentPage > TotalPages) CurrentPage = TotalPages;
            if (CurrentPage < 1) CurrentPage = 1;

            var pageRows = filtered
                .Skip((CurrentPage - 1) * _pageSize)
                .Take(_pageSize)
                .ToList();

            Logs.Clear();
            foreach (var row in pageRows)
                Logs.Add(row);

            UpdatePaginationButtons();
        }

        // ================= PAGE BUTTONS =================
        private void UpdatePaginationButtons()
        {
            PaginationButtons.Clear();

            int start = Math.Max(1, CurrentPage - 2);
            int end = Math.Min(TotalPages, CurrentPage + 2);

            if (start > 1)
            {
                PaginationButtons.Add(new PaginationButton
                {
                    Content = "1",
                    PageNumber = 1,
                    Command = GoToPageCommand
                });

                if (start > 2)
                    PaginationButtons.Add(new PaginationButton
                    {
                        Content = "...",
                        IsEllipsis = true
                    });
            }

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

            if (end < TotalPages)
            {
                if (end < TotalPages - 1)
                    PaginationButtons.Add(new PaginationButton
                    {
                        Content = "...",
                        IsEllipsis = true
                    });

                PaginationButtons.Add(new PaginationButton
                {
                    Content = TotalPages.ToString(),
                    PageNumber = TotalPages,
                    Command = GoToPageCommand
                });
            }
        }

        // ================= EXPORT / PRINT / VIEW =================
        private void ExportExcel()
        {
            var sb = new StringBuilder();
            sb.AppendLine("DateTime,Username,Action,Reason");

            foreach (var l in _allLogs)
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




