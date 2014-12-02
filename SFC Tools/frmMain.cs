using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Collections;
using SFC_Tools.Resources;
using SFC_Tools.Forms;

namespace SFC_Tools
{
    public partial class frmMain : Form
    {
        private bool isConfig = true;
        private UserControl userControl;
        public frmMain()
        {
            InitializeComponent();
        }

        private void Test_Click(object sender, EventArgs e)
        {
            //MessageBox.Show(SFCStartup.dba.strTest());
            if (!(this.userControl is ucTestICT))
            {
                this.userControl.Dispose();
                this.userControl = new ucTestICT();
                this.LoadControl(userControl);
            }
            
        }

        private void LoadControl(UserControl uControl) 
        {

            //uControl.Location = this.grpContain.Location;//new System.Drawing.Point(190,7);
            uControl.Name = "userControl";
            uControl.Size = this.grpContain.Size; //new System.Drawing.Size(800, 600);//
            uControl.TabIndex = 3;
            uControl.Parent = this.grpContain;
            this.grpContain.Controls.Add(uControl);           
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            if (!File.Exists(Application.ExecutablePath + ".config"))
            {
                this.isConfig = false;
                if (DialogResult.No == MessageBox.Show("Config File Is Missing,Do you want to continue any way?", "System", MessageBoxButtons.YesNo, MessageBoxIcon.Warning))
                {
                    Application.Exit();
                }
            }
            if (this.isConfig)
            {
                this.userControl = new ucTestICT();
                this.LoadControl(userControl);
                this.statusBar.Panels[1].Text = DateTime.Today.ToShortDateString();
            }
            else
            {
                this.btnTest.Enabled = false;
                this.btnReadFile.Enabled = false;
                this.userControl = new ucAnalyseTestLog();
                this.LoadControl(userControl);
                this.statusBar.Panels[1].Text = DateTime.Today.ToShortDateString();
            }

            ArrayList arr = (ArrayList)LanguageConfig.GetLanguageList("EN");

            ToolStripMenuItem subItem;
            subItem = AddContextMenu("&Language", menuStrip1.Items, null);
            ToolStripMenuItem subItemA;
            subItemA = AddContextMenu("&LanguageChose", subItem.DropDownItems, null);
            foreach (string strTemp in arr)
            {
                AddContextMenu(strTemp, subItem.DropDownItems, shit);
            }
          
        }
        private void shit(object sender,EventArgs e)
        {
            MessageBox.Show("0.0");
        }
        ToolStripMenuItem AddContextMenu(string strText, ToolStripItemCollection cms, EventHandler callback)
        {
            if (strText == "")
            {
                ToolStripSeparator tsp = new ToolStripSeparator();
                cms.Add(tsp);
                return null;
            }
            else if(!string.IsNullOrEmpty(strText))
            {
                ToolStripMenuItem tsmi = new ToolStripMenuItem(strText);
                tsmi.Tag = Text + "TAG";
                if (callback != null)
                {
                    tsmi.Click += callback;
                }
                cms.Add(tsmi);
                return tsmi;
            }
            return null;
        }
        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        public void setStatus(int i,string strText)
        {
            this.statusBar.Panels[i].Text = "Path:"+strText;
        }

        private void btnReadFile_Click(object sender, EventArgs e)
        {
            if(!(this.userControl is ucReadFile))
            {
                this.userControl.Dispose();
                this.userControl= new ucReadFile();
                this.LoadControl(userControl);
            }
        }
        public void setBtnEnable(bool bFlag)
        {
            this.btnExit.Enabled = bFlag;
            this.btnTest.Enabled = bFlag;
        }

        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                if (!SFCStartup.dba.GetFormState())
                {
                    e.Cancel = true;
                    MessageBox.Show("Please Close Listening File Function!", "System", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    //return;
                }
            }
            catch (Exception ex)
            {
                //MessageBox.Show("Connect DataBase Error!","System",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
        }

        private void btnIctLogAnalyse_Click(object sender, EventArgs e)
        {
            if (!(this.userControl is ucAnalyseTestLog))
            {
                this.userControl.Dispose();
                this.userControl = new ucAnalyseTestLog();
                this.LoadControl(userControl);
            }
        }

        private void btnRoute_Click(object sender, EventArgs e)
        {
            if (!(this.userControl is ucRoute))
            {
                this.userControl.Dispose();
                frmLoadRoute frmLoad = new frmLoadRoute();
                this.userControl = new ucRoute(frmLoad);
                this.LoadControl(userControl);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ucRoute tt = new ucRoute();
            tt.InitData(133);
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            if (!(this.userControl is ucTest))
            {
                this.userControl.Dispose();
                userControl = new ucTest();
                this.LoadControl(userControl);
            }
        }

        private void englishToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ChineseToolStripMenuItem.Checked)
            {
                englishToolStripMenuItem.Checked = !englishToolStripMenuItem.Checked;
                ChineseToolStripMenuItem.Checked = !ChineseToolStripMenuItem.Checked;
            }
            else
                englishToolStripMenuItem.Checked = !englishToolStripMenuItem.Checked;

            getShownLang();
            this.frmMain_Load(sender, e);
        }

