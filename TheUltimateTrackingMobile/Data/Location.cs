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

namespace TheUltimateTrackingMobile.Data
{
    public class Location
    {
        public string address { get; set; }
        public string direction { get; set; }
        public string lat { get; set; }
        public string lon { get; set; }
        public string speed { get; set; }
        public string time { get; set; }
        public string vehicle_id { get; set; }

    }
}