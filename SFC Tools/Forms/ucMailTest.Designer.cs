namespace SFC_Tools.Forms
{
    partial class ucMailTest
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
            this.btnGo = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.lblTotalCnt = new System.Windows.Forms.Label();
            this.btnMyTest = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // btnGo
            // 
            this.btnGo.Location = new System.Drawing.Point(106, 463);
            this.btnGo.Name = "btnGo";
            this.btnGo.Size = new System.Drawing.Size(168, 44);
            this.btnGo.TabIndex = 0;
            this.btnGo.Text = "Go";
            this.btnGo.UseVisualStyleBackColor = true;
            this.btnGo.Click += new System.EventHandler(this.btnGo_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(3, 37);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.Size = new System.Drawing.Size(736, 420);
            this.dataGridView1.TabIndex = 1;
            this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 472);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "Count:";
            // 
            // lblTotalCnt
            // 
            this.lblTotalCnt.AutoSize = true;
            this.lblTotalCnt.Location = new System.Drawing.Point(50, 472);
            this.lblTotalCnt.Name = "lblTotalCnt";
            this.lblTotalCnt.Size = new System.Drawing.Size(0, 12);
            this.lblTotalCnt.TabIndex = 3;
            // 
            // btnMyTest
            // 
            this.btnMyTest.Location = new System.Drawing.Point(434, 463);
            this.btnMyTest.Name = "btnMyTest";
            this.btnMyTest.Size = new System.Drawing.Size(168, 44);
            this.btnMyTest.TabIndex = 0;
            this.btnMyTest.Text = "MultiThreadTest";
            this.btnMyTest.UseVisualStyleBackColor = true;
            this.btnMyTest.Click += new System.EventHandler(this.btnMyTest_Click);
            // 
            // ucMailTest
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.Controls.Add(this.lblTotalCnt);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.btnMyTest);
            this.Controls.Add(this.btnGo);
            this.Name = "ucMailTest";
            this.Size = new System.Drawing.Size(742, 519);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnGo;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblTotalCnt;
        private System.Windows.Forms.Button btnMyTest;
    }
}
