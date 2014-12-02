using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Runtime;
using System.IO;

namespace SFC_Tools.Forms
{
    public partial class ucCurl : UserControl
    {
        public ucCurl()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("0.0");
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            txtBack.Text = "";
            string strUrl = txtUrl.Text.Trim();
            if (strUrl=="")
            {
                MessageBox.Show("Please Input a URL!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtUrl.SelectAll();
                txtUrl.Focus();
                return;
            }
            string strFile = txtFile.Text.Trim();
            if (!File.Exists(strFile))
            {
                MessageBox.Show("File:"+strFile+" not exist!","Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
                txtFile.SelectAll();
                txtFile.Focus();
                return;
            }
            string strUser = txtUser.Text.Trim();
            if (strUser.Length==0)
            {
                MessageBox.Show("Please Input a User Name!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtUser.SelectAll();
                txtUser.Focus();
                return;
            }

            string strPwd = txtPassWord.Text.Trim();
            if (strPwd.Length == 0)
            {
                MessageBox.Show("Please Input a PassWord Name!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtPassWord.SelectAll();
                txtPassWord.Focus();
                return;
            }
            string szField = "Submit1";
            string szValue = "Upload";
            try
            {
                txtBack.Text = http_post_file(strUrl, strFile, strUser, strPwd, szField, szValue);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
           // txtBack.Text = Get_Upload_Result();
        }
        [DllImport("Curl\\MyLibCurl.dll", EntryPoint = "http_post_file")]
        public static extern string http_post_file(string szUrl, string szFile, string szUser, string szPwd, string szFiled, string szValue);

        [DllImport("TEST\\MyLibCurl.dll", EntryPoint = "http_post_file")]
        public static extern string http_post_file(string szUrl, string szFile, string szUser, string szPwd);
        private void btnTest_Click(object sender, EventArgs e)
        {
           
        }

        private void btnTest_Click_1(object sender, EventArgs e)
        {
                      
            txtBack.Text = http_post_file(txtUrl.Text, txtFile.Text,txtUser.Text, txtPassWord.Text);
            string strSourc = "This is a Test Hello World!,hello";
            string strTarget="";
            strTarget = "hello";
            if (strSourc.IndexOf(strTarget) > 0)
            {
                MessageBox.Show("OK");
            }
            else
            {
                MessageBox.Show("NO");
            }
        }

        //[DllImport("MyLibCurl.dll", EntryPoint = "Get_Upload_Result")]
        //public static extern string Get_Upload_Result();
        
    }
}
