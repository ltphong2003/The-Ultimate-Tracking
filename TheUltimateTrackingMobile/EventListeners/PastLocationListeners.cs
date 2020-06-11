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
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = false)] 
    public class PastLocationListeners : Java.Lang.Object, IValueEventListener
    {
        List<Location> pastlocationList = new List<Location>();

        public event EventHandler<pastlocationDataEventArgs> pastlocationDataRetrieved;

        public class pastlocationDataEventArgs : EventArgs
        {
            public List<Location> PastLocation { get; set; }
        }

        public void OnCancelled(DatabaseError error)
        {

        }

        public void OnDataChange(DataSnapshot snapshot)
        {
            if (snapshot.Value != null)
            {
                var child = snapshot.Children.ToEnumerable<DataSnapshot>();
                pastlocationList.Clear();

                foreach (DataSnapshot pastlocationData in child)
                {

                    Location location = new Location();

                    if (pastlocationData.Child("address").Value != null) location.address = pastlocationData.Child("address").Value.ToString();
                    if (pastlocationData.Child("direction").Value != null) location.direction = pastlocationData.Child("direction").Value.ToString();
                    if (pastlocationData.Child("lat").Value != null) location.lat = pastlocationData.Child("lat").Value.ToString();
                    if (pastlocationData.Child("lon").Value != null) location.lon = pastlocationData.Child("lon").Value.ToString();
                    if (pastlocationData.Child("speed").Value != null) location.speed = pastlocationData.Child("speed").Value.ToString();
                    if (pastlocationData.Child("time").Value != null) location.time = pastlocationData.Child("time").Value.ToString();
                    if (pastlocationData.Child("vehicle_id").Value != null) location.vehicle_id = pastlocationData.Child("vehicle_id").Value.ToString();

                    pastlocationList.Add(location);
                }
                pastlocationDataRetrieved.Invoke(this, new pastlocationDataEventArgs { PastLocation = pastlocationList });
            }
        }

        public void Create()
        {
            string path = "past/1" + "13-05-2020";
            DatabaseReference locationref = AppDataHelpers.GetDatabase().GetReference(path);
            locationref.AddValueEventListener(this);
        }
    }
}