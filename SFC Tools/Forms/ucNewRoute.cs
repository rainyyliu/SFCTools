using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.Data.Entity;
using System.Data.Linq;

using SFC_Tools.Model;

namespace SFC_Tools.Forms
{
    public partial class ucNewRoute : UserControl
    {
        private TreeNode trHead =new TreeNode("0");
        private TreeNode trNodeLast=null;
        private int iIndex = 0;

        private readonly int IMAGE_HEIGHT = 300;
        private readonly int ROUTE_MAIN_HEIGTH = 150;
        private readonly int REPAIR_HEIGTH = 230;
        private readonly int STATION_WIDTH = 15;
        private readonly int RAEPAIR_LINE_OFFSET = 13;
        private readonly int STATION_SPACE = 60;
        private readonly int RETURN_LINE_OFFSET = 4;
        private Dictionary<string, Point> m_StationList= new Dictionary<string,Point>();
        private Dictionary<string, Rectangle> m_StationArea = new Dictionary<string, Rectangle>();
        private ArrayList m_stationRepairList = new ArrayList();
        private ArrayList m_stationReturnList = new ArrayList();
        private ArrayList m_stationSpecialList = new ArrayList();
        Image myImg;

        private KeyValuePair<string, Rectangle> CurrentSelRcl;
        private KeyValuePair<string, Rectangle> PreviousSelRcl;

        private KeyValuePair<string, Rectangle> CurrentSelStation;

        private Point m_ptPos;
        private Point m_ptPosOriginal;
        private Point m_ptLastPos;
        private string m_sLastStation;
        private bool bIsGrid;

        private DataTable dtMain=null;
        private DataTable dtAllStations = null;

        private RouteTableModel mdNewRoute = new RouteTableModel();
        private bool bIsJump=false;
        private Point m_ptStart;
        private Point m_ptEnd;

        private bool bIsBeginDraw = false;

        private ArrayList arrCurPosLst = new ArrayList();

        public ucNewRoute()
        {
            InitializeComponent();
#if DEBUG
            bIsGrid = true;
            this.chkGrid.Checked = true;
#else
            bIsGrid=false;
            this.chkGrid.Checked = false;
#endif
        }

        private void ucNewRoute_Load(object sender, EventArgs e)
        {
            ArrayList arrRoute = SFCStartup.dba.GetRouteNameList();
            this.cbbRoute.DataSource = arrRoute;
        }

        private void InitData() 
        {
            this.m_StationList.Clear();
            this.m_stationRepairList.Clear();
            this.m_stationReturnList.Clear();
            this.m_stationSpecialList.Clear();
            this.m_StationArea.Clear();
            this.trNodeLast = null;
            this.treeView1.Nodes.Clear();
            this.trHead.Text = null;
            arrCurPosLst.Clear();
            mdNewRoute = new RouteTableModel();
            CurrentSelRcl = new KeyValuePair<string,Rectangle>();
            PreviousSelRcl = new KeyValuePair<string,Rectangle>();
            iIndex = 0;
            dtMain=new DataTable();
            DataColumn dc;
            dc = new DataColumn();
            dc.ColumnName = "GROUP_NAME";
            dc.Caption = "GROUP_NAME";
            dtMain.Columns.Add(dc);

            dc = new DataColumn();
            dc.ColumnName = "GROUP_NEXT";
            dc.Caption = "GROUP_NEXT";
            dtMain.Columns.Add(dc);

            this.mdNewRoute.RouteCode = SFCStartup.dba.GetRouteCode(this.cbbRoute.Text);
        }
        private void GetRouteInfoByCode(int iRoute,string sType,DataTable dtTarget)
        {

            dtAllStations = SFC_Tools.SFCStartup.dba.GetAllRouteInfo(iRoute);

            if (sType == "MAIN_INFO")
                dtMain = dtTarget;
            else
                dtMain = SFC_Tools.SFCStartup.dba.GetMainRoute(iRoute);

            this.dtAllStations=SFC_Tools.SFCStartup.dba.GetAllRouteInfo(iRoute);

            TreeNode root = new TreeNode("0");
            GetMainRouteInfo(dtMain, root);

            this.treeView1.Nodes.Add(root);
            this.treeView1.ExpandAll();
            if (trNodeLast != null)
                GetStationsByString(this.trNodeLast.FullPath);

            GetSpecialLine(dtMain);  //get special line stations
            DataTable dtReapir;
            if (sType == "REPAIR_INFO")
                dtReapir = dtTarget.Copy();
            else
                dtReapir = SFC_Tools.SFCStartup.dba.GetRepairStationsByRouteCode(iRoute);
            GetRepiarStationInfo(dtReapir);
            DataTable dtReturn = SFC_Tools.SFCStartup.dba.GetReturnStationsByRouteCode(iRoute);
            GetReturnStationInfo(dtReturn);
        }

        private void GetRouteInfoByCodeA(int iRoute, string sType, DataTable dtTarget)
        {

            if (sType == "GET_FROM_DB")
                dtAllStations = SFC_Tools.SFCStartup.dba.GetAllRouteInfo(iRoute);
            else
                dtAllStations = dtTarget;

            dtMain = SFC_Tools.SFCStartup.dba.GetMainRoute(dtAllStations);

            TreeNode root = new TreeNode("0");
            GetMainRouteInfo(dtMain, root);

            this.treeView1.Nodes.Add(root);
            this.treeView1.ExpandAll();
            if (trNodeLast != null)
                GetStationsByString(this.trNodeLast.FullPath);

            GetSpecialLine(dtAllStations);  //get special line stations
            DataTable dtReapir;
            dtReapir = SFC_Tools.SFCStartup.dba.GetRepairStationsByRouteCode(dtAllStations);
            GetRepiarStationInfo(dtReapir);
            DataTable dtReturn = SFC_Tools.SFCStartup.dba.GetReturnStationsByRouteCode(dtAllStations);
            GetReturnStationInfo(dtReturn);
        }

