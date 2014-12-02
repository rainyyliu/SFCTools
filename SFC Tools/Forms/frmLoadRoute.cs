using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using SFC_Tools.Model;


namespace SFC_Tools
{
    public partial class frmLoadRoute : Form
    {
        public delegate void ChangeRouteEventHandler(object Sender, Model.RouteCodeOfEventArgs e);
        public event ChangeRouteEventHandler RouteChange;
        public int strValue;

        public void OnRouteChange(Model.RouteCodeOfEventArgs e)
        {
            if (RouteChange != null)
            {
                this.RouteChange(this, e);
            }
        }
        public frmLoadRoute()
        {
            InitializeComponent();
            this.comboBox1.Items.Clear();
            ArrayList arrRouteLst = SFCStartup.dba.GetRouteNameList();
            foreach (string strItem in arrRouteLst)
            {
                this.comboBox1.Items.Add(strItem);
            }
        }

        private void glassButton1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void glassButton2_Click(object sender, EventArgs e)
        {
            strValue = SFCStartup.dba.GetRouteCode(this.comboBox1.Text);
            RouteCodeOfEventArgs ex = new RouteCodeOfEventArgs(strValue);
            OnRouteChange(ex);
            Close();
        }

    }
}
