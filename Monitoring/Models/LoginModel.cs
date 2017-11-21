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

namespace Monitoring.Models
{
    public class LoginModel
    {
        
       
            public object INTEGRANTES { get; set; }
            public int CODIGO { get; set; }
            public string NOMBRE { get; set; }
            public string PASSWD { get; set; }
            public string TALLER { get; set; }
            public DateTime FECHA_AUD { get; set; }
            public string HORA_AUD { get; set; }
            public int IDINTEGRANTE { get; set; }
        
    }
}