using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using SFC_Tools.Classes;
using SFC_Tools.Model;
using System.Text.RegularExpressions;

namespace SFC_Tools.Forms
{
    public partial class ucDS05BomImport : ucParentControl
    {
        private const string ISACTIVE = "Y";
        private const string CREATEBY = "DS05";
        private const string UPDATEBY = "DS05";
        private const string CLIENTID = "DeRun";
        private const string PLANTID = "DeRun";
        private string BomNo = string.Empty;
        public ucDS05BomImport()
        {
            InitializeComponent();
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {

            if (string.IsNullOrEmpty(this.txtFilePath.Text))
            {
                this.txtFilePath.Text = string.Empty;
                OpenFileDialog ofd = new OpenFileDialog();
                ofd.Filter = "xlsx files (*.xlsx)|*.xlsx|xls files (*.xls)|*.xls|All files (*.*)|*.*";

                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    this.txtFilePath.Text = ofd.FileName;
                    /*NPOIRWExcel rwExcel = new NPOIRWExcel(ofd.FileName);
                    rwExcel.GetWorkSheet(CBomConfigration.iMaxSheetNo);
                    MessageBox.Show(rwExcel.GetModelName());
                    DoFun( a=> "hello"+a);*/
                }
            }
            if(!string.IsNullOrEmpty(txtFilePath.Text))
                ImportBomFromExcel(txtFilePath.Text);
            txtFilePath.SelectAll();
            txtFilePath.Focus();
        }

        private void ImportBomFromExcel(string sPath)
        {
            DataTable dt = NPOIHelper.ReadExcelToDataTable(sPath, 1);
            this.dataGridView1.DataSource = dt;
            string sRoot = GetRootNode(dt);
            DataRow[] rows = dt.Rows.Cast<DataRow>().Where(row => Regex.IsMatch(row[1].ToString(), @"^" + sRoot + "$")).ToArray();
            if (rows.Count() != 1)
                throw new Exception("Bom Formate Wrong!");
            else
            {
                DataRow drRoot=null;
                foreach (DataRow dr in rows)
                {
                    drRoot = dr;
                }
                TreeNode root = new TreeNode(sRoot);
                root.Tag = new MDBomItem() { ItemCount = 0, BomItem = drRoot, IsAltGroup = false };
                this.tvBom.Nodes.Add(root);
                SetSubNode(dt, root);
                lblTotalCnt.Text = "Total:" + dt.Rows.Count;
                lblTreeNodesCnt.Text = "Deal Count:";
            }
        }
        
        /// <summary>
        /// 获取根节点，成品料号
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        private string GetRootNode(DataTable dt)
        {
            string sRoot = string.Empty;
            DataRow[] rows = dt.Rows.Cast<DataRow>().Where(row => Regex.IsMatch(row[1].ToString(), @".+@")).ToArray();
            foreach(DataRow dr in rows)
            {
                sRoot= dr[1].ToString();
            }
            if(!string.IsNullOrEmpty(sRoot))
                sRoot = sRoot.Substring(0, sRoot.Length - 1);            
            return sRoot;
        }

