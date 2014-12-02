using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
//using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Collections;
using System.Threading;

namespace SFC_Tools
{
    public partial class ucReadFile : UserControl
    {
        private bool bReadFile = false;
        public ucReadFile()
        {
            InitializeComponent();
        }

        private void ucReadFile_Load(object sender, EventArgs e)
        {
            AddColumns();
        }
        private void ucReadFile_Disposed(object sender, EventArgs e)
        {
           
            if (this.bReadFile)
            {
                MessageBox.Show("Please Close Listening Read File First!","System",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                this.bReadFile = false;
                return;
            }
        }
        private void AddColumns()
        {
            DataGridViewTextBoxColumn cloBarCode = new DataGridViewTextBoxColumn();
            cloBarCode.Name = "Barcode";
            cloBarCode.ReadOnly = true;
            cloBarCode.DataPropertyName = "SERIAL_NUMER";
            cloBarCode.Width = 220;
            DataGridViewTextBoxColumn cloResult = new DataGridViewTextBoxColumn();
            cloResult.Name = "Test Result";
            cloResult.ReadOnly = true;
            cloResult.DataPropertyName = "RESULT";
            cloResult.Width = 150;
            DataGridViewTextBoxColumn cloTime = new DataGridViewTextBoxColumn();
            cloTime.Name = "Operate Time";
            cloTime.ReadOnly = true;
            cloTime.DataPropertyName="OPERATE_TIME";
            cloTime.Width = 200;

            this.dgvFileInfo.Columns.Clear();
            this.dgvFileInfo.Columns.Add(cloBarCode);
            this.dgvFileInfo.Columns.Add(cloResult);
            this.dgvFileInfo.Columns.Add(cloTime);

        }

        private void btnBeginRead_Click(object sender, EventArgs e)
        {
            try
            {
                this.bReadFile = true;
                lblReadPath.Text = "";
                if (lblReadPath.Text.Length != 0)
                {
                    if (DialogResult.OK == MessageBox.Show("Do you want to continue Read?", "System", MessageBoxButtons.OKCancel, MessageBoxIcon.Question))
                    {

                    }
                    else
                    {
                        if (DialogResult.OK == this.fbdReadFile.ShowDialog())
                        {
                            this.btnBeginRead.Enabled = false;
                            SFCStartup.dba.SetFormState(false);
                            frmMain fm = (frmMain)this.ParentForm;
                            fm.setBtnEnable(false);
                            lblReadPath.Text = fbdReadFile.SelectedPath;
                            Thread td = new Thread(new ThreadStart(MyReadFileProc));
                            td.IsBackground = true;
                            td.Start();
                        }
                        else
                        {
                            return;
                        }
                    }
                }
                if (DialogResult.OK == this.fbdReadFile.ShowDialog())
                {
                    this.btnBeginRead.Enabled = false;
                    SFCStartup.dba.SetFormState(false);
                    frmMain fm = (frmMain)this.ParentForm;
                    fm.setBtnEnable(false);
                    lblReadPath.Text = fbdReadFile.SelectedPath;
                    Thread td = new Thread(new ThreadStart(MyReadFileProc));
                    td.IsBackground = true;
                    td.Start();
                }
                else
                {
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("System Error!" + ex.Message.ToString(),"System",MessageBoxButtons.OK,MessageBoxIcon.Asterisk);
            }
        }

      
        private void MyReadFileProc()
        {
            if (lblReadPath.Text == "")
            {
                MessageBox.Show("Please Select a File Path!", "System", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            try
            {
                while (true)
                {
                    if (this.bReadFile)
                    {
                        ReadFile(lblReadPath.Text);
                    }
                    else
                    {
                        return;
                    }

                }
            }
            catch (Exception ex)
            {
                //this.bReadFile = false;
                //return;
                MyReadFileProc();
            }
        }
        delegate void InsertDataViewDelegate(string strBarCode, string strRes, string strTime);
        private void MyInsertDataView(string strBarCode, string strRes, string strTime)
        {
            if (!(this.dgvFileInfo.InvokeRequired))
            {
                this.dgvFileInfo.Rows.Insert(0, strBarCode, strRes, strTime);
                SFCStartup.dba.InsertTestLog(strBarCode, strRes, strTime);
            }
            else
            {
                InsertDataViewDelegate idvd = new InsertDataViewDelegate(MyInsertDataView);
                this.BeginInvoke(idvd, new object[] { strBarCode, strRes, strTime });
            }
        }
        private void ReadFile(string strPath)
        {
            try
            {
                DirectoryInfo di = new DirectoryInfo(strPath);
                foreach (FileInfo fi in di.GetFiles("*.DDD"))
                {
                    string strFilePath;
                    strFilePath = di + "\\" +fi.ToString();
                    ArrayList arrFile=AnalyseFile(strFilePath,2);
                    //this.dgvFileInfo.Rows.Insert(0,arrFile[0].ToString(),arrFile[1].ToString(),DateTime.Now.ToLocalTime());
                    MyInsertDataView(arrFile[0].ToString(), arrFile[1].ToString(), DateTime.Now.ToLocalTime().ToString());
                    Thread.Sleep(300);
                    File.Delete(strFilePath);
                }
                //this.bReadFile = false;
            }catch(Exception ex)
            {
                throw new Exception(ex.Message.ToString());
            }
        }
        private ArrayList AnalyseFile(string strPath,int iCount)
        {
            StreamReader sr = new StreamReader(strPath);
            ArrayList arrRes=new ArrayList();
            for (int i = 0; i < iCount; i++)
            {
                arrRes.Add(sr.ReadLine());
            }
            sr.Close();
            return arrRes;
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            try
            {
                this.bReadFile = false;
                this.btnBeginRead.Enabled = true;
                frmMain fm = (frmMain)this.ParentForm;
                fm.setBtnEnable(true);
                SFCStartup.dba.SetFormState(true);
            }
            catch (Exception ex)
            {
                MessageBox.Show("System Error!"+ex.Message.ToString(),"System",MessageBoxButtons.OK,MessageBoxIcon.Asterisk);
            }
        }

        private void panel1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            txtPath.Visible = !txtPath.Visible;
        }

        private void txtPath_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar ==13)
            {
                if (txtPath.Text.Trim() != "")
                {
                    lblReadPath.Text = txtPath.Text;
                    txtPath.Visible = false;
                }
                else
                {
                    txtPath.Text = "";
                    txtPath.Focus();
                }
            }
        }
   
    }
}
