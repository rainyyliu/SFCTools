using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using SFC_Tools.Classes;

namespace SFC_Tools.Forms
{
    public partial class ucEnDeCrypt : UserControl
    {
        public ucEnDeCrypt()
        {
            InitializeComponent();
        }

        private void btnEnCrypt_Click(object sender, EventArgs e)
        {
            if (this.rtbDeCrypt.Text.Trim().Length == 0)
            {
                MessageBox.Show("Please Input a Original Data!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.rtbDeCrypt.Focus();
                return;
            }
            this.rtbEnCrypt.Text = "";
            try
            {
                this.rtbEnCrypt.Text = Convert.ToBase64String(SecretHelper.PrivateEncrypt(this.rtbDeCrypt.Text.Trim(), "", ""));
            }
            catch (Exception ex)
            {
                this.rtbEnCrypt.Text = "Error:"+ex.Message.ToString();
            }
        }

        private void btnDeCrypt_Click(object sender, EventArgs e)
        {
            if (this.rtbEnCrypt.Text.Trim().Length == 0)
            {
                MessageBox.Show("Please Input a EnCrypted Data!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.rtbEnCrypt.Focus();
                return;
            }
            this.rtbDeCrypt.Text = "";
            try
            {
                this.rtbDeCrypt.Text = SecretHelper.DecryptFromBase64String(this.rtbEnCrypt.Text.Trim());
                 //SecretHelper.DecryptFromBase64String(this.rtbEnCrypt.Text, "A+ FrameworkChinaWales Wang1973.09.09Man", "A+ Framework中华人民共和国王智一九七三年九月九日男");
                //MessageBox.Show(SecretHelper.DecryptFromBase64String(this.rtbEnCrypt.Text.Trim()));
            }
            catch (Exception ex)
            {
                this.rtbDeCrypt.Text = "error:" + ex.Message.ToString();
            }

        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            this.rtbEnCrypt.Clear();
            this.rtbDeCrypt.Clear();
        }
    }
}
