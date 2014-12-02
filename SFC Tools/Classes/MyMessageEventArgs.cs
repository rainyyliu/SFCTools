using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace SFC_Tools.Classes
{
    public class MyMessageEventArgs:EventArgs
    {
        public string MyMessage;
        public ArrayList arrStationInfo;
        public MDScanMessage ScanMsg;
        public StationStatus stationStatus;
        public MyMessageEventArgs(string message)
        {
            MyMessage = message;
        }

        public MyMessageEventArgs(ArrayList arr)
        {
            arrStationInfo = arr;
        }

        public MyMessageEventArgs(MDScanMessage msg)
        {
            ScanMsg = msg;
        }

        public MyMessageEventArgs(StationStatus ss)
        {
            stationStatus = ss;
        }
    }
    public class MDScanMessage
    {
        public bool bSendMsg { set; get; }
        public string ScanMessage { set; get; }
    }

    public class StationStatus
    {
        public int ID { set; get; }
        public bool Status { set; get; }
    }
}
