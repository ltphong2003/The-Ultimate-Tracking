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
using Firebase;
using Firebase.Database;

namespace TheUltimateTrackingMobile.Helpers
{
    public static class AppDataHelpers
    {
        public static FirebaseDatabase GetDatabase()
        {
            var app = FirebaseApp.InitializeApp(Application.Context);
            FirebaseDatabase database;

            if (app == null)
            {
                var option = new FirebaseOptions.Builder()
                    .SetApplicationId("the-ultimate-tracking")
                    .SetApiKey("AIzaSyDBRyDScgrGSfPkgytaS-M5OuLIn06M2e4")
                    .SetDatabaseUrl("https://the-ultimate-tracking.firebaseio.com")
                    .SetStorageBucket("the-ultimate-tracking.appspot.com")
                    .Build();

                app = FirebaseApp.InitializeApp(Application.Context, option);
                database = FirebaseDatabase.GetInstance(app);
            }
            else
            {
                database = FirebaseDatabase.GetInstance(app);
            }    
            return database;
        }
    }
}