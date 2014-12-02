using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using System.CodeDom;
using System.CodeDom.Compiler;
using System.Reflection;
using System.IO;
using System.Net;
using System.Web;
using System.Web.Services.Description;
using Microsoft.CSharp;
using SFC_Tools.ServiceReference5;
using LabelManager2;
using System.Configuration;
using System.Data.SqlClient;
using SFC_Tools.Classes;
using System.Collections;


namespace SFC_Tools.Forms
{
    public partial class ucWebServiceTest : UserControl
    {
        public ucWebServiceTest()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //SFC_Tools.ServiceReference5.Service1SoapClient ssc = new Service1SoapClient();
           
            string sUid = "YZDWS";
            string sPwd = "aditdkt..";
            string sUrl = "http://10.205.4.164/Yzdcd/Service1.asmx";
            if(this.chkProduct.Checked) 
                sUrl = "http://10.197.10.28:8003/";
            ArrayList arr = new ArrayList();
            arr.Add("TOE51A");
            DownLoadWareHouseInfoByCode(arr);
            return;
           /* TiptopService.Service1 tpSrv = new TiptopService.Service1()
            {
                 Url = sUrl,
                TPSoapValue = new TiptopService.TPSoap()
                {
                    UserID = sUid,
                    UserPWD = sPwd
                }
            };
            decimal xx=2400;
            try
            {
                TiptopService.ServiceOut outValue = tpSrv.FinishedGoodsBill("TipTop",
                    "6Ce", "WCe-E76808", "1B41NEQ00-600-GAc", xx, "TOE52A", "");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
            */
            
            //MessageBox.Show(this.dtWorkOrderInfo.Rows[0].Cells[0].Value.ToString());
            //TiptopService.ServiceOut outVal= tpSrv.QueryStock("TipTop", "TOE52A");
            //tpSrv.FinishedGoodsBill("1A01K1B00-000-GB", "1A01K1B00-000-GB", "1A01K1B00-000-GB", "1A01K1B00-000-GB", 20, "11", "111");
            //string[] tt = ssc.GoodsBill("1A01K1B00-000-GB", "1A01K1B00-000-GB", "1A01K1B00-000-GB", "1A01K1B00-000-GB", 20, "", "");
            //foreach (string xx in tt)
            //{
            //    MessageBox.Show(xx);
            //}
        }

        private void btnCSTest_Click(object sender, EventArgs e)
        {
            /*string sPath = txtPath.Text.Trim();
            if (File.Exists(sPath))
            {
                ApplicationClass APS = new ApplicationClass();
                APS.Documents.Open(sPath, false);
                Document dc = APS.ActiveDocument;
                dc.Variables.FreeVariables.Item("V1").Value = "世界你好！";
                dc.PrintDocument();
                dc.Close();
                APS.Quit();
            }
            else
            {
                MessageBox.Show("File Not Exists!");
            }*/
        }

