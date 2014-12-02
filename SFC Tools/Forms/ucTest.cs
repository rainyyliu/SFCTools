using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Reflection;
using LabelManager2;
using System.Threading;
using System.Globalization;
using System.Security.Cryptography;
using System.Collections;
using CommonFuns;
using System.Resources;
using System.Xml;
using SFC_Tools.Model;
using System.Text.RegularExpressions;
using CoreLibs;
using System.Net;
using SFC_Tools.ServiceReference2;
using SFC_Tools.ServiceReference3;
using SFC_Tools.ServiceReference4;
using System.Runtime.InteropServices;
using System.Runtime;
using System.Drawing.Printing;
using System.Drawing.Drawing2D;

namespace SFC_Tools
{

    public partial class ucTest : UserControl
    {
        string barcode = "MC606ZMCL";
        int snEnd = 6;
        int snStart = 4;
        char[] carryChrArr = "0123456789ABCDEFGHIJKLMNPRSTUVWXYZ-+".ToCharArray();
        int carryNum = "0123456789ABCDEFGHIJKLMNPRSTUVWXYZ-+".Length;
        ProgressBar pgbPrint;

        //LabelManager2.Application ac = new LabelManager2.Application();
        public ucTest()
        {
            InitializeComponent();
            pgbPrint = new ProgressBar();
            pgbPrint.Parent = panel1;
            pgbPrint.Dock = DockStyle.Fill;
            pgbPrint.Visible = false;
        }

        private void glsBtnPre_Click(object sender, EventArgs e)
        {
            int i = 0;
            while(i<50)
            {
                string xx=add(1);
                MessageBox.Show(xx);
                i++;
            }
        }

        public  string add(int num)
        {
            int chrInt = 0;
            char[] barChrArr = barcode.ToCharArray();

            for (int i = snEnd - 1; i > snStart - 2; i--)
            {
                chrInt = this.transferCharToInt(this.carryChrArr, barChrArr[i]);
                chrInt = chrInt + num;
                barChrArr[i] = this.transferIntToChar(this.carryChrArr, chrInt % this.carryNum);

                num = chrInt / this.carryNum;
                if (num == 0)
                    break;
            }

            this.barcode = new string(barChrArr);
            return this.barcode;
        }
        public char transferIntToChar(char[] chrArr, int num)
        {
            if (num < 0 || num > chrArr.Length - 1)
                throw new Exception("錯誤﹕數字:" + num + "超出該進位范圍﹗");
            //throw new Exception("Error:  The number:"+num+" is bigger than the carry number");

            return chrArr[num];
        }
        public int transferCharToInt(char[] chrArr, char chr)
        {
            int startIndex = -1;
            int endIndex = chrArr.Length;
            int i = 0;
            for (i = 0; i < endIndex; i++)
            {
                if (chr == chrArr[i])
                {
                    startIndex = i;
                    break;
                }
            }
            if (startIndex == -1)
                throw new Exception("錯誤﹕進位字符串<" + new string(chrArr) + ">中沒有包含字符<" + chr + ">");
            return i;
        }
        public void MarkWater(string filePath, string waterFile)
        {
            //GIF不水印    
            int i = filePath.LastIndexOf(".");
            string ex = filePath.Substring(i, filePath.Length - i);
            if (string.Compare(ex, ".gif", true) == 0)
            {
                return;
            }
            string BasePath = System.IO.Directory.GetCurrentDirectory();
            string ModifyImagePath = BasePath + filePath;//修改的图像路径    
            int lucencyPercent = 25;
           System.Drawing.Image modifyImage = null;
           System.Drawing.Image drawedImage = null;
            Graphics g = null;
            try
            {
                //建立图形对象    
                modifyImage = System.Drawing.Image.FromFile(ModifyImagePath, true);
                drawedImage = System.Drawing.Image.FromFile(BasePath + waterFile, true);
                g = Graphics.FromImage(modifyImage);
                //获取要绘制图形坐标    
                int x = modifyImage.Width - drawedImage.Width;
                int y = modifyImage.Height - drawedImage.Height;
                //设置颜色矩阵    
                float[][] matrixItems ={    
            new float[] {1, 0, 0, 0, 0},    
            new float[] {0, 1, 0, 0, 0},    
            new float[] {0, 0, 1, 0, 0},    
            new float[] {0, 0, 0, (float)lucencyPercent/100f, 0},    
            new float[] {0, 0, 0, 0, 1}};

                ColorMatrix colorMatrix = new ColorMatrix(matrixItems);
                ImageAttributes imgAttr = new ImageAttributes();
                imgAttr.SetColorMatrix(colorMatrix, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);
                //绘制阴影图像    
                g.DrawImage(drawedImage, new Rectangle(x, y, drawedImage.Width, drawedImage.Height), 10, 10, drawedImage.Width, drawedImage.Height, GraphicsUnit.Pixel, imgAttr);
                //保存文件    
                string[] allowImageType = { ".jpg", ".gif", ".png", ".bmp", ".tiff", ".wmf", ".ico" };
                FileInfo fi = new FileInfo(ModifyImagePath);
                ImageFormat imageType = ImageFormat.Gif;
                switch (fi.Extension.ToLower())
                {
                    case ".jpg": imageType = ImageFormat.Jpeg; break;
                    case ".gif": imageType = ImageFormat.Gif; break;
                    case ".png": imageType = ImageFormat.Png; break;
                    case ".bmp": imageType = ImageFormat.Bmp; break;
                    case ".tif": imageType = ImageFormat.Tiff; break;
                    case ".wmf": imageType = ImageFormat.Wmf; break;
                    case ".ico": imageType = ImageFormat.Icon; break;
                    default: break;
                }
                MemoryStream ms = new MemoryStream();
                modifyImage.Save(ms, imageType);
                byte[] imgData = ms.ToArray();
                modifyImage.Dispose();
                drawedImage.Dispose();
                g.Dispose();
                FileStream fs = null;
                File.Delete(ModifyImagePath);
                fs = new FileStream(ModifyImagePath, FileMode.Create, FileAccess.Write);
                if (fs != null)
                {
                    fs.Write(imgData, 0, imgData.Length);
                    fs.Close();
                }
            }
            finally
            {
                try
                {
                    drawedImage.Dispose();
                    modifyImage.Dispose();
                    g.Dispose();
                }
                catch
                {
                }
            }
        }

