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
using FireSharp.Response;
using FireSharp.Interfaces;
using FireSharp.Config;
using Firebase.Database;
using Firebase.Database.Query;
namespace TheUltimateTracking
{
    public partial class Signup : Form
    {
        public Signup()
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
                  email = item.Object.email,
                  password = item.Object.password,
                  user_id = item.Object.user_id
              }).ToList();
        }
        public async Task<UserAcc> GetPerson(string email)
        {
            var allPersons = await GetAllUsers();
            await firebase
              .Child("user")
              .OnceAsync<UserAcc>();
            return allPersons.Where(a => a.email == email).FirstOrDefault();
        }
        public async  void InsertUser(UserAcc us)
        {
            bool isExist = false;
            string email = us.email;
            var user = await GetPerson(email);
            if (user == null)
            {
                FirebaseResponse response = await ConnectDatabase.client.GetAsync("user/detail");
                UserDetail detail = response.ResultAs<UserDetail>();
                int user_number = Convert.ToInt32(detail.number);
                user_number++;
                detail.number = user_number;
                us.user_id = user_number.ToString();
                FirebaseResponse updateUserNumber = await ConnectDatabase.client.UpdateAsync("user/detail", detail);
                SetResponse insertUser = await ConnectDatabase.client.SetAsync("user/" + user_number, us);
                this.Close();
            }
            else
            {
                MessageBox.Show("Account is existed", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSignup_Click(object sender, EventArgs e)
        {
            if ((txtEmailSignup.Text != "") && (txtPasswordSignup.Text != "")&&(txtPasswordSignup.Text==txtPassconfirm.Text))
            {
                UserAcc new_user = new UserAcc
                {
                    email = txtEmailSignup.Text,
                    password = txtPasswordSignup.Text,
                    user_id ="a"
                };
                InsertUser(new_user);
            }
            
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {

        }

        private void Signup_Load(object sender, EventArgs e)
        {

        }
    }
}