        private void btnGetData_Click(object sender, EventArgs e)
        {
            //MessageBox.Show(Convert.ToBase64String(SecretHelper.PrivateEncrypt("111", "", "")));
            //byte[] xx = SecretHelper.PrivateEncrypt("111", "", "");
            //MessageBox.Show(SecretHelper.DecryptFromBase64String("UIocarTKvGxHoVmStKWj1g==", "A+ FrameworkChinaWales Wang1973.09.09Man", "A+ Framework中华人民共和国王智一九七三年九月九日男"));
            string conn;
            AppSettingsReader reader=new AppSettingsReader();
            conn = reader.GetValue("MsSQLConn", typeof(string)).ToString();

            SqlConnection sconn = new SqlConnection(conn);

            sconn.Close();
            sconn.Open();

            SqlCommand sc = sconn.CreateCommand();
            sc.CommandText = "TI17_GetWorkOrderInfo_SP";
            sc.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter sd = new SqlDataAdapter();
            sd.SelectCommand = sc;
            DataSet ds=new DataSet();
            sd.Fill(ds) ;
            sconn.Close();
            this.dtWorkOrderInfo.DataSource = ds.Tables[2];
            if (ds.Tables[2].Rows.Count > 0)
                this.btnTipTop.Enabled = true;
            else
                this.btnTipTop.Enabled = false;
#if DEBUG
           // MessageBox.Show("Hello World");
            string xxx;
#else
            //MessageBox.Show("Oh Shit！");
            string tt;
#endif
        }
        private void DownLoadWareHouseInfoByCode(ArrayList storageCode)
        {
            /*
            //ServiceException sExp = null;
            StringBuilder sbErrorMessage = new StringBuilder();
            StringBuilder sbStackTrace = new StringBuilder();

            ArrayList arrTargetCode = new ArrayList();
            try
            {
                string TipConfigCode = "TOE51A\\TOE53A";//this.Host.Function.GetParamValue("TipTopWareHouseCode");
                string[] codes = TipConfigCode.Split(new char[] { ',', '/', '，', '\\' }, StringSplitOptions.RemoveEmptyEntries);
                //get valid storageCode
                foreach (string sConfigCode in storageCode)
                {
                    foreach (string sCode in codes)
                    {
                        if (sConfigCode == sCode)
                            arrTargetCode.Add(sCode);
                    }
                }

                //get code info by storageCode
                if (arrTargetCode.Count > 0)
                {
                    DataTable result = null;
                    string url = "http://10.197.10.28:8003/";//this.Host.Function.GetParamValue("TipTopWebServiceURL");
                    string uid = "YZDWS";// this.Host.Function.GetParamValue("TipTopWebServiceUID");
                    string pwd = "aditdkt..";// this.Host.Function.GetParamValue("TipTopWebServicePWD");
                    string client = "TipTop";// this.Host.Function.GetParamValue("TipTopClient");
                    TiptopService.Service1 service = new TiptopService.Service1()
                    {
                        Url = url,
                        TPSoapValue = new TiptopService.TPSoap()
                        {
                            UserID = uid,
                            UserPWD = pwd
                        }
                    };

                    #region GetAllStorageInfo
                    foreach (string code in arrTargetCode)
                    {
                        if (result == null)
                        {
                            try
                            {
                                TiptopService.ServiceOut serviceOut = service.QueryStock(client, code);
                                if (serviceOut.Status == "Y")
                                {
                                    result = serviceOut.TableInfo;
                                }
                                else
                                {
                                    //this.Host.Logging(new ServiceException(serviceOut.Error), EventFacilityLevel.User, EventSeverityLevel.Error, "QueryStock", string.Empty, string.Format("TI15,Client:{0},Code:{1}", client, code));
                                }
                            }
                            catch (Exception ex)
                            {
                                sbErrorMessage.AppendFormat("Download Storage{0}, {1} \r\n", code, ex.Message);
                                sbStackTrace.AppendFormat("Download Storage{0} ,{1} \r\n", code, ex.StackTrace);
                                //this.Host.Logging(ex.Message);
                            }
                        }
                        else
                        {
                            try
                            {
                                TiptopService.ServiceOut serviceOut = service.QueryStock(client, code);
                                if (serviceOut.Status == "Y")
                                {
                                    DataTable temp = serviceOut.TableInfo;
                                    result.Merge(temp);
                                }
                                else
                                {
                                    //this.Host.Logging(new ServiceException(serviceOut.Error), EventFacilityLevel.User, EventSeverityLevel.Error, "QueryStock", string.Empty, string.Format("TI15,Client:{0},Code:{1}", client, code));
                                }
                            }
                            catch (Exception ex)
                            {
                                sbErrorMessage.AppendFormat("Download Storage{0}, {1} \r\n", code, ex.Message);
                                sbStackTrace.AppendFormat("Download Storage{0} ,{1} \r\n", code, ex.StackTrace);
                                //this.Host.Logging(ex.Message);
                            }
                        }
                    }
                    #endregion

                    //var param = DBTransaction.CreateParam("TI18.MMStorageDetail.Insert");
                
                    DataSet ds = new DataSet();
                    ds.Tables.Add(result);
                    //ds.Tables.Add(newDt);
                    //param.DataSetValue = ds;
                    //this.Host.InvokeService(
                    //    (sender1, args) =>
                    //    {
                    //        if (args.Results.HasError)
                    //        {
                    //            this.Host.Logging(args.Results.Default.Error);
                    //        }
                    //    }, param);
                }
            }
            catch (Exception ex)
            {
                sbErrorMessage.AppendFormat("DownLoadWareHouseInfoByCode Error:{0}", ex.Message);
                sbStackTrace.AppendFormat("DownLoadWareHouseInfoByCode Error:{0}", ex.StackTrace);
            }
            if (sbErrorMessage.Length > 0 || sbStackTrace.Length > 0)
            {
                //sExp = new ServiceException();
                //sExp.Message = sbErrorMessage.ToString();
                //sExp.StackTrace = sbErrorMessage.ToString();
            }
            */
            //return sExp;
        }// end of function

    }
}