        /// <summary>
        /// 设置非替代料的下阶料
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="node"></param>
        private void SetSubNode(DataTable dt,TreeNode node)
        {
            string sCurrent = node.Text;

            DataRow[] rows = dt.Rows.Cast<DataRow>().Where(row => Regex.IsMatch(row[1].ToString(), @"^" + sCurrent +"-"+ "[0-9]*[1-9][0-9]*$")).ToArray();
            foreach (DataRow dr in rows)
            {
                if (CheckItemExists(dt, dr[1].ToString()) <= 1)
                {
                    TreeNode tnNode = new TreeNode(dr[1].ToString());
                    tnNode.Tag = new MDBomItem() { ItemCount = 0, BomItem = dr, IsAltGroup =false};
                    node.Nodes.Add(tnNode);
                    
                    SetSubNode(dt, tnNode);
                }
                else
                {
                    int iPos = 0;
                    DataRow[] aRow = dt.Rows.Cast<DataRow>().Where(row => Regex.IsMatch(row[1].ToString(), @"^" + sCurrent + "$")).ToArray();
                    foreach (DataRow dr1 in aRow)
                    {
                        iPos = dt.Rows.IndexOf(dr1);
                    }
                   
                    int iLen = SetSubNodeByRowID(dt, iPos, node);
                    node.Tag = new MDBomItem() { ItemCount = iLen, BomItem = dr, IsAltGroup = false };
                    node.Text = node.Text + "XX" + iLen;
                    break;
                }
            }

            //替代料信息
            DataRow[] insteadRows = dt.Rows.Cast<DataRow>().Where(row => Regex.IsMatch(row[1].ToString(), @"^" + sCurrent + "&$")).ToArray();

            for (int i = 0; i < insteadRows.Count(); i++)
            {
                string sSuffix = string.Empty;
                string sNode = insteadRows[i][1].ToString();
                for (int j = 0; j < i; j++)
                {
                    sSuffix = sSuffix + "&";
                }
                sNode = sNode + sSuffix;
                TreeNode tnNode = new TreeNode(sNode);
                tnNode.Tag = new MDBomItem() { ItemCount = 0, BomItem = insteadRows[i], IsAltGroup = false };
                node.Parent.Nodes.Add(tnNode);
               
                int iPos = dt.Rows.IndexOf(insteadRows[i]);
                int iLen=((MDBomItem)node.Tag).ItemCount;
                SetSubNodeByRowID(dt, iPos, iLen, tnNode);
            }
        }

        /// <summary>
        /// 替代料下阶料
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="node"></param>
        private void SetSubNodeA(DataTable dt, TreeNode node)
        {
            string sCurrent = node.Text;
            int iPos=sCurrent.IndexOf(@"&");
            if (iPos >= 0)
                sCurrent = sCurrent.Substring(0,iPos);
            DataRow[] rows = dt.Rows.Cast<DataRow>().Where(row => Regex.IsMatch(row[1].ToString(), @"^" + sCurrent + "-" + "[0-9]*[1-9][0-9]*$")).ToArray();
            foreach (DataRow dr in rows)
            {
                if (CheckItemExists(dt, dr[1].ToString()) <= 1)
                {
                    TreeNode tnNode = new TreeNode(dr[1].ToString());
                    tnNode.Tag = new MDBomItem() { ItemCount = 0, BomItem = dr, IsAltGroup = false };
                    node.Nodes.Add(tnNode);
                    
                    SetSubNodeA(dt, tnNode);
                }
            }
        }

        /// <summary>
        /// 抓取被替代料的下阶料
        /// </summary>
        /// <param name="dt">目标datatable</param>
        /// <param name="iRow">起始行</param>
        /// <param name="node">当前节点</param>
        /// <returns></returns>
        private int SetSubNodeByRowID(DataTable dt,int iRow,TreeNode node)
        {
            string sNdtext = node.Text;
            DataTable dtTemp = dt.Clone();
            for (int i = iRow; i < dt.Rows.Count; i++)
            {
                if (dt.Rows[i][1].ToString().IndexOf(@"&") >= 0)
                    break;
                else
                    dtTemp.ImportRow(dt.Rows[i]);
            }
            if (dtTemp.Rows.Count > 0)
                SetSubNode(dtTemp,node);

            return dtTemp.Rows.Count;
        }

        /// <summary>
        /// 替代料下阶料
        /// </summary>
        /// <param name="dt">目标datatable</param>
        /// <param name="iRow">起始行</param>
        /// <param name="iLen">长度</param>
        /// <param name="node">当前节点</param>
        private void SetSubNodeByRowID(DataTable dt, int iRow,int iLen, TreeNode node)
        {
            string sNdtext = node.Text;
            DataTable dtTemp = dt.Clone();
            for (int i = iRow; i < iLen+iRow; i++)
            {
               dtTemp.ImportRow(dt.Rows[i]);
            }
            if (dtTemp.Rows.Count > 0)
            {
                ((MDBomItem)node.Tag).IsAltGroup = true;
                SetSubNodeA(dtTemp, node);
            }

        }

        /// <summary>
        /// 检查是否是替代料组
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="sItem"></param>
        /// <returns>1：非替代料组 >1:是替代料组，并且返回组别数量</returns>
        private int CheckItemExists(DataTable dt,string sItem)
        {
            DataRow[] rows = dt.Rows.Cast<DataRow>().Where(row => Regex.IsMatch(row[1].ToString(), @"^"+sItem+"$")).ToArray();
            return rows.Count();
        }
        private void DoFun(Func<string,string>Fun)
        {
            MessageBox.Show(Fun("The Damned World!"));
        }

