using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using SFC_Tools.Classes;
using System.Threading;

namespace SFC_Tools.Forms
{
    public partial class ucWorkLog : UserControl
    {
        string sSourceFolder = string.Empty;
        string sTargetFolder = string.Empty;
        delegate void showProgressDelegate(int newPos);
        delegate void setProgressCallBack(int iValue);
        delegate void setProgressTextCallback(string sMsg);
        delegate void appendTextCallBack(string sMsg);
        private long iTotalFiles = 0;
        Thread tdCopyFiles;
        private void appendText(string sMsg)
        {
            if (rtbFileList.InvokeRequired)
            {
                appendTextCallBack atc = new appendTextCallBack(appendText);
                this.BeginInvoke(atc, new object[] { sMsg });
            }
            else
            {
                rtbFileList.AppendText(sMsg+"\r\n");
            }
        }
        public void myShowProgress(int newPos)
        {
            if (!this.pgbFormateProgress.InvokeRequired)
            {
               
                if (pgbFormateProgress.Value >= pgbFormateProgress.Maximum)
                {
                    pgbFormateProgress.Value = pgbFormateProgress.Minimum;
                }
                else
                    pgbFormateProgress.Value = newPos; 
            }
            else
            {
                if (newPos >= 100)
                {
                    newPos = 0;
                }
                showProgressDelegate sp = new showProgressDelegate(myShowProgress);
                this.BeginInvoke(sp, new object[] { newPos });
            }
        }
        public ucWorkLog()
        {
            InitializeComponent();
        }
        
        private void btnSoureFolder_Click(object sender, EventArgs e)
        {
            this.txtSource.Enabled = false;
            this.txtTarget.Enabled = false;
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            fbd.Description = "Select The Source Folder";
            if (fbd.ShowDialog() == DialogResult.OK)
            {
                sSourceFolder=fbd.SelectedPath.ToString();
                txtSource.Text = sSourceFolder;
            }
        }

        private void setProgressText(string sMsg)
        {
            if (lblFileName.InvokeRequired)
            {
                setProgressTextCallback sp = new setProgressTextCallback(setProgressText);
                this.BeginInvoke(sp, new object[] { sMsg });
            }
            else
            {
                lblFileName.Text = sMsg;
                lblFileName.Refresh();
            }
        }

    
        private void setProgress(int iValue)
        {
            if (this.pgbFormateProgress.InvokeRequired)
            {
                if (iValue >= 99)
                    iValue = 0;
                setProgressCallBack sp = new setProgressCallBack(setProgress);
                this.BeginInvoke(sp, new object[] { iValue++ });
            }
            else
            {
                if (iValue >= 100)
                    iValue = 0;
                pgbFormateProgress.Value = iValue;
            }
        }
        public void CopyAll(DirectoryInfo source, DirectoryInfo target,int iValue)
        {
            // Check if the target directory exists, if not, create it.
            if (Directory.Exists(target.FullName) == false)
            {
                Directory.CreateDirectory(target.FullName);
            }

            // Copy each file into it's new directory.
            foreach (FileInfo fi in source.GetFiles())
            {
                fi.CopyTo(Path.Combine(target.ToString(), fi.Name), true);
                //myShowProgress(pgbFormateProgress.Value++);
                if (this.pgbFormateProgress.InvokeRequired)
                {
                    showProgressDelegate sp = new showProgressDelegate(setProgress);
                    this.BeginInvoke(sp, new object[] { iValue++});
                }
                else
                {
                    iValue++;
                    if (iValue >= 99)
                        iValue = 0;
                    pgbFormateProgress.Value = iValue;
                    //myShowProgress(iValue++);
                }
            }

            // Copy each subdirectory using recursion.
            foreach (DirectoryInfo diSourceSubDir in source.GetDirectories())
            {
                DirectoryInfo nextTargetSubDir =
                    target.CreateSubdirectory(diSourceSubDir.Name);
                CopyAll(diSourceSubDir, nextTargetSubDir,0);
            }
        }


