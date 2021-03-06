﻿using Android.App;
using Android.Widget;
using Android.OS;
using Android.Content;
using Java.Lang;
using Android.Preferences;
using Android.Support.V4.Content;
using Android.Support.V4.App;
using Android;
using Android.Util;
using Android.Support.Design.Widget;
using Android.Net;
using Android.Locations;
using Android.Support.V7.App;
using Android.Telephony;
using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp.Response;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Firebase.Database.Query;
using Firebase.Database;
using System.Threading.Tasks;

namespace LocUpdFgService
{
	/**
	 * The only activity in this sample.
	 *
	 * Note: for apps running in the background on "O" devices (regardless of the targetSdkVersion),
	 * location may be computed less frequently than requested when the app is not in the foreground.
	 * Apps that use a foreground service -  which involves displaying a non-dismissable
	 * notification -  can bypass the background location limits and request location updates as before.
	 *
	 * This sample uses a long-running bound and started service for location updates. The service is
	 * aware of foreground status of this activity, which is the only bound client in
	 * this sample. After requesting location updates, when the activity ceases to be in the foreground,
	 * the service promotes itself to a foreground service and continues receiving location updates.
	 * When the activity comes back to the foreground, the foreground service stops, and the
	 * notification associated with that foreground service is removed.
	 *
	 * While the foreground service notification is displayed, the user has the option to launch the
	 * activity from the notification. The user can also remove location updates directly from the
	 * notification. This dismisses the notification and stops the service.
	 */
	[Activity(Label = "The Ultimate Tracking", Icon = "@mipmap/GPS", MainLauncher = true)]
	public class MainActivity : AppCompatActivity, ISharedPreferencesOnSharedPreferenceChangeListener
	{
		const string Tag = "MainActivity";

		// Used in checking for runtime permissions.
		const int RequestPermissionsRequestCode = 34;

		// The BroadcastReceiver used to listen from broadcasts from the service.
		MyReceiver myReceiver;

		// A reference to the service used to get location updates.
		LocationUpdatesService Service;

		// Tracks the bound state of the service.
		bool Bound;

		// UI elements.
		Button RequestLocationUpdatesButton;
		Button RemoveLocationUpdatesButton;
		Button RegisterVehicleButton;

		// Monitors the state of the connection to the service.
		CustomServiceConnection ServiceConnection;
		class CustomServiceConnection : Object, IServiceConnection
		{
			public MainActivity Activity { get; set; }
			public void OnServiceConnected(ComponentName name, IBinder service)
			{
				LocationUpdatesServiceBinder binder = (LocationUpdatesServiceBinder)service;
				Activity.Service = binder.GetLocationUpdatesService();
				Activity.Bound = true;
			}

			public void OnServiceDisconnected(ComponentName name)
			{
				Activity.Service = null;
				Activity.Bound = false;
			}
		}

		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);

			myReceiver = new MyReceiver { Context = this };
			ServiceConnection = new CustomServiceConnection { Activity = this };

			SetContentView(Resource.Layout.activity_main);

