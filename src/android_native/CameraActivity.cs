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

namespace Com.Duarti.XamarinApp
{
    [Activity(Label = "Camera", Theme = "@style/AppTheme.NoActionBar", ParentActivity = typeof(MainActivity))]
    public class CameraActivity : AppCompatActivity
    {

        private Camera mCamera;
        private CameraPreview mPreview;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.activity_camera_preview);

            var toolbar = (Toolbar)FindViewById(Resource.Id.toolbar);

            SetSupportActionBar(toolbar);

            SupportActionBar.SetDisplayHomeAsUpEnabled(true);

            // Create an instance of Camera
            mCamera = GetCamera();

            if (mCamera != null)
            {
                switch (Resources.Configuration.Orientation)
                {
                    case Android.Content.Res.Orientation.Landscape:
                        mCamera.SetDisplayOrientation(0);
                        break;
                    case Android.Content.Res.Orientation.Portrait:
                        mCamera.SetDisplayOrientation(90);
                        break;
                    default:
                        mCamera.SetDisplayOrientation(90);
                        break;
                }

                // Create our Preview view and set it as the content of our activity.
                mPreview = new CameraPreview(this, mCamera);
                var preview = (ViewGroup)FindViewById(Resource.Id.camera_preview);
                preview.AddView(mPreview);
            }
        }

        public static Intent NewIntent(Context context)
        {
            var intent = new Intent(context, typeof(CameraActivity));

            return intent;
        }

        public static Camera GetCamera()
        {
            try
            {
                return Camera.Open();
            }
            catch
            {
            }

            return null;
        }
    }
}