        private void button1_Click(object sender, EventArgs e)
        {
           
            if (this.cbbRoute.Text.Trim().Length == 0)
                return;
            InitData();
            int iRoute = SFCStartup.dba.GetRouteCode(this.cbbRoute.Text);
            GetRouteInfoByCodeA(iRoute, "GET_FROM_DB", null);
            
            DrawMainRoute();
        }
        private void GetSpecialLine(DataTable dt)
        {
            foreach (DataRow dr in dt.Rows)
            {
                string sFrom = dr["GROUP_NAME"].ToString();
                string sTo = dr["GROUP_NEXT"].ToString();
                if (m_StationList.ContainsKey(sFrom) && m_StationList.ContainsKey(sTo))
                {
                    Point ptFrom = m_StationList[sFrom];
                    Point ptTo = m_StationList[sTo];
                    ptFrom.X = ptFrom.X + this.STATION_WIDTH;
                    ptFrom.Y = ROUTE_MAIN_HEIGTH;
                    ptTo.X = ptTo.X + this.STATION_WIDTH;
                    ptTo.Y = ROUTE_MAIN_HEIGTH;
                    if (Math.Abs(ptTo.X - ptFrom.X) > (this.STATION_SPACE + this.STATION_WIDTH))
                    {
                        MdRepairStationsInfo msi = new MdRepairStationsInfo();
                        msi.FROM_STATION_GROUP = sFrom;
                        msi.FROM_STATION_POSITION = ptFrom;
                        msi.TO_STATION_GROUP = sTo;
                        msi.TO_STATION_POSITION = ptTo;
                        this.m_stationSpecialList.Add(msi);
                    }
                }
            }
        }

        private void GetRepiarStationInfo(DataTable dt)
        {
#if DEBUG
            string sTemp=null;
#endif
            foreach (DataRow dr in dt.Rows)
            {
                string sKey=dr["GROUP_NAME"].ToString();   //group name
                string sValue = dr["GROUP_NEXT"].ToString();  //group next 
                if (this.m_StationList.ContainsKey(sKey))
                {
                    Point pointA= m_StationList[sKey];
                    pointA.Y = ROUTE_MAIN_HEIGTH;
                    Point pointB = pointA;
                    pointB.Y = this.REPAIR_HEIGTH;
                    MdRepairStationsInfo rpStation= new MdRepairStationsInfo();
                    int iPos = bIsExistsInRepairList(sValue, this.m_stationRepairList);
                    if (iPos > 0)
                    {
                        pointB = ((MdRepairStationsInfo)m_stationRepairList[iPos]).TO_STATION_POSITION;
                        rpStation.ISEXISTS = true;
                    }
                    else
                    {
                        rpStation.ISEXISTS = false;
                    }

                    pointA.X = pointA.X + RAEPAIR_LINE_OFFSET;
                    if (!rpStation.ISEXISTS)
                        pointB.X = pointB.X + RAEPAIR_LINE_OFFSET;
                    rpStation.FROM_STATION_GROUP=sKey;
                    rpStation.TO_STATION_GROUP=sValue;
                    rpStation.FROM_STATION_POSITION=pointA;
                    rpStation.TO_STATION_POSITION=pointB;
                    m_stationRepairList.Add(rpStation);
                    if (!rpStation.ISEXISTS)
                    {   Rectangle rcl=new Rectangle();
                        rcl.X=rpStation.TO_STATION_POSITION.X-18;
                        rcl.Y=rpStation.TO_STATION_POSITION.Y-18;
                        rcl.Width=38;
                        rcl.Height=38;
                        this.m_StationArea.Add(rpStation.TO_STATION_GROUP,rcl);
                    }
#if DEBUG
                    sTemp = sTemp + rpStation.FROM_STATION_GROUP + ":[" + pointA.X + "," + pointA.Y + "]" + rpStation.TO_STATION_GROUP + ":[" + pointB.X + "," + pointB.Y + "]" + "\r\n";
#endif
                }
            }
#if DEBUG    
            //MessageBox.Show(sTemp);
#endif
        }
        private void GetReturnStationInfo(DataTable dt)
        {   
            this.m_stationReturnList.Clear();
            foreach (DataRow dr in dt.Rows)
            {
                MdRepairStationsInfo msi = new MdRepairStationsInfo();
                msi.FROM_STATION_GROUP = dr["GROUP_NAME"].ToString();
                msi.TO_STATION_GROUP = dr["GROUP_NEXT"].ToString();
                int iPos = bIsExistsInRepairList(msi.FROM_STATION_GROUP, m_stationRepairList);
                if (iPos < 0)
                    continue;
                Point pointA=((MdRepairStationsInfo)m_stationRepairList[iPos]).TO_STATION_POSITION;
                pointA.X = pointA.X + RETURN_LINE_OFFSET;

                msi.FROM_STATION_POSITION = pointA;
                if (this.m_StationList.ContainsKey(msi.TO_STATION_GROUP))
                {
                    Point point= m_StationList[msi.TO_STATION_GROUP];
                    point.Y=ROUTE_MAIN_HEIGTH;
                    point.X=point.X+STATION_WIDTH;
                    msi.TO_STATION_POSITION = point;
                }
                
                this.m_stationReturnList.Add(msi);
            }
            
        }
        private int bIsExistsInRepairList(string sStation, ArrayList arr)
        {
            int iPos = -1;
            int j=0;
            foreach (MdRepairStationsInfo msi in arr)
            {
                if (sStation == msi.TO_STATION_GROUP)  
                {
                    iPos = j;
                    break;
                }
                j = j + 1;
            }
            return iPos;
        }

        private void GetMainRouteInfo(DataTable dt, TreeNode td)
        {
            if (dt.Rows.Count <= 0)
                return;
            DataRow[] drs = dt.Select("GROUP_NAME='" +td.Text +"' ");
            foreach (DataRow dr in drs)
            {
                TreeNode tnNew = new TreeNode(dr["GROUP_NEXT"].ToString());
                td.Nodes.Add(tnNew);
                if (tnNew.Level > iIndex)
                {
                    trNodeLast = tnNew;
                    iIndex = tnNew.Level;
                }
                GetMainRouteInfo(dt, tnNew);
            }
        }

