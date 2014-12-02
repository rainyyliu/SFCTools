using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SFC_Tools.Classes;
using System.Threading;

namespace SFC_Tools.Forms
{
    public partial class ucTransTablesFormOraToMySql : UserControl
    {
        delegate void setProgressCallBack(int iValue);
        delegate void setDataTableProc(DataTable dt);
        delegate void setRichTextLogProc(string sLog);
        MyTransTablesFormOraToMySql myTrans;
        MySqlDAL mySqlDb;
        public ucTransTablesFormOraToMySql()
        {
            InitializeComponent();
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            try
            {
                myTrans = new MyTransTablesFormOraToMySql(txtDbName.Text.Trim(), txtSchema.Text.Trim(), txtPwd.Text.Trim());
                mySqlDb = new MySqlDAL(txtMySqlDb.Text.Trim());
                rtbGenerateLog.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message,"Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
                return;
            }
            this.btnConnect.Enabled = false;
            this.btnGenerate.Enabled = true;

        }
        private void SetDataSourc(DataTable dt)
        {
            if (this.dgvTableInfo.InvokeRequired)
            {
                setDataTableProc sp = new setDataTableProc(SetDataSourc);
                this.BeginInvoke(sp, new object[] { dt });
            }
            else
            {
                this.dgvTableInfo.DataSource = dt;
            }
        }
  

        private void setRichTextLog(string sLog)
        {
            if (this.rtbGenerateLog.InvokeRequired)
            {
                setRichTextLogProc sp = new setRichTextLogProc(setRichTextLog);
                this.BeginInvoke(sp, new object[] {sLog });
            }
            else
            {
                this.rtbGenerateLog.AppendText(sLog + "\r\n");
            }
        }
        private void setProgress(int iValue)
        {
            if (this.pgbTrans.InvokeRequired)
            {
                if (iValue >= 99)
                    iValue = 0;
                setProgressCallBack sp = new setProgressCallBack(setProgress);
                this.BeginInvoke(sp, new object[] { iValue });
            }
            else
            {
                if (iValue >= 100)
                    iValue = 0;
                pgbTrans.Value = iValue;
            }
        }
        private void TransTableFromOraToMySqlProc()
        {
            try
            {
                DataSet dsTable = myTrans.GetTablesFromOra();
                DataTable dtTable = dsTable.Tables[0];
                SetDataSourc(dtTable);
                int iSum = dtTable.Rows.Count;
                int iValue = 0;
                mySqlDb.MyExecuteNonSql("create schema " + txtSchema.Text.Trim() + ";");
                foreach (DataRow dr in dtTable.Rows)
                {
                    DataSet dsValue = myTrans.GetTableScript(dr[0].ToString());
                    string sSql = GetCreateScriptByRes(dsValue, dr[0].ToString());
                    mySqlDb.MyExecuteNonSql(sSql);
                    mySqlDb.MyExecuteNonSql("COMMIT");
                    setRichTextLog(DateTime.Now.ToString() + ":["+iValue.ToString()+"]Table " + dr[0].ToString() + " Created Successful!");
                    setProgress(Convert.ToInt32(iValue * 100 / iSum));
                    iValue++;
                }
                setProgress(100);
            }
            catch (Exception ex)
            {
                mySqlDb.MyExecuteNonSql("drop schema "+txtSchema.Text.Trim()+";");
                MessageBox.Show(ex.Message.ToString(), "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }
        private string GetCreateScriptByRes(DataSet ds,string sTableName)
        {
            string m_STableScript = "CREATE TABLE " + sTableName+"(";
            string strNull = string.Empty;
            int i = 0;
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                i++;
                if (i == ds.Tables[0].Rows.Count)
                {
                    strNull = " ";
                }
                else
                {
                    if (dr[3].ToString() == "N")
                    {
                        strNull = "NOT NULL,";
                    }
                    else
                    {
                        strNull = ",";
                    }
                }
                string sColName=dr[0].ToString();
                if (sColName == "USAGE")
                    sColName = sColName + "A";

                switch (dr[1].ToString())
                { 
                    case "VARCHAR2":
                        m_STableScript = m_STableScript + sColName + " VARCHAR(" + dr[2].ToString() + ")" + strNull;
                        break;
                    case "DATE":
                        m_STableScript = m_STableScript + sColName + " " + dr[1].ToString() + strNull;
                        break;
                    case "NUMBER":
                        m_STableScript = m_STableScript + sColName + " INT(" + dr[2].ToString() + ")" + strNull;
                        break;
                    case "CHAR":
                        m_STableScript = m_STableScript + sColName + " VARCHAR(" + dr[2].ToString() + ")" + strNull;
                        break;
                    case "LONG":
                        m_STableScript = m_STableScript + sColName + " " + dr[1].ToString() + strNull;
                        break;
                    case "NVARCHAR2":
                        m_STableScript = m_STableScript + sColName + " VARCHAR(" + dr[2].ToString() + ")" + strNull;
                        break;
                    case "BLOB":
                        m_STableScript = m_STableScript + sColName + " " + dr[1].ToString() + strNull;
                        break;
                    default:
                        break;
                    
                }
            }
            m_STableScript = m_STableScript + " );";
            return m_STableScript;
        }
        
        private void txtDbName_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Return)
            {
                if (txtDbName.Text.Trim().Length == 0)
                {
                    MessageBox.Show("Pelease Input DB Name!", "DB Name Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtDbName.Text = "";
                    txtDbName.SelectAll();
                    txtDbName.Focus();
                    return;
                }
                else
                {
                    txtSchema.SelectAll();
                    txtSchema.Focus();
                }
            }
        }

        private void ucTransTablesFormOraToMySql_Load(object sender, EventArgs e)
        {
            this.txtDbName.Focus();
        }

        private void txtSchema_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Return)
            {
                if (txtSchema.Text.Trim().Length == 0)
                {
                    MessageBox.Show("Pelease Input Schema Name!", "Schema Name Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtSchema.Text = "";
                    txtSchema.SelectAll();
                    txtSchema.Focus();
                    return;
                }
                else
                {
                    txtPwd.SelectAll();
                    txtPwd.Focus();
                }
            }
        }

        private void txtPwd_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Return)
            {

                if (txtPwd.Text.Trim().Length == 0)
                {
                    MessageBox.Show("Pelease Input Pass Word!", " Pass Word Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtPwd.Text = "";
                    txtPwd.SelectAll();
                    txtPwd.Focus();
                    return;
                }
                else
                {
                    btnConnect_Click(sender, new EventArgs());
                }
            }
        }

        private void btnGenerate_Click(object sender, EventArgs e)
        {
            try
            {
                this.rtbGenerateLog.Focus();
                Thread tdTrans = new Thread(new ThreadStart(TransTableFromOraToMySqlProc));
                tdTrans.Start();
                btnConnect.Enabled = true;
                btnGenerate.Enabled = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Connect Data Base Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtDbName.SelectAll();
                txtDbName.Focus();
                return;
            }

        }
    }
}
