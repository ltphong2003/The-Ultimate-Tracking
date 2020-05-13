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
namespace TheUltimateTracking
{
    public partial class Signup : Form
    {
        public Signup()
        {
            InitializeComponent();
        }

        private void btnSignup_Click(object sender, EventArgs e)
        {
            if ((txtEmailSignup.Text != "") && (txtPasswordSignup.Text != "")&&(txtPasswordSignup.Text==txtPassconfirm.Text))
            {
                UserAcc new_user = new UserAcc
                {
                    email = txtEmailSignup.Text,
                    password = txtPasswordSignup.Text
                };
                User.InsertUser(new_user);
            }
            this.Close();
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
