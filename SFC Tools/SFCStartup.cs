using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;

namespace SFC_Tools
{
    class SFCStartup
    {
        public static DBAccess dba;
        public SFCStartup()
        {
            try
            {
                string strConn = this.GetDbConn();
                dba = new DBAccess(strConn);
            }
            catch (Exception ex)
            {
                //throw new Exception("Config File is Missing!"+ex.Message.ToString());
            }
        }
        private string GetDbConn()
        {
            string strConn;
            AppSettingsReader asr=new AppSettingsReader();
            strConn = asr.GetValue("DBconn", typeof(string)).ToString();
            return strConn;
        }

    }
}