        private void glassButton1_Click(object sender, EventArgs e)
        {
            //LabelManager2.ApplicationClass ac = new LabelManager2.ApplicationClass();
            LabelManager2.Application ac = new LabelManager2.Application();

            ac.Documents.Open(@"C:\V1.Lab", true);
            ac.Visible = false;

            Document dc = ac.ActiveDocument;

            //dc.Variables.FormVariables.Item("V1").Value = "123";
            dc.Variables.FreeVariables.Item("V1").Value = "Hello World!";
            dc.PrintDocument(1);

            ac.Documents.CloseAll(true);
            
        }

        private void ucTest_Load(object sender, EventArgs e)
        {
            listView1.Clear();
            listView1.LargeImageList = imageList1;
            listView1.Items.Add("设置上下班时间", "设置上下班时间",0);
            listView1.Items.Add("是否启用短信提醒", "是否启用短信提醒", 1);
            listView1.Items.Add("设置密码", "设置密码",2);
           // MessageBox.Show("Load");
        }

        private void ucTest_Leave(object sender, EventArgs e)
        {
           // MessageBox.Show("Leave");
            
        }

        private void glassButton2_Click(object sender, EventArgs e)
        {
            string strTemp = "xxxxxxxxxxxxxx\r\nttttttttttttt\r\npppppppppppp\r\n";
            strTemp=strTemp.Replace("\r\n", "|");
            MessageBox.Show(strTemp);
        }  
  
        ~ucTest()
        {
            MessageBox.Show("shit");
            //this.ac.Documents.CloseAll();
            //this.ac.Quit();
        }

        private void ucTest_ControlRemoved(object sender, ControlEventArgs e)
        {
            MessageBox.Show("0.0");
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            
            MessageBox.Show(timer1.Interval.ToString()+e.ToString());
        }

        private void glassButton3_Click(object sender, EventArgs e)
        {
            timer1.Start();
            
        }

        private void glassButton4_Click(object sender, EventArgs e)
        {
            ///*
            //ExcelFileRead a = new ExcelFileRead();
            //a.OpenExcel(@"E:\YY.xlsx");
            //int xx=a.GetRowsCount();
            //MessageBox.Show(xx.ToString());
            //a.QuitExcel();
            // * */
            //ExcelFileWrite efw = new ExcelFileWrite();
            //efw.OpenExcel("");
            ////efw.SetCellsAlignment("A1", 3, 2);//V 234,H123
            //efw.SetWorkSheet(1);
            //efw.WriteCellValue("XXX", "A1");
            ////efw.SaveAs(@"E:\TT.xlsx");
            //efw.SaveAs("");
            //efw.QuitExcel();
        }

        private void gbtnLocalEnZh_Click(object sender, EventArgs e)
        {
            //if (this.gbtnLocalEnZh.Text == "ZH")
            //{
            //    this.gbtnLocalEnZh.Text = "EN";
            //    Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo("en");
            //    ApplyResource();
            //}
            //else
            //{
            //    this.gbtnLocalEnZh.Text = "ZH";
            //    Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo("zh-CN");
            //    ApplyResource();
            //}
        }
        private void ApplyResource()
        {
            //System.ComponentModel.ComponentResourceManager res = new ComponentResourceManager(typeof(ucTest));
            //foreach (Control ctl in Controls)
            //{
            //    res.ApplyResources(ctl, ctl.Name);
            //}
            ////菜单
            ////foreach (ToolStripMenuItem item in this.menuStrip1.Items)
            ////{
            ////    res.ApplyResources(item, item.Name);
            ////    foreach (ToolStripMenuItem subItem in item.DropDownItems)
            ////    {
            ////        res.ApplyResources(subItem, subItem.Name);
            ////    }
            ////}
            ////Caption
            //res.ApplyResources(this, "$this");
        }

        private void gbtnMd5_Click(object sender, EventArgs e)
        {
            MD5 myMd5 = MD5.Create();
            byte[]xx= myMd5.ComputeHash(Encoding.Default.GetBytes("hellow World"));
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < xx.Length; i++)
            {
                sb.Append(xx[i].ToString("X2"));
            }
            //MessageBox.Show(sb.ToString());

