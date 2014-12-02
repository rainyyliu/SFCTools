using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SFC_Tools.Model
{
    class TestModel
    {
        private string strSN;
        private string strMachineCode;
        private string strDate;
        private string strHHPN;
        private string strDateCode;
        private string strLote;
        private string strVendor;
        private string strLOCATION;
        private string strkp;
      
       public TestModel(string strSn, string strMc, string strDt, string strHHPN, string strDC, string strLt, string strVen, string strLoc, string strKp)
        {
            this.strSN = strSn;
            this.strMachineCode = strMc;
            this.strDate = strDt;
            this.strHHPN = strHHPN;
            this.strDateCode = strDC;
            this.strLote = strLt;
            this.strVendor = strVen;
            this.strLOCATION = strLoc;
            this.strkp = strKp;
        }

        public string SN
        {
            get { return this.strSN; }
        }
        public string MC
        {
            get { return this.strMachineCode; }
        }
        public string DT
        {
            get { return this.strDate; }
        }
        public string HP
        {
            get { return this.strHHPN; }
        }
        public string DC
        {
            get { return this.strDateCode; }
        }
        public string LT
        {
            get { return this.strLote; }
        }
        public string VD
        {
            get { return this.strVendor; }
        }
        public string LA
        {
            get { return this.strLOCATION; }
        }
        public string KP
        {
            get { return this.strkp; }
        }
    }
}
