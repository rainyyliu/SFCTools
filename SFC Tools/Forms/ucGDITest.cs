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
    public partial class ucGDITest : UserControl
    {
        private bool[,] m_Brics = new bool[9, 15];

        Point m_ptPosTest;

        private int xOffSet=28;
        private int yOffset=25;
        public ucGDITest()
        {
            InitializeComponent();
        }

        private void lblTest_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.Clear(Color.Black);
            for(int i=0;i<675;i+=75)
            {
                for (int j = 0; j < 455; j += 30)
                {
                    //横线
                    if (j == 8 * 30)
                        g.DrawLine(new Pen(Color.Yellow), 0, j, 675, j);
                    else if(j == 3 * 30 || j==12*30)
                        g.DrawLine(new Pen(Color.Red), 0, j, 675, j);
                    else
                        g.DrawLine(new Pen(Color.Blue), 0, j, 675, j);
                } 
                g.DrawLine(new Pen(Color.Blue), i, 0, i, 455);
            }

            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 15; j++)
                {
                    if (m_Brics[i, j])
                    {
                        g.FillRectangle(new SolidBrush(Color.Green), new Rectangle(new Point(i*75+1,j*30+1),new Size(75-1,30-1)));
                    }
                }
            }
        }

        private void lblTest_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Point pt = e.Location;
                int newX=pt.X / 75;
                int newY = pt.Y / 30;
                m_Brics[newX, newY] = !m_Brics[newX, newY];
                this.lblTest.Invalidate();
            }
        }

        private void btnTest_Click(object sender, EventArgs e)
        {

        }

        private void btnTest_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                int px = Cursor.Position.X - m_ptPosTest.X;
                int py = Cursor.Position.Y - m_ptPosTest.Y;
                ((Button)sender).Location = new Point(((Button)sender).Location.X + px, ((Button)sender).Location.Y + py);
                //btnTest.Location = new Point(btnTest.Location.X + px, btnTest.Location.Y + py);
                m_ptPosTest = Cursor.Position;
            } 
        }

        private void btnTest_MouseDown(object sender, MouseEventArgs e)
        {
            this.m_ptPosTest = Cursor.Position;
        }

        private void btnTest_MouseUp(object sender, MouseEventArgs e)
        {
            //j=7
            Point ptCurrentPos = ((Button)sender).Location;
            double dwValue = 0;
            int iIdx=0;
            for (int i = 0; i < 9; i = i + 2)
            {
                int xPos = i * 75;
                int yPos = 7 * 30;
                double distValue = Math.Sqrt(Math.Abs(ptCurrentPos.X - xPos) * Math.Abs(ptCurrentPos.X - xPos) + Math.Abs(ptCurrentPos.Y - yPos) * Math.Abs(ptCurrentPos.Y - yPos));
                if (i==0 || dwValue>distValue)
                {
                    dwValue = distValue;
                    iIdx = i;
                }
                
            }
            Point ptNew = new Point(iIdx * 75 + xOffSet + 1, 30 * 8 + yOffset +1);
            ((Button)sender).Location = ptNew;
            this.m_Brics[iIdx, 7] = true;
        }
    }
}
