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
using SFC_Tools.Classes;
using SFC_Tools.Model;
using System.Threading;

namespace SFC_Tools.Forms
{
    public partial class ucXmlTest : UserControl
    {
        public ucXmlTest()
        {
            InitializeComponent();
        }

        private string ConvertLocalDateTimeToUTC(DateTime dtNow)
        {
            string sUtcTime = string.Empty;
            sUtcTime = dtNow.ToUniversalTime().ToString("yyyy-MM-ddTHH:mm:ss") + dtNow.ToString("zzz");
            return sUtcTime;
        }
        private string ConvertLocalDateTimeToUTC(string  sdtNow)
        {
            if (sdtNow == null)
                return string.Empty;
            if (sdtNow.Trim().Length == 0)
                return string.Empty;
           
            string[] dt = sdtNow.Split('-');
            DateTime dtNow = new DateTime(Convert.ToInt32(dt[0]), Convert.ToInt32(dt[1]), Convert.ToInt32(dt[2]), Convert.ToInt32(dt[3])
                ,Convert.ToInt32(dt[4]),Convert.ToInt32(dt[5]));
            string sUtcTime = string.Empty;
            sUtcTime = dtNow.ToUniversalTime().ToString("yyyy-MM-ddTHH:mm:ss") + dtNow.ToString("zzz");
            return sUtcTime;
        }

