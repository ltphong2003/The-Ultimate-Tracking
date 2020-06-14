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
    public class VehicleId
    {
        public string imei { get; set; }
        public string lpn { get; set; }
        public string name { get; set; }
        public string user_id { get; set; }
        public string vehicle_id { get; set; }
    }
}