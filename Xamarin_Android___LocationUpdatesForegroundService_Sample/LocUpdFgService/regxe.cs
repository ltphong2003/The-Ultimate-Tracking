using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using Android.Telephony;
using System.Net;
using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp.Response;

namespace LocUpdFgService
{
    public class regxe : DialogFragment
    {
        Button btnReg;
        TextView tvimei;
        TextView tvStatus;
        EditText edtTK;
        EditText edtMK;
        EditText edtBSX;
        EditText edtName;
        ProgressDialog mDialog;
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
           

        }
        public class VehicleAcc
        {
            public string imei { get; set; }
            public string lpn { get; set; }
            public string name { get; set; }
            public string user_id { get; set; }
            public string vehicle_id { get; set; }
        }
        public class PastValueVehi
        {
            public string number { get; set; }
        }
        public class User
        {
            public string email { get; set; }
            public string password { get; set; }
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
        private async void BtnReg_Click(object sender, EventArgs e)
        {
            string email = edtTK.Text;
            string pass = edtMK.Text;
            string bsx = edtBSX.Text;
            string imei = tvimei.Text;
            string Name = edtName.Text;
            try
            {
                int User_id=0;
                FirebaseResponse response_vdetail = await ConnectDatabase.client.GetAsync("user/detail");
                PastValueVehi detail = response_vdetail.ResultAs<PastValueVehi>();
                int vehi_number = Convert.ToInt32(detail.number);
                for (int i = 1; i <= vehi_number; i++)
                {
                    FirebaseResponse response_vehi = await ConnectDatabase.client.GetAsync("user/" + i.ToString());
                    User vehi_data = response_vehi.ResultAs<User>();
                    if ((vehi_data.email == email)&&(vehi_data.password==pass))
                    {
                        User_id = i;
                    }
                }
                FirebaseResponse response_vdetail1 = await ConnectDatabase.client.GetAsync("vehicle/detail");
                PastValueVehi detail1 = response_vdetail1.ResultAs<PastValueVehi>();
                int vehi_number1 = Convert.ToInt32(detail1.number);
                bool is_exist = false;
                for (int i = 1; i <= vehi_number1; i++)
                {
                    FirebaseResponse response_vehi = await ConnectDatabase.client.GetAsync("vehicle/" + i.ToString());
                    VehicleAcc vehi_data = response_vehi.ResultAs<VehicleAcc>();
                    if (vehi_data.imei == imei)
                    {
                        is_exist = true;
                        break;
                    }
                }
                if (!is_exist)
                {
                    vehi_number++;
                    detail.number = vehi_number.ToString();
                    VehicleAcc vehi = new VehicleAcc
                    {
                        imei = imei,
                        lpn = bsx,
                        name = Name,
                        user_id = User_id.ToString(),
                        vehicle_id = vehi_number.ToString()
                    };
                    FirebaseResponse response_createvehi = await ConnectDatabase.client.SetAsync("vehicle/" + vehi_number, vehi);
                    FirebaseResponse response_detailvehi = await ConnectDatabase.client.UpdateAsync("vehicle/detail", detail);
                }
            }
            catch (Exception ex)
            { 
            
            }
            this.Activity.RunOnUiThread(() =>
            {

                this.Dismiss();

            });
            // Toast.MakeText(Android.App.Application.Context, user + " " + pass + " " + bsx, ToastLength.Long).Show();
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment
            // return inflater.Inflate(Resource.Layout.YourFragment, container, false);
        
            var view = inflater.Inflate(Resource.Layout.regxe, container, false);
            tvimei = view.FindViewById<TextView>(Resource.Id.tvRegImei);
            TelephonyManager mTelephonyMgr;
            //Telephone Number  
            mTelephonyMgr = (TelephonyManager)Android.App.Application.Context.GetSystemService(Android.Content.Context.TelephonyService);
            //IMEI number  
            String m_deviceId = mTelephonyMgr.DeviceId;
            tvimei.Text = m_deviceId;
            btnReg = view.FindViewById<Button>(Resource.Id.btnRegDK);
            edtTK = view.FindViewById<EditText>(Resource.Id.tvRegTK);
            edtMK = view.FindViewById<EditText>(Resource.Id.tvRegMK);
            edtBSX = view.FindViewById<EditText>(Resource.Id.tvRegBSX);
            edtName= view.FindViewById<EditText>(Resource.Id.tvRegName);
            btnReg.Click += BtnReg_Click;
            return view;
        }
    }
}