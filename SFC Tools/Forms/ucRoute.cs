using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
//using SFC_Tools.ServicRoute;//Web service
using System.Drawing.Printing;
using SFC_Tools.Model;
using System.IO;


namespace SFC_Tools
{ 
    public partial class ucRoute : UserControl
    {
        public frmLoadRoute fmNewRoute;
        //void MyRouteChanged(object sender, EventArgs e)
        //{
        //    InitData(SFCStartup.dba.GetInitedRouteCode());
        //}
        void MyRouteChanged(object sender, RouteCodeOfEventArgs e)
        {
            InitData(e.iRouteCode);
        }
        public ucRoute(frmLoadRoute ldRoute)
        {
            this.fmNewRoute = ldRoute;
            ldRoute.RouteChange += new frmLoadRoute.ChangeRouteEventHandler(MyRouteChanged);
            InitializeComponent();
            this.ppdTest = new PrintPreviewDialog();
        }
        public ucRoute()
        {
            InitializeComponent();
            this.ppdTest = new PrintPreviewDialog();
        }
        
        private int iTestRouteCode = 117;
        private string routerName;

        private StationGroupModel[] stMainLien;
        private StationGroupModel[] stSpecialRoute;
        private StationGroupModel[] stRepairlRoute;
        private StationGroupModel[] stReturnRoute;

        private readonly int IMAGE_HEIGHT = 300;
        private readonly int ROUTE_MAIN_HEIGTH = 150;
        private readonly int REPAIR_HEIGTH = 230;
        private readonly int STATION_WIDTH = 15;
        private readonly int STATION_SPACE = 60;

        Image myImg;
        //Print test
        int totalPage = 6;
        int page;
        int maxPage;
        //public delegate void RouteChangeHandlerDelegate(int iRouteCode);
       // public event RouteChangeHandlerDelegate RouteChangeHandler;

