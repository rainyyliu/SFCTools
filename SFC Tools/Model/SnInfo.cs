using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SFC_Tools.Model
{
    class SnInfo
    {
        private string strStartSN;
        private string strEndSn;

        public SnInfo(string StartSn, string EndSn)
        {
            this.strStartSN = StartSn;
            this.strEndSn = EndSn;
        }

        public string START_SN
        {
            get { return this.strStartSN; }
        }
        public string END_SN
        {
            get { return this.strEndSn; }
        }
       
    }
}
