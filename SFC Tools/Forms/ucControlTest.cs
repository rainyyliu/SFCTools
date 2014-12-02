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
    public partial class ucControlTest : UserControl
    {
        Point m_ptPos;
        Point m_ptOriginal;
        bool bIsMove = true;
        public ucControlTest()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (bIsMove == true) { MessageBox.Show("sfdfdf"); }
        }

        private void button1_MouseDown(object sender, MouseEventArgs e)
        {
            m_ptPos = Cursor.Position;
            m_ptOriginal = button1.Location;
            string stemp = string.Format("Position:[{0},{1}]",m_ptOriginal.X,m_ptOriginal.Y);
            this.label1.Text = stemp;
        }

        private void button1_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left) 
            {
                int px = Cursor.Position.X - m_ptPos.X;
                int py = Cursor.Position.Y - m_ptPos.Y; 
                button1.Location = new Point(button1.Location.X + px, button1.Location.Y + py);
                m_ptPos = Cursor.Position;
                bIsMove = false;
            } 
        }

        private void button1_MouseUp(object sender, MouseEventArgs e)
        {
            bIsMove = true;
            button1.Location = m_ptOriginal;
            string stemp = string.Format("Position:[{0}-{1}]", m_ptOriginal.X, m_ptOriginal.Y);
            this.label1.Text = stemp;
        }


    }
}
