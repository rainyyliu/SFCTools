using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SFC_Tools.Classes;
using SFC_Tools.Model;

using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Collections;

namespace SFC_Tools.Forms
{
    public partial class ucSMOTest : ucParentControl
    {

       
        public DataSet dsStationInfo;
        public TSTATIONINFO StnInfo;
        //public ArrayList arrStation;
        public ArrayList SecInfo;
        private CSockConnThread _subThreadSockConn;
        private CGW28RunThread _subThreadGW28Run;
        private CSMOThread _subThreadSmo;
        private string[,] strDctInfo=new string[64,64];
        private const int MAX_SCAN_ITEMS = 500;
        private delegate void MessageHandler(MyMessageEventArgs e);
       
        //Thread tCheckStationStatus;


        delegate void ShowMessage(string message,string sDtID,bool bIsSend);
        delegate void UpdateStationStatus(int x, int y, bool status);

        private void OnUpdateStationStatus(int x, int y, bool status)
        {
            if(this.lvTerminalInfo.InvokeRequired)
            {
                UpdateStationStatus up = new UpdateStationStatus(OnUpdateStationStatus);
                this.lvTerminalInfo.BeginInvoke(up, new object[] { x, y, status });
            }
            else
            {
                if (status)
                {
                    this.lvTerminalInfo.Items[y].SubItems[x].Tag = "OK";

                }
                else
                {
                    this.lvTerminalInfo.Items[y].SubItems[x].Tag = "NG";
                }
                this.lvTerminalInfo.Invalidate();
            }
        }

        private void ShowMessageToView(string message, string sDTAID, bool bIsSend)
        {
            if (this.lvScanInfo.InvokeRequired)
            {
                ShowMessage sm = new ShowMessage(ShowMessageToView);
                lvScanInfo.BeginInvoke(sm, new object[] { message, sDTAID, bIsSend });
            }
            else
            {
                string sTimeInfo=DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                ListViewItem lvi = new ListViewItem();
                if (bIsSend)
                {
                    lvi.ImageIndex = 0;
                    lvi.Text = sTimeInfo+"(SeMsg):";
                }
                else
                {
                    lvi.ImageIndex = 1;
                    lvi.Text = sTimeInfo + "(ReMsg):"; 
                }

                lvi.SubItems.Add(sDTAID);
                lvi.SubItems.Add(message);

                this.lvScanInfo.Items.Insert(0, lvi);

                if (lvScanInfo.Items.Count > MAX_SCAN_ITEMS)
                {
                    lvScanInfo.Items.RemoveAt(MAX_SCAN_ITEMS);
                }

            }
        
        }

      
        public ucSMOTest()
        {
            InitializeComponent();    
        }

        private void ucSMOTest_Load(object sender, EventArgs e)
        {
            mdHostInfo hostinfo=CMESAccess.GetHostInfoByName("FXC");
            if (hostinfo != null)
            {
                dsStationInfo = CMESAccess.GetStationInfoByHostID(hostinfo.HostID);
                Globle.arrStation = GetStationInforFromDataSet(dsStationInfo);
                GetSectionInfoFromDataSet(dsStationInfo);
                SecInfo = GetSectionInfoFromDataSet(dsStationInfo);
                TreeNode root = new TreeNode();
                root.Text = hostinfo.HostName + "[" + hostinfo.HostID.ToString() + "]";
                root.Name = hostinfo.HostID;
                root.ImageIndex = 0;
                tvHostInfo.Nodes.Add(root);
                AddTreeChildren(tvHostInfo, root, dsStationInfo.Tables[0]);
            }

            tvHostInfo.ImageList = imgLstTreeView;
            tvHostInfo.ExpandAll();
            tvHostInfo.SelectedNode = tvHostInfo.Nodes[0];

            InitListViewStation(lvStationTypeInfo);
            InitListViewScanInfo(lvScanInfo);
            InitListViewWorkTypeInfo(lvWorkTypeInfo);

        }
        private void MyMessage(MyMessageEventArgs e)
        {
            /*
             *每隔2.5秒更新工站状态信息，重新绘制界面
             */
            //Globle.arrStation = e.arrStationInfo;
            //for (int i = 0; i < Globle.arrStation.Count; i++)
            //{
            //    int x = ((TSTATIONINFO)Globle.arrStation[i]).DctMapData.inCol;
            //    int y = ((TSTATIONINFO)Globle.arrStation[i]).DctMapData.inRow;

            //    if (((TSTATIONINFO)Globle.arrStation[i]).strDctInfo.OnLineStatus == "OK")
            //    {
            //        this.lvTerminalInfo.Items[y].SubItems[x].Tag = "OK";

            //    }
            //    else
            //    {
            //        this.lvTerminalInfo.Items[y].SubItems[x].Tag = "NG";
            //    }
            //}
            //this.lvTerminalInfo.Invalidate();

            //if (this._subThreadGW28Run != null)
            //    _subThreadGW28Run.UpdateData();
            this.OnUpdateStationStatus(((TSTATIONINFO)Globle.arrStation[e.stationStatus.ID]).DctMapData.inRow, ((TSTATIONINFO)Globle.arrStation[e.stationStatus.ID]).DctMapData.inCol, e.stationStatus.Status);

        }

        private void subThreadUpdateStationStatus()
        {
            while (true)
            {
                for (int i = 0; i < Globle.arrStation.Count; i++)
                {
                    int x = ((TSTATIONINFO)Globle.arrStation[i]).DctMapData.inCol;
                    int y = ((TSTATIONINFO)Globle.arrStation[i]).DctMapData.inRow;

                    if (((TSTATIONINFO)Globle.arrStation[i]).strDctInfo.OnLineStatus == "OK")
                    {
                        //this.lvTerminalInfo.Items[y].SubItems[x].Tag = "OK";
                        OnUpdateStationStatus(x, y, true);

                    }
                    else
                    {
                        OnUpdateStationStatus(x, y, false);
                        //this.lvTerminalInfo.Items[y].SubItems[x].Tag = "NG";
                    }
                }
                Thread.Sleep(2000);
            }
        }
        private void _SubThread_MessageSend(object sender, MyMessageEventArgs e)
        {
            MessageHandler handler = new MessageHandler(MyMessage);
            this.Invoke(handler, new object[] { e });
        }

        private void _subThreadGW28Run_MessageSend(object sender, MyMessageEventArgs e)
        {
            ShowMessageToView(e.ScanMsg.ScanMessage, "0", e.ScanMsg.bSendMsg);
        }

        /// <summary>
        /// 生成工站信息树
        /// </summary>
        /// <param name="tv"></param>
        /// <param name="tn"></param>
        private void AddTreeChildren(TreeView tv,TreeNode tn,DataTable dt)
        {
            if (dt == null)
                return;
            TreeNode trLine, trSection, trGroup,trStation;
            string sLine, sSection, sGroup, sStation;

            foreach (DataRow dr in dt.Rows)
            {
                sLine = dr["lineid"].ToString();
                sSection = dr["sectionid"].ToString();
                sGroup = dr["groupid"].ToString();
                sStation = dr["stationid"].ToString();
                
                if (tn.Nodes.IndexOfKey(sLine) < 0)
                {
                    trLine = new TreeNode(sLine);
                    trLine.Name = sLine;
                    trLine.ImageIndex = 1;
                    trLine.SelectedImageIndex = 1;
                    tn.Nodes.Add(trLine);

                    trSection = new TreeNode(sSection);
                    trSection.Name = sSection;
                    trSection.ImageIndex = 2;
                    trSection.SelectedImageIndex = 2;
                    trLine.Nodes.Add(trSection);

                    trGroup = new TreeNode(sGroup);
                    trGroup.Name = sGroup;
                    trGroup.ImageIndex = 3;
                    trGroup.SelectedImageIndex = 3;
                    trSection.Nodes.Add(trGroup);

                    trStation = new TreeNode(sStation);
                    trStation.Name = sStation;
                    trStation.ImageIndex = 4;
                    trStation.SelectedImageIndex = 4;
                    trGroup.Nodes.Add(trStation);
                }
                else if (tn.Nodes[tn.Nodes.IndexOfKey(sLine)].Nodes.IndexOfKey(sSection) < 0)
                {
                    trLine = tn.Nodes[tn.Nodes.IndexOfKey(sLine)];
                    trSection = new TreeNode(sSection);
                    trSection.Name = sSection;
                    trSection.ImageIndex = 2;
                    trSection.SelectedImageIndex = 2;
                    trLine.Nodes.Add(trSection);

                    trGroup = new TreeNode(sGroup);
                    trGroup.Name = sGroup;
                    trGroup.ImageIndex = 3;
                    trGroup.SelectedImageIndex = 3;
                    trSection.Nodes.Add(trGroup);

                    trStation = new TreeNode(sStation);
                    trStation.Name = sStation;
                    trStation.ImageIndex = 4;
                    trStation.SelectedImageIndex = 4;
                    trGroup.Nodes.Add(trStation); 
                }
                else if (tn.Nodes[tn.Nodes.IndexOfKey(sLine)].Nodes[tn.Nodes[tn.Nodes.IndexOfKey(sLine)].Nodes.IndexOfKey(sSection)].Nodes.IndexOfKey(sGroup) < 0)
                {
                    trSection = tn.Nodes[tn.Nodes.IndexOfKey(sLine)].Nodes[tn.Nodes[tn.Nodes.IndexOfKey(sLine)].Nodes.IndexOfKey(sSection)];
                   
                    trGroup = new TreeNode(sGroup);
                    trGroup.Name = sGroup;
                    trGroup.ImageIndex = 3;
                    trGroup.SelectedImageIndex = 3;
                    trSection.Nodes.Add(trGroup);

                    trStation = new TreeNode(sStation);
                    trStation.Name = sStation;
                    trStation.ImageIndex = 4;
                    trStation.SelectedImageIndex = 4;
                    trGroup.Nodes.Add(trStation);

                }
                //  tn.Nodes[tn.Nodes.IndexOfKey(sLine)].Nodes[tn.Nodes[tn.Nodes.IndexOfKey(sLine)].Nodes.IndexOfKey(sSection)].Nodes[tn.Nodes[tn.Nodes.IndexOfKey(sLine)].Nodes[tn.Nodes[tn.Nodes.IndexOfKey(sLine)].Nodes.IndexOfKey(sSection)].Nodes.IndexOfKey(sGroup)].Nodes.IndexOfkey()
                else if (tn.Nodes[tn.Nodes.IndexOfKey(sLine)].Nodes[tn.Nodes[tn.Nodes.IndexOfKey(sLine)].Nodes.IndexOfKey(sSection)].Nodes[tn.Nodes[tn.Nodes.IndexOfKey(sLine)].Nodes[tn.Nodes[tn.Nodes.IndexOfKey(sLine)].Nodes.IndexOfKey(sSection)].Nodes.IndexOfKey(sGroup)].Nodes.IndexOfKey(sStation) < 0)
                {
                    trGroup = tn.Nodes[tn.Nodes.IndexOfKey(sLine)].Nodes[tn.Nodes[tn.Nodes.IndexOfKey(sLine)].Nodes.IndexOfKey(sSection)].Nodes[tn.Nodes[tn.Nodes.IndexOfKey(sLine)].Nodes[tn.Nodes[tn.Nodes.IndexOfKey(sLine)].Nodes.IndexOfKey(sSection)].Nodes.IndexOfKey(sGroup)];

                    trStation = new TreeNode(sStation);
                    trStation.Name = sStation;
                    trStation.ImageIndex = 4;
                    trStation.SelectedImageIndex = 4;
                    trGroup.Nodes.Add(trStation);
                }
                
            }
        }

