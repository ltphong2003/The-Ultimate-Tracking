using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.Gms.Location;
using Android.Gms.Tasks;
using Android.Locations;
using Android.OS;
using Android.Runtime;
using Android.Support.V4.App;
using Android.Support.V4.Content;
using Android.Util;
using System;
using Java.Lang;
using Xamarin.Essentials;
using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp.Response;
using System.Linq;
using Task = Android.Gms.Tasks.Task;

namespace LocUpdFgService
{
	public class NowLoca
	{
		public string lat { get; set; }
		public string lon { get; set; }
		public string time { get; set; }
		public string direction { get; set; }
		public string speed { get; set; }
		public string vehicle_id { get; set; }
		public string address { get; set; }

	}
	public class PastDetail
	{
		public string number { get; set; }
	}

		/**
		 * A bound and started service that is promoted to a foreground service when location updates have
		 * been requested and all clients unbind.
		 *
		 * For apps running in the background on "O" devices, location is computed only once every 10
		 * minutes and delivered batched every 30 minutes. This restriction applies even to apps
		 * targeting "N" or lower which are run on "O" devices.
		 *
		 * This sample show how to use a long-running service for location updates. When an activity is
		 * bound to this service, frequent location updates are permitted. When the activity is removed
		 * from the foreground, the service promotes itself to a foreground service, and location updates
		 * continue. When the activity comes back to the foreground, the foreground service stops, and the
		 * notification assocaited with that service is removed.
		 */
		[Service(Label = "LocationUpdatesService", Enabled = true, Exported = true)]
	[IntentFilter(new string[] { "com.xamarin.LocUpdFgService.LocationUpdatesService" })]
	public class LocationUpdatesService : Service
	{
		const string LocationPackageName = "com.xamarin.LocUpdFgService";

		public string Tag = "LocationUpdatesService";

		string ChannelId = "channel_01";

        public const string ActionBroadcast = LocationPackageName + ".broadcast";
		public const string ExtraLocation = LocationPackageName + ".location";
		const string ExtraStartedFromNotification = LocationPackageName + ".started_from_notification";
		string vehicle_id1;

		IBinder Binder;

		/**
	     * The desired interval for location updates. Inexact. Updates may be more or less frequent.
	     */
		const long UpdateIntervalInMilliseconds = 5000;

		/**
		 * The fastest rate for active location updates. Updates will never be more frequent
		 * than this value.
		 */
		const long FastestUpdateIntervalInMilliseconds = UpdateIntervalInMilliseconds / 2;

		/**
		 * The identifier for the notification displayed for the foreground service.
		 */
		const int NotificationId = 12345678;

		/**
		 * Used to check whether the bound activity has really gone away and not unbound as part of an
		 * orientation change. We create a foreground service notification only if the former takes
		 * place.
		 */
		bool ChangingConfiguration = false;

		NotificationManager NotificationManager;

		/**
		 * Contains parameters used by {@link com.google.android.gms.location.FusedLocationProviderApi}.
		 */
		LocationRequest LocationRequest;

		/**
		 * Provides access to the Fused Location Provider API.
		 */
		FusedLocationProviderClient FusedLocationClient;

		/**
		 * Callback for changes in location.
		 */
		LocationCallback LocationCallback;

		Handler ServiceHandler;

		/**
		 * The current location.
		 */
		public Android.Locations.Location Location=null;

		public LocationUpdatesService()
		{
			Binder = new LocationUpdatesServiceBinder(this);
		}

		class LocationCallbackImpl : LocationCallback
		{
			public LocationUpdatesService Service { get; set; }
			public override void OnLocationResult(LocationResult result)
			{
				base.OnLocationResult(result);
				Service.OnNewLocation(result.LastLocation);
			}
		}

