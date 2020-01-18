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

namespace The_Ultimate_Tracking
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void btnSignup_Click(object sender, EventArgs e)
        {
            if ((txtEmailSignup.Text!="")&&(txtPasswordSignup.Text!=""))
            {
                UserAcc new_user = new UserAcc
                {
                    email = txtEmailSignup.Text,
                    password = txtPasswordSignup.Text
                };
               User.InsertUser(new_user);
            }
        }

        private void Login_Load(object sender, EventArgs e)
        {

        }
    }
}