        private void ucRoute_Load(object sender, EventArgs e)
        {
            InitData(iTestRouteCode);
        }
        public void InitData(int iRtCode)
        {
            try
            {
                iTestRouteCode = iRtCode;
                routerName = SFCStartup.dba.GetRouteName(iTestRouteCode);
                if (routerName == "N/A" || routerName == null)
                {
                    return;
                }
                this.GetGroupDataInit();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
                return;
            }
            myDrawTest();
        }
        public void myDrawTest()
        {
            //int imageWidth = routerMainInfo.Length * (STATION_WIDTH + STATION_SPACE) + STATION_SPACE / 2;
            //int imageWidth = mytestRoute.Length * (STATION_WIDTH + STATION_SPACE) + STATION_SPACE / 2;
            int imageWidth = stMainLien.Length * (STATION_WIDTH + STATION_SPACE) + STATION_SPACE / 2;
            Bitmap image = new Bitmap(imageWidth, IMAGE_HEIGHT);
            Graphics g = Graphics.FromImage(image);
            try
            {
                g.Clear(Color.White);

                Pen imageBorderPan = new Pen(Color.Silver, 2);

                //Draw the border of the image
                g.DrawRectangle(imageBorderPan, 1, 1, imageWidth - 1, IMAGE_HEIGHT - 1);

                int freeSpace = STATION_SPACE / 2 + STATION_WIDTH;

                string routeName = "Route Name:" + routerName;
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
                
                System.IO.MemoryStream ms = new System.IO.MemoryStream();
                image.Save(ms, System.Drawing.Imaging.ImageFormat.Gif);
                Image imgRoute= Image.FromStream(ms);
                pbRoute.Width = image.Width;
                pbRoute.BackgroundImage = imgRoute;
                myImg = imgRoute;
                
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
        private int GetStationPositionX(int index)
        {
            int x = STATION_SPACE / 2 + STATION_WIDTH + (index - 1) * (STATION_SPACE + STATION_WIDTH);
            return x;
        }
        protected void myPrintDoc_PrintPage(object Sender,PrintPageEventArgs e)
        {
            string drawString = "sample text";
            Font DrawFont = new Font("Arial",16);
            SolidBrush drawBrush = new SolidBrush(Color.Blue);
            float x = 150.0F;
            float y = 50.0F;
            StringFormat drawFormat = new StringFormat();
            drawFormat.FormatFlags = StringFormatFlags.NoWrap;
            e.Graphics.DrawString(drawString, DrawFont, drawBrush, x, y, drawFormat);
            e.HasMorePages = false;
        }

        private void pbRoute_Click(object sender, EventArgs e)
        {
            pbRoute.SizeMode = PictureBoxSizeMode.Zoom;
            this.pbRoute.Size = new Size(this.pbRoute.Size.Width + 50, this.pbRoute.Size.Height);
        }
        //used to do prit view
        private void pdTest_PrintPage(object sender, PrintPageEventArgs e)
        {
            Graphics g = e.Graphics;
            using (Font myFont = new Font("Lucda Console", 72))
            {
                //Image imgTemp = Image.FromFile(@"C:\111.jpg");
                //int iw=e.PageSettings.PaperSize.Width;
                g.DrawImage(myImg, 0, 0, e.PageSettings.PaperSize.Width/*myImg.Width - 200*/, myImg.Height);
                ++page;
                e.HasMorePages = (page <= maxPage); 
            }

        }

        private void GetGroupDataInit()
        {
            List<RouteTableModel> rtmRouteTable = new List<RouteTableModel>();
            List<StationGroupModel> sgmMainStation = new List<StationGroupModel>();

            sgmMainStation.Clear();
            rtmRouteTable = SFCStartup.dba.GetRouteMainLine(iTestRouteCode);
            string strRouteGroupBegin = "0";
            string strRouteGroupNext;
            strRouteGroupNext = strRouteGroupBegin;
            //Get Main Line
            for (int i = 0; i < rtmRouteTable.Count; i++)
            {
                strRouteGroupNext = GetMainStationName(rtmRouteTable, strRouteGroupNext, 0);
                if (strRouteGroupNext != "N/A")
                {
                    StationGroupModel sgmTemp = new StationGroupModel();
                    sgmTemp.GroupName = strRouteGroupNext;
                    sgmTemp.iIndex = sgmMainStation.Count + 1;
                    sgmTemp.iNextIndex = sgmMainStation.Count + 2;
                    sgmMainStation.Add(sgmTemp);
                }
                else
                    break;
            }
            stMainLien = new StationGroupModel[sgmMainStation.Count];
            for (int i = 0; i < sgmMainStation.Count; i++)
            {
                stMainLien[i] = sgmMainStation[i];
            }
            //Get Special Route
            string strSpecialGroup;
            List<StationGroupModel> sgmSpecialStation = new List<StationGroupModel>();
            for (int i = 0; i < sgmMainStation.Count; i++)
            {
                strSpecialGroup = GetSpecialStationName(rtmRouteTable, sgmMainStation[i].GroupName, 0);
                if (strSpecialGroup == "N/A")
                {
                    continue;
                }
                else
                {
                    StationGroupModel sgmSpecialGroup = new StationGroupModel();
                    sgmSpecialGroup.GroupName = sgmMainStation[i].GroupName;
                    sgmSpecialGroup.iIndex = i + 1;
                    for (int j = i; j < sgmMainStation.Count; j++)
                    {
                        if (sgmMainStation[j].GroupName == strSpecialGroup)
                        {
                            sgmSpecialGroup.iNextIndex = j + 1;
                            break;
                        }
                    }
                    sgmSpecialStation.Add(sgmSpecialGroup);
                }
            }
            this.stSpecialRoute = new StationGroupModel[sgmSpecialStation.Count];
            for (int i = 0; i < sgmSpecialStation.Count; i++)
            {
                stSpecialRoute[i] = sgmSpecialStation[i];
            }

            //Get Repair Station
            string strRepairNext;
            List<StationGroupModel> sgmRepairStation = new List<StationGroupModel>();
            for (int i = 0; i < sgmMainStation.Count; i++)
            {
                strRepairNext = GetRepairStationName(rtmRouteTable, sgmMainStation[i].GroupName, 1);
                if (strRepairNext == "N/A")
                {
                    continue;
                }
                else
                {
                    bool bIsSameStation = false;
                    for (int j = 0; j < sgmRepairStation.Count; j++)
                    {
                        if (strRepairNext == sgmRepairStation[j].GroupName)
                        {
                            StationGroupModel sgmRepairGroup = new StationGroupModel();
                            sgmRepairGroup.GroupName = sgmRepairStation[j].GroupName;
                            sgmRepairGroup.iIndex = sgmRepairStation[j].iNextIndex;// = sgmRepairStation[j].GroupName;
                            sgmRepairGroup.iNextIndex = i+1;
                            sgmRepairStation.Add(sgmRepairGroup);
                            bIsSameStation = true;
                            break;
                        }
                    }
                    if (!bIsSameStation)
                    {
                        StationGroupModel sgmRepairGroup = new StationGroupModel();
                        sgmRepairGroup.iIndex = i + 1;
                        sgmRepairGroup.GroupName = strRepairNext;
                        sgmRepairGroup.iNextIndex = i + 1;
                        sgmRepairStation.Add(sgmRepairGroup);
                    }
                }
            }
            stRepairlRoute = new StationGroupModel[sgmRepairStation.Count];
            for (int i = 0; i < sgmRepairStation.Count; i++)
            {
                stRepairlRoute[i] = sgmRepairStation[i]; 
            }
            //Get Return Station Info
            stReturnRoute = new StationGroupModel[sgmRepairStation.Count];
            for (int i = 0; i < sgmRepairStation.Count; i++)
            {
                string strReturnStation=GetReturnSationName(rtmRouteTable,sgmRepairStation[i].GroupName, 0);
                if (strReturnStation != "N/A")
                {
                    for (int j = 0; j < sgmMainStation.Count; j++)
                    {
                        if (strReturnStation == sgmMainStation[j].GroupName)
                        {
                            StationGroupModel sgmReturnGroup = new StationGroupModel();
                            sgmReturnGroup.GroupName = sgmRepairStation[i].GroupName;
                            sgmReturnGroup.iIndex = sgmRepairStation[i].iIndex;
                            sgmReturnGroup.iNextIndex = j+1;
                            stReturnRoute[i] = sgmReturnGroup;
                        }
                    }
                }
            }
           // stReturnRoute = stRepairlRoute; 
        }//get group data init finish

        //Get Next Station
        private string GetNextStationName(List<RouteTableModel> tmpRouteTable, string strGroupName, int iFlag)
        {
            string strRet = "N/A";
            for (int i = 0; i < tmpRouteTable.Count; i++)
            {
                if (strGroupName == tmpRouteTable[i].GroupName && iFlag == tmpRouteTable[i].StateFlag)
                {
                    strRet = tmpRouteTable[i].GroupNext;
                    break;
                }
            }
            return strRet;
        }
        private string GetNextGroupName(List<RouteTableModel> tmpRouteTable, ArrayList arriPos)
        {
            string strRet = "N/A";
            ArrayList arrFirst = new ArrayList();
            ArrayList arrBehind = new ArrayList();
            int iFirst = int.Parse(arriPos[0].ToString());
            int iSencond = int.Parse(arriPos[1].ToString());
            arrFirst.Add(tmpRouteTable[iFirst].GroupNext);
            arrBehind.Add(tmpRouteTable[iSencond].GroupNext);
            int iPos = 0;
            bool bIsContinue = true;
            while (bIsContinue)
            {
                int iCNT = arrFirst.Count;
                string strFirtNext = GetNextStationName(tmpRouteTable, arrFirst[iCNT - 1].ToString(), 0);
                arrFirst.Add(strFirtNext);
                string strSecondNext = GetNextStationName(tmpRouteTable, arrBehind[iCNT - 1].ToString(), 0);
                arrBehind.Add(strSecondNext);
                for (int i = 0; i < arrFirst.Count; i++)
                {
                    int iFindPos = arrBehind.IndexOf(arrFirst[i]);
                    if (iFindPos >= 0)
                    {
                        if (iFindPos == (arrFirst.Count - 1))
                        {
                            strRet = arrBehind[0].ToString();
                            bIsContinue = false;
                            break;
                        }
                        else
                        {
                            strRet = arrFirst[0].ToString();
                            bIsContinue = false;
                            break;
                        }
                    }
                }
                iPos = iPos + 1;
            }

            return strRet;
        }
        //GetMainStationName
        private string GetMainStationName( List<RouteTableModel> tmpRouteTable,string strGroupName,int iFlag)
        {
            string strRet = null;
            ArrayList arriPos = new ArrayList();
            arriPos.Clear();
            for (int i = 0; i < tmpRouteTable.Count; i++)
            {
                if (strGroupName == tmpRouteTable[i].GroupName && iFlag==tmpRouteTable[i].StateFlag)
                {
                    arriPos.Add(i.ToString());
                }
            }
            //根据 step No 判断选择
            if (arriPos.Count == 2)
            {
                strRet=GetNextGroupName(tmpRouteTable, arriPos);
            }
           /* if (arriPos.Count == 2)
            {
                ArrayList arrFirst = new ArrayList();
                ArrayList arrBehind = new ArrayList();
                int iFirst=int.Parse(arriPos[0].ToString());
                int iSencond=int.Parse(arriPos[1].ToString());
                arrFirst.Add(tmpRouteTable[iFirst].GroupNext);
                arrBehind.Add(tmpRouteTable[iSencond].GroupNext);
                int iPos = 0;
                bool bIsContinue = true;
                while (bIsContinue)
                {
                    int iCNT = arrFirst.Count;
                    string strFirtNext = GetNextStationName(tmpRouteTable, arrFirst[iCNT-1].ToString(), 0);
                    arrFirst.Add(strFirtNext);
                    string strSecondNext = GetNextStationName(tmpRouteTable, arrBehind[iCNT-1].ToString(), 0);
                    arrBehind.Add(strSecondNext);
                    for (int i = 0; i <arrFirst.Count; i++)
                    {
                        int iFindPos = arrBehind.IndexOf(arrFirst[i]);
                        if (iFindPos >= 0)
                        {
                            if ( iFindPos == (arrFirst.Count - 1))
                            {
                                strRet = arrBehind[0].ToString();
                                bIsContinue = false;
                                break;
                            }
                            else
                            {
                                strRet = arrFirst[0].ToString();
                                bIsContinue = false;
                                break;
                            }
                        }
                    }
                    iPos = iPos + 1;
                }
            }*/
           if (arriPos.Count >2)
            {     
                for (int i = 1; i < arriPos.Count; i++)
                {
                    int j = int.Parse(arriPos[i].ToString());
                    if (tmpRouteTable[j - 1].StepSeqNo > tmpRouteTable[j].StepSeqNo)
                    {
                        strRet = tmpRouteTable[j].GroupNext;
                    }
                    else
                    {
                        strRet = tmpRouteTable[j - 1].GroupNext;
                    }
                }
            }
            if (arriPos.Count == 1)
            {
                strRet = tmpRouteTable[int.Parse(arriPos[0].ToString())].GroupNext;
            }
            if ( arriPos.Count < 1 )
            {
                strRet = "N/A";
            }
            return strRet;
        }
        private string GetSpecialStationName(List<RouteTableModel> tmpRouteTable, string strGroupName, int iFlag)
        {
            string strRev = null;
            ArrayList arriPos = new ArrayList();
            arriPos.Clear();
            for (int i = 0; i < tmpRouteTable.Count; i++)
            {
                if (strGroupName == tmpRouteTable[i].GroupName && iFlag == tmpRouteTable[i].StateFlag)
                {
                    arriPos.Add(i.ToString());
                }
            }
            if (arriPos.Count < 2)
            {
                strRev = "N/A";
            }
            /*if (arriPos.Count== 2)
            {
                int j = int.Parse(arriPos[0].ToString());
                int k = int.Parse(arriPos[1].ToString());
                if (tmpRouteTable[j].StepSeqNo > tmpRouteTable[k].StepSeqNo)
                {
                    strRev = tmpRouteTable[j].GroupNext;
                }
                else
                {
                    strRev = tmpRouteTable[k].GroupNext;
                }
            }*/
            if (arriPos.Count == 2)
            {
                ArrayList arrFirst = new ArrayList();
                ArrayList arrBehind = new ArrayList();
                int iFirst = int.Parse(arriPos[0].ToString());
                int iSencond = int.Parse(arriPos[1].ToString());
                arrFirst.Add(tmpRouteTable[iFirst].GroupNext);
                arrBehind.Add(tmpRouteTable[iSencond].GroupNext);
                int iPos = 0;
                bool bIsContinue = true;
                while (bIsContinue)
                {
                    int iCNT = arrFirst.Count;
                    string strFirtNext = GetNextStationName(tmpRouteTable, arrFirst[iCNT - 1].ToString(), 0);
                    arrFirst.Add(strFirtNext);
                    string strSecondNext = GetNextStationName(tmpRouteTable, arrBehind[iCNT - 1].ToString(), 0);
                    arrBehind.Add(strSecondNext);
                    for (int i = 0; i < arrFirst.Count; i++)
                    {
                        int iFindPos = arrBehind.IndexOf(arrFirst[i]);
                        if (iFindPos >= 0)
                        {
                            if (iFindPos == (arrFirst.Count - 1))
                            {
                                strRev = arrFirst[0].ToString();
                                bIsContinue = false;
                                break;
                            }
                            else
                            {
                                strRev = arrBehind[0].ToString();
                                bIsContinue = false;
                                break;
                            }
                        }
                    }
                    iPos = iPos + 1;
                }
            }
            //when the special staion more than 2(nv site),need to deal...

            //repair station

            return strRev;
        }
        private string GetRepairStationName(List<RouteTableModel> tmpRouteTable, string strGroupName, int iFlag)
        {
            string strRev = "N/A";
            ArrayList arriPos = new ArrayList();
            arriPos.Clear();
            for (int i = 0; i < tmpRouteTable.Count; i++)
            {
                if (strGroupName == tmpRouteTable[i].GroupName && iFlag == tmpRouteTable[i].StateFlag)
                {
                    strRev = tmpRouteTable[i].GroupNext;
                    break;
                }
            }
            return strRev;
        }
        private string GetReturnSationName(List<RouteTableModel> tmpRouteTable, string strGroupName, int iFlag)
        {
            string strRev = "N/A";
            ArrayList arriPos = new ArrayList();
            arriPos.Clear();
            for (int i = 0; i < tmpRouteTable.Count; i++)
            {
                if (strGroupName == tmpRouteTable[i].GroupName && iFlag == tmpRouteTable[i].StateFlag)
                {
                    strRev = tmpRouteTable[i].GroupNext;
                    break;
                }
            }
            return strRev; 
        }

        private void glsBtnPre_Click(object sender, EventArgs e)
        {
            if (this.myImg == null)
                return;
            frmViewPic myPicView = new frmViewPic();
            myPicView.initFormPic(myImg);
            myPicView.ShowDialog();
        }

        private void glsPrintPre_Click(object sender, EventArgs e)
        {
            string strPaperName="A3";
            foreach(PaperSize ps in pdTest.PrinterSettings.PaperSizes)
            {
                strPaperName = ps.PaperName;
            }

            if (this.myImg == null)
                return;
            page = 1;
            maxPage = totalPage;
            pdTest.DefaultPageSettings.PaperSize = new PaperSize(/*"A3"*/strPaperName, 900, 300);
            ppdTest.Document = pdTest;
            ppdTest.ShowDialog();
        }

        private void glsRouteChange_Click(object sender, EventArgs e)
        {
            //frmLoadRoute frmNewRoute= new frmLoadRoute(this);

            fmNewRoute.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ArrayList xx=SFCStartup.dba.Test();
            MessageBox.Show(xx.Count.ToString());

        }

    }
}
