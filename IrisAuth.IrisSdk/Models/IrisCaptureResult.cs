using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IrisAuth.IrisSdk.Models
{
    public class IrisCaptureResult
    {
        public bool Success { get; private set; }
        public string Error { get; private set; }
        public string ImageBase64 { get; private set; }
        public int Quality { get; private set; }

        public static IrisCaptureResult CreateSuccess(string base64, int quality)
        {
            return new IrisCaptureResult
            {
                Success = true,
                ImageBase64 = base64,
                Quality = quality
            };
        }

        public static IrisCaptureResult CreateFailure(string error)
        {
            return new IrisCaptureResult
            {
                Success = false,
                Error = error
            };
        }
    }
}
