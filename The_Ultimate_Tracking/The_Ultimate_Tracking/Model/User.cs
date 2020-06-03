using System;
using System.Collections.Generic;
using System.Text;

namespace The_Ultimate_Tracking.Model
{
    class User
    {
        public string email { get; set; }
        public string password { get; set; }
    }

    class Vehicle
    {
        public string imei { get; set; }
        public string lpn { get; set; }
        public string name { get; set; }
        public string user_id { get; set; }
        public string vehicle_id { get; set; }
    }
}
