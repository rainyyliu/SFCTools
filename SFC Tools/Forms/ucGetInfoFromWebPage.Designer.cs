namespace SFC_Tools.Forms
{
    partial class ucGetInfoFromWebPage
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ucGetInfoFromWebPage));
            this.pnlParent = new System.Windows.Forms.Panel();
            this.chkMesTickets = new System.Windows.Forms.CheckBox();
            this.chkAllDue = new System.Windows.Forms.CheckBox();
            this.pbgGetTicketListProgress = new System.Windows.Forms.ProgressBar();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.cmbCategory = new System.Windows.Forms.ComboBox();
            this.cmbGroup = new System.Windows.Forms.ComboBox();
            this.btnGetTaskLists = new System.Windows.Forms.Button();
            this.lblTotal = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.dgvTaskListInfo = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.btnGetData = new System.Windows.Forms.Button();
            this.btnGo = new System.Windows.Forms.Button();
            this.webBrowser1 = new System.Windows.Forms.WebBrowser();
            this.pnlParent.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTaskListInfo)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlParent
            // 
            this.pnlParent.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlParent.Controls.Add(this.chkMesTickets);
            this.pnlParent.Controls.Add(this.chkAllDue);
            this.pnlParent.Controls.Add(this.pbgGetTicketListProgress);
            this.pnlParent.Controls.Add(this.groupBox1);
            this.pnlParent.Controls.Add(this.btnGetTaskLists);
            this.pnlParent.Controls.Add(this.lblTotal);
            this.pnlParent.Controls.Add(this.label2);
            this.pnlParent.Controls.Add(this.dgvTaskListInfo);
            this.pnlParent.Controls.Add(this.label1);
            this.pnlParent.Controls.Add(this.btnGetData);
            this.pnlParent.Controls.Add(this.btnGo);
            this.pnlParent.Controls.Add(this.webBrowser1);
            this.pnlParent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlParent.Location = new System.Drawing.Point(0, 0);
            this.pnlParent.Name = "pnlParent";
            this.pnlParent.Size = new System.Drawing.Size(751, 520);
            this.pnlParent.TabIndex = 0;
            // 
            // chkMesTickets
            // 
            this.chkMesTickets.AutoSize = true;
            this.chkMesTickets.Location = new System.Drawing.Point(10, 486);
            this.chkMesTickets.Name = "chkMesTickets";
            this.chkMesTickets.Size = new System.Drawing.Size(66, 16);
            this.chkMesTickets.TabIndex = 16;
            this.chkMesTickets.Text = "MES TKs";
            this.chkMesTickets.UseVisualStyleBackColor = true;
            this.chkMesTickets.CheckedChanged += new System.EventHandler(this.chkMesTickets_CheckedChanged);
            // 
            // chkAllDue
            // 
            this.chkAllDue.AutoSize = true;
            this.chkAllDue.Location = new System.Drawing.Point(9, 468);
            this.chkAllDue.Name = "chkAllDue";
            this.chkAllDue.Size = new System.Drawing.Size(66, 16);
            this.chkAllDue.TabIndex = 15;
            this.chkAllDue.Text = "All Due";
            this.chkAllDue.UseVisualStyleBackColor = true;
            this.chkAllDue.CheckedChanged += new System.EventHandler(this.chkAllDue_CheckedChanged);
            // 
            // pbgGetTicketListProgress
            // 
            this.pbgGetTicketListProgress.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pbgGetTicketListProgress.Location = new System.Drawing.Point(0, 503);
            this.pbgGetTicketListProgress.Name = "pbgGetTicketListProgress";
            this.pbgGetTicketListProgress.Size = new System.Drawing.Size(749, 15);
            this.pbgGetTicketListProgress.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.pbgGetTicketListProgress.TabIndex = 14;
            this.pbgGetTicketListProgress.Validated += new System.EventHandler(this.pbgGetTicketListProgress_Validated);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.cmbCategory);
            this.groupBox1.Controls.Add(this.cmbGroup);
            this.groupBox1.Location = new System.Drawing.Point(216, 457);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(529, 43);
            this.groupBox1.TabIndex = 13;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Setting";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(251, 20);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(59, 12);
            this.label4.TabIndex = 14;
            this.label4.Text = "Categary:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(44, 20);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 12);
            this.label3.TabIndex = 13;
            this.label3.Text = "Group:";
            // 
            // cmbCategory
            // 
            this.cmbCategory.FormattingEnabled = true;
            this.cmbCategory.Items.AddRange(new object[] {
            "L6",
            "L10"});
            this.cmbCategory.Location = new System.Drawing.Point(319, 16);
            this.cmbCategory.Name = "cmbCategory";
            this.cmbCategory.Size = new System.Drawing.Size(194, 20);
            this.cmbCategory.TabIndex = 12;
            this.cmbCategory.SelectedIndexChanged += new System.EventHandler(this.cmbCategory_SelectedIndexChanged);
            this.cmbCategory.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.cmbCategory_KeyPress);
            // 
            // cmbGroup
            // 
            this.cmbGroup.FormattingEnabled = true;
            this.cmbGroup.Items.AddRange(new object[] {
            "CMMSG",
            "CESBG"});
            this.cmbGroup.Location = new System.Drawing.Point(97, 16);
            this.cmbGroup.Name = "cmbGroup";
            this.cmbGroup.Size = new System.Drawing.Size(130, 20);
            this.cmbGroup.TabIndex = 11;
            this.cmbGroup.SelectedIndexChanged += new System.EventHandler(this.cmbGroup_SelectedIndexChanged);
            // 
            // btnGetTaskLists
            // 
            this.btnGetTaskLists.Location = new System.Drawing.Point(91, 471);
            this.btnGetTaskLists.Name = "btnGetTaskLists";
            this.btnGetTaskLists.Size = new System.Drawing.Size(116, 28);
            this.btnGetTaskLists.TabIndex = 10;
            this.btnGetTaskLists.Text = "GetTaskLists";
            this.btnGetTaskLists.UseVisualStyleBackColor = true;
            this.btnGetTaskLists.Click += new System.EventHandler(this.btnTestTwo_Click);
            // 
            // lblTotal
            // 
            this.lblTotal.AutoSize = true;
            this.lblTotal.Location = new System.Drawing.Point(83, 456);
            this.lblTotal.Name = "lblTotal";
            this.lblTotal.Size = new System.Drawing.Size(11, 12);
            this.lblTotal.TabIndex = 9;
            this.lblTotal.Text = "0";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 456);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(71, 12);
            this.label2.TabIndex = 8;
            this.label2.Text = "Total Rows:";
            // 
            // dgvTaskListInfo
            // 
            this.dgvTaskListInfo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvTaskListInfo.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvTaskListInfo.Location = new System.Drawing.Point(3, 54);
            this.dgvTaskListInfo.Name = "dgvTaskListInfo";
            this.dgvTaskListInfo.RowTemplate.Height = 24;
            this.dgvTaskListInfo.Size = new System.Drawing.Size(744, 400);
            this.dgvTaskListInfo.TabIndex = 7;
            this.dgvTaskListInfo.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvTaskListInfo_CellClick);
            this.dgvTaskListInfo.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvTaskListInfo_CellContentClick);
            this.dgvTaskListInfo.RowPostPaint += new System.Windows.Forms.DataGridViewRowPostPaintEventHandler(this.dgvTaskListInfo_RowPostPaint);
            this.dgvTaskListInfo.Sorted += new System.EventHandler(this.dgvTaskListInfo_Sorted);
            this.dgvTaskListInfo.UserDeletedRow += new System.Windows.Forms.DataGridViewRowEventHandler(this.dgvTaskListInfo_UserDeletedRow);
            this.dgvTaskListInfo.UserDeletingRow += new System.Windows.Forms.DataGridViewRowCancelEventHandler(this.dgvTaskListInfo_UserDeletingRow);
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
            this.label1.Location = new System.Drawing.Point(3, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(743, 40);
            this.label1.TabIndex = 6;
            this.label1.Text = "Task Lists";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnGetData
            // 
            this.btnGetData.Location = new System.Drawing.Point(591, 233);
            this.btnGetData.Name = "btnGetData";
            this.btnGetData.Size = new System.Drawing.Size(116, 32);
            this.btnGetData.TabIndex = 2;
            this.btnGetData.Text = "Get";
            this.btnGetData.UseVisualStyleBackColor = true;
            this.btnGetData.Visible = false;
            this.btnGetData.Click += new System.EventHandler(this.btnGetData_Click);
            // 
            // btnGo
            // 
            this.btnGo.Location = new System.Drawing.Point(469, 221);
            this.btnGo.Name = "btnGo";
            this.btnGo.Size = new System.Drawing.Size(116, 32);
            this.btnGo.TabIndex = 1;
            this.btnGo.Text = "Go";
            this.btnGo.UseVisualStyleBackColor = true;
            this.btnGo.Visible = false;
            this.btnGo.Click += new System.EventHandler(this.btnGo_Click);
            // 
            // webBrowser1
            // 
            this.webBrowser1.Location = new System.Drawing.Point(47, 204);
            this.webBrowser1.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowser1.Name = "webBrowser1";
            this.webBrowser1.Size = new System.Drawing.Size(722, 23);
            this.webBrowser1.TabIndex = 0;
            this.webBrowser1.Visible = false;
            this.webBrowser1.DocumentCompleted += new System.Windows.Forms.WebBrowserDocumentCompletedEventHandler(this.webBrowser1_DocumentCompleted);
            // 
            // ucGetInfoFromWebPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pnlParent);
            this.Name = "ucGetInfoFromWebPage";
            this.Size = new System.Drawing.Size(751, 520);
            this.pnlParent.ResumeLayout(false);
            this.pnlParent.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTaskListInfo)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlParent;
        private System.Windows.Forms.WebBrowser webBrowser1;
        private System.Windows.Forms.Button btnGo;
        private System.Windows.Forms.Button btnGetData;
        private System.Windows.Forms.DataGridView dgvTaskListInfo;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnGetTaskLists;
        private System.Windows.Forms.ComboBox cmbGroup;
        private System.Windows.Forms.ComboBox cmbCategory;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ProgressBar pbgGetTicketListProgress;
        private System.Windows.Forms.Label lblTotal;
        private System.Windows.Forms.CheckBox chkAllDue;
        private System.Windows.Forms.CheckBox chkMesTickets;
    }
}
