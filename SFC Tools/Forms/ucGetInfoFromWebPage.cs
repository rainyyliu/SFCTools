using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Collections;
using System.Net;
using System.Xml;
using System.Runtime.InteropServices;
using System.Globalization;
using SFC_Tools.Classes;

namespace SFC_Tools.Forms
{

    public partial class ucGetInfoFromWebPage : UserControl
    {
        private string[,] myCategory;
        //for cmm part
        private string[,] myCmmCategory = { { "00-0 Consulting", "0000001N" }, { "01-1 SAP Sustain", "0000000S" }, { "01-2 SCM Solution", "0000000V" },
            { "02-1 PLM", "00000002" },{ "03-1 A2A", "0000000P" },{ "03-2 B2B", "0000000D" },  { "04-1 Service", "0000000G" },
            { "04-2 QMS", "0000000C" },{ "04-3 MES-eFox", "00000012" },{ "04-4 L6 SFC", "0000000A" },  { "04-5 SQL BI", "0000000H" },
            { "04-6 WH APP", "00000016" },{ "04-7 CQ SFC Dev", "0000001X" },{ "05-01 eHR System", "00000006" },{ "05-02 IINC", "0000000R" },
            { "05-03 ServiceDesk","0000001W" },{ "05-04 eMold", "0000000F" },{ "05-05 eIMF", "0000001H" },{ "05-06 SAP BI", "00000018" },
            { "05-07 DDNP", "0000001C" },{ "05-08 ITSS Solution", "0000001U" },{ "05-09 VMI Solution", "0000000B" },{ "06-1 Network Admin", "0000000E" },
            { "06-2 DB Admin", "0000001A" },{ "06-3 Server Admin.", "0000001B" },{ "07-1 CQ OA", "0000001F" },{ "07-2 CQ SFC Onsite", "0000001G" },
            { "08-1 LH OA","0000000K" },{ "08-2 LH SFC", "00000013" },{ "08-3 116", "0000001Y" },{ "09-1 WH OA", "00000014" },
            { "09-2 WH Onsite", "00000015" },{ "09-3 WH Infrastructure", "0000001Q" },{ "10-1 System Solution", "0000000W" },{ "FireFly", "0000001V" } };
        //for ces part
        private string[,] myCesCategory = { { "00-0 Consulting", "0000001N" }, { "01-1 SAP Sustain", "0000000S" }, { "01-2 PLM", "00000002" },
                                          { "01-3 A2A", "0000000P" },{ "01-4 B2B", "0000000D" },{ "02-01 eHR System", "00000006" },{ "02-02 IINC", "0000000R" },
            { "02-03 ServiceDesk","0000001W" },{ "02-04 eMold", "0000000F" },{ "02-05 eIMF", "0000001H" },{ "02-06 SAP BI", "00000018" },
            { "02-07 DDNP", "0000001C" },{ "02-08 ITSS Solution", "0000001U" },{ "02-09 SCM Solution", "0000000V" },{ "02-10 VMI Solution", "0000000B" },              
            { "03-1 ESSN SFC L10", "00000012" },{ "03-2 ESSN SFC L6", "0000000A" },  { "03-3 SQL BI", "0000000H" },{ "03-4 Service", "0000000G" },{ "03-5 QMS", "0000000C" },
            { "03-6 EPBG SFC", "00000025" },{ "04-1 Network Admin", "0000000E" },{ "04-2 DB Admin", "0000001A" },{ "04-3 Server Admin", "0000001B" },
            { "04-4 TJ Infrastructure", "0000001L" },{ "04-5 TJW Infrastructure", "0000001Z" },{ "05-1 LH ESSN OA", "0000000K" },{ "05-2 LH ESSN SFC Onsite", "00000013" },
            { "05-3 LH EPBG OA","00000022" },{ "05-4 LH EPBG SFC Onsite", "00000023" },{ "05-5 LH 116", "00000024" },{ "06-1. TJ OA", "0000001J" },
            { "06-2. TJ SFC On site", "0000001K" },{ "06-3 TJ APP", "0000001M" },{ "06-4 TJ 116", "00000027" },{ "07-1 TJW OA", "00000021" },{ "07-2 TJW SFC", "0000001Y" }
            ,{ "07-3 TJW APP", "00000020" },{ "07-4 TJW SAP", "00000026" },{ "08-1 System Solution", "0000000W" }
             };
        //string url = "http://its.cmmsg.efoxconn.com/hd/hdCategoryProblemList.asp?pageMode=Browse&hdDomainID=00002&treeID=0000000A&inclDesc=1&rcInString=xAxxTxxBxxEx&ListType=group&ListOwn=False&Caption=null";
        //string url = "http://its.cmmsg.efoxconn.com/hd/hdCategoryProblemList.asp";
        private string urlLogin; //= "http://its.cmmsg.efoxconn.com/login1.asp";
        private string ticketUrl;
        private string strBaseUrl;
        private string strCategory;
        DataTable dtTicktList;
        int iCatLen = 35;
        TextOnProgressBar vSubWindow;
        public ucGetInfoFromWebPage()
        {
            InitializeComponent();
            //webBrowser1.Navigate("http://its.cmmsg.efoxconn.com/defaultA.asp");
            //btnGo_Click(this,null);
            AddColumns();
            initUrl();
            iniCaregory();
            //loginSystem();
            pbgGetTicketListProgress.BackColor = Color.LightGray;
            vSubWindow = new TextOnProgressBar();
            Font f = new Font(this.Font, FontStyle.Bold);
            vSubWindow.Font = f;
            vSubWindow.Text = "0%";
            vSubWindow.ForeColor = Color.White;
            vSubWindow.Control = pbgGetTicketListProgress;
        }
        private void iniCaregory()
        {
            cmbCategory.Items.Clear();
            iCatLen = myCategory.Length / 2;
            for (int i = 0; i < iCatLen; i++)
            {
                cmbCategory.Items.Add(myCategory[i,0]);
            }
            cmbCategory.SelectedIndex = 0;
            cmbGroup.SelectedIndex = 0;
        }
        //init coloumn
        private void AddColumns()
        {
            DataGridViewTextBoxColumn cloCT = new DataGridViewTextBoxColumn();
            cloCT.Name = "CATEGORY";
            cloCT.ReadOnly = true;
            cloCT.DataPropertyName = "CATEGORY";
            cloCT.Width = 70;

            DataGridViewTextBoxColumn cloID = new DataGridViewTextBoxColumn();
            cloID.Name = "Ticket#";
            cloID.ReadOnly = true;
            cloID.DataPropertyName = "TICKET_ID";
            cloID.Width = 70;
            DataGridViewTextBoxColumn cloType = new DataGridViewTextBoxColumn();
            cloType.Name = "Ticket Type";
            cloType.ReadOnly = true;
            cloType.DataPropertyName = "TICKET_TYPE";
            cloType.Width = 70;
            DataGridViewTextBoxColumn cloSite = new DataGridViewTextBoxColumn();
            cloSite.Name = "Site";
            cloSite.ReadOnly = true;
            cloSite.DataPropertyName = "TICKET_SITE";
            cloSite.Width = 50;
            //BU
            DataGridViewTextBoxColumn cloBu = new DataGridViewTextBoxColumn();
            cloBu.Name = "BU";
            cloBu.ReadOnly = true;
            cloBu.DataPropertyName = "TICKET_BU";
            cloBu.Width = 70;
            //cost code
            DataGridViewTextBoxColumn cloCostCode = new DataGridViewTextBoxColumn();
            cloCostCode.Name = "Cost Code";
            cloCostCode.ReadOnly = true;
            cloCostCode.DataPropertyName = "TICKET_COST";
            cloCostCode.Width = 70;
            //subject
            DataGridViewTextBoxColumn cloSubject = new DataGridViewTextBoxColumn();
            cloSubject.Name = "Subject";
            cloSubject.ReadOnly = true;
            cloSubject.DataPropertyName = "TICKET_SUBJECT";
            cloSubject.Width = 200;
            //Request By
            DataGridViewTextBoxColumn cloRequester = new DataGridViewTextBoxColumn();
            cloRequester.Name = "Request By";
            cloRequester.ReadOnly = true;
            cloRequester.DataPropertyName = "TICKET_REQUESTER";
            cloRequester.Width = 70;
            //approved date
            DataGridViewTextBoxColumn cloApprovedDt = new DataGridViewTextBoxColumn();
            cloApprovedDt.Name = "Approved Date";
            cloApprovedDt.ReadOnly = true;
            cloApprovedDt.DataPropertyName = "TICKET_APPROVEDT";
            cloApprovedDt.Width = 80;
            //Estimated Completion Date
            DataGridViewTextBoxColumn cloEndDt = new DataGridViewTextBoxColumn();
            cloEndDt.Name = "Estimated Completion Date";
            cloEndDt.ReadOnly = true;
            cloEndDt.DataPropertyName = "TICKET_ENDDT";
            cloEndDt.Width = 80;
            // Status 
            DataGridViewTextBoxColumn cloStatus = new DataGridViewTextBoxColumn();
            cloStatus.Name = " Status";
            cloStatus.ReadOnly = true;
            cloStatus.DataPropertyName = "TICKET_STATUS";
            cloStatus.Width = 80;
            //Estimated Work Hours(H)
            DataGridViewTextBoxColumn cloWorkHours = new DataGridViewTextBoxColumn();
            cloWorkHours.Name = " Estimated Work Hours(H)";
            cloWorkHours.ReadOnly = true;
            cloWorkHours.DataPropertyName = "TICKET_WORKHOURS";
            cloWorkHours.Width = 80;
            //Assign To
            DataGridViewTextBoxColumn cloAssignTo = new DataGridViewTextBoxColumn();
            cloAssignTo.Name = "Assign To";
            cloAssignTo.ReadOnly = true;
            cloAssignTo.DataPropertyName = "TICKET_ASSIGNTO";
            cloAssignTo.Width = 80;
            /*
            DataGridViewButtonColumn cloBtnTest = new DataGridViewButtonColumn();
            cloBtnTest.Name = "delete";
            cloBtnTest.Text = "DELETE";
            cloBtnTest.DataPropertyName = "TASK_DELETE";
            cloBtnTest.Width = 60;
            */
            this.dgvTaskListInfo.Columns.Clear();
            this.dgvTaskListInfo.Columns.Add(cloCT);
            this.dgvTaskListInfo.Columns.Add(cloID);
            this.dgvTaskListInfo.Columns.Add(cloType);
            this.dgvTaskListInfo.Columns.Add(cloSite);
            this.dgvTaskListInfo.Columns.Add(cloBu);
            this.dgvTaskListInfo.Columns.Add(cloCostCode);
            this.dgvTaskListInfo.Columns.Add(cloSubject);
            this.dgvTaskListInfo.Columns.Add(cloRequester);
            this.dgvTaskListInfo.Columns.Add(cloApprovedDt);
            this.dgvTaskListInfo.Columns.Add(cloEndDt);
            this.dgvTaskListInfo.Columns.Add(cloStatus);
            this.dgvTaskListInfo.Columns.Add(cloWorkHours);
            this.dgvTaskListInfo.Columns.Add(cloAssignTo);
            //this.dgvTaskListInfo.Columns.Add(cloBtnTest);
            btnGetData.Enabled = false;
            dgvTaskListInfo.AutoGenerateColumns = false;

            dgvTaskListInfo.AllowUserToDeleteRows = true;
        }

