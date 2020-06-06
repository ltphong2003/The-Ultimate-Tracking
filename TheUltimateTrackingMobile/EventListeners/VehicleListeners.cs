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
using Firebase.Database;
using Firebase.Events;
using TheUltimateTrackingMobile.Data;
using TheUltimateTrackingMobile.Helpers;

namespace TheUltimateTrackingMobile.EventListeners
{
    public class VehicleListeners : Java.Lang.Object, IValueEventListener
    {
        List<VehicleId> vehicleidList = new List<VehicleId>();
        //List<VehicleDetail> vehicledetailList = new List<VehicleDetail>();

        public event EventHandler<vehicleDataEventArgs> vehicleDataRetrieved;

        public class vehicleDataEventArgs : EventArgs
        {
            public List<VehicleId> VehicleId { get; set; }

            //public List<VehicleDetail> VehicleDetail { get; set; }
        }

        public void OnCancelled(DatabaseError error)
        {
            
        }

        public void OnDataChange(DataSnapshot snapshot)
        {
            if (snapshot.Value != null)
            {
                var child = snapshot.Children.ToEnumerable<DataSnapshot>();
                vehicleidList.Clear();
                //vehicledetailList.Clear();

                foreach (DataSnapshot vehicleData in child)
                {
                    if (vehicleData.Key != "detail")
                    {
                        VehicleId vehicleid = new VehicleId();
                        vehicleid.imei = vehicleData.Child("imei").Value.ToString();
                        vehicleid.lpn = vehicleData.Child("lpn").Value.ToString();
                        vehicleid.name = vehicleData.Child("name").Value.ToString();
                        vehicleid.user_id = vehicleData.Child("user_id").Value.ToString();
                        vehicleid.vehicle_id = vehicleData.Child("vehicle_id").Value.ToString();

                        vehicleidList.Add(vehicleid);
                    }
                    else
                    {
                        //VehicleDetail vehicledetail = new VehicleDetail();
                        //vehicledetail.number = vehicleData.Child("number").Value.ToString();

                        //vehicledetailList.Add(vehicledetail);
                    }

                }
                vehicleDataRetrieved.Invoke(this, new vehicleDataEventArgs { VehicleId = vehicleidList });
            }
        }

        public void Create()
        {
            DatabaseReference vehicleref = AppDataHelpers.GetDatabase().GetReference("vehicle");
            vehicleref.AddValueEventListener(this);
        }
    }
}