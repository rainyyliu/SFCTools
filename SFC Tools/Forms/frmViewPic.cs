using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Imaging;


namespace SFC_Tools
{
    public partial class frmViewPic : Form
    {
        Image imgView;
        
        public frmViewPic()
        {
            InitializeComponent();
        }

        private void frmViewPic_Load(object sender, EventArgs e)
        {
            
        }

        public void initFormPic(Image imgInput)
        {
            try
            {
                this.imgView = imgInput;
                this.pictureBox1.Image = imgView;
                pictureBox1.Width = imgView.Width;
                pictureBox1.Height = imgView.Height;
                // pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            }
            catch (Exception ex)
            { }
        }
        //Test pic
        public static void DoConvert(string ASrcFileName, string ADestFileName, int AWidth, int AHeight, int AQuality)
        {
            Image ASrcImg = Image.FromFile(ASrcFileName);
            if (ASrcImg.Width <= AWidth && ASrcImg.Height <= AHeight)
            {//图片的高宽均小于目标高宽，直接保存 
                ASrcImg.Save(ADestFileName);
                return;
            }
            double ADestRate = AWidth * 1.0 / AHeight;
            double ASrcRate = ASrcImg.Width * 1.0 / ASrcImg.Height;
            //裁剪后的宽度 
            double ACutWidth = ASrcRate > ADestRate ? (ASrcImg.Height * ADestRate) : ASrcImg.Width;
            //裁剪后的高度 
            double ACutHeight = ASrcRate > ADestRate ? ASrcImg.Height : (ASrcImg.Width / ADestRate);
            //待裁剪的矩形区域，根据原图片的中心进行裁剪 
            Rectangle AFromRect = new Rectangle(Convert.ToInt32((ASrcImg.Width - ACutWidth) / 2), Convert.ToInt32((ASrcImg.Height - ACutHeight) / 2), (int)ACutWidth, (int)ACutHeight);
            //目标矩形区域 
            Rectangle AToRect = new Rectangle(0, 0, AWidth, AHeight);

            Image ADestImg = new Bitmap(AWidth, AHeight);
            Graphics ADestGraph = Graphics.FromImage(ADestImg);
            ADestGraph.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
            ADestGraph.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            ADestGraph.DrawImage(ASrcImg, AToRect, AFromRect, GraphicsUnit.Pixel);

            //获取系统image/jpeg编码信息 
            ImageCodecInfo[] AInfos = ImageCodecInfo.GetImageEncoders();
            ImageCodecInfo AInfo = null;
            foreach (ImageCodecInfo i in AInfos)
            {
                if (i.MimeType == "image/jpeg")
                {
                    AInfo = i;
                    break;
                }
            }
            //设置转换后图片质量参数 
            EncoderParameters AParams = new EncoderParameters(1);
            AParams.Param[0] = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, (long)AQuality);
            //保存 
            ADestImg.Save(ADestFileName, AInfo, AParams);
        }

        private void pictureBox1_DoubleClick(object sender, EventArgs e)
        {
            /*this.pictureBox1.Image.Save(@"C:\temp.bmp");
            DoConvert(@"C:\temp.bmp", @"C:\temp1.bmp", 800,400, 100);
            this.pictureBox1.Image = Image.FromFile(@"C:\temp1.bmp");*/
            Bitmap bitNewPic,bitImptPic;
            bitImptPic=(Bitmap)this.pictureBox1.Image;
            //bitNewPic = this.mySmallPic(bitImptPic, pictureBox1.Image.Width - 20, pictureBox1.Image.Height - 10);
            bitNewPic = SmallPic(bitImptPic, pictureBox1.Image.Width - 10);
            this.pictureBox1.Image = bitNewPic;
        }
        /// <summary>
        /// 缩小图片
        /// </summary>
        /// <param name="strOldPic">源图文件名(包括路径)</param>
        /// <param name="strNewPic">缩小后保存为文件名(包括路径)</param>
        /// <param name="intWidth">缩小至宽度</param>
        /// <param name="intHeight">缩小至高度</param>
        public Bitmap mySmallPic(Bitmap bmpOldPic,int intWidth, int intHeight)
        {

            System.Drawing.Bitmap objPic, objNewPic;
            try
            {
                objPic = new System.Drawing.Bitmap(bmpOldPic);
                objNewPic = new System.Drawing.Bitmap(objPic, intWidth, intHeight);
                return objNewPic;

            }
            catch (Exception exp) { throw exp; }
            finally
            {
                objPic = null;
                objNewPic = null;
            }
        }

        public Bitmap SmallPic(Bitmap bmpOldPic, int intWidth)
        {

            System.Drawing.Bitmap objPic, objNewPic;
            try
            {
                objPic = bmpOldPic;
                int intHeight = (intWidth * objPic.Height) / objPic.Width;
                objNewPic = new System.Drawing.Bitmap(objPic, intWidth, intHeight);
                 //objNewPic = new System.Drawing.Bitmap(objPic, 290, 290);
                return objNewPic;

            }
            catch (Exception exp) { throw exp; }
            finally
            {
                objPic = null;
                objNewPic = null;
            }
        }
        //************************************************************//
        //下面给出三个简单的方法，后面两个方法是扩展，估计有时用得着
        //************************************************************//
        /// <summary>
        /// 缩小图片
        /// </summary>
        /// <param name="strOldPic">源图文件名(包括路径)</param>
        /// <param name="strNewPic">缩小后保存为文件名(包括路径)</param>
        /// <param name="intWidth">缩小至宽度</param>
        /// <param name="intHeight">缩小至高度</param>
        public void SmallPic(string strOldPic, string strNewPic, int intWidth, int intHeight)
        {

            System.Drawing.Bitmap objPic, objNewPic;
            try
            {
                objPic = new System.Drawing.Bitmap(strOldPic);
                objNewPic = new System.Drawing.Bitmap(objPic, intWidth, intHeight);
                objNewPic.Save(strNewPic);

            }
            catch (Exception exp) { throw exp; }
            finally
            {
                objPic = null;
                objNewPic = null;
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            //this.pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            Screen []sc = Screen.AllScreens;
            //MessageBox.Show(.ToString());
            if(pictureBox1.Width<=sc[0].WorkingArea.Width)
            {
                this.pictureBox1.Size = new Size(this.pictureBox1.Size.Width + 50, this.pictureBox1.Size.Height + 25);
                this.Height = this.Height + 25;
                this.Width = this.Width + 50;
                this.pnlContainer.Width = this.pnlContainer.Width + 50;
                this.pnlContainer.Height = this.pnlContainer.Height + 25;
            }
           // this.Height = this.Height + 100;
        }

    }
}


