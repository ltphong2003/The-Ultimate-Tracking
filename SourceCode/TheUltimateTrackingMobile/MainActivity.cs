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
using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp.Response;
using Android.Renderscripts;

namespace TheUltimateTrackingMobile
{
   
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", Icon = "@mipmap/TUTLogo",MainLauncher = true)]
    public class MainActivity : AppCompatActivity, IOnMapReadyCallback, Android.Gms.Maps.GoogleMap.IInfoWindowAdapter, Android.Gms.Maps.GoogleMap.IOnInfoWindowClickListener
    {
        //Map
        GoogleMap mainMap;
        public static int user_id = 0;
        public static List<int> vehiclenum;
        //PermissionsRequest
        const int RequestID = 0;
        readonly string[] permissionsGroup =
        {
            Manifest.Permission.AccessCoarseLocation,
            Manifest.Permission.AccessFineLocation,
        };

        //Firebase
        VehicleListeners vehicleListeners;
        List<VehicleId> vehicleidList;

        LocationListeners locationListeners;
        List<Location> locationList;

        List<List<LatLng>> past = new List<List<LatLng>>();

        //Controls
        Button zoomin;
        Button zoomout;
        Spinner vehicleFind;
     
        public class ConnectDatabase
        {
            public static IFirebaseConfig config = new FirebaseConfig
            {
                AuthSecret = "cOsfW43OgJtNzF6tTrSPO6jqkTj65ajlxpPfIWTw",
                BasePath = "https://the-ultimate-tracking.firebaseio.com/"
            };
            public static IFirebaseClient client = new FireSharp.FirebaseClient(config);
        }
        public class User
        {
            public string email { get; set; }
            public string password { get; set; }
        }
        public class PastValueVehi
        {
            public int number { get; set; }
        }
        private async void Startlocat_mOnStartComplete(object sender, OnStartLocationEventArgs e)
        {
            string user = e.Username;
            string pass = e.Password;
   
            FirebaseResponse response_vdetail = await ConnectDatabase.client.GetAsync("user/detail");
            PastValueVehi detail = response_vdetail.ResultAs<PastValueVehi>();
            int vehi_number = Convert.ToInt32(detail.number);
            for (int i = 1; i <= vehi_number; i++)
            {
                FirebaseResponse response_vehi = await ConnectDatabase.client.GetAsync("user/" + i.ToString());
                User vehi_data = response_vehi.ResultAs<User>();
                if ((vehi_data.email == user) && (vehi_data.password == pass))
                {
                    user_id = i;
                    ConnectControls();
                    RetrieveVehicle();
                    //ConnectControls();
                    break;          
                }
            }
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);

            SupportMapFragment mapFragment = (SupportMapFragment)SupportFragmentManager.FindFragmentById(Resource.Id.map);
            mapFragment.GetMapAsync(this);
            CheckSpecialPermission();
            Android.App.FragmentTransaction trans = FragmentManager.BeginTransaction();
            startlocation startlocat = new startlocation();
            startlocat.Show(trans, "Start");
            startlocat.mOnStartComplete += Startlocat_mOnStartComplete;
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        public void OnMapReady(GoogleMap googleMap)
        {
            mainMap = googleMap;
        }

        bool CheckSpecialPermission()
        {
            bool permissionGranted = false;
            if (ActivityCompat.CheckSelfPermission(this, Manifest.Permission.AccessFineLocation) != Android.Content.PM.Permission.Granted &&
                ActivityCompat.CheckSelfPermission(this, Manifest.Permission.AccessCoarseLocation) != Android.Content.PM.Permission.Granted)
            {
                RequestPermissions(permissionsGroup, RequestID);
            }
            else
            {
                permissionGranted = true;
            }
            return permissionGranted;
        }

        public void RetrieveVehicle()
        {
            vehicleListeners = new VehicleListeners();
            vehicleListeners.Create();
            vehicleListeners.vehicleDataRetrieved += VehicleListeners_vehicleDataRetrieved;

        }

        void SetVehicleNum()
        {
            List<int> vehiclenumber = new List<int>();
            for (int i = 0; i < vehicleidList.Count; i++)
            {
                int num = Convert.ToInt32(vehicleidList[i].vehicle_id);
                vehiclenumber.Add(num);
            }
            vehiclenum = vehiclenumber;
            RetrieveLocation();
        }

        private void VehicleListeners_vehicleDataRetrieved(object sender, VehicleListeners.vehicleDataEventArgs e)
        {
            
            vehicleidList = e.VehicleId;
            List<string> dataList = new List<string>();
            for (int i = 0; i < vehicleidList.Count; i++)
            {
                dataList.Add(vehicleidList[i].name);
            }
            var ArrayAdapter1 = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleSpinnerItem, dataList);
            vehicleFind.Adapter = ArrayAdapter1;
            SetVehicleNum();
            
            //vehicledetailList = e.VehicleDetail;
        }

