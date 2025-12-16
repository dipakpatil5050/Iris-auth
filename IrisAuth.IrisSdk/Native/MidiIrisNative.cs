using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
namespace IrisAuth.IrisSdk.Native
{
    internal static class MidiIrisNative
    {
        private const string DLL = "MIDIris_Auth_Core.dll";

        /* =======================
           ENUMS
        ======================== */

        public enum DEVICE_DETECTION_EVENT
        {
            EVENT_DISCONNECTED = 0,
            EVENT_CONNECTED = 1
        }

        public enum IMAGE_FORMAT
        {
            BMP = 0,
            RAW = 1,
            K3 = 2,
            K7 = 3
        }

        public enum MIDIRIS_Auth_LOG_LEVEL
        {
            OFF = 0,
            ERROR = 1
        }

        /* =======================
           STRUCTS
        ======================== */

        [StructLayout(LayoutKind.Sequential)]
        public struct MIDIRIS_IMAGE_QUALITY
        {
            public int Quality;
            public int X;
            public int Y;
            public int R;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct MIDIRIS_IMAGE_DATA
        {
            public IntPtr BitmapImage;
            public int BitmapImageLen;
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        public struct MIDIris_DEVICE_INFO
        {
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 13)]
            public string SerialNo;

            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 13)]
            public string Firmware;

            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 7)]
            public string Make;

            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 12)]
            public string Model;

            public int Width;
            public int Height;
            public int DPI;
        }

        /* =======================
           CALLBACK DELEGATES
        ======================== */

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void PREVIEW_CALLBACK(
            int errorCode,
            ref MIDIRIS_IMAGE_QUALITY quality,
            ref MIDIRIS_IMAGE_DATA imageData
        );

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void CAPTURE_COMPLETE_CALLBACK(
            int errorCode,
            ref MIDIRIS_IMAGE_QUALITY quality,
            ref MIDIRIS_IMAGE_DATA imageData
        );

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void DEVICE_DETECTION(
            string deviceName,
            DEVICE_DETECTION_EVENT status
        );

        /* =======================
           DLL IMPORTS
        ======================== */

        [DllImport(DLL, CallingConvention = CallingConvention.Cdecl)]
        public static extern int MIDIRIS_Auth_InitDevice(
            int fd,
            byte[] pcProductName,
            int productNameLen,
            ref MIDIris_DEVICE_INFO deviceInfo
        );

        [DllImport(DLL, CallingConvention = CallingConvention.Cdecl)]
        public static extern int MIDIRIS_Auth_UninitDevice();

        [DllImport(DLL, CallingConvention = CallingConvention.Cdecl)]
        public static extern int MIDIRIS_Auth_StartCapture(
            int quality,
            int timeout,
            PREVIEW_CALLBACK previewCallback,
            CAPTURE_COMPLETE_CALLBACK captureCallback
        );

        [DllImport(DLL, CallingConvention = CallingConvention.Cdecl)]
        public static extern int MIDIRIS_Auth_StopCapture();

        [DllImport(DLL, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr MIDIRIS_Auth_GetErrDescription(int errorNo);
    }
}
