using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace SFC_Tools.MyControl
{
    public partial class ucSpeicalBtn : UserControl
    {
        private int iColorlevel = 0;
        private int iTitlePos = 0;
        public ucSpeicalBtn()
        {
            InitializeComponent();
        }

        private void button1_Paint(object sender, PaintEventArgs e)
        {
            SFC_Tools.Classes.BitmapRegion.CreateControlRegion(button1, (Bitmap)this.button1.Image);

            ////////////////////////////////////////////
            if (iTitlePos == 0)
                SetTitle();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            iColorlevel = 2;
            this.panel1.Invalidate();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            if (iColorlevel != 0)
            {
                Graphics g = e.Graphics;
                Size xx = button1.Size;
                Point lc = button1.Location;
                lc.X = lc.X - 5;
                lc.Y = lc.Y - 5;
                xx.Width = xx.Width + 10;
                xx.Height = xx.Height + 10;
                Rectangle r = new Rectangle(lc, xx);
                GraphicsPath gp = new GraphicsPath();
                gp.AddRectangle(r);
                if(this.iColorlevel==1)
                    g.DrawPath(new Pen(Color.BlueViolet, 3), gp);
                else if(this.iColorlevel==2)
                    g.DrawPath(new Pen(Color.Tomato, 3), gp);              
            }
            
        }
   
        private void SetTitle()
        {
            Point pt = button1.Location;
            Size size = button1.Size;
            Point md = new Point(pt.X + size.Width / 2, pt.Y + size.Height / 2);
            Size szTitle = lblTitle.Size;
            Point ptTitle = lblTitle.Location;
            int iXoffset = md.X - (ptTitle.X + szTitle.Width / 2);
            lblTitle.Location = new Point(pt.X + iXoffset, ptTitle.Y);
            iTitlePos = 1;
        }


        public void setColorLevel(int iNum)
        {
            this.iColorlevel = iNum;
        }

        public void setButtonText(string sText)
        {
            this.lblTitle.Text = sText;
            lblTitle.Invalidate();
        }

        private void panel1_Click(object sender, EventArgs e)
        {
            //iColorlevel = 2;
            this.panel1.Invalidate();
        }

        private void button1_Leave(object sender, EventArgs e)
        {
            this.iColorlevel = 0;
            this.panel1.Invalidate();
        }

        private void ucSpeicalBtn_Load(object sender, EventArgs e)
        {
           
        }

    }
}
