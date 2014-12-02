namespace SFC_Tools.Forms
{
    partial class ucXmlTest
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
            this.btnReadXml = new System.Windows.Forms.Button();
            this.rtxtXmlFiles = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // btnReadXml
            // 
            this.btnReadXml.Location = new System.Drawing.Point(290, 464);
            this.btnReadXml.Name = "btnReadXml";
            this.btnReadXml.Size = new System.Drawing.Size(109, 29);
            this.btnReadXml.TabIndex = 0;
            this.btnReadXml.Text = "btnRead";
            this.btnReadXml.UseVisualStyleBackColor = true;
            this.btnReadXml.Click += new System.EventHandler(this.btnReadXml_Click);
            // 
            // rtxtXmlFiles
            // 
            this.rtxtXmlFiles.Location = new System.Drawing.Point(3, 13);
            this.rtxtXmlFiles.Name = "rtxtXmlFiles";
            this.rtxtXmlFiles.Size = new System.Drawing.Size(743, 408);
            this.rtxtXmlFiles.TabIndex = 1;
            this.rtxtXmlFiles.Text = "";
            // 
            // ucXmlTest
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.rtxtXmlFiles);
            this.Controls.Add(this.btnReadXml);
            this.Name = "ucXmlTest";
            this.Size = new System.Drawing.Size(749, 518);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnReadXml;
        private System.Windows.Forms.RichTextBox rtxtXmlFiles;
    }
}