        private void initWebPage()
        {
            if (webBrowser1.Url.ToString() != "http://its.cmmsg.efoxconn.com/defaultA.asp")
                loginSystem();
            WebBrowser myWeb = new WebBrowser();
            HtmlElement ClickBtn = null;
            HtmlDocument doc = webBrowser1.Document;
            for (int i = 0; i < doc.All.Count; i++)
            {
                if (doc.All[i].TagName.ToUpper().Equals("INPUT"))
                {
                    switch (doc.All[i].Name)
                    {
                        case "USERNAME":
                            doc.All[i].InnerText = "yongyuliu";　　// 用户名 
                            break;
                        case "PASSWORD":
                            doc.All[i].InnerText = "Lyy618589";　　　　　　// 密码 
                            break;
                        case "bt_SUBMIT":
                            ClickBtn = doc.All[i];
                            break;
                    }
                }
            }
            ClickBtn.InvokeMember("Click");　　　　　　　　　　　　// 点击“登录”按钮 
            myWeb.Navigate("http://its.cmmsg.efoxconn.com/hd/hdCategoryProblemList.asp?pageMode=Browse&hdDomainID=00002&treeID=0000000A&inclDesc=1&rcInString=xAxxTxxBxxEx&ListType=group&ListOwn=False&Caption=null");
            btnGetData.Enabled = true;
            //btnGo.Enabled = false;
        }
        private void loginSystem()
        {
            webBrowser1.Update();
            webBrowser1.Navigate("http://its.cmmsg.efoxconn.com/defaultA.asp");
            while (webBrowser1.IsBusy)
            {
                Thread.Sleep(500);
            }
            webBrowser1.Update();
        }
        private void btnGo_Click(object sender, EventArgs e)
        {
            initWebPage();
        }

