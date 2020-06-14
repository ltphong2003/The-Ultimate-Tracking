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

namespace LocUpdFgService
{
    public class OnStartLocationEventArgs: EventArgs
    {
        private string mUsername;
        private string mPassword;
        public string Username
        {
            get { return mUsername; }
            set { mUsername = value; }
        }
        public string Password
        {
            get { return mPassword; }
            set { mPassword = value; }
        }
        public OnStartLocationEventArgs(string username, string password)
        {
            Username = username;
            Password = password;
        }
    }
    public class startlocation : DialogFragment
    {
        TextView edtTK;
        TextView edtMK;
        Button btn;
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }
        public event EventHandler<OnStartLocationEventArgs> mOnStartComplete;
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment
            // return inflater.Inflate(Resource.Layout.YourFragment, container, false);
            var view = inflater.Inflate(Resource.Layout.updatelocation, container, false);
            edtTK = view.FindViewById<EditText>(Resource.Id.edtLocationTK);
            edtMK = view.FindViewById<EditText>(Resource.Id.edtLocationMK);
            btn = view.FindViewById<Button>(Resource.Id.btnLocationStart);
            btn.Click += Btn_Click;
            return view;
        }

        private void Btn_Click(object sender, EventArgs e)
        {
           string user = edtTK.Text;
            string pass = edtMK.Text;
            mOnStartComplete.Invoke(this,new OnStartLocationEventArgs(user,pass));
            this.Dismiss();
            /*TelephonyManager mTelephonyMgr;
            //Telephone Number  
            mTelephonyMgr = (TelephonyManager)Android.App.Application.Context.GetSystemService(Android.Content.Context.TelephonyService);
            //IMEI number  
            String m_deviceId = mTelephonyMgr.DeviceId;
            Intent mIntent = new Intent(Android.App.Application.Context, typeof(UpdateLocation));
            mIntent.PutExtra("user", user);
            mIntent.PutExtra("pass", pass);
            mIntent.PutExtra("imei", m_deviceId);*/
            
        }
    }
}