		public override void OnCreate()
		{
			FusedLocationClient = LocationServices.GetFusedLocationProviderClient(this);

			LocationCallback = new LocationCallbackImpl { Service = this };

			CreateLocationRequest();
            GetLastLocation();

			HandlerThread handlerThread = new HandlerThread(Tag);
			handlerThread.Start();
			ServiceHandler = new Handler(handlerThread.Looper);
			NotificationManager = (NotificationManager) GetSystemService(NotificationService);

		    if (Build.VERSION.SdkInt >= Build.VERSION_CODES.O)
		    {
		        string name = GetString(Resource.String.app_name);
		        NotificationChannel mChannel = new NotificationChannel(ChannelId, name, NotificationManager.ImportanceDefault);
		        NotificationManager.CreateNotificationChannel(mChannel);
		    }
        }
		public bool kt = false;
		public override StartCommandResult OnStartCommand(Intent intent, StartCommandFlags flags, int startId)
		{
			Log.Info(Tag, "Service started");
			var startedFromNotification = intent.GetBooleanExtra(ExtraStartedFromNotification, false);
			// We got here because the user decided to remove location updates from the notification.
			if (startedFromNotification)
			{
				RemoveLocationUpdates();
				StopSelf();
			}
			vehicle_id1 = intent.GetStringExtra("vehicle_id");
			// Tells the system to not try to recreate the service after it has been killed.
			return StartCommandResult.NotSticky;
		}

		public override void OnConfigurationChanged(Android.Content.Res.Configuration newConfig)
		{
			base.OnConfigurationChanged(newConfig);
			ChangingConfiguration = true;
		}

		public override IBinder OnBind(Intent intent)
		{
			// Called when a client (MainActivity in case of this sample) comes to the foreground
			// and binds with this service. The service should cease to be a foreground service
			// when that happens.
			Log.Info(Tag, "in onBind()");
			StopForeground(true);
			ChangingConfiguration = false;
			return Binder;
		}

		public override void OnRebind(Intent intent)
		{
			// Called when a client (MainActivity in case of this sample) returns to the foreground
			// and binds once again with this service. The service should cease to be a foreground
			// service when that happens.
			Log.Info(Tag, "in onRebind()");
			StopForeground(true);
			ChangingConfiguration = false;
			base.OnRebind(intent);
		}

		public override bool OnUnbind(Intent intent)
		{
			Log.Info(Tag, "Last client unbound from service");

			// Called when the last client (MainActivity in case of this sample) unbinds from this
			// service. If this method is called due to a configuration change in MainActivity, we
			// do nothing. Otherwise, we make this service a foreground service.
			if (!ChangingConfiguration && Utils.RequestingLocationUpdates(this))
			{
				Log.Info(Tag, "Starting foreground service");
				/*
				// TODO(developer). If targeting O, use the following code.
				if (Build.VERSION.SDK_INT == Build.VERSION_CODES.O) {
					mNotificationManager.startServiceInForeground(new Intent(this,
							LocationUpdatesService.class), NOTIFICATION_ID, getNotification());
				} else {
					startForeground(NOTIFICATION_ID, getNotification());
				}
				 */
				StartForeground(NotificationId, GetNotification());
			}
			return true; // Ensures onRebind() is called when a client re-binds.
		}

		public override void OnDestroy()
		{
			ServiceHandler.RemoveCallbacksAndMessages(null);
		}

		/**
	     * Makes a request for location updates. Note that in this sample we merely log the
	     * {@link SecurityException}.
	     */
		public void RequestLocationUpdates(string vehicle_id)
		{
			Log.Info(Tag, "Requesting location updates");
			Utils.SetRequestingLocationUpdates(this, true);
			Intent intent = new Intent(ApplicationContext, typeof(LocationUpdatesService));
			intent.PutExtra("vehicle_id", vehicle_id);
			StartService(intent);
	        try {
	            FusedLocationClient.RequestLocationUpdates(LocationRequest, LocationCallback, Looper.MyLooper());
	        } catch (SecurityException unlikely) {
	            Utils.SetRequestingLocationUpdates(this, false);
				Log.Error(Tag, "Lost location permission. Could not request updates. " + unlikely);
			}
		}

		/**
	     * Removes location updates. Note that in this sample we merely log the
	     * {@link SecurityException}.
	     */
		public void RemoveLocationUpdates()
		{
			Log.Info(Tag, "Removing location updates");
			try
			{
				FusedLocationClient.RemoveLocationUpdates(LocationCallback);
				Utils.SetRequestingLocationUpdates(this, false);
				StopSelf();
			}
			catch (SecurityException unlikely)
			{
				Utils.SetRequestingLocationUpdates(this, true);
				Log.Error(Tag, "Lost location permission. Could not remove updates. " + unlikely);
			}
		}

