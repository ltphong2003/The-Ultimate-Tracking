using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Database;
using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp.Response;
using Firebase.Database;
using Firebase.Database.Query;

namespace TheUltimateTracking
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }
        FirebaseClient firebase = new FirebaseClient("https://the-ultimate-tracking.firebaseio.com/");
        public async Task<List<UserAcc>> GetAllUsers()
        {

            return (await firebase
              .Child("user")
              .OnceAsync<UserAcc>()).Select(item => new UserAcc
              {
                 email=item.Object.email,
                 password=item.Object.password,
                 user_id=item.Object.user_id
              }).ToList();
        }
        public async Task<UserAcc> GetPerson(string email)
        {
            var allPersons = await GetAllUsers();
            await firebase
              .Child("user")
              .OnceAsync<UserAcc>();
            return allPersons.Where(a => a.email== email).FirstOrDefault();
        }
        private async void btnLogin_Click(object sender, EventArgs e)
        {
            string email = txtEmail.Text;
            string pass = txtPass.Text;
            if ((email != "") && (pass != ""))
            {
                var user = await GetPerson(email);
                if (user != null)
                {
                    if (user.password == pass)
                    {
                        Main.user_id = Convert.ToInt32(user.user_id);
                        this.Close();
                        return;
                    }
                }


            }
            else MessageBox.Show("Email hoặc mật khẩu không đúng");    
        }

        private void btnSignup_Click(object sender, EventArgs e)
        {
            Signup a = new Signup();
            a.Location = this.Location;
            a.StartPosition = FormStartPosition.Manual;
            a.ShowDialog();
        }

        private void Login_Load(object sender, EventArgs e)
        {

        }
    }
}
