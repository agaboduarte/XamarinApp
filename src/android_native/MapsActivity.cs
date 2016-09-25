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
using Android.Support.V4.App;
using Android.Gms.Maps;
using Android.Gms.Maps.Model;

namespace Com.Duarti.XamarinApp
{
    [Activity(Label = "MapsActivity")]
    public class MapsActivity : FragmentActivity, IOnMapReadyCallback
    {
        private GoogleMap mMap;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            
            SetContentView(Resource.Layout.activity_maps);
            // Obtain the SupportMapFragment and get notified when the map is ready to be used.
            var mapFragment = (SupportMapFragment)SupportFragmentManager.FindFragmentById(Resource.Id.map);
            mapFragment.GetMapAsync(this);
        }

        public void OnMapReady(GoogleMap googleMap)
        {
            mMap = googleMap;

            // Add a marker in Sydney and move the camera
            var sydney = new LatLng(-34, 151);
            mMap.AddMarker(new MarkerOptions().SetPosition(sydney).SetTitle("Marker in Sydney"));
            mMap.MoveCamera(CameraUpdateFactory.NewLatLng(sydney));
        }
    }
}