        /**
	     * Returns the {@link NotificationCompat} used as part of the foreground service.
	     */
        Notification GetNotification()
        {
            Intent intent = new Intent(this, typeof(LocationUpdatesService));

            var text = Utils.GetLocationText(Location);

            // Extra to help us figure out if we arrived in onStartCommand via the notification or not.
            intent.PutExtra(ExtraStartedFromNotification, true);

            // The PendingIntent that leads to a call to onStartCommand() in this service.
            var servicePendingIntent = PendingIntent.GetService(this, 0, intent, PendingIntentFlags.UpdateCurrent);

            // The PendingIntent to launch activity.
            var activityPendingIntent = PendingIntent.GetActivity(this, 0, new Intent(this, typeof(MainActivity)), 0);

            NotificationCompat.Builder builder = new NotificationCompat.Builder(this)
                .AddAction(Resource.Drawable.ic_launch, GetString(Resource.String.launch_activity),
                    activityPendingIntent)
                .AddAction(Resource.Drawable.ic_cancel, GetString(Resource.String.remove_location_updates),
                    servicePendingIntent)
                .SetContentText(text)
                .SetContentTitle(Utils.GetLocationTitle(this))
                .SetOngoing(true)
                .SetPriority((int) NotificationPriority.High)
                .SetSmallIcon(Resource.Drawable.GPS)
                .SetTicker(text)
                .SetWhen(JavaSystem.CurrentTimeMillis());

            if (Build.VERSION.SdkInt>= Build.VERSION_CODES.O)
            {
                builder.SetChannelId(ChannelId);
            }

            return builder.Build();
        }

        private void GetLastLocation()
        {
            try
            {
                FusedLocationClient.LastLocation.AddOnCompleteListener(new GetLastLocationOnCompleteListener { Service = this });
            }
            catch (SecurityException unlikely)
            {
                Log.Error(Tag, "Lost location permission." + unlikely);
            }
        }

        public async void OnNewLocation(Android.Locations.Location location)
		{
			Log.Info(Tag, "New location: " + location);
			Android.Locations.Location locat = Location;
			float[] result = new float[1];
			if (kt)
			Android.Locations.Location.DistanceBetween(locat.Latitude, locat.Longitude, location.Latitude, location.Longitude, result);
			if (((!kt)||(result[0]>=2))&&(location.Speed>0))
			{
				Location = location;
				kt = true;
				// Notify anyone listening for broadcasts about the new location.
				Intent intent = new Intent(ActionBroadcast);
				intent.PutExtra(ExtraLocation, location);
				LocalBroadcastManager.GetInstance(ApplicationContext).SendBroadcast(intent);
				string lat1 = location.Latitude.ToString();
				string lng = location.Longitude.ToString();
				string spe = location.Speed.ToString();
				string time1 = DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss");
				string address1 = "";
				try
				{

					var placemarks = await Geocoding.GetPlacemarksAsync(location.Latitude,location.Longitude);

					var placemark = placemarks.FirstOrDefault();
					if (placemark != null)
					{
						var geocodeAddress =
							$"AdminArea:       {placemark.AdminArea}\n" +
							$"CountryCode:     {placemark.CountryCode}\n" +
							$"CountryName:     {placemark.CountryName}\n" +
							$"FeatureName:     {placemark.FeatureName}\n" +
							$"Locality:        {placemark.Locality}\n" +
							$"PostalCode:      {placemark.PostalCode}\n" +
							$"SubAdminArea:    {placemark.SubAdminArea}\n" +
							$"SubLocality:     {placemark.SubLocality}\n" +
							$"SubThoroughfare: {placemark.SubThoroughfare}\n" +
							$"Thoroughfare:    {placemark.Thoroughfare}\n";
						address1 = placemark.SubThoroughfare+" "+placemark.Thoroughfare+","+'\n'+placemark.SubAdminArea+", "+placemark.AdminArea+", "+placemark.CountryName;
					}
				}
				catch (FeatureNotSupportedException fnsEx)
				{
					// Feature not supported on device
				}
				catch (System.Exception  ex)
				{
					// Handle exception that may have occurred in geocoding
				}

				var data = new NowLoca
				{
					address = address1,
					lat = lat1,
					lon = lng,
					direction = location.Bearing.ToString(),
					time = time1,
					speed = spe,
					vehicle_id = vehicle_id1,
				};
				Updata(data);
				Downdata(data);

				// Update notification content if running as a foreground service.
				if (ServiceIsRunningInForeground(this))
				{
					NotificationManager.Notify(NotificationId, GetNotification());
				}
			}
		}

