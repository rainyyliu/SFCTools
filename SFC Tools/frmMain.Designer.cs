namespace SFC_Tools
{
    partial class frmMain
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.toolStripContainer1 = new System.Windows.Forms.ToolStripContainer();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.systemToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.testSMOToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.languageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.englishToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ChineseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusBar = new System.Windows.Forms.StatusBar();
            this.pnlPath = new System.Windows.Forms.StatusBarPanel();
            this.others = new System.Windows.Forms.StatusBarPanel();
            this.pnlAuthor = new System.Windows.Forms.StatusBarPanel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnExit = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.btnFileFormate = new System.Windows.Forms.Button();
            this.btnCurl = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.btnRoute = new System.Windows.Forms.Button();
            this.btnIctLogAnalyse = new System.Windows.Forms.Button();
            this.btnReadFile = new System.Windows.Forms.Button();
            this.btnTest = new System.Windows.Forms.Button();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.btnSapTest = new System.Windows.Forms.Button();
            this.btnMailTest = new System.Windows.Forms.Button();
            this.btnPCMS = new System.Windows.Forms.Button();
            this.btnReadXMLFile = new System.Windows.Forms.Button();
            this.btnWebInfo = new System.Windows.Forms.Button();
            this.btnOraToMySql = new System.Windows.Forms.Button();
            this.btnSuCleaner = new System.Windows.Forms.Button();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.btnUserInfo = new System.Windows.Forms.Button();
            this.btnEnDeCrypt = new System.Windows.Forms.Button();
            this.btnWebServiceTest = new System.Windows.Forms.Button();
            this.btnSPAnalyse = new System.Windows.Forms.Button();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.btnMultiThreadCom = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.btnControl = new System.Windows.Forms.Button();
            this.btnGDI = new System.Windows.Forms.Button();
            this.btnNewRoute = new System.Windows.Forms.Button();
            this.grpContain = new System.Windows.Forms.GroupBox();
            this.btnDS05 = new System.Windows.Forms.Button();
            this.toolStripContainer1.TopToolStripPanel.SuspendLayout();
            this.toolStripContainer1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlPath)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.others)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlAuthor)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.tabPage4.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStripContainer1
            // 
            // 
            // toolStripContainer1.ContentPanel
            // 
            resources.ApplyResources(this.toolStripContainer1.ContentPanel, "toolStripContainer1.ContentPanel");
            resources.ApplyResources(this.toolStripContainer1, "toolStripContainer1");
            this.toolStripContainer1.Name = "toolStripContainer1";
            // 
            // toolStripContainer1.TopToolStripPanel
            // 
            this.toolStripContainer1.TopToolStripPanel.Controls.Add(this.menuStrip1);
            // 
            // menuStrip1
            // 
            resources.ApplyResources(this.menuStrip1, "menuStrip1");
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.systemToolStripMenuItem});
            this.menuStrip1.Name = "menuStrip1";
            // 
            // systemToolStripMenuItem
            // 
            this.systemToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.testSMOToolStripMenuItem,
            this.languageToolStripMenuItem});
            this.systemToolStripMenuItem.Name = "systemToolStripMenuItem";
            resources.ApplyResources(this.systemToolStripMenuItem, "systemToolStripMenuItem");
            // 
            // testSMOToolStripMenuItem
            // 
            this.testSMOToolStripMenuItem.Name = "testSMOToolStripMenuItem";
            resources.ApplyResources(this.testSMOToolStripMenuItem, "testSMOToolStripMenuItem");
            // 
            // languageToolStripMenuItem
            // 
            this.languageToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.englishToolStripMenuItem,
            this.ChineseToolStripMenuItem});
            this.languageToolStripMenuItem.Name = "languageToolStripMenuItem";
            resources.ApplyResources(this.languageToolStripMenuItem, "languageToolStripMenuItem");
            // 
            // englishToolStripMenuItem
            // 
            this.englishToolStripMenuItem.Checked = true;
            this.englishToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.englishToolStripMenuItem.Name = "englishToolStripMenuItem";
            resources.ApplyResources(this.englishToolStripMenuItem, "englishToolStripMenuItem");
            this.englishToolStripMenuItem.Click += new System.EventHandler(this.englishToolStripMenuItem_Click);
            // 
            // ChineseToolStripMenuItem
            // 
            this.ChineseToolStripMenuItem.Name = "ChineseToolStripMenuItem";
            resources.ApplyResources(this.ChineseToolStripMenuItem, "ChineseToolStripMenuItem");
            this.ChineseToolStripMenuItem.Click += new System.EventHandler(this.ChineseToolStripMenuItem_Click);
            // 
            // statusBar
            // 
            resources.ApplyResources(this.statusBar, "statusBar");
            this.statusBar.Name = "statusBar";
            this.statusBar.Panels.AddRange(new System.Windows.Forms.StatusBarPanel[] {
            this.pnlPath,
            this.others,
            this.pnlAuthor});
            this.statusBar.ShowPanels = true;
            // 
            // pnlPath
            // 
            resources.ApplyResources(this.pnlPath, "pnlPath");
            // 
            // others
            // 
            resources.ApplyResources(this.others, "others");
            // 
            // pnlAuthor
            // 
            this.pnlAuthor.AutoSize = System.Windows.Forms.StatusBarPanelAutoSize.Spring;
            resources.ApplyResources(this.pnlAuthor, "pnlAuthor");
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnExit);
            this.groupBox1.Controls.Add(this.tabControl1);
            resources.ApplyResources(this.groupBox1, "groupBox1");
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.TabStop = false;
            // 
            // btnExit
            // 
            this.btnExit.BackColor = System.Drawing.Color.LightSteelBlue;
            resources.ApplyResources(this.btnExit, "btnExit");
            this.btnExit.ForeColor = System.Drawing.Color.Blue;
            this.btnExit.Name = "btnExit";
            this.btnExit.UseVisualStyleBackColor = false;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // tabControl1
            // 
            resources.ApplyResources(this.tabControl1, "tabControl1");
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Controls.Add(this.tabPage4);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.SystemColors.Control;
            this.tabPage1.Controls.Add(this.btnFileFormate);
            this.tabPage1.Controls.Add(this.btnCurl);
            this.tabPage1.Controls.Add(this.button1);
            this.tabPage1.Controls.Add(this.btnRoute);
            this.tabPage1.Controls.Add(this.btnIctLogAnalyse);
            this.tabPage1.Controls.Add(this.btnReadFile);
            this.tabPage1.Controls.Add(this.btnTest);
            resources.ApplyResources(this.tabPage1, "tabPage1");
            this.tabPage1.Name = "tabPage1";
            // 
            // btnFileFormate
            // 
            this.btnFileFormate.BackColor = System.Drawing.Color.LightSteelBlue;
            resources.ApplyResources(this.btnFileFormate, "btnFileFormate");
            this.btnFileFormate.ForeColor = System.Drawing.Color.Blue;
            this.btnFileFormate.Image = global::SFC_Tools.Properties.Resources.ART_3_2;
            this.btnFileFormate.Name = "btnFileFormate";
            this.btnFileFormate.UseVisualStyleBackColor = false;
            this.btnFileFormate.Click += new System.EventHandler(this.btnFileFormate_Click);
            // 
            // btnCurl
            // 
            this.btnCurl.BackColor = System.Drawing.Color.LightSteelBlue;
            resources.ApplyResources(this.btnCurl, "btnCurl");
            this.btnCurl.ForeColor = System.Drawing.Color.Blue;
            this.btnCurl.Image = global::SFC_Tools.Properties.Resources.ART_3_5;
            this.btnCurl.Name = "btnCurl";
            this.btnCurl.UseVisualStyleBackColor = false;
            this.btnCurl.Click += new System.EventHandler(this.btnCurl_Click);
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.LightSteelBlue;
            resources.ApplyResources(this.button1, "button1");
            this.button1.ForeColor = System.Drawing.Color.Blue;
            this.button1.Image = global::SFC_Tools.Properties.Resources.ART_3_1;
            this.button1.Name = "button1";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // btnRoute
            // 
            this.btnRoute.BackColor = System.Drawing.Color.LightSteelBlue;
            resources.ApplyResources(this.btnRoute, "btnRoute");
            this.btnRoute.ForeColor = System.Drawing.Color.Blue;
            this.btnRoute.Image = global::SFC_Tools.Properties.Resources.ART_3_3;
            this.btnRoute.Name = "btnRoute";
            this.btnRoute.UseVisualStyleBackColor = false;
            this.btnRoute.Click += new System.EventHandler(this.btnRoute_Click);
            // 
            // btnIctLogAnalyse
            // 
            this.btnIctLogAnalyse.BackColor = System.Drawing.Color.LightSteelBlue;
            resources.ApplyResources(this.btnIctLogAnalyse, "btnIctLogAnalyse");
            this.btnIctLogAnalyse.ForeColor = System.Drawing.Color.Blue;
            this.btnIctLogAnalyse.Image = global::SFC_Tools.Properties.Resources.ART_3_8;
            this.btnIctLogAnalyse.Name = "btnIctLogAnalyse";
            this.btnIctLogAnalyse.UseVisualStyleBackColor = false;
            this.btnIctLogAnalyse.Click += new System.EventHandler(this.btnIctLogAnalyse_Click);
            // 
            // btnReadFile
            // 
            this.btnReadFile.BackColor = System.Drawing.Color.LightSteelBlue;
            resources.ApplyResources(this.btnReadFile, "btnReadFile");
            this.btnReadFile.ForeColor = System.Drawing.Color.Blue;
            this.btnReadFile.Image = global::SFC_Tools.Properties.Resources.ART_3_13;
            this.btnReadFile.Name = "btnReadFile";
            this.btnReadFile.UseVisualStyleBackColor = false;
            this.btnReadFile.Click += new System.EventHandler(this.btnReadFile_Click);
            // 
            // btnTest
            // 
            this.btnTest.BackColor = System.Drawing.Color.LightSteelBlue;
            resources.ApplyResources(this.btnTest, "btnTest");
            this.btnTest.ForeColor = System.Drawing.Color.Blue;
            this.btnTest.Name = "btnTest";
            this.btnTest.UseVisualStyleBackColor = false;
            this.btnTest.Click += new System.EventHandler(this.Test_Click);
            // 
            // tabPage2
            // 
            this.tabPage2.BackColor = System.Drawing.SystemColors.Control;
            this.tabPage2.Controls.Add(this.btnSapTest);
            this.tabPage2.Controls.Add(this.btnMailTest);
            this.tabPage2.Controls.Add(this.btnPCMS);
            this.tabPage2.Controls.Add(this.btnReadXMLFile);
            this.tabPage2.Controls.Add(this.btnWebInfo);
            this.tabPage2.Controls.Add(this.btnOraToMySql);
            this.tabPage2.Controls.Add(this.btnSuCleaner);
            resources.ApplyResources(this.tabPage2, "tabPage2");
            this.tabPage2.Name = "tabPage2";
            // 
            // btnSapTest
            // 
            this.btnSapTest.BackColor = System.Drawing.Color.LightSteelBlue;
            resources.ApplyResources(this.btnSapTest, "btnSapTest");
            this.btnSapTest.ForeColor = System.Drawing.Color.Blue;
            this.btnSapTest.Image = global::SFC_Tools.Properties.Resources.Only;
            this.btnSapTest.Name = "btnSapTest";
            this.btnSapTest.UseVisualStyleBackColor = false;
            this.btnSapTest.Click += new System.EventHandler(this.btnSapTest_Click);
            // 
            // btnMailTest
            // 
            this.btnMailTest.BackColor = System.Drawing.Color.LightSteelBlue;
            resources.ApplyResources(this.btnMailTest, "btnMailTest");
            this.btnMailTest.ForeColor = System.Drawing.Color.Blue;
            this.btnMailTest.Image = global::SFC_Tools.Properties.Resources.NAV1A;
            this.btnMailTest.Name = "btnMailTest";
            this.btnMailTest.UseVisualStyleBackColor = false;
            this.btnMailTest.Click += new System.EventHandler(this.btnMailTest_Click);
            // 
            // btnPCMS
            // 
            this.btnPCMS.BackColor = System.Drawing.Color.LightSteelBlue;
            resources.ApplyResources(this.btnPCMS, "btnPCMS");
            this.btnPCMS.ForeColor = System.Drawing.Color.Blue;
            this.btnPCMS.Image = global::SFC_Tools.Properties.Resources.ART_3_5;
            this.btnPCMS.Name = "btnPCMS";
            this.btnPCMS.UseVisualStyleBackColor = false;
            this.btnPCMS.Click += new System.EventHandler(this.btnPCMS_Click);
            // 
            // btnReadXMLFile
            // 
            this.btnReadXMLFile.BackColor = System.Drawing.Color.LightSteelBlue;
            resources.ApplyResources(this.btnReadXMLFile, "btnReadXMLFile");
            this.btnReadXMLFile.ForeColor = System.Drawing.Color.Blue;
            this.btnReadXMLFile.Image = global::SFC_Tools.Properties.Resources.PLAYII;
            this.btnReadXMLFile.Name = "btnReadXMLFile";
            this.btnReadXMLFile.UseVisualStyleBackColor = false;
            this.btnReadXMLFile.Click += new System.EventHandler(this.btnReadXMLFile_Click);
            // 
            // btnWebInfo
            // 
            this.btnWebInfo.BackColor = System.Drawing.Color.LightSteelBlue;
            resources.ApplyResources(this.btnWebInfo, "btnWebInfo");
            this.btnWebInfo.ForeColor = System.Drawing.Color.Blue;
            this.btnWebInfo.Name = "btnWebInfo";
            this.btnWebInfo.UseVisualStyleBackColor = false;
            this.btnWebInfo.Click += new System.EventHandler(this.btnWebInfo_Click);
            // 
            // btnOraToMySql
            // 
            this.btnOraToMySql.BackColor = System.Drawing.Color.LightSteelBlue;
            resources.ApplyResources(this.btnOraToMySql, "btnOraToMySql");
            this.btnOraToMySql.ForeColor = System.Drawing.Color.Blue;
            this.btnOraToMySql.Name = "btnOraToMySql";
            this.btnOraToMySql.UseVisualStyleBackColor = false;
            this.btnOraToMySql.Click += new System.EventHandler(this.btnOraToMySql_Click);
            // 
            // btnSuCleaner
            // 
            this.btnSuCleaner.BackColor = System.Drawing.Color.LightSteelBlue;
            resources.ApplyResources(this.btnSuCleaner, "btnSuCleaner");
            this.btnSuCleaner.ForeColor = System.Drawing.Color.Blue;
            this.btnSuCleaner.Name = "btnSuCleaner";
            this.btnSuCleaner.UseVisualStyleBackColor = false;
            this.btnSuCleaner.Click += new System.EventHandler(this.btnSuCleaner_Click);
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.btnUserInfo);
            this.tabPage3.Controls.Add(this.btnEnDeCrypt);
            this.tabPage3.Controls.Add(this.btnWebServiceTest);
            this.tabPage3.Controls.Add(this.btnSPAnalyse);
            resources.ApplyResources(this.tabPage3, "tabPage3");
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // btnUserInfo
            // 
            this.btnUserInfo.BackColor = System.Drawing.Color.LightSteelBlue;
            resources.ApplyResources(this.btnUserInfo, "btnUserInfo");
            this.btnUserInfo.ForeColor = System.Drawing.Color.Blue;
            this.btnUserInfo.Image = global::SFC_Tools.Properties.Resources.ART_3_12;
            this.btnUserInfo.Name = "btnUserInfo";
            this.btnUserInfo.UseVisualStyleBackColor = false;
            this.btnUserInfo.Click += new System.EventHandler(this.btnUserInfo_Click);
            // 
            // btnEnDeCrypt
            // 
            this.btnEnDeCrypt.BackColor = System.Drawing.Color.LightSteelBlue;
            resources.ApplyResources(this.btnEnDeCrypt, "btnEnDeCrypt");
            this.btnEnDeCrypt.ForeColor = System.Drawing.Color.Blue;
            this.btnEnDeCrypt.Image = global::SFC_Tools.Properties.Resources.ART_3_8;
            this.btnEnDeCrypt.Name = "btnEnDeCrypt";
            this.btnEnDeCrypt.UseVisualStyleBackColor = false;
            this.btnEnDeCrypt.Click += new System.EventHandler(this.btnEnDeCrypt_Click);
            // 
            // btnWebServiceTest
            // 
            this.btnWebServiceTest.BackColor = System.Drawing.Color.LightSteelBlue;
            resources.ApplyResources(this.btnWebServiceTest, "btnWebServiceTest");
            this.btnWebServiceTest.ForeColor = System.Drawing.Color.Blue;
            this.btnWebServiceTest.Image = global::SFC_Tools.Properties.Resources.NAV1A;
            this.btnWebServiceTest.Name = "btnWebServiceTest";
            this.btnWebServiceTest.UseVisualStyleBackColor = false;
            this.btnWebServiceTest.Click += new System.EventHandler(this.btnWebServiceTest_Click);
            // 
            // btnSPAnalyse
            // 
            this.btnSPAnalyse.BackColor = System.Drawing.Color.LightSteelBlue;
            resources.ApplyResources(this.btnSPAnalyse, "btnSPAnalyse");
            this.btnSPAnalyse.ForeColor = System.Drawing.Color.Blue;
            this.btnSPAnalyse.Name = "btnSPAnalyse";
            this.btnSPAnalyse.UseVisualStyleBackColor = false;
            this.btnSPAnalyse.Click += new System.EventHandler(this.btnSPAnalyse_Click);
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.btnDS05);
            this.tabPage4.Controls.Add(this.btnMultiThreadCom);
            this.tabPage4.Controls.Add(this.button2);
            this.tabPage4.Controls.Add(this.btnControl);
            this.tabPage4.Controls.Add(this.btnGDI);
            this.tabPage4.Controls.Add(this.btnNewRoute);
            resources.ApplyResources(this.tabPage4, "tabPage4");
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // btnMultiThreadCom
            // 
            this.btnMultiThreadCom.BackColor = System.Drawing.Color.LightSteelBlue;
            resources.ApplyResources(this.btnMultiThreadCom, "btnMultiThreadCom");
            this.btnMultiThreadCom.ForeColor = System.Drawing.Color.Blue;
            this.btnMultiThreadCom.Image = global::SFC_Tools.Properties.Resources.SMO;
            this.btnMultiThreadCom.Name = "btnMultiThreadCom";
            this.btnMultiThreadCom.UseVisualStyleBackColor = false;
            this.btnMultiThreadCom.Click += new System.EventHandler(this.btnMultiThreadCom_Click);
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.LightSteelBlue;
            resources.ApplyResources(this.button2, "button2");
            this.button2.ForeColor = System.Drawing.Color.Blue;
            this.button2.Image = global::SFC_Tools.Properties.Resources.SMO;
            this.button2.Name = "button2";
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // btnControl
            // 
            this.btnControl.BackColor = System.Drawing.Color.LightSteelBlue;
            resources.ApplyResources(this.btnControl, "btnControl");
            this.btnControl.ForeColor = System.Drawing.Color.Blue;
            this.btnControl.Image = global::SFC_Tools.Properties.Resources.ART_3_13;
            this.btnControl.Name = "btnControl";
            this.btnControl.UseVisualStyleBackColor = false;
            this.btnControl.Click += new System.EventHandler(this.btnControl_Click);
            // 
            // btnGDI
            // 
            this.btnGDI.BackColor = System.Drawing.Color.LightSteelBlue;
            resources.ApplyResources(this.btnGDI, "btnGDI");
            this.btnGDI.ForeColor = System.Drawing.Color.Blue;
            this.btnGDI.Image = global::SFC_Tools.Properties.Resources.ART_3_12;
            this.btnGDI.Name = "btnGDI";
            this.btnGDI.UseVisualStyleBackColor = false;
            this.btnGDI.Click += new System.EventHandler(this.btnGDI_Click);
            // 
            // btnNewRoute
            // 
            this.btnNewRoute.BackColor = System.Drawing.Color.LightSteelBlue;
            resources.ApplyResources(this.btnNewRoute, "btnNewRoute");
            this.btnNewRoute.ForeColor = System.Drawing.Color.Blue;
            this.btnNewRoute.Name = "btnNewRoute";
            this.btnNewRoute.UseVisualStyleBackColor = false;
            this.btnNewRoute.Click += new System.EventHandler(this.btnNewRoute_Click);
            // 
            // grpContain
            // 
            resources.ApplyResources(this.grpContain, "grpContain");
            this.grpContain.Name = "grpContain";
            this.grpContain.TabStop = false;
            // 
            // btnDS05
            // 
            this.btnDS05.BackColor = System.Drawing.Color.LightSteelBlue;
            resources.ApplyResources(this.btnDS05, "btnDS05");
            this.btnDS05.ForeColor = System.Drawing.Color.Blue;
            this.btnDS05.Image = global::SFC_Tools.Properties.Resources.SMO;
            this.btnDS05.Name = "btnDS05";
            this.btnDS05.UseVisualStyleBackColor = false;
            this.btnDS05.Click += new System.EventHandler(this.btnDS05_Click);
            // 
            // frmMain
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.grpContain);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.statusBar);
            this.Controls.Add(this.toolStripContainer1);
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.Name = "frmMain";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmMain_FormClosing);
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.toolStripContainer1.TopToolStripPanel.ResumeLayout(false);
            this.toolStripContainer1.TopToolStripPanel.PerformLayout();
            this.toolStripContainer1.ResumeLayout(false);
            this.toolStripContainer1.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlPath)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.others)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlAuthor)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            this.tabPage4.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.StatusBar statusBar;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem systemToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem testSMOToolStripMenuItem;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.Button btnTest;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.GroupBox grpContain;
        private System.Windows.Forms.StatusBarPanel pnlPath;
        private System.Windows.Forms.StatusBarPanel others;
        private System.Windows.Forms.StatusBarPanel pnlAuthor;
        private System.Windows.Forms.Button btnReadFile;
        private System.Windows.Forms.Button btnIctLogAnalyse;
        private System.Windows.Forms.Button btnRoute;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.ToolStripMenuItem languageToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem englishToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ChineseToolStripMenuItem;
        private System.Windows.Forms.ToolStripContainer toolStripContainer1;
        private System.Windows.Forms.Button btnCurl;
        private System.Windows.Forms.Button btnFileFormate;
        private System.Windows.Forms.Button btnSuCleaner;
        private System.Windows.Forms.Button btnOraToMySql;
        private System.Windows.Forms.Button btnWebInfo;
        private System.Windows.Forms.Button btnReadXMLFile;
        private System.Windows.Forms.Button btnPCMS;
        private System.Windows.Forms.Button btnMailTest;
        private System.Windows.Forms.Button btnSapTest;
        private System.Windows.Forms.Button btnSPAnalyse;
        private System.Windows.Forms.Button btnWebServiceTest;
        private System.Windows.Forms.Button btnEnDeCrypt;
        private System.Windows.Forms.Button btnUserInfo;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.Button btnNewRoute;
        private System.Windows.Forms.Button btnGDI;
        private System.Windows.Forms.Button btnControl;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button btnMultiThreadCom;
        private System.Windows.Forms.Button btnDS05;
    }
}

