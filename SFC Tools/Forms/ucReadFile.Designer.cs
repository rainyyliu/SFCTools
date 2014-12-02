namespace SFC_Tools
{
    partial class ucReadFile
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ucReadFile));
            this.panel1 = new System.Windows.Forms.Panel();
            this.txtPath = new System.Windows.Forms.TextBox();
            this.btnStop = new System.Windows.Forms.Button();
            this.lblReadPath = new System.Windows.Forms.Label();
            this.btnBeginRead = new System.Windows.Forms.Button();
            this.dgvFileInfo = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.fbdReadFile = new System.Windows.Forms.FolderBrowserDialog();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvFileInfo)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Controls.Add(this.txtPath);
            this.panel1.Controls.Add(this.btnStop);
            this.panel1.Controls.Add(this.lblReadPath);
            this.panel1.Controls.Add(this.btnBeginRead);
            this.panel1.Controls.Add(this.dgvFileInfo);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(750, 527);
            this.panel1.TabIndex = 0;
            this.panel1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.panel1_MouseDoubleClick);
            // 
            // txtPath
            // 
            this.txtPath.Font = new System.Drawing.Font("Microsoft JhengHei", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.txtPath.Location = new System.Drawing.Point(298, 483);
            this.txtPath.Name = "txtPath";
            this.txtPath.Size = new System.Drawing.Size(269, 25);
            this.txtPath.TabIndex = 9;
            this.txtPath.Visible = false;
            this.txtPath.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtPath_KeyPress);
            // 
            // btnStop
            // 
            this.btnStop.Font = new System.Drawing.Font("Microsoft JhengHei", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.btnStop.Location = new System.Drawing.Point(587, 483);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(134, 29);
            this.btnStop.TabIndex = 8;
            this.btnStop.Text = "Stop Read";
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // lblReadPath
            // 
            this.lblReadPath.AutoSize = true;
            this.lblReadPath.Font = new System.Drawing.Font("Microsoft JhengHei", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lblReadPath.ForeColor = System.Drawing.Color.Blue;
            this.lblReadPath.Location = new System.Drawing.Point(162, 488);
            this.lblReadPath.Name = "lblReadPath";
            this.lblReadPath.Size = new System.Drawing.Size(0, 19);
            this.lblReadPath.TabIndex = 7;
            // 
            // btnBeginRead
            // 
            this.btnBeginRead.Font = new System.Drawing.Font("Microsoft JhengHei", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.btnBeginRead.Location = new System.Drawing.Point(15, 482);
            this.btnBeginRead.Name = "btnBeginRead";
            this.btnBeginRead.Size = new System.Drawing.Size(134, 29);
            this.btnBeginRead.TabIndex = 6;
            this.btnBeginRead.Text = "Read File";
            this.btnBeginRead.UseVisualStyleBackColor = true;
            this.btnBeginRead.Click += new System.EventHandler(this.btnBeginRead_Click);
            // 
            // dgvFileInfo
            // 
            this.dgvFileInfo.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvFileInfo.Location = new System.Drawing.Point(15, 72);
            this.dgvFileInfo.Name = "dgvFileInfo";
            this.dgvFileInfo.RowTemplate.Height = 24;
            this.dgvFileInfo.Size = new System.Drawing.Size(705, 404);
            this.dgvFileInfo.TabIndex = 5;
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
            this.label1.Location = new System.Drawing.Point(18, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(703, 52);
            this.label1.TabIndex = 4;
            this.label1.Text = "Read File";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ucReadFile
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel1);
            this.Name = "ucReadFile";
            this.Size = new System.Drawing.Size(750, 527);
            this.Load += new System.EventHandler(this.ucReadFile_Load);
            this.Disposed += new System.EventHandler(this.ucReadFile_Disposed);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvFileInfo)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnBeginRead;
        private System.Windows.Forms.DataGridView dgvFileInfo;
        private System.Windows.Forms.Label lblReadPath;
        private System.Windows.Forms.FolderBrowserDialog fbdReadFile;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.TextBox txtPath;

    }
}
