using Android.Content;

using Android.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android.Runtime;
using Android.Hardware;
using Android.Graphics;
using Android.Util;

namespace Com.Duarti.XamarinApp
{
    public class CameraPreview : SurfaceView, ISurfaceHolderCallback
    {
        private Android.Hardware.Camera mCamera { get; set; }
      
        public CameraPreview(Context context, Android.Hardware.Camera camera)
            : base(context)
        {
            mCamera = camera;

            Holder.AddCallback(this);

            // deprecated setting, but required on Android versions prior to 3.0
            Holder.SetType(SurfaceType.PushBuffers);
        }

        public void SurfaceChanged(ISurfaceHolder holder, [GeneratedEnum] Format format, int width, int height)
        {
            // If your preview can change or rotate, take care of those events here.
            // Make sure to stop the preview before resizing or reformatting it.

            if (Holder.Surface == null)
            {
                // preview surface does not exist
                return;
            }

            // stop preview before making changes
            try
            {
                mCamera.StopPreview();
            }
            catch (Exception e)
            {
                // ignore: tried to stop a non-existent preview
            }

            // set preview size and make any resize, rotate or
            // reformatting changes here

            // start preview with new settings
            try
            {
                mCamera.SetPreviewDisplay(Holder);
                mCamera.StartPreview();

            }
            catch (Exception e)
            {
                Log.Debug("", "Error starting camera preview: " + e.Message);
            }
        }

        public void SurfaceCreated(ISurfaceHolder holder)
        {
            // The Surface has been created, now tell the camera where to draw the preview.
            try
            {
                mCamera.SetPreviewDisplay(holder);
                mCamera.StartPreview();
            }
            catch (Java.IO.IOException e)
            {
                Log.Debug("", "Error setting camera preview: " + e.Message);
            }
        }

        public void SurfaceDestroyed(ISurfaceHolder holder)
        {
            // If your preview can change or rotate, take care of those events here.
            // Make sure to stop the preview before resizing or reformatting it.

            if (Holder.Surface == null)
            {
                // preview surface does not exist
                return;
            }

            // stop preview
            try
            {
                mCamera.StopPreview();
                mCamera.Release();
                mCamera.Dispose();
            }
            catch (Exception e)
            {
                // ignore: tried to stop a non-existent preview
            }

            GC.Collect();
        }
    }
}