        private void ChineseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (englishToolStripMenuItem.Checked)
            {
                englishToolStripMenuItem.Checked = !englishToolStripMenuItem.Checked;
                ChineseToolStripMenuItem.Checked = !ChineseToolStripMenuItem.Checked;
            }
            else
                ChineseToolStripMenuItem.Checked = !ChineseToolStripMenuItem.Checked;
            getShownLang();
            this.frmMain_Load(sender, e);
        }
        private void getShownLang()
        {
            string strType="EN";
            if (englishToolStripMenuItem.Checked)
                strType = "EN";
            if (ChineseToolStripMenuItem.Checked)
                strType = "ZH";
           
            LanguageConfig.getNames(this, strType);
        }

        private void btnCurl_Click(object sender, EventArgs e)
        {
            if (!(this.userControl is ucCurl))
            {
                this.userControl.Dispose();
                userControl = new ucCurl();
                this.LoadControl(userControl);
            }
        }


        private void btnFileFormate_Click(object sender, EventArgs e)
        {
            if (!(this.userControl is uFileFormate))
            {
                this.userControl.Dispose();
                userControl = new uFileFormate();
                this.LoadControl(userControl);
            }
        }

        private void btnSuCleaner_Click(object sender, EventArgs e)
        {
            if (!(this.userControl is ucWorkLog))
            {
                this.userControl.Dispose();
                userControl = new ucWorkLog();
                this.LoadControl(userControl);
            }
        }

        private void btnOraToMySql_Click(object sender, EventArgs e)
        {
            if (!(this.userControl is ucTransTablesFormOraToMySql))
            {
                this.userControl.Dispose();
                userControl = new ucTransTablesFormOraToMySql();
                this.LoadControl(userControl);
            }
        }

        private void btnWebInfo_Click(object sender, EventArgs e)
        {
            if (!(this.userControl is ucGetInfoFromWebPage))
            {
                this.userControl.Dispose();
                userControl = new ucGetInfoFromWebPage();
                this.LoadControl(userControl);
            }
        }

        private void btnReadXMLFile_Click(object sender, EventArgs e)
        {
            if (!(this.userControl is ucXmlTest))
            {
                this.userControl.Dispose();
                userControl = new ucXmlTest();
                this.LoadControl(userControl);
            }
        }

        private void btnPCMS_Click(object sender, EventArgs e)
        {
            if (!(this.userControl is ucPCMSMaintain))
            {
                this.userControl.Dispose();
                userControl = new ucPCMSMaintain();
                this.LoadControl(userControl);
            }
        }

        private void btnMailTest_Click(object sender, EventArgs e)
        {
            if (!(this.userControl is ucMailTest))
            {
                this.userControl.Dispose();
                userControl = new ucMailTest();
                this.LoadControl(userControl);
            }
        }

        private void btnSapTest_Click(object sender, EventArgs e)
        {
            if (!(this.userControl is ucSapTest))
            {
                this.userControl.Dispose();
                userControl = new ucSapTest();
                this.LoadControl(userControl);
            }
        }

        private void btnSPAnalyse_Click(object sender, EventArgs e)
        {
            if (!(this.userControl is ucSpAnalyse))
            {
                this.userControl.Dispose();
                userControl = new ucSpAnalyse();
                this.LoadControl(userControl);
            }
        }

        private void btnWebServiceTest_Click(object sender, EventArgs e)
        {
            if (!(this.userControl is ucWebServiceTest))
            {
                this.userControl.Dispose();
                userControl = new ucWebServiceTest();
                this.LoadControl(userControl);
            }
        }

        private void btnEnDeCrypt_Click(object sender, EventArgs e)
        {
            if (!(this.userControl is ucEnDeCrypt))
            {
                this.userControl.Dispose();
                userControl = new ucEnDeCrypt();
                this.LoadControl(userControl);
            }
        }

        private void btnUserInfo_Click(object sender, EventArgs e)
        {
            if (!(this.userControl is ucGetAllPwds))
            {
                this.userControl.Dispose();
                userControl = new ucGetAllPwds();
                this.LoadControl(userControl);
            }
        }

        private void btnNewRoute_Click(object sender, EventArgs e)
        {
            if (!(this.userControl is ucNewRoute))
            {
                this.userControl.Dispose();
                userControl = new ucNewRoute();
                this.LoadControl(userControl);
            }
        }

        private void btnGDI_Click(object sender, EventArgs e)
        {
            if (!(this.userControl is ucGDITest))
            {
                this.userControl.Dispose();
                userControl = new ucGDITest();
                this.LoadControl(userControl);
            }
        }

        private void btnControl_Click(object sender, EventArgs e)
        {
            if (!(this.userControl is ucControlTest))
            {
                this.userControl.Dispose();
                userControl = new ucControlTest();
                this.LoadControl(userControl);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (!(this.userControl is ucSMOTest))
            {
                this.userControl.Dispose();
                userControl = new ucSMOTest();
                ucSMOTest ucSMO = new ucSMOTest();
                ucSMO.SetTitle = "SMO APPLICATION";
                userControl = ucSMO;
                this.LoadControl(userControl);
            }
        }

        private void btnMultiThreadCom_Click(object sender, EventArgs e)
        {
            if (!(this.userControl is ucMultiThreadCommunicate))
            {
                this.userControl.Dispose();
                userControl = new ucMultiThreadCommunicate();
                ucMultiThreadCommunicate ucThread = new ucMultiThreadCommunicate();
                ucThread.SetTitle = "MULTI THREAD COMMUNICATE TEST";
                userControl = ucThread;
                this.LoadControl(userControl);
            }
        }

        private void btnDS05_Click(object sender, EventArgs e)
        {
            if (!(this.userControl is ucDS05BomImport))
            {
                this.userControl.Dispose();
                userControl = new ucDS05BomImport();
                ucDS05BomImport ucThread = new ucDS05BomImport();
                ucThread.SetTitle = "MULTI THREAD COMMUNICATE TEST";
                userControl = ucThread;
                this.LoadControl(userControl);
            }
        }       

    }
}
