/*************************************************************** 
*    Create Tag:Solomon20090410
*    Review Tag:
*    Description: Write excel file
*    Version: 1.0.0.0
***************************************************************/

using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;
using System.Reflection;

namespace SFC_Tools.FileWR.ExcelFile
{
    public class ExcelFileWrite : ExcelFileRead
    {
        public ExcelFileWrite()
        {

        }

        protected override void SetReadFlag()
        {
            isRead = false;
        }

        /// <summary>
        /// index:From 1 to 256
        /// </summary>
        /// <param name="index"></param>
        public void AddWorkSheet(int index)
        {
            if (index == 0)
            {
                index = 1;
            }
            if (ExcelBook == null)
            {
                ExcelBook = ExcelApp.Workbooks.Add(Microsoft.Office.Interop.Excel.XlWBATemplate.xlWBATWorksheet);
            }
            if (ExcelSheets == null)
            {
                ExcelSheets = ExcelBook.Worksheets;
            }
            if (index > ExcelSheets.Count)
            {
                ExcelBook.Worksheets.Add(Missing.Value, Missing.Value, Missing.Value, Microsoft.Office.Interop.Excel.XlSheetType.xlWorksheet);
                index = ExcelSheets.Count;
            }
            SetWorkSheet(index);
        }

        public void WriteCellValue(object value, int rowIndex, int colIndex)
        {
            Microsoft.Office.Interop.Excel.Range range = GetRange(rowIndex, colIndex);
            if (range == null)
            {
                return;
            }
            range.NumberFormat = "@";  
            range.NumberFormatLocal = "@";
            range.Value2 = value;
        }

        public void WriteCellValue(object value, string cell)
        {
            Microsoft.Office.Interop.Excel.Range range = GetRange(cell, "");
            if (range == null)
            {
                return;
            }
            range.NumberFormat = "@";
            range.NumberFormatLocal = "@";
            range.Value2 = value;
        }

        public void WriteCellPicture(int rowIndex, int colIndex, string picturePath)
        {
            Microsoft.Office.Interop.Excel.Range range = GetRange(rowIndex, colIndex);
            if (range == null)
            {
                return;
            }
            range.Select();
            Microsoft.Office.Interop.Excel.Pictures pics = (Microsoft.Office.Interop.Excel.Pictures)ExcelSheet.Pictures(Missing.Value);
            pics.Insert(picturePath, Missing.Value);
        }

        public void WriteCellPicture(string cell, string picturePath)
        {
            Microsoft.Office.Interop.Excel.Range range = GetRange(cell, "");
            if (range == null)
            {
                return;
            }
            range.Select();
            Microsoft.Office.Interop.Excel.Pictures pics = (Microsoft.Office.Interop.Excel.Pictures)ExcelSheet.Pictures(Missing.Value);
            pics.Insert(picturePath, Missing.Value);
        }

        public void WriteCellPicture(int rowIndex, int colIndex, string picturePath, float pictureWidth, float pictureHeight)
        {
            Microsoft.Office.Interop.Excel.Range range = GetRange(rowIndex, colIndex);
            if (range == null)
            {
                return;
            }
            range.Select();
            float rangeLeft = 0;
            float rangeTop = 0;
            rangeLeft = Convert.ToSingle(range.Left);
            rangeTop = Convert.ToSingle(range.Top);
            ExcelSheet.Shapes.AddPicture(picturePath, Microsoft.Office.Core.MsoTriState.msoFalse, Microsoft.Office.Core.MsoTriState.msoTrue, rangeLeft, rangeTop, pictureWidth, pictureHeight);
        }

        public void WriteCellPicture(string cell, string picturePath, float pictureWidth, float pictureHeight)
        {
            Microsoft.Office.Interop.Excel.Range range = GetRange(cell, "");
            if (range == null)
            {
                return;
            }
            range.Select();
            float rangeLeft = 0;
            float rangeTop = 0;
            rangeLeft = Convert.ToSingle(range.Left);
            rangeTop = Convert.ToSingle(range.Top);
            ExcelSheet.Shapes.AddPicture(picturePath, Microsoft.Office.Core.MsoTriState.msoFalse, Microsoft.Office.Core.MsoTriState.msoTrue, rangeLeft, rangeTop, pictureWidth, pictureHeight);
        }

        public void WriteCellsValue(object[,] values, string startCell, string endCell)
        {
            Microsoft.Office.Interop.Excel.Range range = GetRange(startCell, endCell);
            if (range == null)
            {
                return;
            }
            range.Value2 = values;
        }

        public void MergeCells(string startCell, string endCell)
        {
            Microsoft.Office.Interop.Excel.Range range = ExcelSheet.get_Range(startCell, endCell);
            if (range != null)
            {
                range.Merge(null);
            }
        }

        public void SetCellsWidth(string startCell, string endCell, int width)
        {
            Microsoft.Office.Interop.Excel.Range range = GetRange(startCell, endCell);
            if (range == null)
            {
                return;
            }
            range.ColumnWidth = width;
        }