        private void DrawMainRoute()
        {
            int imageWidth=1;
            bool bIsStationExists = false;
            if (m_StationList.Count > 0)
                bIsStationExists = true;

            if (bIsStationExists)
                imageWidth = (this.m_StationList.Count + 1) * (STATION_WIDTH + STATION_SPACE) + STATION_SPACE / 2;
            else
                imageWidth = 700;

            if (imageWidth < 700)
                imageWidth = 700;

            Bitmap image = new Bitmap(imageWidth, IMAGE_HEIGHT);
            this.panel1.HorizontalScroll.Maximum = imageWidth;
            Graphics g = Graphics.FromImage(image);
            try
            {
                g.Clear(Color.White);
                Pen imageBorderPan = new Pen(Color.Silver, 2);

                //Draw the border of the image
                g.DrawRectangle(imageBorderPan, 1, 1, imageWidth - 1, IMAGE_HEIGHT - 1);

                int freeSpace = STATION_SPACE / 2 + STATION_WIDTH;
                string routeName = "Route Name:" + this.cbbRoute.Text.Trim();
                Font titleFont = new Font("Arial", 20, FontStyle.Bold);
                SolidBrush titleBrush = new SolidBrush(Color.FromArgb(49, 18, 176));
                g.DrawString(routeName, titleFont, titleBrush, freeSpace, 20);

                //Draw the main line
                if (bIsStationExists)
                    g.DrawLine(new Pen(Color.DarkSlateBlue, 5), freeSpace, ROUTE_MAIN_HEIGTH, (STATION_WIDTH + STATION_SPACE) * (this.m_StationList.Count - 1) - STATION_WIDTH * 2, ROUTE_MAIN_HEIGTH);

                string imagePath = AppDomain.CurrentDomain.SetupInformation.ApplicationBase + @"images\PC2.ICO";// @"images\Station.jpg";
                string imageRepairPath = AppDomain.CurrentDomain.SetupInformation.ApplicationBase + @"images\Oth014.ICO";// @"images\Station.jpg";
                System.Drawing.Image newImage = System.Drawing.Image.FromFile(imagePath);
                Image imgRepair = Image.FromFile(imageRepairPath);
                Font stationFont = new Font("Arial", 8);
                SolidBrush brush = new SolidBrush(Color.Black);
                #region Reapir Info
                //Draw Return Line
                foreach (MdRepairStationsInfo msi in m_stationReturnList)
                {
                    if ((msi.FROM_STATION_POSITION.X == 0 && msi.FROM_STATION_POSITION.Y == 0) || (msi.TO_STATION_POSITION.X == 0 && msi.TO_STATION_POSITION.Y == 0))
                        continue;
                    g.DrawLine(new Pen(Color.Lime, 3), msi.FROM_STATION_POSITION.X, msi.FROM_STATION_POSITION.Y, msi.TO_STATION_POSITION.X+1, msi.TO_STATION_POSITION.Y);
                }
               
                //Draw Reapir Line
                foreach (MdRepairStationsInfo msi in m_stationRepairList)
                {
                    g.DrawLine(new Pen(Color.Red, 3), msi.FROM_STATION_POSITION.X, msi.FROM_STATION_POSITION.Y, msi.TO_STATION_POSITION.X, msi.TO_STATION_POSITION.Y);
                    //g.DrawLine(new Pen(Color.Green, 3), msi.FROM_STATION_POSITION.X + 4, msi.FROM_STATION_POSITION.Y, msi.TO_STATION_POSITION.X + 4, msi.TO_STATION_POSITION.Y);
                    //g.DrawImage(imgRepair, new Point(msi.TO_STATION_POSITION.X - STATION_WIDTH, msi.TO_STATION_POSITION.Y - STATION_WIDTH));
                    //g.DrawRectangle(new Pen(Color.DarkGoldenrod, 4), msi.TO_STATION_POSITION.X - STATION_WIDTH - 3, msi.TO_STATION_POSITION.Y - STATION_WIDTH - 2, 38, 38);
                    Point point = new Point();
                    point.X = msi.TO_STATION_POSITION.X - Convert.ToInt32((msi.TO_STATION_GROUP.Length / 2) * stationFont.Size);
                    point.Y = REPAIR_HEIGTH + STATION_WIDTH + 10;
                    g.FillRectangle(new SolidBrush(Color.White), point.X, point.Y, stationFont.Size * msi.TO_STATION_GROUP.Length, stationFont.Size * 2);
                    //画出工站名称
                    g.DrawString(msi.TO_STATION_GROUP, stationFont, brush, point);
                }
                #endregion

                #region Draw The Click Rectangle
                //Draw the Click Rectangle
                foreach (KeyValuePair<string, Rectangle> kvp in this.m_StationArea)
                {
                    if (kvp.Value.Location == CurrentSelRcl.Value.Location)
                    {
                        g.DrawRectangle(new Pen(Color.Red, 4), kvp.Value.X, kvp.Value.Y, kvp.Value.Width, kvp.Value.Height);
                        g.FillRectangle(new SolidBrush(Color.White), kvp.Value.X + 2, kvp.Value.Y + 2, kvp.Value.Width - 4, kvp.Value.Height - 4);
                        continue;
                    }
                    else if (kvp.Value.Location == PreviousSelRcl.Value.Location)
                    {
                        g.DrawRectangle(new Pen(Color.DarkGreen, 4), kvp.Value.X, kvp.Value.Y, kvp.Value.Width, kvp.Value.Height);
                        g.FillRectangle(new SolidBrush(Color.White), kvp.Value.X + 2, kvp.Value.Y + 2, kvp.Value.Width - 4, kvp.Value.Height - 4);
                        continue;
                    }
                }
                #endregion

                //Draw Reapir Line
                foreach (MdRepairStationsInfo msi in m_stationRepairList)
                {
                    g.DrawImage(imgRepair, new Point(msi.TO_STATION_POSITION.X - STATION_WIDTH, msi.TO_STATION_POSITION.Y - STATION_WIDTH));
                }

                #region Draw Special Line
                foreach (MdRepairStationsInfo msi in this.m_stationSpecialList)
                {
                    int x1 = msi.FROM_STATION_POSITION.X;
                    int y1 = msi.FROM_STATION_POSITION.Y;

                    int x2 = msi.TO_STATION_POSITION.X;
                    int y2 = msi.TO_STATION_POSITION.Y;
                    int width = Math.Abs(x2 - x1);
                    int height = width / 2;
                    g.DrawArc(new Pen(Color.DarkBlue, 5), x1, (y1 - height / 2 - STATION_WIDTH), width, height, 180, 180);
                }
                #endregion

                
                string sTemp = "";
                int i = 0;
                foreach (KeyValuePair<string, Point> kvp in m_StationList)
                {

                    int x = GetStationPositionX(i);
                    Point point = new Point();
                    //画出工站图标
                    g.DrawImage(newImage, kvp.Value);
                    //g.DrawRectangle(new Pen(Color.DarkGoldenrod, 4), kvp.Value.X - 3, kvp.Value.Y - 2, 38, 38);
                    sTemp = sTemp + kvp.Value.X + "--" + kvp.Value.Y;
                    point = kvp.Value;
                    point.X = x - Convert.ToInt32((kvp.Key.Length / 2) * stationFont.Size);
                    point.Y = ROUTE_MAIN_HEIGTH + STATION_WIDTH + 10;

                    sTemp = sTemp + "||" + point.X + "--" + point.Y + "\r\n";

                    //画出工站名称矩形
                    g.FillRectangle(new SolidBrush(Color.White), point.X, point.Y, stationFont.Size * kvp.Key.Length, stationFont.Size * 2);
                    //画出工站名称
                    g.DrawString(kvp.Key, stationFont, brush, point);
                    i = i + 1;
                }
                

                if (bIsGrid)
                {
                    for (i = freeSpace; i < image.Width; i += 75)
                    {
                        for (int j = 0; j < image.Height; j += 30)
                            g.DrawLine(new Pen(Color.Green), new Point(0, j), new Point(image.Width, j));
                        g.DrawLine(new Pen(Color.Green), new Point(i, 0), new Point(i, image.Height));
                    }
                }

                //
                if (this.bIsBeginDraw)
                {
                    
                    for (int x = 1; x < arrCurPosLst.Count; x++)
                    {
                        if (rdoCancelJump.Checked)
                            g.DrawLine(new Pen(Color.Gray, 5), (Point)arrCurPosLst[x - 1], (Point)arrCurPosLst[x]);
                        else
                            g.DrawLine(new Pen(Color.Blue, 3), (Point)arrCurPosLst[x - 1], (Point)arrCurPosLst[x]);
                    }
                }

                System.IO.MemoryStream ms = new System.IO.MemoryStream();
                image.Save(ms, System.Drawing.Imaging.ImageFormat.Gif);
                Image imgRoute = Image.FromStream(ms);
                if (image.Width < 400)
                    pbMain.Width = 400;
                else
                    this.pbMain.Width = image.Width;
                this.pbMain.BackgroundImage = imgRoute;
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
                this.bIsJump = false;
            }
        }

