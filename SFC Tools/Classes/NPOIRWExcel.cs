using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using NPOI;
using NPOI.HSSF.UserModel;
using NPOI.XSSF.UserModel;
using NPOI.SS.UserModel;
using System.IO;


namespace SFC_Tools.Classes
{
    class NPOIRWExcel
    {
        IWorkbook workbook;
        ISheet sheet;
        IRow row;
        ICell cell;

        private int iCurrentRow; //the current row of the item
        private int iStartRow;// The start row of the item
        private int iMaxSheetNo;//The max sheet no
        private int iMinSheetNo;// the min sheet no
        private int iModelCol;//The COL NO. of the model name
        private int iModelRow;//The ROW NO. of the model name
        private int iItemCol;//The COL NO. of the Item
        private int iHhpnCol;//The COL NO. of HH.PN
        private int iDescCol;//The COL NO. of supplier
        private int iSupplierCol;//The COL NO. of Supplier
        private int iSupplierPnCol;//The COL NO. of Supplier P/N
        private int iLocationCol;//The COL No.of Location
        private int iLocationUsageCol;//The COL NO. of Location

        private int iSheetCount;//The Sheets count of workbook

        private string sFilePath;
        private bool bEnable = false;
        private bool bEnd = false;
        private int iEmptyRow = 0;//Empty Row Count;

        /// <summary>
        /// Initialize the parameters of the Excel.
        /// </summary>
        /// <param name="sPath">file path</param>
        public NPOIRWExcel(string sPath)
        {
            this.sFilePath = sPath;

            iStartRow = CBomConfigration.iStartRow;
            iMaxSheetNo = CBomConfigration.iMaxSheetNo;
            iMinSheetNo = CBomConfigration.iMinSheetNo;
            iModelCol = CBomConfigration.iModelCol;
            iModelRow = CBomConfigration.iModelRow;
            iItemCol = CBomConfigration.iItemCol;
            iHhpnCol = CBomConfigration.iHhpnCol;
            iDescCol = CBomConfigration.iDescCol;
            iSupplierCol = CBomConfigration.iSupplierCol;
            iSupplierPnCol = CBomConfigration.iSupplierPnCol;
            iLocationCol = CBomConfigration.iLocationCol;
            iLocationUsageCol = CBomConfigration.iLocationUsageCol;
            bEnable = true;

            using (FileStream fs = new FileStream(sPath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {    
                try
                {
                    //excel 2007 or even higher
                    workbook = new XSSFWorkbook(fs);
                }
                catch (Exception ex)
                {
                    try
                    {
                        //excel 2003 or even lower
                        workbook = new HSSFWorkbook(fs);
                    }
                    catch (Exception exx)
                    {
                        bEnable = false;
                        throw new Exception("ReadExcelToDataTable:" + exx.Message.ToString());
                    }
                }
            }
            iSheetCount=workbook.NumberOfSheets;
            if (iMaxSheetNo > iSheetCount)
            {
                CBomConfigration.iMaxSheetNo = iSheetCount;
                iMaxSheetNo = iSheetCount;
            }
        }

        /// <summary>
        /// Excel file readable status
        /// True:Excel file available
        /// False:Excel file unavailable
        /// </summary>
        /// <returns>Boolean result</returns>
        public bool Enable()
        {
            return this.bEnable;
        }

        /// <summary>
        /// Check if read excel file finished
        /// </summary>
        /// <returns>Boolean results</returns>
        public bool End()
        {
            return this.bEnd;
        }

        /// <summary>
        /// Prepare for the current work sheet.
        /// </summary>
        /// <param name="iIndex"></param>
        public void GetWorkSheet(int iIndex)
        {
            sheet = workbook.GetSheetAt(iIndex);
            iCurrentRow = iStartRow;
            iEmptyRow = 0;
            bEnd = true;
        }

        /// <summary>
        /// Move to next row.
        /// </summary>
        /// <returns></returns>
        public bool NextRow()
        {
            iCurrentRow++;
            /*if (string.IsNullOrEmpty(GetHhpn()) && string.IsNullOrEmpty(GetSupplier()) && string.IsNullOrEmpty(GetSupplierPn()) &&
                string.IsNullOrEmpty(GetLocation()) )
                iEmptyRow++;
            else
                iEmptyRow=0;
            */
            if (iEmptyRow > 10)
            {
                bEnd = false;
                return false;
            }
            else
            {
                bEnd = true;
                return true;
            }
        }

        /// <summary>
        /// Get the Model Name.
        /// </summary>
        /// <returns></returns>
        public string GetModelName()
        {
            try
            {
                return sheet.GetRow(iModelRow).GetCell(iModelCol).ToString();
            }
            catch
            {
                return string.Empty;
            }

        }

        /// <summary>
        /// Get The Item.
        /// </summary>
        /// <returns></returns>
        public string GetItem()
        {
            try
            {
                return sheet.GetRow(iCurrentRow).GetCell(iItemCol).ToString();
            }
            catch
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// Get the HH.P/N
        /// </summary>
        /// <returns></returns>
        public string GetHhpn()
        {
            try
            {
                return sheet.GetRow(iCurrentRow).GetCell(iHhpnCol).ToString();
            }
            catch
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// Get the Description
        /// </summary>
        /// <returns></returns>
        public string GetDesc()
        {
            try
            {
                return sheet.GetRow(iCurrentRow).GetCell(iDescCol).ToString();
            }
            catch
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// Get the Supplier.
        /// </summary>
        /// <returns></returns>
        public string GetSupplier()
        {
            try
            {
                return sheet.GetRow(iCurrentRow).GetCell(iSupplierCol).ToString();
            }
            catch
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// Get the Supplier P/N.
        /// </summary>
        /// <returns></returns>
        public string GetSupplierPn()
        {
            try
            {
                return sheet.GetRow(iCurrentRow).GetCell(iSupplierPnCol).ToString();
            }
            catch
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// Get the LoacationUsage.
        /// </summary>
        /// <returns></returns>
        public string GetLoacationUsage()
        {
            try
            {
                return sheet.GetRow(iCurrentRow).GetCell(iLocationUsageCol).ToString();
            }
            catch
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// Get the Location.
        /// </summary>
        /// <returns></returns>
        public string GetLocation()
        {
            try
            {
                return sheet.GetRow(iCurrentRow).GetCell(iLocationCol).ToString();
            }
            catch
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// Get the Value of specified Cell
        /// </summary>
        /// <returns></returns>
        public string GetCellValue(int iRow,int iCol)
        {
            try
            {
                return sheet.GetRow(iRow).GetCell(iCol).ToString();
            }
            catch
            {
                return string.Empty;
            }
        }
        public void Close()
        {
            workbook.Clear();
        }

        
    }


    /// <summary>
    /// All Positions in BOM Excel
    /// </summary>
    public class CBomConfigration
    {
        public static int iStartRow = 6;//The start row of the item
        public static int iMaxSheetNo = 1;//The max sheet no
        public static int iMinSheetNo = 1;//The max sheet no
        public static int iModelCol = 2;//The COL NO. of model name
        public static int iModelRow = 4;//The Row NO. of model name
        public static int iItemCol = 1;//The COL NO. of Item
        public static int iHhpnCol = 2;//The COL NO. of HH.PN
        public static int iDescCol = 3;//The COL NO. of Description
        public static int iSupplierCol = 4;//The COL NO. of Supplier
        public static int iSupplierPnCol = 5;//The COL NO. of Supplier P/N
        public static int iLocationUsageCol = 6;//The COL NO. of Location
        public static int iLocationCol = 7;//The COL NO. of Location
    }
}