        private void InitListViewStation(ListView ls)
        {
            ls.BeginUpdate();
            ColumnHeader ch1 = new ColumnHeader();
            ch1.Name = "chDTAItem";
            ch1.Text = "DTAItem";
            ch1.Width = 120;
            ls.Columns.Add(ch1);

            ColumnHeader ch2 = new ColumnHeader();
            ch2.Name = "chValue";
            ch2.Text = "Value";
            ch2.Width = 100;
            ls.Columns.Add(ch2);

            ListViewItem lvi = new ListViewItem("HostID");
            lvi.Text="HOST ID";
            ls.Items.Add(lvi);

            ListViewItem lvi1 = new ListViewItem("Line");
            lvi1.Text = "NAMEOF LINE";
            ls.Items.Add(lvi1);

            ListViewItem lvi2 = new ListViewItem("Section");
            lvi2.Text = "NAMEOF SECTION";
            ls.Items.Add(lvi2);

            ListViewItem lvi3 = new ListViewItem("Group");
            lvi3.Text = "NAMEOF GROUP";
            ls.Items.Add(lvi3);
            
            ListViewItem lvi4 = new ListViewItem("Station");
            lvi4.Text = "NAMEOF STATION";
            ls.Items.Add(lvi4);

            ListViewItem lvi5 = new ListViewItem("StationNumber");
            lvi5.Text = "STATION NUMBER";
            ls.Items.Add(lvi5);

            ListViewItem lvi6 = new ListViewItem("StationType");
            lvi6.Text = "STATION TYPE";
            ls.Items.Add(lvi6);

            ListViewItem lvi7 = new ListViewItem("StationTypeName");
            lvi7.Text = "NAMEOF STATION TYPE";
            ls.Items.Add(lvi7);

            ListViewItem lviLine = new ListViewItem("MidLine");
            lviLine.Text = "-------------------";
            ls.Items.Add(lviLine);

            ListViewItem lvi8 = new ListViewItem("DataIn");
            lvi8.Text = "DTA IN";
            ls.Items.Add(lvi8);

            ListViewItem lvi9 = new ListViewItem("InType");
            lvi9.Text = "IN TYPE";
            ls.Items.Add(lvi9);

            ListViewItem lvi10 = new ListViewItem("OutType");
            lvi10.Text = "OUT TYPE";
            ls.Items.Add(lvi10);

            ListViewItem lvi11 = new ListViewItem("StationTypeName");
            lvi11.Text = "DTA PORT";
            ls.Items.Add(lvi11);

            ListViewItem lvi12 = new ListViewItem("DtaReMsg");
            lvi12.Text = "DTA ReMsg";
            ls.Items.Add(lvi12);

            ListViewItem lvi13 = new ListViewItem("DtaSeMsg");
            lvi13.Text = "DTA SeMsg";
            ls.Items.Add(lvi13);


            ls.GridLines = true;//显示网格线
            ls.FullRowSelect = true;//是否全行选择
            ls.HideSelection = false;//失去焦点时显示选择的项
            ls.HoverSelection = true;//当鼠标停留数秒时自动选择项
            ls.MultiSelect = false;//设置只能单选
            ls.View = View.Details;
            ls.EndUpdate();
        }

        /// <summary>
        /// init the list view of scan info,set columns 
        /// </summary>
        /// <param name="ls"></param>
        private void InitListViewScanInfo(ListView ls)
        {
            ColumnHeader ch1 = new ColumnHeader();
            ch1.Name = "chTime";
            ch1.Text = "Time";
            ch1.Width = 200;
            ls.Columns.Add(ch1);

            ColumnHeader ch2 = new ColumnHeader();
            ch2.Name = "chDataID";
            ch2.Text = "DATAID";
            ch2.Width = 50;
            ls.Columns.Add(ch2);

            ColumnHeader ch3 = new ColumnHeader();
            ch3.Name = "chScanInfo";
            ch3.Text = "Information";
            ch3.Width = 200;
            ls.Columns.Add(ch3);

            ls.GridLines = true;//显示网格线
            ls.FullRowSelect = true;//是否全行选择
            ls.HideSelection = false;//失去焦点时显示选择的项
            ls.HoverSelection = true;//当鼠标停留数秒时自动选择项
            ls.MultiSelect = false;//设置只能单选
            ls.View = View.Details;

            ImageList imgLst = new ImageList();
            imgLst.Images.Add((Image)Properties.Resources.icSend);
            imgLst.Images.Add((Image)Properties.Resources.icReceive);
            lvScanInfo.StateImageList = imgLst;
            lvScanInfo.SmallImageList = imgLst;
            lvScanInfo.LargeImageList = imgLst;
        }

        private void InitListViewWorkTypeInfo(ListView ls)
        {
            ColumnHeader ch1 = new ColumnHeader();
            ch1.Name = "chIndex";
            ch1.Text = "INDEX";
            ch1.Width = 100;
            ls.Columns.Add(ch1);

            ColumnHeader ch2 = new ColumnHeader();
            ch2.Name = "chWorkType";
            ch2.Text = "WORK TYPE";
            ch2.Width = 100;
            ls.Columns.Add(ch2);

            ColumnHeader ch3 = new ColumnHeader();
            ch3.Name = "chWorkTypeName";
            ch3.Text = "WORK TYPE NAME";
            ch3.Width = 100;
            ls.Columns.Add(ch3);

            ColumnHeader ch4 = new ColumnHeader();
            ch4.Name = "chNumberOfProc";
            ch4.Text = "NUMBER OF PROCEDURE";
            ch4.Width = 100;
            ls.Columns.Add(ch4);

            ColumnHeader ch5 = new ColumnHeader();
            ch5.Name = "chIsLast";
            ch5.Text = "LAST";
            ch5.Width = 100;
            ls.Columns.Add(ch5);

            ColumnHeader ch6 = new ColumnHeader();
            ch6.Name = "chRule";
            ch6.Text = "RULE";
            ch6.Width = 100;
            ls.Columns.Add(ch6);

            ColumnHeader ch7 = new ColumnHeader();
            ch7.Name = "chFork";
            ch7.Text = "FORK";
            ch7.Width = 100;
            ls.Columns.Add(ch7);

            ColumnHeader ch8 = new ColumnHeader();
            ch8.Name = "chSecond";
            ch8.Text = "SECOND";
            ch8.Width = 100;
            ls.Columns.Add(ch8);

            ColumnHeader ch9 = new ColumnHeader();
            ch9.Name = "chLength";
            ch9.Text = "LENGTH";
            ch9.Width = 100;
            ls.Columns.Add(ch9);

            ls.GridLines = true;//显示网格线
            ls.FullRowSelect = true;//是否全行选择
            ls.HideSelection = false;//失去焦点时显示选择的项
            ls.HoverSelection = true;//当鼠标停留数秒时自动选择项
            ls.MultiSelect = false;//设置只能单选
            ls.View = View.Details;
        }
        private void InitListView(ListView listView1)
        {
            //添加列头
            ColumnHeader ch1 = new ColumnHeader();
            ch1.Width = 100; //列标头宽
            ch1.Text = "学号";　//列标头名称
            ColumnHeader ch2 = new ColumnHeader();
            ch2.Width = 100;
            ch2.Text = "姓名";
            listView1.Columns.Add(ch1);//在同一行上添别的列（此处一行共两列)
            listView1.Columns.Add(ch2);

            //设置属性
            listView1.GridLines = true;//显示网格线
            listView1.FullRowSelect = true;//是否全行选择
            listView1.HideSelection = false;//失去焦点时显示选择的项
            listView1.HoverSelection = true;//当鼠标停留数秒时自动选择项
            listView1.MultiSelect = false;//设置只能单选

            //ImageList li = new ImageList();
            //li.ImageSize = new Size(80, 80);//指定图标的大小
            //li.Images.Add(Image.FromFile("pen.jpg"));//添加图标
            //li.Images.Add(Image.FromFile("box.jpg"));
            //li.Images.Add(Image.FromFile("file.jpg"));
            //listView1.LargeImageList = li;//设置大图标的集合

            //ImageList sm = new ImageList();
            //sm.ImageSize = new Size(30, 30);//指定图标大小
            //sm.Images.Add(Image.FromFile("pen.jpg"));
            //sm.Images.Add(Image.FromFile("box.jpg"));
            //sm.Images.Add(Image.FromFile("file.jpg"));
            //listView1.SmallImageList = sm;//设置小图标

            //添加项
            ListViewItem lv = new ListViewItem("钢笔");//第一列的记录为钢笔
            lv.SubItems.Add("001");//添加第二列的内容为001
            lv.SubItems.Add("派克");//添加第三列的内容
            lv.ImageIndex = 0;//指定图像的索引
            listView1.Items.Add(lv);

            listView1.View = View.Details;


        }

