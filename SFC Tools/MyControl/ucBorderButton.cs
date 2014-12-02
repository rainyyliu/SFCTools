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
    public partial class ucBorderButton :Button
    {
              // PaintType.Focus;
        private int myPrintMode = 0;
        public ucBorderButton()
        {
            InitializeComponent();
        }

        private void btnNewButton_Paint(object sender, PaintEventArgs e)
        {
          
        }

        public void setButtonStatus(int pType)
        {
            myPrintMode =pType;
            this.Invalidate();
        }

        private void btnNewButton_Click(object sender, EventArgs e)
        {
            
        }

        private void ucBorderButton_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            Rectangle r = e.ClipRectangle;

            //g.FillRectangle(new SolidBrush(Color.BlueViolet), r);//白色背景

            GraphicsPath gp = new GraphicsPath();
            gp.AddRectangle(r);
            if (myPrintMode == 0)
                g.DrawPath(new Pen(Color.BlueViolet, 4), gp);
            else if (myPrintMode == 1)
                g.DrawPath(new Pen(Color.IndianRed, 4), gp);
            else
                g.DrawPath(new Pen(Color.Gray, 4), gp);
        }

        private void label1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            Rectangle r = e.ClipRectangle;

            //g.FillRectangle(new SolidBrush(Color.BlueViolet), r);//白色背景

            GraphicsPath gp = new GraphicsPath();
            gp.AddRectangle(r);
            if (myPrintMode == 0)
                g.DrawPath(new Pen(Color.BlueViolet, 4), gp);
            else if (myPrintMode == 1)
                g.DrawPath(new Pen(Color.IndianRed, 4), gp);
            else
                g.DrawPath(new Pen(Color.Gray, 4), gp);
        }

        private void label1_Click(object sender, EventArgs e)
        {
            this.label1.Invalidate();
        }
    }

   
}
