using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.OleDb;
using System.Data;

namespace SFC_Tools.Classes
{
    class MyTransTablesFormOraToMySql
    {
        private OleDbConnection oleConn;
        private string m_Schema=string.Empty;
        private string strConn;
        public MyTransTablesFormOraToMySql(string sDbName,string sSchema,string sPassWord)
        {
            this.strConn = "Provider=OraOLEDB.Oracle.1;Data Source=" + sDbName + ";User ID=" + sSchema + ";Password="+sPassWord;
            try
            {
                oleConn = new OleDbConnection(strConn);
                oleConn.Open();
                oleConn.Close();
                m_Schema = sSchema;
            }
            catch (Exception ex)
            {
                throw new Exception("Connection Data Base Failed！"+ex.Message.ToString());
            }
        }
        ~MyTransTablesFormOraToMySql()
        {
            this.oleConn.Close();
        }

        public DataSet GetTablesFromOra()
        {
            DataSet dsTables = new DataSet();
            string strSql = "SELECT OWNER||'.'||TABLE_NAME AS TABLE_NAME FROM ALL_TABLES WHERE OWNER='" + m_Schema + "'";
            using (OleDbConnection connection = new OleDbConnection(strConn))
            {
                using (OleDbDataAdapter da = new OleDbDataAdapter(strSql, connection))
                {
                    try
                    {
                        da.Fill(dsTables, "dsTables");
                    }
                    catch (System.Data.OleDb.OleDbException ex)
                    {
                        return dsTables;
                        throw new Exception(ex.Message);
                    }
                    finally
                    {
                        if (oleConn.State != ConnectionState.Closed)
                        {
                            oleConn.Close();
                        }
                    }
                }
                return dsTables;
            }  
        }

        public DataSet GetTableScript(string sTable)
        {
            DataSet dsTable = new DataSet();
            string strSql = "SELECT COLUMN_NAME,DATA_TYPE,DATA_LENGTH,NULLABLE FROM  ALL_TAB_COLUMNS WHERE OWNER||'.'||TABLE_NAME='" + sTable + "'";
            using (OleDbConnection connection = new OleDbConnection(strConn))
            {
                using (OleDbDataAdapter da = new OleDbDataAdapter(strSql, connection))
                {
                    try
                    {
                        da.Fill(dsTable, "dsTable");
                    }
                    catch (System.Data.OleDb.OleDbException ex)
                    {
                        return dsTable;
                        throw new Exception(ex.Message);
                    }
                    finally
                    {
                        if (oleConn.State != ConnectionState.Closed)
                        {
                            oleConn.Close();
                        }
                    }
                }
                return dsTable;
            }  
        }
        
    }
}
