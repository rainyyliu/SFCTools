namespace SFC_Tools.Forms
{
    partial class uFileFormate
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(uFileFormate));
            this.panel1 = new System.Windows.Forms.Panel();
            this.button1 = new System.Windows.Forms.Button();
            this.rtbNewFile = new System.Windows.Forms.RichTextBox();
            this.pgbFormateProgress = new System.Windows.Forms.ProgressBar();
            this.btnBeginRead = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.ofdTargetFile = new System.Windows.Forms.OpenFileDialog();
            this.sfdNewFile = new System.Windows.Forms.SaveFileDialog();
            this.btnCodeFormate = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Controls.Add(this.btnCodeFormate);
            this.panel1.Controls.Add(this.button1);
            this.panel1.Controls.Add(this.rtbNewFile);
            this.panel1.Controls.Add(this.pgbFormateProgress);
            this.panel1.Controls.Add(this.btnBeginRead);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(750, 527);
            this.panel1.TabIndex = 1;
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Microsoft JhengHei", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.button1.Location = new System.Drawing.Point(556, 454);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(134, 29);
            this.button1.TabIndex = 10;
            this.button1.Text = "Save";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // rtbNewFile
            // 
            this.rtbNewFile.Location = new System.Drawing.Point(20, 67);
            this.rtbNewFile.Name = "rtbNewFile";
            this.rtbNewFile.Size = new System.Drawing.Size(703, 368);
            this.rtbNewFile.TabIndex = 9;
            this.rtbNewFile.Text = "";
            // 
            // pgbFormateProgress
            // 
            this.pgbFormateProgress.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pgbFormateProgress.Location = new System.Drawing.Point(0, 499);
            this.pgbFormateProgress.Name = "pgbFormateProgress";
            this.pgbFormateProgress.Size = new System.Drawing.Size(746, 24);
            this.pgbFormateProgress.TabIndex = 8;
            // 
            // btnBeginRead
            // 
            this.btnBeginRead.Font = new System.Drawing.Font("Microsoft JhengHei", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.btnBeginRead.Location = new System.Drawing.Point(348, 454);
            this.btnBeginRead.Name = "btnBeginRead";
            this.btnBeginRead.Size = new System.Drawing.Size(134, 29);
            this.btnBeginRead.TabIndex = 6;
            this.btnBeginRead.Text = "Formate";
            this.btnBeginRead.UseVisualStyleBackColor = true;
            this.btnBeginRead.Click += new System.EventHandler(this.btnBeginRead_Click);
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
            this.label1.Text = "JS File Formate";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnCodeFormate
            // 
            this.btnCodeFormate.Font = new System.Drawing.Font("Microsoft JhengHei", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.btnCodeFormate.Location = new System.Drawing.Point(38, 454);
            this.btnCodeFormate.Name = "btnCodeFormate";
            this.btnCodeFormate.Size = new System.Drawing.Size(134, 29);
            this.btnCodeFormate.TabIndex = 11;
            this.btnCodeFormate.Text = "Formate Code";
            this.btnCodeFormate.UseVisualStyleBackColor = true;
            this.btnCodeFormate.Click += new System.EventHandler(this.btnCodeFormate_Click);
            // 
            // uFileFormate
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel1);
            this.Name = "uFileFormate";
            this.Size = new System.Drawing.Size(750, 527);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnBeginRead;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.OpenFileDialog ofdTargetFile;
        private System.Windows.Forms.ProgressBar pgbFormateProgress;
        private System.Windows.Forms.RichTextBox rtbNewFile;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.SaveFileDialog sfdNewFile;
        private System.Windows.Forms.Button btnCodeFormate;
    }
}
