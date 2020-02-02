namespace TheUltimateTracking
{
    partial class Main
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.HomePage = new System.Windows.Forms.TabPage();
            this.GMap = new GMap.NET.WindowsForms.GMapControl();
            this.TrackingPage = new System.Windows.Forms.TabPage();
            this.SettingPage = new System.Windows.Forms.TabPage();
            this.imglIcon = new System.Windows.Forms.ImageList(this.components);
            this.gMapControl1 = new GMap.NET.WindowsForms.GMapControl();
            this.tabControl1.SuspendLayout();
            this.HomePage.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Alignment = System.Windows.Forms.TabAlignment.Left;
            this.tabControl1.Controls.Add(this.HomePage);
            this.tabControl1.Controls.Add(this.TrackingPage);
            this.tabControl1.Controls.Add(this.SettingPage);
            this.tabControl1.DrawMode = System.Windows.Forms.TabDrawMode.OwnerDrawFixed;
            this.tabControl1.ImageList = this.imglIcon;
            this.tabControl1.ItemSize = new System.Drawing.Size(100, 120);
            this.tabControl1.Location = new System.Drawing.Point(2, -1);
            this.tabControl1.Multiline = true;
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1063, 719);
            this.tabControl1.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this.tabControl1.TabIndex = 0;
            this.tabControl1.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.tabControl1_DrawItem);
            // 
            // HomePage
            // 
            this.HomePage.Controls.Add(this.gMapControl1);
            this.HomePage.Controls.Add(this.GMap);
            this.HomePage.ImageIndex = 0;
            this.HomePage.Location = new System.Drawing.Point(124, 4);
            this.HomePage.Name = "HomePage";
            this.HomePage.Padding = new System.Windows.Forms.Padding(3);
            this.HomePage.Size = new System.Drawing.Size(935, 711);
            this.HomePage.TabIndex = 0;
            this.HomePage.Text = "Home";
            this.HomePage.UseVisualStyleBackColor = true;
            // 
            // GMap
            // 
            this.GMap.Bearing = 0F;
            this.GMap.CanDragMap = true;
            this.GMap.EmptyTileColor = System.Drawing.Color.Navy;
            this.GMap.GrayScaleMode = false;
            this.GMap.HelperLineOption = GMap.NET.WindowsForms.HelperLineOptions.DontShow;
            this.GMap.LevelsKeepInMemory = 5;
            this.GMap.Location = new System.Drawing.Point(0, -4);
            this.GMap.MarkersEnabled = true;
            this.GMap.MaxZoom = 2;
            this.GMap.MinZoom = 2;
            this.GMap.MouseWheelZoomEnabled = true;
            this.GMap.MouseWheelZoomType = GMap.NET.MouseWheelZoomType.MousePositionAndCenter;
            this.GMap.Name = "GMap";
            this.GMap.NegativeMode = false;
            this.GMap.PolygonsEnabled = true;
            this.GMap.RetryLoadTile = 0;
            this.GMap.RoutesEnabled = true;
            this.GMap.ScaleMode = GMap.NET.WindowsForms.ScaleModes.Integer;
            this.GMap.SelectedAreaFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(65)))), ((int)(((byte)(105)))), ((int)(((byte)(225)))));
            this.GMap.ShowTileGridLines = false;
            this.GMap.Size = new System.Drawing.Size(939, 637);
            this.GMap.TabIndex = 0;
            this.GMap.Zoom = 0D;
            // 
            // TrackingPage
            // 
            this.TrackingPage.ImageIndex = 1;
            this.TrackingPage.Location = new System.Drawing.Point(124, 4);
            this.TrackingPage.Name = "TrackingPage";
            this.TrackingPage.Padding = new System.Windows.Forms.Padding(3);
            this.TrackingPage.Size = new System.Drawing.Size(935, 711);
            this.TrackingPage.TabIndex = 1;
            this.TrackingPage.Text = "Tracking";
            this.TrackingPage.UseVisualStyleBackColor = true;
            // 
            // SettingPage
            // 
            this.SettingPage.ImageIndex = 2;
            this.SettingPage.Location = new System.Drawing.Point(124, 4);
            this.SettingPage.Name = "SettingPage";
            this.SettingPage.Size = new System.Drawing.Size(935, 711);
            this.SettingPage.TabIndex = 2;
            this.SettingPage.Text = "Setting";
            this.SettingPage.UseVisualStyleBackColor = true;
            // 
            // imglIcon
            // 
            this.imglIcon.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imglIcon.ImageStream")));
            this.imglIcon.TransparentColor = System.Drawing.Color.Transparent;
            this.imglIcon.Images.SetKeyName(0, "pin.png");
            this.imglIcon.Images.SetKeyName(1, "track.png");
            this.imglIcon.Images.SetKeyName(2, "support.png");
            // 
            // gMapControl1
            // 
            this.gMapControl1.Bearing = 0F;
            this.gMapControl1.CanDragMap = true;
            this.gMapControl1.EmptyTileColor = System.Drawing.Color.Navy;
            this.gMapControl1.GrayScaleMode = false;
            this.gMapControl1.HelperLineOption = GMap.NET.WindowsForms.HelperLineOptions.DontShow;
            this.gMapControl1.LevelsKeepInMemory = 5;
            this.gMapControl1.Location = new System.Drawing.Point(0, -4);
            this.gMapControl1.MarkersEnabled = true;
            this.gMapControl1.MaxZoom = 2;
            this.gMapControl1.MinZoom = 2;
            this.gMapControl1.MouseWheelZoomEnabled = true;
            this.gMapControl1.MouseWheelZoomType = GMap.NET.MouseWheelZoomType.MousePositionAndCenter;
            this.gMapControl1.Name = "gMapControl1";
            this.gMapControl1.NegativeMode = false;
            this.gMapControl1.PolygonsEnabled = true;
            this.gMapControl1.RetryLoadTile = 0;
            this.gMapControl1.RoutesEnabled = true;
            this.gMapControl1.ScaleMode = GMap.NET.WindowsForms.ScaleModes.Integer;
            this.gMapControl1.SelectedAreaFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(65)))), ((int)(((byte)(105)))), ((int)(((byte)(225)))));
            this.gMapControl1.ShowTileGridLines = false;
            this.gMapControl1.Size = new System.Drawing.Size(939, 594);
            this.gMapControl1.TabIndex = 1;
            this.gMapControl1.Zoom = 0D;
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1067, 718);
            this.Controls.Add(this.tabControl1);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Main";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.tabControl1.ResumeLayout(false);
            this.HomePage.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage HomePage;
        private System.Windows.Forms.TabPage TrackingPage;
        private System.Windows.Forms.TabPage SettingPage;
        private System.Windows.Forms.ImageList imglIcon;
        private GMap.NET.WindowsForms.GMapControl GMap;
        private GMap.NET.WindowsForms.GMapControl gMapControl1;
    }
}

