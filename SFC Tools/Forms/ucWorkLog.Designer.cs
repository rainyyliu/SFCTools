namespace SFC_Tools.Forms
{
    partial class ucWorkLog
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ucWorkLog));
            this.panel1 = new System.Windows.Forms.Panel();
            this.txtTarget = new System.Windows.Forms.TextBox();
            this.txtSource = new System.Windows.Forms.TextBox();
            this.lblFileName = new System.Windows.Forms.Label();
            this.rtbFileList = new System.Windows.Forms.RichTextBox();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnTargetFolder = new System.Windows.Forms.Button();
            this.pgbFormateProgress = new System.Windows.Forms.ProgressBar();
            this.btnSoureFolder = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.ckbLenovoCleaner = new System.Windows.Forms.CheckBox();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Controls.Add(this.ckbLenovoCleaner);
            this.panel1.Controls.Add(this.txtTarget);
            this.panel1.Controls.Add(this.txtSource);
            this.panel1.Controls.Add(this.lblFileName);
            this.panel1.Controls.Add(this.rtbFileList);
            this.panel1.Controls.Add(this.btnOK);
            this.panel1.Controls.Add(this.btnTargetFolder);
            this.panel1.Controls.Add(this.pgbFormateProgress);
            this.panel1.Controls.Add(this.btnSoureFolder);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(750, 517);
            this.panel1.TabIndex = 2;
            // 
            // txtTarget
            // 
            this.txtTarget.Enabled = false;
            this.txtTarget.Location = new System.Drawing.Point(182, 134);
            this.txtTarget.Name = "txtTarget";
            this.txtTarget.Size = new System.Drawing.Size(539, 21);
            this.txtTarget.TabIndex = 14;
            this.txtTarget.Leave += new System.EventHandler(this.txtTarget_Leave);
            // 
            // txtSource
            // 
            this.txtSource.Enabled = false;
            this.txtSource.Location = new System.Drawing.Point(181, 91);
            this.txtSource.Name = "txtSource";
            this.txtSource.Size = new System.Drawing.Size(539, 21);
            this.txtSource.TabIndex = 13;
            this.txtSource.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtSource_KeyPress);
            this.txtSource.Leave += new System.EventHandler(this.txtSource_Leave);
            // 
            // lblFileName
            // 
            this.lblFileName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblFileName.Location = new System.Drawing.Point(21, 426);
            this.lblFileName.Name = "lblFileName";
            this.lblFileName.Size = new System.Drawing.Size(700, 25);
            this.lblFileName.TabIndex = 12;
            // 
            // rtbFileList
            // 
            this.rtbFileList.Location = new System.Drawing.Point(21, 166);
            this.rtbFileList.Name = "rtbFileList";
            this.rtbFileList.Size = new System.Drawing.Size(703, 251);
            this.rtbFileList.TabIndex = 11;
            this.rtbFileList.Text = "";
            // 
            // btnOK
            // 
            this.btnOK.Font = new System.Drawing.Font("Microsoft JhengHei", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.btnOK.Location = new System.Drawing.Point(308, 453);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(134, 29);
            this.btnOK.TabIndex = 10;
            this.btnOK.Text = "Go";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnTargetFolder
            // 
            this.btnTargetFolder.Font = new System.Drawing.Font("Microsoft JhengHei", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.btnTargetFolder.Location = new System.Drawing.Point(21, 132);
            this.btnTargetFolder.Name = "btnTargetFolder";
            this.btnTargetFolder.Size = new System.Drawing.Size(134, 29);
            this.btnTargetFolder.TabIndex = 10;
            this.btnTargetFolder.Text = "Target";
            this.btnTargetFolder.UseVisualStyleBackColor = true;
            this.btnTargetFolder.Click += new System.EventHandler(this.btnTargetFolder_Click);
            // 
            // pgbFormateProgress
            // 
            this.pgbFormateProgress.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pgbFormateProgress.Location = new System.Drawing.Point(0, 489);
            this.pgbFormateProgress.Name = "pgbFormateProgress";
            this.pgbFormateProgress.Size = new System.Drawing.Size(746, 24);
            this.pgbFormateProgress.TabIndex = 8;
            // 
            // btnSoureFolder
            // 
            this.btnSoureFolder.Font = new System.Drawing.Font("Microsoft JhengHei", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.btnSoureFolder.Location = new System.Drawing.Point(21, 87);
            this.btnSoureFolder.Name = "btnSoureFolder";
            this.btnSoureFolder.Size = new System.Drawing.Size(134, 29);
            this.btnSoureFolder.TabIndex = 6;
            this.btnSoureFolder.Text = "Source";
            this.btnSoureFolder.UseVisualStyleBackColor = true;
            this.btnSoureFolder.Click += new System.EventHandler(this.btnSoureFolder_Click);
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
            this.label1.Location = new System.Drawing.Point(20, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(703, 52);
            this.label1.TabIndex = 4;
            this.label1.Text = "My Work Log";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.label1.DoubleClick += new System.EventHandler(this.label1_DoubleClick);
            // 
            // ckbLenovoCleaner
            // 
            this.ckbLenovoCleaner.AutoSize = true;
            this.ckbLenovoCleaner.Checked = true;
            this.ckbLenovoCleaner.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ckbLenovoCleaner.Location = new System.Drawing.Point(24, 463);
            this.ckbLenovoCleaner.Name = "ckbLenovoCleaner";
            this.ckbLenovoCleaner.Size = new System.Drawing.Size(192, 16);
            this.ckbLenovoCleaner.TabIndex = 15;
            this.ckbLenovoCleaner.Text = "Lenovo System Update Cleaner";
            this.ckbLenovoCleaner.UseVisualStyleBackColor = true;
            // 
            // ucWorkLog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel1);
            this.Name = "ucWorkLog";
            this.Size = new System.Drawing.Size(750, 517);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnTargetFolder;
        private System.Windows.Forms.ProgressBar pgbFormateProgress;
        private System.Windows.Forms.Button btnSoureFolder;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RichTextBox rtbFileList;
        private System.Windows.Forms.Label lblFileName;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.TextBox txtTarget;
        private System.Windows.Forms.TextBox txtSource;
        private System.Windows.Forms.CheckBox ckbLenovoCleaner;
    }
}
