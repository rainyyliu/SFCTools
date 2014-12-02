using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace SFC_Tools.Forms
{
    public partial class uFileFormate : UserControl
    {
        string strNewFileName;

        public uFileFormate()
        {
            InitializeComponent();
        }

        private void btnBeginRead_Click(object sender, EventArgs e)
        {
            string strFilePath = string.Empty;
            Stream myStream = null;

            ofdTargetFile.InitialDirectory = "c:\\";
            ofdTargetFile.Filter = "js files (*.js)|*.js|txt files (*.txt)|*.txt|All files (*.*)|*.*";
            ofdTargetFile.FilterIndex = 0;
            ofdTargetFile.RestoreDirectory = true;
            if (DialogResult.OK == this.ofdTargetFile.ShowDialog())
            {
                try
                {
                    if ((myStream = ofdTargetFile.OpenFile()) != null)
                    {
                        strNewFileName = ofdTargetFile.SafeFileName;
                        string strTmp=string.Empty;
                        StreamReader sr = new StreamReader(myStream);
                        while (sr.Peek() > 0)
                        {
                            strTmp+=sr.ReadLine();
                        }
                        string strTarget=string.Empty;
                        string strFormate = string.Empty;
                        int iLen = 0;
                        pgbFormateProgress.Maximum = 100;
                        pgbFormateProgress.Minimum = 0;
                        int iMaxPrg = strTmp.Length;
                        int iPos=0;
                        foreach (char ch in strTmp)
                        {
                            iPos++;
                            pgbFormateProgress.Value = iPos*100 / iMaxPrg;
                            if (ch == '{')
                            {
                                iLen++;
                                strTarget = strTarget + "\r\n" + strFormate + "{" + "\r\n";
                                strFormate = GetMewFormate(iLen);
                                strTarget = strTarget + strFormate ;
                            }
                            else if (ch == '}')
                            {
                                iLen--;
                                strFormate = GetMewFormate(iLen);
                                strTarget = strTarget + "\r\n" + strFormate + "}" + "\r\n" + strFormate;
                            }
                            else if (ch == ';')
                            {
                                strTarget = strTarget + ";" + "\r\n" + strFormate;
                            }
                            else
                            {
                                strTarget = strTarget + ch;
                            }
                        }
                        rtbNewFile.Text = strTarget;
                        myStream.Close();
                        sr.Close();
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message.ToString());
                }
            }
        }

        private string GetMewFormate(int ilen)
        {
            string strTemp = string.Empty;
            for (int i = 0; i < ilen; i++)
            {
                strTemp = strTemp + "\t";
            }
            return strTemp;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            sfdNewFile.InitialDirectory = "c:\\";
            sfdNewFile.Filter = "js files (*.js)|*.js|txt files (*.txt)|*.txt|All files (*.*)|*.*";
            sfdNewFile.FilterIndex = 0;
            sfdNewFile.RestoreDirectory = true;
            sfdNewFile.FileName = "New_"+strNewFileName;
            //Stream myStream;
            if (sfdNewFile.ShowDialog() == DialogResult.OK)
            {
                FileStream fs = (FileStream)sfdNewFile.OpenFile();
                string strText = rtbNewFile.Text;
                byte[] arr = new UTF8Encoding(true).GetBytes(strText);
                fs.Write(arr, 0, arr.Length);
                fs.Flush();
                fs.Close();
            }
        }
        private bool tryCanvertString(string sValue)
        {
            bool bIsConvertOk = true;
            int iCvt = -1;
            try
            {
                iCvt = Convert.ToInt32(sValue.Trim());
            }
            catch (Exception ex)
            {
                bIsConvertOk = false;
            }
            return bIsConvertOk;
        }
        private void btnCodeFormate_Click(object sender, EventArgs e)
        {
            string xx=this.rtbNewFile.Text;
            int i=rtbNewFile.Lines.Count();
            string sTarget="";
            foreach(string sValue in rtbNewFile.Lines)
            {
                if (sValue.Trim().Length < 1)
                {
                    sTarget += (sValue + "\r\n");
                    continue; 
                }
                string strTemp = sValue;
                int j = 0;
                for (int k = 0; k < 4; k++)
                {
                    try
                    {
                        string sPrefix = strTemp.Substring(j, 1);
                    
                        if (tryCanvertString(sPrefix))
                        {
                            strTemp = strTemp.Substring(1);
                            j = 0;
                        }
                        else
                        {
                            j = k;
                            continue;
                        }
                    }
                    catch (Exception ex)
                    {
                        continue;
                    }
                }
                sTarget += (strTemp + "\r\n");
            }
            this.rtbNewFile.Text = sTarget;
            Clipboard.SetText(rtbNewFile.Text.Trim());
        }
    }
}