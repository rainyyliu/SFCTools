namespace SFC_Tools.Forms
{
    partial class ucSMOTest
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ucSMOTest));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.tvHostInfo = new System.Windows.Forms.TreeView();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.splitContainer3 = new System.Windows.Forms.SplitContainer();
            this.lvStationTypeInfo = new System.Windows.Forms.ListView();
            this.lvScanInfo = new System.Windows.Forms.ListView();
            this.splitContainer4 = new System.Windows.Forms.SplitContainer();
            this.lvWorkTypeInfo = new System.Windows.Forms.ListView();
            this.lvTerminalInfo = new System.Windows.Forms.ListView();
            this.imgLstTreeView = new System.Windows.Forms.ImageList(this.components);
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tsbtnRefresh = new System.Windows.Forms.ToolStripButton();
            this.tsbtnStart = new System.Windows.Forms.ToolStripButton();
            this.tsbtnPause = new System.Windows.Forms.ToolStripButton();
            this.tsbtnStop = new System.Windows.Forms.ToolStripButton();
            this.pageSetupDialog1 = new System.Windows.Forms.PageSetupDialog();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.splitContainer3.Panel1.SuspendLayout();
            this.splitContainer3.Panel2.SuspendLayout();
            this.splitContainer3.SuspendLayout();
            this.splitContainer4.Panel1.SuspendLayout();
            this.splitContainer4.Panel2.SuspendLayout();
            this.splitContainer4.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Location = new System.Drawing.Point(16, 83);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.tvHostInfo);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
            this.splitContainer1.Size = new System.Drawing.Size(746, 425);
            this.splitContainer1.SplitterDistance = 223;
            this.splitContainer1.TabIndex = 12;
            // 
            // tvHostInfo
            // 
            this.tvHostInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tvHostInfo.Location = new System.Drawing.Point(0, 0);
            this.tvHostInfo.Name = "tvHostInfo";
            this.tvHostInfo.Size = new System.Drawing.Size(223, 425);
            this.tvHostInfo.TabIndex = 0;
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.splitContainer3);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.splitContainer4);
            this.splitContainer2.Size = new System.Drawing.Size(519, 425);
            this.splitContainer2.SplitterDistance = 165;
            this.splitContainer2.TabIndex = 0;
            // 
            // splitContainer3
            // 
            this.splitContainer3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer3.Location = new System.Drawing.Point(0, 0);
            this.splitContainer3.Name = "splitContainer3";
            // 
            // splitContainer3.Panel1
            // 
            this.splitContainer3.Panel1.Controls.Add(this.lvStationTypeInfo);
            // 
            // splitContainer3.Panel2
            // 
            this.splitContainer3.Panel2.Controls.Add(this.lvScanInfo);
            this.splitContainer3.Size = new System.Drawing.Size(519, 165);
            this.splitContainer3.SplitterDistance = 267;
            this.splitContainer3.TabIndex = 0;
            // 
            // lvStationTypeInfo
            // 
            this.lvStationTypeInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvStationTypeInfo.GridLines = true;
            this.lvStationTypeInfo.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lvStationTypeInfo.Location = new System.Drawing.Point(0, 0);
            this.lvStationTypeInfo.Name = "lvStationTypeInfo";
            this.lvStationTypeInfo.Size = new System.Drawing.Size(267, 165);
            this.lvStationTypeInfo.TabIndex = 0;
            this.lvStationTypeInfo.UseCompatibleStateImageBehavior = false;
            // 
            // lvScanInfo
            // 
            this.lvScanInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvScanInfo.Location = new System.Drawing.Point(0, 0);
            this.lvScanInfo.Name = "lvScanInfo";
            this.lvScanInfo.Size = new System.Drawing.Size(248, 165);
            this.lvScanInfo.TabIndex = 0;
            this.lvScanInfo.UseCompatibleStateImageBehavior = false;
            // 
            // splitContainer4
            // 
            this.splitContainer4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer4.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.splitContainer4.Location = new System.Drawing.Point(0, 0);
            this.splitContainer4.Name = "splitContainer4";
            this.splitContainer4.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer4.Panel1
            // 
            this.splitContainer4.Panel1.Controls.Add(this.lvWorkTypeInfo);
            // 
            // splitContainer4.Panel2
            // 
            this.splitContainer4.Panel2.Controls.Add(this.lvTerminalInfo);
            this.splitContainer4.Size = new System.Drawing.Size(519, 256);
            this.splitContainer4.SplitterDistance = 85;
            this.splitContainer4.TabIndex = 0;
            // 
            // lvWorkTypeInfo
            // 
            this.lvWorkTypeInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvWorkTypeInfo.Location = new System.Drawing.Point(0, 0);
            this.lvWorkTypeInfo.Name = "lvWorkTypeInfo";
            this.lvWorkTypeInfo.Size = new System.Drawing.Size(519, 85);
            this.lvWorkTypeInfo.TabIndex = 0;
            this.lvWorkTypeInfo.UseCompatibleStateImageBehavior = false;
            // 
            // lvTerminalInfo
            // 
            this.lvTerminalInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvTerminalInfo.Location = new System.Drawing.Point(0, 0);
            this.lvTerminalInfo.Name = "lvTerminalInfo";
            this.lvTerminalInfo.Size = new System.Drawing.Size(519, 167);
            this.lvTerminalInfo.TabIndex = 0;
            this.lvTerminalInfo.UseCompatibleStateImageBehavior = false;
            this.lvTerminalInfo.DrawColumnHeader += new System.Windows.Forms.DrawListViewColumnHeaderEventHandler(this.lvTerminalInfo_DrawColumnHeader);
            this.lvTerminalInfo.DrawItem += new System.Windows.Forms.DrawListViewItemEventHandler(this.lvTerminalInfo_DrawItem);
            this.lvTerminalInfo.DrawSubItem += new System.Windows.Forms.DrawListViewSubItemEventHandler(this.lvTerminalInfo_DrawSubItem);
            // 
            // imgLstTreeView
            // 
            this.imgLstTreeView.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imgLstTreeView.ImageStream")));
            this.imgLstTreeView.TransparentColor = System.Drawing.Color.Transparent;
            this.imgLstTreeView.Images.SetKeyName(0, "SMO2000.ICO");
            this.imgLstTreeView.Images.SetKeyName(1, "Line.ico");
            this.imgLstTreeView.Images.SetKeyName(2, "Section.ico");
            this.imgLstTreeView.Images.SetKeyName(3, "Group.ICO");
            this.imgLstTreeView.Images.SetKeyName(4, "icoStation.ico");
            this.imgLstTreeView.Images.SetKeyName(5, "bitmap2.bmp");
            this.imgLstTreeView.Images.SetKeyName(6, "iconSend.ico");
            this.imgLstTreeView.Images.SetKeyName(7, "icoReceive.ico");
            // 
            // toolStrip1
            // 
            this.toolStrip1.Dock = System.Windows.Forms.DockStyle.None;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbtnRefresh,
            this.tsbtnStart,
            this.tsbtnPause,
            this.tsbtnStop});
            this.toolStrip1.Location = new System.Drawing.Point(16, 58);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.toolStrip1.Size = new System.Drawing.Size(104, 25);
            this.toolStrip1.TabIndex = 13;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // tsbtnRefresh
            // 
            this.tsbtnRefresh.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbtnRefresh.Image = ((System.Drawing.Image)(resources.GetObject("tsbtnRefresh.Image")));
            this.tsbtnRefresh.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnRefresh.Name = "tsbtnRefresh";
            this.tsbtnRefresh.Size = new System.Drawing.Size(23, 22);
            this.tsbtnRefresh.Text = "toolStripButton1";
            // 
            // tsbtnStart
            // 
            this.tsbtnStart.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbtnStart.Image = ((System.Drawing.Image)(resources.GetObject("tsbtnStart.Image")));
            this.tsbtnStart.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnStart.Name = "tsbtnStart";
            this.tsbtnStart.Size = new System.Drawing.Size(23, 22);
            this.tsbtnStart.Click += new System.EventHandler(this.tsbtnStart_Click);
            // 
            // tsbtnPause
            // 
            this.tsbtnPause.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbtnPause.Image = ((System.Drawing.Image)(resources.GetObject("tsbtnPause.Image")));
            this.tsbtnPause.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnPause.Name = "tsbtnPause";
            this.tsbtnPause.Size = new System.Drawing.Size(23, 22);
            this.tsbtnPause.Text = "toolStripButton3";
            this.tsbtnPause.Click += new System.EventHandler(this.tsbtnPause_Click);
            // 
            // tsbtnStop
            // 
            this.tsbtnStop.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbtnStop.Image = global::SFC_Tools.Properties.Resources.Stop1;
            this.tsbtnStop.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnStop.Name = "tsbtnStop";
            this.tsbtnStop.Size = new System.Drawing.Size(23, 22);
            this.tsbtnStop.Text = "toolStripButton4";
            this.tsbtnStop.Click += new System.EventHandler(this.tsbtnStop_Click);
            // 
            // ucSMOTest
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.splitContainer1);
            this.Name = "ucSMOTest";
            this.Size = new System.Drawing.Size(785, 518);
            this.Load += new System.EventHandler(this.ucSMOTest_Load);
            this.Controls.SetChildIndex(this.splitContainer1, 0);
            this.Controls.SetChildIndex(this.toolStrip1, 0);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            this.splitContainer2.ResumeLayout(false);
            this.splitContainer3.Panel1.ResumeLayout(false);
            this.splitContainer3.Panel2.ResumeLayout(false);
            this.splitContainer3.ResumeLayout(false);
            this.splitContainer4.Panel1.ResumeLayout(false);
            this.splitContainer4.Panel2.ResumeLayout(false);
            this.splitContainer4.ResumeLayout(false);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TreeView tvHostInfo;
        private System.Windows.Forms.ImageList imgLstTreeView;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.SplitContainer splitContainer3;
        private System.Windows.Forms.SplitContainer splitContainer4;
        private System.Windows.Forms.ListView lvStationTypeInfo;
        private System.Windows.Forms.ListView lvScanInfo;
        private System.Windows.Forms.ListView lvWorkTypeInfo;
        private System.Windows.Forms.ListView lvTerminalInfo;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton tsbtnRefresh;
        private System.Windows.Forms.ToolStripButton tsbtnStart;
        private System.Windows.Forms.ToolStripButton tsbtnPause;
        private System.Windows.Forms.ToolStripButton tsbtnStop;
        private System.Windows.Forms.PageSetupDialog pageSetupDialog1;
    }
}
