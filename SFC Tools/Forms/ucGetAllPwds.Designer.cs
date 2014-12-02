namespace SFC_Tools.Forms
{
    partial class ucGetAllPwds
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ucGetAllPwds));
            this.label1 = new System.Windows.Forms.Label();
            this.btnGet = new System.Windows.Forms.Button();
            this.dgvUserInfo = new System.Windows.Forms.DataGridView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblCount = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtName = new System.Windows.Forms.TextBox();
            this.rbtnPrd = new System.Windows.Forms.RadioButton();
            this.rbtnSit = new System.Windows.Forms.RadioButton();
            this.rbtnDev = new System.Windows.Forms.RadioButton();
            this.pgbProgress = new System.Windows.Forms.ProgressBar();
            this.Username = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.UserPassword = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.RealPwd = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Realname = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgvUserInfo)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.LightSteelBlue;
            this.label1.Font = new System.Drawing.Font("DFKai-SB", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label1.ForeColor = System.Drawing.Color.Blue;
            this.label1.Image = ((System.Drawing.Image)(resources.GetObject("label1.Image")));
            this.label1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label1.Location = new System.Drawing.Point(12, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(727, 52);
            this.label1.TabIndex = 9;
            this.label1.Text = "Get All Pwd From DB";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnGet
            // 
            this.btnGet.Location = new System.Drawing.Point(556, 48);
            this.btnGet.Name = "btnGet";
            this.btnGet.Size = new System.Drawing.Size(155, 31);
            this.btnGet.TabIndex = 10;
            this.btnGet.Text = "Get";
            this.btnGet.UseVisualStyleBackColor = true;
            this.btnGet.Click += new System.EventHandler(this.btnGet_Click);
            // 
            // dgvUserInfo
            // 
            this.dgvUserInfo.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvUserInfo.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Username,
            this.UserPassword,
            this.RealPwd,
            this.Realname});
            this.dgvUserInfo.Location = new System.Drawing.Point(11, 72);
            this.dgvUserInfo.Name = "dgvUserInfo";
            this.dgvUserInfo.RowTemplate.Height = 23;
            this.dgvUserInfo.Size = new System.Drawing.Size(727, 324);
            this.dgvUserInfo.TabIndex = 11;
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.lblCount);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.txtName);
            this.panel1.Controls.Add(this.rbtnPrd);
            this.panel1.Controls.Add(this.rbtnSit);
            this.panel1.Controls.Add(this.rbtnDev);
            this.panel1.Controls.Add(this.btnGet);
            this.panel1.Location = new System.Drawing.Point(11, 405);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(727, 94);
            this.panel1.TabIndex = 12;
            // 
            // lblCount
            // 
            this.lblCount.AutoSize = true;
            this.lblCount.Location = new System.Drawing.Point(613, 15);
            this.lblCount.Name = "lblCount";
            this.lblCount.Size = new System.Drawing.Size(11, 12);
            this.lblCount.TabIndex = 16;
            this.lblCount.Text = "0";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(554, 15);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 12);
            this.label3.TabIndex = 16;
            this.label3.Text = "Count:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(127, 40);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 12);
            this.label2.TabIndex = 15;
            this.label2.Text = "Name:";
            // 
            // txtName
            // 
            this.txtName.Enabled = false;
            this.txtName.Location = new System.Drawing.Point(193, 36);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(221, 21);
            this.txtName.TabIndex = 14;
            this.txtName.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtName_KeyPress);
            // 
            // rbtnPrd
            // 
            this.rbtnPrd.AutoSize = true;
            this.rbtnPrd.Location = new System.Drawing.Point(19, 63);
            this.rbtnPrd.Name = "rbtnPrd";
            this.rbtnPrd.Size = new System.Drawing.Size(41, 16);
            this.rbtnPrd.TabIndex = 13;
            this.rbtnPrd.Text = "PRD";
            this.rbtnPrd.UseVisualStyleBackColor = true;
            // 
            // rbtnSit
            // 
            this.rbtnSit.AutoSize = true;
            this.rbtnSit.Location = new System.Drawing.Point(19, 37);
            this.rbtnSit.Name = "rbtnSit";
            this.rbtnSit.Size = new System.Drawing.Size(41, 16);
            this.rbtnSit.TabIndex = 12;
            this.rbtnSit.Text = "SIT";
            this.rbtnSit.UseVisualStyleBackColor = true;
            // 
            // rbtnDev
            // 
            this.rbtnDev.AutoSize = true;
            this.rbtnDev.Checked = true;
            this.rbtnDev.Location = new System.Drawing.Point(19, 15);
            this.rbtnDev.Name = "rbtnDev";
            this.rbtnDev.Size = new System.Drawing.Size(41, 16);
            this.rbtnDev.TabIndex = 11;
            this.rbtnDev.TabStop = true;
            this.rbtnDev.Text = "Dev";
            this.rbtnDev.UseVisualStyleBackColor = true;
            // 
            // pgbProgress
            // 
            this.pgbProgress.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pgbProgress.Location = new System.Drawing.Point(0, 505);
            this.pgbProgress.Name = "pgbProgress";
            this.pgbProgress.Size = new System.Drawing.Size(751, 15);
            this.pgbProgress.TabIndex = 13;
            // 
            // Username
            // 
            this.Username.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.Username.DataPropertyName = "Username";
            this.Username.HeaderText = "Username";
            this.Username.Name = "Username";
            // 
            // UserPassword
            // 
            this.UserPassword.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.UserPassword.DataPropertyName = "UserPassword";
            this.UserPassword.HeaderText = "UserPassword";
            this.UserPassword.Name = "UserPassword";
            this.UserPassword.Width = 250;
            // 
            // RealPwd
            // 
            this.RealPwd.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.RealPwd.DataPropertyName = "RealPwd";
            this.RealPwd.HeaderText = "RealPwd";
            this.RealPwd.Name = "RealPwd";
            this.RealPwd.Width = 200;
            // 
            // Realname
            // 
            this.Realname.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.Realname.DataPropertyName = "Realname";
            this.Realname.HeaderText = "Realname";
            this.Realname.Name = "Realname";
            this.Realname.Width = 120;
            // 
            // ucGetAllPwds
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pgbProgress);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.dgvUserInfo);
            this.Controls.Add(this.label1);
            this.Name = "ucGetAllPwds";
            this.Size = new System.Drawing.Size(751, 520);
            ((System.ComponentModel.ISupportInitialize)(this.dgvUserInfo)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnGet;
        private System.Windows.Forms.DataGridView dgvUserInfo;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.RadioButton rbtnPrd;
        private System.Windows.Forms.RadioButton rbtnSit;
        private System.Windows.Forms.RadioButton rbtnDev;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Label lblCount;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ProgressBar pgbProgress;
        private System.Windows.Forms.DataGridViewTextBoxColumn Username;
        private System.Windows.Forms.DataGridViewTextBoxColumn UserPassword;
        private System.Windows.Forms.DataGridViewTextBoxColumn RealPwd;
        private System.Windows.Forms.DataGridViewTextBoxColumn Realname;
    }
}
