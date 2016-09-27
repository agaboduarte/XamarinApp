using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Support.V7.App;
using Android.Support.V7.Widget;
using Android.Hardware;
using Android.Hardware.Camera2;
using Android.Util;
using Com.Duarti.XamarinApp.Camera2;
using Java.Lang;

namespace Com.Duarti.XamarinApp
{
    [Activity(Label = "Camera", Theme = "@style/AppTheme.NoActionBar", ParentActivity = typeof(MainActivity))]
    public class Camera2Activity : AppCompatActivity
    {
        private CameraStateCallback CameraCallback;
        private AutoFitTextureView Texture;
        private CameraCaptureSession CameraSession;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.activity_camera_preview);

            var toolbar = (Toolbar)FindViewById(Resource.Id.toolbar);

            SetSupportActionBar(toolbar);

            SupportActionBar.SetDisplayHomeAsUpEnabled(true);

            var manager = (CameraManager)GetSystemService(Context.CameraService);
            var cameraId = manager.GetCameraIdList().First();

            var preview = (ViewGroup)FindViewById(Resource.Id.camera_preview);


            CameraCallback = new CameraStateCallback();

            CameraCallback.OnChanged += delegate (object sender, EventArgs e)
            {
                Texture = new AutoFitTextureView(this);

                Texture.SurfaceTextureAvailable += Texture_SurfaceTextureAvailable;
                Texture.SurfaceTextureDestroyed += Texture_SurfaceTextureDestroyed;

                preview.AddView(Texture);
            };

            manager.OpenCamera(cameraId, CameraCallback, null);
        }

        private void Texture_SurfaceTextureDestroyed(object sender, TextureView.SurfaceTextureDestroyedEventArgs e)
        {
            CameraSession.Close();
            CameraCallback.Camera.Close();
        }

        private void Texture_SurfaceTextureAvailable(object sender, TextureView.SurfaceTextureAvailableEventArgs e)
        {
            var camera = CameraCallback.Camera;

            var surfaceTexture = Texture.SurfaceTexture;

            surfaceTexture.SetDefaultBufferSize(e.Width, e.Height);

            var surface = new Surface(surfaceTexture);

            camera.CreateCaptureSession(new List<Surface>() { surface }, new CameraCaptureSessionStateCallback((session) =>
            {
                CameraSession = session;

                var builder = camera.CreateCaptureRequest(CameraTemplate.Preview);
                builder.AddTarget(surface);

                var request = builder.Build();

                CameraSession.SetRepeatingRequest(request, null, null);
            }), null);
        }

        public static Intent NewIntent(Context context)
        {
            var intent = new Intent(context, typeof(Camera2Activity));

            return intent;
        }
    }
}