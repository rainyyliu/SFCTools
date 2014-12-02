using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SFC_Tools.Forms
{
    public partial class ucParentControl : UserControl
    {
        public ucParentControl()
        {
            InitializeComponent();
        }

        public string SetTitle
        {
            get { return this.label1.Text; }
            set { this.label1.Text = value; }
        }

    }
}
