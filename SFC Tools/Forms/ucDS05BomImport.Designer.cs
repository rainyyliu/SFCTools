namespace SFC_Tools.Forms
{
    partial class ucDS05BomImport
    {
        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblTreeNodesCnt = new System.Windows.Forms.Label();
            this.lblTotalCnt = new System.Windows.Forms.Label();
            this.tvBom = new System.Windows.Forms.TreeView();
            this.txtFilePath = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.btnLoad = new System.Windows.Forms.Button();
            this.btnTest = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.lblTreeNodesCnt);
            this.panel1.Controls.Add(this.lblTotalCnt);
            this.panel1.Controls.Add(this.tvBom);
            this.panel1.Controls.Add(this.txtFilePath);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.dataGridView1);
            this.panel1.Controls.Add(this.btnTest);
            this.panel1.Controls.Add(this.btnLoad);
            this.panel1.Location = new System.Drawing.Point(12, 65);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(754, 448);
            this.panel1.TabIndex = 12;
            // 
            // lblTreeNodesCnt
            // 
            this.lblTreeNodesCnt.AutoSize = true;
            this.lblTreeNodesCnt.Location = new System.Drawing.Point(128, 378);
            this.lblTreeNodesCnt.Name = "lblTreeNodesCnt";
            this.lblTreeNodesCnt.Size = new System.Drawing.Size(0, 12);
            this.lblTreeNodesCnt.TabIndex = 6;
            // 
            // lblTotalCnt
            // 
            this.lblTotalCnt.AutoSize = true;
            this.lblTotalCnt.Location = new System.Drawing.Point(14, 378);
            this.lblTotalCnt.Name = "lblTotalCnt";
            this.lblTotalCnt.Size = new System.Drawing.Size(0, 12);
            this.lblTotalCnt.TabIndex = 5;
            // 
            // tvBom
            // 
            this.tvBom.Location = new System.Drawing.Point(448, 3);
            this.tvBom.Name = "tvBom";
            this.tvBom.Size = new System.Drawing.Size(303, 366);
            this.tvBom.TabIndex = 4;
            // 
            // txtFilePath
            // 
            this.txtFilePath.Location = new System.Drawing.Point(88, 411);
            this.txtFilePath.Name = "txtFilePath";
            this.txtFilePath.Size = new System.Drawing.Size(473, 21);
            this.txtFilePath.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(8, 414);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "File Path:";
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(5, 3);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.Size = new System.Drawing.Size(439, 366);
            this.dataGridView1.TabIndex = 1;
            // 
            // btnLoad
            // 
            this.btnLoad.Location = new System.Drawing.Point(619, 400);
            this.btnLoad.Name = "btnLoad";
            this.btnLoad.Size = new System.Drawing.Size(52, 40);
            this.btnLoad.TabIndex = 0;
            this.btnLoad.Text = ".....";
            this.btnLoad.UseVisualStyleBackColor = true;
            this.btnLoad.Click += new System.EventHandler(this.btnLoad_Click);
            // 
            // btnTest
            // 
            this.btnTest.Location = new System.Drawing.Point(678, 400);
            this.btnTest.Name = "btnTest";
            this.btnTest.Size = new System.Drawing.Size(52, 40);
            this.btnTest.TabIndex = 0;
            this.btnTest.Text = "Test";
            this.btnTest.UseVisualStyleBackColor = true;
            this.btnTest.Click += new System.EventHandler(this.btnTest_Click);
            // 
            // ucDS05BomImport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel1);
            this.Name = "ucDS05BomImport";
            this.Size = new System.Drawing.Size(783, 516);
            this.Controls.SetChildIndex(this.panel1, 0);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnLoad;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtFilePath;
        private System.Windows.Forms.TreeView tvBom;
        private System.Windows.Forms.Label lblTotalCnt;
        private System.Windows.Forms.Label lblTreeNodesCnt;
        private System.Windows.Forms.Button btnTest;
    }
}
