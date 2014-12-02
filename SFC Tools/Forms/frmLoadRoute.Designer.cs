namespace SFC_Tools
{
    partial class frmLoadRoute
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmLoadRoute));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.glassButton1 = new Glass.GlassButton();
            this.glassButton2 = new Glass.GlassButton();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.Transparent;
            this.groupBox1.Controls.Add(this.glassButton1);
            this.groupBox1.Controls.Add(this.glassButton2);
            this.groupBox1.Controls.Add(this.comboBox1);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft YaHei", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox1.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.groupBox1.Location = new System.Drawing.Point(18, 15);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(418, 253);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Select Route Name";
            // 
            // glassButton1
            // 
            this.glassButton1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.glassButton1.Font = new System.Drawing.Font("Microsoft YaHei", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.glassButton1.ForeColor = System.Drawing.SystemColors.GradientInactiveCaption;
            //this.glassButton1.Image = global::SFC_Tools.Properties.Resources.SNOW;
            this.glassButton1.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.glassButton1.Location = new System.Drawing.Point(228, 187);
            this.glassButton1.Name = "glassButton1";
            this.glassButton1.Size = new System.Drawing.Size(140, 35);
            this.glassButton1.TabIndex = 25;
            this.glassButton1.Text = "exit";
            this.glassButton1.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.glassButton1.Click += new System.EventHandler(this.glassButton1_Click);
            // 
            // glassButton2
            // 
            this.glassButton2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.glassButton2.Font = new System.Drawing.Font("Microsoft YaHei", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.glassButton2.ForeColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.glassButton2.Image = ((System.Drawing.Image)(resources.GetObject("glassButton2.Image")));
            this.glassButton2.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.glassButton2.Location = new System.Drawing.Point(64, 187);
            this.glassButton2.Name = "glassButton2";
            this.glassButton2.Size = new System.Drawing.Size(136, 35);
            this.glassButton2.TabIndex = 24;
            this.glassButton2.Text = "OK";
            this.glassButton2.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.glassButton2.Click += new System.EventHandler(this.glassButton2_Click);
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(90, 107);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(237, 33);
            this.comboBox1.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(83, 46);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(256, 25);
            this.label1.TabIndex = 0;
            this.label1.Text = "Please Select a New Route";
            // 
            // frmLoadRoute
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.ClientSize = new System.Drawing.Size(455, 288);
            this.Controls.Add(this.groupBox1);
            this.Name = "frmLoadRoute";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Load New Route";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboBox1;
        private Glass.GlassButton glassButton1;
        private Glass.GlassButton glassButton2;

    }
}