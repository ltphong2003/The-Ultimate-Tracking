using Firebase.Database;
using Firebase.Database.Query;

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using The_Ultimate_Tracking.Model;

namespace The_Ultimate_Tracking.ViewModel
{
    class FirebaseHelper
    {
        //Connect app with firebase using API Url  
        public static FirebaseClient firebase = new FirebaseClient("https://the-ultimate-tracking.firebaseio.com/");

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
        public class PastValue
        {
            public string number { get; set; }
        }
        public static async Task<List<PastValue>> GetAllDetails()
        {
            return (await firebase
              .Child("user/detail")
              .OnceAsync<PastValue>()).Select(item => new PastValue
              {
                  number = item.Object.number
              }).ToList();
        }

        public static async Task<PastValue> ReadDetail()
        {
            try
            {
                var alldetail = await GetAllDetails();
                await firebase
                .Child("user/detail")
                .OnceAsync<User>();
                return alldetail.FirstOrDefault();
            }
            catch (Exception e)
            {
                Debug.WriteLine($"Error:{e}");
                return null;
            }
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
        public class PastValueVehi
        {
            public string numbervehi { get; set; }
        }
        public static async Task<List<PastValueVehi>> GetAllDetailsVehi()
        {
            return (await firebase
              .Child("vehicle/detail")
              .OnceAsync<PastValueVehi>()).Select(item => new PastValueVehi
              {
                  numbervehi = item.Object.numbervehi
              }).ToList();
        }

        public static async Task<PastValueVehi> ReadDetailVehi()
        {
            try
            {
                var alldetail = await GetAllDetailsVehi();
                await firebase
                .Child("vehicle/detail")
                .OnceAsync<User>();
                return alldetail.FirstOrDefault();
            }
            catch (Exception e)
            {
                Debug.WriteLine($"Error:{e}");
                return null;
            }
        }

        //Add Vehicle
        public static async Task<bool> AddVehicle(string Imei, string Lpn, string Name, string User_id, string Vehicle_Id)
        {
            try
            {
                await firebase
                .Child("vehicle")
                .PostAsync(new Vehicle() { 
                    imei = Imei, name = Name, lpn = Lpn, user_id = User_id, vehicle_id = Vehicle_Id
                });
                return true;
            }
            catch (Exception e)
            {
                Debug.WriteLine($"Error:{e}");
                return false;
            }
        }
    }
}