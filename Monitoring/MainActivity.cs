using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.Widget;
using Android.OS;
using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;
using Monitoring.Assets;
using Monitoring.Models;
using Newtonsoft.Json;


namespace Monitoring
{
    [Activity(Label = "Monitoring", MainLauncher = true, Icon = "@drawable/Icon2")]
    public class MainActivity : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            AppCenter.Start("67583e9e-49e1-4682-9a89-3114ad3cc49b",
                typeof(Analytics), typeof(Crashes));
            base.OnCreate(bundle);

            SetContentView(Resource.Layout.Main);
            // Get our button from the layout resource,  
            // and attach an event to it  
            EditText txtUser = FindViewById<EditText>(Resource.Id.txtUser);
            EditText txtPassword = FindViewById<EditText>(Resource.Id.txtPassword);
            
            var btnsign = FindViewById<Button>(Resource.Id.btnLogin);
            var json = new LoginModel();
            
            btnsign.Click += async (sender, e) => {

                // Get the latitude and longitude entered by the user and create a query.
           
                // Fetch the weather information asynchronously, 
                // parse the results, then update the screen:
                json = await LoginBusiness.GetUser(txtUser.Text, txtPassword.Text);
                if (json != null)
                {
                    Toast.MakeText(this, "Login Success", ToastLength.Short).Show();
                    var showLocationActivity = new Intent(this, typeof(ShowLocation));
                    showLocationActivity.PutExtra("userData", JsonConvert.SerializeObject(json));
                    StartActivity(showLocationActivity);                    
                }
                else
                {
                    Toast.MakeText(this, "Username or Password invalid", ToastLength.Short).Show();
                }
                // ParseAndDisplay (json);
            };

          




            // Set our view from the "main" layout resource
            // SetContentView (Resource.Layout.Main);
        }

        
    }
}

