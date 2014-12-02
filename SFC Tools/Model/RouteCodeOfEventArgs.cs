using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SFC_Tools.Model
{
    public class RouteCodeOfEventArgs:EventArgs
    {
        public int iRouteCode;
        public  RouteCodeOfEventArgs(int iNumber)
        {
            iRouteCode = iNumber;
        }
    }
}