        private void pnl_Click(object sender, EventArgs e)
        {
            IntPtr mHwd = ((Panel)sender).Handle;
            Graphics g = Graphics.FromHwnd(mHwd);
            g.DrawRectangle(new Pen(Color.Green), new Rectangle(0, 0, 30, 30));
        }

        private void pnl_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                int px = Cursor.Position.X - m_ptPos.X;
                int py = Cursor.Position.Y - m_ptPos.Y;
                ((Panel)sender).Location = new Point(((Panel)sender).Location.X + px, ((Panel)sender).Location.Y + py);
                m_ptPos = Cursor.Position;

                int pxA = Cursor.Position.X - m_ptLastPos.X;
                m_ptLastPos = Cursor.Position;
            }

        }

        private int GetStationPositionX(int index)
        {
            int x = STATION_SPACE / 2 + STATION_WIDTH + (index - 1) * (STATION_SPACE + STATION_WIDTH);
            return x;
        }

        private void GetStationsByString(string sStation)
        {
            this.m_StationList.Clear();
            int i = 0;
            string []s= sStation.Split('\\');
            foreach (string str in s)
            {
                m_sLastStation = str;
                int x = GetStationPositionX(i);
                Point point = new Point();
                point.X = x - STATION_WIDTH;
                point.Y = ROUTE_MAIN_HEIGTH - STATION_WIDTH;
                this.m_StationList.Add(str,point);
                Rectangle rcl=new Rectangle();
                rcl.X=point.X-3;
                rcl.Y=point.Y-2;
                rcl.Width=38;
                rcl.Height=38;
                this.m_StationArea.Add(str,rcl);
                i = i + 1;
            }
        }

        private string CheckPosInStationArea(Point pt)
        {
            string sRes="N/A";
            Rectangle stationRec=new Rectangle();
            foreach (KeyValuePair<string,Rectangle> kvp in m_StationArea)
            {
                Rectangle rcl=kvp.Value;
                stationRec=rcl;
                Point ptleft = new Point();
                ptleft.X = rcl.X;
                ptleft.Y = rcl.Y+30;

                Point ptRight = new Point();
                ptRight.X = ptleft.X + rcl.Width;
                ptRight.Y = ptleft.Y + rcl.Height;

                if ((pt.X >= ptleft.X && pt.X < ptRight.X) && (pt.Y >= ptleft.Y && pt.Y <= ptRight.Y))
                {
                    this.lblPoint.Text = "Position:[X="+pt.X+",Y="+pt.Y+"]Rect[X1="+rcl.X+",Y1="+rcl.Y+",X2="+ptRight.X+",Y2="+ptRight.Y+"]";
                    sRes = kvp.Key;
                    break;
                }
            }
            return sRes;
        }

        private void cbbRoute_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.button1_Click(sender, e);
        }

        private void pnlComm_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                int px = Cursor.Position.X - m_ptPos.X;
                int py = Cursor.Position.Y - m_ptPos.Y;
                ((Panel)sender).Location = new Point(((Panel)sender).Location.X + px, ((Panel)sender).Location.Y + py);
                m_ptPos = Cursor.Position;
                //this.lblPoint.Text=this.lblPoint.Text+"\r\n"+px+"\r\n";

                int pxA = Cursor.Position.X - m_ptLastPos.X;

                int iOffSet;
                if (pxA > 0)
                    iOffSet = 1;
                else
                    iOffSet = -1;

                this.lblPoint.Text = pxA+"-xx";
                int iStep = this.myImg.Width / 300 ;
                
                //if ((panel1.HorizontalScroll.Minimum <= (this.panel1.HorizontalScroll.Value + iStep))&&((this.panel1.HorizontalScroll.Value + iStep) <= panel1.HorizontalScroll.Maximum))
                //    this.panel1.HorizontalScroll.Value = this.panel1.HorizontalScroll.Value + iStep;
                m_ptLastPos = Cursor.Position;
            } 
            
        }

        private void pnlComm_MouseDown(object sender, MouseEventArgs e)
        {
            m_ptPos = Cursor.Position;
            m_ptPosOriginal = ((Panel)sender).Location;
        }

        private void chkGrid_CheckedChanged(object sender, EventArgs e)
        {
            if (this.chkGrid.Checked)
                bIsGrid = true;
            else
                bIsGrid = false;

            this.DrawMainRoute();
        }

        private void pnlComm_MouseUp(object sender, MouseEventArgs e)
        {
            int iIndex = 0;
            if (this.m_StationList != null)
                iIndex = m_StationList.Count;
            if (iIndex==0)
            {
                InitData();
                this.mdNewRoute.RouteCode = SFC_Tools.SFCStartup.dba.GetNewRouteCode();
                this.mdNewRoute.RouteDesc = cbbRoute.Text;
                this.mdNewRoute.StepSeqNo = 0;
                DataRow dr1 = dtAllStations.NewRow();
                dr1["ROUTE_CODE"] = mdNewRoute.RouteCode;
                dr1["GROUP_NAME"] = "0";
                dr1["GROUP_NEXT"] = "TEST" + "_" + iIndex;
                dr1["STATE_FLAG"] = 0;
                dr1["STEP_SEQUENCE"] = 0;
                dr1["ROUTE_DESC"] = mdNewRoute.RouteDesc;
                dtAllStations.Rows.Add(dr1);

                GetRouteInfoByCodeA(mdNewRoute.RouteCode, "", dtAllStations);

                DrawMainRoute();

                ((Panel)sender).Location = m_ptPosOriginal;
                return;
            }
            Point pt = new Point();
            pt.X = iIndex * 75 - (STATION_SPACE / 2 + STATION_WIDTH);
            pt.Y = ROUTE_MAIN_HEIGTH - 15;
            ((Panel)sender).Location = pt;
            /////////////////////////////////////////////
            DataTable dtNew = dtAllStations.Copy();
            string sStation="TEST" + "_" + iIndex;
            DataRow[] dr=dtNew.Select("GROUP_NAME= '"+sStation+"'");
            if (dr.Count() <= 0)
            {
                DataRow drA = dtNew.NewRow();
                drA["ROUTE_CODE"] = dtNew.Rows[0]["ROUTE_CODE"];
                drA["GROUP_NAME"] = m_sLastStation;
                drA["GROUP_NEXT"] = sStation;
                drA["STATE_FLAG"] = 0;
                drA["STEP_SEQUENCE"] = GetNewRouteStepSequence(dtAllStations);
                drA["ROUTE_DESC"] = dtNew.Rows[0]["ROUTE_DESC"];
                dtNew.Rows.Add(drA);
                if (this.cbbRoute.Text.Trim().Length == 0)
                    return;
                InitData();
                int iRoute = SFCStartup.dba.GetRouteCode(this.cbbRoute.Text);
                //GetRouteInfoByCode(iRoute, "MAIN_INFO", dtNew);
                GetRouteInfoByCodeA(mdNewRoute.RouteCode, "", dtNew);

                DrawMainRoute();
            }
            //////////////////////////////////////////////
            ((Panel)sender).Location = m_ptPosOriginal;
        }
        
        private void pbMain_MouseMove(object sender, MouseEventArgs e)
        {         
            string sPos ="Current Position: X ["+e.X.ToString()+"] Y:"+"["+e.Y.ToString()+"]";
            this.lblPoint.Text = sPos;
            string sText=CheckPosInStationArea(e.Location);
            if (sText != "N/A")
            {
                if (this.rdoDelStation.Checked)
                    this.pnlTest.Visible = true;
                else
                    this.pnlTest.Visible = false;
                this.Cursor = Cursors.Hand;
                this.pnlTest.Location = new Point(m_StationArea[sText].Location.X - this.panel1.HorizontalScroll.Value, m_StationArea[sText].Location.Y + 40);
                this.pnlTest.Tag = sText;
                CurrentSelStation = new KeyValuePair<string,Rectangle>(sText, m_StationArea[sText]);
            }
            else
            {
                if(bIsBeginDraw)
                    this.Cursor = Cursors.Cross;
                else
                    this.Cursor = Cursors.Default;
                this.pnlTest.Location=new Point(-100,-100);
                CurrentSelStation = new KeyValuePair<string,Rectangle>();
            }
            if (this.bIsBeginDraw)
            {
                this.m_ptEnd = new Point(e.X, e.Y-22);
                this.pbMain.Refresh();
            }
        }
        
        private void pbMain_Click(object sender, EventArgs e)
        {
            Point pt=new Point(((MouseEventArgs)e).X,((MouseEventArgs)e).Y-30);
            if (Cursor.Current == Cursors.Hand)
            {
                foreach (KeyValuePair<string, Rectangle> kvp in this.m_StationArea)
                {
                    if (pt.X >= kvp.Value.X && pt.Y >= kvp.Value.Y & pt.X <= (kvp.Value.X + kvp.Value.Width) && pt.Y <= (kvp.Value.Y + kvp.Value.Height))
                    {
                        if (CurrentSelRcl.Value == kvp.Value)
                            continue;
                        if (CurrentSelRcl.Value.Width != 0)
                        {
                            PreviousSelRcl = CurrentSelRcl;
                        }

                        CurrentSelRcl = kvp;
                        break;
                    }
                }
            }
            this.DrawMainRoute();
        }
        
        private void pbMain_MouseDown(object sender, MouseEventArgs e)
        {
            if (this.Cursor == Cursors.Hand)
            {
                pnlTest.Location = Cursor.Position;
                m_ptPos = pnlTest.Location;
                m_ptStart = new Point(CurrentSelStation.Value.Location.X+CurrentSelStation.Value.Width/2,CurrentSelStation.Value.Location.Y+CurrentSelStation.Value.Height/2);
                bIsBeginDraw = true;
                string sText = CheckPosInStationArea(e.Location);
                if (sText != "N/A")
                {
                    this.PreviousSelRcl = new KeyValuePair<string,Rectangle>(sText,this.m_StationArea[sText]);
                }
            }
           
        }
        
        private void pbMain_Paint(object sender, PaintEventArgs e)
        {
            if (bIsBeginDraw)
            {
                if (m_ptStart.X != 0 && m_ptStart.Y != 0)
                    this.arrCurPosLst.Add(m_ptStart);
                m_ptStart = m_ptEnd;
                this.DrawMainRoute();
            }
        }
        
        private void pbMain_MouseUp(object sender, MouseEventArgs e)
        {
            bIsBeginDraw = false;
            int iCnt = arrCurPosLst.Count;
            this.arrCurPosLst.Clear();
            
            if (this.rdoJumStation.Checked || this.rdoCancelJump.Checked)
            {
                string sText = CheckPosInStationArea(e.Location);
                if (sText != "N/A")
                {
                    this.CurrentSelRcl = new KeyValuePair<string, Rectangle>(sText, this.m_StationArea[sText]);
                    if (CurrentSelRcl.Key == PreviousSelRcl.Key)
                    {
                        this.DrawMainRoute();
                        return;
                    }
                    if (iCnt <= 5) //至少要5个有效点
                    {
                        this.DrawMainRoute();
                        return; 
                    }
                    bool bIsJump=false;
                    if(rdoJumStation.Checked)
                        bIsJump=true;
                    this.JumpOrCancelStationsInMainRoute(PreviousSelRcl, CurrentSelRcl, bIsJump);
                    
                }  
            }
            this.CurrentSelRcl = new KeyValuePair<string, Rectangle>();
            this.PreviousSelRcl = new KeyValuePair<string, Rectangle>();
            this.DrawMainRoute();
        }

        private void pnlTest_MouseDown(object sender, MouseEventArgs e)
        {
            if (this.Cursor == Cursors.Hand)
            {
                m_ptPos = Cursor.Position;
            }
        }

        private void pnlTest_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                int px = Cursor.Position.X - m_ptPos.X;
                int py = Cursor.Position.Y - m_ptPos.Y;
                ((Panel)sender).Location = new Point(((Panel)sender).Location.X + px, ((Panel)sender).Location.Y + py);
                m_ptPos = Cursor.Position;
                m_ptLastPos = Cursor.Position;
            } 
        }

        private void pnlTest_MouseUp(object sender, MouseEventArgs e)
        {
            if (pnlTest.Location.X <= 30 && pnlTest.Location.Y <= 30)
            {
                if (DialogResult.OK == MessageBox.Show("Delete?" + this.pnlTest.Location.X + "," + this.pnlTest.Location.Y))
                {
                    DataTable dtNewRepair = dtAllStations.Clone();
                    //主路由中含有此工站
                    string sLastStation="";
                    string sNextStation="";
                    if (this.m_StationList.ContainsKey(pnlTest.Tag.ToString()))
                    {
                        int i = 0;
                        int j = 0;
                        //获取上一个工站及下一个工站
                        foreach (KeyValuePair<string, Point> kvp in m_StationList)
                        {

                            if (kvp.Key == pnlTest.Tag.ToString())
                            {
                                j = i;
                            }

                            if (j == 0)
                                sLastStation = kvp.Key;

                            if (j + 1 == i && j!=0)
                                sNextStation = kvp.Key;

                            i = i + 1;
                        }

                        foreach (DataRow dr in dtAllStations.Rows)
                        {
                            if (dr["GROUP_NAME"].ToString() == sLastStation && dr["GROUP_NEXT"].ToString() == pnlTest.Tag.ToString() && sNextStation.Trim().Length!=0)
                            {
                                dr["GROUP_NEXT"] = sNextStation;
                                DataRow drNew = dtNewRepair.NewRow();
                                drNew.ItemArray = dr.ItemArray;
                                dtNewRepair.Rows.Add(drNew);
                            }
                            else if (dr["GROUP_NAME"].ToString() != pnlTest.Tag.ToString() && dr["GROUP_NEXT"].ToString() != pnlTest.Tag.ToString())
                            {
                                DataRow drNew = dtNewRepair.NewRow();
                                drNew.ItemArray = dr.ItemArray;
                                dtNewRepair.Rows.Add(drNew);
                            }
                        }
                    }
                    else
                    {  
                        //删除的是维修工站
                        foreach (DataRow dr in dtAllStations.Rows)
                        {
                            if (!((dr["GROUP_NAME"].ToString() == pnlTest.Tag.ToString() && dr["STATE_FLAG"].ToString() == "0") ||
                                (dr["GROUP_NEXT"].ToString() == pnlTest.Tag.ToString() && dr["STATE_FLAG"].ToString() == "1"))
                                )
                            {
                                DataRow drNew = dtNewRepair.NewRow();
                                drNew.ItemArray = dr.ItemArray;
                                dtNewRepair.Rows.Add(drNew);
                            }
                        }
                    }

                    InitData();
                    int iRoute = SFCStartup.dba.GetRouteCode(this.cbbRoute.Text);
                    GetRouteInfoByCodeA(iRoute, "", dtNewRepair);

                    DrawMainRoute();
                    this.panel1.HorizontalScroll.Value = 0;
                }
            }
            else
            {
                this.pnlTest.Location = new Point(-100,-100);
            }

        }

        private void panel1_Scroll(object sender, ScrollEventArgs e)
        {
            this.pnlDel.Location = new Point(this.panel1.HorizontalScroll.Value, pnlDel.Location.Y);
            string sTemp="Value:"+ this.panel1.HorizontalScroll.Value;
            sTemp = sTemp + "\r\n X=" + this.pnlDel.Location.X;
            this.lblPoint.Text = sTemp;
            this.pnlDel.Location = new Point(10,pnlDel.Location.Y);
        }

        private void pnlTest_MouseClick(object sender, MouseEventArgs e)
        {

        }

        private void JumpOrCancelStationsInMainRoute(KeyValuePair<string, Rectangle> kvpFirst, KeyValuePair<string, Rectangle> kvpSecond, bool bIsJumpStation)
        {
            DataTable dtNew = dtAllStations.Copy();
            bool bIsExits = false;
            if (bIsJumpStation)
            {
                #region 两个工站之间划线
                foreach (DataRow dr in dtMain.Rows)
                {
                    if ((dr["GROUP_NAME"].ToString() == kvpFirst.Key && dr["GROUP_NEXT"].ToString() == kvpSecond.Key)
                        || (dr["GROUP_NEXT"].ToString() == kvpFirst.Key && dr["GROUP_NAME"].ToString() == kvpSecond.Key))
                    {
                        bIsExits = true;
                        break;
                    }
                }
                int iStationType = 0;
                if (!bIsExits)
                {
                    if (kvpFirst.Value.Location.X == 0 || kvpSecond.Value.Location.X == 0)
                        return;

                    //检查当前站是否存在维修或者返回
                    if (this.m_StationList.ContainsKey(kvpFirst.Key))
                    {
                        //非维修工站,有且只有1维修工站
                        if (kvpFirst.Value.Location.Y == kvpSecond.Value.Location.Y)
                        {
                            //主路由上跳站
                            DataRow drNew = dtNew.NewRow();
                            drNew["GROUP_NAME"] = kvpFirst.Value.Location.X < kvpSecond.Value.Location.X ? kvpFirst.Key : kvpSecond.Key;
                            drNew["GROUP_NEXT"] = kvpFirst.Value.Location.X < kvpSecond.Value.Location.X ? kvpSecond.Key : kvpFirst.Key;
                            drNew["ROUTE_CODE"] = dtAllStations.Rows[0]["ROUTE_CODE"];
                            drNew["STATE_FLAG"] = iStationType;
                            drNew["STEP_SEQUENCE"] = GetNewRouteStepSequence(dtAllStations);
                            drNew["ROUTE_DESC"] = dtAllStations.Rows[0]["ROUTE_DESC"];
                            dtNew.Rows.Add(drNew);
                        }
                        else
                        {
                            //维修线路
                            DataRow[] drs = dtAllStations.Select("GROUP_NAME='" + kvpFirst.Key + "' AND STATE_FLAG=1 ");
                            if (drs.Count() > 0)
                                return;

                            DataRow drNew = dtNew.NewRow();
                            drNew["GROUP_NAME"] = kvpFirst.Key;
                            drNew["GROUP_NEXT"] = kvpSecond.Key;
                            drNew["ROUTE_CODE"] = dtAllStations.Rows[0]["ROUTE_CODE"];
                            drNew["STATE_FLAG"] = 1;
                            drNew["STEP_SEQUENCE"] = GetNewRouteStepSequence(dtAllStations);
                            drNew["ROUTE_DESC"] = dtAllStations.Rows[0]["ROUTE_DESC"];
                            dtNew.Rows.Add(drNew);
                        }
                    }
                    else
                    {
                        //维修工站，有并且只有一个返回站，并且另一个工站不能为维修站
                        if (kvpFirst.Value.Location.Y == kvpSecond.Value.Location.Y)
                            return;
                        DataRow[] drs = dtAllStations.Select("GROUP_NAME='" + kvpFirst.Key + "' AND STATE_FLAG=0 ");
                        if (drs.Count() > 0)
                            return;
                        iStationType = 1;

                        //维修返回
                        DataRow drNew = dtNew.NewRow();
                        drNew["GROUP_NAME"] = kvpFirst.Key;
                        drNew["GROUP_NEXT"] = kvpSecond.Key;
                        drNew["ROUTE_CODE"] = dtAllStations.Rows[0]["ROUTE_CODE"];
                        drNew["STATE_FLAG"] = 0;
                        drNew["STEP_SEQUENCE"] = GetNewRouteStepSequence(dtAllStations);
                        drNew["ROUTE_DESC"] = dtAllStations.Rows[0]["ROUTE_DESC"];
                        dtNew.Rows.Add(drNew);
                    }

                    InitData();
                    int iRoute = SFCStartup.dba.GetRouteCode(this.cbbRoute.Text);
                    GetRouteInfoByCodeA(iRoute, "", dtNew);

                    DrawMainRoute();
                }
                #endregion
            }//划线处理逻辑结束
            else
            {
                #region 擦除两个工站之间的连线
                if (kvpFirst.Value.Location.X == 0 || kvpSecond.Value.Location.X == 0)
                    return;

                 //检查当前站是否存在维修或者返回
                if (this.m_StationList.ContainsKey(kvpFirst.Key))
                {
                    if (kvpFirst.Value.Location.Y == kvpSecond.Value.Location.Y)//跳站
                    {
                        int iLocA = kvpFirst.Value.Location.X;
                        int iLocB = kvpSecond.Value.Location.X;
                        int iLen = iLocA - iLocB;
                        if ((Math.Abs(iLen) / (this.STATION_SPACE + this.STATION_WIDTH)) >= 2)
                        {
                            string sFirst, sSencond;
                            if (iLocA < iLocB)
                            {
                                sFirst = kvpFirst.Key;
                                sSencond = kvpSecond.Key;
                            }
                            else
                            {
                                sFirst = kvpSecond.Key;
                                sSencond = kvpFirst.Key;
                            }
                            foreach (DataRow dr in dtNew.Select("GROUP_NAME='" + sFirst + "' AND GROUP_NEXT='" + sSencond + "'  AND STATE_FLAG=0"))
                            {
                                dtNew.Rows.Remove(dr);
                                dtNew.AcceptChanges();
                            }

                        }
                    }
                    else
                    {
                        //维修
                        DataRow []drs=dtNew.Select("GROUP_NEXT='" + kvpSecond.Key.ToString() + "' AND STATE_FLAG=1 ");    

                        foreach (DataRow dr in dtNew.Select("GROUP_NAME='" + kvpFirst.Key.ToString() + "' AND GROUP_NEXT='" + kvpSecond.Key.ToString() + "' AND STATE_FLAG=1"))
                        {
                            dtNew.Rows.Remove(dr);
                            dtNew.AcceptChanges();
                        }
                        if (drs.Count() == 1)
                        {
                            DataRow[]drReturn=dtNew.Select("GROUP_NAME='" + kvpSecond.Key.ToString() + "' AND STATE_FLAG=0");
                            if (drReturn.Count() > 0)
                            {
                                dtNew.Rows.Remove(drReturn[0]);
                                dtNew.AcceptChanges();
                            }
                        }
                    }
                }
                else
                {
                    //维修 返回
                    DataRow[] drs = dtNew.Select("GROUP_NEXT='" + kvpSecond.Key.ToString() + "' AND STATE_FLAG=1 ");

                    foreach (DataRow dr in dtNew.Select("GROUP_NAME='" + kvpFirst.Key.ToString() + "' AND GROUP_NEXT='" + kvpSecond.Key.ToString() + "' AND STATE_FLAG=0"))
                    {
                        dtNew.Rows.Remove(dr);
                        dtNew.AcceptChanges();
                    }
                    if (drs.Count() == 1)
                    {
                        DataRow[] drReturn = dtNew.Select("GROUP_NAME='" + kvpSecond.Key.ToString() + "' AND STATE_FLAG=0");
                        if (drReturn.Count() > 0)
                        {
                            dtNew.Rows.Remove(drReturn[0]);
                            dtNew.AcceptChanges();
                        }
                    }

                }

                InitData();
                int iRoute = SFCStartup.dba.GetRouteCode(this.cbbRoute.Text);
                GetRouteInfoByCodeA(iRoute, "", dtNew);

                DrawMainRoute();
                #endregion
            }
        }

        public int GetNewRouteStepSequence(DataTable dtTarget)
        {
            int iRouteCode = 0;
            iRouteCode = Convert.ToInt32(dtTarget.Compute("MAX(STEP_SEQUENCE)", "").ToString())+1;
            return iRouteCode;
        }

        private void btnCancelJump_Click(object sender, EventArgs e)
        { 
            //DataTable dtNew = dtMain.Copy();
            if (CurrentSelRcl.Key == null || PreviousSelRcl.Key == null)
                return;
            bool bIsExits = false;
            string sPre = CurrentSelRcl.Value.Location.X < PreviousSelRcl.Value.Location.X ? CurrentSelRcl.Key : PreviousSelRcl.Key;
            string sAfter = CurrentSelRcl.Value.Location.X > PreviousSelRcl.Value.Location.X ? CurrentSelRcl.Key : PreviousSelRcl.Key;
            foreach (MdRepairStationsInfo msi in this.m_stationSpecialList)
            {
                if (msi.FROM_STATION_GROUP == sPre && msi.TO_STATION_GROUP == sAfter)
                {
                    bIsExits = true;
                    break;
                }
                
            }
            if (bIsExits)
            {
                DataTable dtNew=this.dtAllStations.Copy();
                foreach(DataRow dr in dtNew.Rows)
                {
                    if (dr["GROUP_NAME"].ToString() == sPre && dr["GROUP_NEXT"].ToString()==sAfter)
                    {
                        dtNew.Rows.Remove(dr);
                        break;
                    }
                }
                
                InitData();

                GetRouteInfoByCodeA(mdNewRoute.RouteCode, "", dtNew);

                DrawMainRoute();
            }
        }

        private void pnlRepair_MouseUp(object sender, MouseEventArgs e)
        {
            int iIndex = 0;
            if (this.m_stationRepairList != null)
                iIndex = m_StationList.Count;
            double dwDist = 0;
            
            //寻找被占用的维修站坐标
            ArrayList arr=new ArrayList();
            foreach (MdRepairStationsInfo msi in m_stationRepairList)
            {
                int x = (msi.TO_STATION_POSITION.X - 30) / (STATION_SPACE + STATION_WIDTH );
                x = x + 1;
                arr.Add(x);
            }

            //寻找维修发起站并计算释放点坐标到目标点的距离是否为有效距离
            int i = 0;
            string sFrom = "";
            foreach (KeyValuePair<string,Point>  kvp in m_StationList)
            {
                if (arr.IndexOf(i) >= 0)
                {
                    i = i + 1;
                    continue;
                }

                int pAx=kvp.Value.X;
                int pAy = REPAIR_HEIGTH;
                    
                int pBx=this.pnlRepair.Location.X+panel1.HorizontalScroll.Value;
                int pBy=this.pnlRepair.Location.Y-162;

                double dwValue = Math.Sqrt((pAx - pBx) * (pAx - pBx) + (pAy - pBy) * (pAy - pBy));
                if (dwDist == 0 || dwDist > dwValue)
                {
                    dwDist = dwValue;
                    iIndex = i;
                    sFrom = kvp.Key;
                }
                i = i + 1;
            }

            //如果距离小于100
            if (dwDist < 100)
            {
                //设置目标维修工站的坐落位置
                this.pnlRepair.Location = new Point(iIndex * (STATION_SPACE + STATION_WIDTH)+43, REPAIR_HEIGTH+162);

                int iRoute = SFCStartup.dba.GetRouteCode(this.cbbRoute.Text);
                //DataTable dtRep=SFC_Tools.SFCStartup.dba.GetRepairStationsByRouteCode(iRoute);
                DataRow dr =this.dtAllStations.NewRow();
                if (this.dtAllStations.Rows.Count > 0)
                {
                    dr["ROUTE_CODE"] = dtAllStations.Rows[0]["ROUTE_CODE"];
                    dr["STATE_FLAG"] = 1;
                    dr["STEP_SEQUENCE"] = Convert.ToInt32(dtAllStations.Rows[dtAllStations.Rows.Count - 1]["STEP_SEQUENCE"]) + 1;
                    dr["ROUTE_DESC"] = dtAllStations.Rows[0]["ROUTE_DESC"];
                }
                else
                {
                    dr["ROUTE_CODE"] = 0;
                    dr["STATE_FLAG"] = 1;
                    dr["STEP_SEQUENCE"] = 1;
                    dr["ROUTE_DESC"] = "TEST";
                }
                dr["GROUP_NAME"] = sFrom;
                dr["GROUP_NEXT"] = "R_" + sFrom;
                dtAllStations.Rows.Add(dr);

                InitData();
                GetRouteInfoByCodeA(iRoute, "", dtAllStations);
                DrawMainRoute();
            }
            else
                ((Panel)sender).Location=m_ptPosOriginal;

            Point pt = new Point();
            pt.X = iIndex * 75 - (STATION_SPACE / 2 + STATION_WIDTH);
            pt.Y = ROUTE_MAIN_HEIGTH - 15;
            ((Panel)sender).Location=m_ptPosOriginal;
        }

        private void btnRepair_Click(object sender, EventArgs e)
        {
            if (this.CurrentSelRcl.Key == null || this.PreviousSelRcl.Key == null)
                return;
            if (this.CurrentSelRcl.Key.ToString() != "0"
                && this.PreviousSelRcl.Key.ToString()!="0"
                && CurrentSelRcl.Value.Y != PreviousSelRcl.Value.Y
                )
            {
                KeyValuePair<string, Rectangle> sRepair;
                KeyValuePair<string, Rectangle> sStation;
                //根据左边判断维修工站
                if (CurrentSelRcl.Value.Y > PreviousSelRcl.Value.Y)
                {
                    sRepair = CurrentSelRcl;
                    sStation = PreviousSelRcl;
                }
                else
                {
                    sRepair = PreviousSelRcl;
                    sStation = CurrentSelRcl;
                }
                DataRow[] drB = dtAllStations.Select("GROUP_NAME='" + sStation.Key + "'  AND STATE_FLAG=1 ");
                if (drB.Count() > 0)
                    return;
                DataRow[] dr = dtAllStations.Select("GROUP_NAME='" + sStation.Key + "' AND GROUP_NEXT='" + sRepair.Key + "' AND STATE_FLAG=1 ");
                if (dr.Count() == 0)
                {
                    DataRow drA = dtAllStations.NewRow();
                    drA["ROUTE_CODE"] = this.mdNewRoute.RouteCode;
                    drA["GROUP_NAME"] = sStation.Key;
                    drA["GROUP_NEXT"] = sRepair.Key;
                    drA["STATE_FLAG"] = 1;
                    drA["STEP_SEQUENCE"] = GetNewRouteStepSequence(dtAllStations);
                    drA["ROUTE_DESC"] = mdNewRoute.RouteDesc;              

                    dtAllStations.Rows.Add(drA);
                    InitData();

                    GetRouteInfoByCodeA(mdNewRoute.RouteCode, "", dtAllStations);

                    DrawMainRoute();
                }     
            }   
        }

        private void btnReturn_Click(object sender, EventArgs e)
        {
            if (this.CurrentSelRcl.Key == null || this.PreviousSelRcl.Key == null)
                return;
            if (this.CurrentSelRcl.Key.ToString() != "0"
                && this.PreviousSelRcl.Key.ToString() != "0"
                && CurrentSelRcl.Value.Y != PreviousSelRcl.Value.Y
                )
            {
                KeyValuePair<string, Rectangle> sRepair;
                KeyValuePair<string, Rectangle> sStation;
                //根据左边判断维修工站
                if (CurrentSelRcl.Value.Y > PreviousSelRcl.Value.Y)
                {
                    sRepair = CurrentSelRcl;
                    sStation = PreviousSelRcl;
                }
                else
                {
                    sRepair = PreviousSelRcl;
                    sStation = CurrentSelRcl;
                }
                DataRow[] drB = dtAllStations.Select("GROUP_NAME='" + sRepair.Key + "'  AND STATE_FLAG=0 ");
                if (drB.Count() > 0)
                    return;
                DataRow[] dr = dtAllStations.Select("GROUP_NAME='" + sStation.Key + "' AND GROUP_NEXT='" + sRepair.Key + "' AND STATE_FLAG=0 ");
                if (dr.Count() == 0)
                {
                    DataRow drA = dtAllStations.NewRow();
                    drA["ROUTE_CODE"] = this.mdNewRoute.RouteCode;
                    drA["GROUP_NAME"] = sRepair.Key;
                    drA["GROUP_NEXT"] = sStation.Key;
                    drA["STATE_FLAG"] = 0;
                    drA["STEP_SEQUENCE"] = GetNewRouteStepSequence(dtAllStations);
                    drA["ROUTE_DESC"] = mdNewRoute.RouteDesc;

                    dtAllStations.Rows.Add(drA);
                    InitData();

                    GetRouteInfoByCodeA(mdNewRoute.RouteCode, "", dtAllStations);

                    DrawMainRoute();
                }


            }
        }

        
    }
}
