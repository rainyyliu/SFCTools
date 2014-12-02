using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using SFC_Tools.Classes;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using PriviAuthentication;
using PriviAuthentication.Model;
using System.Collections;
using System.Threading;

namespace SFC_Tools.Forms
{
    public partial class ucPCMSMaintain : UserControl
    {

        MySqlDAL mySqlDb;
        [DllImportAttribute("user32.dll")]
        private static extern bool AnimateWindow(IntPtr hwnd, int dwTime, int dwFlags);
        public const Int32 AW_CENTER = 0x00000010;

        /// <summary>
        /// AW_HIDE: 隐藏窗口
        /// </summary>
        public const Int32 AW_HIDE = 0x00010000;

        /// <summary>
        /// AW_ACTIVE: 激活窗口, 在使用了 AW_HIDE 效果时不可使用此效果
        /// </summary>
        public const Int32 AW_ACTIVATE = 0x00020000;

        /// <summary>
        /// AW_SLIDE : 使用滑动类型, 默认为该类型. 当使用 AW_CENTER 效果时, 此效果被忽略
        /// </summary>
        public const Int32 AW_SLIDE = 0x00040000;
        private bool bIsOpen = true;
        protected Module module;
        protected Role myRole;
        protected ModuleInfo moduleInfo;

        public ucPCMSMaintain()
        {
            InitializeComponent();
            this.SetStyle(ControlStyles.UserPaint, true);
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            this.SetStyle(ControlStyles.DoubleBuffer, true);
            base.SetStyle(ControlStyles.UserPaint |// 控件将自行绘制，而不是通过操作系统来绘制
            ControlStyles.OptimizedDoubleBuffer |// 该控件首先在缓冲区中绘制，而不是直接绘制到屏幕上，这样可以减少闪烁             ControlStyles.AllPaintingInWmPaint | // 控件将忽略 WM_ERASEBKGND 窗口消息以减少闪烁
            ControlStyles.ResizeRedraw| // 在调整控件大小时重绘控件
            ControlStyles.SupportsTransparentBackColor,// 控件接受 alpha 组件小于 255 的 BackColor 以模拟透明
            true);  // 设置以上值为 true 
            base.UpdateStyles();
            //AnimateWindow(this.btnConnect.Handle, 1000, AW_SLIDE + AW_CENTER);
            this.pnlRightCov.Parent = this.pnlTop;
            this.pnlLeftCov.Location = new Point(0,0);            
            this.pnlRightCov.Location = new Point(78, 0);
            //this.pnlRightCov.Container.Dispose();
            //this.pnlRightCov.Container.Add(this.pnlTop);
            
            txtModuleID.Enabled = false;

        }

