/*************************************************************** 
*    Create Tag:Solomon20080917
*    Review Tag:
*    Description: Local config manipulate
*    Version: 1.0.0.0
***************************************************************/

using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.OleDb;
using System.Web;

namespace SFC_Tools.FileWR.ExcelFile
{
    public class ConfigInfo
    {
        OleDbDataAdapter adapter;
        OleDbCommand command;
        OleDbConnection connection;
        string errorMessage = "";

        #region Property
        public string ErrorMessage
        {
            get
            {
                return errorMessage;
            }
        }
        #endregion Property

        public ConfigInfo()
        {
            ConnetDataBase(AppDomain.CurrentDomain.BaseDirectory + @"\Data\Config.mdb");
        }

        public ConfigInfo(string dataFilePath)
        {
            ConnetDataBase(dataFilePath);
        }

        public void ConnetDataBase(string dataFilePath)
        {
            try
            {
                errorMessage = "";
                string connectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + dataFilePath ;
                adapter = new OleDbDataAdapter();
                command = new OleDbCommand();
                connection = new OleDbConnection(connectionString);
                command.Connection = connection;
            }
            catch (Exception exception)
            {
                errorMessage = exception.Message;
            }
        }

        public string ReadConfigInfo(string sectionName, string groupName)
        {
            try
            {
                errorMessage = "";
                string commandText = "SELECT DATA_VALUE FROM SYS_CONFIG_T WHERE SECTION_NAME ='" + sectionName + "' AND GROUP_NAME ='" + groupName + "'";
                DataSet dsData;
                ExecuteDataSet(commandText, out dsData);
                if (dsData != null)
                    if (dsData.Tables.Count > 0)
                        if (dsData.Tables[0].Rows.Count > 0)
                            return dsData.Tables[0].Rows[0][0].ToString().Trim();
                return "";
            }
            catch (Exception exception)
            {
                errorMessage = exception.Message;
                return "";
            }
        }

        /// <summary>
        /// Write config info for system
        /// </summary>
        /// <param name="sectionName"></param>
        /// <param name="groupName"></param>
        /// <param name="values"></param>
        /// <returns></returns>
        public int WriteConfigInfo(string sectionName, string groupName, object values)
        {

            return WriteConfigInfo(sectionName, groupName, values, "U");
        }

        /// <summary>
        /// Write config info for system/user
        /// </summary>
        /// <param name="sectionName"></param>
        /// <param name="groupName"></param>
        /// <param name="values"></param>
        /// <param name="flag"></param>
        /// <returns></returns>
        public int WriteConfigInfo(string sectionName, string groupName, object values, string flag)
        {

            RemoveConfigInfo(sectionName, groupName);
            string commandText = "INSERT INTO SYS_CONFIG_T VALUES('" + sectionName + "','" + groupName + "','" + values.ToString() + "','" + flag + "','SYSTEM',NOW())";
            return ExecuteNonQuery(commandText);
        }

        public int RemoveConfigInfo(string sectionName, string groupName)
        {
            string commandText = "DELETE FROM SYS_CONFIG_T WHERE SECTION_NAME ='" + sectionName + "' AND GROUP_NAME ='" + groupName + "'";
            return ExecuteNonQuery(commandText);
        }

        public string GetSqlStatement(string sqlID)
        {
            string factoryType = ReadConfigInfo("FactoryType", "FactoryType");
            factoryType = factoryType.Trim();
            return ReadConfigInfo(sqlID, factoryType);
        }

        /// <summary>
        /// Get the selected dataset
        /// </summary>
        /// <param name="cmdText"></param>
        /// <param name="dataSet"></param>
        /// <returns></returns>
        public bool ExecuteDataSet(string cmdText, out DataSet dataSet)
        {
            dataSet = null;
            try
            {
                errorMessage = "";
                command.CommandText = cmdText;
                dataSet = new DataSet();
                adapter.SelectCommand = command;
                adapter.Fill(dataSet);
                command.Connection.Close();
                return true;
            }
            catch (Exception exception)
            {
                errorMessage = exception.Message;
                return false;
            }
        }