        private string ConvertLocalDateTimeToUTCA(string sdtNow)
        {
            if (sdtNow == null)
                return string.Empty;
            if (sdtNow.Trim().Length == 0)
                return string.Empty;

            string[] dt = sdtNow.Split('-');
            DateTime dtNow = new DateTime(Convert.ToInt32(dt[0]), Convert.ToInt32(dt[1]), Convert.ToInt32(dt[2]), Convert.ToInt32(dt[3])
                , Convert.ToInt32(dt[4]), Convert.ToInt32(dt[5]));
            string sUtcTime = string.Empty;
            sUtcTime = dtNow.ToUniversalTime().ToString("yyyyMMddTHHmmss");
            return sUtcTime;
        }
        private void btnReadXml_Click(object sender, EventArgs e)
        {

            DateTime dtNow = DateTime.Now;
            dtNow = dtNow.AddHours(8);
            string xx = dtNow.ToString("yyyy-MM-dd-HH-mm-ss");
            MessageBox.Show(xx);
            return;
            rtxtXmlFiles.Clear();
            string sBasePath = "G:\\Rain.Liu\\Web\\FTPRoot\\RAINTEST\\";
            string sPath;

            XmlFileRW xmlWriter = new XmlFileRW();
            BrFenixDell bfd = new BrFenixDell();
            bfd.QUANTITY = 1;
            bfd.STARTTIME = ConvertLocalDateTimeToUTC(DateTime.Now);
            Thread.Sleep(1000);
            bfd.ENDTIME = ConvertLocalDateTimeToUTC(DateTime.Now);
            /*bfd.GUID = "8ae52b8b-130e-452b-94d3-dc3cf25d1100";
              bfd.STATIONNAME = "FT1";
              bfd.EMP = "F1038723";
              bfd.CATEGORY = "MB";
              bfd.MFNAME = "Foxconn";
              bfd.MFSITE = "FENIX";
              bfd.MFPN = "5HVFH";
              bfd.MFSN = "BR05HVFH1081936L00DGA02";
              bfd.TESTGRADE = "PASS";
              bfd.TESTSTARTTIME = bfd.STARTTIME;
              bfd.TESTENDTIME = bfd.ENDTIME;
              bfd.TESTLINE = "FT1_46";*/
            string sStation = "'FT','OOBA'";
            ArrayList arrUnitLists = SFC_Tools.SFCStartup.dba.GetUnitReportInfo("20120723", sStation, "8ae52b-130e-452b-94d3-dc3cf25d1100", "MB", "FOXCONN", "FENIX");
            for (int i = 0; i < arrUnitLists.Count; i++)
            {
                BrFenixDell bfdTarget = (BrFenixDell)arrUnitLists[i];
                string sName = "SigmaProbe_";
                sName = sName + bfdTarget.CATEGORY + "_";
                sName = sName + bfdTarget.MFNAME + "_";
                sName = sName + bfdTarget.MFSITE + "_";
                sName = sName + bfdTarget.STATIONNAME + "_";
                sName = sName + ConvertLocalDateTimeToUTCA(bfdTarget.TESTSTARTTIME) + "_";
                sName = sName + bfdTarget.MFSN + ".xml";
                sPath = sBasePath + sName;
                if (File.Exists(sPath))
                    File.Delete(sPath);
                bfdTarget.STARTTIME = ConvertLocalDateTimeToUTC(bfdTarget.STARTTIME);
                bfdTarget.ENDTIME = ConvertLocalDateTimeToUTC(bfdTarget.ENDTIME);
                bfdTarget.TESTSTARTTIME = ConvertLocalDateTimeToUTC(bfdTarget.TESTSTARTTIME);
                bfdTarget.TESTENDTIME = ConvertLocalDateTimeToUTC(bfdTarget.TESTENDTIME);
                xmlWriter.WriteXmlFile(sPath, bfdTarget);
                StreamReader sr = new StreamReader(sPath);
                while (sr.Peek() > 0)
                {
                    rtxtXmlFiles.AppendText(sr.ReadLine() + "\r\n");
                }
                sr.Close();
            }
            ArrayList arrLotPassLists = SFC_Tools.SFCStartup.dba.GetPassLotReportInfo("20120723", sStation, "8ae52b-130e-452b-94d3-dc3cf25d1100", "MB", "FOXCONN", "FENIX");
            for (int i = 0; i < arrLotPassLists.Count; i++)
            {
                BrFenixDell bfdTarget = (BrFenixDell)arrLotPassLists[i];

                string sName = "SigmaProbe_";
                sName = sName + bfdTarget.CATEGORY + "_";
                sName = sName + bfdTarget.MFNAME + "_";
                sName = sName + bfdTarget.MFSITE + "_";
                sName = sName + bfdTarget.STATIONNAME + "_";
                sName = sName + ConvertLocalDateTimeToUTCA(bfdTarget.TESTSTARTTIME) + "_";
                sName = sName + bfdTarget.MFSN.Replace("-", "") + ".xml";
                sPath = sBasePath + sName;
                if (File.Exists(sPath))
                    File.Delete(sPath);
                bfdTarget.STARTTIME = ConvertLocalDateTimeToUTC(bfdTarget.STARTTIME);
                bfdTarget.ENDTIME = ConvertLocalDateTimeToUTC(bfdTarget.ENDTIME);
                bfdTarget.TESTSTARTTIME = ConvertLocalDateTimeToUTC(bfdTarget.TESTSTARTTIME);
                bfdTarget.TESTENDTIME = ConvertLocalDateTimeToUTC(bfdTarget.TESTENDTIME);
                xmlWriter.WriteXmlFileLot(sPath, bfdTarget);
                StreamReader sr = new StreamReader(sPath);
                while (sr.Peek() > 0)
                {
                    rtxtXmlFiles.AppendText(sr.ReadLine() + "\r\n");
                }
                sr.Close();
            }
            ArrayList arrLotFailLists = SFC_Tools.SFCStartup.dba.GetFailLotReportInfo("20120723", sStation, "8ae52b-130e-452b-94d3-dc3cf25d1100", "MB", "FOXCONN", "FENIX");
            for (int i = 0; i < arrLotFailLists.Count; i++)
            {
                BrFenixDell bfdTarget = (BrFenixDell)arrLotFailLists[i];

                string sName = "SigmaProbe_";
                sName = sName + bfdTarget.CATEGORY + "_";
                sName = sName + bfdTarget.MFNAME + "_";
                sName = sName + bfdTarget.MFSITE + "_";
                sName = sName + bfdTarget.STATIONNAME + "_";
                sName = sName + ConvertLocalDateTimeToUTCA(bfdTarget.TESTSTARTTIME) + "_";
                sName = sName + bfdTarget.MFSN.Replace("-","") + ".xml";
                sPath = sBasePath + sName;
                if (File.Exists(sPath))
                    File.Delete(sPath);
                bfdTarget.STARTTIME = ConvertLocalDateTimeToUTC(bfdTarget.STARTTIME);
                bfdTarget.ENDTIME = ConvertLocalDateTimeToUTC(bfdTarget.ENDTIME);
                bfdTarget.TESTSTARTTIME = ConvertLocalDateTimeToUTC(bfdTarget.TESTSTARTTIME);
                bfdTarget.TESTENDTIME = ConvertLocalDateTimeToUTC(bfdTarget.TESTENDTIME);
                xmlWriter.WriteXmlFileLot(sPath, bfdTarget);
                StreamReader sr = new StreamReader(sPath);
                while (sr.Peek() > 0)
                {
                    rtxtXmlFiles.AppendText(sr.ReadLine() + "\r\n");
                }
                sr.Close();
            }
            //xmlWriter.WriteXmlFileA(@"G:\\Rain.Liu\\Web\\FTPRoot\\RAINTESTA.xml");
            /* FileInfo f = new FileInfo(sPath);
             this.rtxtXmlFiles.AppendText(xmlWriter.GetXmlFileInfo(f.FullName));*/
            /*
            DirectoryInfo di = new DirectoryInfo(@"G:\Rain.Liu\Web\FTPRoot\MB_Foxconn_Jundiai_20130623T120116");
            foreach (FileInfo fi in di.GetFiles("*.xml"))
            {
                string strFilePath;
                strFilePath = fi.ToString();
                
                XmlFileRW xml = new XmlFileRW();
                string sGrade=xml.GetXmlFileGrade(di.FullName + "\\" + strFilePath);
                strFilePath=strFilePath+ "---"+sGrade +"\r\n";
                this.rtxtXmlFiles.AppendText(strFilePath);
            }
            this.rtxtXmlFiles.AppendText(rtxtXmlFiles.Lines.Count().ToString());
             * */
        }
    }
}

