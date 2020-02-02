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
    class ConnectDatabase
    {
        public static IFirebaseConfig config = new FirebaseConfig
        {
            AuthSecret = "cOsfW43OgJtNzF6tTrSPO6jqkTj65ajlxpPfIWTw",
            BasePath = "https://the-ultimate-tracking.firebaseio.com/"
        };
        public static IFirebaseClient client = new FireSharp.FirebaseClient(config);

    }
    class UserAcc
    {
        public string email { get; set; }
        public string password { get; set; }
    }
    class UserDetail
    {
        public int number { get; set; }
    }
    class User
    {     
        public async static void InsertUser(UserAcc us)
        {
            bool isExist = false;
            FirebaseResponse response = await ConnectDatabase.client.GetTaskAsync("user/detail");
            UserDetail detail = response.ResultAs<UserDetail>();
            int user_number = Convert.ToInt32(detail.number);
            for (int i = 1; i <= user_number; i++)
            {
                FirebaseResponse response_user = await ConnectDatabase.client.GetTaskAsync("user/" + i.ToString());
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
                FirebaseResponse updateUserNumber = await ConnectDatabase.client.UpdateTaskAsync("user/detail", detail);
                SetResponse insertUser = await ConnectDatabase.client.SetTaskAsync("user/" + user_number, us);
            }
        }
    }
}
