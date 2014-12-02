using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Threading;

namespace SFC_Tools
{
  
   
    public partial class ucTestICT : UserControl
    {
        Thread tinserData;
        private int myFlag=0;
        delegate void showProgressDelegate(int newPos);
        private bool isCreateFile = false;
        private DataSet dsSn;
        private DataTable dtSn;
        public  void myShowProgress(int newPos)
        {
            if (!this.pgInsert.InvokeRequired)
            {
                pgInsert.Value = newPos;
                if (this.isCreateFile)
                {
                    if (pgInsert.Value > pgInsert.Minimum)
                    {
                        this.btnInput.Enabled = false;
                        this.btnStart.Enabled = false;
                    }
                    if (pgInsert.Value == pgInsert.Maximum)
                    {
                        this.btnInput.Enabled = true;
                        this.btnStart.Enabled = true;
                    }
                }
                else
                {
                    pgInsert.Value = 0;
                    btnInput.Enabled = true;
                    btnStart.Enabled = true;
                }
            }
            else
            {
                showProgressDelegate sp = new showProgressDelegate(myShowProgress);
                this.BeginInvoke(sp, new object[] { newPos });
            }
        }
       
        public ucTestICT()
        {
            InitializeComponent();
        }

        private void btnPath_Click(object sender, EventArgs e)
        {
            if (this.folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                txtPath.Text = folderBrowserDialog.SelectedPath;
                frmMain fMain = (frmMain)this.Parent.Parent;
                fMain.setStatus(0,txtPath.Text);
            }
        }

