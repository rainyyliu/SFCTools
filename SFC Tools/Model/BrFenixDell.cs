using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SFC_Tools.Model
{
    class BrFenixDell
    {
        private int _QUANTITY;
        private string _STARTTIME;
        private string _ENDTIME;
        private string _GUID;
        private string _STATIONNAME;
        private string _EMPNO;
        private string _CATEGORY;
        private string _MFNAME;
        private string _MFSITE;
        private string _MFPN;
        private string _MFSN;
        private string _TESTGRADE;
        private string _TESTSTARTTIME;
        private string _TESTENDTIME;
        private string _TESTLINE;
        
        public BrFenixDell()
        {
        
        }

        public BrFenixDell(int qty, string starttime, string endtime, string guid, string station, string emp, string category, string mfname,
            string mfsite,string mfpn,string mfsn,string grade,string teststarttime,string testendtime,string testline
            )
        {
            this._QUANTITY = qty;
            this._STARTTIME = starttime;
            this._ENDTIME = endtime;
            this._GUID = guid;
            this._STATIONNAME = station;
            this._EMPNO = emp;
            this._CATEGORY=category;
            this._MFNAME = mfname;
            this._MFSITE = mfsite;
            this._MFPN = mfpn;
            this._MFSN = mfsn;
            this._TESTGRADE = grade;
            this._TESTSTARTTIME = teststarttime;
            this._TESTENDTIME = testendtime;
            this._TESTLINE = testline;
        }

        public int QUANTITY
        {
            set { this._QUANTITY = value; }
            get { return this._QUANTITY; }
        }

        public string STARTTIME
        {
            set { this._STARTTIME = value; }
            get { return this._STARTTIME; }
        }

        public string ENDTIME
        {
            set { this._ENDTIME = value; }
            get { return this._ENDTIME; }
        }

        public string GUID
        {
            set { this._GUID = value; }
            get { return this._GUID; }
        }

        public string STATIONNAME
        {
            set { this._STATIONNAME = value; }
            get { return this._STATIONNAME; }
        }

        public string EMP
        {
            set { this._EMPNO = value; }
            get { return this._EMPNO; }
        }

        public string CATEGORY
        {
            set { this._CATEGORY = value; }
            get { return this._CATEGORY; }
        }

        public string MFNAME
        {
            set { this._MFNAME = value; }
            get { return this._MFNAME; }
        }

        public string MFSITE
        {
            set { this._MFSITE = value; }
            get { return this._MFSITE; }
        }

        public string MFPN
        {
            set { this._MFPN = value; }
            get { return this._MFPN; }
        }

        public string MFSN
        {
            set { this._MFSN = value; }
            get { return this._MFSN; }
        }

        public string TESTGRADE
        {
            set { this._TESTGRADE = value; }
            get { return this._TESTGRADE; }
        }

        public string TESTSTARTTIME
        {
            set { this._TESTSTARTTIME = value; }
            get { return this._TESTSTARTTIME; }
        }

        public string TESTENDTIME
        {
            set { this._TESTENDTIME = value; }
            get { return this._TESTENDTIME; }
        }

        public string TESTLINE
        {
            set { this._TESTLINE = value; }
            get { return this._TESTLINE; }
        }
    }
}