        private void tsbtnStart_Click(object sender, EventArgs e)
        {
            if (tsbtnStart.Checked)
                return;
            tsbtnStart.Checked = true;
            tsbtnPause.Checked = false;
            tsbtnStop.Checked = false;
            DrawTerminalStationInfo();
           
            
            _subThreadSockConn = new CSockConnThread();
            this._subThreadSockConn.MessageSend += new CSockConnThread.MyMessageEventHandler(this._SubThread_MessageSend);
            _subThreadSockConn.StartProcess();


            _subThreadGW28Run = new CGW28RunThread();
            _subThreadGW28Run.MessageSend += new CGW28RunThread.MyMessageEventHandler(this._subThreadGW28Run_MessageSend);
            _subThreadGW28Run.StartProcess();

            _subThreadSmo = new CSMOThread();
            _subThreadSmo.StartProcess();

            //Thread t = new Thread(new ThreadStart(subThreadUpdateStationStatus));
            //t.Start();
        }

        private void DrawTerminalStationInfo()
        {
            lvTerminalInfo.Items.Clear();
            lvTerminalInfo.Columns.Clear();
            for (int i = 0; i < SecInfo.Count; i++)
            {
                int iSec = 0;
                for (int j = 0; j < Globle.arrStation.Count; j++)
                {
                    if (((TSECTIONINFO)SecInfo[i]).LineName == ((TSTATIONINFO)Globle.arrStation[j]).LineName &&
                        ((TSECTIONINFO)SecInfo[i]).SectionName == ((TSTATIONINFO)Globle.arrStation[j]).SectionName)
                    {
                        DctMap dcMap = new DctMap();
                        dcMap.DCTID = ((TSTATIONINFO)Globle.arrStation[j]).strDctConfigInfo.inNodeID;
                        dcMap.inRow = iSec;
                        dcMap.inCol = i;
                        dcMap.DctInfo = ((TSTATIONINFO)Globle.arrStation[j]).StationName + ((TSTATIONINFO)Globle.arrStation[j]).TaskCode;
                        ((TSTATIONINFO)Globle.arrStation[j]).DctMapData = dcMap;
                        strDctInfo[i, iSec] = ((TSTATIONINFO)Globle.arrStation[j]).GroupName;
                        //lv.SubItems.Add(subItem);
                        iSec++;
                    }
                }

                ColumnHeader cl = new ColumnHeader();
                cl.Name = ((TSECTIONINFO)SecInfo[i]).LineName + "-->" + ((TSECTIONINFO)SecInfo[i]).SectionName;
                cl.Tag = ((TSECTIONINFO)SecInfo[i]).SectionName;
                cl.Text = ((TSECTIONINFO)SecInfo[i]).LineName + "-->" + ((TSECTIONINFO)SecInfo[i]).SectionName;
                cl.Width = 150;
                cl.ImageIndex = 5;
                lvTerminalInfo.Columns.Add(cl);
            }

            //i is the row here 
            for (int i = 0; i < 64; i++)
            {
                ListViewItem lv = new ListViewItem();
                lv.UseItemStyleForSubItems = false;
                //j is the col here
                int k = 0;
                if (strDctInfo[0, i] != null)
                {
                    lv.Text = strDctInfo[0, i];
                    lv.Tag = "NG";
                }
                for (int j = 1; j < lvTerminalInfo.Columns.Count; j++)
                {
                    ListViewItem.ListViewSubItem subItem = new ListViewItem.ListViewSubItem();
                    subItem.Tag = "NG";
                    if (strDctInfo[j, i] != null)
                    {
                        subItem.Text = strDctInfo[j, i];

                    }
                    else
                    {
                        subItem.Tag = "N";
                        subItem.Text = "";
                        k++;
                        //continue;
                    }
                    lv.SubItems.Add(subItem);
                }
                this.lvTerminalInfo.Items.Insert(i, lv);
                if (k == (lvTerminalInfo.Columns.Count - 1))
                    break;
            }

            lvTerminalInfo.OwnerDraw = true;
            lvTerminalInfo.GridLines = true;//显示网格线
            //lvTerminalInfo.FullRowSelect = false;//是否全行选择
            lvTerminalInfo.HideSelection = false;//失去焦点时显示选择的项
            lvTerminalInfo.HoverSelection = true;//当鼠标停留数秒时自动选择项
            lvTerminalInfo.MultiSelect = false;//设置只能单选
            lvTerminalInfo.StateImageList = imgLstTreeView;
            lvTerminalInfo.View = View.Details;
            lvTerminalInfo.SetSortIcon(9, SortOrder.Ascending);
        }

        /// <summary>
        /// Formate the info from recordset
        /// </summary>
        /// <param name="ds">original station info</param>
        /// <returns>station info list</returns>
        private ArrayList GetStationInforFromDataSet(DataSet ds)
        {
            ArrayList arr = new ArrayList();
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                TSTATIONINFO sStationInfo = new TSTATIONINFO();
                sStationInfo.HostID = dr["hostid"].ToString();
                sStationInfo.GroupName = dr["groupid"].ToString();
                sStationInfo.StationName = dr["stationid"].ToString();
                sStationInfo.LineName = dr["lineid"].ToString();
                sStationInfo.SectionName = dr["sectionid"].ToString();
                TDCTCONFIG dctConfig = new TDCTCONFIG();
                TDCTINFO dctInfo = new TDCTINFO();
                sStationInfo.strDctConfigInfo = dctConfig;
                dctInfo.OnLineStatus = "NG";
                sStationInfo.strDctInfo = dctInfo;
                sStationInfo.strDctConfigInfo.stIP = dr["socketip"].ToString();
                sStationInfo.strDctConfigInfo.Port = dr["socket_port"].ToString();
                sStationInfo.strDctConfigInfo.inInType=Convert.ToInt32(dr["input_type"].ToString());
                sStationInfo.strDctConfigInfo.inOutType = Convert.ToInt32(dr["output_type"].ToString());
                arr.Add(sStationInfo);
            }
            return arr;
        }

        private ArrayList GetSectionInfoFromDataSet(DataSet ds)
        {
            ArrayList arr = new ArrayList();
            var SectionInfo = (from t in ds.Tables[0].AsEnumerable()
                      select  new { lineid = t.Field<string>("lineid"), sectionid = t.Field<string>("sectionid") }
                        ).Distinct();
            int iIndex=0;
            foreach (var item in SectionInfo)
            {
                TSECTIONINFO SecInfo = new TSECTIONINFO();
                SecInfo.SecIndx=iIndex;
                SecInfo.SectionName = item.sectionid;
                SecInfo.LineName = item.lineid;
                SecInfo.GroupNumber = iIndex;

                arr.Add(SecInfo);
            }
            return arr;
        }
        private void tsbtnPause_Click(object sender, EventArgs e)
        {
            tsbtnStart.Checked = false;
            tsbtnPause.Checked = true;
            tsbtnStop.Checked = false;
            //Thread.Sleep(2000);
            for (int i = 0; i < Globle.arrStation.Count; i++)
            {
                if (((TSTATIONINFO)Globle.arrStation[i]).strDctInfo.OnLineStatus == "OK")
                    ((TSTATIONINFO)Globle.arrStation[i]).strDctConfigInfo.SOCKET.Close();
            }
            this._subThreadSockConn.Pause();
            this._subThreadGW28Run.Pause();
            //tCheckStationStatus.Abort();
            this.lvTerminalInfo.Columns.Clear();
            this.lvTerminalInfo.Items.Clear();
        }

