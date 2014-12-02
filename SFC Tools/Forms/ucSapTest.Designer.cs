namespace SFC_Tools.Forms
{
    partial class ucSapTest
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ucSapTest));
            this.label1 = new System.Windows.Forms.Label();
            this.pnlMain = new System.Windows.Forms.Panel();
            this.txtLan = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtPwd = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtUser = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtClient = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtSysNo = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtServerIP = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnGo = new System.Windows.Forms.Button();
            this.btnGoProxy = new System.Windows.Forms.Button();
            this.pnlMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.BackColor = System.Drawing.Color.LightSteelBlue;
            this.label1.Font = new System.Drawing.Font("DFKai-SB", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label1.ForeColor = System.Drawing.Color.Blue;
            this.label1.Image = ((System.Drawing.Image)(resources.GetObject("label1.Image")));
            this.label1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label1.Location = new System.Drawing.Point(14, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(703, 52);
            this.label1.TabIndex = 5;
            this.label1.Text = "SAP RFC TEST";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pnlMain
            // 
            this.pnlMain.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlMain.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlMain.Controls.Add(this.txtLan);
            this.pnlMain.Controls.Add(this.label7);
            this.pnlMain.Controls.Add(this.txtPwd);
            this.pnlMain.Controls.Add(this.label6);
            this.pnlMain.Controls.Add(this.txtUser);
            this.pnlMain.Controls.Add(this.label5);
            this.pnlMain.Controls.Add(this.txtClient);
            this.pnlMain.Controls.Add(this.label4);
            this.pnlMain.Controls.Add(this.txtSysNo);
            this.pnlMain.Controls.Add(this.label3);
            this.pnlMain.Controls.Add(this.txtServerIP);
            this.pnlMain.Controls.Add(this.label2);
            this.pnlMain.Location = new System.Drawing.Point(16, 83);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Size = new System.Drawing.Size(700, 351);
            this.pnlMain.TabIndex = 6;
            // 
            // txtLan
            // 
            this.txtLan.Location = new System.Drawing.Point(409, 41);
            this.txtLan.Name = "txtLan";
            this.txtLan.Size = new System.Drawing.Size(193, 21);
            this.txtLan.TabIndex = 11;
            this.txtLan.Text = "EN";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(332, 41);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(59, 12);
            this.label7.TabIndex = 10;
            this.label7.Text = "Language:";
            // 
            // txtPwd
            // 
            this.txtPwd.Location = new System.Drawing.Point(409, 137);
            this.txtPwd.Name = "txtPwd";
            this.txtPwd.Size = new System.Drawing.Size(193, 21);
            this.txtPwd.TabIndex = 9;
            this.txtPwd.Text = "l1006";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(332, 137);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(59, 12);
            this.label6.TabIndex = 8;
            this.label6.Text = "PassWord:";
            // 
            // txtUser
            // 
            this.txtUser.Location = new System.Drawing.Point(409, 89);
            this.txtUser.Name = "txtUser";
            this.txtUser.Size = new System.Drawing.Size(193, 21);
            this.txtUser.TabIndex = 7;
            this.txtUser.Text = "SAPSFCBR01";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(332, 92);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(35, 12);
            this.label5.TabIndex = 6;
            this.label5.Text = "User:";
            // 
            // txtClient
            // 
            this.txtClient.Location = new System.Drawing.Point(106, 141);
            this.txtClient.Name = "txtClient";
            this.txtClient.Size = new System.Drawing.Size(193, 21);
            this.txtClient.TabIndex = 5;
            this.txtClient.Text = "802";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(38, 144);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(47, 12);
            this.label4.TabIndex = 4;
            this.label4.Text = "Client:";
            // 
            // txtSysNo
            // 
            this.txtSysNo.Location = new System.Drawing.Point(106, 86);
            this.txtSysNo.Name = "txtSysNo";
            this.txtSysNo.Size = new System.Drawing.Size(193, 21);
            this.txtSysNo.TabIndex = 3;
            this.txtSysNo.Text = "00";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(38, 89);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(47, 12);
            this.label3.TabIndex = 2;
            this.label3.Text = "Sys No:";
            // 
            // txtServerIP
            // 
            this.txtServerIP.Location = new System.Drawing.Point(106, 38);
            this.txtServerIP.Name = "txtServerIP";
            this.txtServerIP.Size = new System.Drawing.Size(193, 21);
            this.txtServerIP.TabIndex = 1;
            this.txtServerIP.Text = "10.18.222.152";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(38, 41);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(47, 12);
            this.label2.TabIndex = 0;
            this.label2.Text = "Server:";
            // 
            // btnGo
            // 
            this.btnGo.Location = new System.Drawing.Point(301, 455);
            this.btnGo.Name = "btnGo";
            this.btnGo.Size = new System.Drawing.Size(138, 31);
            this.btnGo.TabIndex = 7;
            this.btnGo.Text = "Go";
            this.btnGo.UseVisualStyleBackColor = true;
            this.btnGo.Click += new System.EventHandler(this.btnGo_Click);
            // 
            // btnGoProxy
            // 
            this.btnGoProxy.Location = new System.Drawing.Point(578, 455);
            this.btnGoProxy.Name = "btnGoProxy";
            this.btnGoProxy.Size = new System.Drawing.Size(138, 31);
            this.btnGoProxy.TabIndex = 8;
            this.btnGoProxy.Text = "Go Proxy";
            this.btnGoProxy.UseVisualStyleBackColor = true;
            this.btnGoProxy.Click += new System.EventHandler(this.btnGoProxy_Click);
            // 
            // ucSapTest
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.Controls.Add(this.btnGoProxy);
            this.Controls.Add(this.btnGo);
            this.Controls.Add(this.pnlMain);
            this.Controls.Add(this.label1);
            this.Name = "ucSapTest";
            this.Size = new System.Drawing.Size(738, 515);
            this.pnlMain.ResumeLayout(false);
            this.pnlMain.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel pnlMain;
        private System.Windows.Forms.TextBox txtServerIP;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtSysNo;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtClient;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtPwd;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtUser;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnGo;
        private System.Windows.Forms.TextBox txtLan;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button btnGoProxy;
    }
}
