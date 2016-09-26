using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.OS;
using Android.Support.Design.Widget;
using Android.Support.V7.App;
using Android.Support.V7.Widget;
using Android.Support.V4.Widget;
using Android.Support.V4.View;
using Android.Gms.Maps;
using Android.Graphics;

namespace Com.Duarti.XamarinApp
{
    [Activity(Label = "XamarinApp", MainLauncher = true, Icon = "@drawable/icon", Theme = "@style/AppTheme.NoActionBar")]
    public class MainActivity : AppCompatActivity, NavigationView.IOnNavigationItemSelectedListener
    {
        protected Android.Widget.RelativeLayout ContentMain
        {
            get
            {
                return FindViewById<Android.Widget.RelativeLayout>(Resource.Id.content_main);
            }
        }

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);

            var toolbar = (Toolbar)FindViewById(Resource.Id.toolbar);
            SetSupportActionBar(toolbar);

            var fab = (FloatingActionButton)FindViewById(Resource.Id.fab);
            fab.Click += delegate (object sender, EventArgs e)
            {
                Snackbar
                    .Make((View)sender, "Replace with your own action", Snackbar.LengthLong)
                    .SetAction("Action", default(Action<View>))
                    .Show();
            };

            var drawer = (DrawerLayout)FindViewById(Resource.Id.drawer_layout);
            var toggle = new ActionBarDrawerToggle(
                this,
                drawer,
                toolbar, 
                Resource.String.navigation_drawer_open,
                Resource.String.navigation_drawer_close);

            drawer.AddDrawerListener(toggle);

            toggle.SyncState();

            var navigationView = (NavigationView)FindViewById(Resource.Id.nav_view);
            navigationView.SetNavigationItemSelectedListener(this);
        }

        public override void OnBackPressed()
        {
            var drawer = (DrawerLayout)FindViewById(Resource.Id.drawer_layout);

            if (drawer.IsDrawerOpen(GravityCompat.Start))
            {
                drawer.CloseDrawer(GravityCompat.Start);
            }
            else
            {
                base.OnBackPressed();
            }
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            // Inflate the menu; this adds items to the action bar if it is present.
            MenuInflater.Inflate(Resource.Menu.main, menu);
            return true;
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            // Handle action bar item clicks here. The action bar will
            // automatically handle clicks on the Home/Up button, so long
            // as you specify a parent activity in AndroidManifest.xml.
            int id = item.ItemId;

            //noinspection SimplifiableIfStatement
            if (id == Resource.Id.action_settings)
            {
                Snackbar
                    .Make(FindViewById(Resource.Id.fab), "Replace with your own action", Snackbar.LengthLong)
                    .SetAction("Action", default(Action<View>))
                    .Show();

                return true;
            }

            if (id == Resource.Id.action_view_map)
            {
                var intent = MapsActivity.NewIntent(ApplicationContext);

                StartActivity(intent);

                return true;
            }

            return base.OnOptionsItemSelected(item);
        }

        public bool OnNavigationItemSelected(IMenuItem menuItem)
        {
            // Handle navigation view item clicks here.
            int id = menuItem.ItemId;

            if (id == Resource.Id.nav_camera)
            {
                var intent = CameraActivity.NewIntent(ApplicationContext);

                StartActivity(intent);
            }
            else if (id == Resource.Id.nav_gallery)
            {
            }
            else if (id == Resource.Id.nav_slideshow)
            {
            }
            else if (id == Resource.Id.nav_manage)
            {
            }
            else if (id == Resource.Id.nav_share)
            {
            }
            else if (id == Resource.Id.nav_send)
            {
            }

            var drawer = (DrawerLayout)FindViewById(Resource.Id.drawer_layout);
            drawer.CloseDrawer(GravityCompat.Start);

            return true;
        }
    }
}

