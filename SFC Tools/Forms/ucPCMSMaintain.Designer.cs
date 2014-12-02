namespace SFC_Tools.Forms
{
    partial class ucPCMSMaintain
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ucPCMSMaintain));
            this.panel2 = new System.Windows.Forms.Panel();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.txtMySqlDb = new System.Windows.Forms.TextBox();
            this.btnConnect = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.btnDisConn = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.imgIcons = new System.Windows.Forms.ImageList(this.components);
            this.pnlTop = new System.Windows.Forms.Panel();
            this.pnlLeftCov = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.pnlRightCov = new System.Windows.Forms.Panel();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnInsert = new System.Windows.Forms.Button();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.txtSortNo = new System.Windows.Forms.TextBox();
            this.txtModuleDesc = new System.Windows.Forms.TextBox();
            this.txtFilePath = new System.Windows.Forms.TextBox();
            this.txtModuleName = new System.Windows.Forms.TextBox();
            this.chkIsFolder = new System.Windows.Forms.CheckBox();
            this.chkEnable = new System.Windows.Forms.CheckBox();
            this.lbFilePath = new System.Windows.Forms.Label();
            this.lbModuleDescription = new System.Windows.Forms.Label();
            this.lbSortNo = new System.Windows.Forms.Label();
            this.lbModuleName = new System.Windows.Forms.Label();
            this.txtModuleID = new System.Windows.Forms.TextBox();
            this.lbModuleID = new System.Windows.Forms.Label();
            this.trvMenSec = new System.Windows.Forms.TreeView();
            this.panel2.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.pnlTop.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.groupBox2);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 457);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(746, 66);
            this.panel2.TabIndex = 12;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.txtMySqlDb);
            this.groupBox2.Controls.Add(this.btnConnect);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.btnDisConn);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(0, 0);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(746, 66);
            this.groupBox2.TabIndex = 13;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "MySql";
            // 
            // txtMySqlDb
            // 
            this.txtMySqlDb.Location = new System.Drawing.Point(89, 23);
            this.txtMySqlDb.Name = "txtMySqlDb";
            this.txtMySqlDb.Size = new System.Drawing.Size(197, 21);
            this.txtMySqlDb.TabIndex = 8;
            this.txtMySqlDb.Text = "MyEasyWeb";
            // 
            // btnConnect
            // 
            this.btnConnect.Font = new System.Drawing.Font("Microsoft JhengHei", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.btnConnect.Location = new System.Drawing.Point(355, 19);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(134, 29);
            this.btnConnect.TabIndex = 10;
            this.btnConnect.Text = "Connect";
            this.btnConnect.UseVisualStyleBackColor = true;
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 28);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(59, 12);
            this.label6.TabIndex = 7;
            this.label6.Text = "DataBase:";
            // 
            // btnDisConn
            // 
            this.btnDisConn.Enabled = false;
            this.btnDisConn.Font = new System.Drawing.Font("Microsoft JhengHei", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.btnDisConn.Location = new System.Drawing.Point(548, 19);
            this.btnDisConn.Name = "btnDisConn";
            this.btnDisConn.Size = new System.Drawing.Size(134, 29);
            this.btnDisConn.TabIndex = 11;
            this.btnDisConn.Text = "DisConn";
            this.btnDisConn.UseVisualStyleBackColor = true;
            this.btnDisConn.Click += new System.EventHandler(this.btnGenerate_Click);
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // imgIcons
            // 
            this.imgIcons.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imgIcons.ImageStream")));
            this.imgIcons.TransparentColor = System.Drawing.Color.Transparent;
            this.imgIcons.Images.SetKeyName(0, "folder_cool.ico");
            this.imgIcons.Images.SetKeyName(1, "kaddressbook.ico");
            this.imgIcons.Images.SetKeyName(2, "evolution2.ico");
            this.imgIcons.Images.SetKeyName(3, "evolution.ico");
            // 
            // pnlTop
            // 
            this.pnlTop.Controls.Add(this.pnlLeftCov);
            this.pnlTop.Controls.Add(this.panel1);
            this.pnlTop.Controls.Add(this.trvMenSec);
            this.pnlTop.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlTop.Location = new System.Drawing.Point(0, 0);
            this.pnlTop.Name = "pnlTop";
            this.pnlTop.Size = new System.Drawing.Size(746, 457);
            this.pnlTop.TabIndex = 17;
            // 
            // pnlLeftCov
            // 
            this.pnlLeftCov.BackgroundImage = global::SFC_Tools.Properties.Resources.a;
            this.pnlLeftCov.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnlLeftCov.Location = new System.Drawing.Point(8, 426);
            this.pnlLeftCov.Name = "pnlLeftCov";
            this.pnlLeftCov.Size = new System.Drawing.Size(375, 457);
            this.pnlLeftCov.TabIndex = 20;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.pnlRightCov);
            this.panel1.Controls.Add(this.btnDelete);
            this.panel1.Controls.Add(this.btnInsert);
            this.panel1.Controls.Add(this.btnUpdate);
            this.panel1.Controls.Add(this.txtSortNo);
            this.panel1.Controls.Add(this.txtModuleDesc);
            this.panel1.Controls.Add(this.txtFilePath);
            this.panel1.Controls.Add(this.txtModuleName);
            this.panel1.Controls.Add(this.chkIsFolder);
            this.panel1.Controls.Add(this.chkEnable);
            this.panel1.Controls.Add(this.lbFilePath);
            this.panel1.Controls.Add(this.lbModuleDescription);
            this.panel1.Controls.Add(this.lbSortNo);
            this.panel1.Controls.Add(this.lbModuleName);
            this.panel1.Controls.Add(this.txtModuleID);
            this.panel1.Controls.Add(this.lbModuleID);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(297, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(449, 457);
            this.panel1.TabIndex = 19;
            // 
            // pnlRightCov
            // 
            this.pnlRightCov.BackgroundImage = global::SFC_Tools.Properties.Resources.b;
            this.pnlRightCov.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnlRightCov.Location = new System.Drawing.Point(179, 426);
            this.pnlRightCov.Name = "pnlRightCov";
            this.pnlRightCov.Size = new System.Drawing.Size(380, 457);
            this.pnlRightCov.TabIndex = 28;
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(290, 378);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(91, 30);
            this.btnDelete.TabIndex = 24;
            this.btnDelete.Text = "Delete";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnInsert
            // 
            this.btnInsert.Location = new System.Drawing.Point(179, 378);
            this.btnInsert.Name = "btnInsert";
            this.btnInsert.Size = new System.Drawing.Size(91, 30);
            this.btnInsert.TabIndex = 23;
            this.btnInsert.Text = "Insert";
            this.btnInsert.UseVisualStyleBackColor = true;
            this.btnInsert.Click += new System.EventHandler(this.btnInsert_Click);
            // 
            // btnUpdate
            // 
            this.btnUpdate.Location = new System.Drawing.Point(68, 378);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(91, 30);
            this.btnUpdate.TabIndex = 22;
            this.btnUpdate.Text = "Update";
            this.btnUpdate.UseVisualStyleBackColor = true;
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // txtSortNo
            // 
            this.txtSortNo.Location = new System.Drawing.Point(196, 277);
            this.txtSortNo.Name = "txtSortNo";
            this.txtSortNo.Size = new System.Drawing.Size(186, 21);
            this.txtSortNo.TabIndex = 11;
            // 
            // txtModuleDesc
            // 
            this.txtModuleDesc.Location = new System.Drawing.Point(196, 227);
            this.txtModuleDesc.Name = "txtModuleDesc";
            this.txtModuleDesc.Size = new System.Drawing.Size(186, 21);
            this.txtModuleDesc.TabIndex = 10;
            // 
            // txtFilePath
            // 
            this.txtFilePath.Location = new System.Drawing.Point(68, 156);
            this.txtFilePath.Multiline = true;
            this.txtFilePath.Name = "txtFilePath";
            this.txtFilePath.Size = new System.Drawing.Size(314, 43);
            this.txtFilePath.TabIndex = 9;
            // 
            // txtModuleName
            // 
            this.txtModuleName.Location = new System.Drawing.Point(196, 81);
            this.txtModuleName.Name = "txtModuleName";
            this.txtModuleName.Size = new System.Drawing.Size(186, 21);
            this.txtModuleName.TabIndex = 8;
            // 
            // chkIsFolder
            // 
            this.chkIsFolder.AutoSize = true;
            this.chkIsFolder.Location = new System.Drawing.Point(310, 322);
            this.chkIsFolder.Name = "chkIsFolder";
            this.chkIsFolder.Size = new System.Drawing.Size(72, 16);
            this.chkIsFolder.TabIndex = 7;
            this.chkIsFolder.Text = "IsFolder";
            this.chkIsFolder.UseVisualStyleBackColor = true;
            // 
            // chkEnable
            // 
            this.chkEnable.AutoSize = true;
            this.chkEnable.Location = new System.Drawing.Point(66, 322);
            this.chkEnable.Name = "chkEnable";
            this.chkEnable.Size = new System.Drawing.Size(60, 16);
            this.chkEnable.TabIndex = 6;
            this.chkEnable.Text = "Enable";
            this.chkEnable.UseVisualStyleBackColor = true;
            // 
            // lbFilePath
            // 
            this.lbFilePath.AutoSize = true;
            this.lbFilePath.Location = new System.Drawing.Point(66, 132);
            this.lbFilePath.Name = "lbFilePath";
            this.lbFilePath.Size = new System.Drawing.Size(59, 12);
            this.lbFilePath.TabIndex = 5;
            this.lbFilePath.Text = "FilePath:";
            // 
            // lbModuleDescription
            // 
            this.lbModuleDescription.AutoSize = true;
            this.lbModuleDescription.Location = new System.Drawing.Point(66, 232);
            this.lbModuleDescription.Name = "lbModuleDescription";
            this.lbModuleDescription.Size = new System.Drawing.Size(113, 12);
            this.lbModuleDescription.TabIndex = 4;
            this.lbModuleDescription.Text = "ModuleDescription:";
            // 
            // lbSortNo
            // 
            this.lbSortNo.AutoSize = true;
            this.lbSortNo.Location = new System.Drawing.Point(66, 277);
            this.lbSortNo.Name = "lbSortNo";
            this.lbSortNo.Size = new System.Drawing.Size(47, 12);
            this.lbSortNo.TabIndex = 3;
            this.lbSortNo.Text = "SortNo:";
            // 
            // lbModuleName
            // 
            this.lbModuleName.AutoSize = true;
            this.lbModuleName.Location = new System.Drawing.Point(66, 87);
            this.lbModuleName.Name = "lbModuleName";
            this.lbModuleName.Size = new System.Drawing.Size(71, 12);
            this.lbModuleName.TabIndex = 2;
            this.lbModuleName.Text = "ModuleName:";
            // 
            // txtModuleID
            // 
            this.txtModuleID.Location = new System.Drawing.Point(196, 34);
            this.txtModuleID.Name = "txtModuleID";
            this.txtModuleID.Size = new System.Drawing.Size(186, 21);
            this.txtModuleID.TabIndex = 1;
            // 
            // lbModuleID
            // 
            this.lbModuleID.AutoSize = true;
            this.lbModuleID.Location = new System.Drawing.Point(66, 42);
            this.lbModuleID.Name = "lbModuleID";
            this.lbModuleID.Size = new System.Drawing.Size(59, 12);
            this.lbModuleID.TabIndex = 0;
            this.lbModuleID.Text = "ModuleID:";
            // 
            // trvMenSec
            // 
            this.trvMenSec.Dock = System.Windows.Forms.DockStyle.Left;
            this.trvMenSec.ImageIndex = 0;
            this.trvMenSec.ImageList = this.imgIcons;
            this.trvMenSec.Location = new System.Drawing.Point(0, 0);
            this.trvMenSec.Name = "trvMenSec";
            this.trvMenSec.SelectedImageIndex = 0;
            this.trvMenSec.Size = new System.Drawing.Size(297, 457);
            this.trvMenSec.TabIndex = 17;
            this.trvMenSec.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.trvMenSec_AfterSelect);
            // 
            // ucPCMSMaintain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.Controls.Add(this.pnlTop);
            this.Controls.Add(this.panel2);
            this.Name = "ucPCMSMaintain";
            this.Size = new System.Drawing.Size(746, 523);
            this.Load += new System.EventHandler(this.ucPCMSMaintain_Load);
            this.panel2.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.pnlTop.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox txtMySqlDb;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button btnConnect;
        private System.Windows.Forms.Button btnDisConn;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.ImageList imgIcons;
        private System.Windows.Forms.Panel pnlTop;
        private System.Windows.Forms.TreeView trvMenSec;
        private System.Windows.Forms.Panel pnlLeftCov;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox txtSortNo;
        private System.Windows.Forms.TextBox txtModuleDesc;
        private System.Windows.Forms.TextBox txtFilePath;
        private System.Windows.Forms.TextBox txtModuleName;
        private System.Windows.Forms.CheckBox chkIsFolder;
        private System.Windows.Forms.CheckBox chkEnable;
        private System.Windows.Forms.Label lbFilePath;
        private System.Windows.Forms.Label lbModuleDescription;
        private System.Windows.Forms.Label lbSortNo;
        private System.Windows.Forms.Label lbModuleName;
        private System.Windows.Forms.TextBox txtModuleID;
        private System.Windows.Forms.Label lbModuleID;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnInsert;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.Panel pnlRightCov;
    }
}
