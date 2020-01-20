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

namespace The_Ultimate_Tracking
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Signup a = new Signup();
            a.Location = this.Location;
            a.StartPosition = FormStartPosition.Manual;
            a.ShowDialog();
        }

        private void Login_Load(object sender, EventArgs e)
        {
           
        }

        private async void btnLogin_Click(object sender, EventArgs e)
        {
            string email = txtEmail.Text;
            string pass = txtPass.Text;
            if ((email!="")&&(pass!=""))
            {
                FirebaseResponse response = await ConnectDatabase.client.GetTaskAsync("user/detail");
                UserDetail detail = response.ResultAs<UserDetail>();
                int user_number = Convert.ToInt32(detail.number);
                for (int i = 1; i <= user_number; i++)
                {
                    FirebaseResponse response_user = await ConnectDatabase.client.GetTaskAsync("user/" + i.ToString());
                    UserAcc us_data = response_user.ResultAs<UserAcc>();
                    if (us_data.email == email)
                    {
                        Main.user_id = i;
                        this.Close();
                        return;
                    }
                }
            }
            MessageBox.Show("Email hoặc mật khẩu không đúng");
        }
    }
}
