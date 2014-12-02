using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Odbc;
using System.Data;

namespace SFC_Tools.Classes
{
    class MySqlDAL
    {
        string strConn = string.Empty;
        OdbcConnection odbcConn;
        public MySqlDAL(string sDbName)
        {
            //this.strConn = "Provider=MSDASQL.1;Password=" + sPassWord + ";Persist Security Info=True;User ID=" + sSchema + ";Data Source=" + sDbName + ";Initial Catalog=sfis1";
            this.strConn = "DSN="+sDbName+";SERVER=127.0.0.1;PORT=3306";
            try
            {
                odbcConn = new OdbcConnection(strConn);
                odbcConn.Open();
                odbcConn.Close();
            }
            catch (Exception ex)
            {
                throw new Exception("Connection MySql DataBase Failed！"+ex.Message.ToString());
            }

        }
        public void DisConn()
        {
            this.odbcConn.Close();
        }
        public int MyExecuteNonSql(string sSql)
        {
            try
            {
                // Create a new Odbc command
                OdbcCommand cmd = new OdbcCommand();
                //Prepare the command
                if (odbcConn.State != ConnectionState.Open)
                    odbcConn.Open();

                cmd.CommandText = sSql;
                cmd.Connection = odbcConn;
                cmd.CommandType = CommandType.Text;
                //Execute the command
                int val = cmd.ExecuteNonQuery();
                cmd.Parameters.Clear();
                return val;
            }
            catch (Exception ex)
            {
                return 0;
                throw new Exception(ex.Message);
            }
        }
    }
}
