using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace IrisAuth.ViewModels
{
    public class PaginationButton
    {
        public string Content { get; set; }
        public int PageNumber { get; set; }
        public bool IsSelected { get; set; }
        public bool IsEllipsis { get; set; }
        public ICommand Command { get; set; }
    }
}
