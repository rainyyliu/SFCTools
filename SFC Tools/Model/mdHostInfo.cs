using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Net.Sockets;

namespace SFC_Tools.Model
{
    class mdHostInfo
    {
        public string HostID { get; set; }
        public string HostName { get; set; }
        public int HostStatus { get; set; }
        public string HostDesc { get; set; }
        public int TerminalProtocol { get; set; }
    }

    public class TDCTINFO
    {
    /*
     struct TDCTINFO
	{
		CString OnLineStatus;
	    CString LastW;
		int inLastW;
		int inWTIndx;
		int last;
		int rule;
		CString CurResult;
		CString CurWT;
		int inCurWT;
		int CurinWTIndx;
		int Curlast;
		int Currule;
		int inBuzz;
		BOOL blUndo;
		BOOL CheckFlag;
	    CString ReadBuffer;
		CString SendBuffer;
	};
     */
        public string OnLineStatus { set; get; }
        public string LastW { set; get; }
        public int inLastW { set; get; }
        public int inWTIndx { set; get; }
        public int last { set; get; }
        public int rule { set; get; }
        public string CurResult { set; get; }
        public string CurWT { set; get; }
        public int inCurWT { set; get; }
        public int CurinWTIndx { set; get; }
        public int Curlast { set; get; }
        public int Currule { set; get; }
        public int inBuzz { set; get; }
        public bool blUndo { set; get; }
        public bool CheckFlag { set; get; }
        public string ReadBuffer { set; get; }
        public string SendBuffer { set; get; }
    }
    public class TDCTCONFIG
    {
        /*
         CString HostID;
	    int inHostID;
	    CString NodeID;
	    int inNodeID;
         * 
	    CString PortID;
	    int inPortID;
         * 
	    CString StationNumber;
	    int inStationNumber;
	    CString InType;
	    int inInType;
	    CString OutType;
	    int inOutType;
	    CString stIP;
	    CString Port;
	    SOCKET Socket;
         */
        public string  HostID  {set;get;}
        public int  inHostID  {set;get;}
        public string  NodeID  {set;get;}
        public int  inNodeID  {set;get;}
        public string  PortID  {set;get;}
        public int  inPortID  {set;get;}
        public string  StationNumber  {set;get;}
        public int  inStationNumber  {set;get;}
        public string  InType  {set;get;}
        public int  inInType  {set;get;}
        public string  OutType  {set;get;}
        public int  inOutType  {set;get;}
        public string  stIP  {set;get;}
        public string  Port  {set;get;}
        public Socket SOCKET  {set;get;}
    }
    public class TSTATIONINFO
    {
        public string HostID { set; get; }
        public int inHostID { set; get; }
        public string StationIdx { set; get; }
        public int inStationIdx { set; get; }
        public string StationNumber { set; get; }
        public int inStationNumber { set; get; }

        public string StationName { set; get; }
        public string LineName { set; get; }
        public string GroupName { set; get; }
        public string SectionName { set; get; }
        public string TaskCode { set; get; }

        public int inTaskCode { set; get; }
        public string CycleTime { set; get; }
        public int inCycleTime { set; get; }
        public string StationType { set; get; }
        public int inStationType { set; get; }
        public string StationTypeName { set; get; }
        public string ReMsg { set; get; }
        public string SeMsg { set; get; }

        //public string StationType { set; get; }
        public TDCTCONFIG strDctConfigInfo { set; get; }
        public TDCTINFO strDctInfo { set; get; }
        public DctMap DctMapData { set; get; }
    }
    public class TSECTIONINFO
    {
        public int SecIndx { get; set; }
        public int GroupNumber { get; set; }
        public string LineName { get; set; }
        public string SectionName { get; set; }
    }

    public class IcmpPacket
    {
        public Byte Type;    // type of message  
        public Byte SubCode;    // type of sub code  
        public UInt16 CheckSum;   // ones complement checksum of struct  
        public UInt16 Identifier;      // identifier  
        public UInt16 SequenceNumber;     // sequence number  
        public Byte[] Data;
    } // class IcmpPacket  

    public class DctMap
    {
        public int DCTID { set; get; }
        public int inRow { set; get; }
        public int inCol { set; get; }
        public string DctInfo { set; get; }
    }
}


/*
 * 
    struct DctMap
	{
		int DCTID;
		int inRow;
		int inCol;
		CString DctInfo;
	};
 * 
struct TSECTIONINFO
{
	int SecIndx;
	int GroupNumber;
	CString LineName;
	CString SectionName;
};

 * * 
 
struct TSTATIONINFO
{
	CString HostID;
	int inHostID;
 * 
	CString StationIdx;
	int inStationIdx;
 * 
	CString StationNumber;
	int inStationNumber;
 * 
	CString StationName;
	CString LineName;
	CString GroupName;
	CString SectionName;
	CString TaskCode;
 * 
	int inTaskCode;
	CString CycleTime;
	int inCycleTime;
	CString StationType;
	int inStationType;
	CString StationTypeName;
 * 
	CString ReMsg;
	CString SeMsg;
 * 
	struct TMAINSTATIONTYPEINFO strMainStnInfo;
	struct TDCTCONFIG strDctConfigInfo;
	struct TDCTINFO strDctInfo;
	struct DctMap DctMapData;
	struct TSTATIONTMPBUFFER strTmpBuf[256];
};
                                      
struct TDCTCONFIG
{
	CString HostID;
	int inHostID;
	CString NodeID;
	int inNodeID;
	CString PortID;
	int inPortID;
	CString StationNumber;
	int inStationNumber;
	CString InType;
	int inInType;
	CString OutType;
	int inOutType;
	CString stIP;
	CString Port;
	SOCKET Socket;
};
                                       
struct TDCTINFO
{
	CString OnLineStatus;
	CString LastW;
	int inLastW;
	int inWTIndx;
	int last;
	int rule;
	CString CurResult;
	CString CurWT;
	int inCurWT;
	int CurinWTIndx;
	int Curlast;
	int Currule;
	int inBuzz;
	BOOL blUndo;
	BOOL CheckFlag;
	CString ReadBuffer;
	CString SendBuffer;
};
*/