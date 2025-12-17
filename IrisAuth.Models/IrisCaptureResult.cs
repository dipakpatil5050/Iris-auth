using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IrisAuth.Models
{
    public class IrisCaptureResult
    {
        public bool Success { get; set; }
        public int Quality { get; set; }
        public string ImageBase64 { get; set; }
        public string Error { get; set; }
    }
}
