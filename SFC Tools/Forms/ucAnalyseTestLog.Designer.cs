namespace SFC_Tools
{
    partial class ucAnalyseTestLog
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ucAnalyseTestLog));
            this.ofdIctLog = new System.Windows.Forms.OpenFileDialog();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnExcel = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.cmbTime = new System.Windows.Forms.ComboBox();
            this.pgbRead = new System.Windows.Forms.ProgressBar();
            this.lblTotal = new System.Windows.Forms.Label();
            this.btnReadLog = new System.Windows.Forms.Button();
            this.dgvTestLogAnalyse = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.sfdIctExcel = new System.Windows.Forms.SaveFileDialog();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTestLogAnalyse)).BeginInit();
            this.SuspendLayout();
            // 
            // ofdIctLog
            // 
            this.ofdIctLog.FileName = "IctLog";
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Controls.Add(this.pgbRead);
            this.panel1.Controls.Add(this.lblTotal);
            this.panel1.Controls.Add(this.btnReadLog);
            this.panel1.Controls.Add(this.dgvTestLogAnalyse);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(660, 509);
            this.panel1.TabIndex = 0;
            // 
            // panel2
            // 
            this.panel2.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.panel2.Controls.Add(this.btnExcel);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.cmbTime);
            this.panel2.Location = new System.Drawing.Point(287, 458);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(355, 29);
            this.panel2.TabIndex = 14;
            // 
            // btnExcel
            // 
            this.btnExcel.Location = new System.Drawing.Point(31, 0);
            this.btnExcel.Name = "btnExcel";
            this.btnExcel.Size = new System.Drawing.Size(103, 28);
            this.btnExcel.TabIndex = 18;
            this.btnExcel.Text = "Excel";
            this.btnExcel.UseVisualStyleBackColor = true;
            this.btnExcel.Click += new System.EventHandler(this.btnExcel_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft JhengHei", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label2.ForeColor = System.Drawing.Color.Blue;
            this.label2.Location = new System.Drawing.Point(140, 6);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 17);
            this.label2.TabIndex = 17;
            this.label2.Text = "Total Time:";
            // 
            // cmbTime
            // 
            this.cmbTime.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTime.Font = new System.Drawing.Font("Microsoft JhengHei", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.cmbTime.FormattingEnabled = true;
            this.cmbTime.Items.AddRange(new object[] {
            "0",
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9",
            "10"});
            this.cmbTime.Location = new System.Drawing.Point(223, 4);
            this.cmbTime.Name = "cmbTime";
            this.cmbTime.Size = new System.Drawing.Size(129, 24);
            this.cmbTime.TabIndex = 16;
            this.cmbTime.SelectedIndexChanged += new System.EventHandler(this.cmbTime_SelectedIndexChanged);
            // 
            // pgbRead
            // 
            this.pgbRead.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.pgbRead.Location = new System.Drawing.Point(1, 488);
            this.pgbRead.Name = "pgbRead";
            this.pgbRead.Size = new System.Drawing.Size(657, 22);
            this.pgbRead.TabIndex = 13;
            // 
            // lblTotal
            // 
            this.lblTotal.AutoSize = true;
            this.lblTotal.Font = new System.Drawing.Font("Microsoft JhengHei", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lblTotal.ForeColor = System.Drawing.Color.Blue;
            this.lblTotal.Location = new System.Drawing.Point(19, 458);
            this.lblTotal.Name = "lblTotal";
            this.lblTotal.Size = new System.Drawing.Size(43, 17);
            this.lblTotal.TabIndex = 12;
            this.lblTotal.Text = "Total:";
            // 
            // btnReadLog
            // 
            this.btnReadLog.Location = new System.Drawing.Point(154, 459);
            this.btnReadLog.Name = "btnReadLog";
            this.btnReadLog.Size = new System.Drawing.Size(103, 28);
            this.btnReadLog.TabIndex = 11;
            this.btnReadLog.Text = "Open";
            this.btnReadLog.UseVisualStyleBackColor = true;
            this.btnReadLog.Click += new System.EventHandler(this.btnReadLog_Click);
            // 
            // dgvTestLogAnalyse
            // 
            this.dgvTestLogAnalyse.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvTestLogAnalyse.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvTestLogAnalyse.Location = new System.Drawing.Point(14, 72);
            this.dgvTestLogAnalyse.Name = "dgvTestLogAnalyse";
            this.dgvTestLogAnalyse.RowTemplate.Height = 24;
            this.dgvTestLogAnalyse.Size = new System.Drawing.Size(627, 383);
            this.dgvTestLogAnalyse.TabIndex = 10;
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
            this.label1.Location = new System.Drawing.Point(13, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(630, 52);
            this.label1.TabIndex = 9;
            this.label1.Text = "ICT Log Analyse";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ucAnalyseTestLog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel1);
            this.Name = "ucAnalyseTestLog";
            this.Size = new System.Drawing.Size(660, 509);
            this.Load += new System.EventHandler(this.ucAnalyseTestLog_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTestLogAnalyse)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.OpenFileDialog ofdIctLog;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lblTotal;
        private System.Windows.Forms.Button btnReadLog;
        private System.Windows.Forms.DataGridView dgvTestLogAnalyse;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ProgressBar pgbRead;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cmbTime;
        private System.Windows.Forms.Button btnExcel;
        private System.Windows.Forms.SaveFileDialog sfdIctExcel;
    }
}