			// Check that the user hasn't revoked permissions by going to Settings.
			if (Utils.RequestingLocationUpdates(this))
			{
				if (!CheckPermissions())
				{
					RequestPermissions();
				}
			}
		}
		public class VehicleAcc
		{
			public string imei { get; set; }
			public string lpn { get; set; }
			public string name { get; set; }
			public string user_id { get; set; }
			public string vehicle_id { get; set; }
		}
		FirebaseClient firebase = new FirebaseClient("https://the-ultimate-tracking.firebaseio.com/");
		public class PastValueVehi
		{
			public int number { get; set; }
		}
		public class User
		{
			public string email { get; set; }
			public string password { get; set; }
			public string user_id { get; set; }
		}
		public class ConnectDatabase
		{
			public static IFirebaseConfig config = new FirebaseConfig
			{
				AuthSecret = "cOsfW43OgJtNzF6tTrSPO6jqkTj65ajlxpPfIWTw",
				BasePath = "https://the-ultimate-tracking.firebaseio.com/"
			};
			public static IFirebaseClient client = new FireSharp.FirebaseClient(config);
		}
		public async Task<List<User>> GetAllUsers()
		{

			return (await firebase
			  .Child("user")
			  .OnceAsync<User>()).Select(item => new User
			  {
				  email = item.Object.email,
				  password = item.Object.password,
				  user_id = item.Object.user_id
			  }).ToList();
		}
		public async Task<User> GetPerson(string email)
		{
			var allPersons = await GetAllUsers();
			await firebase
			  .Child("user")
			  .OnceAsync<User>();
			return allPersons.Where(a => a.email == email).FirstOrDefault();
		}
		public async Task<List<VehicleAcc>> GetAllVehicles()
		{

			return (await firebase
			  .Child("vehicle")
			  .OnceAsync<VehicleAcc>()).Select(item => new VehicleAcc
			  {
				  imei = item.Object.imei,
				  lpn = item.Object.lpn,
				  name = item.Object.name,
				  user_id = item.Object.user_id,
				  vehicle_id = item.Object.vehicle_id
			  }).ToList();
		}
		public async Task<VehicleAcc> GetVehicle(string imei)
		{
			var allPersons = await GetAllVehicles();
			await firebase
			  .Child("vehicle")
			  .OnceAsync<VehicleAcc>();
			return allPersons.Where(a => a.imei == imei).FirstOrDefault();
		}
		private async void Startlocat_mOnStartComplete(object sender, OnStartLocationEventArgs e)
		{
			string user = e.Username;
			string pass = e.Password;
			TelephonyManager mTelephonyMgr;
			//Telephone Number  
			mTelephonyMgr = (TelephonyManager)Android.App.Application.Context.GetSystemService(Android.Content.Context.TelephonyService);
			//IMEI number  
			string  m_deviceId = mTelephonyMgr.DeviceId;
			var user1 = await GetPerson(user);
			if (user1 != null)
			{
				if (user1.password == pass)
				{
					var vehicle = await GetVehicle(m_deviceId);
					if (vehicle != null)
					{
						Service.RequestLocationUpdates(vehicle.vehicle_id);
					}

				}
			}
		}

		protected override void OnStart()
		{
			base.OnStart();

			PreferenceManager.GetDefaultSharedPreferences(this).RegisterOnSharedPreferenceChangeListener(this);

			RequestLocationUpdatesButton = FindViewById(Resource.Id.request_location_updates_button) as Button;
			RemoveLocationUpdatesButton = FindViewById(Resource.Id.remove_location_updates_button) as Button;
			RegisterVehicleButton = FindViewById(Resource.Id.vehicle_registration_button) as Button;
			RequestLocationUpdatesButton.Click += (sender, e) => {
				if (!CheckPermissions())
				{
					RequestPermissions();
				}
				else
				{
					Android.App.FragmentTransaction trans = FragmentManager.BeginTransaction();
					startlocation startlocat = new startlocation();
					startlocat.Show(trans, "Start");
					startlocat.mOnStartComplete += Startlocat_mOnStartComplete;
					
				}
			};

			RemoveLocationUpdatesButton.Click += (sender, e) => {
				Service.RemoveLocationUpdates();
			};
			RegisterVehicleButton.Click += (sender, e) => {
				Android.App.FragmentTransaction trans = FragmentManager.BeginTransaction();
				regxe reg = new regxe();
				reg.Show(trans, "Đăng ký xe");
			};

			// Restore the state of the buttons when the activity (re)launches.
			SetButtonsState(Utils.RequestingLocationUpdates(this));

			// Bind to the service. If the service is in foreground mode, this signals to the service
			// that since this activity is in the foreground, the service can exit foreground mode.
			BindService(new Intent(this, typeof(LocationUpdatesService)), ServiceConnection, Bind.AutoCreate);
		}

		protected override void OnResume()
		{
			base.OnResume();
			LocalBroadcastManager.GetInstance(this).RegisterReceiver(myReceiver,
				new IntentFilter(LocationUpdatesService.ActionBroadcast));
		}

		protected override void OnPause()
		{
			LocalBroadcastManager.GetInstance(this).UnregisterReceiver(myReceiver);
			base.OnPause();
		}

		protected override void OnStop()
		{
			if (Bound)
			{
				// Unbind from the service. This signals to the service that this activity is no longer
				// in the foreground, and the service can respond by promoting itself to a foreground
				// service.
				UnbindService(ServiceConnection);
				Bound = false;
			}
			PreferenceManager.GetDefaultSharedPreferences(this)
					.UnregisterOnSharedPreferenceChangeListener(this);
			base.OnStop();
		}

		/**
	     * Returns the current state of the permissions needed.
	     */
		bool CheckPermissions()
		{
			return PermissionChecker.PermissionGranted == ContextCompat.CheckSelfPermission(this,
				Manifest.Permission.AccessFineLocation);
		}

		void RequestPermissions()
		{
			var shouldProvideRationale = ActivityCompat.ShouldShowRequestPermissionRationale(this,
				Manifest.Permission.AccessFineLocation);

			// Provide an additional rationale to the user. This would happen if the user denied the
			// request previously, but didn't check the "Don't ask again" checkbox.
			if (shouldProvideRationale)
			{
				Log.Info(Tag, "Displaying permission rationale to provide additional context.");
				Snackbar.Make(
						FindViewById(Resource.Id.activity_main),
						Resource.String.permission_rationale,
						Snackbar.LengthIndefinite)
						.SetAction(Resource.String.ok, (obj) => {
							// Request permission
							ActivityCompat.RequestPermissions(this,
									new string[] { Manifest.Permission.AccessFineLocation },
									RequestPermissionsRequestCode);
						})
						.Show();
			}
			else
			{
				Log.Info(Tag, "Requesting permission");
				// Request permission. It's possible this can be auto answered if device policy
				// sets the permission in a given state or the user denied the permission
				// previously and checked "Never ask again".
				ActivityCompat.RequestPermissions(this,
						new string[] { Manifest.Permission.AccessFineLocation },
						RequestPermissionsRequestCode);
			}
		}

		public override void OnRequestPermissionsResult(int requestCode, string[] permissions,
						 Android.Content.PM.Permission[] grantResults)
		{
			Log.Info(Tag, "onRequestPermissionResult");
			if (requestCode == RequestPermissionsRequestCode)
			{
				if (grantResults.Length <= 0)
				{
					// If user interaction was interrupted, the permission request is cancelled and you
					// receive empty arrays.
					Log.Info(Tag, "User interaction was cancelled.");
				}
				else if (grantResults[0] == PermissionChecker.PermissionGranted)
				{
					// Permission was granted.
					Service.RequestLocationUpdates("1");
				}
				else
				{
					// Permission denied.
					SetButtonsState(false);
					Snackbar.Make(
							FindViewById(Resource.Id.activity_main),
							Resource.String.permission_denied_explanation,
							Snackbar.LengthIndefinite)
							.SetAction(Resource.String.settings, (obj) => {
								// Build intent that displays the App settings screen.
								Intent intent = new Intent();
								intent.SetAction(Android.Provider.Settings.ActionApplicationDetailsSettings);
								var uri = Uri.FromParts("package", PackageName, null);
								intent.SetData(uri);
								intent.SetFlags(ActivityFlags.NewTask);
								StartActivity(intent);
							})
							.Show();
				}
			}
		}

		/**
	     * Receiver for broadcasts sent by {@link LocationUpdatesService}.
	     */
		class MyReceiver : BroadcastReceiver
		{
			public Context Context { get; set; }
			public override void OnReceive(Context context, Intent intent)
			{
				var location = intent.GetParcelableExtra(LocationUpdatesService.ExtraLocation) as Location;
				if (location != null)
				{
					Toast.MakeText(Context, Utils.GetLocationText(location), ToastLength.Short).Show();
				}

			}
		}

		public void OnSharedPreferenceChanged(ISharedPreferences sharedPreferences, string key)
		{
			// Update the buttons state depending on whether location updates are being requested.
			if (key.Equals(Utils.KeyRequestingLocationUpdates))
			{
				SetButtonsState(sharedPreferences.GetBoolean(Utils.KeyRequestingLocationUpdates, false));
			}
		}

		void SetButtonsState(bool requestingLocationUpdates)
		{
			if (requestingLocationUpdates)
			{
				RequestLocationUpdatesButton.Enabled = false;
				RemoveLocationUpdatesButton.Enabled = true;
			}
			else
			{
				RequestLocationUpdatesButton.Enabled = true;
				RemoveLocationUpdatesButton.Enabled = false;
			}
		}
	}
}

