using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Hardware.Camera2;
using Android.Util;

namespace Com.Duarti.XamarinApp.Camera2
{
    public class CameraStateCallback : CameraDevice.StateCallback
    {
        public CameraDevice Camera { get; protected set; }

        public event EventHandler OnChanged;

        public override void OnDisconnected(CameraDevice camera)
        {
            Camera = null;
            OnChanged(this, EventArgs.Empty);
        }

        public override void OnError(CameraDevice camera, [GeneratedEnum] Android.Hardware.Camera2.CameraError error)
        {
            Log.Debug("CameraStateCallback", string.Format("OnError: {0}", error));
            OnChanged(this, EventArgs.Empty);
        }

        public override void OnOpened(CameraDevice camera)
        {
            Camera = camera;
            OnChanged(this, EventArgs.Empty);
        }
    }

    public class CameraCaptureSessionStateCallback : CameraCaptureSession.StateCallback
    {
        public CameraCaptureSessionStateCallback(Action<CameraCaptureSession> callback)
        {
            Callback = callback;
        }

        public Action<CameraCaptureSession> Callback { get; set; }

        public override void OnConfigured(CameraCaptureSession session)
        {
            Callback(session);
        }

        public override void OnConfigureFailed(CameraCaptureSession session)
        {
            Callback(session);
        }
    }
}