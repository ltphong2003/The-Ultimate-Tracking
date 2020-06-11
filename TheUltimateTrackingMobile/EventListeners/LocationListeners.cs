﻿using System;
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
    public class LocationListeners : Java.Lang.Object, IValueEventListener
    {
        List<Location> locationList = new List<Location>();

        public event EventHandler<locationDataEventArgs> locationDataRetrieved;

        public class locationDataEventArgs : EventArgs
        {
            public List<Location> Location { get; set; }
        }

        public void OnCancelled(DatabaseError error)
        {

        }

        public void OnDataChange(DataSnapshot snapshot)
        {
            if (snapshot.Value != null)
            {
                var child = snapshot.Children.ToEnumerable<DataSnapshot>();
                locationList.Clear();

                foreach (DataSnapshot locationData in child)
                {
                    
                    Location location = new Location();
                        
                    if (locationData.Child("address") != null) location.address = locationData.Child("address").Value.ToString();
                    if (locationData.Child("direction") != null) location.direction = locationData.Child("direction").Value.ToString();
                    if (locationData.Child("lat") != null) location.lat = locationData.Child("lat").Value.ToString();
                    if (locationData.Child("lon") != null) location.lon = locationData.Child("lon").Value.ToString();
                    if (locationData.Child("speed") != null) location.speed = locationData.Child("speed").Value.ToString();
                    if (locationData.Child("time") != null) location.time = locationData.Child("time").Value.ToString();
                    if (locationData.Child("vehicle_id") != null) location.vehicle_id = locationData.Child("vehicle_id").Value.ToString();

                    locationList.Add(location);
                }
                locationDataRetrieved.Invoke(this, new locationDataEventArgs { Location = locationList });
            }
        }

        public void Create()
        {
            DatabaseReference locationref = AppDataHelpers.GetDatabase().GetReference("now");
            locationref.AddValueEventListener(this);
        }
    }
}