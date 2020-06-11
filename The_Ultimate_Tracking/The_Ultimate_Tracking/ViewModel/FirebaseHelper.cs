using Firebase.Database;
using Firebase.Database.Query;

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using The_Ultimate_Tracking.Model;
using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp.Response;
namespace The_Ultimate_Tracking.ViewModel
{
    class FirebaseHelper
    {
        //Connect app with firebase using API Url  
        public static FirebaseClient firebase = new FirebaseClient("https://the-ultimate-tracking.firebaseio.com/");
        //new FirebaseOptions { AuthTokenAsyncFactory = async() => (await auth.GetFreshAuthAsync()).FirebaseToken });

        //Read all user data
        public static async Task<List<User>> GetAllUsers()
        {
            try
            {
                var userlist = (await firebase
                .Child("user")
                .OnceAsync<User>()).Select(item =>
                new User
                {
                    email = item.Object.email,
                    password = item.Object.password
                })             
                .ToList();
                return userlist;
            }
            catch (Exception e)
            {
                Debug.WriteLine($"Error:{e}");
                return null;
            }
        }

        //Read user data    
        public static async Task<User> GetUser(string Email)
        {
            try
            {
                var allUsers = await GetAllUsers();
                await firebase
                .Child("user")
                .OnceAsync<User>();
                return allUsers.Where(a => a.email == Email).FirstOrDefault();
            }
            catch (Exception e)
            {
                Debug.WriteLine($"Error:{e}");
                return null;
            }
        }

        //Get user detail
        public static int User_id;
        public static async Task<string> GetAllDetails(string email)
        {
            FirebaseResponse response_vdetail = await ConnectDatabase.client.GetAsync("user/detail");
            PastValueVehi detail = response_vdetail.ResultAs<PastValueVehi>();
            int vehi_number = Convert.ToInt32(detail.number);
            bool is_exist = false;
            for (int i = 1; i <= vehi_number; i++)
            {
                FirebaseResponse response_vehi = await ConnectDatabase.client.GetAsync("user/" + i.ToString());
                User vehi_data = response_vehi.ResultAs<User>();
                if (vehi_data.email == email)
                {
                    User_id = i;                
                }
            }
            return User_id.ToString();
        }

        //Add User
        public static async Task<bool> AddUser(string Email, string Password)
        {
            try
            {              
                await firebase
                .Child("user")
                .PostAsync(new User() { email = Email, password = Password });
                return true;
            }
            catch (Exception e)
            {
                Debug.WriteLine($"Error:{e}");
                return false;
            }
        }

        //Update user    
        public static async Task<bool> UpdateUser(string Email, string Password)
        {
            try
            {
                var toUpdateUser = (await firebase
                .Child("user")
                .OnceAsync<User>()).Where(a => a.Object.email == Email).FirstOrDefault();
                await firebase
                .Child("user")
                .Child(toUpdateUser.Key)
                .PutAsync(new User() { email = Email, password = Password });
                return true;
            }
            catch (Exception e)
            {
                Debug.WriteLine($"Error:{e}");
                return false;
            }
        }

        //Delete User    
        public static async Task<bool> DeleteUser(string Email)
        {
            try
            {
                var toDeletePerson = (await firebase
                .Child("user")
                .OnceAsync<User>()).Where(a => a.Object.email == Email).FirstOrDefault();
                await firebase.Child("user").Child(toDeletePerson.Key).DeleteAsync();
                return true;
            }
            catch (Exception e)
            {
                Debug.WriteLine($"Error:{e}");
                return false;
            }
        }

        //Get vehicle detail
        public static async Task<List<PastValueVehi>> GetAllDetailsVehi()
        {
            //try
            {
                return (await firebase
                  .Child("vehicle/detail")                 
                  .OnceAsync<PastValueVehi>()).Select(item => 
                  new PastValueVehi
                  {
                      number = item.Object.number
                  }).ToList();
            }
            /*catch (Exception e)
            {
                Debug.WriteLine($"Error:{e}");
                return null;
            }*/
        }

        public static async Task<PastValueVehi> ReadDetailVehi()
        {
            try
            {
                var alldetail = await GetAllDetailsVehi();
                await firebase
                .Child("vehicle")
                .Child("detail")
                .OnceAsync<PastValueVehi>();
                return alldetail.FirstOrDefault();
            }
            catch (Exception e)
            {
                Debug.WriteLine($"Error:{e}");
                return null;
            }
        }

        //Add Vehicle
        public class VehicleAcc
        {
            public string imei { get; set; }
            public string lpn { get; set; }
            public string name { get; set; }
            public string user_id { get; set; }
            public string vehicle_id { get; set; }
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
        public static async Task<bool>AddVehicle(string Imei, string Lpn, string Name)
        {
            try
            {
                FirebaseResponse response_vdetail = await ConnectDatabase.client.GetAsync("vehicle/detail");
                PastValueVehi detail = response_vdetail.ResultAs<PastValueVehi>();
                int vehi_number = Convert.ToInt32(detail.number);
                bool is_exist = false;
                for (int i=1;i<=vehi_number;i++)
                {
                    FirebaseResponse response_vehi = await ConnectDatabase.client.GetAsync("vehicle/" + i.ToString());
                    VehicleAcc vehi_data = response_vehi.ResultAs<VehicleAcc>();
                    if (vehi_data.imei==Imei)
                    {
                        is_exist = true;
                        return false;
                    }
                }
                if (!is_exist)
                {
                    vehi_number++;
                    detail.number = vehi_number.ToString();
                    VehicleAcc vehi = new VehicleAcc
                    {
                        imei = Imei,
                        lpn = Lpn,
                        name = Name,
                        user_id = User_id.ToString(),
                        vehicle_id = vehi_number.ToString()
                    };
                    FirebaseResponse response_createvehi = await ConnectDatabase.client.SetAsync("vehicle/" + vehi_number,vehi);
                    FirebaseResponse response_detailvehi = await ConnectDatabase.client.UpdateAsync("vehicle/detail" , detail);
                }
                return true;
            }
            catch (Exception e)
            {
                Debug.WriteLine($"Error:{e}");
                return false;
            }
        }
        
        //Read all vehicle data
        public static async Task<List<Vehicle>> GetAllVehis()
        {
            try
            {
                return (await firebase
                .Child("vehicle")
                .OnceAsync<Vehicle>()).Select(item =>
                new Vehicle
                {
                    imei = item.Object.imei,
                    lpn = item.Object.lpn,
                    name = item.Object.name,
                    user_id = item.Object.user_id,
                    vehicle_id = item.Object.vehicle_id
                })
                .ToList();
            }
            catch (Exception e)
            {
                Debug.WriteLine($"Error:{e}");
                return null;
            }
        }
    }
}