        private void SearchTreeNodeInfo(TreeView tv)
        {
            TreeNode root = tv.Nodes[0];
            BomNo = ((DataRow)(((MDBomItem)root.Tag).BomItem))[2] + "-" + ((DataRow)(((MDBomItem)root.Tag).BomItem))[3];
            SaveMainInfo(((DataRow)(((MDBomItem)root.Tag).BomItem)));
            SearchSubTreeNode(root);
            
        }

        private void SaveMainInfo(DataRow dr)
        {
            MDBomMainInfo mBomMain = new MDBomMainInfo();
            mBomMain.IsActive = ISACTIVE;
            mBomMain.CreatedBy = CREATEBY;
            mBomMain.UpdatedBy = UPDATEBY;
            mBomMain.ClientID = CLIENTID;
            mBomMain.PlantID = PLANTID;
            mBomMain.MaterialNo = dr[2].ToString();
            mBomMain.BomNo = BomNo;
            mBomMain.SourceType = dr[9].ToString();
            mBomMain.NetWeight = Convert.ToDecimal(dr[8].ToString());
            mBomMain.Description = dr[10] + " " + dr[11];
            CMESAccess.SaveBomMainInfo(mBomMain);
        }

        private void SaveDetailInfo(DataRow dr,string sLevel,string sParentMaterial)
        {
            MDBomDetailInfo mBomDetail = new MDBomDetailInfo();
            mBomDetail.IsActive = ISACTIVE;
            mBomDetail.CreatedBy = CREATEBY;
            mBomDetail.UpdatedBy = UPDATEBY;
            mBomDetail.ClientID = CLIENTID;
            mBomDetail.PlantID = PLANTID;
            mBomDetail.BomNo = BomNo;
            mBomDetail.Description = dr[10] + " " + dr[11];
            mBomDetail.MaterialLevel = sLevel;
            string sSeq = dr[1].ToString();
            sSeq=sSeq.Substring(sSeq.Length - 1, 1);
            mBomDetail.SeqNo = Convert.ToInt32(sSeq);
            mBomDetail.ParentMaterialNo = sParentMaterial;

            CMESAccess.SaveBomDetailInfo(mBomDetail);
        }

        private void SaveAltInfo()
        {
            MDBomAltInfo mBomAlt = new MDBomAltInfo();
            mBomAlt.IsActive = ISACTIVE;
            mBomAlt.CreatedBy = CREATEBY;
            mBomAlt.UpdatedBy = UPDATEBY;
            mBomAlt.ClientID = CLIENTID;
            mBomAlt.PlantID = PLANTID;
            mBomAlt.BomNo = BomNo;          
          
            CMESAccess.SaveBomAltInfo(mBomAlt);
        }

        private void SearchSubTreeNode(TreeNode tn)
        {
            foreach (TreeNode tnNodes in tn.Nodes)
            {
                //MessageBox.Show(tnNodes.Text);
               /*
                if(((MDBomItem)tnNodes.Tag).IsAltGroup)
                    MessageBox.Show(((DataRow)(((MDBomItem)tnNodes.Tag).BomItem))[1].ToString() + "\r\n" +
                    ((DataRow)(((MDBomItem)tnNodes.Tag).BomItem))[2].ToString()+"\r\n"+"OK");
                else
                MessageBox.Show(((DataRow)(((MDBomItem)tnNodes.Tag).BomItem))[1].ToString()+"\r\n"+
                    ((DataRow)(((MDBomItem)tnNodes.Tag).BomItem))[2].ToString());*/
                SaveDetailInfo(((DataRow)(((MDBomItem)tnNodes.Tag).BomItem)), tnNodes.Level.ToString(), ((DataRow)(((MDBomItem)tnNodes.Parent.Tag).BomItem))[2].ToString());
                SearchSubTreeNode(tnNodes);
            }
        }

        private void btnTest_Click(object sender, EventArgs e)
        {
            SearchTreeNodeInfo(this.tvBom);
        }
    }


    public class MDBomItem
    {
        public int ItemCount { set; get; }
        public DataRow BomItem { set; get; }
        public bool IsAltGroup { set; get; }
             
    }

}
