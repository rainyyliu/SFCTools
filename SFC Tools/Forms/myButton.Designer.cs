namespace SFC_Tools
{
    partial class myButton
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(myButton));
            this.glassButton2 = new Glass.GlassButton();
            this.SuspendLayout();
            // 
            // glassButton2
            // 
            this.glassButton2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.glassButton2.Font = new System.Drawing.Font("Microsoft YaHei", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.glassButton2.ForeColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.glassButton2.Image = ((System.Drawing.Image)(resources.GetObject("glassButton2.Image")));
            this.glassButton2.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.glassButton2.Location = new System.Drawing.Point(0, 0);
            this.glassButton2.Name = "glassButton2";
            this.glassButton2.Size = new System.Drawing.Size(132, 39);
            this.glassButton2.TabIndex = 22;
            this.glassButton2.Text = "exit";
            this.glassButton2.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            
            // 
            // myButton
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.Controls.Add(this.glassButton2);
            this.Name = "myButton";
            this.Size = new System.Drawing.Size(132, 39);
            this.ResumeLayout(false);

        }

        #endregion

        private Glass.GlassButton glassButton2;

    }
}