		public async void Updata(NowLoca n)
		{
			try
			{
				var current = Connectivity.NetworkAccess;
				if (current == Xamarin.Essentials.NetworkAccess.Internet)
				{
					// Connection to internet is available
					IFirebaseConfig config = new FirebaseConfig
					{
						AuthSecret = "cOsfW43OgJtNzF6tTrSPO6jqkTj65ajlxpPfIWTw",
						BasePath = "https://the-ultimate-tracking.firebaseio.com/"
					};
					IFirebaseClient client = new FireSharp.FirebaseClient(config);
					FirebaseResponse update = await client.UpdateAsync("now/" + vehicle_id1, n);
				}
			}
			catch
			{

			}
		}

		public async void Downdata(NowLoca n)
		{
			try
			{
				var current = Connectivity.NetworkAccess;
				if (current == Xamarin.Essentials.NetworkAccess.Internet)
				{
					IFirebaseConfig config = new FirebaseConfig
					{
						AuthSecret = "cOsfW43OgJtNzF6tTrSPO6jqkTj65ajlxpPfIWTw",
						BasePath = "https://the-ultimate-tracking.firebaseio.com/"
					};
					IFirebaseClient client = new FireSharp.FirebaseClient(config);
					string date = DateTime.Now.ToString("dd-MM-yyyy");
					var detail1 = new PastDetail
					{
						number = "1"
					};
					var detail2 = new PastDetail
					{
						number = "1"
					};
					int past_number = 1;
					try
					{
						FirebaseResponse response = await client.GetAsync("past/" + vehicle_id1 + "/" + date + "/detail");
						detail1 = response.ResultAs<PastDetail>();
						past_number = Convert.ToInt32(detail1.number);
						past_number++;
						detail1.number = past_number.ToString();
						FirebaseResponse PastNumber = await client.UpdateAsync("past/" + vehicle_id1 + "/" + date + "/detail", detail1);
					}
					catch
					{
						FirebaseResponse PastNumber = await client.UpdateAsync("past/" + vehicle_id1 + "/" + date + "/detail", detail2);
					}
					FirebaseResponse insert = await client.SetAsync("past/" + vehicle_id1 + "/" + date + "/" + past_number, n);
				}
			}
			catch
			{

			}
		}
		/**
	     * Sets the location request parameters.
	     */
		void CreateLocationRequest()
		{
			LocationRequest = new LocationRequest();
			LocationRequest.SetInterval(UpdateIntervalInMilliseconds);
			LocationRequest.SetFastestInterval(FastestUpdateIntervalInMilliseconds);
			LocationRequest.SetPriority(LocationRequest.PriorityHighAccuracy);
		}

		/**
	     * Returns true if this is a foreground service.
	     *
	     * @param context The {@link Context}.
	     */
		public bool ServiceIsRunningInForeground(Context context)
		{
			var manager = (ActivityManager) context.GetSystemService(ActivityService);
			foreach (var service in manager.GetRunningServices(Integer.MaxValue))
			{
				if (Class.Name.Equals(service.Service.ClassName))
				{
					if (service.Foreground)
					{
						return true;
					}
				}
			}
			return false;
		}
	}

	/**
	 * Class used for the client Binder.  Since this service runs in the same process as its
	 * clients, we don't need to deal with IPC.
	 */
	public class LocationUpdatesServiceBinder : Binder
	{
		readonly LocationUpdatesService service;

		public LocationUpdatesServiceBinder(LocationUpdatesService service)
		{
			this.service = service;
		}

		public LocationUpdatesService GetLocationUpdatesService()
		{
			return service;
		}
	}

    public class GetLastLocationOnCompleteListener : Java.Lang.Object, IOnCompleteListener
    {
        public LocationUpdatesService Service { get; set; }

        public void OnComplete(Task task)
        {
            if (task.IsSuccessful && task.Result != null)
            {
                Service.Location = (Android.Locations.Location)task.Result;
            }
            else
            {
                Log.Warn(Service.Tag, "Failed to get location.");
            }
        }
    }
}
