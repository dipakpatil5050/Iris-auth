using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
namespace IrisAuth.IrisSdk.Native
{
    [StructLayout(LayoutKind.Sequential)]
    internal struct MIDIRIS_IMAGE_QUALITY
    {
        public int Quality;
        public int X;
        public int Y;
        public int R;
    }

    [StructLayout(LayoutKind.Sequential)]
    internal struct MIDIRIS_IMAGE_DATA
    {
        public IntPtr BitmapImage;
        public int BitmapImageLen;
    }
    [StructLayout(LayoutKind.Sequential)]
    public struct MIDIris_DEVICE_INFO
    {
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 13)]
        public string SerialNo;

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 12)]
        public string Model;

        public int Width;
        public int Height;
        public int DPI;
    }
}