            string tt=(Convert.ToBase64String(Encoding.Default.GetBytes("Hello World")));
            MessageBox.Show(tt);

            byte[] xxx = Convert.FromBase64String(tt);
            string sb1=Encoding.Default.GetString(xxx);
            
            MessageBox.Show(sb1);
        }

        private void gbtnNextChr_Click(object sender, EventArgs e)
        {
            /*
            GetNextXChar gnx = new GetNextXChar();
            gnx.GetNextXCharByByte("CCAF78DC342F", "0123456789ABCDEF");
            string strXX=gnx.GetChar(1);
            MessageBox.Show(strXX);
             * */
            //MessageBox.Show(TestNextChr("CCAF78DC342F", "0123456789ABCDEF",10));
            RinGetXChar rgx = new RinGetXChar();
            ArrayList arrBase = new ArrayList();
            arrBase.Add("0123");
            arrBase.Add("456");
            arrBase.Add("23");
            arrBase.Add("FGTZ");
            //MessageBox.Show(rgx.GetNextXChar("342F", "0123456789ABCDEF",10));
            for (int i = 0; i < 30;i+=2 )
                MessageBox.Show(rgx.GetNextXChar("342F", arrBase, i));
        }
        private string TestNextChr(string strInput, string strBase,int iStep=1)
        {
            string strRet = string.Empty;  
            char[] chInput;
            chInput = strInput.ToCharArray();
            int iForward = iStep;
            int iBaseLen = strBase.Length;
            for (int i = strInput.Length - 1; i >= 0; i--)
            { 
                int iPos=0;
                iPos = strBase.IndexOf(chInput[i]);
                if (iPos < 0)
                {
                    return("ERROR-0x000001:"+"Base String Error!");
                }
                iPos = iPos + iForward;
                iForward = iPos / iBaseLen;
                iPos = iPos % iBaseLen;
                chInput[i]=strBase[iPos];
            }
            if (iForward > 0)
            {
                return ("ERROR-0x000002" + "Over Flow Happen,Pelease input the right string!");
            }
            strRet = new string(chInput);
            return strRet;
        }
        private void gbtnProgress_Click(object sender, EventArgs e)
        {
            this.pgbPrint.Visible = true;
            this.pgbPrint.Maximum = 100000;
            for (int i = 0; i < 10000; i++)
            {
                setPgbPos(pgbPrint.Value + 1);
            }
            testPgb();
        }
        private void testPgb()
        {
            try{

                for (int i = 0; i < 10000; i++)
                {
                    setPgbPos(pgbPrint.Value + 1);
                }
                for (int i = 0; i < 80000; i++)
                {
                    setPgbPos(pgbPrint.Value + 1);
                }
            }catch(Exception ex)
            {
                
            }
        }
        private void setPgbPos(int iPos)
        {
            pgbPrint.Value = iPos;
        }

