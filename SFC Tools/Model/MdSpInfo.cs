using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SFC_Tools.Model
{
    class MdSpInfo
    {
        private string strOwner;
        private string strSpName;
        public MdSpInfo(string sOwner,string sSpName)
        {
            this.strOwner = sOwner;
            this.strSpName = sSpName;
        }

        public string SP_OWNER
        {
            get { return this.strOwner; }
            set { this.strOwner = value; }
        }
        public string SP_NAME
        {
            get { return this.strSpName; }
            set { this.strSpName = value; }
        }
    }
}
