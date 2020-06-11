using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Runtime;
using Android.Widget;
using Android.Gms.Maps;
using Android;
using Android.Support.V4.App;
using Android.Content.PM;
using Firebase.Database;
using TheUltimateTrackingMobile.Helpers;
using TheUltimateTrackingMobile.EventListeners;
using System.Collections.Generic;
using TheUltimateTrackingMobile.Data;
using Android.Gms.Maps.Model;
using System.Linq;
using System;
using Android.Media;
using Android.Graphics;
using Org.Apache.Http.Conn.Routing;
using Android.Views;
using System.ComponentModel;
using Android.Text;
using Android.Support.Design.Widget;
using Firebase;
using Java.Sql;
using TheUltimateTrackingMobile.Activities;

namespace TheUltimateTrackingMobile.Activities
{
    [Activity(Label = "PastActivity")]
    public class PastActivity : AppCompatActivity
    {
        Button closeButton;
        TextView pastText;

        PastLocationListeners pastlocationListeners;
        List<Location> pastlocationList;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.history);

            ContactControls();
            RetrievePastLocation();
        }

        void ContactControls()
        {
            closeButton = (Button)FindViewById(Resource.Id.closeBtn);
            spinner = (Spinner)FindViewById(Resource.Id.pastspinner);
            spinner.ItemSelected += new EventHandler<AdapterView.ItemSelectedEventArgs>(spinner_ItemSelected);
        }


        public void RetrievePastLocation()
        {
            pastlocationListeners = new PastLocationListeners();
            pastlocationListeners.Create();
            pastlocationListeners.pastlocationDataRetrieved += PastlocationListeners_pastlocationDataRetrieved;
        }

        private void PastlocationListeners_pastlocationDataRetrieved(object sender, PastLocationListeners.pastlocationDataEventArgs e)
        {
            pastlocationList = e.PastLocation;
            SetHistory();
        }

        
        Spinner spinner;

        void SetHistory()
        {
            List<string> past = new List<string>();
            pastText = (TextView)FindViewById(Resource.Id.pastTxt);
            for (int i = 0; i < pastlocationList.Count; ++i)
            {
                past.Add(pastlocationList[i].address);

            }
            var ArrayAdapter1 = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleSpinnerItem, past);
            spinner.Adapter = ArrayAdapter1;

        }
        private void spinner_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            Spinner sp = (Spinner)sender;
           
        }
    }
}