        public void RetrieveLocation()
        {
            locationListeners = new LocationListeners();
            locationListeners.Create();
            locationListeners.locationDataRetrieved += LocationListeners_locationDataRetrieved;
        }

        private void LocationListeners_locationDataRetrieved(object sender, LocationListeners.locationDataEventArgs e)
        {
            locationList = e.Location;
            SetLocation();
            vehicleFind.ItemSelected += new EventHandler<AdapterView.ItemSelectedEventArgs>(spinner_ItemSelected);
        }

        private void SetLocation()
        { 
            mainMap.Clear();
            List<LatLng> positions = new List<LatLng>();
            for (int i = 0; i < locationList.Count; i++)
            {
                string markernum = i.ToString();
                LatLng myposition = new LatLng(Convert.ToDouble(locationList[i].lat), Convert.ToDouble(locationList[i].lon));
                positions.Add(myposition);
                mainMap.MoveCamera(CameraUpdateFactory.NewLatLngZoom(myposition, 15));
                MarkerOptions options = new MarkerOptions()
                    .SetPosition(myposition)
                    .SetTitle(markernum);

                mainMap.AddMarker(options);

                mainMap.SetInfoWindowAdapter(this);
                mainMap.SetOnInfoWindowClickListener(this);
            }
            past.Add(positions);
            if (past.Count > 1)
            {
                for (int i = 0; i < past.Count - 1; i++)
                    for (int j = 0; j < locationList.Count; j++)
                    {
                        PolylineOptions line = new PolylineOptions();
                        double rlat1 = Math.PI * past[i][j].Latitude / 180;
                        double rlat2 = Math.PI * past[i + 1][j].Latitude / 180;
                        double theta = past[i][j].Longitude - past[i + 1][j].Longitude;
                        double rtheta = Math.PI * theta / 180;
                        double dist = Math.Sin(rlat1) * Math.Sin(rlat2) + Math.Cos(rlat1) * Math.Cos(rlat2) * Math.Cos(rtheta);
                        dist = Math.Acos(dist);
                        dist = dist * 180 / Math.PI;
                        dist = dist * 60 * 1.1515;
                        dist = dist * 1.609344 / 1000;
                        if (dist <= 2)
                        {
                            line.Add(past[i][j]);
                            line.Add(past[i + 1][j]);

                            mainMap.AddPolyline(line);
                        }

                    }
            }
        }

        public View GetInfoContents(Marker marker)
        {
            return null;
        }

        public View GetInfoWindow(Marker marker)
        {
            View view = LayoutInflater.Inflate(Resource.Layout.markerinfo, null, false);

            string markernum = marker.Title;
            int number = Convert.ToInt32(markernum);
            for (int i = 0; i < vehicleidList.Count; i++)
            {
                if (locationList[number].vehicle_id == vehicleidList[i].vehicle_id)
                {
                    string vehicleText = "Vehicle: " + vehicleidList[i].name;
                    string licenseText = "License: " + vehicleidList[i].lpn;
                    string addressText = "Address: " + locationList[number].address;
                    string timeText = "Last update: " + locationList[number].time;


                    view.FindViewById<TextView>(Resource.Id.txtVehicle).Text = vehicleText;
                    view.FindViewById<TextView>(Resource.Id.txtLicense).Text = licenseText;
                    view.FindViewById<TextView>(Resource.Id.txtAddress).Text = addressText;
                    view.FindViewById<TextView>(Resource.Id.txtTime).Text = timeText;

                    LatLng myposition = new LatLng(Convert.ToDouble(locationList[number].lat), Convert.ToDouble(locationList[number].lon));
                    mainMap.MoveCamera(CameraUpdateFactory.NewLatLngZoom(myposition, 18));
                }
            }
            

            return view;
        }

        public void ConnectControls()
        {
            zoomin = (Button)FindViewById(Resource.Id.zoominBtn);
            zoomout = (Button)FindViewById(Resource.Id.zoomoutBtn);
            vehicleFind = (Spinner)FindViewById(Resource.Id.vehicleSpinner);

            zoomin.Click += Zoomin_Click;
            zoomout.Click += Zoomout_Click;
        }

        private void spinner_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            Spinner sp = (Spinner)sender;
            int loca = e.Position;
            LatLng myposition = new LatLng(Convert.ToDouble(locationList[loca].lat), Convert.ToDouble(locationList[loca].lon));
            mainMap.MoveCamera(CameraUpdateFactory.NewLatLngZoom(myposition, 16));
        }

        private void Zoomout_Click(object sender, EventArgs e)
        {
            mainMap.MoveCamera(CameraUpdateFactory.ZoomOut());
        }

        private void Zoomin_Click(object sender, EventArgs e)
        {
            mainMap.MoveCamera(CameraUpdateFactory.ZoomIn());
        }

        public void OnInfoWindowClick(Marker marker)
        {
            
        }
    }
} 