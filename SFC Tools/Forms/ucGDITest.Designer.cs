namespace SFC_Tools.Forms
{
    partial class ucGDITest
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
            this.lblTest = new System.Windows.Forms.Label();
            this.btnTest = new System.Windows.Forms.Button();
            this.btnRepair = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblTest
            // 
            this.lblTest.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblTest.Location = new System.Drawing.Point(26, 53);
            this.lblTest.Name = "lblTest";
            this.lblTest.Size = new System.Drawing.Size(675, 450);
            this.lblTest.TabIndex = 0;
            this.lblTest.Paint += new System.Windows.Forms.PaintEventHandler(this.lblTest_Paint);
            this.lblTest.MouseClick += new System.Windows.Forms.MouseEventHandler(this.lblTest_MouseClick);
            // 
            // btnTest
            // 
            this.btnTest.Location = new System.Drawing.Point(44, 13);
            this.btnTest.Name = "btnTest";
            this.btnTest.Size = new System.Drawing.Size(73, 28);
            this.btnTest.TabIndex = 1;
            this.btnTest.Text = "Test";
            this.btnTest.UseVisualStyleBackColor = true;
            this.btnTest.Click += new System.EventHandler(this.btnTest_Click);
            this.btnTest.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btnTest_MouseDown);
            this.btnTest.MouseMove += new System.Windows.Forms.MouseEventHandler(this.btnTest_MouseMove);
            this.btnTest.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btnTest_MouseUp);
            // 
            // btnRepair
            // 
            this.btnRepair.Location = new System.Drawing.Point(153, 13);
            this.btnRepair.Name = "btnRepair";
            this.btnRepair.Size = new System.Drawing.Size(73, 28);
            this.btnRepair.TabIndex = 1;
            this.btnRepair.Text = "Repair";
            this.btnRepair.UseVisualStyleBackColor = true;
            this.btnRepair.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btnTest_MouseDown);
            this.btnRepair.MouseMove += new System.Windows.Forms.MouseEventHandler(this.btnTest_MouseMove);
            this.btnRepair.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btnTest_MouseUp);
            // 
            // ucGDITest
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnRepair);
            this.Controls.Add(this.btnTest);
            this.Controls.Add(this.lblTest);
            this.Name = "ucGDITest";
            this.Size = new System.Drawing.Size(740, 517);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblTest;
        private System.Windows.Forms.Button btnTest;
        private System.Windows.Forms.Button btnRepair;
    }
}
