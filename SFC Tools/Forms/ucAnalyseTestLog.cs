using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Collections;
using System.Text.RegularExpressions;
//using Microsoft.Office.Interop.Excel;
using Microsoft.CSharp;
using System.Threading;
using Microsoft.Office.Interop.Excel;
//using Microsoft.Office.Core;


namespace SFC_Tools
{
    public partial class ucAnalyseTestLog : UserControl
    {
        private bool bIsRead = false;
        private Int32 iRowID=1;
        private int iTotal = 0;
        private long iLen = 0;
        private long iValue = 0;
        private string strPath="N/A";
        private string strIctLogName = "N/A";
        private string strLogPath = "N/A";
        private int iFlag = 0;
        public ucAnalyseTestLog()
        {
            InitializeComponent();
        }

        private void btnReadLog_Click(object sender, EventArgs e)
        {
              ofdIctLog.Filter = "ICT LOG(*.Log)|*.log";
              if (DialogResult.OK == ofdIctLog.ShowDialog())
              {
                 strPath = ofdIctLog.FileName;
                 myReadIctLog();
             }
             else
             {
                return;
             }/*
              if (this.strPath != "N/A")
              {
                  Thread thd = new Thread(new ThreadStart(myReadIctLog));
                  thd.IsBackground = true;
                  thd.Start();
              }*/
        }
        private void myReadIctLog()
        {
            string strPath;
            strPath = this.strPath;
            FileStream fs = new FileStream(strPath, FileMode.Open,FileAccess.Read);
            StreamReader sr = new StreamReader(fs,Encoding.Default);
            ArrayList arrIctLog = new ArrayList();
            strIctLogName = this.GetFileName(strPath);
            try
            {
                dataInitD();
                dataInit();
                initProgress(1000);
                while (sr.Peek() > 0)
                {
                    string strThisLine = sr.ReadLine();
                    this.iLen = iLen + strThisLine.Length;
                    iValue=(iLen *1000/fs.Length);
                    //this.pgbRead.Value = int.Parse(iValue.ToString());
                    this.SetProgress(int.Parse(iValue.ToString()));
                    if (strThisLine.Substring(0, 21) == "Begin To Read File---")
                    {
                        this.bIsRead = true;
                    }
                    if (this.bIsRead)
                    {
                        arrIctLog.Add(strThisLine);
                    }
                    if (strThisLine.Substring(0, 20) == "End To Write File---")
                    {
                        this.bIsRead = false;
                        if (arrIctLog.Count == 6)
                        {
                            AnalyseTime(arrIctLog);
                        }
                        arrIctLog.Clear();
                    }
                   
                }
                sr.Close();
                //this.pgbRead.Value = 1000;
                this.SetProgress(1000);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Read Log Error!" + ex.Message.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                this.bIsRead = false;
                sr.Close();
                fs.Close();
                this.SetProgress(0);
            }
        }
        delegate void dataInitDelegate();
        private void dataInitD()
        {
            if (!dgvTestLogAnalyse.InvokeRequired)
            {
                this.dgvTestLogAnalyse.Rows.Clear();
            }
            else {
                dataInitDelegate did = new dataInitDelegate(dataInitD);
                BeginInvoke(did, new object[] { });
            }

        }
        private void dataInit()
        {
            //this.dgvTestLogAnalyse.Rows.Clear();
            this.iRowID = 1;
            this.iTotal = 0; 
            //this.pgbRead.Minimum = 0;
            //this.pgbRead.Value = 0;
            //this.pgbRead.Maximum = 1000;
            this.iLen = 0;
            this.iValue = 0;
            this.iTotal = 0;
        }
        //analyse data
        private void AnalyseTime(ArrayList arrLst)
        {
            try
            {
                string strFileNmae;
                strFileNmae = arrLst[0].ToString();
                string strSeparate = "---";
                string[] strnewFile = Regex.Split(strFileNmae, strSeparate);
                strFileNmae=GetFileName(strnewFile[1].ToString());
                string strTime = strGetTime(arrLst[0].ToString());
                TimeSpan tsBegin,tsBeginProc,tsBeginWrit;
                TimeSpan tsEnd,tsEndProc;
                TimeSpan tsInterval,tsRead,tsCallProc,tsWrite;
                string strEndTime=strGetTime(arrLst[5].ToString());
                string strEndRead = strGetTime(arrLst[1].ToString());
                string strBeginProc = strGetTime(arrLst[2].ToString());
                string strEndProc = strGetTime(arrLst[3].ToString());
                string strBeginWrite = strGetTime(arrLst[4].ToString());
                tsBegin = this.strFormateTime(strTime);
                strTime = tsBegin.ToString();
                tsEnd = this.strFormateTime(strEndTime);
                strEndTime = tsEnd.ToString();
                tsInterval = tsEnd.Subtract(tsBegin).Duration();

                tsRead = this.strFormateTime(strEndRead);
                tsRead = tsRead.Subtract(tsBegin).Duration();

                tsBeginProc=this.strFormateTime(strBeginProc);
                tsEndProc=this.strFormateTime(strEndProc);
                tsCallProc = tsEndProc.Subtract(tsBeginProc).Duration();

                tsBeginWrit=this.strFormateTime(strBeginWrite);
                tsWrite = tsEnd.Subtract(tsBeginWrit).Duration();
                if (tsInterval.TotalSeconds >= this.iFlag)
                {
                    InsertPgv(0,
                    this.iRowID,
                    strFileNmae/*arrLst[0].ToString()*/,
                    tsInterval.TotalSeconds.ToString(),/*arrLst[3].ToString(), */
                    tsRead.TotalSeconds.ToString()/*strTime*//*arrLst[1].ToString()*/,
                    tsCallProc.TotalSeconds.ToString(),/*strEndTime,*//*arrLst[2].ToString(),*/
                    tsWrite.TotalSeconds.ToString() /*arrLst[4].ToString()*/
                   );
                    this.iTotal = this.iTotal + 1;          
                }
                this.lblTotal.Text = "Total:" + iTotal.ToString();
                this.iRowID = this.iRowID + 1;
            }
            catch (Exception ex)
            {
               
            }
        }
        delegate void InsertPgvDelegate(int iRow, int ID, string strFileName, string strTTime, string strReadTime, string strSpTime, string strWriteTime);
        private void InsertPgv(int iRow, int ID, string strFileName, string strTTime, string strReadTime, string strSpTime, string strWriteTime)  
       {
           if (!this.pgbRead.InvokeRequired)
           {
               this.dgvTestLogAnalyse.Rows.Insert(iRow, ID, strFileName, strTTime, strReadTime, strSpTime, strWriteTime);
           }
           else
           {
               InsertPgvDelegate itpd = new InsertPgvDelegate(InsertPgv);
               BeginInvoke(itpd, new object[] { iRow, ID, strFileName, strTTime, strReadTime, strSpTime, strWriteTime });
           }
       }