        private void btnGetData_Click(object sender, EventArgs e)
        {
            
            //MessageBox.Show(webBrowser1.Document.Url.ToString());
            dgvTaskListInfo.Rows.Clear();
            while (webBrowser1.IsBusy)
            {
                Thread.Sleep(100);
            }
            HtmlDocument NewDoc = webBrowser1.Document;

            HtmlElement elemColl = null;
            elemColl = NewDoc.GetElementById("Table6");
            String str = elemColl.InnerHtml;
            //MessageBox.Show(str);
            //获取table中行的集合
            HtmlElementCollection htmlTr = elemColl.GetElementsByTagName("TR");
            //解析每一行中的没个值
            foreach (HtmlElement tr in htmlTr)
            {
                HtmlElementCollection htmlTD = tr.GetElementsByTagName("TD");
                if (htmlTD.Count > 0)
                {
                    string sRowText = "";
                    ArrayList arrTicketItems=new ArrayList();
                    foreach (HtmlElement td in htmlTD)
                    {
                        sRowText = sRowText + "  " + td.InnerText;
                        arrTicketItems.Add(td.InnerText);
                        //MessageBox.Show(tr.OuterHtml);
                        //MessageBox.Show(td.OuterHtml);
                        //MessageBox.Show(td.OuterText);
                        //MessageBox.Show(td.GetAttribute("CLASS").ToString());
                        //if (((mshtml.HTMLTableCellClass)(td.DomElement)).className == "cTableColumnContentC")
                        // MessageBox.Show(tr.InnerHtml);
                    }
                   // MessageBox.Show(sRowText);
                    dgvTaskListInfo.Rows.Insert(0, arrTicketItems[0], arrTicketItems[1], arrTicketItems[2], arrTicketItems[3],
                        arrTicketItems[4], arrTicketItems[5], arrTicketItems[6], arrTicketItems[7], arrTicketItems[8], arrTicketItems[10], arrTicketItems[11], arrTicketItems[12]);
                }
               
            }
            lblTotal.Text = (dgvTaskListInfo.Rows.Count-1).ToString();
        }

