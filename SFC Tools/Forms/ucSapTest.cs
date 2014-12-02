using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Ask.Peony.SAPFunctionProxy;
using System.Collections;

namespace SFC_Tools.Forms
{
    public partial class ucSapTest : UserControl
    {
        public ucSapTest()
        {
            InitializeComponent();
        }

        private void btnGo_Click(object sender, EventArgs e)
        {
            //RFCProxy rfcProxy = new RFCProxy(txtServerIP.Text, txtClient.Text, txtLan.Text, txtUser.Text, txtPwd.Text, Convert.ToInt32(txtSysNo.Text));
            RFCProxy rfcProxy = new RFCProxy("ASHOST=10.18.222.152 SYSNR=00 CLIENT=802 USER=SAPSFCBR01 PASSWD=l1006",false);

            DataTable dtSendData=new DataTable();
            DataColumn column;
            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "AUFNR";
            dtSendData.Columns.Add(column);

             column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "MATNR";
            dtSendData.Columns.Add(column);

             column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "CHARG";
            dtSendData.Columns.Add(column);

             column = new DataColumn();
            column.DataType = System.Type.GetType("System.Int32");
            column.ColumnName = "MENGE";
            dtSendData.Columns.Add(column);

            DataRow dr=dtSendData.NewRow();
            dr["AUFNR"] = "000080157313";
            dr["MATNR"] = "002.01092.055LOTE";
            dr["CHARG"] = "1258936325";
            dr["MENGE"] = 10;
            dtSendData.Rows.Add(dr);

            Hashtable htInputTalbe = new Hashtable();
            htInputTalbe.Add("I_CHARG", dtSendData);
            /*
            Hashtable outParam1 = new Hashtable();
            outParam1.Add("TYPE", "TEST");
            outParam1.Add("CODE", "TEST");
            outParam1.Add("MESSAGE", "TEST");
            */
             Hashtable htoutTable = new Hashtable();
             htoutTable.Add("RETURN", new DataTable());
            // htoutTable.Add("E_RETURN", outParam1);


             Hashtable htOutputParams = new Hashtable();

             htOutputParams.Add("TYPE", "");
             htOutputParams.Add("CODE", "");
             htOutputParams.Add("MESSAGE", "");

             Hashtable outParameters = new Hashtable();
             Hashtable outStructures = new Hashtable();
             outStructures.Add("E_RETURN", htOutputParams);

             Hashtable htoutDataTable = new Hashtable();
             htoutDataTable.Add("I_CHARG",new DataTable());
             SAPLoginStatus log = rfcProxy.LoginStatus;
             bool bisSend = rfcProxy.InvokeSAPRFCorABAPFunction("Z_RFC_BR_L6_BATCH_UPLOAD", null, null, htInputTalbe, ref outParameters, ref outStructures, ref htoutDataTable);
             if (!bisSend)
                 MessageBox.Show(rfcProxy.ErrMsg.ToString());
             else
             {
                 bool bCommit = rfcProxy.CommitWork('X');
                 if ((outStructures["E_RETURN"] as Hashtable)["TYPE"].ToString() != "S")
                     MessageBox.Show((outStructures["E_RETURN"] as Hashtable)["MESSAGE"].ToString());
                 else
                    MessageBox.Show("OK"); 
             }

            //  public bool InvokeSAPRFCorABAPFunction(string funName, Hashtable inParameters, ref Hashtable outParameters, ref Hashtable outStructures, ref Hashtable outTables);
        }

        private void btnGoProxy_Click(object sender, EventArgs e)
        {
            ClassLibrary1.Class1 xx = new ClassLibrary1.Class1();
            xx.Upload_Data_TO_SAP("PP", "002.01092.055LOTE", "1258936325", -10);
            string sTemp = xx.STYPE;
            sTemp = xx.SMSG;
        }
        /*
         [RfcParameter(AbapName = "E_RETURN",RfcType=RFCTYPE.RFCTYPE_STRUCTURE, Optional = true, Direction = RFCINOUT.OUT)]
         [XmlElement("E_RETURN", IsNullable=false)]
         out BAPIRETURN E_Return,
         [RfcParameter(AbapName = "I_CHARG",RfcType=RFCTYPE.RFCTYPE_ITAB, Optional = false, Direction = RFCINOUT.INOUT)]
         [XmlArray("I_CHARG", IsNullable=false)]
         [XmlArrayItem("item", IsNullable=false)]
         ref ZTBPP064Table I_Charg)
        {
            object[]results = null;
            results = this.SAPInvoke("Z_Rfc_Br_L6_Batch_Upload",new object[] {
                                I_Charg});
            E_Return = (BAPIRETURN) results[0];
            I_Charg = (ZTBPP064Table) results[1];

        }

             */
    }
}
