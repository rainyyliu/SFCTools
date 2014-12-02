using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.OleDb;
using System.Collections;
using System.Threading;

namespace SFC_Tools.Forms
{
    public partial class ucSpAnalyse : UserControl
    {
        delegate void SetProgressValue(int iValue);
        delegate void SetProgressMaxValue(int iValue);
        delegate void setDataGridValue(DataSet dtRes);

        private void SetDataGrid(DataSet dtRes)
        {
            if (this.progressBar1.InvokeRequired)
            {
                setDataGridValue spv = new setDataGridValue(SetDataGrid);
                this.BeginInvoke(spv, new object[] { dtRes });
            }
            else
                this.dataGridView1.DataSource = dtRes.Tables[0];
        }

        private void SetProgress(int iValue)
        {
            if (this.progressBar1.InvokeRequired)
            {
                SetProgressValue spv = new SetProgressValue(SetProgress);
                this.BeginInvoke(spv, new object[] { iValue });
            }
            else
                this.progressBar1.Value = iValue;
        }

        private void SetProgressMax(int iValue)
        {
            if (this.progressBar1.InvokeRequired)
            {
                SetProgressValue spv = new SetProgressValue(SetProgressMax);
                this.BeginInvoke(spv, new object[] { iValue });
            }
            else
                this.progressBar1.Maximum = iValue;
        }

        public ucSpAnalyse()
        {
            InitializeComponent();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {

            Thread thread = new Thread(new ThreadStart(GetSMOSpInfo));
            thread.Start();
            
            //MessageBox.Show("DONE");
        }

        private void GetSMOSpInfo()
        {
            if ((txtDb.Text.Trim().Length == 0) || (this.txtSchema.Text.Trim().Length == 0))
                return;
            OleDbConnection myConn = new OleDbConnection("Provider=MSDAORA.1;Password=" + this.txtSchema.Text + ";User ID=" + this.txtSchema.Text + ";Data Source="+this.txtDb.Text+";Persist Security Info=True");
            DataSet dtProc = SFC_Tools.SFCStartup.dba.GetSMOStoredProc(myConn);
            //this.dataGridView1.DataSource = dtProc.Tables[0];
            SetDataGrid(dtProc);
            this.progressBar1.Minimum = 0;
            SetProgressMax(dtProc.Tables[0].Rows.Count);
            int j = 0;
            foreach (DataRow dr in dtProc.Tables[0].Rows)
            {

                string sOwner, sProcName, sTemp;
                sTemp = dr[0].ToString();
                int iPos = sTemp.IndexOf(".");
                if (iPos > 0)
                {
                    sOwner = sTemp.Substring(0, iPos);
                    sProcName = sTemp.Substring(iPos + 1, sTemp.Length - iPos - 1);
                }
                else
                {
                    sOwner = "SFIS1";
                    sProcName = sTemp;
                }
                SFC_Tools.SFCStartup.dba.GetSubProcsByProc(sProcName, sOwner, myConn, this.txtDb.Text.Trim());

                SetProgress(j++);
            }
            SetProgress(0);
        }

    }
}
