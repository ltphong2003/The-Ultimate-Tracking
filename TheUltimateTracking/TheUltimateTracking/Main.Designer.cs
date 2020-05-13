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
            this.btnFind = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.cbbcar = new System.Windows.Forms.ComboBox();
            this.gmap = new GMap.NET.WindowsForms.GMapControl();
            this.TrackingPage = new System.Windows.Forms.TabPage();
            this.btnclear = new System.Windows.Forms.Button();
            this.objtracking = new BrightIdeasSoftware.ObjectListView();
            this.TimeColumn = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.AddressColumn = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.SpeedColumn = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.btncarrun = new System.Windows.Forms.Button();
            this.btnshowpath = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.dateTimetracking = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.cbbtracking = new System.Windows.Forms.ComboBox();
            this.GMapTracking = new GMap.NET.WindowsForms.GMapControl();
            this.SettingPage = new System.Windows.Forms.TabPage();
            this.button1 = new System.Windows.Forms.Button();
            this.txtname = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txtlicense = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.cbbvehicle_setting = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtnewpassconfirm = new System.Windows.Forms.TextBox();
            this.txtnewpass = new System.Windows.Forms.TextBox();
            this.txtpass = new System.Windows.Forms.TextBox();
            this.imglIcon = new System.Windows.Forms.ImageList(this.components);
            this.timernow = new System.Windows.Forms.Timer(this.components);
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.timertracking = new System.Windows.Forms.Timer(this.components);
            this.tabControl1.SuspendLayout();
            this.HomePage.SuspendLayout();
            this.TrackingPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.objtracking)).BeginInit();
            this.SettingPage.SuspendLayout();
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
            this.HomePage.Controls.Add(this.btnFind);
            this.HomePage.Controls.Add(this.label3);
            this.HomePage.Controls.Add(this.cbbcar);
            this.HomePage.Controls.Add(this.gmap);
            this.HomePage.ImageIndex = 0;
            this.HomePage.Location = new System.Drawing.Point(124, 4);
            this.HomePage.Name = "HomePage";
            this.HomePage.Padding = new System.Windows.Forms.Padding(3);
            this.HomePage.Size = new System.Drawing.Size(935, 711);
            this.HomePage.TabIndex = 0;
            this.HomePage.Text = "Home";
            this.HomePage.UseVisualStyleBackColor = true;
            // 
            // btnFind
            // 
            this.btnFind.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnFind.Location = new System.Drawing.Point(733, 661);
            this.btnFind.Name = "btnFind";
            this.btnFind.Size = new System.Drawing.Size(179, 36);
            this.btnFind.TabIndex = 7;
            this.btnFind.Text = "Find";
            this.btnFind.UseVisualStyleBackColor = true;
            this.btnFind.Click += new System.EventHandler(this.btnFind_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(6, 664);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(147, 26);
            this.label3.TabIndex = 5;
            this.label3.Text = "Select vehicle";
            // 
            // cbbcar
            // 
            this.cbbcar.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbbcar.FormattingEnabled = true;
            this.cbbcar.Location = new System.Drawing.Point(174, 661);
            this.cbbcar.Name = "cbbcar";
            this.cbbcar.Size = new System.Drawing.Size(542, 34);
            this.cbbcar.TabIndex = 4;
            // 
            // gmap
            // 
            this.gmap.Bearing = 0F;
            this.gmap.CanDragMap = true;
            this.gmap.EmptyTileColor = System.Drawing.Color.RoyalBlue;
            this.gmap.GrayScaleMode = false;
            this.gmap.HelperLineOption = GMap.NET.WindowsForms.HelperLineOptions.DontShow;
            this.gmap.LevelsKeepInMemmory = 5;
            this.gmap.Location = new System.Drawing.Point(0, -4);
            this.gmap.MarkersEnabled = true;
            this.gmap.MaxZoom = 18;
            this.gmap.MinZoom = 2;
            this.gmap.MouseWheelZoomEnabled = true;
            this.gmap.MouseWheelZoomType = GMap.NET.MouseWheelZoomType.MousePositionAndCenter;
            this.gmap.Name = "gmap";
            this.gmap.NegativeMode = false;
            this.gmap.PolygonsEnabled = true;
            this.gmap.RetryLoadTile = 0;
            this.gmap.RoutesEnabled = true;
            this.gmap.ScaleMode = GMap.NET.WindowsForms.ScaleModes.Integer;
            this.gmap.SelectedAreaFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(65)))), ((int)(((byte)(105)))), ((int)(((byte)(225)))));
            this.gmap.ShowTileGridLines = false;
            this.gmap.Size = new System.Drawing.Size(943, 654);
            this.gmap.TabIndex = 0;
            this.gmap.Zoom = 13D;
            this.gmap.Load += new System.EventHandler(this.gmap_Load);
            // 
            // TrackingPage
            // 
            this.TrackingPage.Controls.Add(this.btnclear);
            this.TrackingPage.Controls.Add(this.objtracking);
            this.TrackingPage.Controls.Add(this.btncarrun);
            this.TrackingPage.Controls.Add(this.btnshowpath);
            this.TrackingPage.Controls.Add(this.label2);
            this.TrackingPage.Controls.Add(this.dateTimetracking);
            this.TrackingPage.Controls.Add(this.label1);
            this.TrackingPage.Controls.Add(this.cbbtracking);
            this.TrackingPage.Controls.Add(this.GMapTracking);
            this.TrackingPage.ImageIndex = 1;
            this.TrackingPage.Location = new System.Drawing.Point(124, 4);
            this.TrackingPage.Name = "TrackingPage";
            this.TrackingPage.Padding = new System.Windows.Forms.Padding(3);
            this.TrackingPage.Size = new System.Drawing.Size(935, 711);
            this.TrackingPage.TabIndex = 1;
            this.TrackingPage.Text = "Tracking";
            // 
            // btnclear
            // 
            this.btnclear.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnclear.Location = new System.Drawing.Point(256, 187);
            this.btnclear.Name = "btnclear";
            this.btnclear.Size = new System.Drawing.Size(109, 38);
            this.btnclear.TabIndex = 10;
            this.btnclear.Text = "Clear";
            this.btnclear.UseVisualStyleBackColor = true;
            this.btnclear.Click += new System.EventHandler(this.btnclear_Click);
            // 
            // objtracking
            // 
            this.objtracking.AllColumns.Add(this.TimeColumn);
            this.objtracking.AllColumns.Add(this.AddressColumn);
            this.objtracking.AllColumns.Add(this.SpeedColumn);
            this.objtracking.CellEditUseWholeCell = false;
            this.objtracking.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.TimeColumn,
            this.AddressColumn,
            this.SpeedColumn});
            this.objtracking.Cursor = System.Windows.Forms.Cursors.Default;
            this.objtracking.HideSelection = false;
            this.objtracking.Location = new System.Drawing.Point(11, 231);
            this.objtracking.Name = "objtracking";
            this.objtracking.RowHeight = 40;
            this.objtracking.Size = new System.Drawing.Size(354, 472);
            this.objtracking.TabIndex = 9;
            this.objtracking.UseCompatibleStateImageBehavior = false;
            this.objtracking.View = System.Windows.Forms.View.Details;
            this.objtracking.ItemActivate += new System.EventHandler(this.objtracking_ItemActivate);
            // 
            // TimeColumn
            // 
            this.TimeColumn.AspectName = "Time";
            this.TimeColumn.Groupable = false;
            this.TimeColumn.Text = "Time";
            this.TimeColumn.WordWrap = true;
            // 
            // AddressColumn
            // 
            this.AddressColumn.AspectName = "Address";
            this.AddressColumn.FillsFreeSpace = true;
            this.AddressColumn.Groupable = false;
            this.AddressColumn.Text = "Address";
            this.AddressColumn.WordWrap = true;
            // 
            // SpeedColumn
            // 
            this.SpeedColumn.AspectName = "Speed";
            this.SpeedColumn.Groupable = false;
            this.SpeedColumn.Text = "Speed";
            this.SpeedColumn.WordWrap = true;
            // 
            // btncarrun
            // 
            this.btncarrun.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btncarrun.Location = new System.Drawing.Point(143, 187);
            this.btncarrun.Name = "btncarrun";
            this.btncarrun.Size = new System.Drawing.Size(107, 38);
            this.btncarrun.TabIndex = 7;
            this.btncarrun.Text = "Run";
            this.btncarrun.UseVisualStyleBackColor = true;
            this.btncarrun.Click += new System.EventHandler(this.btncarrun_Click);
            // 
            // btnshowpath
            // 
            this.btnshowpath.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnshowpath.Location = new System.Drawing.Point(11, 187);
            this.btnshowpath.Name = "btnshowpath";
            this.btnshowpath.Size = new System.Drawing.Size(126, 38);
            this.btnshowpath.TabIndex = 6;
            this.btnshowpath.Text = "Show path";
            this.btnshowpath.UseVisualStyleBackColor = true;
            this.btnshowpath.Click += new System.EventHandler(this.btnshowpath_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(6, 84);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(121, 26);
            this.label2.TabIndex = 5;
            this.label2.Text = "Select date";
            // 
            // dateTimetracking
            // 
            this.dateTimetracking.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dateTimetracking.Location = new System.Drawing.Point(11, 129);
            this.dateTimetracking.Name = "dateTimetracking";
            this.dateTimetracking.Size = new System.Drawing.Size(354, 32);
            this.dateTimetracking.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(6, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(147, 26);
            this.label1.TabIndex = 3;
            this.label1.Text = "Select vehicle";
            // 
            // cbbtracking
            // 
            this.cbbtracking.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbbtracking.FormattingEnabled = true;
            this.cbbtracking.Location = new System.Drawing.Point(11, 35);
            this.cbbtracking.Name = "cbbtracking";
            this.cbbtracking.Size = new System.Drawing.Size(354, 34);
            this.cbbtracking.TabIndex = 2;
            // 
            // GMapTracking
            // 
            this.GMapTracking.Bearing = 0F;
            this.GMapTracking.CanDragMap = true;
            this.GMapTracking.EmptyTileColor = System.Drawing.Color.RoyalBlue;
            this.GMapTracking.GrayScaleMode = false;
            this.GMapTracking.HelperLineOption = GMap.NET.WindowsForms.HelperLineOptions.DontShow;
            this.GMapTracking.LevelsKeepInMemmory = 5;
            this.GMapTracking.Location = new System.Drawing.Point(371, 6);
            this.GMapTracking.MarkersEnabled = true;
            this.GMapTracking.MaxZoom = 18;
            this.GMapTracking.MinZoom = 2;
            this.GMapTracking.MouseWheelZoomEnabled = true;
            this.GMapTracking.MouseWheelZoomType = GMap.NET.MouseWheelZoomType.MousePositionAndCenter;
            this.GMapTracking.Name = "GMapTracking";
            this.GMapTracking.NegativeMode = false;
            this.GMapTracking.PolygonsEnabled = true;
            this.GMapTracking.RetryLoadTile = 0;
            this.GMapTracking.RoutesEnabled = true;
            this.GMapTracking.ScaleMode = GMap.NET.WindowsForms.ScaleModes.Integer;
            this.GMapTracking.SelectedAreaFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(65)))), ((int)(((byte)(105)))), ((int)(((byte)(225)))));
            this.GMapTracking.ShowTileGridLines = false;
            this.GMapTracking.Size = new System.Drawing.Size(569, 697);
            this.GMapTracking.TabIndex = 1;
            this.GMapTracking.Zoom = 13D;
            // 
            // SettingPage
            // 
            this.SettingPage.Controls.Add(this.button1);
            this.SettingPage.Controls.Add(this.txtname);
            this.SettingPage.Controls.Add(this.label9);
            this.SettingPage.Controls.Add(this.txtlicense);
            this.SettingPage.Controls.Add(this.label8);
            this.SettingPage.Controls.Add(this.label7);
            this.SettingPage.Controls.Add(this.cbbvehicle_setting);
            this.SettingPage.Controls.Add(this.label6);
            this.SettingPage.Controls.Add(this.label5);
            this.SettingPage.Controls.Add(this.label4);
            this.SettingPage.Controls.Add(this.txtnewpassconfirm);
            this.SettingPage.Controls.Add(this.txtnewpass);
            this.SettingPage.Controls.Add(this.txtpass);
            this.SettingPage.ImageIndex = 2;
            this.SettingPage.Location = new System.Drawing.Point(124, 4);
            this.SettingPage.Name = "SettingPage";
            this.SettingPage.Size = new System.Drawing.Size(935, 711);
            this.SettingPage.TabIndex = 2;
            this.SettingPage.Text = "Setting";
            this.SettingPage.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(340, 368);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(135, 39);
            this.button1.TabIndex = 12;
            this.button1.Text = "Change";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // txtname
            // 
            this.txtname.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtname.Location = new System.Drawing.Point(349, 308);
            this.txtname.Name = "txtname";
            this.txtname.Size = new System.Drawing.Size(382, 32);
            this.txtname.TabIndex = 11;
            this.txtname.TextChanged += new System.EventHandler(this.txtname_TextChanged);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(25, 311);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(71, 26);
            this.label9.TabIndex = 10;
            this.label9.Text = "Name";
            // 
            // txtlicense
            // 
            this.txtlicense.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtlicense.Location = new System.Drawing.Point(349, 249);
            this.txtlicense.Name = "txtlicense";
            this.txtlicense.Size = new System.Drawing.Size(382, 32);
            this.txtlicense.TabIndex = 9;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(25, 252);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(167, 26);
            this.label8.TabIndex = 8;
            this.label8.Text = "License number";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(25, 202);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(84, 26);
            this.label7.TabIndex = 7;
            this.label7.Text = "Vehicle";
            // 
            // cbbvehicle_setting
            // 
            this.cbbvehicle_setting.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbbvehicle_setting.FormattingEnabled = true;
            this.cbbvehicle_setting.Location = new System.Drawing.Point(349, 199);
            this.cbbvehicle_setting.Name = "cbbvehicle_setting";
            this.cbbvehicle_setting.Size = new System.Drawing.Size(382, 34);
            this.cbbvehicle_setting.TabIndex = 6;
            this.cbbvehicle_setting.SelectedIndexChanged += new System.EventHandler(this.cbbvehicle_setting_SelectedIndexChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(25, 143);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(280, 26);
            this.label6.TabIndex = 5;
            this.label6.Text = "New password confirmation";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(25, 90);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(155, 26);
            this.label5.TabIndex = 4;
            this.label5.Text = "New password";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(25, 43);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(108, 26);
            this.label4.TabIndex = 3;
            this.label4.Text = "Password";
            // 
            // txtnewpassconfirm
            // 
            this.txtnewpassconfirm.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtnewpassconfirm.Location = new System.Drawing.Point(349, 140);
            this.txtnewpassconfirm.Name = "txtnewpassconfirm";
            this.txtnewpassconfirm.PasswordChar = '*';
            this.txtnewpassconfirm.Size = new System.Drawing.Size(382, 32);
            this.txtnewpassconfirm.TabIndex = 2;
            // 
            // txtnewpass
            // 
            this.txtnewpass.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtnewpass.Location = new System.Drawing.Point(349, 90);
            this.txtnewpass.Name = "txtnewpass";
            this.txtnewpass.PasswordChar = '*';
            this.txtnewpass.Size = new System.Drawing.Size(382, 32);
            this.txtnewpass.TabIndex = 1;
            // 
            // txtpass
            // 
            this.txtpass.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtpass.Location = new System.Drawing.Point(349, 43);
            this.txtpass.Name = "txtpass";
            this.txtpass.PasswordChar = '*';
            this.txtpass.Size = new System.Drawing.Size(382, 32);
            this.txtpass.TabIndex = 0;
            // 
            // imglIcon
            // 
            this.imglIcon.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imglIcon.ImageStream")));
            this.imglIcon.TransparentColor = System.Drawing.Color.Transparent;
            this.imglIcon.Images.SetKeyName(0, "pin.png");
            this.imglIcon.Images.SetKeyName(1, "track.png");
            this.imglIcon.Images.SetKeyName(2, "support.png");
            // 
            // timernow
            // 
            this.timernow.Interval = 5000;
            this.timernow.Tick += new System.EventHandler(this.timernow_Tick);
            // 
            // timertracking
            // 
            this.timertracking.Interval = 500;
            this.timertracking.Tick += new System.EventHandler(this.timertracking_Tick);
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1067, 718);
            this.Controls.Add(this.tabControl1);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Main";
            this.Text = "The Ultimate Tracking";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.tabControl1.ResumeLayout(false);
            this.HomePage.ResumeLayout(false);
            this.HomePage.PerformLayout();
            this.TrackingPage.ResumeLayout(false);
            this.TrackingPage.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.objtracking)).EndInit();
            this.SettingPage.ResumeLayout(false);
            this.SettingPage.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage HomePage;
        private System.Windows.Forms.TabPage TrackingPage;
        private System.Windows.Forms.TabPage SettingPage;
        private System.Windows.Forms.ImageList imglIcon;
        private GMap.NET.WindowsForms.GMapControl gmap;
        private System.Windows.Forms.Timer timernow;
        private GMap.NET.WindowsForms.GMapControl GMapTracking;
        private System.Windows.Forms.ComboBox cbbtracking;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker dateTimetracking;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btncarrun;
        private System.Windows.Forms.Button btnshowpath;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cbbcar;
        private System.Windows.Forms.Button btnFind;
        private BrightIdeasSoftware.ObjectListView objtracking;
        private BrightIdeasSoftware.OLVColumn TimeColumn;
        private BrightIdeasSoftware.OLVColumn AddressColumn;
        private BrightIdeasSoftware.OLVColumn SpeedColumn;
        private System.Windows.Forms.Timer timertracking;
        private System.Windows.Forms.Button btnclear;
        private System.Windows.Forms.TextBox txtnewpassconfirm;
        private System.Windows.Forms.TextBox txtnewpass;
        private System.Windows.Forms.TextBox txtpass;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox txtname;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtlicense;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox cbbvehicle_setting;
        private System.Windows.Forms.Label label6;
    }
}