        private void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
           // initWebPage();
        }

        private void btnTestTwo_Click(object sender, EventArgs e)
        {
            //ItemExchangeInMatrix xx = new ItemExchangeInMatrix();
            myBase64 myData = new myBase64();
            this.dgvTaskListInfo.DataSource = null;
            DataTable dtTemp = null;
            dtTicktList = this.InitDTTable();
            if (chkAllDue.Checked)
            {
                for (int i = 0; i < myCategory.Length / 2; i++)
                {
                    this.cmbCategory.Text = myCategory[i, 0].ToString();
                    setProgress(0);
                    initUrl();
                    setProgress(5);
                    CookieContainer cc = new CookieContainer();
                    Hashtable param = new Hashtable();
                    param.Add("USERHEIGHT", "900");
                    param.Add("USERWIDTH", "1440");
                    param.Add("PASSWORD", myData.Base64Decode("THl5NjE4NTg5"));
                    param.Add("USERNAME", myData.Base64Decode("eW9uZ3l1bGl1"));
                    string result = PostAndGetHTML(urlLogin, cc, param);
                    setProgress(30);
                    param.Clear();
                    setProgress(35);
                    dtTemp = GoToUrl(ticketUrl, cc, "table6", this.cmbCategory.Text);
                    /*if (i == 0)
                        dtTicktList = dtTemp;
                    else
                    {*/
                        object[] obj = new object[dtTicktList.Columns.Count];
                        for (int j = 0; j < dtTemp.Rows.Count; j++)
                        {
                            dtTemp.Rows[j].ItemArray.CopyTo(obj, 0);
                           if(CheckTicketDue(obj[9].ToString())!=0)
                            dtTicktList.Rows.Add(obj);
                        }
                    if (dtTicktList == null)
                    {
                        setProgress(100);
                        return;
                    }
                    setProgress(75);
                    this.dgvTaskListInfo.DataSource = dtTicktList;
                    this.lblTotal.Text = dtTicktList.Rows.Count.ToString();
                    setProgress(85);
                    //DataTable dt7 = GoToUrl("http://its.cesbg.efoxconn.com/hd/hdDomainList2.asp?hdDomainID=00002#", cc, "table1");
                    SetDataGridBackColor();
                    setProgress(100);
                }
            }
            else if(chkMesTickets.Checked)
            {
                string[,] sMesCatLists = { { "CMMSG", "04-3 MES-eFox" }, { "CMMSG", "04-4 L6 SFC" }, { "CMMSG", "04-6 WH APP" },
                 { "CESBG", "03-1 ESSN SFC L10" },{ "CESBG", "03-2 ESSN SFC L6" },{ "CESBG", "03-6 EPBG SFC" },{ "CESBG", "07-2 TJW SFC" }};
                for (int i = 0; i < 7; i++)
                {
                    this.cmbGroup.Text = sMesCatLists[i, 0].ToString();
                    this.cmbCategory.Text = sMesCatLists[i, 1].ToString();
                    setProgress(0);
                    initUrl();
                    setProgress(5);
                    CookieContainer cc = new CookieContainer();
                    Hashtable param = new Hashtable();
                    param.Add("USERHEIGHT", "900");
                    param.Add("USERWIDTH", "1440");
                    param.Add("PASSWORD", myData.Base64Decode("THl5NjE4NTg5"));
                    param.Add("USERNAME", myData.Base64Decode("eW9uZ3l1bGl1"));
                    string result = PostAndGetHTML(urlLogin, cc, param);
                    setProgress(30);
                    param.Clear();
                    setProgress(35);
                    dtTemp = GoToUrl(ticketUrl, cc, "table6", this.cmbCategory.Text);
        
                    object[] obj = new object[dtTicktList.Columns.Count];
                    for (int j = 0; j < dtTemp.Rows.Count; j++)
                    {
                        dtTemp.Rows[j].ItemArray.CopyTo(obj, 0);
                        //if(CheckTicketDue(obj[9].ToString())!=0)
                        dtTicktList.Rows.Add(obj);
                    }

                    if (dtTicktList == null)
                    {
                        setProgress(100);
                        return;
                    }
                    setProgress(75);
                    this.dgvTaskListInfo.DataSource = dtTicktList;
                    this.lblTotal.Text = dtTicktList.Rows.Count.ToString();
                    setProgress(85);
                    //DataTable dt7 = GoToUrl("http://its.cesbg.efoxconn.com/hd/hdDomainList2.asp?hdDomainID=00002#", cc, "table1");
                    SetDataGridBackColor();
                    setProgress(100);
                }
            }
            else
            {
                setProgress(0);
                initUrl();
                setProgress(5);
                CookieContainer cc = new CookieContainer();
                Hashtable param = new Hashtable();
                param.Add("USERHEIGHT", "900");
                param.Add("USERWIDTH", "1440");
                param.Add("PASSWORD", myData.Base64Decode("THl5NjE4NTg5"));
                param.Add("USERNAME", myData.Base64Decode("eW9uZ3l1bGl1"));
                string result = PostAndGetHTML(urlLogin, cc, param);
                setProgress(30);
                param.Clear();
                setProgress(35);
                dtTemp = GoToUrl(ticketUrl, cc, "table6", this.cmbCategory.Text);
                dtTicktList = dtTemp;
                if (dtTicktList == null)
                {
                    setProgress(100);
                    return;
                }
                setProgress(75);
                this.dgvTaskListInfo.DataSource = dtTicktList;
                this.lblTotal.Text = dtTicktList.Rows.Count.ToString();
                setProgress(85);
                //DataTable dt7 = GoToUrl("http://its.cesbg.efoxconn.com/hd/hdDomainList2.asp?hdDomainID=00002#", cc, "table1");
                SetDataGridBackColor();
                setProgress(100);
            }
        }
        private void setProgress(int iPos)
        {
            this.pbgGetTicketListProgress.Value = iPos;
            vSubWindow.Text = iPos + "%";
            //this.pbgGetTicketListProgress.Update();
            /*int percent = (int)(((double)pbgGetTicketListProgress.Value / (double)pbgGetTicketListProgress.Maximum) * 100);
            Graphics g = pbgGetTicketListProgress.CreateGraphics();
            g.DrawString("                          ",
                         new Font("Arial", (float)11.25, FontStyle.Italic),
                         Brushes.Red, new PointF(pbgGetTicketListProgress.Width / 2 - 20,
                         pbgGetTicketListProgress.Height / 2 - 7));
            Thread.Sleep(200);
            g.DrawString(percent.ToString() + "%",
                          new Font("Arial", (float)11.25, FontStyle.Italic),
                          Brushes.Red, new PointF(pbgGetTicketListProgress.Width / 2 - 20,
                          pbgGetTicketListProgress.Height / 2 - 7));*/
        
        }
        private int CheckTicketDue(string strDueDate)
        {   /*
             * bIsDue = 0       normal
             * bIsDue = 1       warning
             * bIsDue = 2       due
            */
            int bIsDue = 0;
            strDueDate = strDueDate.Replace("-", "");
            if (strDueDate.Trim().Length == 0)
                return 0;
            CultureInfo provider = CultureInfo.InvariantCulture;
            string strFormate = "yyyyMMdd";
            DateTime dtDueDate = DateTime.ParseExact(strDueDate, strFormate, provider);
            DateTime dtEnd = DateTime.Now;
            dtEnd = dtEnd.AddDays(3);
            TimeSpan ts = dtDueDate - dtEnd;
            if (ts.TotalHours <= 0)
            {
                if (ts.TotalDays < -3)
                {
                    bIsDue = 2;
                }
                else
                    bIsDue = 1;
            }
            else
            {
                bIsDue = 0;
            }
            return bIsDue;
        }
        private void SetDataGridBackColor()
        {
                for (int i = 0; i < dgvTaskListInfo.Rows.Count-1; i++)
                {
                    string strState = dgvTaskListInfo.Rows[i].Cells[10].Value.ToString().ToLower();
                    if (strState == "已受理 due")
                        dgvTaskListInfo.Rows[i].DefaultCellStyle.BackColor = Color.LightCoral;
                    else if (dgvTaskListInfo.Rows[i].Cells[9].Value.ToString().ToLower().Trim() != "")
                    {
                        string strDueDate = dgvTaskListInfo.Rows[i].Cells[9].Value.ToString().ToLower().Trim();
                        int iDueFlag=CheckTicketDue(strDueDate);

                        if(iDueFlag==2)
                            dgvTaskListInfo.Rows[i].DefaultCellStyle.BackColor = Color.LightCoral;
                        if (iDueFlag == 1)
                            dgvTaskListInfo.Rows[i].DefaultCellStyle.BackColor = Color.Yellow;
                        if (iDueFlag == 0)
                        {
                            if (i % 2 == 0)
                                dgvTaskListInfo.Rows[i].DefaultCellStyle.BackColor = Color.LightGray;
                        }
                    }
                    else
                    {
                        if (i % 2 == 0)
                            dgvTaskListInfo.Rows[i].DefaultCellStyle.BackColor = Color.LightGray;
                    }
                }
        }
        private DataTable GoToUrl(string url, CookieContainer cc, string tableID, string sCartegory)
        {
            try
            {
                string result = "";
                Uri myUri = new Uri(url);
                setProgress(40);
                // Create a 'HttpWebRequest' object for the specified url. 
                HttpWebRequest myHttpWebRequest = (HttpWebRequest)WebRequest.Create(myUri);
                myHttpWebRequest.CookieContainer = cc;
                setProgress(45);
                // Send the request and wait for response.
                HttpWebResponse myHttpWebResponse = (HttpWebResponse)myHttpWebRequest.GetResponse();
                setProgress(50);
                /*if (myHttpWebResponse.StatusCode == HttpStatusCode.OK)
                     result="\nRequest succeeded and the requested information is in the response ,Description : {0}"+
                                        myHttpWebResponse.StatusDescription;
                if (myUri.Equals(myHttpWebResponse.ResponseUri))
                    result=result+"\nThe Request Uri was not redirected by the server";
                else
                    result=result+"\nThe Request Uri was redirected to"+myHttpWebResponse.ResponseUri;*/
                // Release resources of response object.
                System.IO.Stream stream = myHttpWebResponse.GetResponseStream();
                setProgress(55);
                System.Text.Encoding en = System.Text.Encoding.UTF8;//GetEncoding.; //("x-mac-chinesetrad");
                result = new System.IO.StreamReader(stream, en).ReadToEnd();

                setProgress(60);
                result = GetTargetStr(result, tableID);
                setProgress(65);
                myHttpWebResponse.Close();
                DataTable TKList = InitDTTable();
                setProgress(70);
                TKList = ConvertToDT(TKList, result,sCartegory);
                return TKList;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        private DataTable InitDTTable()
        {
            DataTable dt = new DataTable("TicketList");
            DataColumn column;
            //ticket no
            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "CATEGORY";
            column.Caption = "CATEGORY";
            dt.Columns.Add(column);
            //ticket no
            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "TICKET_ID";
            column.Caption = "Ticket No";
            dt.Columns.Add(column);
            //Ticket Type
            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "TICKET_TYPE";
            column.Caption = "Ticket Type";
            dt.Columns.Add(column);
            //Site
            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "TICKET_SITE";
            column.Caption = "Site";
            dt.Columns.Add(column);
            //BU
            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "TICKET_BU";
            column.Caption = "BU";
            dt.Columns.Add(column);
            //Cost Code
            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "TICKET_COST";
            column.Caption = "Cost Code";
            dt.Columns.Add(column);
            //Subject
            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "TICKET_SUBJECT";
            column.Caption = "Subject";
            dt.Columns.Add(column);
            //Request By
            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "TICKET_REQUESTER";
            column.Caption = "Request By";
            dt.Columns.Add(column);
            //Approved Date
            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "TICKET_APPROVEDT";
            column.Caption = "Approved Date";
            dt.Columns.Add(column);
            //Estimated Completion Date
            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "TICKET_ENDDT";
            column.Caption = "Estimated Completion Date";
            dt.Columns.Add(column);
            //P
            /*column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "P";
            column.Caption = "P";
            dt.Columns.Add(column);*/
            //Status
            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "TICKET_STATUS";
            column.Caption = "Status";
            dt.Columns.Add(column);
            //Estimated Work Hours(H)
            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "TICKET_WORKHOURS";
            column.Caption = "Estimated Work Hours(H)";
            dt.Columns.Add(column);
            //Assign To
            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "TICKET_ASSIGNTO";
            column.Caption = "Assign To";
            dt.Columns.Add(column);
            /*
            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "TASK_DELETE";
            column.Caption = "TASK_DELETE";
            dt.Columns.Add(column);
            */
            return dt;
        }
        private string GetTargetStr(string soureHTML,string targetTableID)
        {
            string strTarget = "";
            int iStart = soureHTML.ToLower().IndexOf(targetTableID)+3;
            if (iStart > 2)
            {
                //get the begin position

                int iIndex = soureHTML.ToLower().IndexOf("<table");
                int iLen = iStart;
                int iMidEnd = iStart;
                int iNewStart=1;
                while (iIndex > 0)
                {
                    iNewStart = iIndex+1;
                    iLen = iStart - iNewStart;
                    iIndex = soureHTML.ToLower().IndexOf("<table", iNewStart, iLen);
                }
                //iStart = soureHTML.ToLower().LastIndexOf("<table",0,iStart);
                strTarget = soureHTML.Substring(iNewStart, soureHTML.Length-iNewStart);
                //get then end position
                int iEnd = iNewStart;
                int iLeft = 1;
                while (iStart > 0)
                {
                    int iPos = soureHTML.ToLower().IndexOf("<table", iEnd);
                    int iNewPos = soureHTML.ToLower().IndexOf("</table>", iEnd);
                    if (iPos > 0 && iPos < iNewPos)
                    {
                        //strTarget = strTarget.Substring(iPos+1);
                        iEnd = iPos+1;
                        iLeft++;
                    }
                    else
                    {
                        //strTarget = strTarget.Substring(iNewPos+1);
                        iLeft--;
                        iEnd = iNewPos+1;
                    }
                    if (iLeft <= 0)
                    {
                        break;
                    }
                }
                strTarget = soureHTML.Substring(iNewStart - 1, iEnd - iNewStart+9);//9 is the table's length
            }
            return strTarget;
        }
        
        private DataTable ConvertToDT(DataTable dt,string tableHTML,string sCartegory)
        {
            int ifirstData = tableHTML.ToLower().IndexOf("<td");
            if (ifirstData < 0)
                return dt;
            int lastDT = tableHTML.ToLower().LastIndexOf("</td>");
            int firstRow = tableHTML.ToLower().IndexOf("<tr")+3;
            int index = tableHTML.ToLower().IndexOf("<tr", firstRow) + 3;
            bool isUnknowErr = false;
            while (index < lastDT)
            {
                DataRow dr = dt.NewRow();
                for (int i = 1; i < dt.Columns.Count; i++)
                {
                    dr[0] = sCartegory;
                    string value = "";
                    int startTD = tableHTML.ToLower().IndexOf("<td", index) + 3;
                    if (startTD < 3)//no data found 
                    {
                        isUnknowErr = true;
                        break;
                    }
                    int endTD = tableHTML.ToLower().IndexOf("</td>",startTD);
                    if (endTD < 0)
                        break;
                    string tdStr = tableHTML.Substring(startTD,endTD-startTD);
                    tdStr = tdStr.Replace("&nbsp;", "").Replace("\t", "").Replace("\r", "");
                    string[] v = tdStr.Split('<','>');
                    for (int j = 0; j < v.Length;j++ )
                    {
                        j++;
                        if (v[j] != "")
                        {  
                            value = v[j];
                            break;
                        }
                    }
                    dr[i] = value.Trim();
                    index = endTD;
                }
               // dr[13] = "FUCK";
                if (isUnknowErr)
                    break;
                dt.Rows.Add(dr);
                
            }
            return dt;
        }

        private string PostAndGetHTML(string targetURL,CookieContainer cc,Hashtable param)
        {
            int iOdPos = this.pbgGetTicketListProgress.Value;
            string sfromData = "";
            foreach (DictionaryEntry de in param)
            {
                sfromData += de.Key.ToString() + "=" + de.Value.ToString()+"&";
            }
            //remove the last "&"
            if (sfromData.Length > 0)
            {
                sfromData = sfromData.Substring(0,sfromData.Length-1); 
            }
            setProgress(iOdPos+5);
            ASCIIEncoding encoding = new ASCIIEncoding();
            byte[] data = encoding.GetBytes(sfromData);
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(targetURL);
            setProgress(iOdPos + 10);
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = data.Length;
            request.UserAgent = "Mozilla/4.0 (compatible MSIE 7.0;Windows NT 5.1;SV1;.NET CLR 2.0.1124)";
            System.IO.Stream  newStream= request.GetRequestStream();
            setProgress(iOdPos + 15);
            newStream.Write(data,0,data.Length);
            newStream.Close();
            setProgress(iOdPos + 20);
            request.CookieContainer = cc;
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            cc.Add(response.Cookies);
            System.IO.Stream stream = response.GetResponseStream();
            setProgress(iOdPos + 25);
            string sresult=new System.IO.StreamReader(stream,System.Text.Encoding.Default).ReadToEnd();
            return sresult;
        }
        
        private void dgvTaskListInfo_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
        
            Color color = dgvTaskListInfo.RowHeadersDefaultCellStyle.ForeColor;
            if (dgvTaskListInfo.Rows[e.RowIndex].Selected)
                color = dgvTaskListInfo.RowHeadersDefaultCellStyle.SelectionForeColor;
            else
                color = dgvTaskListInfo.RowHeadersDefaultCellStyle.ForeColor;
            using (SolidBrush b = new SolidBrush(color))
            {
                e.Graphics.DrawString((e.RowIndex + 1).ToString(), e.InheritedRowStyle.Font, b, e.RowBounds.Location.X + 20, e.RowBounds.Location.Y + 6);
            }
        }

        private void cmbGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            if (this.cmbGroup.Text == "CMMSG")
            {
                myCategory = this.myCmmCategory;
            }
            else
            {
                myCategory = myCesCategory;
            }
            int iCatLen = myCategory.Length / 2;
            cmbCategory.Items.Clear();
            for (int i = 0; i < iCatLen; i++)
            {
                this.cmbCategory.Items.Add(myCategory[i, 0]);
            }
            this.cmbCategory.SelectedIndex = 0;
            initUrl();
        }

