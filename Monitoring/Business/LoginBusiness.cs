using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Monitoring.Models;
using Newtonsoft.Json;

namespace Monitoring.Assets
{
    public class LoginBusiness
    {
        public static async Task<LoginModel> GetUser(string user, string password)
        {
            
            using (var client = new HttpClient())
            {
                string apiUrl = "http://fixmensintegration.azurewebsites.net/api/Login?user=" + user + "&password=" + password;
                HttpResponseMessage response = await client.GetAsync(apiUrl);

                if (response.IsSuccessStatusCode)
                {
                    string result = await response.Content.ReadAsStringAsync();
                    var rootResult = JsonConvert.DeserializeObject<LoginModel>(result);
                    return rootResult;
                }
                return null;
            }
        }
    }
}