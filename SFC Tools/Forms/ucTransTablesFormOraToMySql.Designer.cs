namespace SFC_Tools.Forms
{
    partial class ucTransTablesFormOraToMySql
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ucTransTablesFormOraToMySql));
            this.panel1 = new System.Windows.Forms.Panel();
            this.pgbTrans = new System.Windows.Forms.ProgressBar();
            this.dgvTableInfo = new System.Windows.Forms.DataGridView();
            this.TABLE_NAME = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lblTitle = new System.Windows.Forms.Label();
            this.rtbGenerateLog = new System.Windows.Forms.RichTextBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.txtMySqlDb = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtPwd = new System.Windows.Forms.TextBox();
            this.txtSchema = new System.Windows.Forms.TextBox();
            this.txtDbName = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btnConnect = new System.Windows.Forms.Button();
            this.btnGenerate = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTableInfo)).BeginInit();
            this.panel2.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Controls.Add(this.rtbGenerateLog);
            this.panel1.Controls.Add(this.pgbTrans);
            this.panel1.Controls.Add(this.dgvTableInfo);
            this.panel1.Controls.Add(this.lblTitle);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(750, 527);
            this.panel1.TabIndex = 1;
            // 
            // pgbTrans
            // 
            this.pgbTrans.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pgbTrans.Location = new System.Drawing.Point(0, 502);
            this.pgbTrans.Name = "pgbTrans";
            this.pgbTrans.Size = new System.Drawing.Size(746, 21);
            this.pgbTrans.TabIndex = 8;
            // 
            // dgvTableInfo
            // 
            this.dgvTableInfo.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvTableInfo.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.TABLE_NAME});
            this.dgvTableInfo.Location = new System.Drawing.Point(15, 71);
            this.dgvTableInfo.Name = "dgvTableInfo";
            this.dgvTableInfo.RowTemplate.Height = 24;
            this.dgvTableInfo.Size = new System.Drawing.Size(705, 222);
            this.dgvTableInfo.TabIndex = 5;
            // 
            // TABLE_NAME
            // 
            this.TABLE_NAME.DataPropertyName = "TABLE_NAME";
            this.TABLE_NAME.HeaderText = "TABLE_NAME";
            this.TABLE_NAME.Name = "TABLE_NAME";
            this.TABLE_NAME.Width = 500;
            // 
            // lblTitle
            // 
            this.lblTitle.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTitle.BackColor = System.Drawing.Color.LightSteelBlue;
            this.lblTitle.Font = new System.Drawing.Font("DFKai-SB", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lblTitle.ForeColor = System.Drawing.Color.Blue;
            this.lblTitle.Image = ((System.Drawing.Image)(resources.GetObject("lblTitle.Image")));
            this.lblTitle.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblTitle.Location = new System.Drawing.Point(18, 9);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(703, 52);
            this.lblTitle.TabIndex = 4;
            this.lblTitle.Text = "Transport Tables Form Oracle To MySql";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // rtbGenerateLog
            // 
            this.rtbGenerateLog.Location = new System.Drawing.Point(15, 298);
            this.rtbGenerateLog.Name = "rtbGenerateLog";
            this.rtbGenerateLog.Size = new System.Drawing.Size(705, 93);
            this.rtbGenerateLog.TabIndex = 10;
            this.rtbGenerateLog.Text = "";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.groupBox2);
            this.panel2.Controls.Add(this.groupBox1);
            this.panel2.Controls.Add(this.btnConnect);
            this.panel2.Controls.Add(this.btnGenerate);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 396);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(746, 106);
            this.panel2.TabIndex = 11;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.txtMySqlDb);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Location = new System.Drawing.Point(444, 1);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(275, 100);
            this.groupBox2.TabIndex = 13;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "MySql";
            // 
            // txtMySqlDb
            // 
            this.txtMySqlDb.Location = new System.Drawing.Point(89, 40);
            this.txtMySqlDb.Name = "txtMySqlDb";
            this.txtMySqlDb.Size = new System.Drawing.Size(180, 21);
            this.txtMySqlDb.TabIndex = 8;
            this.txtMySqlDb.Text = "MyEasyWeb";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 45);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(59, 12);
            this.label6.TabIndex = 7;
            this.label6.Text = "DataBase:";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtPwd);
            this.groupBox1.Controls.Add(this.txtSchema);
            this.groupBox1.Controls.Add(this.txtDbName);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(14, 1);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(275, 100);
            this.groupBox1.TabIndex = 12;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Oracle";
            // 
            // txtPwd
            // 
            this.txtPwd.Location = new System.Drawing.Point(85, 72);
            this.txtPwd.Name = "txtPwd";
            this.txtPwd.PasswordChar = '*';
            this.txtPwd.Size = new System.Drawing.Size(180, 21);
            this.txtPwd.TabIndex = 8;
            this.txtPwd.Text = "SFIS1";
            this.txtPwd.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtPwd_KeyPress);
            // 
            // txtSchema
            // 
            this.txtSchema.Location = new System.Drawing.Point(85, 45);
            this.txtSchema.Name = "txtSchema";
            this.txtSchema.Size = new System.Drawing.Size(180, 21);
            this.txtSchema.TabIndex = 8;
            this.txtSchema.Text = "SFIS1";
            this.txtSchema.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtSchema_KeyPress);
            // 
            // txtDbName
            // 
            this.txtDbName.Location = new System.Drawing.Point(85, 18);
            this.txtDbName.Name = "txtDbName";
            this.txtDbName.Size = new System.Drawing.Size(180, 21);
            this.txtDbName.TabIndex = 8;
            this.txtDbName.Text = "TESTESS";
            this.txtDbName.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtDbName_KeyPress);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(8, 77);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(59, 12);
            this.label3.TabIndex = 7;
            this.label3.Text = "PassWord:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(8, 49);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(47, 12);
            this.label2.TabIndex = 7;
            this.label2.Text = "Schema:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 12);
            this.label1.TabIndex = 7;
            this.label1.Text = "DataBase:";
            // 
            // btnConnect
            // 
            this.btnConnect.Font = new System.Drawing.Font("Microsoft JhengHei", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.btnConnect.Location = new System.Drawing.Point(298, 22);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(134, 29);
            this.btnConnect.TabIndex = 10;
            this.btnConnect.Text = "Connect";
            this.btnConnect.UseVisualStyleBackColor = true;
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // btnGenerate
            // 
            this.btnGenerate.Enabled = false;
            this.btnGenerate.Font = new System.Drawing.Font("Microsoft JhengHei", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.btnGenerate.Location = new System.Drawing.Point(298, 62);
            this.btnGenerate.Name = "btnGenerate";
            this.btnGenerate.Size = new System.Drawing.Size(134, 29);
            this.btnGenerate.TabIndex = 11;
            this.btnGenerate.Text = "Generate";
            this.btnGenerate.UseVisualStyleBackColor = true;
            this.btnGenerate.Click += new System.EventHandler(this.btnGenerate_Click);
            // 
            // ucTransTablesFormOraToMySql
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel1);
            this.Name = "ucTransTablesFormOraToMySql";
            this.Size = new System.Drawing.Size(750, 527);
            this.Load += new System.EventHandler(this.ucTransTablesFormOraToMySql_Load);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvTableInfo)).EndInit();
            this.panel2.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DataGridView dgvTableInfo;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.ProgressBar pgbTrans;
        private System.Windows.Forms.DataGridViewTextBoxColumn TABLE_NAME;
        private System.Windows.Forms.RichTextBox rtbGenerateLog;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox txtMySqlDb;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtPwd;
        private System.Windows.Forms.TextBox txtSchema;
        private System.Windows.Forms.TextBox txtDbName;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnConnect;
        private System.Windows.Forms.Button btnGenerate;
    }
}