        private void gbtnMoCheck_Click(object sender, EventArgs e)
        {
          ArrayList arrSns= SFCStartup.dba.GetSNsByMoOpition("NVIDIAP140");
          for (int i = 0; i < arrSns.Count; i++)
          {
              Model.SnInfo si = (Model.SnInfo)arrSns[i];
              for (int j = i+1; j < arrSns.Count; j++)
              {
                  Model.SnInfo sij = (Model.SnInfo)arrSns[j];
                  if (Convert.ToDecimal(sij.START_SN) >= Convert.ToDecimal(si.START_SN) && Convert.ToDecimal(sij.START_SN) <= Convert.ToDecimal(si.END_SN))
                  {
                      MessageBox.Show("Error:"+i.ToString()+"--"+j.ToString()+"--"+sij.START_SN);
                      return;
                  }
                  if (Convert.ToDecimal(sij.END_SN) >= Convert.ToDecimal(si.START_SN) && Convert.ToDecimal(sij.END_SN) <= Convert.ToDecimal(si.END_SN))
                  {
                      MessageBox.Show("Error:" + i.ToString() + "--" + j.ToString() + "--" + sij.START_SN);
                      return;
                  }

                  if (Convert.ToDecimal(si.START_SN) >= Convert.ToDecimal(sij.START_SN) && Convert.ToDecimal(si.START_SN) <= Convert.ToDecimal(sij.END_SN))
                  {
                      MessageBox.Show("Error:" + i.ToString() + "--" + j.ToString() + "--" + sij.START_SN);
                      return;
                  }
                  if (Convert.ToDecimal(si.END_SN) >= Convert.ToDecimal(sij.START_SN) && Convert.ToDecimal(si.END_SN) <= Convert.ToDecimal(sij.END_SN))
                  {
                      MessageBox.Show("Error:" + i.ToString() + "--" + j.ToString() + "--" + sij.START_SN);
                      return;
                  }
              }
          }
          MessageBox.Show(Convert.ToDecimal(((Model.SnInfo)arrSns[0]).END_SN).ToString());
          MessageBox.Show("Good!");
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void gbtnDeCode_Click(object sender, EventArgs e)
        {
            CommonFuns.Encrypt test = new Encrypt();
            //this.txtValue.Text = test.Decode("/a6eITh6FakRgy4SPiwqwmsl6H8YSIxubVaV33Bd2U6PDPwYRevk3jdr1WGsIkF0t55k0MJHXnDHu25KdxOFQWTUhZagpeERuTIx3GdO6Po=");
            //this.txtValue.Text = test.Decode("mlHKjg9+4Dt025BCQCQr+Q==");
            
            //this.txtValue.Text = test.Decode("bIrW63eTzVK1xzuZzYIXqQ==");
            //this.txtValue.Text = txtValue.Text + "[" + test.Decode("0OrruMbzkz4=") + "]";
            

            string strInput = txtValue.Text;
            txtValue.Text=test.Decode(strInput);
            /*myBase64 myb64 = new myBase64();
            string outPut = string.Empty;
            outPut = myb64.Base64Code(strInput);
            outPut = outPut + "--";
            outPut = outPut + new Encrypt().Encode(strInput);
            txtValue.Text = "[" + strInput + "]" + outPut;
            string strtemp;
            strtemp = strInput;
            */
                    
        }

        private void bgtnPCM_Click(object sender, EventArgs e)
        {
            

            bool isValid = true;
			BlockInfo blockInfo;

            ArrayList blockMacCurList = (ArrayList)SFCStartup.dba.getBlockMACCurrent("002481");
			for(int i=0;i<blockMacCurList.Count;i++)
			{
				blockInfo = (BlockInfo)blockMacCurList[i];

				long intBlockStartNew = Int64.Parse(txtStart.Text,System.Globalization.NumberStyles.AllowHexSpecifier);
				long intBlockEndNew = Int64.Parse(txtEnd.Text,System.Globalization.NumberStyles.AllowHexSpecifier);
				long intBlockStart = Int64.Parse(blockInfo.BlockStart,System.Globalization.NumberStyles.AllowHexSpecifier);
				long intBlockEnd = Int64.Parse(blockInfo.BlockEnd,System.Globalization.NumberStyles.AllowHexSpecifier);

                //if((intBlockStartNew>intBlockStart && intBlockEndNew<intBlockEnd)
                //    ||(intBlockStartNew<intBlockStart && intBlockEndNew>intBlockStart)
                //    ||(intBlockStartNew<intBlockEnd && intBlockEndNew>intBlockEnd))
                //{
                //    isValid = false;
                //    break;
                //}
                //Condition A
                if (intBlockStartNew >= intBlockStart && intBlockEndNew <= intBlockEnd)
                {
                    isValid = false;
                    break;
                }
                //Condition B
                if (intBlockStartNew <= intBlockStart && intBlockEndNew >= intBlockStart)
                {
                    isValid = false;
                    break;
                }
                //Condition C
                if (intBlockStartNew <= intBlockEnd && intBlockEndNew >= intBlockEnd)
                {
                    isValid = false;
                    break;
                } 
			}
            MessageBox.Show(isValid.ToString());

        }

        private void gBtnOracle_Click(object sender, EventArgs e)
        {
            MessageBox.Show(SFCStartup.dba.TestOracleParam());
        }
        
        private void button1_Click(object sender, EventArgs e)
        {
            listView1.Dock = DockStyle.None;
            button1.Dock = DockStyle.Top;
            button2.Dock = DockStyle.Bottom;
            button3.SendToBack();
            button3.Dock = DockStyle.Bottom;
            button5.SendToBack();
            button5.Dock = DockStyle.Bottom;
            button6.SendToBack();
            button6.Dock = DockStyle.Bottom;
            listView1.BringToFront();
            listView1.Dock = DockStyle.Bottom;
            listView1.Clear();
            listView1.Items.Add("设置上下班时间", "设置上下班时间", 0);
            listView1.Items.Add("是否启用短信提醒", "是否启用短信提醒", 1);
            listView1.Items.Add("设置密码", "设置密码", 2);
        }
        
        private void button2_Click(object sender, EventArgs e)
        {
            listView1.Dock = DockStyle.None;
            button2.Dock = DockStyle.Top;
            button1.SendToBack();
            button1.Dock = DockStyle.Top;
            button3.SendToBack();
            button3.Dock = DockStyle.Bottom;
            button5.SendToBack();
            button5.Dock = DockStyle.Bottom;
            button6.SendToBack();
            button6.Dock = DockStyle.Bottom;
            listView1.Dock = DockStyle.Bottom;
            listView1.Clear();
            listView1.Items.Add("近期工作记录","近期工作记录",3);
            listView1.Items.Add("近期工作计划", "近期工作计划", 4);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            listView1.Dock = DockStyle.None;
            button3.SendToBack();
            button3.Dock = DockStyle.Top;
            button2.SendToBack();
            button2.Dock = DockStyle.Top;
            button1.SendToBack();
            button1.Dock = DockStyle.Top;
            button5.SendToBack();
            button5.Dock = DockStyle.Bottom;
            button6.SendToBack();
            button6.Dock = DockStyle.Bottom;
            listView1.Dock = DockStyle.Bottom;
            listView1.Clear();
            listView1.Items.Add("编辑工作进 度报告","编辑工作进度报告",5);
            listView1.Items.Add("编辑项目设计图", "编辑项目设计图", 6);
        }

        

        private void button5_Click(object sender, EventArgs e)
        {
            listView1.Dock = DockStyle.None;
            button5.SendToBack();
            button5.Dock = DockStyle.Top;
            button3.SendToBack();
            button3.Dock = DockStyle.Top;
            button2.SendToBack();
            button2.Dock = DockStyle.Top;
            button1.SendToBack();
            button1.Dock = DockStyle.Top;
            button6.SendToBack();
            button6.Dock = DockStyle.Bottom;
            listView1.Dock = DockStyle.Bottom;
            listView1.Clear();
            listView1.Items.Add("编辑工作进 度报告", "编辑工作进度报告", 5);
            listView1.Items.Add("编辑项目设计图", "编辑项目设计图", 6);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            listView1.Dock = DockStyle.None;
            button6.SendToBack();
            button6.Dock = DockStyle.Top;
            button5.SendToBack();
            button5.Dock = DockStyle.Top;
            button3.SendToBack();
            button3.Dock = DockStyle.Top;
            button2.SendToBack();
            button2.Dock = DockStyle.Top;
            button1.SendToBack();
            button1.Dock = DockStyle.Top;
           
            listView1.Dock = DockStyle.Bottom;
            listView1.Clear();
            listView1.Items.Add("AAAA", "AAAA", 7);
            listView1.Items.Add("BBBB", "BBB", 8);
        }


        private void gbtCountChar_Click(object sender, EventArgs e)
        {
            GetLogcationQtyByStr("sxx",1);
        }
        private int GetLogcationQtyByStr(string sLoc, int iFlag)
        {
            int iQty = 0;
            //string strTarget;
            //strTarget = sLoc.Replace(",", "");
            //iQty = sLoc.Length - strTarget.Length;
            //MessageBox.Show(iQty.ToString());
            iQty = Regex.Matches(sLoc, ",").Count;
            // MessageBox.Show(iQty.ToString());
            if (iFlag==0)
                iQty++;
            return iQty;
        }

        private void gbtnHex_Click(object sender, EventArgs e)
        {
            string sHex = "51865EBFAF4B146B1B14E69ABF62D9EFD4D6B7D8998D868AE0CDA42FE1F5935E";
            byte[] bt = new byte[sHex.Length / 2];
            for(int i=0;i<bt.Length;i++)
            {
                try{
                    bt[i]=byte.Parse(sHex.Substring(i*2,2),System.Globalization.NumberStyles.HexNumber);
                }catch(Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }

            System.Text.Encoding chs = System.Text.Encoding.GetEncoding("gb2312");            
            string sRes=chs.GetString(bt);
            MessageBox.Show(sRes);

        }

        private void gbtnEnCodeTest_Click(object sender, EventArgs e)
        {
            string sInput = txtValue.Text.Trim();
            //MessageBox.Show(CoreLibs.Cryptography.eFoxEnDecryption.Encode(sInput));
            CommonFuns.Encrypt xx = new CommonFuns.Encrypt();
            //MessageBox.Show(xx.Encode(sInput));
            string xxx = "B5JOgP35R7d16bDxnz34s+/qAFTclHUmy97vZmJQMy6RbtHcp5ryVIqXYxBx8JHfqnCR/tJf6zc=";
            txtValue.Text = xx.Encode(sInput);
            MessageBox.Show(xx.Decode(xxx));
            string tt=xx.Encode("F1040893");
            MessageBox.Show(tt);
            //byte[] myEmpNo = Convert.FromBase64String("ASQ9BM+KBl9VcmQYILI1HQ==");
            //System.Text.Encoding encode = System.Text.Encoding.UTF8;
            //MessageBox.Show( encode.GetString(myEmpNo));
           // MessageBox.Show(xx.Decode("F5J6CQGR2A6y9m666gys0JFRPCCPpFHBIJFpPTEl5ERRP5EI0"));
        }

        private void gbtnUrlTest_Click(object sender, EventArgs e)
        {

            ServiceReference4.DataServiceSoapClient MyXX = new DataServiceSoapClient();
            string []sTT=MyXX.GetOnlinePersons();
            MyXX.LoadMessage("F3214045");
            
            ServiceReference3.AccreditInfoSoapClient myTemp= new AccreditInfoSoapClient();
            //myTemp.Open();
            //獲取離職人員信息(臺灣)
            DataSet dsXX=myTemp.GetResignedEployeesInfo("20130501", "20130505");
           
           
            ServiceReference2.AuthenticateServiceSoapClient xx = new ServiceReference2.AuthenticateServiceSoapClient("AuthenticateServiceSoap1");
            ServiceReference2.MembershipUser test;
            
            test=xx.GetUser("F1037969");
            string temp=xx.GetPostCerName();
            string xxxxx = test.Email.ToString();
            string tt = test.Comment.ToString();
          
            //in system.net class
            string sUrl = "http://its.cmmsg.efoxconn.com/defaultA.asp";
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(sUrl);
            request.Timeout = 3000;
            request.Headers.Set("Pragma", "no-cache");
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream streamReceive = response.GetResponseStream();
            Encoding encoding = Encoding.GetEncoding("GB2312");
            StreamReader streamReader = new StreamReader(streamReceive, encoding);
            MessageBox.Show(streamReader.ReadToEnd());
        }

        private void gbtnDateTimeTest_Click(object sender, EventArgs e)
        {
            DateTime dtNow = DateTime.Now;
            MessageBox.Show(ConvertLocalDateTimeToUTC(dtNow));
        }

        private string ConvertLocalDateTimeToUTC(DateTime dtNow)
        {
            string sUtcTime = string.Empty;
            sUtcTime = dtNow.ToUniversalTime().ToString("yyyy-MM-ddTHH:mm:ss")+dtNow.ToString("zzz");
            return sUtcTime;
        }

        private void gbtnMemTest_Click(object sender, EventArgs e)
        {
           // MemoryStream ms = new MemoryStream(1024 * 1024 * 500);
            Test();
        }
        private byte[] Test()
        {
            byte[] arr = new byte[1024 * 1024 * 500];
            for (int i = 0; i < 1024 * 1024 * 500 - 1; i++)
            {
                arr[i] = 0;
            }
            return arr;
        }

        [DllImport("D:\\SendSmtpMail.dll")]
        public static extern void RSetPost(int nPost);

        [DllImport("D:\\SendSmtpMail.dll")]
        public static extern void RSetHost( string strHost );

        [DllImport("D:\\SendSmtpMail.dll", EntryPoint = "DllMain", ExactSpelling = false, CallingConvention = CallingConvention.Cdecl)]
        public static extern void RSetAccount(string strAccount);

        [DllImport("D:\\SendSmtpMail.dll", EntryPoint = "DllMain", ExactSpelling = false, CallingConvention = CallingConvention.Cdecl)]
        public static extern void RSetPassword(string strPassword);

        [DllImport("SendSmtpMail.dll", EntryPoint = "DllMain", ExactSpelling = false, CallingConvention = CallingConvention.Cdecl)]
        public static extern void RSetMailFrom(string strMailFrom);

        [DllImport("SendSmtpMail.dll", EntryPoint = "DllMain", ExactSpelling = false, CallingConvention = CallingConvention.Cdecl)]
        public static extern void RSetSendTo(string strSendTo);

        [DllImport("SendSmtpMail.dll", EntryPoint = "DllMain", ExactSpelling = false, CallingConvention = CallingConvention.Cdecl)]
        public static extern void RSetSubject(string strSubject);

        [DllImport("SendSmtpMail.dll", EntryPoint = "DllMain", ExactSpelling = false, CallingConvention = CallingConvention.Cdecl)]
        public static extern void RSetDateTime(string strDateTime);

        [DllImport("SendSmtpMail.dll", EntryPoint = "DllMain", ExactSpelling = false, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool RAddDataFromString(string strData); 

        [DllImport("SendSmtpMail.dll", EntryPoint = "DllMain", ExactSpelling = false, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool RAddDataFromBuffer(string szData, int iLen); 

        [DllImport("SendSmtpMail.dll", EntryPoint = "DllMain", ExactSpelling = false, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool RAddDataFromFile(string strFileName);

        [DllImport("SendSmtpMail.dll", EntryPoint = "DllMain", ExactSpelling = false, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool RAddAttachedFile( string strFilePath, string strFileName );

        [DllImport("SendSmtpMail.dll", EntryPoint = "DllMain", ExactSpelling = false, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool RSandThread();

        [DllImport("SendSmtpMail.dll", EntryPoint = "DllMain", ExactSpelling = false, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool RStarMailThread();

        [DllImport("SendSmtpMail.dll", EntryPoint = "RAdd", ExactSpelling = false, CallingConvention = CallingConvention.Cdecl)]
        public static extern int  RAdd(int a,int b);

        private void glassButton6_Click(object sender, EventArgs e)
        {
            int xx = RAdd(1, 2);
            RSetHost("smtp.126.com");
            RSetPost(25);
            RSetAccount("yefeng654321");
            RSetPassword("密码");
           RSetMailFrom("yefeng654321@126.com");
            RSetSendTo("yefeng654321@126.com");
           RSetSubject("测试");
           RSetDateTime("2008-12-29");
           RAddDataFromBuffer("123456789", 9);
           RAddAttachedFile("c:\\", "1.txt");
           RStarMailThread();
        }

        private void btnDynamicSql_Click(object sender, EventArgs e)
        {
            string strValue = this.txtValue.Text;
            SFCStartup.dba.bInsertTestSql(strValue,"WLAN MAC");
            MessageBox.Show(SFCStartup.dba.TestDySql());
        }
        private string Get128CodeString(string inputData)
        {
            string result;
            int checksum = 104;
            for (int ii = 0; ii < inputData.Length; ii++)
            {
                if (inputData[ii] >= 32)
                {
                    checksum += (inputData[ii] - 32) * (ii + 1);
                }
                else
                {
                    checksum += (inputData[ii] + 64) * (ii + 1);
                }
            }
            checksum = checksum % 103;
            if (checksum < 95)
            {
                checksum += 32;
            }
            else
            {
                checksum += 100;
            }
            result = Convert.ToChar(204) + inputData.ToString() + Convert.ToChar(checksum) + Convert.ToChar(206);
            return result;
        }

        public void PrintLable()
        {
            PrintDocument pd = new PrintDocument();
            StandardPrintController controler = new StandardPrintController();

            try
            {
                pd.PrintPage += new PrintPageEventHandler(this.PrintCustomLable);
                pd.PrintController = controler;
                pd.Print();
                return;
            }
            catch (Exception err)
            {
                Console.WriteLine(err.Message);
                return;
            }
            finally
            {
                pd.Dispose();
            }

        }
        public void PrintCustomLable(Object Sender, PrintPageEventArgs av)
        {
            Font ft1 = new System.Drawing.Font("Times New Roman", 18, FontStyle.Regular, GraphicsUnit.World);
            Font ft2 = new System.Drawing.Font("Code 128", 64, FontStyle.Regular, GraphicsUnit.World);
            Brush br = new SolidBrush(Color.Black);
            Margins margins = new Margins(50, 50, 50, 145);
            av.PageSettings.Margins = margins;

            av.Graphics.DrawString(Get128CodeString("Hello World"), ft2, br, 50, -3);
            av.Graphics.DrawString("Hello World", ft1, br, 110, 60);
            av.HasMorePages = false;
        }

        private void DrawBarCode()
        {
            int imageWidth = this.pictureBox1.Width;
            int IMAGE_HEIGHT = this.pictureBox1.Height;
            Bitmap image = new Bitmap(imageWidth, IMAGE_HEIGHT);
            Graphics g = Graphics.FromImage(image);
            try{
                g.Clear(Color.White);

            Font ft1 = new System.Drawing.Font("Times New Roman", 18, FontStyle.Regular, GraphicsUnit.World);
            Font ft2 = new System.Drawing.Font("Code 128", 64, FontStyle.Regular, GraphicsUnit.World);
            Brush br = new SolidBrush(Color.Black);
                        

           g.DrawString(Get128CodeString("Hello World"), ft2, br, 0, 0);
           g.DrawString("Hello World", ft1, new SolidBrush(Color.Blue), 80, 65);
           /*
            try
            {
                g.Clear(Color.White);

                Pen imageBorderPan = new Pen(Color.Silver, 2);

                //Draw the border of the image
                g.DrawRectangle(imageBorderPan, 1, 1, imageWidth - 1, IMAGE_HEIGHT - 1);

                Font titleFont = new Font("Arial", 20, FontStyle.Bold);
                SolidBrush titleBrush = new SolidBrush(Color.FromArgb(49, 18, 176));
                g.DrawString(routeName, titleFont, titleBrush, freeSpace, 20);

                //Draw the main line
                g.DrawLine(new Pen(Color.DarkSlateBlue, 5), freeSpace, ROUTE_MAIN_HEIGTH, imageWidth - freeSpace, ROUTE_MAIN_HEIGTH);
                //Draw rain test repair line
                for (int i = 0; i < this.stRepairlRoute.Length; i++)
                {
                    StationGroupModel stationModel = (StationGroupModel)stRepairlRoute[i];
                    int x1 = GetStationPositionX(stationModel.iIndex);
                    int y1 = REPAIR_HEIGTH;

                    int x2 = GetStationPositionX(stationModel.iNextIndex);
                    int y2 = ROUTE_MAIN_HEIGTH;

                    g.DrawLine(new Pen(Color.Red, 3), x1 - 2, y1, x2 - 2, y2);
                }
                //Rain Draw Sepcial Line
                for (int i = 0; i < this.stSpecialRoute.Length; i++)
                {
                    StationGroupModel stationModel = (StationGroupModel)stSpecialRoute[i];
                    int x1 = GetStationPositionX(stationModel.iIndex);
                    int y1 = ROUTE_MAIN_HEIGTH;

                    int x2 = GetStationPositionX(stationModel.iNextIndex);
                    int y2 = ROUTE_MAIN_HEIGTH;

                    int width = Math.Abs(x2 - x1);
                    int height = width / 2;

                    g.DrawArc(new Pen(Color.Blue, 3), x1, (y1 - height / 2 - STATION_WIDTH), width, height, 180, 180);
                }
                //Rain Draw Return Line
                for (int i = 0; i < this.stReturnRoute.Length; i++)
                {
                    StationGroupModel stationModel = (StationGroupModel)stReturnRoute[i];
                    if (stationModel == null)
                        continue;
                    int x1 = GetStationPositionX(stationModel.iIndex);
                    int y1 = REPAIR_HEIGTH;

                    int x2 = GetStationPositionX(stationModel.iNextIndex);
                    int y2 = ROUTE_MAIN_HEIGTH;

                    g.DrawLine(new Pen(Color.Lime, 3), x1 + 2, y1, x2 + 2, y2);

                }
                string imagePath = AppDomain.CurrentDomain.SetupInformation.ApplicationBase + @"images\Com06.ICO";// @"images\Station.jpg";
                System.Drawing.Image newImage = System.Drawing.Image.FromFile(imagePath);

                Font stationFont = new Font("Arial", 8);
                SolidBrush brush = new SolidBrush(Color.Black);
                //rain test main route
                for (int i = 0; i < this.stMainLien.Length; i++)
                {
                    StationGroupModel stationModel = (StationGroupModel)stMainLien[i];
                    int x = GetStationPositionX(stationModel.iIndex);
                    Point point = new Point();
                    point.X = x - STATION_WIDTH;
                    point.Y = ROUTE_MAIN_HEIGTH - STATION_WIDTH;
                    g.DrawImage(newImage, point);

                    point.X = x - Convert.ToInt32((stationModel.GroupName.Length / 2) * stationFont.Size);
                    point.Y = ROUTE_MAIN_HEIGTH + STATION_WIDTH + 10;

                    g.FillRectangle(new SolidBrush(Color.White), point.X, point.Y, stationFont.Size * stationModel.GroupName.Length, stationFont.Size * 2);
                    g.DrawString(stationModel.GroupName, stationFont, brush, point);
                }
                //rain draw repair station
                //Draw repair Station
                for (int i = 0; i < this.stRepairlRoute.Length; i++)
                {
                    StationGroupModel stationModel = (StationGroupModel)stRepairlRoute[i];
                    int x = GetStationPositionX(stationModel.iIndex);
                    //int x = GetStationPositionX(3);
                    int y = REPAIR_HEIGTH;

                    Point point = new Point();
                    point.X = x - STATION_WIDTH;
                    point.Y = REPAIR_HEIGTH - STATION_WIDTH;
                    g.DrawImage(newImage, point);

                    point.X = x - Convert.ToInt32((stationModel.GroupName.Length / 2) * stationFont.Size);
                    point.Y = REPAIR_HEIGTH + STATION_WIDTH + 10;
                    g.DrawString(stationModel.GroupName, stationFont, brush, point);
                }
            */
                System.IO.MemoryStream ms = new System.IO.MemoryStream();
                image.Save(ms, System.Drawing.Imaging.ImageFormat.Gif);
                System.Drawing.Image imgRoute = System.Drawing.Image.FromStream(ms);
                pictureBox1.Image= imgRoute;
                
               // pbRoute.Width = image.Width;
                //pbRoute.BackgroundImage = imgRoute;
                //myImg = imgRoute;
           
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
            finally
            {
                g.Dispose();
                image.Dispose();
            }
        }
        private void gbtnOraPrcDataset_Click(object sender, EventArgs e)
        {
            //PrintLable();
            this.DrawBarCode();
            return;

                        // create regular expression
         Regex expression = 
            new Regex( @"\d[0-35-9]" );
   
         string string1 = "Jane's Birthday is 05-12-75\n" +
            "Dave's Birthday is 11-04-68\n" +
            "John's Birthday is 04-28-73\n" +
            "Joe's Birthday is 12-17-77";
            string output="";
            foreach ( Match myMatch in expression.Matches( string1 ) )
                output += myMatch.ToString() + "\n";

            object a=1;
            object b = 1;
            bool bValue = a == b;
            bool bValue1 = a.Equals(b);
            return;
            DataSet ds = new DataSet();
            ds = SFCStartup.dba.GetDataByPanel("HPNS6K1843");
            if (ds.Tables.Count > 0)
            {
                DataTable dt = ds.Tables[0];
                foreach (DataColumn dc in dt.Columns)
                {
                    MessageBox.Show(dc.ColumnName);
                }
                foreach (DataRow dr in dt.Rows)
                {
                    MessageBox.Show(dr[0].ToString()+"--"+dr[1].ToString());
                }
            }
        }

        private bool bIsClick = false;
        private void button4_Click(object sender, EventArgs e)
        {
            this.bIsClick = !bIsClick;
            this.button4.Invalidate();
        }

        private void button4_Paint(object sender, PaintEventArgs e)
        {

            Bitmap bmpBob = (Bitmap)this.button4.Image;
            SFC_Tools.Classes.BitmapRegion.CreateControlRegion(button4, bmpBob);
            /*
            this.button3.Cursor = Cursors.Hand;
            Bitmap bmpBob = (Bitmap)this.button4.Image;
            GraphicsPath graphicsPath = CalculateControlGraphicsPath(bmpBob);
            //SFC_Tools.Classes.BitmapRegion.CreateControlRegion(button4, bmpBob);
            this.button4.Region = new Region(graphicsPath);
            */
            ///////////////////////////////////////////////////////////////////////////////////////////////
            /*
            Graphics g = e.Graphics;
            Rectangle r= e.ClipRectangle;
            r.Width = r.Width + 55;
            r.Height = r.Height + 55;
            //g.FillRectangle(new SolidBrush(Color.BlueViolet), r);//白色背景
            
            GraphicsPath gp = new GraphicsPath();
            gp.AddRectangle(r);
            if(this.bIsClick)
                g.DrawPath(new Pen(Color.BlueViolet,4),gp);
            else
                g.DrawPath(new Pen(Color.IndianRed, 3), gp);*/
           
        }

        int i = 0;
        private void ucBorderButton1_Click(object sender, EventArgs e)
        {           
            i=i+1;
            if(i>2)
                i=0;
            ucBorderButton1.setButtonStatus(i);
        }

        private static GraphicsPath CalculateControlGraphicsPath(Bitmap bitmap)
        {

            GraphicsPath graphicsPath = new GraphicsPath();

            Color colorTransparent = bitmap.GetPixel(0, 0);

            int colOpaquePixel = 0;

            for (int row = 0; row < bitmap.Height-1; row++)
            {

                colOpaquePixel = 0;

                for (int col = 0; col < bitmap.Width-1; col++)
                {

                    if (bitmap.GetPixel(col, row) != colorTransparent)
                    {

                        colOpaquePixel = col;

                        int colNext = col;

                        for (colNext = colOpaquePixel; colNext < bitmap.Width; colNext++)
                            if (bitmap.GetPixel(colNext, row) == colorTransparent)
                                break;

                        graphicsPath.AddRectangle(new Rectangle(colOpaquePixel,
                         row, colNext - colOpaquePixel, 1));

                        col = colNext;
                    }
                }
            }

            return graphicsPath;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

      
    }



}

