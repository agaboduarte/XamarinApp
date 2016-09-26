using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Support.V4.App;
using Android.Gms.Maps;
using Android.Gms.Maps.Model;
using Android.Support.V7.App;
using Android.App;
using Android.Support.V7.Widget;

namespace Com.Duarti.XamarinApp
{
    [Activity(Label = "Maps", Icon = "@drawable/icon", Theme = "@style/AppTheme.NoActionBar", ParentActivity = typeof(MainActivity))]
    public class MapsActivity : AppCompatActivity, IOnMapReadyCallback
    {
        public GoogleMap Map { get; protected set; }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_maps);

            var toolbar = (Toolbar)FindViewById(Resource.Id.toolbar);

            SetSupportActionBar(toolbar);

            SupportActionBar.SetDisplayHomeAsUpEnabled(true);

            var mapFragment = (SupportMapFragment)SupportFragmentManager.FindFragmentById(Resource.Id.map);

            mapFragment.GetMapAsync(this);
        }

        public void OnMapReady(GoogleMap googleMap)
        {
            Map = googleMap;

            // Add a marker in Sydney and move the camera
            var sydney = new LatLng(-34, 151);

            var marker = new MarkerOptions()
                .SetPosition(sydney)
                .SetTitle("Marker in Sydney");

            Map.AddMarker(marker);
            Map.MoveCamera(CameraUpdateFactory.NewLatLng(sydney));

            Map.MyLocationEnabled = true;
        }

        public static Intent NewIntent(Context applicationContext)
        {
            var intent = new Intent(applicationContext, typeof(MapsActivity));

            return intent;
        }
    }
}