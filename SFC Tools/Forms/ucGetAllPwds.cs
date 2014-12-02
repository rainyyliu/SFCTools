using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Configuration;

using System.Data.SqlClient;
using SFC_Tools.Classes;
using System.Threading;

namespace SFC_Tools.Forms
{
    public partial class ucGetAllPwds : UserControl
    {
        private DataTable dt;
        public ucGetAllPwds()
        {
            InitializeComponent();
           
        }
        
        private void btnGet_Click(object sender, EventArgs e)
        {
            //this.dgvUserInfo.DataSource = new DataTable();
            if(this.dt!=null)
                if(this.dt.Rows.Count>0)
                    this.dt.Clear();
            this.dgvUserInfo.Columns[0].Width = 100;
            this.dgvUserInfo.Columns[1].Width = 260;
            this.dgvUserInfo.Columns[2].Width = 150;
            this.dgvUserInfo.Columns[3].Width = 120;

                
            this.lblCount.Text = "0";
            this.txtName.Enabled = false;
            string sConn;
            AppSettingsReader reader=new AppSettingsReader();
            if (this.rbtnDev.Checked)
                sConn = reader.GetValue("MsSQLConnDev", typeof(string)).ToString();
            else if (this.rbtnSit.Checked)
                sConn = reader.GetValue("MsSQLConnSit", typeof(string)).ToString();
            else
                sConn = reader.GetValue("MsSQLConnPrd", typeof(string)).ToString();

            SqlConnection conn = new SqlConnection(sConn);

            conn.Open();

            SqlCommand sc = conn.CreateCommand();
            sc.CommandText = " SELECT Username,UserPassword,'' as RealPwd,Realname FROM ADUser";
            sc.CommandType = CommandType.Text;
            SqlDataAdapter sd = new SqlDataAdapter();
            sd.SelectCommand = sc;
            DataSet ds = new DataSet();
            sd.Fill(ds);
            dt = ds.Tables[0];
            this.dgvUserInfo.DataSource = dt;
            this.lblCount.Text = dt.Rows.Count.ToString();
            Thread t = new Thread(new ThreadStart(DeCryptPwd));
            t.Start();
            conn.Close();
        }

        private void DeCryptPwd()
        {
            int iCount = 0;
            iCount = dt.Rows.Count;

            for (int i = 0; i < iCount; i++)
            {
                DataRow[] dr= dt.Select("RealPwd<>'' and UserPassword='" + dt.Rows[i][1].ToString() + "'");
                if (dr.Length > 0)
                {
                    dt.Rows[i][2] = dr[0][2].ToString();
                }
                else
                    dt.Rows[i][2] = DecryptedData(dt.Rows[i][1].ToString());
                SetProgressPos(iCount,i);
            }
            this.dgvUserInfo.DataSource = dt;
            SetInputEnable(iCount);
        }
        delegate void SetProgressValue(int iMax,int iValue);
        delegate void SetInputEnableDelegate(int iVal);
        delegate void SetlableCationDelegate(int iVal);
        private void SetProgressPos(int iMax,int iVlalue)
        {
            if (this.pgbProgress.InvokeRequired)
            {
                SetProgressValue sp = new SetProgressValue(SetProgressPos);
                this.BeginInvoke(sp, new object[] { iMax, iVlalue });
            }
            else
            {
                this.pgbProgress.Maximum = iMax;
                this.pgbProgress.Value = iVlalue;
            }
            
        }
        private void SetInputEnable(int iVal)
        {
            if (this.txtName.InvokeRequired)
            {
                SetInputEnableDelegate sp = new SetInputEnableDelegate(SetInputEnable);
                this.BeginInvoke(sp, new object[] { iVal });
            }
            else
            {
                if (iVal > 0)
                    this.txtName.Enabled = true;
                else
                    this.txtName.Enabled = false; 
            }

            if (this.lblCount.InvokeRequired)
            {
                if (this.txtName.InvokeRequired)
                {
                    SetlableCationDelegate sp = new SetlableCationDelegate(SetInputEnable);
                    this.BeginInvoke(sp, new object[] { iVal });
                }
                else
                    this.lblCount.Text = iVal.ToString();
            }
        }

        private string DecryptedData(string sInData)
        {
            return SecretHelper.DecryptFromBase64String(sInData, "A+ FrameworkChinaWales Wang1973.09.09Man", "A+ Framework中华人民共和国王智一九七三年九月九日男");
        }

        private void txtName_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                if (txtName.Text.Trim().Length == 0)
                    this.dgvUserInfo.DataSource = dt;
                else
                {
                    string sCal =dt.Columns[1].ColumnName + " like " + "'%" + this.txtName.Text.Trim() + "%' or "+ dt.Columns[3].ColumnName + " like " + "'%" + this.txtName.Text.Trim() + "%'";
                    DataRow[] drs = dt.Select(sCal);
                    DataTable dtNew = dt.Clone();
                    foreach (DataRow dr in drs)
                    {
                        dtNew.ImportRow(dr);
                    }
                    this.dgvUserInfo.DataSource = dtNew;
                    this.lblCount.Text = dtNew.Rows.Count.ToString();
                }
            }
        }

    }
}