        public int ExecuteNonQuery(string cmdText)
        {
            try
            {
                errorMessage = "";
                command.CommandText = cmdText;
                if (command.Connection.State == ConnectionState.Open)
                    command.Connection.Close();
                command.Connection.Open();
                int effectedRows = command.ExecuteNonQuery();
                command.Connection.Close();
                return effectedRows;
            }
            catch (Exception exception)
            {
                errorMessage = exception.Message;
                return -1;
            }
        }

        /// <summary>
        /// Insert the remoting server information
        /// </summary>
        /// <param name="name"></param>
        /// <param name="hostName"></param>
        /// <param name="hostPort"></param>
        /// <param name="defaultValue"></param>
        public int InsertServiceName(string serviceName, string serviceType, string hostName, int hostPort, string groupId, int sequenceNo)
        {
            if (DeleteServiceName(serviceName) == -1)
                return -1;
            string cmdText = "INSERT INTO SYS_SERVICE_T VALUES ('" + serviceName + "','" 
                                                                 + serviceType + "','" 
                                                                 + hostName + "',"
                                                                 + hostPort + ",'"
                                                                 + groupId + "'," 
                                                                 +sequenceNo + ",'SYSTEM',NOW())";
            return ExecuteNonQuery(cmdText);
        }

        //="INSERT INTO pcdata(medate) VALUES ('#"&DateStr&"#')"'
        /// <summary>
        /// Delete the remoting server information
        /// </summary>
        /// <param name="name"></param>
        public int DeleteServiceName(string serviceName)
        {
            string cmdText = "";
            if (serviceName == "%")
            {
                cmdText = "DELETE FROM SYS_SERVICE_T";
            }
            else
            {
                cmdText = "DELETE FROM SYS_SERVICE_T WHERE SERVICE_NAME ='" + serviceName + "'";
            }
            return ExecuteNonQuery(cmdText);
        }

        public bool ExistResource(string resourceName, string languageId)
        {
            string cmdText = "";
            DataSet dataSet = null;
            cmdText = "SELECT * FROM SYS_RESOURCE_T WHERE RESOURCE_NAME ='" + resourceName + "' AND LANGUAGE_ID='" + languageId + "'";
            bool flag = ExecuteDataSet(cmdText, out dataSet);
            if (!flag)
            {
                return false;
            }
            if (SFC_Tools.FileWR.ExcelFile.UtilityClass.ExistDataInDataSet(dataSet))
            {
                return true;
            }
            return false;
        }

        public int DeleteResource(string resourceName, string languageId)
        {
            string cmdText = "";
            cmdText = "DELETE FROM SYS_RESOURCE_T WHERE RESOURCE_NAME ='" + resourceName + "' AND LANGUAGE_ID='" + languageId+ "'";
            return ExecuteNonQuery(cmdText);
        }


        public int InsertResource(string resourceName, string languageId, string resourceValue, bool update)
        {
            if (ExistResource(resourceName, languageId))
            {
                if (!update)
                {
                    return -1;
                }
                if (DeleteResource(resourceName, languageId) == -1)
                {
                    return -1;
                }
            }
            string cmdText = "INSERT INTO SYS_RESOURCE_T VALUES ('" + resourceName + "','"
                                                                 + languageId + "','"
                                                                 + resourceValue + "','"
                                                                 + "STRING" + "',''"
                                                                 + ",'SYSTEM',NOW())";
            return ExecuteNonQuery(cmdText);
        }

        public string GetSytstemVersion()
        {
            try
            {
                errorMessage = "";
                string commandText = "SELECT VERSION FROM SYS_VERSION_T";
                DataSet dsData;
                ExecuteDataSet(commandText, out dsData);
                if (dsData != null)
                    if (dsData.Tables.Count > 0)
                        if (dsData.Tables[0].Rows.Count > 0)
                            return dsData.Tables[0].Rows[0][0].ToString().Trim();
                return "";
            }
            catch (Exception exception)
            {
                errorMessage = exception.Message;
                return "";
            }
        }
    }
}
