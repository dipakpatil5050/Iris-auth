using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IrisAuth.ViewModels
{
    public class MatchingViewModel : ViewModelBase
    {
        private string _statusMessage;
        public string StatusMessage
        {
            get { return _statusMessage; }
            set
            {
                _statusMessage = value;
                OnPropertyChanged(nameof(StatusMessage));
            }
        }
        private int _progress;
        public int Progress
        {
            get { return _progress; }
            set
            {
                _progress = value;
                OnPropertyChanged(nameof(Progress));
            }
        }
        private bool _isMatching;
        public bool IsMatching
        {
            get { return _isMatching; }
            set
            {
                _isMatching = value;
                OnPropertyChanged(nameof(IsMatching));
            }
        }
        public MatchingViewModel()
        {
            StatusMessage = "Ready to match.";
            Progress = 0;
            IsMatching = false;
        }


    }
}
