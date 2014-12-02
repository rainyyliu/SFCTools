using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;
using NPOI;
using NPOI.Util;
using NPOI.HSSF;
using NPOI.HSSF.UserModel;
using NPOI.HSSF.Util;
using NPOI.HPSF;
using NPOI.XSSF;
using System.IO;
using NPOI.XSSF.UserModel;
using NPOI.SS.UserModel;

namespace SFC_Tools.Classes
{
    class NPOIHelper
    {
        /// <summary>
        /// 读取Excel文件
        /// 默认第一行为文件头
        /// </summary>
        /// <param name="sPath">Excel 文档路径</param>
        /// <returns>DataTable形式返回整个文档的内容</returns>
        public static DataTable ReadExcelToDataTable(string sPath,int iSheet=0)
        {
            DataTable dt = new DataTable();

            IWorkbook workbook;
            ISheet sheet;
            IRow headerRow;
            ICell cell;
            try
            {
                using (FileStream fs = new FileStream(sPath, FileMode.Open, FileAccess.Read,FileShare.ReadWrite))
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
                            throw new Exception("ReadExcelToDataTable:" + exx.Message.ToString());
                        }
                    }
                }
                sheet = workbook.GetSheetAt(iSheet);
                System.Collections.IEnumerator rows = sheet.GetRowEnumerator();
                string xx = sheet.SheetName;
                headerRow = sheet.GetRow(4);
                int iCellCount = headerRow.LastCellNum;

                for (int j = 0; j < iCellCount; j++)
                {
                    cell = headerRow.GetCell(j);
                    dt.Columns.Add(cell.ToString());
                }
                //行
                for (int i = (sheet.FirstRowNum + 5); i < sheet.LastRowNum; i++)
                {
                    IRow row = sheet.GetRow(i);
                    if (row == null)
                        continue;
                    DataRow dr = dt.NewRow();
                    //列
                    for (int j = row.FirstCellNum; j < iCellCount; j++)
                    {
                        if (row.GetCell(j) != null)
                            dr[j] = row.GetCell(j).ToString();
                    }
                    if(dr[0].ToString().Trim()!="" && dr[1].ToString().Trim()!="")
                        dt.Rows.Add(dr);
                }
            }
            catch (Exception e)
            {
                LogHelper.WriteError("ReadExcelToDataTable:"+e.Message.ToString());
                throw new Exception("ReadExcelToDataTable:" + e.Message.ToString());
            }
            return dt;
        }
    }
}