        private void lvTerminalInfo_DrawColumnHeader(object sender, DrawListViewColumnHeaderEventArgs e)
        {
            try
            {
                int tColumnCount;
                Rectangle tRect = new Rectangle();
                Point tPoint = new Point();

                Font tFont = new Font("宋体", 9, FontStyle.Regular);
                //SolidBrush tBackBrush = new SolidBrush(System.Drawing.Color.FromArgb(((int)(((byte)(56)))), ((int)(((byte)(56)))), ((int)(((byte)(56))))));
                //SolidBrush tBackBrush = new SolidBrush(e.BackColor);
                SolidBrush tBackBrush = new SolidBrush(System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(192))))));
                SolidBrush tFtontBrush;

                //tFtontBrush = new SolidBrush(System.Drawing.SystemColors.GradientActiveCaption);
                tFtontBrush = new SolidBrush(Color.Black);

                if (lvTerminalInfo.Columns.Count == 0)
                    return;


                tColumnCount = lvTerminalInfo.Columns.Count;
                tRect.Y = 0;
                tRect.Height = e.Bounds.Height - 1;
                tPoint.Y = 3;
                for (int i = 0; i < tColumnCount; i++)
                {
                    if (i == 0)
                    {
                        tRect.X = 0;
                        tRect.Width = lvTerminalInfo.Columns[i].Width;
                    }
                    else
                    {
                        tRect.X += tRect.Width;
                        tRect.X += 1;
                        tRect.Width = lvTerminalInfo.Columns[i].Width - 1;
                    }

                    e.Graphics.FillRectangle(tBackBrush, tRect);
                    //tPoint.X = tRect.X + 3;
                    tPoint.X = tRect.X + 20;
                    e.Graphics.DrawString(lvTerminalInfo.Columns[i].Text, tFont, tFtontBrush, tPoint);

                    Rectangle rcA = e.Bounds;
                    rcA.X = rcA.X + 1;
                    rcA.Y = rcA.Y + 1;
                    rcA.Width = rcA.Width - 2;
                    rcA.Height = rcA.Height - 2;
                    e.Graphics.DrawRectangle(new Pen(Color.Goldenrod), rcA);

                    Bitmap bt = SFC_Tools.Properties.Resources.bitmap2;
                    Icon ic = Icon.FromHandle(bt.GetHicon());
                    Rectangle rc = new Rectangle();

                    rc = tRect;
                    rc.X = rc.X + 5;
                    rc.Y = rc.Y + 2;
                    rc.Width = 12;
                    rc.Height = 12;
                    //e.Graphics.DrawIcon(ic, rc);
                    e.Graphics.DrawImage(bt, new Point(rc.X, rc.Y));
                }
            }
            catch (Exception ex)
            { 
            
            }
        }

        private void lvTerminalInfo_DrawItem(object sender, DrawListViewItemEventArgs e)
        {
            //Point tPoint = new Point();
            //SolidBrush tFrontBrush = new SolidBrush(Color.Black);

            //Font tFont = new Font("宋体", 9, FontStyle.Bold);
            //tPoint.X = e.Bounds.X + 3;
            //tPoint.Y = e.Bounds.Y + 2;
            //if (e.Item.SubItems[0].Tag == "X")
            //    e.Graphics.FillRectangle(new SolidBrush(Color.Green), e.Bounds);
            //else
            //    e.Graphics.FillRectangle(new SolidBrush(Color.Red), e.Bounds);
            //e.Graphics.DrawString(e.Item.Text, tFont, tFrontBrush, tPoint);

            Point tPoint = new Point();
            SolidBrush tFrontBrush = new SolidBrush(Color.Black);

            Font tFont = new Font("宋体", 9, FontStyle.Bold);
            tPoint.X = e.Bounds.X + 3;
            tPoint.Y = e.Bounds.Y + 2;
            Rectangle rc = e.Bounds;
            int i = e.ItemIndex;
            if (i >= lvTerminalInfo.Columns.Count)
            {
                i = lvTerminalInfo.Columns.Count - 1;
            }
            if (lvTerminalInfo.Columns[i] != null)
                rc.Width = this.lvTerminalInfo.Columns[i].Width;
            string x = e.Item.Text;
            if (e.Item.Tag != null)
                if (e.Item.Tag.ToString() == "OK")
                    e.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(0, 255, 0)), rc);
                else if (e.Item.Tag.ToString() == "NG")
                    e.Graphics.FillRectangle(new SolidBrush(Color.Red), rc);
            //else
            //   e.Graphics.FillRectangle(new SolidBrush(Color.Red), rc);

            e.Graphics.DrawString(e.Item.Text, tFont, tFrontBrush, tPoint);

        }

        private void lvTerminalInfo_DrawSubItem(object sender, DrawListViewSubItemEventArgs e)
        {
            Point tPoint = new Point();
            SolidBrush tFrontBrush = new SolidBrush(Color.Black);
            
            Font tFont = new Font("宋体", 9, FontStyle.Bold);
            tPoint.X = e.Bounds.X + 3;
            tPoint.Y = e.Bounds.Y + 2;
            Rectangle rc=e.Bounds;
            int i = e.ItemIndex;
            if (i >= lvTerminalInfo.Columns.Count)
            {
                i = lvTerminalInfo.Columns.Count-1;
            }
            if(lvTerminalInfo.Columns[i]!=null)
                rc.Width=this.lvTerminalInfo.Columns[i].Width;

            if (e.SubItem.Tag != null)
                if (e.SubItem.Tag.ToString() == "OK")
                    e.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(0,255,0)), rc);
                else if (e.SubItem.Tag.ToString() == "NG")
                    e.Graphics.FillRectangle(new SolidBrush(Color.Red), rc);
            //else
            //   e.Graphics.FillRectangle(new SolidBrush(Color.Red), rc);

            e.Graphics.DrawString(e.SubItem.Text, tFont, tFrontBrush, tPoint);
        }

        private void tsbtnStop_Click(object sender, EventArgs e)
        {
            //lvTerminalInfo.Items[1].UseItemStyleForSubItems = false;
            //lvTerminalInfo.Items[1].SubItems[0].BackColor = Color.Green;
            //MessageBox.Show(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            
            Thread t = new Thread(new ThreadStart(testThreadProc));
            t.Start();
        }

        private void testThreadProc()
        {
            int i = 0;
            while(i<10000)
            {
                ShowMessageToView(i+"Hello the damned world！", "0", true);
                i++;
                ShowMessageToView(i+"2Hello the damned world！", "0", false);
                i++;
                Thread.Sleep(10);
            }
            
        }
    }
   
    public class CSockConnThread
    {
        private bool StartConnThread =false;
        private const int ICMP_ECHO=8;
        private Thread checkSockthread;

        public delegate void MyMessageEventHandler(object sender, MyMessageEventArgs e);
        public event MyMessageEventHandler MessageSend;
        public void OnMyMessageSend(object sender, MyMessageEventArgs e)
        {
            if (MessageSend != null)
                this.MessageSend(sender, e);
        }
        public CSockConnThread()
        {
        }
        private void run()
        {
            while (StartConnThread)
            {
                if (StartConnThread == true)
                {
                    for (int i = 0; i < Globle.arrStation.Count; i++)
                    {
                        if (((TSTATIONINFO)Globle.arrStation[i]).strDctConfigInfo.stIP != "0.0.0.0" &&
                           ((TSTATIONINFO)Globle.arrStation[i]).strDctConfigInfo.Port != "0" &&
                           ((TSTATIONINFO)Globle.arrStation[i]).strDctConfigInfo.Port != "")
                        {
                            if (((TSTATIONINFO)Globle.arrStation[i]).strDctInfo.OnLineStatus == "NG")
                            {
                                #region off line station check status
                                IPEndPoint ipepServer = new IPEndPoint(IPAddress.Parse(((TSTATIONINFO)Globle.arrStation[i]).strDctConfigInfo.stIP), 0);
                                if (PingCheck(ipepServer))
                                {
                                    IPEndPoint ipe = new IPEndPoint(IPAddress.Parse(((TSTATIONINFO)Globle.arrStation[i]).strDctConfigInfo.stIP), Convert.ToInt32(((TSTATIONINFO)Globle.arrStation[i]).strDctConfigInfo.Port));
                                    Socket s = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                                    ((TSTATIONINFO)Globle.arrStation[i]).strDctConfigInfo.SOCKET = s;
                                    try
                                    {
                                        s.Connect((EndPoint)ipe);
                                        if (s.Connected)
                                        {
                                            //dr["CheckFlag"] = "Y"; //Y,N
                                            ((TSTATIONINFO)Globle.arrStation[i]).strDctInfo.CheckFlag = true;
                                            ((TSTATIONINFO)Globle.arrStation[i]).strDctInfo.OnLineStatus = "OK";
                                            this.OnMyMessageSend(this, new MyMessageEventArgs(new StationStatus() { ID = i, Status = true }));
                                            //dr["OnLineStatus"] = "OK";//OK,NG                                        
                                        }
                                        else
                                        {
                                            ((TSTATIONINFO)Globle.arrStation[i]).strDctInfo.OnLineStatus = "NG";
                                            ((TSTATIONINFO)Globle.arrStation[i]).strDctConfigInfo.SOCKET.Close();
                                        }
                                    }
                                    catch (Exception ex)
                                    {
                                        ((TSTATIONINFO)Globle.arrStation[i]).strDctInfo.OnLineStatus = "NG";
                                        if (((TSTATIONINFO)Globle.arrStation[i]).strDctConfigInfo.SOCKET != null)
                                        {
                                            ((TSTATIONINFO)Globle.arrStation[i]).strDctConfigInfo.SOCKET.Close();
                                            ((TSTATIONINFO)Globle.arrStation[i]).strDctConfigInfo.SOCKET = null;
                                        }
                                    }
                                }
                                else
                                    if (((TSTATIONINFO)Globle.arrStation[i]).strDctConfigInfo.SOCKET != null)
                                    {
                                        ((TSTATIONINFO)Globle.arrStation[i]).strDctConfigInfo.SOCKET.Close();
                                    }
                                #endregion
                            }
                            else
                            {
                                //to the stations are online already
                                try
                                {
                                    byte[] btCheckChr = new byte[2];
                                    btCheckChr[0] = 0x1b;
                                    btCheckChr[1] = 0x10;
                                    ((TSTATIONINFO)Globle.arrStation[i]).strDctConfigInfo.SOCKET.Send(btCheckChr, btCheckChr.Length, 0);
                                    if (((TSTATIONINFO)Globle.arrStation[i]).strDctInfo.CheckFlag == true)
                                    {
                                        ((TSTATIONINFO)Globle.arrStation[i]).strDctInfo.CheckFlag = false;
                                    }
                                    else
                                    {
                                        ((TSTATIONINFO)Globle.arrStation[i]).strDctConfigInfo.SOCKET.Close();
                                        ((TSTATIONINFO)Globle.arrStation[i]).strDctInfo.OnLineStatus = "NG";
                                        this.OnMyMessageSend(this, new MyMessageEventArgs(new StationStatus() { ID=i,Status=false}));
                                    }

                                }
                                catch (Exception ex)
                                {
                                    ((TSTATIONINFO)Globle.arrStation[i]).strDctInfo.OnLineStatus = "NG";
                                    this.OnMyMessageSend(this, new MyMessageEventArgs(new StationStatus() { ID = i, Status = false }));
                                    ((TSTATIONINFO)Globle.arrStation[i]).strDctConfigInfo.SOCKET.Close();
                                }

                            }
                        }
                    }
                }
                else
                {
                    break;
                }
                //this.OnMyMessageSend(this, new MyMessageEventArgs(Globle.arrStation));
                Thread.Sleep(2500);
            }
        }

        /// <summary>
        /// used to check the client connect status 
        /// </summary>
        /// <param name="s">client socket</param>

        private bool IsSocketConnected(Socket s)
        {
            bool bBlockingStatus = s.Blocking;
            try
            {
                byte[] tmp = new byte[1];
                //byte[] tmp=new byte[2];
                //tmp[0] = 0x1b;
                //tmp[1] = 0x0d;
                //byte[] btHead = new byte[5];
                //btHead = System.Text.Encoding.UTF8.GetBytes(0x1b+"000");
                //byte[] x = new byte[] { 0x01 };
                //x.CopyTo(btHead,4);
                //byte[] tmpA = System.Text.Encoding.UTF8.GetBytes("HELLO THE DAMNED WORLD!");
                //s.Blocking=false;
                //byte[] btFull = new byte[btHead.Length + tmpA.Length];
                //btHead.CopyTo(btFull,0);
                //tmpA.CopyTo(btFull, btHead.Length);
                //int tt = s.Send(btFull, btFull.Length, 0);

                int tt = s.Send(tmp, 0, 0);
                //Thread.Sleep(10);
                //client.Poll(10,SelectMode.SelectRead)
                if (s.Poll(-1, SelectMode.SelectRead))
                {
                    if (s.Receive(tmp) > 0)
                        return true;
                    else
                        return false;
                }
                else
                    return false;
            }
            catch(SocketException ex)
            {
                if (ex.NativeErrorCode.Equals(10035))
                    return true;
                else
                    return false;
            }
            catch(Exception e)
            {
                string xx = e.Message.ToString();
                return false;
            }
            finally
            {
                s.Blocking=bBlockingStatus;
            }
            //return true;
        }

        public void Pause()
        {
            this.StartConnThread = false;
            for (int i = 0; i < Globle.arrStation.Count; i++)
            {
                ((TSTATIONINFO)Globle.arrStation[i]).strDctConfigInfo.SOCKET = null;
            }
            Thread.Sleep(2000);
            this.checkSockthread.Abort();
        }
        public void StartProcess()
        {
            this.StartConnThread = true;
            //run();
            checkSockthread = new Thread(new ThreadStart(run));
            checkSockthread.Start();
        }
        private bool PingCheck(IPEndPoint ipe)
        {
            Socket rawSocket;
            string str;
            int nRet;
            long dwTimeSet;
            string cTTL="xx";
            rawSocket = new Socket(AddressFamily.InterNetwork,SocketType.Raw,ProtocolType.Icmp);
            //設置超時相應時間為1S
            rawSocket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.SendTimeout, 1000);
            int t=SendEchoRequest(rawSocket,ipe);
            //LogHelper.WriteLog("send data:" + "[" + ipe.Address+":"+t.ToString()+"]");
            nRet=WaitForEchoReply(rawSocket);
            //LogHelper.WriteLog("return data:" + "[" + ipe.Address + ":" + nRet.ToString() + "]");
            if (nRet==0)
            {
                LogHelper.WriteError("send data:" + "[" + ipe.Address + ":" + t.ToString() + "]");
                LogHelper.WriteError("return data:" + "[" + ipe.Address + ":" + nRet.ToString() + "]");
                rawSocket.Close();
                return false;
            }
            else
            {
                RecvEchoReply(rawSocket, ipe, ref cTTL);
                rawSocket.Close();
                return true;
            }
            
        }
        private int RecvEchoReply(Socket s,IPEndPoint ipe, ref string cTTL)
        { 
            Byte [] ReceiveBuffer = new Byte[256];
            int nBytes = 0;
            EndPoint pe=(EndPoint)ipe;
            
            nBytes = s.ReceiveFrom(ReceiveBuffer,256,0, ref pe);
            cTTL = ReceiveBuffer.ToString();
            return nBytes;
        }
        private int SendEchoRequest(Socket s,IPEndPoint ipe)
        { 
             int PacketSize = 0;
             int dwStart = 0;
             IcmpPacket packet = new IcmpPacket();

             // 构建要发送的包
             packet.Type = ICMP_ECHO; //8
             packet.SubCode = 0;
             packet.CheckSum = UInt16.Parse("0");
             packet.Identifier   = UInt16.Parse("1");
             packet.SequenceNumber = UInt16.Parse("1");
             int PingData = 0; // sizeof(IcmpPacket) - 8;
             packet.Data = new Byte[PingData];


             //Variable to hold the total Packet size
             PacketSize = PingData + 8;
             Byte[] icmp_pkt_buffer = new Byte[PacketSize];
             Int32 Index = 0;
             //Call a Method Serialize which counts
             //The total number of Bytes in the Packet
             Index = Serialize(
                packet,
                icmp_pkt_buffer,
                PacketSize,
                PingData);
             //Error in Packet Size
             if (Index == -1)
             {
                 return 0;
             }
             Double double_length = Convert.ToDouble(Index);
             Double dtemp = Math.Ceiling(double_length / 2);
             int cksum_buffer_length = Convert.ToInt32(dtemp);
             //Create a Byte Array
             UInt16[] cksum_buffer = new UInt16[cksum_buffer_length];
             //Code to initialize the Uint16 array
             int icmp_header_buffer_index = 0;
             for (int i = 0; i < cksum_buffer_length; i++)
             {
                 cksum_buffer[i] = BitConverter.ToUInt16(icmp_pkt_buffer, icmp_header_buffer_index);
                 icmp_header_buffer_index += 2;
             }
             //Call a method which will return a checksum
             UInt16 u_cksum = checksum(cksum_buffer, cksum_buffer_length);
             //Save the checksum to the Packet
             packet.CheckSum = u_cksum;

             // Now that we have the checksum, serialize the packet again
             Byte[] sendbuf = new Byte[PacketSize];
             //again check the packet size
             Index = Serialize(
                packet,
                sendbuf,
                PacketSize,
                PingData);
             //if there is a error report it
             if (Index == -1)
             {
                 return 0;
             }

             dwStart = System.Environment.TickCount; // Start timing
             return (s.SendTo(sendbuf, PacketSize, 0, ipe));
        }
        private int WaitForEchoReply(Socket s)
        {
            ArrayList arr = new ArrayList();
            arr.Add(s);
            Thread.Sleep(100);
            Socket.Select(arr, null, null, 1500);
            return arr.Count;
        }
        private Int32 Serialize(IcmpPacket packet, Byte[] Buffer,
            Int32 PacketSize, Int32 PingData)
        {
            Int32 cbReturn = 0;
            // serialize the struct into the array
            int Index = 0;

            Byte[] b_type = new Byte[1];
            b_type[0] = (packet.Type);

            Byte[] b_code = new Byte[1];
            b_code[0] = (packet.SubCode);

            Byte[] b_cksum = BitConverter.GetBytes(packet.CheckSum);
            Byte[] b_id = BitConverter.GetBytes(packet.Identifier);
            Byte[] b_seq = BitConverter.GetBytes(packet.SequenceNumber);

            Array.Copy(b_type, 0, Buffer, Index, b_type.Length);
            Index += b_type.Length;

            Array.Copy(b_code, 0, Buffer, Index, b_code.Length);
            Index += b_code.Length;

            Array.Copy(b_cksum, 0, Buffer, Index, b_cksum.Length);
            Index += b_cksum.Length;

            Array.Copy(b_id, 0, Buffer, Index, b_id.Length);
            Index += b_id.Length;

            Array.Copy(b_seq, 0, Buffer, Index, b_seq.Length);
            Index += b_seq.Length;

            // copy the data
            Array.Copy(packet.Data, 0, Buffer, Index, PingData);
            Index += PingData;
            if (Index != PacketSize/* sizeof(IcmpPacket) */)
            {
                cbReturn = -1;
                return cbReturn;
            }

            cbReturn = Index;
            return cbReturn;
        }
        private UInt16 checksum(UInt16[] buffer, int size)
        {
            Int32 cksum = 0;
            int counter;
            counter = 0;

            while (size > 0)
            {
                UInt16 val = buffer[counter];
                cksum += Convert.ToInt32(buffer[counter]);
                counter += 1;
                size -= 1;
            }
            cksum = (cksum >> 16) + (cksum & 0xffff);
            cksum += (cksum >> 16);
            return (UInt16)(~cksum);
        }
    }

    public class CGW28RunThread
    {
        private bool StartConnThread = false;
       
        private Thread subGW28Thread;

        #region send message to main thread
        public delegate void MyMessageEventHandler(object sender, MyMessageEventArgs e);
        public event MyMessageEventHandler MessageSend;
        public void OnMyMessageSend(object sender, MyMessageEventArgs e)
        {
            if (MessageSend != null)
                this.MessageSend(sender, e);
        }
        #endregion

        public CGW28RunThread()
        {
           
        }
        public void Run()
        {
            while (true)
            {
                try
                {
                    for (int i = 0; i < Globle.arrStation.Count; i++)
                    {
                        GW28Work(i);
                    }
                    Thread.Sleep(200);
                }
                catch (Exception ex)
                {
                    LogHelper.WriteError(ex.Message);
                }
            }
        }

        public void StartProcess()
        {
            subGW28Thread = new Thread(new ThreadStart(Run));
            subGW28Thread.Start();
        }
        public void Pause()
        {
            subGW28Thread.Abort();
        }

        private void GW28Work(int inStnIndx)
        {
            StationStatus ss = new StationStatus();
            ss.ID = inStnIndx;
            if (((TSTATIONINFO)Globle.arrStation[inStnIndx]).strDctInfo.OnLineStatus == "OK")
            {
                ss.Status = true;
                //OnMyMessageSend(this, new MyMessageEventArgs(ss));
                ReceiveFromGW28(inStnIndx);
                SendDataToGW28(inStnIndx);
            }
            else
            { 
                ss.Status=false;
                //OnMyMessageSend(this, new MyMessageEventArgs(ss));
            }
        }

        private void ReceiveFromGW28(int inStnIndx)
        { 
             try
            {
                byte[] chReBuff = new byte[128];
                ((TSTATIONINFO)Globle.arrStation[inStnIndx]).strDctConfigInfo.SOCKET.Receive(chReBuff, 128, 0);
                string sReData = System.Text.Encoding.Default.GetString(chReBuff);
                //string sReData = chReBuff.ToString();
                int iPos = sReData.IndexOf("\r");
                while (iPos > 0)
                {
                    if (string.IsNullOrEmpty(sReData))
                    {
                        break;
                    }
                    string Socketstr = sReData.Substring(0, iPos);
                    #region switch case
                    switch (((TSTATIONINFO)Globle.arrStation[inStnIndx]).strDctConfigInfo.inInType)
                    {
                        case 1://recieve from K/B
                            break;
                        case 2://com1
                            {
                                if (Socketstr.Length > 2)
                                {
                                    if (Socketstr[0] == 0x1b && Socketstr[1] == 0x07)
                                    {
                                        if (Socketstr[2] == 0x0a) //delete
                                            Socketstr = Socketstr.Substring(0, 3);
                                        else
                                            Socketstr = Socketstr.Substring(0, 2);
                                    }
                                    else
                                        Socketstr = string.Empty;
                                }
                                else
                                    Socketstr = string.Empty;
                                break;
                            }
                        case 3:////digital in FOR CMC
                            {
                                if (Socketstr.Length > 2)
                                {
                                    if (Socketstr[0] == 0x12)
                                    {
                                        if (Socketstr[1] == 0x01)
                                        {
                                            Socketstr = "01"; ///Open
                                        }
                                        if (Socketstr[1] == 0x00)
                                        {
                                            Socketstr = "00"; ///close
                                        }

                                    }
                                    else if (Socketstr[0] == 0x1b && Socketstr[1] == 0x0a)
                                    {
                                        Socketstr = Socketstr.Substring(0, 2);
                                    }
                                    else
                                        Socketstr = string.Empty;

                                }
                                else
                                    Socketstr = string.Empty;

                                break;
                            }
                        case 11:////com2 FOR GW28
                            {
                                if (Socketstr.Length > 2)
                                {
                                    if (Socketstr[0] == 0x1b && Socketstr[1] == 0x07)
                                    {
                                        if (Socketstr[2] == 0x0a)
                                            Socketstr = Socketstr.Substring(0, 3);
                                        else
                                            Socketstr = Socketstr.Substring(0, 2);

                                        Socketstr = SpanString(Socketstr);
                                        if (!string.IsNullOrEmpty(Socketstr))
                                        {
                                            while (!string.IsNullOrEmpty(((TSTATIONINFO)Globle.arrStation[inStnIndx]).strDctInfo.SendBuffer))
                                            {
                                                ((TSTATIONINFO)Globle.arrStation[inStnIndx]).strDctInfo.SendBuffer = Socketstr + 0x0d;
                                                ((TSTATIONINFO)Globle.arrStation[inStnIndx]).strDctInfo.inBuzz = 2;
                                                ((TSTATIONINFO)Globle.arrStation[inStnIndx]).strDctConfigInfo.inOutType = 14;
                                                SendDataToGW28(inStnIndx);
                                                break;
                                            }
                                        }

                                    }
                                    else
                                        Socketstr = string.Empty;
                                }
                                else
                                    Socketstr = string.Empty;

                                break;
                            }
                        default:
                            break;
                    }
                    #endregion

                    Socketstr = SpanString(Socketstr);
                    if (!string.IsNullOrEmpty(Socketstr))
                    {
                        while (!string.IsNullOrEmpty(Socketstr))
                        {
                            if (string.IsNullOrEmpty(((TSTATIONINFO)Globle.arrStation[inStnIndx]).strDctInfo.ReadBuffer))
                            {
                                ((TSTATIONINFO)Globle.arrStation[inStnIndx]).strDctInfo.ReadBuffer = Socketstr;
                                ((TSTATIONINFO)Globle.arrStation[inStnIndx]).ReMsg = "{\"" + Socketstr + "\"}";
                                //Show the message to UI interface
                                MDScanMessage msm = new MDScanMessage();
                                msm.bSendMsg=false;
                                msm.ScanMessage = ((TSTATIONINFO)Globle.arrStation[inStnIndx]).ReMsg;
                                this.OnMyMessageSend(this, new MyMessageEventArgs(msm));
                                Socketstr = string.Empty;
                                break;
                            }
                            else
                            {
                                Thread.Sleep(100);
                            }
                        }
                    }//end of if
                    sReData = sReData.Substring(sReData.Length-iPos-1);
                    iPos = sReData.IndexOf("\r");
                }
                ((TSTATIONINFO)Globle.arrStation[inStnIndx]).strDctInfo.CheckFlag = true;
            }
            catch (Exception ex)
            {
                LogHelper.WriteError("SendDataToGW28:" + inStnIndx +"\r\n"+ ex.ToString() + "\r\n" + ex.StackTrace.ToString());
            }
        }

        private void SendDataToGW28(int inStnIndx)
        {
            int nRet;
            if (!string.IsNullOrEmpty(((TSTATIONINFO)Globle.arrStation[inStnIndx]).strDctInfo.SendBuffer))
            {
                try
                {
                    //2 mean deal the message successfully
                    if (((TSTATIONINFO)Globle.arrStation[inStnIndx]).strDctInfo.inBuzz == 2)
                    {
                        switch (((TSTATIONINFO)Globle.arrStation[inStnIndx]).strDctConfigInfo.inOutType)
                        {
                            case 0:
                                break;
                            case 2:
                                {
                                    #region success02
                                    /*
                                        nRet=send(pItems->StnInfo[inStnIndx].strDctConfigInfo.Socket,
                                        "\x1b\x07"+pItems->StnInfo[inStnIndx].strDctInfo.SendBuffer+"O\x0d",
                                        pItems->StnInfo[inStnIndx].strDctInfo.SendBuffer.GetLength()+4,0);
                                     */
                                    byte[] btHead = new byte[2];
                                    btHead[0] = 0x1b;
                                    btHead[1] = 0x07;

                                    byte[] btTail = new byte[1];
                                    btTail[1] = 0x0d;

                                    byte[] btSend = System.Text.Encoding.UTF8.GetBytes(((TSTATIONINFO)Globle.arrStation[inStnIndx]).strDctInfo.SendBuffer + "O");

                                    byte[] SendBuf = new byte[btHead.Length + btTail.Length + btSend.Length];
                                    btHead.CopyTo(SendBuf, 0);
                                    btSend.CopyTo(SendBuf, btHead.Length);
                                    btTail.CopyTo(SendBuf, btHead.Length + btSend.Length);

                                    nRet = ((TSTATIONINFO)Globle.arrStation[inStnIndx]).strDctConfigInfo.SOCKET.Send(
                                        SendBuf, SendBuf.Length, 0);
                                    #endregion
                                    break;
                                }
                            case 5:
                                {
                                    #region success05
                                    /*
                                        nRet=send(pItems->StnInfo[inStnIndx].strDctConfigInfo.Socket,
                                        "\x1b\x0c\x02\x01\x01"+pItems->StnInfo[inStnIndx].strDctInfo.SendBuffer,
                                        pItems->StnInfo[inStnIndx].strDctInfo.SendBuffer.GetLength()+5,0);
                                    nRet=send(pItems->StnInfo[inStnIndx].strDctConfigInfo.Socket,
                                        "\x1b\x09\x00",3,0);
                                    ///////add for stopping track
                                    nRet=send(pItems->StnInfo[inStnIndx].strDctConfigInfo.Socket,
                                        "\x1b\x09\x01",3,0);
                                    Sleep(1000);
                                    nRet=send(pItems->StnInfo[inStnIndx].strDctConfigInfo.Socket,
                                        "\x1b\x09\x00",3,0);
                                    break;
                                     */

                                    #region send text data to CMC
                                    byte[] btSend = System.Text.Encoding.UTF8.GetBytes(((TSTATIONINFO)Globle.arrStation[inStnIndx]).strDctInfo.SendBuffer);

                                    byte[] btHead = new byte[5];
                                    btHead[0] = 0x1b;
                                    btHead[1] = 0x0c;
                                    btHead[2] = 0x02;
                                    btHead[3] = 0x01;
                                    btHead[4] = 0x01;

                                    byte[] SendBuf = new byte[btHead.Length + btSend.Length];
                                    btHead.CopyTo(SendBuf, 0);
                                    btSend.CopyTo(SendBuf, btHead.Length);

                                    nRet = ((TSTATIONINFO)Globle.arrStation[inStnIndx]).strDctConfigInfo.SOCKET.Send(
                                        SendBuf, SendBuf.Length, 0);
                                    #endregion

                                    byte[] btControlClose = new byte[3];
                                    btHead[0] = 0x1b;
                                    btHead[1] = 0x09;
                                    btHead[2] = 0x00;

                                    byte[] btControlOpen = new byte[3];
                                    btHead[0] = 0x1b;
                                    btHead[1] = 0x09;
                                    btHead[2] = 0x01;


                                    nRet = ((TSTATIONINFO)Globle.arrStation[inStnIndx]).strDctConfigInfo.SOCKET.Send(
                                       btControlClose, btControlClose.Length, 0);

                                    nRet = ((TSTATIONINFO)Globle.arrStation[inStnIndx]).strDctConfigInfo.SOCKET.Send(
                                      btControlOpen, btControlOpen.Length, 0);
                                    Thread.Sleep(1000);

                                    nRet = ((TSTATIONINFO)Globle.arrStation[inStnIndx]).strDctConfigInfo.SOCKET.Send(
                                      btControlClose, btControlClose.Length, 0);
                                    #endregion
                                    break;
                                }
                            case 7:
                                {
                                    #region success07
                                    /*
                                     nRet=send(pItems->StnInfo[inStnIndx].strDctConfigInfo.Socket,
                                    "\x1b\x0c\x02\x01\x01"+pItems->StnInfo[inStnIndx].strDctInfo.SendBuffer,
                                    pItems->StnInfo[inStnIndx].strDctInfo.SendBuffer.GetLength()+5,0);
                                     */
                                    byte[] btSend = System.Text.Encoding.UTF8.GetBytes(((TSTATIONINFO)Globle.arrStation[inStnIndx]).strDctInfo.SendBuffer);

                                    byte[] btHead = new byte[5];
                                    btHead[0] = 0x1b;
                                    btHead[1] = 0x0c;
                                    btHead[2] = 0x02;
                                    btHead[3] = 0x01;
                                    btHead[4] = 0x01;

                                    byte[] SendBuf = new byte[btHead.Length + btSend.Length];
                                    btHead.CopyTo(SendBuf, 0);
                                    btSend.CopyTo(SendBuf, btHead.Length);

                                    nRet = ((TSTATIONINFO)Globle.arrStation[inStnIndx]).strDctConfigInfo.SOCKET.Send(
                                        SendBuf, SendBuf.Length, 0);
                                    #endregion
                                    break;
                                }
                            case 11:
                                {
                                    #region success 11
                                    /**
                                        nRet=send(pItems->StnInfo[inStnIndx].strDctConfigInfo.Socket,
                                        "\x1b\x0c\x02\x01\x01"+pItems->StnInfo[inStnIndx].strDctInfo.SendBuffer,
                                         pItems->StnInfo[inStnIndx].strDctInfo.SendBuffer.GetLength()+5,0);
                                     */

                                    byte[] btSend = System.Text.Encoding.UTF8.GetBytes(((TSTATIONINFO)Globle.arrStation[inStnIndx]).strDctInfo.SendBuffer);

                                    byte[] btHead = new byte[5];
                                    btHead[0] = 0x1b;
                                    btHead[1] = 0x0c;
                                    btHead[2] = 0x02;
                                    btHead[3] = 0x01;
                                    btHead[4] = 0x01;

                                    byte[] SendBuf = new byte[btHead.Length + btSend.Length];
                                    btHead.CopyTo(SendBuf, 0);
                                    btSend.CopyTo(SendBuf, btHead.Length);

                                    nRet = ((TSTATIONINFO)Globle.arrStation[inStnIndx]).strDctConfigInfo.SOCKET.Send(
                                        SendBuf, SendBuf.Length, 0);

                                    #endregion
                                    break;
                                }
                            case 12:
                                {
                                    #region success12
                                    /*
                                        nRet=send(pItems->StnInfo[inStnIndx].strDctConfigInfo.Socket,
                                        "\x1b\x68"+pItems->StnInfo[inStnIndx].strDctInfo.SendBuffer+"O\x0d",
                                        pItems->StnInfo[inStnIndx].strDctInfo.SendBuffer.GetLength()+4,0);
                                   */
                                    byte[] btHead = new byte[2];
                                    btHead[0] = 0x1b;
                                    btHead[1] = 0x68;

                                    byte[] btTail = new byte[1];
                                    btTail[1] = 0x0d;

                                    byte[] btSend = System.Text.Encoding.UTF8.GetBytes(((TSTATIONINFO)Globle.arrStation[inStnIndx]).strDctInfo.SendBuffer + "O");

                                    byte[] SendBuf = new byte[btHead.Length + btTail.Length + btSend.Length];
                                    btHead.CopyTo(SendBuf, 0);
                                    btSend.CopyTo(SendBuf, btHead.Length);
                                    btTail.CopyTo(SendBuf, btHead.Length + btSend.Length);

                                    nRet = ((TSTATIONINFO)Globle.arrStation[inStnIndx]).strDctConfigInfo.SOCKET.Send(
                                        SendBuf, SendBuf.Length, 0);

                                    #endregion
                                    break;
                                }
                            default:
                                {
                                    #region success default
                                    /*
                                     nRet=send(pItems->StnInfo[inStnIndx].strDctConfigInfo.Socket,
                                    "\x1b\x0c\x02\x01\x01"+pItems->StnInfo[inStnIndx].strDctInfo.SendBuffer,
                                    pItems->StnInfo[inStnIndx].strDctInfo.SendBuffer.GetLength()+5,0);
                                     */

                                    byte[] btSend = System.Text.Encoding.UTF8.GetBytes(((TSTATIONINFO)Globle.arrStation[inStnIndx]).strDctInfo.SendBuffer);

                                    byte[] btHead = new byte[5];
                                    btHead[0] = 0x1b;
                                    btHead[1] = 0x0c;
                                    btHead[2] = 0x02;
                                    btHead[3] = 0x01;
                                    btHead[4] = 0x01;

                                    byte[] SendBuf = new byte[btHead.Length + btSend.Length];
                                    btHead.CopyTo(SendBuf, 0);
                                    btSend.CopyTo(SendBuf, btHead.Length);

                                    nRet = ((TSTATIONINFO)Globle.arrStation[inStnIndx]).strDctConfigInfo.SOCKET.Send(
                                        SendBuf, SendBuf.Length, 0);

                                    #endregion
                                    break;
                                }

                        }
                    }
                    else//Any Error happen
                    {
                        switch (((TSTATIONINFO)Globle.arrStation[inStnIndx]).strDctConfigInfo.inOutType)
                        {
                            case 0:
                                break;
                            case 2:
                                {
                                    #region failed 02
                                    /*
                                     nRet=send(pItems->StnInfo[inStnIndx].strDctConfigInfo.Socket,
                                    "\x1b\x07"+pItems->StnInfo[inStnIndx].strDctInfo.SendBuffer+"E\x0d",
                                    pItems->StnInfo[inStnIndx].strDctInfo.SendBuffer.GetLength()+4,0);
                                     */

                                    byte[] btHead = new byte[2];
                                    btHead[0] = 0x1b;
                                    btHead[1] = 0x07;

                                    byte[] btTail = new byte[1];
                                    btTail[1] = 0x0d;

                                    byte[] btSend = System.Text.Encoding.UTF8.GetBytes(((TSTATIONINFO)Globle.arrStation[inStnIndx]).strDctInfo.SendBuffer + "E");

                                    byte[] SendBuf = new byte[btHead.Length + btTail.Length + btSend.Length];
                                    btHead.CopyTo(SendBuf, 0);
                                    btSend.CopyTo(SendBuf, btHead.Length);
                                    btTail.CopyTo(SendBuf, btHead.Length + btSend.Length);

                                    nRet = ((TSTATIONINFO)Globle.arrStation[inStnIndx]).strDctConfigInfo.SOCKET.Send(
                                        SendBuf, SendBuf.Length, 0);
                                    #endregion
                                    break;
                                }
                            case 5:
                                {
                                    #region failed 05
                                    /*
                                     nRet=send(pItems->StnInfo[inStnIndx].strDctConfigInfo.Socket,
                                    "\x1b\x09\x00",3,0);
                              //add for stopping track
                                    nRet=send(pItems->StnInfo[inStnIndx].strDctConfigInfo.Socket,
                                    "\x1b\x0c\x02\x01\x03"+pItems->StnInfo[inStnIndx].strDctInfo.SendBuffer,
                                    pItems->StnInfo[inStnIndx].strDctInfo.SendBuffer.GetLength()+5,0);
                                     */
                                    byte[] btHead = new byte[3];
                                    btHead[0] = 0x1b;
                                    btHead[1] = 0x09;
                                    btHead[2] = 0x00;

                                    nRet = ((TSTATIONINFO)Globle.arrStation[inStnIndx]).strDctConfigInfo.SOCKET.Send(
                                       btHead, btHead.Length, 0);

                                    byte[] btSend = System.Text.Encoding.UTF8.GetBytes(((TSTATIONINFO)Globle.arrStation[inStnIndx]).strDctInfo.SendBuffer);

                                    btHead = new byte[5];
                                    btHead[0] = 0x1b;
                                    btHead[1] = 0x0c;
                                    btHead[2] = 0x02;
                                    btHead[3] = 0x01;
                                    btHead[4] = 0x03;

                                    byte[] SendBuf = new byte[btHead.Length + btSend.Length];
                                    btHead.CopyTo(SendBuf, 0);
                                    btSend.CopyTo(SendBuf, btHead.Length);

                                    nRet = ((TSTATIONINFO)Globle.arrStation[inStnIndx]).strDctConfigInfo.SOCKET.Send(
                                        SendBuf, SendBuf.Length, 0);
                                    #endregion

                                    break;
                                }
                            case 7:
                                {
                                    #region failed 07
                                    /*
                                        nRet=send(pItems->StnInfo[inStnIndx].strDctConfigInfo.Socket,
                                        "\x1b\x0c\x02\x01\x03"+pItems->StnInfo[inStnIndx].strDctInfo.SendBuffer,
                                        pItems->StnInfo[inStnIndx].strDctInfo.SendBuffer.GetLength()+5,0);
                                     */
                                    byte[] btSend = System.Text.Encoding.UTF8.GetBytes(((TSTATIONINFO)Globle.arrStation[inStnIndx]).strDctInfo.SendBuffer);

                                    byte[] btHead = new byte[5];
                                    btHead[0] = 0x1b;
                                    btHead[1] = 0x0c;
                                    btHead[2] = 0x02;
                                    btHead[3] = 0x01;
                                    btHead[4] = 0x03;

                                    byte[] SendBuf = new byte[btHead.Length + btSend.Length];
                                    btHead.CopyTo(SendBuf, 0);
                                    btSend.CopyTo(SendBuf, btHead.Length);

                                    nRet = ((TSTATIONINFO)Globle.arrStation[inStnIndx]).strDctConfigInfo.SOCKET.Send(
                                        SendBuf, SendBuf.Length, 0);

                                    #endregion
                                    break;
                                }
                            case 11:
                                {
                                    #region failed 11
                                    /*
                                      nRet=send(pItems->StnInfo[inStnIndx].strDctConfigInfo.Socket,
                                      "\x1b\x0c\x02\x01\x01"+pItems->StnInfo[inStnIndx].strDctInfo.SendBuffer,
                                      pItems->StnInfo[inStnIndx].strDctInfo.SendBuffer.GetLength()+5,0);
                                 
                                  nRet=send(pItems->StnInfo[inStnIndx].strDctConfigInfo.Socket,
                                      "\x1b\x09\x00",3,0);
                                  ///////add for stopping track
                                  nRet=send(pItems->StnInfo[inStnIndx].strDctConfigInfo.Socket,
                                      "\x1b\x09\x01",3,0);
                                  Sleep(3000);
                                  nRet=send(pItems->StnInfo[inStnIndx].strDctConfigInfo.Socket,
                                      "\x1b\x09\x00",3,0);
                                   */
                                    #region send text data to CMC
                                    byte[] btSend = System.Text.Encoding.UTF8.GetBytes(((TSTATIONINFO)Globle.arrStation[inStnIndx]).strDctInfo.SendBuffer);

                                    byte[] btHead = new byte[5];
                                    btHead[0] = 0x1b;
                                    btHead[1] = 0x0c;
                                    btHead[2] = 0x02;
                                    btHead[3] = 0x01;
                                    btHead[4] = 0x01;

                                    byte[] SendBuf = new byte[btHead.Length + btSend.Length];
                                    btHead.CopyTo(SendBuf, 0);
                                    btSend.CopyTo(SendBuf, btHead.Length);

                                    nRet = ((TSTATIONINFO)Globle.arrStation[inStnIndx]).strDctConfigInfo.SOCKET.Send(
                                        SendBuf, SendBuf.Length, 0);
                                    #endregion

                                    byte[] btControlA = new byte[3];
                                    btHead[0] = 0x1b;
                                    btHead[1] = 0x09;
                                    btHead[2] = 0x00;

                                    byte[] btControlB = new byte[3];
                                    btHead[0] = 0x1b;
                                    btHead[1] = 0x09;
                                    btHead[2] = 0x01;


                                    nRet = ((TSTATIONINFO)Globle.arrStation[inStnIndx]).strDctConfigInfo.SOCKET.Send(
                                       btControlA, btControlA.Length, 0);

                                    nRet = ((TSTATIONINFO)Globle.arrStation[inStnIndx]).strDctConfigInfo.SOCKET.Send(
                                      btControlB, btControlB.Length, 0);
                                    Thread.Sleep(3000);

                                    nRet = ((TSTATIONINFO)Globle.arrStation[inStnIndx]).strDctConfigInfo.SOCKET.Send(
                                      btControlA, btControlA.Length, 0);
                                    #endregion
                                    break;
                                }
                            case 12:
                                {
                                    #region failed 12
                                    /*
                                       nRet=send(pItems->StnInfo[inStnIndx].strDctConfigInfo.Socket,
                                       "\x1b\x68"+pItems->StnInfo[inStnIndx].strDctInfo.SendBuffer+"E\x0d",
                                       pItems->StnInfo[inStnIndx].strDctInfo.SendBuffer.GetLength()+4,0);
                                    */
                                    byte[] btHead = new byte[2];
                                    btHead[0] = 0x1b;
                                    btHead[1] = 0x68;

                                    byte[] btTail = new byte[1];
                                    btTail[1] = 0x0d;

                                    byte[] btSend = System.Text.Encoding.UTF8.GetBytes(((TSTATIONINFO)Globle.arrStation[inStnIndx]).strDctInfo.SendBuffer + "E");

                                    byte[] SendBuf = new byte[btHead.Length + btTail.Length + btSend.Length];
                                    btHead.CopyTo(SendBuf, 0);
                                    btSend.CopyTo(SendBuf, btHead.Length);
                                    btTail.CopyTo(SendBuf, btHead.Length + btSend.Length);

                                    nRet = ((TSTATIONINFO)Globle.arrStation[inStnIndx]).strDctConfigInfo.SOCKET.Send(
                                        SendBuf, SendBuf.Length, 0);
                                    #endregion
                                    break;
                                }
                            default:
                                {
                                    #region failed default
                                    /*
                                        nRet=send(pItems->StnInfo[inStnIndx].strDctConfigInfo.Socket,
                                        "\x1b\x0c\x02\x01\x03"+pItems->StnInfo[inStnIndx].strDctInfo.SendBuffer,
                                        pItems->StnInfo[inStnIndx].strDctInfo.SendBuffer.GetLength()+5,0);
                                     */

                                    byte[] btSend = System.Text.Encoding.UTF8.GetBytes(((TSTATIONINFO)Globle.arrStation[inStnIndx]).strDctInfo.SendBuffer);

                                    byte[] btHead = new byte[5];
                                    btHead[0] = 0x1b;
                                    btHead[1] = 0x0c;
                                    btHead[2] = 0x02;
                                    btHead[3] = 0x01;
                                    btHead[4] = 0x03;

                                    byte[] SendBuf = new byte[btHead.Length + btSend.Length];
                                    btHead.CopyTo(SendBuf, 0);
                                    btSend.CopyTo(SendBuf, btHead.Length);

                                    nRet = ((TSTATIONINFO)Globle.arrStation[inStnIndx]).strDctConfigInfo.SOCKET.Send(
                                        SendBuf, SendBuf.Length, 0);
                                    #endregion
                                    break;
                                }

                        }
                    }
                    ((TSTATIONINFO)Globle.arrStation[inStnIndx]).strDctInfo.SendBuffer = string.Empty;
                    ((TSTATIONINFO)Globle.arrStation[inStnIndx]).strDctInfo.inBuzz = 5;
                    //showmessage(((TSTATIONINFO)arrStations[inStnIndx]).strDctInfo.SendBuffer);
                }
                catch (Exception ex)
                { 
                    
                }
            }

        }

        private string SpanString(string sSource)
        {
            string sReturn = string.Empty;
            char ch;
            for (int i = 0; i < sSource.Length; i++)
            {
                ch = sSource[i];
                if ((ch >= 32) && (ch <= 126))
                {
                    sReturn = sReturn + ch;
                }              
            }
            return sReturn;
        }
    }

    public class CSMOThread
    {
        private bool bClose = false;
        private Thread subSMOThread;

        public void StartProcess()
        {
            subSMOThread = new Thread(new ThreadStart(WorkFlow));
            subSMOThread.Start();
        }

        public CSMOThread()
        {
            
        }
        public void WorkFlow()
        { 
            while(true)
            {
                if (!bClose)
                {
                    for (int i = 0; i < Globle.arrStation.Count; i++)
                    {
                        int InputVal=2;
                        if (!string.IsNullOrEmpty(((TSTATIONINFO)Globle.arrStation[i]).strDctInfo.SendBuffer))
                        {
                            string SecCode="EMP";
                            #region The logic of UNDO
                            if (((TSTATIONINFO)Globle.arrStation[i]).strDctInfo.SendBuffer == "UNDO")
                            {
                                ((TSTATIONINFO)Globle.arrStation[i]).strDctInfo.blUndo = true;
                                ((TSTATIONINFO)Globle.arrStation[i]).strDctInfo.LastW = "0";
                                ((TSTATIONINFO)Globle.arrStation[i]).strDctInfo.inLastW = 0;
                                ((TSTATIONINFO)Globle.arrStation[i]).strDctInfo.inBuzz = 2;
                                
                                switch (InputVal)
                                {
                                    case 1:
                                        {
                                            string sPrefix = "UNDO";
                                            sPrefix = sPrefix.PadRight(40);
                                            sPrefix = sPrefix + "UNDO OK";
                                            sPrefix = sPrefix.PadRight(20);

                                            ((TSTATIONINFO)Globle.arrStation[i]).strDctInfo.SendBuffer = string.Format("{0} INPUT:{1}?", sPrefix, SecCode);
                                            break;
                                        }
                                    case 2:
                                        {
                                            ((TSTATIONINFO)Globle.arrStation[i]).strDctInfo.SendBuffer = string.Format("{0}{1}{2} INPUT:{3}?0x0d", "UNDO OK", 0x0d, 0x0d, SecCode);
                                            break;
                                        }
                                    case 3:
                                        {
                                            ((TSTATIONINFO)Globle.arrStation[i]).strDctInfo.SendBuffer = string.Format("{0}\n{1}\n INPUT:{2}?\n", "UNDO", "UNDO OK", SecCode);
                                            break;
                                        }
                                    default:
                                        {
                                            ((TSTATIONINFO)Globle.arrStation[i]).strDctInfo.SendBuffer = string.Format("{0}{1}{2} INPUT:{3}?0x0d", "UNDO OK", 0x0d, 0x0d, SecCode);
                                            break;
                                        }
                                }
                                ((TSTATIONINFO)Globle.arrStation[i]).strDctInfo.ReadBuffer = string.Empty;
                                ((TSTATIONINFO)Globle.arrStation[i]).SeMsg = string.Format("{\"{0}\"}", ((TSTATIONINFO)Globle.arrStation[i]).strDctInfo.SendBuffer);
                                continue;
                            }
                            #endregion

                            if (((TSTATIONINFO)Globle.arrStation[i]).strDctInfo.blUndo == true)
                            {
                                #region No Work Type Config
                                if (((TSTATIONINFO)Globle.arrStation[i]).inStationType <= 0)
                                {
                                    ((TSTATIONINFO)Globle.arrStation[i]).strDctInfo.inBuzz = 5;
                                    switch (InputVal)
                                    {
                                        case 1:
                                            {
                                                ((TSTATIONINFO)Globle.arrStation[i]).strDctInfo.SendBuffer = string.Format("{NO Work Type!} {0} Please Config Work Type!", " ");
                                                break;
                                            }
                                        case 2:
                                            {
                                                ((TSTATIONINFO)Globle.arrStation[i]).strDctInfo.SendBuffer = string.Format("{NO Work Type!}{0}{0} Please Config Work Type!{0}", 0x0d);
                                                break;
                                            }
                                        case 3:
                                            {
                                                ((TSTATIONINFO)Globle.arrStation[i]).strDctInfo.SendBuffer = string.Format("{NO Work Type!}\n{0}\nPlease Config Work Type!\n", " ");
                                                break;
                                            }
                                        default:
                                            {
                                                ((TSTATIONINFO)Globle.arrStation[i]).strDctInfo.SendBuffer = string.Format("{NO Work Type!}{0}{0} Please Config Work Type!{0}", 0x0d);
                                                break;
                                            }
                                    }
                                    ((TSTATIONINFO)Globle.arrStation[i]).strDctInfo.ReadBuffer = string.Empty;
                                    ((TSTATIONINFO)Globle.arrStation[i]).SeMsg = string.Format("{\"{0}\"}", ((TSTATIONINFO)Globle.arrStation[i]).strDctInfo.SendBuffer);
                                    continue;
                                }
                                #endregion

                            }
                        }
                    }
                    
                }
                Thread.Sleep(100);
            }
        }

             
    }

    public class Globle
    {
        public static ArrayList arrStation=new ArrayList();
    }
}
