/*************************************************************** 
*    Create Tag:Solomon20090410
*    Review Tag:
*    Description: Read excel file
*    Version: 1.0.0.0
***************************************************************/

using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Reflection;
using Microsoft.Office.Interop.Excel;

namespace SFC_Tools.FileWR.ExcelFile
{
    public class ExcelFileRead
    {
        
        protected int sheetCount;//The sheets count of workbook
        protected Microsoft.Office.Interop.Excel.Application ExcelApp;// Excel Application
        protected Microsoft.Office.Interop.Excel._Workbook ExcelBook;//Excel Workbook
        protected Microsoft.Office.Interop.Excel.Sheets ExcelSheets;//Excel Sheets
        protected Microsoft.Office.Interop.Excel._Worksheet ExcelSheet;//Excel Sheet

        protected bool enable = false;//Excel Application can use or not
        protected string filePath;//Excel file path
        protected bool isRead = true;

        public ExcelFileRead()
        {

        }

        public bool Enable
        {
            get
            {
                return enable;
            }
        }

        protected virtual void SetReadFlag()
        {
            isRead = true;
        }

        public void OpenExcel(string fullFileName)
        {
            this.filePath = fullFileName;
            if (ExcelApp == null)
            {
                ExcelApp = new Microsoft.Office.Interop.Excel.Application();
            }
            if (ExcelApp != null)
            {
                enable = true;
                ExcelApp.Visible = false;
                ExcelApp.UserControl = false;
                SetReadFlag();
                if (!File.Exists(fullFileName))
                {
                    if (isRead)
                    {
                        return;
                    }
                    ExcelBook = ExcelApp.Workbooks.Add(Microsoft.Office.Interop.Excel.XlWBATemplate.xlWBATWorksheet);
                }
                else
                {
                    ExcelBook = ExcelApp.Workbooks.Open(fullFileName, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value);
                }
                ExcelSheets = ExcelBook.Worksheets;
                sheetCount = ExcelSheets.Count;
                SetWorkSheet(1);
            }
            else
            {
                enable = false;
            }
        }

        public void SetWorkSheet(int index)
        {
            if (index == 0)
            {
                index = 1;
            }
            if (index > sheetCount)
            {
                index = sheetCount;
            }
            ExcelSheet = (Microsoft.Office.Interop.Excel._Worksheet)ExcelSheets.get_Item(index);
        }

        public object ReadCellValue(int rowIndex, int colIndex)
        {
            Microsoft.Office.Interop.Excel.Range range = GetRange(rowIndex, colIndex); ;
            return range.Value2;
        }

        public object ReadCellValue(string cell)
        {
            Microsoft.Office.Interop.Excel.Range range = GetRange(cell, "");
            return range.Value2;
        }


        public object[,] ReadCellsValue(string startCell, string endCell)
        {
            Microsoft.Office.Interop.Excel.Range range = GetRange(startCell, endCell);
            return (object[,])range.Value2;
        }

        protected Microsoft.Office.Interop.Excel.Range GetRange(string startCell, string endCell)
        {
            object cell = startCell;
            object cell2 = endCell;
            if (startCell.Length == 0)
            {
                cell = Missing.Value;
            }
            if (endCell.Length == 0)
            {
                cell2 = Missing.Value;
            }
            Microsoft.Office.Interop.Excel.Range range = (Microsoft.Office.Interop.Excel.Range)ExcelSheet.get_Range(cell, cell2);
            return range;
        }

        protected Microsoft.Office.Interop.Excel.Range GetRange(int rowIndex, int colIndex)
        {
            if (rowIndex < 1)
            {
                rowIndex = 1;
            }
            if (colIndex < 1)
            {
                colIndex = 1;
            }
            Microsoft.Office.Interop.Excel.Range range = (Microsoft.Office.Interop.Excel.Range)ExcelSheet.Cells[rowIndex, colIndex];
            return range;
        }

        public int GetRowsCount()
        {
            return ExcelSheet.UsedRange.Rows.Count;
        }

        public int GetColumnsCount()
        {
            return ExcelSheet.UsedRange.Columns.Count;
        }

        public void QuitExcel()
        {
            ExcelBook.Saved = false;
            ExcelBook.Close(false, filePath, Missing.Value);
            ExcelApp.Quit();
            Clear(ExcelSheet);
            Clear(ExcelSheets);
            Clear(ExcelBook);
            Clear(ExcelApp);
        }

        private void Clear(object obj)
        {
            try
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(obj);
                System.GC.Collect();
            }
            catch
            {
                obj = null;
            }
            finally
            {
                obj = null;
            }
        }

    }
}