        private void TidyUpFolder(string strPath)
        {
            if (Directory.Exists(strPath))
            {//if it's a folder 
                DirectoryInfo di = new DirectoryInfo(strPath);
                foreach (DirectoryInfo sdi in di.GetDirectories())
                {
                    DirectoryInfo diNextFolder = di.CreateSubdirectory(sdi.Name);
                    long nLen = GetDirectoryLength(diNextFolder.FullName, 0);
                    if (nLen == 0)
                    {
                        appendText("DEL:" + diNextFolder.FullName + "--" + nLen.ToString());
                        Directory.Delete(diNextFolder.FullName, true);
                        continue;
                    }
                    TidyUpFolder(diNextFolder.FullName);
                }
            }
            else
            {
                //not a folder
                throw new Exception(strPath + " is not a folder!");
            }
        }
        private long GetDirectoryLength(string strPath,long len)
        {
            if (Directory.Exists(strPath))
            {//if it's a folder 
                DirectoryInfo di = new DirectoryInfo(strPath);
                foreach (FileInfo fi in di.GetFiles())
                {
                    len += fi.Length;
                }
                
                DirectoryInfo dif = new DirectoryInfo(strPath);
                DirectoryInfo []xx = dif.GetDirectories();
                foreach (DirectoryInfo sdi in dif.GetDirectories())
                {
                    DirectoryInfo diNextFolder = di.CreateSubdirectory(sdi.Name);
                    GetDirectoryLength(diNextFolder.ToString(),len);
                }
                //MessageBox.Show(len.ToString() + "--" + di.FullName.ToString());
            }
            else
            { 
                //not a folder
                throw new Exception(strPath+" is not a folder!");
            }
            return len;
        }
        private void myFileHandelProc()
        {
            myShowProgress(0);
            if (sSourceFolder.Trim().Length == 0 || sTargetFolder.Trim().Length == 0)
            {
                return;
            }
            DirectoryInfo diSource = new DirectoryInfo(sSourceFolder);
            DirectoryInfo diTarget = new DirectoryInfo(sTargetFolder);
            setProgressText("System Begin to back up the source files. please wait for a while!");
            CopyAll(diSource, diTarget, 0);
            myShowProgress(99);
            setProgressText("File Copy Finish! Please Waitting for begin to analysis system files....");
            Thread.Sleep(2000);
            SearchSubFiles(sSourceFolder);
            setProgressText(iTotalFiles.ToString() + " Files has been analysed!....");
            //GetDirectoryLength(sSourceFolder,0);
            TidyUpFolder(sSourceFolder);
            //sSourceFolder = "";
            //sTargetFolder = "";
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            this.txtSource.Enabled = false;
            this.txtTarget.Enabled = false;
            if (string.IsNullOrEmpty(sSourceFolder))
            {
                btnSoureFolder_Click(sender,e);
            }
            if (string.IsNullOrEmpty(sTargetFolder))
            {
                btnTargetFolder_Click(sender, e);
            }
            rtbFileList.Clear();
            try
            {
              
                pgbFormateProgress.Maximum = 100;
                pgbFormateProgress.Minimum = 0;
                pgbFormateProgress.Value = 0;

                if (ckbLenovoCleaner.Checked)
                {
                    tdCopyFiles = new Thread(new ThreadStart(myFileHandelProc));
                    tdCopyFiles.IsBackground = true;
                    tdCopyFiles.Start();
                }
                else
                {
                    DelSpecifiedFiles dsf = new DelSpecifiedFiles(sSourceFolder);
                    dsf.delFiles();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message.ToString());
            }
        }
        private void SearchSubFiles(string strPath)
        {
            DirectoryInfo di = new DirectoryInfo(strPath);
            foreach (FileInfo fi in di.GetFiles())
            {
                if(fi.Name.ToUpper() == "SWI.XML")
                {
                    continue;
                }
                if (di.FullName.ToUpper().IndexOf("SESSION")>=0 && fi.Name.LastIndexOf(di.Name) >= 0 )
                {
                  
                    if(fi.Name.ToUpper().IndexOf(".XML")>=0)
                    {
                        if (fi.Name.Substring(fi.Name.ToUpper().IndexOf(".XML")+1, 3).ToUpper() == "XML")
                            continue;
                    }
                    setProgressText(fi.FullName.ToString());
                    appendText(fi.FullName.ToString());
                    iTotalFiles++;
                    File.Delete(fi.FullName.ToString());
                }
                else
                {
                    setProgressText(fi.FullName.ToString());
                    appendText(fi.FullName.ToString());
                    iTotalFiles++;
                    File.Delete(fi.FullName.ToString());
                }
            }
            DirectoryInfo dif = new DirectoryInfo(strPath);
            foreach (DirectoryInfo sdi in dif.GetDirectories())
            {
                DirectoryInfo diNextFolder = di.CreateSubdirectory(sdi.Name);
                if (diNextFolder.Name.ToString().ToUpper() == "SYSTEM")
                    // diNextFolder = di.CreateSubdirectory(diNextFolder.Name);
                    continue;
                if (diNextFolder.Name.ToString().ToUpper() == "TEMP")
                    // diNextFolder = di.CreateSubdirectory(diNextFolder.Name);
                    continue;
                if (diNextFolder.Name.ToString().ToUpper() == "TVSUTEMP")
                    //diNextFolder = di.CreateSubdirectory(diNextFolder.Name);
                    continue;
                
                SearchSubFiles(diNextFolder.ToString());
            }
        }

        private void btnTargetFolder_Click(object sender, EventArgs e)
        {
            this.txtSource.Enabled = false;
            this.txtTarget.Enabled = false;
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            fbd.Description = "Select The Target Folder";
            if (fbd.ShowDialog() == DialogResult.OK)
            {
                sTargetFolder = fbd.SelectedPath.ToString();
                txtTarget.Text = sTargetFolder;
            }
        }

        private void label1_DoubleClick(object sender, EventArgs e)
        {
            txtSource.Enabled = true;
        }

        private void txtSource_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                txtSource.Enabled = false;
                txtTarget.Enabled = true;
                txtTarget.Focus();
            }
        }

        private void txtSource_Leave(object sender, EventArgs e)
        {
            sSourceFolder = txtSource.Text;
        }

        private void txtTarget_Leave(object sender, EventArgs e)
        {
            txtTarget.Enabled = false;
            sTargetFolder = txtTarget.Text;
        }
    }
}
