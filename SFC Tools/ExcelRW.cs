using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Office.Interop.Excel;
using System.Windows.Forms;

namespace SFC_Tools
{
    public class ExcelIO:IDisposable
    {
        private ExcelIO()
        {
            status=IsExistExcel()?0:-1;
        }
        public static ExcelIO GetInstance()
        {
            return new ExcelIO();
        }
        private static ExcelIO instance;
        private static readonly object synacRoot = new object();
        private string returnMessage;
        private Microsoft.Office.Interop.Excel.Application xlApp;
        private Microsoft.Office.Interop.Excel.Workbooks workbooks=null;
        private Microsoft.Office.Interop.Excel.Worksheet worksheet = null;
        private Microsoft.Office.Interop.Excel.Range range = null;
        private int status = -1;
        private bool bDisposed = false;

        public string ReturnMessage {
            get { return returnMessage; }
        }

        public int Status
        {
            get { return status; }
        }

        protected bool IsExistExcel()
        {
            try
            {
                xlApp = new Microsoft.Office.Interop.Excel.Application();
                if (xlApp == null)
                {
                    returnMessage = "Craete Excel Object Failed! There is no Excel found in this computer!";
                    return false;
                }
            }
            catch (Exception ex)
            {
                returnMessage = "Please Install Excel rightly!"+ex.Message.ToString();
                return false;
            }
            return true;
        }

        public static string SaveFileDialog()
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.DefaultExt = "xls";
            sfd.Filter = "Excel File 2003(*.xls)|*.xls|Excel File 2007(*.xlsx)|*.xlsx";
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                return sfd.FileName;
            }
            return string.Empty;
        }

        protected void setCesssBorderAround()
        {
            range.BorderAround(Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous,
                               Microsoft.Office.Interop.Excel.XlBorderWeight.xlThin,
                               Microsoft.Office.Interop.Excel.XlColorIndex.xlColorIndexAutomatic,
                               null);
            //range.Borders[Microsoft.Office.Interop.Excel.XlBordersIndex.xlInsideHorizontal];
        }

        public void Dispose()
        { 
            
        }
    }
    
    class ExcelRW
    {

    }
   

}