        private void cmbCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            initUrl();
        }

        private void initUrl()
        {
            if (this.cmbGroup.Text == "CMMSG")
            {
                this.urlLogin = "http://its.cmmsg.efoxconn.com/login1.asp";
                strBaseUrl = "http://its.cmmsg.efoxconn.com/";
                myCategory = this.myCmmCategory;
            }
            else
            {
                this.urlLogin = "http://its.cesbg.efoxconn.com/login1.asp";
                strBaseUrl = "http://its.cesbg.efoxconn.com/";
                myCategory = myCesCategory;
            }
            int iCatLen = myCategory.Length / 2;

            for (int i = 0; i < iCatLen; i++)
            {
                if (this.cmbCategory.Text == myCategory[i, 0])
                {
                    strCategory = myCategory[i, 1];
                    break;
                }
            }
            ticketUrl = strBaseUrl + "hd/hdCategoryProblemList.asp?pageMode=Browse&hdDomainID=00002&treeID=" + strCategory + "&inclDesc=1&rcInString=xAxxTxxBxxEx&ListType=group&ListOwn=False&Caption=null";
        }

        private void dgvTaskListInfo_Sorted(object sender, EventArgs e)
        {
            SetDataGridBackColor();
        }

        private void pbgGetTicketListProgress_Validated(object sender, EventArgs e)
        {
           
        }

        private void cmbCategory_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                btnTestTwo_Click(sender, null);
            }
        }

        private void dgvTaskListInfo_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int i = e.RowIndex;
            if (i > 0)
            { 
                
            }
            //MessageBox.Show(i.ToString());
        }

        private void chkMesTickets_CheckedChanged(object sender, EventArgs e)
        {
            if (chkMesTickets.Checked)
            {
                if (chkAllDue.Checked)
                    chkAllDue.Checked = false;
            }
        }

        private void chkAllDue_CheckedChanged(object sender, EventArgs e)
        {
            if (chkAllDue.Checked)
            {
                if (chkMesTickets.Checked)
                    chkMesTickets.Checked = false;
            }
        }

        private void dgvTaskListInfo_UserDeletedRow(object sender, DataGridViewRowEventArgs e)
        {
           /* int i = e.Row.;
            MessageBox.Show(i.ToString());*/
        }

        private void dgvTaskListInfo_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {
            int i = e.Row.Index;
            MessageBox.Show(i.ToString());
        }

        private void button1_Click(object sender, EventArgs e)
        {
            dgvTaskListInfo.Rows.RemoveAt(1);  
        }

        private void dgvTaskListInfo_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if(e.ColumnIndex==13)
                dgvTaskListInfo.Rows.RemoveAt(1);  
        }

    }
    //the calss is used to create the text on a progress bar
    public class TextOnProgressBar : NativeWindow
    {
        private Control control;
        private string text = "null";
        private Color foreColor = SystemColors.ControlText;
        private Font font = SystemFonts.MenuFont;
        [DllImport("user32.dll")]
        static extern IntPtr GetWindowDC(IntPtr hWnd);

        [DllImport("user32.dll")]
        static extern int ReleaseDC(IntPtr hWnd, IntPtr hDC);
        public Font Font
        {
            set
            {
                font = value;
                if (control != null)
                    control.Invalidate();
            }
        }

        public Color ForeColor
        {
            set
            {
                foreColor = value;
                if (control != null)
                    control.Invalidate();
            }
        }

        public string Text
        {
            set
            {
                text = value;
                if (control != null)
                    control.Invalidate();
            }
        }

        public Control Control
        {
            set
            {
                control = value;
                if (control != null)
                {
                    AssignHandle(control.Handle);
                    control.Invalidate();
                }
            }
        }

        protected override void WndProc(ref Message m)
        {
            const int WM_PAINT = 0x000F;
            base.WndProc(ref m);
            if (control == null) return;
            switch (m.Msg)
            {
                case WM_PAINT:
                    IntPtr vDC = GetWindowDC(m.HWnd);
                    Graphics vGraphics = Graphics.FromHdc(vDC);
                    StringFormat vStringFormat = new StringFormat();
                    vStringFormat.Alignment = StringAlignment.Center;
                    vStringFormat.LineAlignment = StringAlignment.Center;
                    vGraphics.DrawString(text, font, new SolidBrush(foreColor),
                        new Rectangle(0, 0, control.Width, control.Height), vStringFormat);
                    ReleaseDC(m.HWnd, vDC);
                    break;
            }
        }
    }
}
