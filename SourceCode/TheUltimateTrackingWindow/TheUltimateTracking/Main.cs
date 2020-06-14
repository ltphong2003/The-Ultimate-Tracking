using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp.Response;
using Database;
using GMap.NET.MapProviders;
using GMap.NET.WindowsForms;
using GMap.NET;
using Demo.WindowsForms.CustomMarkers;
using System.Net.Http;
using Firebase.Database;
using Firebase.Database.Query;


namespace TheUltimateTracking
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
        }
        public static int user_id = 0 ;
        public static VehicleAcc[] vacc = new VehicleAcc[100000];
        public int vehicle_num = 0;
        public GMapOverlay markers = new GMapOverlay("markers");
        public GMapOverlay markers_tracking = new GMapOverlay("markers");
        public GMapOverlay markers_tracking_show = new GMapOverlay("markers");
        public GMapOverlay routes = new GMapOverlay("routes");
        bool kq = false;
        private void tabControl1_DrawItem(Object sender, System.Windows.Forms.DrawItemEventArgs e)
        {
            Graphics g = e.Graphics;
            Brush _textBrush;

            // Get the item from the collection.
            TabPage _tabPage = tabControl1.TabPages[e.Index];

            // Get the real bounds for the tab rectangle.
            Rectangle _tabBounds = tabControl1.GetTabRect(e.Index);

            if (e.State == DrawItemState.Selected)
            {

                // Draw a different background color, and don't paint a focus rectangle.
                _textBrush = new SolidBrush(Color.White);
                g.FillRectangle(Brushes.DarkGray, e.Bounds);
            }
            else
            {
                _textBrush = new System.Drawing.SolidBrush(e.ForeColor);
                e.DrawBackground();
            }

            // Use our own font.
            Font _tabFont = new Font("Californian FB", 20.0f, FontStyle.Bold, GraphicsUnit.Pixel);

            // Draw string. Center the text.
            StringFormat _stringFlags = new StringFormat();
            _stringFlags.Alignment = StringAlignment.Center;
            _stringFlags.LineAlignment = StringAlignment.Center;
            g.DrawString(_tabPage.Text, _tabFont, _textBrush, _tabBounds, new StringFormat(_stringFlags));
        }

        class pastlv
        {
            string time;
            string address;
            string speed;
            float lat;
            float lon;
            float direction;
            public pastlv(string time,string address,string speed,float lat, float lon,float direction)
            {
                this.time = time;
                this.address = address;
                this.speed = speed;
                this.lat = lat;
                this.lon = lon;
                this.direction = direction;
            }
            public string Time
            {
                get { return time; }
                set { time = value; }
            }
            public string Speed
            {
                get { return speed; }
                set { speed = value; }
            }

            public string Address
            {
                get { return address; }
                set { address = value; }
            }

            public float Lat
            {
                get { return lat; }
                set {lat = value; }
            }
            public float Lon
            {
                get { return lon; }
                set { lon = value; }
            }

            public float Direction
            {
                get { return direction; }
                set { direction = value; }
            }


            static internal List<pastlv> GET()
            {
                List<pastlv> x = new List<pastlv>();
                return x;
            }
        }


        private void Form1_Load(object sender, EventArgs e)
        {
                if (user_id == 0)
            {
                Login form = new Login();
                form.Location = this.Location;
                form.StartPosition = FormStartPosition.Manual;
                form.ShowDialog();
            }
            if (user_id == 0)
            {
                this.Close();
            }
            gmap.MapProvider = GMap.NET.MapProviders.GoogleMapProvider.Instance;
            GMap.NET.GMaps.Instance.Mode = GMap.NET.AccessMode.ServerOnly;
            gmap.ShowCenter = false;
            gmap.Zoom = 15;
            GMapTracking.MapProvider = GMap.NET.MapProviders.GoogleMapProvider.Instance;
            GMapTracking.ShowCenter = false;
            GMapTracking.Zoom = 15;
            SearchVehicle();
      
        }
        public async Task<List<VehicleAcc>> GetAllVehicles()
        {

            return (await firebase
              .Child("vehicle")
              .OnceAsync<VehicleAcc>()).Select(item => new VehicleAcc
              {
                 imei=item.Object.imei,
                 lpn=item.Object.lpn,
                 name=item.Object.name,
                 user_id=item.Object.user_id,
                 vehicle_id=item.Object.vehicle_id
              }).ToList();
        }

        private async void SearchVehicle()
        {
            try
            {
                vehicle_num = 0;
                cbbcar.Items.Clear();
                cbbtracking.Items.Clear();
                cbbvehicle_setting.Items.Clear();
                var vehicles = await GetAllVehicles();
                for (int i=0;i<vehicles.Count-1;i++)
                {
                    VehicleAcc vehicle_data = vehicles[i];
                    if (vehicle_data.user_id == user_id.ToString())
                    {
                        vehicle_num++;
                        vacc[vehicle_num] = vehicle_data;
                        cbbcar.Items.Add(vehicle_data.name + "-" + vehicle_data.lpn);
                        cbbtracking.Items.Add(vehicle_data.name + "-" + vehicle_data.lpn);
                        cbbvehicle_setting.Items.Add(vehicle_data.name + "-" + vehicle_data.lpn);
                    }
                }
                if (!kq)
                {
                    updatelocation();
                    timernow.Start();
                }

            }
            catch
            {

            }
         
        }

        private async void updatelocation()
        {
            try
            {
                if (!kq)
                {
                    for (int i = 1; i <= vehicle_num; i++)
                    {
                        VehicleAcc a = vacc[i];
                        FirebaseResponse response = await ConnectDatabase.client.GetAsync("now/" + a.vehicle_id);
                        VehicleNow vnow = response.ResultAs<VehicleNow>();
                        GMarkerArrow marker1 = new GMarkerArrow(new PointLatLng(vnow.lat, vnow.lon));
                        string speed1 = Convert.ToInt32(vnow.speed).ToString();
                        marker1.ToolTipText = "Information:" + '\n' + '\n' + "Name: " + a.name + '\n' + "License: " + a.lpn + '\n' + "Address: " + vnow.address + '\n' + "Speed: " + speed1 + " m/s" + '\n' + "Last update: " + vnow.time;
                        marker1.ToolTip.Fill = Brushes.Maroon;
                        marker1.ToolTip.Foreground = Brushes.White;
                        marker1.ToolTip.Stroke = Pens.White;
                        marker1.ToolTip.Format.Alignment = StringAlignment.Near;
                        marker1.Bearing = vnow.direction; // Rotation angle
                        marker1.Fill = new SolidBrush(Color.FromArgb(155, Color.Maroon)); // Arrow color
                        markers.Markers.Add(marker1);
                    }
                    gmap.Overlays.Add(markers);
                    kq = true;
                }

                else
                {
                    for (int i = 1; i <= vehicle_num; i++)
                    {
                        VehicleAcc a = vacc[i];
                        FirebaseResponse response = await ConnectDatabase.client.GetAsync("now/" + a.vehicle_id);
                        VehicleNow vnow = response.ResultAs<VehicleNow>();
                        GMarkerArrow marker1 = new GMarkerArrow(new PointLatLng(vnow.lat, vnow.lon));
                        string speed1 = Convert.ToInt32(vnow.speed).ToString();
                        marker1.ToolTipText = "Information:" + '\n' + '\n' + "Name: " + a.name + '\n' + "License: " + a.lpn + '\n' + "Address: " + vnow.address + '\n' + "Speed: " + speed1 + " m/s" + '\n' + "Last update: " + vnow.time;
                        marker1.ToolTip.Fill = Brushes.Maroon;
                        marker1.ToolTip.Foreground = Brushes.White;
                        marker1.ToolTip.Stroke = Pens.White;
                        marker1.ToolTip.Format.Alignment = StringAlignment.Near;
                        marker1.Bearing = vnow.direction; // Rotation angle
                        marker1.Fill = new SolidBrush(Color.FromArgb(155, Color.Maroon)); // Arrow color
                        markers.Markers[i - 1] = marker1;
                    }
                    gmap.Overlays.Add(markers);
                    kq = true;

                }
            }
            catch (Exception ex)
            {

            }
        }
        private async void timernow_Tick(object sender, EventArgs e)
        {
            updatelocation();
        }   

        private void btnFind_Click(object sender, EventArgs e)
        {
            int index = cbbcar.SelectedIndex;
            if ((index!=-1)&&kq)
            gmap.Position = new GMap.NET.PointLatLng(markers.Markers[index].Position.Lat, markers.Markers[index].Position.Lng);
        }

        public static double DistanceTo(double lat1, double lon1, double lat2, double lon2, char unit = 'm')
        {
            double rlat1 = Math.PI * lat1 / 180;
            double rlat2 = Math.PI * lat2 / 180;
            double theta = lon1 - lon2;
            double rtheta = Math.PI * theta / 180;
            double dist =
                Math.Sin(rlat1) * Math.Sin(rlat2) + Math.Cos(rlat1) *
                Math.Cos(rlat2) * Math.Cos(rtheta);
            dist = Math.Acos(dist);
            dist = dist * 180 / Math.PI;
            dist = dist * 60 * 1.1515;

            switch (unit)
            {
                case 'm': //Kilometers -> default
                    return dist * 1.609344/1000;
                case 'N': //Nautical Miles 
                    return dist * 0.8684;
                case 'M': //Miles
                    return dist;
            }

            return dist;
        }
        FirebaseClient firebase = new FirebaseClient("https://the-ultimate-tracking.firebaseio.com/");
        public async Task<List<VehicleNow>> GetAllPersons(string vid,string date)
        {

            return (await firebase
              .Child("past")
              .Child(vid)
              .Child(date)
              .OnceAsync<VehicleNow>()).Select(item => new VehicleNow
              {
                  address=item.Object.address,
                  direction=item.Object.direction,
                  lat=item.Object.lat,
                  lon=item.Object.lon,
                  speed=item.Object.speed,
                  time=item.Object.time,
                  vehicle_id=item.Object.vehicle_id
              }).ToList();
        }
        private async void btnshowpath_Click(object sender, EventArgs e)
        {
            try
            {
                int index = cbbtracking.SelectedIndex;
                if (index >= 0)
                {
                    string sevehicle_id = vacc[index + 1].vehicle_id;
                    string date = dateTimetracking.Value.ToString("dd-MM-yyyy");
                    var allPersons = await GetAllPersons(sevehicle_id,date);
                    objtracking.Objects = null;
                    bool kq = false;
                    if (count > 1)
                    {
                        gmap.Position = new GMap.NET.PointLatLng(allPersons[0].lat, allPersons[0].lon);
                    }
                    DateTime pasttime = new DateTime();
                    List<PointLatLng> points = new List<PointLatLng>();
                    for (int i = 0; i < allPersons.Count - 1; i++)
                    {
                        VehicleNow past_data = allPersons[i];
                        points.Add(new PointLatLng(past_data.lat, past_data.lon));
                        DateTime a = DateTime.Parse(past_data.time);
                        int speed1 = Convert.ToInt32(past_data.speed);
                        pastlv newObject = new pastlv(a.ToString("HH:mm:ss"), past_data.address, speed1.ToString()+"m/s", past_data.lat, past_data.lon, past_data.direction);
                        objtracking.AddObject(newObject);
                    }
                    GMapTracking.Overlays.Add(routes);
                    GMapRoute route = new GMapRoute(points, "Tracking");
                    route.Stroke = new Pen(Color.Red, 3);
                    routes.Routes.Add(route);
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void objtracking_ItemActivate(object sender, EventArgs e)
        {
            markers_tracking.Markers.Clear();
            var item = (pastlv)objtracking.GetItem(objtracking.SelectedIndex).RowObject;
            GMarkerArrow marker1 = new GMarkerArrow(new PointLatLng(item.Lat,item.Lon));
            marker1.Bearing = item.Direction; // Rotation angle
            marker1.Fill = new SolidBrush(Color.FromArgb(155, Color.Blue)); // Arrow color
            markers_tracking.Markers.Add(marker1);
            GMapTracking.Overlays.Add(markers_tracking);
            GMapTracking.Position = new GMap.NET.PointLatLng(item.Lat, item.Lon);
        }
        public int count = 0;
        private void btncarrun_Click(object sender, EventArgs e)
        {
            timertracking.Stop();
            count = 0;
            timertracking.Start();
        }

        private void timertracking_Tick(object sender, EventArgs e)
        {
            if (count < objtracking.Items.Count)
            {
                var item = (pastlv)objtracking.GetItem(count).RowObject;
                GMarkerArrow marker1 = new GMarkerArrow(new PointLatLng(item.Lat, item.Lon));
                marker1.Bearing = item.Direction; // Rotation angle
                marker1.Fill = new SolidBrush(Color.FromArgb(155, Color.Blue)); // Arrow color
                markers_tracking_show.Markers.Clear();
                markers_tracking_show.Markers.Add(marker1);
                GMapTracking.Overlays.Add(markers_tracking_show);
                GMapTracking.Position = new GMap.NET.PointLatLng(item.Lat, item.Lon);
                count++;
            }
            else
            {
                timertracking.Stop();
                markers_tracking_show.Markers.Clear();
                GMapTracking.Overlays.Add(markers_tracking_show);
            }
        }

        private void btnclear_Click(object sender, EventArgs e)
        {
            markers_tracking_show.Markers.Clear();
            markers_tracking.Markers.Clear();
            routes.Routes.Clear();
            timertracking.Stop();
            GMapTracking.Overlays.Add(markers_tracking_show);
            GMapTracking.Overlays.Add(markers_tracking);
            GMapTracking.Overlays.Add(routes);
        }



        private async void button1_Click(object sender, EventArgs e)
        {
            FirebaseResponse response = await ConnectDatabase.client.GetAsync("user/"+user_id.ToString());
            UserAcc us_data = response.ResultAs<UserAcc>();
            if (us_data.password == txtpass.Text)
            {
                if ((txtnewpass.Text==txtnewpassconfirm.Text)&&(txtnewpass.Text!=""))
                {
                    us_data.password = txtnewpass.Text;
                    FirebaseResponse response_user = await ConnectDatabase.client.UpdateAsync("user/" + user_id,us_data);
                }
                if ((txtlicense.Text!="")&&(txtname.Text!=""))
                {
                    int index = cbbvehicle_setting.SelectedIndex;;
                    if (index >= 0)
                    {
                        string sevehicle_id = vacc[index + 1].vehicle_id;
                        FirebaseResponse response_vehicle = await ConnectDatabase.client.GetAsync("vehicle/" + sevehicle_id);
                        Vehicle vehicle_detail = response_vehicle.ResultAs<Vehicle>();
                        vehicle_detail.lpn = txtlicense.Text;vehicle_detail.name = txtname.Text;
                        FirebaseResponse response_vehicle_update = await ConnectDatabase.client.UpdateAsync("vehicle/" + sevehicle_id, vehicle_detail);
                    }
                    SearchVehicle();
                }
                MessageBox.Show("Updated information successfully", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private async void cbbvehicle_setting_SelectedIndexChanged(object sender, EventArgs e)
        {
            int index = cbbvehicle_setting.SelectedIndex;
            if (index >= 0)
            {
                string sevehicle_id = vacc[index + 1].vehicle_id;
                FirebaseResponse response = await ConnectDatabase.client.GetAsync("vehicle/" + sevehicle_id);
                Vehicle vehicle_detail = response.ResultAs<Vehicle>();
                txtlicense.Text = vehicle_detail.lpn;
                txtname.Text = vehicle_detail.name;
            }
        }

        private void txtname_TextChanged(object sender, EventArgs e)
        {

        }

        private void gmap_Load(object sender, EventArgs e)
        {

        }
    }
       
}
