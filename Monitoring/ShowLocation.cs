using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Locations;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using Com.Microsoft.Appcenter;
using Com.Microsoft.Appcenter.Distribute;
using Monitoring.Models;
using Newtonsoft.Json;

namespace Monitoring
{
    [Activity(Label = "ShowLocation")]
    public class ShowLocation : Activity, ILocationListener
    {

        protected override void OnCreate(Bundle savedInstanceState)
        {
            

            base.OnCreate(savedInstanceState);


            SetContentView(Resource.Layout.ShowLocation);
            string text = Intent.GetStringExtra("userData") ?? "Data not available";
            var user = JsonConvert.DeserializeObject<LoginModel>(text);

            EditText txtName = FindViewById<EditText>(Resource.Id.txtName);
           

            txtName.Text = user.NOMBRE;
            var locMgr = GetSystemService(Context.LocationService) as LocationManager;

            string Provider = LocationManager.GpsProvider;

            if (locMgr.IsProviderEnabled(Provider))
            {
                locMgr.RequestLocationUpdates(Provider, 2000, 1, this);
                Location location = locMgr.GetLastKnownLocation(Provider);

                EditText latitude = FindViewById<EditText>(Resource.Id.txtLatitud);
                EditText longitude = FindViewById<EditText>(Resource.Id.txtLongitud);
                latitude.Text = location.Latitude.ToString(CultureInfo.CurrentCulture);
                longitude.Text = location.Longitude.ToString(CultureInfo.CurrentCulture);
            }
            else
            {
                Toast.MakeText(this, Provider + " is not available. Does the device have location services enabled?", ToastLength.Short).Show();

            }
            var btnMap = FindViewById<Button>(Resource.Id.btnMap);

            btnMap.Click += (sender, e) =>
            {

                var geoUri = Android.Net.Uri.Parse("geo:"+ FindViewById<EditText>(Resource.Id.txtLatitud).Text + ","+ FindViewById<EditText>(Resource.Id.txtLongitud).Text);
                var mapIntent = new Intent(Intent.ActionView, geoUri);
                StartActivity(mapIntent);
            };


            // Create your application here
        }

        protected override void OnPause()
        {
            base.OnPause();
            var locMgr = GetSystemService(Context.LocationService) as LocationManager;
            locMgr.RemoveUpdates(this);
        }

        void ILocationListener.OnLocationChanged(Location location)
        {
            EditText latitude = FindViewById<EditText>(Resource.Id.txtLatitud);
            EditText longitude = FindViewById<EditText>(Resource.Id.txtLongitud);
            latitude.Text = location.Latitude.ToString(CultureInfo.CurrentCulture);
            longitude.Text = location.Longitude.ToString(CultureInfo.CurrentCulture);

           
        }

        void ILocationListener.OnProviderDisabled(string provider)
        {
            Toast.MakeText(this, provider + " is disabled", ToastLength.Short).Show();
        }

        void ILocationListener.OnProviderEnabled(string provider)
        {
            Toast.MakeText(this, provider + " is enabled", ToastLength.Short).Show();
        }

        void ILocationListener.OnStatusChanged(string provider, Availability status, Bundle extras)
        {
            Toast.MakeText(this, provider + " changed the status: " + status, ToastLength.Short).Show();
        }
    }
}