using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace The_Ultimate_Tracking
{
    public partial class Main : Form
    {
        public static int user_id = 0;
        public Main()
        {
            InitializeComponent();
        }

        private void tabControl1_DrawItem(object sender, DrawItemEventArgs e)
        {
            var g = e.Graphics;
            var text = this.tabControl1.TabPages[e.Index].Text;
            var sizeText = g.MeasureString(text, this.tabControl1.Font);

            var x = e.Bounds.Left + (e.Bounds.Width - sizeText.Width) / 2;
            var y = e.Bounds.Top + (e.Bounds.Height - sizeText.Height) / 2;

            g.DrawString(text, this.tabControl1.Font, Brushes.Black, x, y);
        }

        private void Main_Load(object sender, EventArgs e)
        {
            if (user_id==0)
            {
                Login form = new Login();
                form.Location = this.Location;
                form.StartPosition = FormStartPosition.Manual;
                form.ShowDialog();
            }
            if (user_id==0)
            {
                this.Close();
            }
        }
    }
}