        private void ucPCMSMaintain_Load(object sender, EventArgs e)
        {
           
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            try
            {
                mySqlDb = new MySqlDAL(txtMySqlDb.Text.Trim());
                this.trvMenSec.Nodes.Clear();
                Thread thdGetTreeNode = new Thread(new ThreadStart(GetTreeRootNode));
                thdGetTreeNode.Start();
                this.btnConnect.Enabled = false;
                this.btnDisConn.Enabled = true;
                this.txtMySqlDb.Enabled = false;
                bIsOpen = true;
                timer1.Enabled = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            
        }

        private void btnGenerate_Click(object sender, EventArgs e)
        {
            mySqlDb.DisConn();
            this.btnConnect.Enabled = true;
            this.btnDisConn.Enabled = false;
            this.txtMySqlDb.Enabled = true;
            bIsOpen = false;
            this.timer1.Enabled = true;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (this.bIsOpen)
            {
                this.pnlLeftCov.SetBounds(this.pnlLeftCov.Location.X-10, 0, pnlLeftCov.Width, pnlLeftCov.Height);
                this.pnlRightCov.SetBounds(this.pnlRightCov.Location.X + 10, 0, pnlRightCov.Width, pnlRightCov.Height);
                if (this.pnlLeftCov.Location.X <= -400)
                {
                    timer1.Enabled = false;
                }
            }
            else
            {
                this.pnlLeftCov.SetBounds(this.pnlLeftCov.Location.X + 10, 0, pnlLeftCov.Width, pnlLeftCov.Height);
                this.pnlRightCov.SetBounds(this.pnlRightCov.Location.X - 10, 0, pnlRightCov.Width, pnlRightCov.Height);
                if (this.pnlLeftCov.Location.X >= 0)
                {
                    timer1.Enabled = false;
                }
            }

        }

        private void GetTreeRootNode()
        {
            this.module = new Module();
            TreeNode myTree = new TreeNode("FENIX SFC");
            IList rootNode = module.getModuleList("", "-1", "", "", "", "0");
            this.moduleInfo = (ModuleInfo)rootNode[0];
            GetSubTreeNode(moduleInfo.Id,moduleInfo.Name,myTree,moduleInfo.ModuleCode);
            AddSubTreeNode(myTree);
        }
        delegate void dlgAddTreeNode(TreeNode TN);
        private void AddSubTreeNode(TreeNode TN)
        {
            if (this.trvMenSec.InvokeRequired)
            {
                dlgAddTreeNode d = new dlgAddTreeNode(AddSubTreeNode);
                this.BeginInvoke(d,new object[]{TN});
            }
            else
                this.trvMenSec.Nodes.Add(TN);
        }

        private void GetSubTreeNode(string moduleId,string nodename,TreeNode tNode,string moduleCode)
        {
            module = new Module();
            IList subNode = module.getModuleList("", moduleId, "", "", "", "");
            for (int i = 0; i < subNode.Count; i++)
            {
                moduleInfo = (ModuleInfo)subNode[i];
                TreeNode subTreeNode = new TreeNode(moduleInfo.Name);
                if (moduleInfo.IsFolder == "0")
                {
                    subTreeNode.ImageIndex = 0;
                }
                else
                    subTreeNode.ImageIndex = 1;
                subTreeNode.Tag = moduleInfo;
                tNode.Nodes.Add(subTreeNode);
                GetSubTreeNode(moduleInfo.Id, moduleInfo.Name, subTreeNode, moduleInfo.ModuleCode);
            }
        }

        private void trvMenSec_AfterSelect(object sender, TreeViewEventArgs e)
        {
            module = new Module();
            initControls();
            //IList subNode = module.getModuleList("", e.Node.FullPath, "", "", "", "");
            moduleInfo = (ModuleInfo)e.Node.Tag;
            if (moduleInfo != null)
            {
                this.txtModuleID.Text = moduleInfo.Id;
                this.txtModuleName.Text = moduleInfo.Name;
                this.txtModuleDesc.Text = moduleInfo.Description;
                this.txtSortNo.Text = moduleInfo.SortNo;
                if (moduleInfo.IsFolder == "0")
                {
                    this.chkIsFolder.Checked = true;
                }
                else
                {   
                    this.chkIsFolder.Checked = false;
                    this.txtFilePath.Text = moduleInfo.WebFilePath;
                }
                if (moduleInfo.Disabled == "0")
                {
                    this.chkEnable.Checked = true;
                }
                else
                    this.chkEnable.Checked = false;
            }
        }

        private void initControls()
        {
            this.txtFilePath.Text = "";
            this.txtModuleDesc.Text = "";
            this.txtModuleID.Text = "";
            this.txtModuleName.Text = "";
            this.txtSortNo.Text = "";
            this.chkEnable.Checked = false;
            this.chkIsFolder.Checked = false;
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {

        }

        private void btnInsert_Click(object sender, EventArgs e)
        {
            myRole = new Role();
            module=new Module();
            string sModuleID=module.getNewModuleID();
            MessageBox.Show(DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss"));
            return;
            myRole.setRolePrivilege("29", sModuleID,"0","Rain.Liu",DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss"));
            myRole.setRolePrivilege("29", sModuleID, "1", "Rain.Liu", DateTime.Now.ToString("yyyy-mm-dd MM:HH:SS"));
            myRole.setRolePrivilege("29", sModuleID, "2", "Rain.Liu", DateTime.Now.ToString("yyyy-mm-dd MM:HH:SS"));
            myRole.setRolePrivilege("29", sModuleID, "3", "Rain.Liu", DateTime.Now.ToString("yyyy-mm-dd MM:HH:SS"));
            myRole.setRolePrivilege("29", sModuleID, "4", "Rain.Liu", DateTime.Now.ToString("yyyy-mm-dd MM:HH:SS"));
           
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {

        }
    }
}
