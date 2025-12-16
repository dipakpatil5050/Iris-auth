using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IrisAuth.Biometric
{
    public interface IBiometricService
    {
        Task<BiometricCaptureResult> CaptureAsync(int timeoutMs, int minQuality);
    }
}