        private void btnEnd_Click(object sender, EventArgs e)
        {
            this.isCreateFile = false;
            txtPath.Enabled = true;
            this.txtEmp.Enabled = true;
            this.txtMachine.Enabled = true;
            this.txtLine.Enabled = true;
            this.txtResult.Enabled = true;
            this.txtMo.Enabled = true;
            this.btnInput.Enabled = true;
            this.pgInsert.Value = 0;
            bGetRandomResult(0);
        }
        private int AnalyseData()
        {
            int iFlag=0;
            switch (cmbAbnormity.Text.Trim())
            {
                case "0":
                    iFlag = 0;
                    break;
                case "10%":
                    iFlag = 1;
                    break;
                case "20%":
                    iFlag = 2;
                    break;
                case "30%":
                    iFlag = 3;
                    break;
                case "40%":
                    iFlag = 4;
                    break;
                case "50%":
                    iFlag =5;
                    break;
                case "60%":
                    iFlag = 6;
                    break;
                case "70%":
                    iFlag = 7;
                    break;
                case "80%":
                    iFlag = 8;
                    break;
                case "90%":
                    iFlag =9;
                    break;
                default:
                    iFlag = 10;
                    break;
            }
            return iFlag;
        }
        private void btnStart_Click(object sender, EventArgs e)
        {
            if (cmbAbnormity.Text.Trim() == "0")
            {
                this.myFlag = 0;
            }
            else
            {
                this.myFlag = AnalyseData();
            }
           
            if (txtPath.Text.Trim().Length != 0)
            {
                txtPath.Enabled = false;
            }
            else
            {
                MessageBox.Show("Please Input a Path!", "System", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPath.Text = "";
                MysetFocus();
                return;
            }
            this.txtEmp.Enabled = false;
            this.txtMachine.Enabled = false;
            this.txtLine.Enabled = false;
            this.txtResult.Enabled = false;
            this.txtMo.Enabled = false;
            tinserData = new Thread(new ThreadStart(CreateTestFileProc));
            tinserData.IsBackground = true;
            tinserData.Start();
        }
        delegate void setFocusDelegate();
        private void MysetFocus()
        {
            if (!this.txtPath.InvokeRequired)
            {
                this.txtPath.Focus();
            }
            else
            {
                setFocusDelegate sfd = new setFocusDelegate(MysetFocus);
                this.BeginInvoke(sfd,new object[]{});
            }
        }
        
        private void CreateTestFileProc()
        {
           this.isCreateFile = true;
           dsSn = SFCStartup.dba.GetInitData(this.txtMo.Text.Trim(), " AND GROUP_NAME='P_MAC'");
           dtSn = dsSn.Tables["WIP_TRACKING_T"];

            myShowProgress(0);
            for (int i = 0; i < dtSn.Rows.Count - 1; i++)
            {
                myShowProgress(i);
                if (this.isCreateFile)
                {
                    string strSnName = dtSn.Rows[i]["SERIAL_NUMBER"].ToString();
                    string strMac = SFCStartup.dba.GetLinkMac(strSnName);
                    string strMachine = txtLine.Text + "&" + txtMachine.Text;
                    string strEmp = txtEmp.Text;
                    string strDate = SFCStartup.dba.GetServerDate("yyyy-mm-dd");
                    string strTime = SFCStartup.dba.GetServerDate("hh24:mm:ss");
                    string strResult;
                    if (this.myFlag == 0)
                    {
                        strResult = txtResult.Text.Trim();
                    }
                    else
                    {
                        if (this.bGetRandomResult(this.myFlag))
                        {
                            strResult = "F";
                        }
                        else
                        {
                            strResult = "P";
                        }
                    }
                    string strFullPath;
                    strFullPath = txtPath.Text + "\\ICT_T1\\" + strSnName + ".CCC";
                    //DirectoryInfo dir = new DirectoryInfo(txtPath.Text);
                    this.CreateFile(strFullPath,strSnName, strMachine, strEmp, strMac, strDate, strTime, strResult);
                    strFullPath = txtPath.Text + "\\ICT_T2\\" + strSnName + ".CCC";
                    //DirectoryInfo dir = new DirectoryInfo(txtPath.Text);
                    this.CreateFile(strFullPath, strSnName, strMachine, strEmp, strMac, strDate, strTime, strResult);
                    strFullPath = txtPath.Text + "\\ICT_T3\\" + strSnName + ".CCC";
                    //DirectoryInfo dir = new DirectoryInfo(txtPath.Text);
                    this.CreateFile(strFullPath, strSnName, strMachine, strEmp, strMac, strDate, strTime, strResult);
                    Thread.Sleep(1000);
                }
                else
                {
                    return;
                }
            }
            myShowProgress(8000);

            if (this.chkCircle.Checked)
            {
                this.isCreateFile = true;
                SFCStartup.dba.RevertData(txtMo.Text.Trim());
                SFCStartup.dba.bCommit();
                CreateTestFileProc();
            }
            else
            {
                this.isCreateFile = false;
            }
        }
        private bool bGetRandomResult(int iFlag)
        {
            bool isBad = false;
            Random rd = new Random(DateTime.Now.TimeOfDay.Milliseconds);
            int iNum=rd.Next(10);
            if (iFlag >= iNum)
            {
                isBad = true;
            }
            return isBad;
        }
        private void CreateFile(string strFullPath,string strFileName,string strMachine,string strEmp,string strMac,string strDate,string strTime,string strResult)
        {

            StreamWriter fileSW = File.CreateText(strFullPath);
            fileSW.WriteLine(strFileName);
            fileSW.WriteLine(strMachine);
            fileSW.WriteLine(strEmp);
            fileSW.WriteLine(strMac);
            fileSW.WriteLine(strDate);
            fileSW.WriteLine(strTime);
            fileSW.WriteLine("0");
            fileSW.WriteLine("0");
            fileSW.WriteLine("0");
            fileSW.WriteLine(strResult);
            fileSW.WriteLine("0x00000000");
            fileSW.WriteLine("DONE");
            fileSW.Close();
           // Thread.Sleep(10);
        }
       
      
        //multi thread to insert data
        private void MyThreadProc()
        {
            //System.Windows.Forms.Control.CheckForIllegalCrossThreadCalls = false;
            DataSet dsBarCode;
            DataSet dsMac;
            DataTable dtBarCode, dtMac;
            try
            {
                dsBarCode = SFCStartup.dba.GetInitData("000431001891-1", " AND GROUP_NAME='P_MAC'");
                dsMac = SFCStartup.dba.GetInitData("000341001338-1"," AND GROUP_NAME='0'");
                dtBarCode = dsBarCode.Tables["WIP_TRACKING_T"];
                dtMac = dsMac.Tables["WIP_TRACKING_T"];
                if (dtMac.Rows.Count != dtBarCode.Rows.Count)
                {
                    MessageBox.Show("The Record Count is not same,Import Failed!", "System", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                myShowProgress(0);
                for (int i = 0; i < dtBarCode.Rows.Count; i++)
                {
                    if (SFCStartup.dba.bCheckExists(dtBarCode.Rows[i]["SERIAL_NUMBER"].ToString(), dtMac.Rows[i]["SERIAL_NUMBER"].ToString()))
                    {
                        SFCStartup.dba.bRollBack();
                        MessageBox.Show("Insert Data Failed,Data Exists!", "System", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    SFCStartup.dba.bInsertCompaq(dtBarCode.Rows[i]["SERIAL_NUMBER"].ToString(), dtMac.Rows[i]["SERIAL_NUMBER"].ToString());
                    myShowProgress(i);
                }
                SFCStartup.dba.bCommit();
                myShowProgress(8000);
                MessageBox.Show("Insert Data Successfully!", "System", MessageBoxButtons.OK, MessageBoxIcon.Information);
                myShowProgress(0);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Get Data Failed！" + ex.Message.ToString());
                return;
            }
        }  
        private void btnInput_Click(object sender, EventArgs e)
        {
            this.btnInput.Enabled = false;
            tinserData = new Thread(new ThreadStart(MyThreadProc));
            tinserData.IsBackground = true;
            tinserData.Start();
            this.btnInput.Enabled = true;
        }

        private void txtMachine_TextChanged(object sender, EventArgs e)
        {
            lblMachine.Text = "";
        }

        private void txtEmp_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                if (txtEmp.Text.Trim() == "")
                {
                    MessageBox.Show("Pleae Input a Emp No!","System",MessageBoxButtons.OK,MessageBoxIcon.Error);
                    txtEmp.Text = "";
                    txtEmp.Focus();
                    return;
                }
                try
                {
                 string strUserName = SFCStartup.dba.GetUserName(this.txtEmp.Text.Trim());
                    if (strUserName != "N/A")
                    {
                        lblEmp.ForeColor = Color.Blue;
                        lblEmp.Text = strUserName;
                        txtMachine.Focus();
                    }
                    else
                    {
                        if (DialogResult.OK == MessageBox.Show("Emp Error,Do you want to Input this Emp No any way!", "System", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning))
                        {
                            lblEmp.ForeColor = Color.Red;
                            lblEmp.Text = "Fail";
                            txtMachine.Focus();
                        }
                        else
                        {
                            txtEmp.SelectAll();
                            txtEmp.Focus();
                            return;
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("System Error" + ex.Message.ToString(), "System", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtEmp.SelectAll();
                    txtEmp.Focus();
                    return;
                }
            }
        }

        private void txtEmp_TextChanged(object sender, EventArgs e)
        {
            lblEmp.Text = "";
        }

        private void txtMachine_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                if (this.txtMachine.Text.Trim() == "")
                {
                    MessageBox.Show("Pleae Input a txtMachine Code!", "System", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtMachine.Text = "";
                    txtMachine.Focus();
                    return;
                }
                string strGroupName = SFCStartup.dba.GetMachinGroup(this.txtMachine.Text.Trim());
                if (strGroupName != "N/A")
                {
                    this.lblMachine.ForeColor = Color.Blue;
                    lblMachine.Text = strGroupName;
                    this.txtLine.Focus();
                }
                else
                {
                    if (DialogResult.OK == MessageBox.Show("Machine Code Error,Do you want to Input this Data any way!", "System", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning))
                    {
                        lblMachine.ForeColor = Color.Red;
                        lblMachine.Text = "Fail";
                        txtLine.Focus();
                    }
                    else
                    {
                        txtMachine.SelectAll();
                        txtMachine.Focus();
                        return;
                    }
                }
            }
        }

        private void txtLine_TextChanged(object sender, EventArgs e)
        {
            lblLine.Text = "";
        }

        private void txtLine_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                if (this.txtLine.Text.Trim() == "")
                {
                    MessageBox.Show("Pleae Input a Line Name!", "System", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtLine.Text = "";
                    txtLine.Focus();
                    return;
                }
                string strInputGroup;
                if (lblMachine.Text != "Fail")
                {
                    strInputGroup = lblMachine.Text;
                }
                else
                {
                    strInputGroup = "N/A";
                }

                string strLineName = SFCStartup.dba.GetLineName(this.txtLine.Text.Trim(), strInputGroup);
                if (strLineName != "N/A")
                {
                    this.lblLine.ForeColor = Color.Blue;
                    lblLine.Text = strLineName;
                    this.txtResult.Focus();
                }
                else
                {
                    if (DialogResult.OK == MessageBox.Show("Line Name Error,Do you want to Input this Data any way!", "System", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning))
                    {
                        lblLine.ForeColor = Color.Red;
                        lblLine.Text = "Fail";
                        txtResult.Focus();
                    }
                    else
                    {
                        txtLine.SelectAll();
                        txtLine.Focus();
                        return;
                    }
                }
            }
        }

        private void txtResult_TextChanged(object sender, EventArgs e)
        {
            lblResult.Text = "";
        }

        private void txtResult_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                txtResult.Text = txtResult.Text.ToUpper();
                if (txtResult.Text.Trim() != "P" && txtResult.Text.Trim() != "F")
                {
                    MessageBox.Show("Please Input Letter P or F!", "System", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtResult.SelectAll();
                    txtResult.Focus();
                    return;
                }
                else
                {
                    //btnStart.Focus();
                    txtMo.Focus();
                    lblResult.Text = txtResult.Text;
                }
            }
        }

        private void txtMo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                if (this.txtMo.Text.Trim() == "")
                {
                    MessageBox.Show("Please Input MO!", "System", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtMo.Text = "";
                    txtMo.Focus();
                    return;
                }
                try
                {
                    DataSet dsMo = SFCStartup.dba.GetInitData(txtMo.Text.Trim(), " AND GROUP_NAME='P_MAC'");
                    if (dsMo.Tables["WIP_TRACKING_T"].Rows.Count == 0)
                    {
                        MessageBox.Show("MO Error!", "System", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        txtMo.Text = "";
                        txtMo.Focus();
                        return;
                    }
                    else
                    {
                        btnStart.Focus();
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Please Input MO!", "System", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtMo.Text = "";
                    txtMo.Focus();
                    return;
                }
            }
        }

        private void ucTestICT_Load(object sender, EventArgs e)
        {
            cmbAbnormity.SelectedIndex = 0;
        }

        private void cmbAbnormity_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.myFlag = AnalyseData();
        }

    }
}