        //
        private TimeSpan strFormateTime(string strParam)
        {
            string strTemp = strParam;
            string strDay="15",strH="00",strM="00",strS="00",strMs="00";
            TimeSpan ts1=new TimeSpan();
            int iPos = strTemp.IndexOf(@"--");
            {
                strTemp = strTemp.Substring(iPos + 2, strParam.Length - iPos - 2);
            }
            try
            {
                iPos = strTemp.IndexOf(@":");
                if (iPos >= 0)
                {
                    strH = strTemp.Substring(0, iPos);
                    strTemp = strTemp.Substring(iPos + 1, strTemp.Length - iPos - 1);
                }
                iPos = strTemp.IndexOf(@":");
                if (iPos >= 0)
                {
                    strM = strTemp.Substring(0, iPos);
                    strTemp = strTemp.Substring(iPos + 1, strTemp.Length - iPos - 1);
                }
                iPos = strTemp.IndexOf(@":");
                if (iPos >= 0)
                {
                    strS = strTemp.Substring(0, iPos);
                    strTemp = strTemp.Substring(iPos + 1, strTemp.Length - iPos - 1);
                }
                strMs = strTemp;
                ts1 = new TimeSpan(int.Parse(strDay), int.Parse(strH), int.Parse(strM), int.Parse(strS), int.Parse(strMs));
            }
            catch (Exception ex)
            { return ts1; }
            return ts1;
        }
        //Get Time
        private string strGetTime(string strParam)
        {
            string strTemp="";
            strTemp = strParam;
            int iPos = strParam.IndexOf(@"LOG\");
            if (iPos >= 0)
            {
                strTemp = strParam.Substring(iPos + 1, strParam.Length - iPos - 1);
            }
            iPos = strParam.IndexOf(@"LOG\");
            while (iPos >= 0)
            {
                strTemp = GetFileName(strTemp);
                iPos = strTemp.IndexOf(@"LOG\");
            }
            return strTemp;
        }
        //Get File Name
        private string GetFileName(string strParam)
        {
                string strTemp;
                strTemp = strParam;
                int iPos = strParam.IndexOf(@"\");
                if (iPos >= 0)
                {
                    strTemp = strParam.Substring(iPos + 1, strParam.Length - iPos - 1);
                }
                iPos = strTemp.IndexOf(@"\");
                while (iPos >= 0)
                {
                    strTemp=GetFileName(strTemp);
                    iPos = strTemp.IndexOf(@"\");
                }
                iPos = strTemp.IndexOf(".");
                if (iPos >= 0)
                {
                    strTemp = strTemp.Substring(0, iPos);
                }
                return strTemp;
        }
        //init datagridview
        private void AddColumn()
        {
            DataGridViewTextBoxColumn dgtLine = new DataGridViewTextBoxColumn();
            dgtLine.Name = "Line NO";
            dgtLine.ReadOnly = true;
            dgtLine.Width = 70;
            //dgtBeginRead.DataPropertyName = "";
            DataGridViewTextBoxColumn dgtFileName = new DataGridViewTextBoxColumn();
            dgtFileName.Name = "File Name";
            dgtFileName.ReadOnly = true;
            dgtFileName.Width = 100;
            DataGridViewTextBoxColumn dgtTotal = new DataGridViewTextBoxColumn();
            dgtTotal.Name = "Total Time";
            dgtTotal.ReadOnly = true;
            dgtTotal.Width = 85;
            DataGridViewTextBoxColumn dgtEndProc = new DataGridViewTextBoxColumn();
            dgtEndProc.Name = "Read File";
            dgtEndProc.ReadOnly = true;
            dgtEndProc.Width = 150;
            DataGridViewTextBoxColumn dgtBeginWrite = new DataGridViewTextBoxColumn();
            dgtBeginWrite.Name = "Call Prc";
            dgtBeginWrite.ReadOnly = true;
            dgtBeginWrite.Width = 150;
            DataGridViewTextBoxColumn dgtEndWrite = new DataGridViewTextBoxColumn();
            dgtEndWrite.Name = "Write File";
            dgtEndWrite.ReadOnly = true;
            dgtEndWrite.Width = 150;

            this.dgvTestLogAnalyse.Columns.Clear();
            this.dgvTestLogAnalyse.Columns.Add(dgtLine);
            this.dgvTestLogAnalyse.Columns.Add(dgtFileName);
            this.dgvTestLogAnalyse.Columns.Add(dgtTotal);
            this.dgvTestLogAnalyse.Columns.Add(dgtEndProc);
            this.dgvTestLogAnalyse.Columns.Add(dgtBeginWrite);
            this.dgvTestLogAnalyse.Columns.Add(dgtEndWrite);
        }

        private void ucAnalyseTestLog_Load(object sender, EventArgs e)
        {
            AddColumn();
            this.cmbTime.SelectedIndex = 4;
            this.iFlag = int.Parse(this.cmbTime.Text);
        }

        private void cmbTime_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.iFlag = int.Parse(this.cmbTime.Text);
            if (this.strPath != "N/A")
            {
                Thread td = new Thread(new ThreadStart(myReadIctLog));
                td.IsBackground = true;
                td.Start();
                //myReadIctLog();
            }
           
        }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            sfdIctExcel.Filter = "(*.XLSX)|*.xlsx|(*.XLS)|*.xls|All files (*.*)|*.*";
            if (this.strIctLogName == "N/A")
            {
                return;
            }
            sfdIctExcel.FileName = this.strIctLogName;
            sfdIctExcel.Title = "ICT Log";
            //sfdIctExcel.CreatePrompt = true;
            // sfdIctExcel.RestoreDirectory = true;
            if (DialogResult.OK == sfdIctExcel.ShowDialog())
            {
                this.strLogPath = sfdIctExcel.FileName;
            }
            else
            {
                return;
            }
            Thread td = new Thread(new ThreadStart(ExportExcel));
            td.IsBackground = true;
            td.Start();
            //ExportExcel();
        }
        delegate void initProgressDelegate(int iMax);
        private void initProgress(int iMax)
        {
            if (!this.pgbRead.InvokeRequired)
            {
                this.pgbRead.Maximum = iMax;
                this.pgbRead.Value = 0;
                this.pgbRead.Minimum = 0;
            }
            else
            {
                initProgressDelegate sp = new initProgressDelegate(initProgress);
                BeginInvoke(sp, new object[] { iMax });
            }
        }
        private void ExportExcel()
        {
            string strSavePath;
            if (this.strLogPath != "N/A")
                strSavePath = this.strLogPath;
            else
                return;
            Microsoft.Office.Interop.Excel.Application appExcel = new Microsoft.Office.Interop.Excel.Application();
            appExcel.Visible = false;
            Microsoft.Office.Interop.Excel.Workbook wbLogInfo = appExcel.Workbooks.Add(true);
            //Microsoft.Office.Interop.Excel.Worksheet wsLogInfo = (Microsoft.Office.Interop.Excel.Worksheet)wbLogInfo.Worksheets["sheet1"];
            //this logic have problems under .net framework 4.0, but work smoothly under .net framework 3.5
            Microsoft.Office.Interop.Excel.Worksheet wsLogInfo=new Microsoft.Office.Interop.Excel.Worksheet();//= (Microsoft.Office.Interop.Excel.Worksheet)wbLogInfo.Worksheets[1];
            wsLogInfo.Name = "Ict Log Info";
            wsLogInfo.get_Range("B2").Value2 = "Line No";
            //wsLogInfo.get_Range("A1").Borders.Color = Color.Blue;
            wsLogInfo.get_Range("B2").Interior.Color = Color.LightGray;//Color.FromArgb(56,102,129);
            wsLogInfo.get_Range("C2").Value2 = "File Name";
            wsLogInfo.get_Range("C2").ColumnWidth = 20;
            wsLogInfo.get_Range("C2").Interior.Color = Color.LightGray;// Color.Gray;
            wsLogInfo.get_Range("D2").Value2 = "Total Time";
            wsLogInfo.get_Range("D2").ColumnWidth = 10;
            wsLogInfo.get_Range("D2").Interior.Color = Color.LightGray;
            wsLogInfo.get_Range("E2").Value2 = "Read File";
            wsLogInfo.get_Range("E2").ColumnWidth = 10;
            wsLogInfo.get_Range("E2").Interior.Color = Color.LightGray;
            wsLogInfo.get_Range("F2").Value2 = "Call Proc";
            wsLogInfo.get_Range("F2").Interior.Color = Color.LightGray;
            wsLogInfo.get_Range("G2").Value2 = "Write File";
            wsLogInfo.get_Range("G2").Interior.Color = Color.LightGray;
            wsLogInfo.get_Range("B2", "G2").BorderAround(Type.Missing, XlBorderWeight.xlMedium, XlColorIndex.xlColorIndexAutomatic, Type.Missing);//.Borders.Color = Color.Blue;
            int iRow = 2;
            string strRangeA, strRangeB, strRangeC, strRangeD, strRangeE, strRangeF = "";
            initProgress(dgvTestLogAnalyse.Rows.Count);
            for (iRow = 2; iRow < dgvTestLogAnalyse.Rows.Count; iRow++)
            {
                strRangeA = "B" + (iRow + 1).ToString();
                strRangeB = "C" + (iRow + 1).ToString();
                strRangeC = "D" + (iRow + 1).ToString();
                strRangeD = "E" + (iRow + 1).ToString();
                strRangeE = "F" + (iRow + 1).ToString();
                strRangeF = "G" + (iRow + 1).ToString();
                wsLogInfo.get_Range(strRangeA).Value2 = dgvTestLogAnalyse.Rows[iRow - 1].Cells[0].Value.ToString();
                wsLogInfo.get_Range(strRangeB).Value2 = dgvTestLogAnalyse.Rows[iRow - 1].Cells[1].Value.ToString();
                wsLogInfo.get_Range(strRangeC).Value2 = dgvTestLogAnalyse.Rows[iRow - 1].Cells[2].Value.ToString();
                wsLogInfo.get_Range(strRangeD).Value2 = dgvTestLogAnalyse.Rows[iRow - 1].Cells[3].Value.ToString();
                wsLogInfo.get_Range(strRangeE).Value2 = dgvTestLogAnalyse.Rows[iRow - 1].Cells[4].Value.ToString();
                wsLogInfo.get_Range(strRangeF).Value2 = dgvTestLogAnalyse.Rows[iRow - 1].Cells[5].Value.ToString();
                this.SetProgress(iRow);
            }
            this.SetProgress(dgvTestLogAnalyse.Rows.Count);
           
            try
            {
                if (File.Exists(strSavePath))
                {
                    File.Delete(strSavePath);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Update File Failed!" + ex.Message.ToString(), "system", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            try
            {  
                wsLogInfo.get_Range("B3", strRangeF).BorderAround(Type.Missing, XlBorderWeight.xlThin, XlColorIndex.xlColorIndexAutomatic, Color.Blue);//.Borders.Color = Color.Blue;
                wsLogInfo.SaveAs(strSavePath, XlFileFormat.xlWorkbookNormal, Type.Missing, Type.Missing, Type.Missing, Type.Missing, XlSaveAsAccessMode.xlExclusive,
                    Type.Missing, Type.Missing, Type.Missing);
                wbLogInfo.Close(false, Type.Missing, Type.Missing);
                appExcel.Quit();
            }
            catch (Exception ex)
            {
                wbLogInfo.Close();
                appExcel.Quit();
            }
            SetProgress(0);
        }
        delegate void setProgressPosDelegate(int iPos);
        private void SetProgress(int iPos)
        {
            if (!this.pgbRead.InvokeRequired)
            {
                pgbRead.Value = iPos;
            }
            else
            {
                setProgressPosDelegate sp = new setProgressPosDelegate(SetProgress);
                this.BeginInvoke(sp, new object[] { iPos });
            }
        }

    }
}
