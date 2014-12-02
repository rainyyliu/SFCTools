namespace SFC_Tools.Forms
{
    partial class ucNewRoute
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ucNewRoute));
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.button1 = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.pnlTest = new System.Windows.Forms.Panel();
            this.pbMain = new System.Windows.Forms.PictureBox();
            this.pnlDel = new System.Windows.Forms.Panel();
            this.lblPoint = new System.Windows.Forms.Label();
            this.cbbRoute = new System.Windows.Forms.ComboBox();
            this.chkGrid = new System.Windows.Forms.CheckBox();
            this.pnlRepair = new System.Windows.Forms.Panel();
            this.pnlComm = new System.Windows.Forms.Panel();
            this.rdoJumStation = new System.Windows.Forms.RadioButton();
            this.rdoDelStation = new System.Windows.Forms.RadioButton();
            this.rdoCancelJump = new System.Windows.Forms.RadioButton();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbMain)).BeginInit();
            this.SuspendLayout();
            // 
            // treeView1
            // 
            this.treeView1.Location = new System.Drawing.Point(3, 124);
            this.treeView1.Name = "treeView1";
            this.treeView1.Size = new System.Drawing.Size(578, 377);
            this.treeView1.TabIndex = 3;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(576, 22);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(130, 38);
            this.button1.TabIndex = 4;
            this.button1.Text = "确定";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // panel1
            // 
            this.panel1.AutoScroll = true;
            this.panel1.Controls.Add(this.pnlTest);
            this.panel1.Controls.Add(this.pbMain);
            this.panel1.Controls.Add(this.pnlDel);
            this.panel1.Location = new System.Drawing.Point(3, 112);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(720, 402);
            this.panel1.TabIndex = 6;
            this.panel1.Scroll += new System.Windows.Forms.ScrollEventHandler(this.panel1_Scroll);
            // 
            // pnlTest
            // 
            this.pnlTest.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pnlTest.BackgroundImage")));
            this.pnlTest.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.pnlTest.Location = new System.Drawing.Point(665, 51);
            this.pnlTest.Name = "pnlTest";
            this.pnlTest.Size = new System.Drawing.Size(38, 37);
            this.pnlTest.TabIndex = 14;
            this.pnlTest.MouseClick += new System.Windows.Forms.MouseEventHandler(this.pnlTest_MouseClick);
            this.pnlTest.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pnlTest_MouseDown);
            this.pnlTest.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pnlTest_MouseMove);
            this.pnlTest.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pnlTest_MouseUp);
            // 
            // pbMain
            // 
            this.pbMain.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.pbMain.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pbMain.Location = new System.Drawing.Point(9, 39);
            this.pbMain.Name = "pbMain";
            this.pbMain.Size = new System.Drawing.Size(700, 350);
            this.pbMain.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pbMain.TabIndex = 7;
            this.pbMain.TabStop = false;
            this.pbMain.Click += new System.EventHandler(this.pbMain_Click);
            this.pbMain.Paint += new System.Windows.Forms.PaintEventHandler(this.pbMain_Paint);
            this.pbMain.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pbMain_MouseDown);
            this.pbMain.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pbMain_MouseMove);
            this.pbMain.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pbMain_MouseUp);
            // 
            // pnlDel
            // 
            this.pnlDel.BackColor = System.Drawing.Color.Transparent;
            this.pnlDel.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pnlDel.BackgroundImage")));
            this.pnlDel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pnlDel.Location = new System.Drawing.Point(3, 3);
            this.pnlDel.Name = "pnlDel";
            this.pnlDel.Size = new System.Drawing.Size(38, 37);
            this.pnlDel.TabIndex = 13;
            // 
            // lblPoint
            // 
            this.lblPoint.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblPoint.Location = new System.Drawing.Point(308, 63);
            this.lblPoint.Name = "lblPoint";
            this.lblPoint.Size = new System.Drawing.Size(242, 43);
            this.lblPoint.TabIndex = 7;
            // 
            // cbbRoute
            // 
            this.cbbRoute.FormattingEnabled = true;
            this.cbbRoute.Location = new System.Drawing.Point(308, 22);
            this.cbbRoute.Name = "cbbRoute";
            this.cbbRoute.Size = new System.Drawing.Size(242, 20);
            this.cbbRoute.TabIndex = 8;
            this.cbbRoute.SelectedIndexChanged += new System.EventHandler(this.cbbRoute_SelectedIndexChanged);
            // 
            // chkGrid
            // 
            this.chkGrid.AutoSize = true;
            this.chkGrid.Location = new System.Drawing.Point(576, 80);
            this.chkGrid.Name = "chkGrid";
            this.chkGrid.Size = new System.Drawing.Size(48, 16);
            this.chkGrid.TabIndex = 10;
            this.chkGrid.Text = "Grid";
            this.chkGrid.UseVisualStyleBackColor = true;
            this.chkGrid.CheckedChanged += new System.EventHandler(this.chkGrid_CheckedChanged);
            // 
            // pnlRepair
            // 
            this.pnlRepair.BackgroundImage = global::SFC_Tools.Properties.Resources.Oth014;
            this.pnlRepair.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.pnlRepair.Location = new System.Drawing.Point(135, 35);
            this.pnlRepair.Name = "pnlRepair";
            this.pnlRepair.Size = new System.Drawing.Size(38, 37);
            this.pnlRepair.TabIndex = 13;
            this.pnlRepair.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pnlComm_MouseDown);
            this.pnlRepair.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pnlComm_MouseMove);
            this.pnlRepair.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pnlRepair_MouseUp);
            // 
            // pnlComm
            // 
            this.pnlComm.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pnlComm.BackgroundImage")));
            this.pnlComm.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.pnlComm.Location = new System.Drawing.Point(179, 35);
            this.pnlComm.Name = "pnlComm";
            this.pnlComm.Size = new System.Drawing.Size(38, 37);
            this.pnlComm.TabIndex = 9;
            this.pnlComm.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pnlComm_MouseDown);
            this.pnlComm.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pnlComm_MouseMove);
            this.pnlComm.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pnlComm_MouseUp);
            // 
            // rdoJumStation
            // 
            this.rdoJumStation.AutoSize = true;
            this.rdoJumStation.Checked = true;
            this.rdoJumStation.Location = new System.Drawing.Point(26, 12);
            this.rdoJumStation.Name = "rdoJumStation";
            this.rdoJumStation.Size = new System.Drawing.Size(47, 16);
            this.rdoJumStation.TabIndex = 14;
            this.rdoJumStation.TabStop = true;
            this.rdoJumStation.Text = "连线";
            this.rdoJumStation.UseVisualStyleBackColor = true;
            // 
            // rdoDelStation
            // 
            this.rdoDelStation.AutoSize = true;
            this.rdoDelStation.Location = new System.Drawing.Point(26, 80);
            this.rdoDelStation.Name = "rdoDelStation";
            this.rdoDelStation.Size = new System.Drawing.Size(71, 16);
            this.rdoDelStation.TabIndex = 14;
            this.rdoDelStation.TabStop = true;
            this.rdoDelStation.Text = "删除工站";
            this.rdoDelStation.UseVisualStyleBackColor = true;
            // 
            // rdoCancelJump
            // 
            this.rdoCancelJump.AutoSize = true;
            this.rdoCancelJump.Location = new System.Drawing.Point(26, 46);
            this.rdoCancelJump.Name = "rdoCancelJump";
            this.rdoCancelJump.Size = new System.Drawing.Size(71, 16);
            this.rdoCancelJump.TabIndex = 14;
            this.rdoCancelJump.TabStop = true;
            this.rdoCancelJump.Text = "取消连线";
            this.rdoCancelJump.UseVisualStyleBackColor = true;
            // 
            // ucNewRoute
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.rdoCancelJump);
            this.Controls.Add(this.rdoDelStation);
            this.Controls.Add(this.rdoJumStation);
            this.Controls.Add(this.pnlRepair);
            this.Controls.Add(this.chkGrid);
            this.Controls.Add(this.pnlComm);
            this.Controls.Add(this.cbbRoute);
            this.Controls.Add(this.lblPoint);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.treeView1);
            this.Name = "ucNewRoute";
            this.Size = new System.Drawing.Size(740, 517);
            this.Load += new System.EventHandler(this.ucNewRoute_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbMain)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TreeView treeView1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Panel panel1;
        public System.Windows.Forms.PictureBox pbMain;
        private System.Windows.Forms.Label lblPoint;
        private System.Windows.Forms.ComboBox cbbRoute;
        private System.Windows.Forms.Panel pnlComm;
        private System.Windows.Forms.CheckBox chkGrid;
        private System.Windows.Forms.Panel pnlDel;
        private System.Windows.Forms.Panel pnlTest;
        private System.Windows.Forms.Panel pnlRepair;
        private System.Windows.Forms.RadioButton rdoJumStation;
        private System.Windows.Forms.RadioButton rdoDelStation;
        private System.Windows.Forms.RadioButton rdoCancelJump;

    }
}