        public void SetCellsAutoFit(string startCell, string endCell)
        {
            Microsoft.Office.Interop.Excel.Range range = GetRange(startCell, endCell);
            if (range == null)
            {
                return;
            }
            range.AutoFit();
        }

        public void SetCellsFont(string startCell, string endCell, Color color, Font font)
        {
            //font =new Font(
            Microsoft.Office.Interop.Excel.Range range = GetRange(startCell, endCell);
            if (range == null)
            {
                return;
            }
            range.Font.Color = color;
            range.Font.Size = font.Size;
            range.Font.Bold = font.Bold;
            range.Font.Name = font.Name;
            range.Font.Italic = font.Italic;
            range.Font.Underline = font.Underline;
        }

        public void SetCellsAlignment(string sCellNumber, int horizontal/*2,3,4*/, int vertical/*1,2,3*/)
        {
            Microsoft.Office.Interop.Excel.Range range = ExcelSheet.get_Range(sCellNumber, Missing.Value);
            if (range == null)
            {
                return;
            }
            range.HorizontalAlignment = horizontal;//2,3,4
            range.VerticalAlignment = vertical;//1,2,3
        }

        public void SetChart(string startCell, string endCell, int chartType, string title, string categoryTitle, string valueTile, double left, double top, double width, double heigh)
        {
            Microsoft.Office.Interop.Excel.ChartObjects chartobjects = (Microsoft.Office.Interop.Excel.ChartObjects)ExcelSheet.ChartObjects(Missing.Value);
            Microsoft.Office.Interop.Excel.ChartObject chartobject = (Microsoft.Office.Interop.Excel.ChartObject)chartobjects.Add(left /*Left*/, top /*Top*/, width /*Width*/, heigh /*Height*/);
            Microsoft.Office.Interop.Excel._Chart chart = (Microsoft.Office.Interop.Excel._Chart)chartobject.Chart;
            Microsoft.Office.Interop.Excel.Range range = GetRange(startCell, endCell);
            if (range == null)
            {
                return;
            }
            Object[] args7 = new Object[11];
            args7[0] = range; // Source
            args7[1] = (Microsoft.Office.Interop.Excel.XlChartType)chartType;//xl3DColumn; // Gallery
            args7[2] = Missing.Value; // Format
            args7[3] = Microsoft.Office.Interop.Excel.XlRowCol.xlRows; // PlotBy
            args7[4] = 0; // CategoryLabels
            args7[5] = 0; // SeriesLabels
            args7[6] = true; // HasLegend
            args7[7] = title; // Title
            args7[8] = categoryTitle; // CategoryTitle
            args7[9] = valueTile; // ValueTitle
            args7[10] = Missing.Value; // ExtraTitle
            chart.GetType().InvokeMember("ChartWizard", BindingFlags.InvokeMethod, null, chart, args7);
        }

        public void SetPrinterName(string printerName)
        {
            ExcelApp.ActivePrinter = printerName;
        }

        public void PrintOut(int copies)
        {
            copies = copies > 0 ? copies : 1;
            ExcelSheet.PrintOut(Type.Missing, Type.Missing, copies, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
        }

        public void Preview()
        {
            ExcelApp.Visible = true;
            ExcelApp.UserControl = true;
            ExcelSheet.PrintPreview(Missing.Value);
        }

        public void SaveAs(string filePath)
        {
            if (filePath == "")
            {
                filePath = this.filePath;
            }

            if (filePath == "")
            {
                SaveFileDialog SaveFileDialog = new SaveFileDialog();
                SaveFileDialog.Filter = "*.xls|*.xls";
                if (SaveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    filePath = SaveFileDialog.FileName;
                }
                else
                {
                    return;
                }
            }
            ExcelBook.SaveAs(filePath, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlShared, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value);
        }

        public void ExportDataSet(DataSet dataSet, string filePath)
        {
            if (!SFC_Tools.FileWR.ExcelFile.UtilityClass.ExistDataInDataSet(dataSet))
            {
                return;
            }
            int iTableIndex = 0;
            int columnIndex = 0;
            int rowIndex = 0;
            string data = "";
            OpenExcel(filePath);
            for (iTableIndex = 0; iTableIndex < dataSet.Tables.Count; iTableIndex++)
            {
                AddWorkSheet(iTableIndex + 1);
                for (columnIndex = 0; columnIndex < dataSet.Tables[iTableIndex].Columns.Count; columnIndex++)
                {
                    WriteCellValue(dataSet.Tables[iTableIndex].Columns[columnIndex].ColumnName, 1, columnIndex + 1);
                }
                for (rowIndex = 0; rowIndex < dataSet.Tables[iTableIndex].Rows.Count; rowIndex++)
                {
                    for (columnIndex = 0; columnIndex < dataSet.Tables[iTableIndex].Columns.Count; columnIndex++)
                    {
                        data = dataSet.Tables[iTableIndex].Rows[rowIndex][columnIndex].ToString();
                        WriteCellValue(data, rowIndex+2, columnIndex+1);
                    }
                }
            }
            SaveAs(filePath);
            QuitExcel();
        }

        public void ExportDataSet(DataSet dataSet)
        {
            ExportDataSet(dataSet, "");
        }
    }
}
