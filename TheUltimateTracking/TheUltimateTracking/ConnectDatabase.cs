using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp.Response;

namespace Database
{
    public class ConnectDatabase
    {
        public static IFirebaseConfig config = new FirebaseConfig
        {
            AuthSecret = "cOsfW43OgJtNzF6tTrSPO6jqkTj65ajlxpPfIWTw",
            BasePath = "https://the-ultimate-tracking.firebaseio.com/"
        };
        public static IFirebaseClient client = new FireSharp.FirebaseClient(config);

    }
    public class UserAcc
    {
        public string email { get; set; }
        public string password { get; set; }
    }
    public class VehicleAcc
    {
        public string imei { get; set; }
        public string lpn { get; set; }
        public string name { get; set; }
        public string password { get; set; }
        public string user_id { get; set; }
        public string vehicle_id { get; set; }
    }
    public class VehicleNow
    { 
        public float direction { get; set; }
        public float lat { get; set; }
        public float lon { get; set; }
        public float speed { get; set; }
        public string time { get; set; }
        public string address { get; set; }
    }
    public class Vehicle
    {
        public string imei { get; set; }
        public string lpn { get; set; }
        public string name { get; set; }
        public string user_id { get; set; }
        public string vehicle_id { get; set; }
    }

    public class UserDetail
    {
        public int number { get; set; }
    }
    public class PastDetail
    {
        public int number { get; set; }
    }
    public class VehicleDetail
    {
        public int number { get; set; }
    }
    public class User
    {     
        public async static void InsertUser(UserAcc us)
        {
            bool isExist = false;
            FirebaseResponse response = await ConnectDatabase.client.GetAsync("user/detail");
            UserDetail detail = response.ResultAs<UserDetail>();
            int user_number = Convert.ToInt32(detail.number);
            for (int i = 1; i <= user_number; i++)
            {
                FirebaseResponse response_user = await ConnectDatabase.client.GetAsync("user/" + i.ToString());
                UserAcc us_data = response_user.ResultAs<UserAcc>();
                if (us_data.email == us.email)
                {   
                    isExist = true;
                    break;
                }
            }
            if (!isExist)
            {
                user_number++;
                detail.number = user_number;
                FirebaseResponse updateUserNumber = await ConnectDatabase.client.UpdateAsync("user/detail", detail);
                SetResponse insertUser = await ConnectDatabase.client.SetAsync("user/" + user_number, us);
            }
        }
    }
}
