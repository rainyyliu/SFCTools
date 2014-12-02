namespace SFC_Tools.Forms
{
    partial class ucEnDeCrypt
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ucEnDeCrypt));
            this.label1 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnEnCrypt = new System.Windows.Forms.Button();
            this.btnDeCrypt = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            this.rtbDeCrypt = new System.Windows.Forms.RichTextBox();
            this.rtbEnCrypt = new System.Windows.Forms.RichTextBox();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.LightSteelBlue;
            this.label1.Font = new System.Drawing.Font("DFKai-SB", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label1.ForeColor = System.Drawing.Color.Blue;
            this.label1.Image = ((System.Drawing.Image)(resources.GetObject("label1.Image")));
            this.label1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label1.Location = new System.Drawing.Point(12, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(727, 52);
            this.label1.TabIndex = 8;
            this.label1.Text = "Decrypt And Encrypt Window";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.rtbDeCrypt);
            this.panel1.Location = new System.Drawing.Point(13, 85);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(727, 171);
            this.panel1.TabIndex = 9;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.rtbEnCrypt);
            this.panel2.Location = new System.Drawing.Point(13, 313);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(727, 171);
            this.panel2.TabIndex = 10;
            // 
            // btnEnCrypt
            // 
            this.btnEnCrypt.Location = new System.Drawing.Point(85, 272);
            this.btnEnCrypt.Name = "btnEnCrypt";
            this.btnEnCrypt.Size = new System.Drawing.Size(159, 28);
            this.btnEnCrypt.TabIndex = 11;
            this.btnEnCrypt.Text = "EnCrypt";
            this.btnEnCrypt.UseVisualStyleBackColor = true;
            this.btnEnCrypt.Click += new System.EventHandler(this.btnEnCrypt_Click);
            // 
            // btnDeCrypt
            // 
            this.btnDeCrypt.Location = new System.Drawing.Point(298, 272);
            this.btnDeCrypt.Name = "btnDeCrypt";
            this.btnDeCrypt.Size = new System.Drawing.Size(159, 28);
            this.btnDeCrypt.TabIndex = 11;
            this.btnDeCrypt.Text = "DeCrypt";
            this.btnDeCrypt.UseVisualStyleBackColor = true;
            this.btnDeCrypt.Click += new System.EventHandler(this.btnDeCrypt_Click);
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(511, 272);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(159, 28);
            this.btnClear.TabIndex = 11;
            this.btnClear.Text = "Clear";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // rtbDeCrypt
            // 
            this.rtbDeCrypt.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtbDeCrypt.Location = new System.Drawing.Point(0, 0);
            this.rtbDeCrypt.Name = "rtbDeCrypt";
            this.rtbDeCrypt.Size = new System.Drawing.Size(727, 171);
            this.rtbDeCrypt.TabIndex = 0;
            this.rtbDeCrypt.Text = "";
            // 
            // rtbEnCrypt
            // 
            this.rtbEnCrypt.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtbEnCrypt.Location = new System.Drawing.Point(0, 0);
            this.rtbEnCrypt.Name = "rtbEnCrypt";
            this.rtbEnCrypt.Size = new System.Drawing.Size(727, 171);
            this.rtbEnCrypt.TabIndex = 0;
            this.rtbEnCrypt.Text = "";
            // 
            // ucEnDeCrypt
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.btnDeCrypt);
            this.Controls.Add(this.btnEnCrypt);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.label1);
            this.Name = "ucEnDeCrypt";
            this.Size = new System.Drawing.Size(751, 520);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btnEnCrypt;
        private System.Windows.Forms.Button btnDeCrypt;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.RichTextBox rtbDeCrypt;
        private System.Windows.Forms.RichTextBox rtbEnCrypt;
    }
}
