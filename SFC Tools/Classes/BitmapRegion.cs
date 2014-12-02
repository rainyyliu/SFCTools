using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace SFC_Tools.Classes
{
    public class BitmapRegion
    {
        public BitmapRegion() { }

        public static void CreateControlRegion(Control control, Bitmap bitmap)
        {
            if (control == null || bitmap == null)
                return;

            control.Width = bitmap.Width;
            control.Height = bitmap.Height;

            if (control is Form)
            {
                Form fm = (Form)control;
                fm.Width = control.Width;
                fm.Height = control.Height;

                fm.FormBorderStyle = FormBorderStyle.None;
                fm.BackgroundImage = bitmap;

                GraphicsPath gp = CalculateControlGraphicsPathA(bitmap);
                fm.Region = new Region(gp);
                fm.Width = bitmap.Width;
                fm.Height = bitmap.Height;
            }
            else if (control is Button) 
            {
                Button bt = (Button)control;
                bt.Text = "";
                bt.Cursor = Cursors.Hand;
                bt.BackgroundImage = bitmap;

                GraphicsPath gp = CalculateControlGraphicsPathA(bitmap);
                bt.Region = new Region(gp);
                //bt.Width = bitmap.Width;
                //bt.Height = bitmap.Height; 
            }

        }

        private static GraphicsPath CalculateControlGraphicsPath(Bitmap bitmap)
        {
            GraphicsPath gp = new GraphicsPath();
            Color colorTransparent = bitmap.GetPixel(0,0);

            int colOpaquePixel = 0;

            for (int row = 0; row < bitmap.Height - 1; row++)
            {
                colOpaquePixel = 0;
                for (int col = 0; col < bitmap.Width-1; col++)
                {
                    if (bitmap.GetPixel(row, col) != colorTransparent)
                    {
                        colOpaquePixel = col;
                        int colNext = col;
                        for (colNext = colOpaquePixel; colNext < bitmap.Width; colNext++)
                        {
                            Color api = bitmap.GetPixel(colNext, row);
                            if (bitmap.GetPixel(colNext, row) == colorTransparent)
                            {
                                break;
                            }

                            gp.AddRectangle(new Rectangle(colOpaquePixel,row, colNext - colOpaquePixel, 1));  
  
                            col = colNext;  
                        }
                    }
                }
            }

                return gp;
        }

        private static GraphicsPath CalculateControlGraphicsPathA(Bitmap bitmap)
        {

            GraphicsPath graphicsPath = new GraphicsPath();

            Color colorTransparent = bitmap.GetPixel(0, 0);

            int colOpaquePixel = 0;

            for (int row = 0; row < bitmap.Height - 1; row++)
            {

                colOpaquePixel = 0;

                for (int col = 0; col < bitmap.Width - 1; col++)
                {

                    if (bitmap.GetPixel(col, row) != colorTransparent)
                    {

                        colOpaquePixel = col;

                        int colNext = col;

                        for (colNext = colOpaquePixel; colNext < bitmap.Width; colNext++)
                            if (bitmap.GetPixel(colNext, row) == colorTransparent)
                                break;

                        graphicsPath.AddRectangle(new Rectangle(colOpaquePixel,
                         row, colNext - colOpaquePixel, 1));

                        col = colNext;
                    }
                }
            }

            return graphicsPath;
        } 

    }
}
