namespace SFC_Tools
{
    partial class ucRoute
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ucRoute));
            this.pdTest = new System.Drawing.Printing.PrintDocument();
            this.pnlBody = new System.Windows.Forms.Panel();
            this.glsPrintPre = new Glass.GlassButton();
            this.glsRouteChange = new Glass.GlassButton();
            this.glsBtnPre = new Glass.GlassButton();
            this.pnlContain = new System.Windows.Forms.Panel();
            this.pbRoute = new System.Windows.Forms.PictureBox();
            this.lblTitle = new System.Windows.Forms.Label();
            this.ppdTest = new System.Windows.Forms.PrintPreviewDialog();
            this.pnlBody.SuspendLayout();
            this.pnlContain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbRoute)).BeginInit();
            this.SuspendLayout();
            // 
            // pdTest
            // 
            this.pdTest.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(this.pdTest_PrintPage);
            // 
            // pnlBody
            // 
            this.pnlBody.BackColor = System.Drawing.SystemColors.Window;
            this.pnlBody.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pnlBody.BackgroundImage")));
            this.pnlBody.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnlBody.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pnlBody.Controls.Add(this.glsPrintPre);
            this.pnlBody.Controls.Add(this.glsRouteChange);
            this.pnlBody.Controls.Add(this.glsBtnPre);
            this.pnlBody.Controls.Add(this.pnlContain);
            this.pnlBody.Controls.Add(this.lblTitle);
            this.pnlBody.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlBody.Location = new System.Drawing.Point(0, 0);
            this.pnlBody.Name = "pnlBody";
            this.pnlBody.Size = new System.Drawing.Size(660, 509);
            this.pnlBody.TabIndex = 0;
            // 
            // glsPrintPre
            // 
            this.glsPrintPre.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.glsPrintPre.Font = new System.Drawing.Font("Microsoft YaHei", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.glsPrintPre.ForeColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.glsPrintPre.Image = ((System.Drawing.Image)(resources.GetObject("glsPrintPre.Image")));
            this.glsPrintPre.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.glsPrintPre.Location = new System.Drawing.Point(475, 428);
            this.glsPrintPre.Name = "glsPrintPre";
            this.glsPrintPre.Size = new System.Drawing.Size(144, 40);
            this.glsPrintPre.TabIndex = 18;
            this.glsPrintPre.Text = "Print Preview";
            this.glsPrintPre.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.glsPrintPre.Click += new System.EventHandler(this.glsPrintPre_Click);
            // 
            // glsRouteChange
            // 
            this.glsRouteChange.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.glsRouteChange.Font = new System.Drawing.Font("Microsoft YaHei", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.glsRouteChange.ForeColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.glsRouteChange.Image = ((System.Drawing.Image)(resources.GetObject("glsRouteChange.Image")));
            this.glsRouteChange.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.glsRouteChange.Location = new System.Drawing.Point(249, 428);
            this.glsRouteChange.Name = "glsRouteChange";
            this.glsRouteChange.Size = new System.Drawing.Size(144, 40);
            this.glsRouteChange.TabIndex = 19;
            this.glsRouteChange.Text = "Load Route";
            this.glsRouteChange.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.glsRouteChange.Click += new System.EventHandler(this.glsRouteChange_Click);
            // 
            // glsBtnPre
            // 
            this.glsBtnPre.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.glsBtnPre.Font = new System.Drawing.Font("Microsoft YaHei", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.glsBtnPre.ForeColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.glsBtnPre.Image = ((System.Drawing.Image)(resources.GetObject("glsBtnPre.Image")));
            this.glsBtnPre.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.glsBtnPre.Location = new System.Drawing.Point(8, 428);
            this.glsBtnPre.Name = "glsBtnPre";
            this.glsBtnPre.Size = new System.Drawing.Size(144, 40);
            this.glsBtnPre.TabIndex = 16;
            this.glsBtnPre.Text = "Preview";
            this.glsBtnPre.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.glsBtnPre.Click += new System.EventHandler(this.glsBtnPre_Click);
            // 
            // pnlContain
            // 
            this.pnlContain.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlContain.AutoScroll = true;
            this.pnlContain.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlContain.Controls.Add(this.pbRoute);
            this.pnlContain.ImeMode = System.Windows.Forms.ImeMode.Alpha;
            this.pnlContain.Location = new System.Drawing.Point(3, 57);
            this.pnlContain.Name = "pnlContain";
            this.pnlContain.Size = new System.Drawing.Size(650, 307);
            this.pnlContain.TabIndex = 6;
            // 
            // pbRoute
            // 
            this.pbRoute.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pbRoute.Location = new System.Drawing.Point(-99, -1);
            this.pbRoute.Name = "pbRoute";
            this.pbRoute.Size = new System.Drawing.Size(627, 284);
            this.pbRoute.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pbRoute.TabIndex = 0;
            this.pbRoute.TabStop = false;
            this.pbRoute.Click += new System.EventHandler(this.pbRoute_Click);
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
            this.lblTitle.Location = new System.Drawing.Point(3, 6);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(650, 48);
            this.lblTitle.TabIndex = 5;
            this.lblTitle.Text = "Route Test";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ppdTest
            // 
            this.ppdTest.AutoScrollMargin = new System.Drawing.Size(0, 0);
            this.ppdTest.AutoScrollMinSize = new System.Drawing.Size(0, 0);
            this.ppdTest.ClientSize = new System.Drawing.Size(400, 300);
            this.ppdTest.DataBindings.Add(new System.Windows.Forms.Binding("ClientSize", global::SFC_Tools.Properties.Settings.Default, "xxx", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.ppdTest.Enabled = true;
            this.ppdTest.Icon = ((System.Drawing.Icon)(resources.GetObject("ppdTest.Icon")));
            this.ppdTest.Name = "ppdTest";
            this.ppdTest.Visible = false;
            // 
            // ucRoute
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pnlBody);
            this.Name = "ucRoute";
            this.Size = new System.Drawing.Size(660, 509);
            this.Load += new System.EventHandler(this.ucRoute_Load);
            this.pnlBody.ResumeLayout(false);
            this.pnlContain.ResumeLayout(false);
            this.pnlContain.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbRoute)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlBody;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Panel pnlContain;
        public System.Windows.Forms.PictureBox pbRoute;
        private System.Windows.Forms.PrintPreviewDialog ppdTest;
        private System.Drawing.Printing.PrintDocument pdTest;
        private Glass.GlassButton glsBtnPre;
        private Glass.GlassButton glsPrintPre;
        private Glass.GlassButton glsRouteChange;
    }
}
