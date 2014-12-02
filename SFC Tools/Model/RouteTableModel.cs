using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SFC_Tools.Model
{
    class RouteTableModel
    {
        public int RouteCode { set; get; }
        public string GroupName { set; get; }
        public string GroupNext { set; get; }
        public int StateFlag { set; get; }
        public int StepSeqNo { set; get; }
        public string RouteDesc { set; get; }
    }
}
