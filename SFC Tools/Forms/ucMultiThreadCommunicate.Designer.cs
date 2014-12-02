namespace SFC_Tools.Forms
{
    partial class ucMultiThreadCommunicate
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
            this.lbThreadInfo = new System.Windows.Forms.ListBox();
            this.btnEnd = new System.Windows.Forms.Button();
            this.btnStart = new System.Windows.Forms.Button();
            this.btnSockTest = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.lbThreadInfo);
            this.panel1.Controls.Add(this.btnEnd);
            this.panel1.Controls.Add(this.btnSockTest);
            this.panel1.Controls.Add(this.btnStart);
            this.panel1.Location = new System.Drawing.Point(16, 68);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(751, 438);
            this.panel1.TabIndex = 12;
            // 
            // lbThreadInfo
            // 
            this.lbThreadInfo.FormattingEnabled = true;
            this.lbThreadInfo.ItemHeight = 12;
            this.lbThreadInfo.Location = new System.Drawing.Point(51, 14);
            this.lbThreadInfo.Name = "lbThreadInfo";
            this.lbThreadInfo.Size = new System.Drawing.Size(643, 220);
            this.lbThreadInfo.TabIndex = 1;
            // 
            // btnEnd
            // 
            this.btnEnd.Location = new System.Drawing.Point(420, 361);
            this.btnEnd.Name = "btnEnd";
            this.btnEnd.Size = new System.Drawing.Size(182, 43);
            this.btnEnd.TabIndex = 0;
            this.btnEnd.Text = "End";
            this.btnEnd.UseVisualStyleBackColor = true;
            this.btnEnd.Click += new System.EventHandler(this.btnEnd_Click);
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(134, 361);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(182, 43);
            this.btnStart.TabIndex = 0;
            this.btnStart.Text = "Start";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // btnSockTest
            // 
            this.btnSockTest.Location = new System.Drawing.Point(291, 280);
            this.btnSockTest.Name = "btnSockTest";
            this.btnSockTest.Size = new System.Drawing.Size(182, 43);
            this.btnSockTest.TabIndex = 0;
            this.btnSockTest.Text = "Start";
            this.btnSockTest.UseVisualStyleBackColor = true;
            this.btnSockTest.Click += new System.EventHandler(this.btnSockTest_Click);
            // 
            // ucMultiThreadCommunicate
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel1);
            this.Name = "ucMultiThreadCommunicate";
            this.Size = new System.Drawing.Size(785, 518);
            this.Load += new System.EventHandler(this.ucMultiThreadCommunicate_Load);
            this.Controls.SetChildIndex(this.panel1, 0);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Button btnEnd;
        private System.Windows.Forms.ListBox lbThreadInfo;
        private System.Windows.Forms.Button btnSockTest;
    }
}
