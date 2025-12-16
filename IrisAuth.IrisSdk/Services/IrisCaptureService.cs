using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IrisAuth.IrisSdk.Native;
using IrisAuth.IrisSdk.Models;
using System.Runtime.InteropServices;
namespace IrisAuth.IrisSdk.Services
{

    public class IrisCaptureService
    {
        private MidiIrisNative.PREVIEW_CALLBACK _previewCb;
        private MidiIrisNative.CAPTURE_COMPLETE_CALLBACK _completeCb;

        public Task<IrisCaptureResult> CaptureAsync(int timeoutMs, int minQuality)
        {
            var tcs = new TaskCompletionSource<IrisCaptureResult>();

            _completeCb = (
                int err,
                ref MidiIrisNative.MIDIRIS_IMAGE_QUALITY quality,
                ref MidiIrisNative.MIDIRIS_IMAGE_DATA image) =>
            {
                if (err != 0)
                {
                    tcs.TrySetResult(
                        IrisCaptureResult.CreateFailure(GetError(err))
                    );
                    return;
                }

                byte[] img = new byte[image.BitmapImageLen];
                Marshal.Copy(image.BitmapImage, img, 0, img.Length);

                tcs.TrySetResult(
                    IrisCaptureResult.CreateSuccess(
                        Convert.ToBase64String(img),
                        quality.Quality
                    )
                );
            };

            // Preview callback (optional – kept empty for now)
            _previewCb = (
                int err,
                ref MidiIrisNative.MIDIRIS_IMAGE_QUALITY q,
                ref MidiIrisNative.MIDIRIS_IMAGE_DATA img) => { };

            int rc = MidiIrisNative.MIDIRIS_Auth_StartCapture(
                minQuality,
                timeoutMs,
                _previewCb,
                _completeCb
            );

            if (rc != 0)
            {
                tcs.TrySetResult(
                    IrisCaptureResult.CreateFailure(GetError(rc))
                );
            }

            return tcs.Task;
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
