using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using IrisAuth.IrisSdk.Native;
namespace IrisAuth.IrisSdk.Device
{
    public class IrisDeviceService
    {
        public bool IsInitialized { get; private set; }

        public bool Init(string deviceName, out string error)
        {
            error = null;

            var info = new MidiIrisNative.MIDIris_DEVICE_INFO();
            byte[] nameBytes = Encoding.ASCII.GetBytes(deviceName);

            int rc = MidiIrisNative.MIDIRIS_Auth_InitDevice(
                0,
                nameBytes,
                nameBytes.Length,
                ref info
            );

            if (rc != 0)
            {
                error = GetError(rc);
                return false;
            }

            IsInitialized = true;
            return true;
        }

        public void Uninit()
        {
            if (!IsInitialized)
                return;

            MidiIrisNative.MIDIRIS_Auth_UninitDevice();
            IsInitialized = false;
        }

        private string GetError(int code)
        {
            IntPtr ptr = MidiIrisNative.MIDIRIS_Auth_GetErrDescription(code);
            return ptr == IntPtr.Zero
                ? $"Error {code}"
                : Marshal.PtrToStringAnsi(ptr);
        }
    }
}
