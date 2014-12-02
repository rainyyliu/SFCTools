using System;
using System.Data;
using System.Data.OleDb;
using System.Collections;
using System.Collections.Generic;
using SFC_Tools.Model;
using System.Data.OracleClient;
using System.Linq;
using System.Linq.Expressions;

namespace SFC_Tools
{
    class DBAccess
    {
        private OleDbConnection oleConn;
        
        private bool bIsCloseForm=true;
        private string strConn;
        public DBAccess(string strConn)
        {
            this.strConn=strConn;
            try
            {
                oleConn = new OleDbConnection(strConn);
                oleConn.Open();
                oleConn.Close();
            }
            catch (Exception ex)
            {
                throw new Exception("Connection Data Base Failed！"+ex.Message.ToString());
            }
        }
        //set from close state
        public void SetFormState(bool bState)
        {
            this.bIsCloseForm = bState;
        }
        //get form close state
        public bool GetFormState()
        {
            return this.bIsCloseForm;
        }
        public  DataSet Query1(/*string connectionString,*/ string SQLString, params OleDbParameter[] cmdParms)
        {
            using (OleDbConnection connection = new OleDbConnection(strConn))
            {
                // OleDbCommand cmd = new OleDbCommand();
                // PrepareCommand(cmd, connection, null, CommandType.Text, SQLString, cmdParms);
                using (OleDbDataAdapter da = new OleDbDataAdapter(SQLString, connection))
                {
                    DataSet ds = new DataSet();
                    try
                    {
                        if (cmdParms != null)
                        {
                            foreach (OleDbParameter parm in cmdParms)
                                da.SelectCommand.Parameters.Add(parm);
                        }

                        da.Fill(ds, "ds");
                        // cmd.Parameters.Clear();

                    }
                    catch (System.Data.OleDb.OleDbException ex)
                    {
                        throw new Exception(ex.Message);
                    }
                    finally
                    {
                        if (oleConn.State != ConnectionState.Closed)
                        {
                            oleConn.Close();
                        }
                    }
                    return ds;
                }
            }

        }

        public string TestOracleParam()
        {
            string strSql;
            strSql = " SELECT * FROM SFISM4.R_WIP_TRACKING_T WHERE ERROR_FLAG=:AA AND STATION_SEQ=:BB AND SECTION_NAME=:CC ";
            oleConn.Close();
            oleConn.Open();
            //OleDbCommand odc = new OleDbCommand(strSql, oleConn);
            OleDbParameter[] parm ={
              new OleDbParameter(":AA",OleDbType.VarNumeric,100),
              new OleDbParameter(":BB",OleDbType.VarNumeric,100),
              new OleDbParameter(":CC",OleDbType.VarChar,100) 
            };
            parm[0].Value = 0;
            parm[1].Value = 0;
            parm[2].Value = "0";
            DataSet ds = Query1(strSql,parm);

           /* OleDbDataAdapter oda = new OleDbDataAdapter(strSql,oleConn);
            if (parm != null)
            {
                foreach (OleDbParameter parmeter in parm)
                {
                    oda.SelectCommand.Parameters.Add(parmeter);
                }
            }*/
            //oda.SelectCommand.Parameters.Add(":AA", OleDbType.VarNumeric, 80).Value = 0;
            //oda.SelectCommand.Parameters.Add(":BB", OleDbType.VarNumeric, 80).Value = 0;
            //oda.SelectCommand.Parameters.Add(":CC", OleDbType.VarChar, 80).Value = "0";
            /*DataSet ds = new DataSet();
            oda.Fill(ds);
            */
            //OleDbDataReader odr = odc.ExecuteReader();
            string xx = string.Empty; ;
            //while (odr.Read())
            //{
            //    xx= odr[0].ToString();
            //}
            xx = ds.Tables[0].Rows[0][0].ToString();
            return xx ;
        }
        //check data exist
        public bool bCheckExists(string strBarCode, string strMac)
        {
            bool isSuccess=false;
            try
            {
                string strSql;
                strSql = "SELECT COUNT(*) AS CNT FROM COMPAQ.COMPAQ_R_MACADDR_T WHERE FLASH_FLAG='MAC' AND BARCODE='" + strBarCode + "' AND MAC_ADDR='" + strMac + "'";
                oleConn.Close();
                oleConn.Open();
                OleDbCommand odc = new OleDbCommand(strSql,oleConn);
                OleDbDataReader odr = odc.ExecuteReader();
                while (odr.Read())
                {
                    string strRev = odr["CNT"].ToString();
                    if (int.Parse(strRev) > 0)
                    {
                        isSuccess = true;
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                isSuccess = false;
            }
            finally
            {
                oleConn.Close();
            }
            return isSuccess;
        }
        //insert data into compaq
        public bool bInsertCompaq(string strBarCode, string strMac)
        {
            bool isSuccess=false;
            try
            {
                string strSql;
                string strTime = strGetTime();
                string strDate = strGetDate();
                strSql = "INSERT INTO COMPAQ.COMPAQ_R_MACADDR_T(BAR_CODE,MAC_ADDR,TEST_TIME,TEST_DATE,FLASH_FLAG) ";
                strSql = strSql + " VALUES('" + strBarCode + "','" + strMac + "','" + strTime + "','" + strDate + "','MAC')";
                OleDbCommand odc=new OleDbCommand(strSql,oleConn);
                oleConn.Close();
                oleConn.Open();
                odc.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                isSuccess = false;
            }
            return isSuccess;
            
        }
        public bool bCommit()
        {
            bool isSuccess = false;
            try
            {
                string strSql;
                strSql = "COMMIT";
                OleDbCommand odc = new OleDbCommand(strSql, oleConn);
                oleConn.Close();
                oleConn.Open();
                odc.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                isSuccess = false;
            }
            return isSuccess;

        }
        public bool bRollBack()
        {
            bool isSuccess = false;
            try
            {
                string strSql;
                strSql = "ROLLBACK";
                OleDbCommand odc = new OleDbCommand(strSql, oleConn);
                oleConn.Close();
                oleConn.Open();
                odc.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                isSuccess = false;
            }
            return isSuccess;

        }
        //Get Test Time
        private string strGetTime()
        {
            string strRev="N/A";
            try
            {
                string strSql;
                strSql = "SELECT TO_CHAR(SYSDATE,'hh24mmss')AS GETTIME FROM DUAL";
                OleDbCommand odc = new OleDbCommand(strSql, oleConn);
                oleConn.Close();
                oleConn.Open();
                OleDbDataReader odr = odc.ExecuteReader();
                while (odr.Read())
                {
                    strRev = odr["GETTIME"].ToString();
                }

            }
            catch (Exception ex)
            {
                strRev = "N/A";
            }
            return strRev;
        }
        //Get Test Date
        private string strGetDate()
        {
            string strRev = "N/A";
            try
            {
                string strSql;
                strSql = "SELECT TO_CHAR(SYSDATE,'yyyy/mm/dd')AS GETDATE FROM DUAL";
                OleDbCommand odc = new OleDbCommand(strSql, oleConn);
                oleConn.Close();
                oleConn.Open();
                OleDbDataReader odr = odc.ExecuteReader();
                while (odr.Read())
                {
                    strRev = odr["GETDATE"].ToString();
                }

            }
            catch (Exception ex)
            {
                strRev = "N/A";
            }
            return strRev;
        }
        //Get IniData
        public DataSet GetInitData(string strMo,string strTrail)
        {
            DataSet dtData=new DataSet();
            try
            {
                string strSql;
                strSql = "SELECT SERIAL_NUMBER FROM SFISM4.R_WIP_TRACKING_T WHERE MO_NUMBER = '" + strMo + "' " + strTrail;
                oleConn.Close();
                oleConn.Open();
                OleDbDataAdapter oda = new OleDbDataAdapter(strSql, oleConn);
                oda.Fill(dtData, "WIP_TRACKING_T");

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message.ToString());
            }
            finally
            {
                oleConn.Close();
            }
            return dtData;
        }
        //Get Linked MAC
        public string GetLinkMac(string strSn)
        {
            string strRev = "N/A";
            try
            {
                string strSql;
                strSql = "SELECT MAC_ADDR FROM  COMPAQ.COMPAQ_R_MACADDR_T WHERE BAR_CODE='" + strSn + "' AND FLASH_FLAG='MAC'";
                OleDbCommand odc = new OleDbCommand(strSql, oleConn);
                oleConn.Close();
                oleConn.Open();
                OleDbDataReader odr = odc.ExecuteReader();
                while (odr.Read())
                {
                    strRev = odr["MAC_ADDR"].ToString();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Get Linked MAC Failed!" + ex.Message.ToString());
            }
            finally
            {
                oleConn.Close();
            }
            return strRev;
        }
        //Get server Date time
        public string GetServerDate(string strFormate)
        {
            string strRev = "N/A";
            try
            {
                string strSql;
                strSql = "SELECT TO_CHAR(SYSDATE,'" + strFormate + "') AS DT FROM DUAL";
                //strSql = "SELECT TO_CHAR(SYSDATE,'yyyy-mm-dd') AS DT FROM DUAL";
                OleDbCommand odc = new OleDbCommand(strSql, oleConn);
                oleConn.Close();
                oleConn.Open();
                OleDbDataReader odr = odc.ExecuteReader();
                while (odr.Read())
                {
                    strRev = odr["DT"].ToString();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Get Date Failed!" + ex.Message.ToString());
            }
            finally
            {
                oleConn.Close();
            }
            return strRev;
        }
        //Get User Name
        public string GetUserName(string strEmp)
        {
            string strRev = "N/A";
            try
            {
                string strSql;
                strSql = "SELECT EMP_NAME FROM SFIS1.C_EMP_DESC_T WHERE EMP_NO='" + strEmp + "'";
                OleDbCommand odc = new OleDbCommand(strSql, oleConn);
                oleConn.Close();
                oleConn.Open();
                OleDbDataReader odr = odc.ExecuteReader();
                while (odr.Read())
                {
                    strRev = odr["EMP_NAME"].ToString();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Get User Name Failed!" + ex.Message.ToString());
            }
            finally
            {
                oleConn.Close();
            }
            return strRev;
        }
        //get machine code
        public string GetMachinGroup(string strMachineCode)
        {
            string strRev = "N/A";
            try
            {
                string strSql;
                strSql = "SELECT GROUP_NAME FROM SFIS1.C_ICT_STATION_T WHERE STATION_CODE='" + strMachineCode + "'";
                OleDbCommand odc = new OleDbCommand(strSql, oleConn);
                oleConn.Close();
                oleConn.Open();
                OleDbDataReader odr = odc.ExecuteReader();
                while (odr.Read())
                {
                    strRev = odr["GROUP_NAME"].ToString();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Get Group NameFailed!" + ex.Message.ToString());
            }
            finally
            {
                oleConn.Close();
            }
            return strRev;
        }
        //check line name
        public string GetLineName(string strLineName,string strGroup)
        {
            string strRev = "N/A";
            string strSqlTail;
            if (strGroup != "N/A")
            {
                strSqlTail = " AND GROUP_NAME='" + strGroup + "'";
            }
            else
            {
                strSqlTail = "";
            }
            try
            {
                string strSql;
                strSql = "SELECT GROUP_NAME FROM SFIS1.C_ICT_STATION_T WHERE LINE_NAME='" + strLineName + "' " + strSqlTail;
                OleDbCommand odc = new OleDbCommand(strSql, oleConn);
                oleConn.Close();
                oleConn.Open();
                OleDbDataReader odr = odc.ExecuteReader();
                while (odr.Read())
                {
                    strRev = odr["GROUP_NAME"].ToString();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Get Group Name Failed!" + ex.Message.ToString());
            }
            finally
            {
                oleConn.Close();
            }
            return strRev;
        }
        //insert test log
        public void InsertTestLog(string strBar,string strRes,string strTime)
        {
            try
            {
                string strSql;
                oleConn.Close();
                oleConn.Open();
                strSql = "INSERT INTO SFISM4.R_ICT_TEST_LOG_T(BAR_CODE,RESULT,OPERATE_TIME) VALUES('" + strBar + "','" + strRes + "',TO_DATE('" + strTime + "','yyyy/mm/dd hh24:mi:ss'))";
                OleDbCommand odc = new OleDbCommand(strSql, oleConn);
                odc.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message.ToString());
            }
            finally
            {
                oleConn.Close();
            }
        }
        public void RevertData(string strMo)
        {
            try
            {
                string strSql;
                strSql = "UPDATE SFISM4.R_WIP_TRACKING_T SET GROUP_NAME='P_MAC',SECTION_NAME='0',STATION_NAME='0',ERROR_FLAG='0' WHERE MO_NUMBER='" + strMo + "'";
                oleConn.Close();
                oleConn.Open();
                OleDbCommand odc = new OleDbCommand(strSql, oleConn);
                int iRes = odc.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message.ToString());
            }
            finally
            {
                oleConn.Close();
            }
        }
        public DataSet GetRouteNameInfo(int iRouteCode)
        {
            DataSet stRtMain=new DataSet();
            try
            {
                string strSql;
                strSql = "SELECT * FROM SFIS1.C_ROUTE_CONTROL_T WHERE ROUTE_CODE='" + iRouteCode + "' AND STATE_FLAG='0' ORDER BY STEP_SEQUENCE";
                oleConn.Close();
                oleConn.Open();
                OleDbDataAdapter oda = new OleDbDataAdapter(strSql, oleConn);
                oda.Fill(stRtMain, "ROUTE_CONTROL");
                
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message.ToString());
            }
            finally
            {
                oleConn.Close();
            }
            return stRtMain;
        }
        //Get RepairStation
        public DataSet GetRouteRepairInfo(int iRouteCode)
        {
            DataSet stRtRepair = new DataSet();
            try
            {
                string strSql;
                strSql = "SELECT * FROM SFIS1.C_ROUTE_CONTROL_T WHERE ROUTE_CODE='" + iRouteCode + "' AND STATE_FLAG='1' ORDER BY STEP_SEQUENCE";
                oleConn.Close();
                oleConn.Open();
                OleDbDataAdapter oda = new OleDbDataAdapter(strSql, oleConn);
                oda.Fill(stRtRepair, "ROUTE_CONTROL_RP");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message.ToString());
            }
            finally
            {
                oleConn.Close();
            }
            return stRtRepair;
        }
        //Get Main Line
        public List<RouteTableModel> GetRouteMainLine(int iRouteCode)
        {
            //ArrayList arrMainLine = new ArrayList();
            List<RouteTableModel> arrMainLine = new List<RouteTableModel>();
            try
            {
                string strSql = "SELECT * FROM SFIS1.C_ROUTE_CONTROL_T WHERE ROUTE_CODE=" + iRouteCode + " ORDER BY STEP_SEQUENCE";
                OleDbCommand odc = new OleDbCommand(strSql, oleConn);
                oleConn.Close();
                oleConn.Open();
                OleDbDataReader oda = odc.ExecuteReader();
                while (oda.Read())
                {
                    RouteTableModel rtmTemp = new RouteTableModel();
                    rtmTemp.RouteCode = int.Parse(oda["ROUTE_CODE"].ToString());
                    rtmTemp.GroupName = oda["GROUP_NAME"].ToString();
                    rtmTemp.GroupNext = oda["GROUP_NEXT"].ToString();
                    rtmTemp.StateFlag = int.Parse(oda["STATE_FLAG"].ToString());
                    rtmTemp.StepSeqNo = int.Parse(oda["STEP_SEQUENCE"].ToString());
                    rtmTemp.RouteDesc = oda["ROUTE_DESC"].ToString();
                    arrMainLine.Add(rtmTemp);
                }

            }
            catch (Exception ex)
            {
                throw new Exception();
            }
            finally
            {
                oleConn.Close();
            }
            return arrMainLine;
        }
        public string GetRouteName(int RouteId)
        {
            string strRouteName = "N/A";
            try
            {
                string strSql = "SELECT DISTINCT ROUTE_NAME FROM SFIS1.C_ROUTE_CONTROL_T A,SFIS1.C_ROUTE_NAME_T B WHERE A.ROUTE_DESC=B.ROUTE_DESC";
                        strSql=strSql+" AND B.ROUTE_CODE="+RouteId;
                OleDbCommand odc=new OleDbCommand(strSql,oleConn);
                oleConn.Close();
                oleConn.Open();
                OleDbDataReader oda=odc.ExecuteReader();
                while(oda.Read())
                {
                    strRouteName=oda["ROUTE_NAME"].ToString();
                }

            }
            catch (Exception ex)
            { 
                strRouteName="N/A";
            }
            finally
            {
                oleConn.Close();
            }

            return strRouteName;
        }
        public ArrayList GetRouteNameList()
        {
            ArrayList arrRouteList = new ArrayList();
            try
            {
                string strSql = "SELECT ROUTE_CODE,ROUTE_NAME FROM SFIS1.C_ROUTE_NAME_T";
                OleDbCommand odc = new OleDbCommand(strSql, oleConn);
                oleConn.Close();
                oleConn.Open();
                OleDbDataReader oda = odc.ExecuteReader();
                while(oda.Read())
                {
                    arrRouteList.Add(oda["ROUTE_NAME"].ToString());
                }
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }
            finally
            {
                oleConn.Close();
            }
            return arrRouteList;
        }
        //Get Route Code
        public int GetRouteCode(string strRouteName)
        {
            int iRetValue=0;
            try
            {
                string strSql = "SELECT DISTINCT ROUTE_CODE FROM SFIS1.C_ROUTE_NAME_T WHERE ROUTE_NAME='" +strRouteName+ "'";
                oleConn.Close();
                oleConn.Open();
                OleDbCommand odc = new OleDbCommand(strSql, oleConn);
                OleDbDataReader oda = odc.ExecuteReader();
                while (oda.Read())
                {
                    iRetValue = int.Parse(oda[0].ToString());
                }
            }
            catch (Exception ex)
            {

            }
            finally
            {
                oleConn.Close();
            }
            return iRetValue;
        }
        public ArrayList Test()
        {
            ArrayList arrRouteList = new ArrayList();
            try
            {
                string strSql = "SELECT D.SERIAL_NUMBER SN, D.MACHINE_CODE MC,TO_CHAR (D.IN_STATION_TIME, 'YYYY-MM-DD HH24:MI:SS')DT, C.HH_PN HP, C.DATE_CODE,C.LOT_NO,NVL(M.VENDOR_NAME,'N/A') AS VENDOR_NAME ,";
                strSql = strSql + "  CASE ";
                strSql = strSql + "  WHEN LOCATION IS NULL ";
                strSql = strSql + "   THEN 'N/A' ";
                strSql = strSql + "  ELSE REPLACE (LOCATION,',','||') ";
                strSql = strSql + "  END AS LOCATION,F.KEY_PART_QTY ";
                strSql = strSql + "    FROM ";
                strSql = strSql + "  (SELECT   A.*, B.MODEL_NAME, C.HH_PN FROM SMTINFO.R_SN_PKG_DETAIL_T A,(SELECT SERIAL_NUMBER, MODEL_NAME, LINE_NAME ";
                strSql = strSql + "  FROM SFISM4.R_WIP_TRACKING_T WHERE GROUP_NAME = 'SHIPPING' AND UPPER (CUSTOMER_NO) LIKE 'NV%' ";
                strSql = strSql + "   AND TO_CHAR (IN_STATION_TIME, 'yyyymmddHH24MISS') BETWEEN '20120229000000' AND '20120301000000') B, ";
                strSql = strSql + "  IQC.R_KPN_INCOMING_T C ";
                strSql = strSql + "  WHERE A.SERIAL_NUMBER = B.SERIAL_NUMBER AND A.PKG_ID = C.PKG_ID ";
                strSql = strSql + "   ORDER BY B.SERIAL_NUMBER) D, SFIS1.C_BOM_DETAIL_T F , IQC.R_KPN_INCOMING_T C LEFT JOIN  ";
                strSql = strSql + "  IQC.C_VENDOR_CODE_T  M  ON M.VENDOR_CODE = C.RESERVE3 ";
                strSql = strSql + "  WHERE C.PKG_ID = D.PKG_ID ";
                strSql = strSql + "  AND INSTR (F.BOM_NO, D.MODEL_NAME) > 0 ";
                strSql = strSql + "  AND F.FEEDER_NO = D.FEEDER_NUMBER ";
                strSql = strSql + "  AND F.KEY_PART_NO = D.HH_PN ";
                strSql = strSql + "  AND M.VENDOR_NAME IS NOT NULL ";
                OleDbCommand odc = new OleDbCommand(strSql, oleConn);
                oleConn.Close();
                oleConn.Open();
                OleDbDataReader oda = odc.ExecuteReader();
               
                while (oda.Read())
                {
                    arrRouteList.Add(new Model.TestModel
                        (
                            oda[0].ToString(),
                            oda[1].ToString(),
                            oda[2].ToString(),
                            oda[3].ToString(),
                            oda[4].ToString(),
                            oda[5].ToString(),
                            oda[6].ToString(),
                            oda[7].ToString(),
                            oda[8].ToString()  
                        ));
                }
                
              
            }
            catch (Exception ex)
            {
                string xx = ex.ToString();
            }
            finally
            {
                oleConn.Close();
            }
            return arrRouteList;
        }
        public string ProcedureTest()
        {
            string arrRouteList = "N/A";
            try
            {

                oleConn.Close();
                oleConn.Open();

                OleDbCommand oleCmd = new OleDbCommand("SFIS1.CHECK_SN", oleConn);
                oleCmd.CommandType = CommandType.StoredProcedure;
                OleDbParameter[] parms = new OleDbParameter[]{
																	   new OleDbParameter("DATA",OleDbType.VarChar,20),
																	   new OleDbParameter("RES",OleDbType.VarChar,50)
																   };
                parms[0].Direction = ParameterDirection.Input;
                parms[0].Value = "123";
                parms[1].Direction = ParameterDirection.ReturnValue;
                if (parms != null)
                {
                    foreach (OleDbParameter parm in parms)
                    {
                        oleCmd.Parameters.Add(parm);
                    }
                }
                oleCmd.ExecuteNonQuery();
                string empRes = oleCmd.Parameters[1].Value.ToString().Trim();
                arrRouteList = empRes;
            }
            catch (Exception ex)
            {
            }
            finally
            {
                oleConn.Close();
            }
            return arrRouteList;
        }

        public ArrayList GetSNsByMoOpition(string strMoOption)
        {
            ArrayList SNs = new ArrayList();
            try
            {
                string strSql = "SELECT START_SN,END_SN FROM SFISM4.R_MO_BASE_T WHERE MO_OPTION='" + strMoOption + "'";
              
                OleDbCommand odc = new OleDbCommand(strSql, oleConn);
                oleConn.Close();
                oleConn.Open();
                OleDbDataReader oda = odc.ExecuteReader();

                while (oda.Read())
                {
                    SNs.Add(new Model.SnInfo
                        (
                            oda[0].ToString(),
                            oda[1].ToString()
                        ));
                }
                oda.Close();
            }
            catch (Exception ex)
            {
                string xx = ex.ToString();
            }
            finally
            {
                oleConn.Close();
            }
            return SNs;
        }
      #region
        //Get unit Report Info
        public ArrayList GetUnitReportInfo(string sDate,string sStation,string sGuid,string sCategory,string sMfName,string sMfSite)
        {
            ArrayList UnitReport = new ArrayList();
            try
            {
                string strSql = "SELECT SERIAL_NUMBER,MODEL_NAME,GROUP_NAME,LINE_NAME,GRADE,START_TIME,EMP_NO FROM (";
                strSql = strSql + " SELECT A.SERIAL_NUMBER,A.MODEL_NAME,A.GROUP_NAME,A.LINE_NAME,'FAIL' AS GRADE,A.EMP_NO,TO_CHAR(IN_STATION_TIME,'yyyy-mm-dd-hh24-mi-ss') AS START_TIME,NVL(B.ERROR_ITEM_CODE,'N/A') ERROR_ITEM_CODE ";
                strSql = strSql + " FROM SFISM4.R_SN_DETAIL_T A, SFISM4.R_REPAIR_T  B ";
                strSql = strSql + " WHERE A.IN_STATION_TIME=B.TEST_TIME(+) ";
                strSql = strSql + "  AND A.SERIAL_NUMBER=B.SERIAL_NUMBER(+) ";
                strSql = strSql + "  AND A.ERROR_FLAG='1' ";
                strSql = strSql + "  AND TO_CHAR(A.IN_STATION_TIME,'yyyymmdd')='" + sDate + "' ";
                strSql = strSql + "  AND GROUP_NAME IN(" + sStation + ")  ";
                strSql = strSql + "   ) WHERE ERROR_ITEM_CODE<>'TNI' ";
                strSql = strSql + "  UNION ALL ";
               // strSql = strSql + "  ";
                strSql = strSql + "  SELECT SERIAL_NUMBER,MODEL_NAME,GROUP_NAME,LINE_NAME,'PASS' AS GRADE,TO_CHAR(IN_STATION_TIME,'yyyy-mm-dd-hh24-mi-ss') AS START_TIME,EMP_NO FROM SFISM4.R_SN_DETAIL_T ";
                strSql = strSql + "  WHERE TO_CHAR(IN_STATION_TIME,'yyyymmdd')='" + sDate + "' ";
                strSql = strSql + "  AND GROUP_NAME IN(" + sStation + ")   ";
                strSql = strSql + "  AND ERROR_FLAG='0' ";

                OleDbCommand odc = new OleDbCommand(strSql, oleConn);
                oleConn.Close();
                oleConn.Open();
                OleDbDataReader oda = odc.ExecuteReader();

                while (oda.Read())
                {
                    UnitReport.Add(new Model.BrFenixDell
                        (
                            1,
                            oda[5].ToString(),
                            null,
                            sGuid,
                            oda[2].ToString(),
                            oda[6].ToString(),
                            sCategory,
                            sMfName,
                            sMfSite,
                            oda[1].ToString(),
                            oda[0].ToString(),
                            oda[4].ToString(),
                            oda[5].ToString(),
                            null,
                            oda[3].ToString()
                        ));
                }
                oda.Close();
            }
            catch (Exception ex)
            {
                string xx = ex.ToString();
            }
            finally
            {
                oleConn.Close();
            }
            return UnitReport;
        }
        //Get Lot Based report Pass
        public ArrayList GetPassLotReportInfo(string sDate, string sStation, string sGuid, string sCategory, string sMfName, string sMfSite)
        {
            ArrayList UnitReport = new ArrayList();
            try
            {
                string strSql = "SELECT MODEL_NAME,GROUP_NAME,LINE_NAME,'PASS' AS GRADE,EMP_NO,TO_CHAR(SYSDATE,'yyyy-mm-dd-hh24-mi-ss') AS START_TIME,MO_NUMBER,COUNT(SERIAL_NUMBER) QTY ";
                strSql = strSql + "  FROM SFISM4.R_SN_DETAIL_T ";
                strSql = strSql + " WHERE TO_CHAR (IN_STATION_TIME, 'yyyymmdd')='" + sDate + "' ";
                strSql = strSql + "  AND GROUP_NAME IN(" + sStation + ")  ";
                strSql = strSql + "  AND ERROR_FLAG = '0' ";
                strSql = strSql + "  GROUP BY MO_NUMBER,MODEL_NAME,GROUP_NAME,LINE_NAME,EMP_NO ";
  
                OleDbCommand odc = new OleDbCommand(strSql, oleConn);
                oleConn.Close();
                oleConn.Open();
                OleDbDataReader oda = odc.ExecuteReader();

                if (oda.Read())
                { 
                    UnitReport.Add(new Model.BrFenixDell
                        (
                            Convert.ToInt32(oda[7].ToString()),                  //qty
                            oda[5].ToString(),  //start time
                            null,               //end time
                            sGuid,              //guid 
                            oda[1].ToString(),  //station
                            oda[4].ToString(),  //emp
                            sCategory,          
                            sMfName,
                            sMfSite,
                            oda[0].ToString(),        //pn
                            oda[6].ToString(),         //sn
                            oda[3].ToString(),  //Pass
                            oda[5].ToString(),  //test start time
                            null,               //test end time
                            oda[2].ToString() //test line
                        )); 
                }
                while (oda.Read())
                {
                   if (((BrFenixDell)UnitReport[0]).STATIONNAME.IndexOf(oda[1].ToString())<0)
                        ((BrFenixDell)UnitReport[0]).STATIONNAME = ((BrFenixDell)UnitReport[0]).STATIONNAME + "," + oda[1].ToString();
                   if (((BrFenixDell)UnitReport[0]).EMP.IndexOf(oda[4].ToString()) < 0)
                       ((BrFenixDell)UnitReport[0]).EMP = ((BrFenixDell)UnitReport[0]).EMP + "," + oda[4].ToString();
                   if (((BrFenixDell)UnitReport[0]).TESTLINE.IndexOf(oda[2].ToString()) < 0)
                       ((BrFenixDell)UnitReport[0]).TESTLINE = ((BrFenixDell)UnitReport[0]).TESTLINE + "," + oda[2].ToString();
                   if (((BrFenixDell)UnitReport[0]).MFSN.IndexOf(oda[6].ToString()) < 0)
                       ((BrFenixDell)UnitReport[0]).MFSN = ((BrFenixDell)UnitReport[0]).MFSN + "," + oda[6].ToString();
                }
                oda.Close();
            }
            catch (Exception ex)
            {
                string xx = ex.ToString();
            }
            finally
            {
                oleConn.Close();
            }
            return UnitReport;
        }
        //Get Lot Based report Fail
        public ArrayList GetFailLotReportInfo(string sDate, string sStation, string sGuid, string sCategory, string sMfName, string sMfSite)
        {
            ArrayList UnitReport = new ArrayList();
            try
            {
                string strSql = "SELECT MODEL_NAME, GROUP_NAME,LINE_NAME, GRADE,START_TIME, EMP_NO,MO_NUMBER,COUNT(SERIAL_NUMBER) QTY ";
                strSql = strSql + "   FROM (SELECT A.SERIAL_NUMBER,A.MODEL_NAME,A.GROUP_NAME,A.LINE_NAME,'FAIL' AS GRADE,A.EMP_NO, ";
                strSql = strSql + "   TO_CHAR (SYSDATE, 'yyyy-mm-dd-hh24-mi-ss') AS START_TIME, ";
                strSql = strSql + "   NVL (B.ERROR_ITEM_CODE, 'N/A') ERROR_ITEM_CODE,A.MO_NUMBER ";
                strSql = strSql + " FROM SFISM4.R_SN_DETAIL_T A, SFISM4.R_REPAIR_T B ";
                strSql = strSql + " WHERE  A.IN_STATION_TIME = B.TEST_TIME(+) ";
                strSql = strSql + " AND A.SERIAL_NUMBER = B.SERIAL_NUMBER(+) ";
                strSql = strSql + "  AND A.ERROR_FLAG = '1' ";
                strSql = strSql + "  AND TO_CHAR (A.IN_STATION_TIME, 'yyyymmdd') = '" + sDate + "' ";
                strSql = strSql + "  AND GROUP_NAME IN(" + sStation + ") ) ";
                strSql = strSql + "   WHERE ERROR_ITEM_CODE <> 'TNI' ";
                strSql = strSql + "   GROUP BY MODEL_NAME,GROUP_NAME,LINE_NAME,EMP_NO,MO_NUMBER ";

                OleDbCommand odc = new OleDbCommand(strSql, oleConn);
                oleConn.Close();
                oleConn.Open();
                OleDbDataReader oda = odc.ExecuteReader();

                if (oda.Read())
                {
                    UnitReport.Add(new Model.BrFenixDell
                        (
                           Convert.ToInt32(oda[7].ToString()),          //qty
                            oda[4].ToString(),  //start time
                            null,               //end time
                            sGuid,              //guid 
                            oda[1].ToString(),  //station
                            oda[5].ToString(),  //emp
                            sCategory,
                            sMfName,
                            sMfSite,
                            oda[0].ToString(),       //pn
                            oda[6].ToString(),      //sn
                            oda[3].ToString(),  //Pass
                            oda[4].ToString(),  //test start time
                            null,               //test end time
                            oda[2].ToString() //test line
                        ));
                }
                while (oda.Read())
                {
                    if (((BrFenixDell)UnitReport[0]).STATIONNAME.IndexOf(oda[1].ToString()) < 0)
                        ((BrFenixDell)UnitReport[0]).STATIONNAME = ((BrFenixDell)UnitReport[0]).STATIONNAME + "," + oda[1].ToString();
                    if (((BrFenixDell)UnitReport[0]).EMP.IndexOf(oda[4].ToString()) < 0)
                        ((BrFenixDell)UnitReport[0]).EMP = ((BrFenixDell)UnitReport[0]).EMP + "," + oda[4].ToString();
                    if (((BrFenixDell)UnitReport[0]).TESTLINE.IndexOf(oda[2].ToString()) < 0)
                        ((BrFenixDell)UnitReport[0]).TESTLINE = ((BrFenixDell)UnitReport[0]).TESTLINE + "," + oda[2].ToString();
                    if (((BrFenixDell)UnitReport[0]).MFSN.IndexOf(oda[6].ToString()) < 0)
                        ((BrFenixDell)UnitReport[0]).MFSN = ((BrFenixDell)UnitReport[0]).MFSN + "," + oda[6].ToString();
                }
                oda.Close();
            }
            catch (Exception ex)
            {
                string xx = ex.ToString();
            }
            finally
            {
                oleConn.Close();
            }
            return UnitReport;
        }
       #endregion
        public IList getBlockMACCurrent(string forePartStr)
        {
            IList currentInfo = new ArrayList();
            BlockInfo blockInfo;

            string sqlStr = "select BLOCK_ID,BLOCK_START,BLOCK_END,PRODUCE_NAME,C_EMP,C_DATE,CLOSE_FLAG from compaq.COMPAQ_BLOCK_T " +
                "where ( substr(block_start,0," + forePartStr.Length + ")='" + forePartStr + "' or '" + forePartStr + "'='' or  '" + forePartStr + "' is null)";
            OleDbCommand odc = new OleDbCommand(sqlStr, oleConn);
            oleConn.Close();
            oleConn.Open();
            OleDbDataReader dr = odc.ExecuteReader();
            while (dr.Read())
             {
                  blockInfo = new BlockInfo(dr[0].ToString(), string.Empty, dr[1].ToString(), dr[2].ToString(), string.Empty,
                        string.Empty, dr[3].ToString(), string.Empty, string.Empty, dr[6].ToString(), string.Empty,
                        dr[4].ToString(), dr[5].ToString());
                 currentInfo.Add(blockInfo);
            }
         
            return currentInfo;
        }

        public bool bInsertTestSql(string sSql,string sParam)
        {
            bool bIsInsertOk = false;
            string strSql = "INSERT INTO SFIS1.C_PARAMETER_INI(PRG_NAME,VR_CLASS,VR_ITEM,VR_NAME,VR_VALUE,VR_DESC)";
            strSql += "  VALUES('ON_LINE_PRINT','DYSQL','1','" + sParam + "','TEST','" + sSql + "') ";
            oleConn.Close();
            oleConn.Open();
            OleDbCommand odc = new OleDbCommand(strSql, oleConn);
            int i=odc.ExecuteNonQuery();
            if (i > 0)
                bIsInsertOk = true;
            else
                bIsInsertOk = false;
            OleDbCommand odc1 = new OleDbCommand("COMMIT", oleConn);
            odc1.ExecuteNonQuery();
            return bIsInsertOk;
        }

        public string TestDySql()
        {
            string sRes = "N/A";
            string strSql = "SELECT VR_DESC FROM  SFIS1.C_PARAMETER_INI WHERE PRG_NAME='ON_LINE_PRINT' AND VR_CLASS='DYSQL'";
            oleConn.Close();
            oleConn.Open();
            OleDbCommand odc = new OleDbCommand(strSql, oleConn);
            oleConn.Close();
            oleConn.Open();
            OleDbDataReader oda = odc.ExecuteReader();
            strSql = "";
            while (oda.Read())
            {
                strSql = oda[0].ToString();
            }
            if (strSql.Trim().Length > 0)
            {
                oleConn.Close();
                oleConn.Open();
                OleDbCommand odcA = new OleDbCommand(strSql, oleConn);
                oleConn.Close();
                oleConn.Open();
                OleDbDataReader odaA = odcA.ExecuteReader();
                while (odaA.Read())
                {
                    sRes = odaA[0].ToString();
                }
            }
            return sRes;
        }

        public DataSet GetDataByPanel(string sPanel)
        {
            DataSet ds = new DataSet();
            oleConn.Close();
            oleConn.Open();

            Hashtable HT = new Hashtable();
            HT.Add("PANEL_ID", sPanel);
            HT.Add("DATA_TYPE", "WIFI_CT_HP_A");

            string sCon = "Password=sfism4;User ID=sfism4;Data Source=ESSDTEST128.68;Persist Security Info=True";
            if (RunProcedure("RE_CURSOR", OracleType.Cursor, ref ds, HT, "SFISM4.ON_LINE_PRINT.GET_ONLINE_PRINT_INFO", sCon)) ;
                return ds;
        }

        private bool RunProcedure(string ReturnParameter, OracleType ParamType, ref DataSet Dataset, Hashtable HT,
        string ProcedureName, string OracleConnection)
        {

            OracleConnection oraConn = new OracleConnection(OracleConnection);
            oraConn.Open();

            OracleCommand dacommand = new OracleCommand(ProcedureName, oraConn);

            dacommand.CommandType = CommandType.StoredProcedure;
            IDictionaryEnumerator Enumerator;
            Enumerator = HT.GetEnumerator();
            object Value = null;
            OracleParameter OracleParam;
            OracleParam = dacommand.Parameters.Add(new OracleParameter(ReturnParameter, ParamType));
            OracleParam.Direction = ParameterDirection.Output;
            while (Enumerator.MoveNext())
            {
                Value = Enumerator.Value;
                OracleParam = dacommand.Parameters.Add(new OracleParameter(Enumerator.Key.ToString(), Value));
            }
            OracleDataAdapter ODAdapter = new OracleDataAdapter(dacommand);
            try
            {
                ODAdapter.Fill(Dataset);
                return true;
            }
            catch (System.Exception e)
            {
                e.ToString();
                return false;
            }
            finally
            {
                HT.Clear();
                dacommand.Parameters.Clear();
                oraConn.Close();
            }
        }

        public DataSet GetSMOStoredProc(OleDbConnection myCon)
        {
            DataSet dtProc=new DataSet();
            string sSql = "SELECT DISTINCT STOREDPROC FROM SFIS1.C_WORK_TYPE_STOREDPROC ORDER BY STOREDPROC";
            OleDbDataAdapter oda = new OleDbDataAdapter(sSql, myCon);
            myCon.Close();
            myCon.Open();

            oda.Fill(dtProc, "C_WORK_TYPE_STOREDPROC");
            myCon.Close();
            return dtProc;
        }

        public ArrayList GetSubProcsByProc(string sProcName,string sOwner,OleDbConnection myConn,string sDB)
        {
            ArrayList arr = new ArrayList();
            string sSql = "SELECT A.OBJECT_TYPE, A.OBJECT_NAME,  B.OWNER, B.OBJECT_TYPE, B.OBJECT_NAME FROM   SYS.DBA_OBJECTS A, SYS.DBA_OBJECTS B, ";
            sSql += " (SELECT OBJECT_ID, REFERENCED_OBJECT_ID FROM   (SELECT OBJECT_ID, REFERENCED_OBJECT_ID  FROM   PUBLIC_DEPENDENCY WHERE  REFERENCED_OBJECT_ID <> OBJECT_ID) A ";
            sSql += " START WITH OBJECT_ID = (SELECT OBJECT_ID FROM  SYS.DBA_OBJECTS WHERE OWNER  = '" + sOwner + "' AND   OBJECT_NAME  = '" + sProcName + "' AND   OBJECT_TYPE  = 'PROCEDURE')";
            sSql += " CONNECT BY PRIOR REFERENCED_OBJECT_ID = OBJECT_ID) C WHERE A.OBJECT_ID = C.OBJECT_ID ";
            sSql += " AND   B.OBJECT_ID = C.REFERENCED_OBJECT_ID AND   A.OWNER NOT IN ('SYS', 'SYSTEM')";
            sSql += " AND   B.OWNER NOT IN ('SYS', 'SYSTEM') AND   A.OBJECT_NAME <> 'DUAL'";
            sSql += " AND   B.OBJECT_NAME <> 'DUAL' AND   B.OBJECT_TYPE='PROCEDURE'";
            OleDbCommand odc = new OleDbCommand(sSql, myConn);
            myConn.Close();
            myConn.Open();
            OleDbDataReader odr = odc.ExecuteReader();
            SFC_Tools.SFCStartup.dba.InsertProcInfoToDB(sDB, sOwner, sProcName);
            while (odr.Read())
            {
                InsertProcInfoToDB(sDB, odr[2].ToString(), odr[4].ToString());
            }
            myConn.Close();
            bCommit();
            return arr;
        }

        public int InsertProcInfoToDB(string sDb,string sOwner,string sProc)
        {
            int i = 0;
            try
            {
                string sSql = "SELECT COUNT(*) FROM SFISM4.R_SPROC_INFO_T WHERE DB_NAME='" + sDb + "' AND OWNER_NAME='" + sOwner + "' AND PROC_NAME='" + sProc + "' ";
                OleDbCommand odca = new OleDbCommand(sSql, this.oleConn);
                oleConn.Close();
                oleConn.Open();

                OleDbDataReader odr = odca.ExecuteReader();
                if (odr.Read())
                {
                    if (Convert.ToInt32(odr[0].ToString()) == 0)
                    {
                        sSql = "INSERT INTO SFISM4.R_SPROC_INFO_T(DB_NAME,OWNER_NAME,PROC_NAME) ";
                        sSql += " VALUES('" + sDb + "','" + sOwner + "','" + sProc + "') ";
                        OleDbCommand odc = new OleDbCommand(sSql, this.oleConn);
                        oleConn.Close();
                        oleConn.Open();
                        i = odc.ExecuteNonQuery();
                    }
                }
                oleConn.Close();
            }
            catch (Exception ex)
            { 
                
            }
            return i;
        }

        public DataTable GetMainRoute(int iRouteCode)
        {
            DataTable dt = new DataTable();
            string sSql;
            sSql = "SELECT DISTINCT GROUP_NAME,GROUP_NEXT,STATE_FLAG FROM SFIS1.C_ROUTE_CONTROL_T WHERE ROUTE_CODE=" + iRouteCode + " AND STATE_FLAG=0 AND GROUP_NAME NOT IN(SELECT GROUP_NEXT FROM  SFIS1.C_ROUTE_CONTROL_T WHERE ROUTE_CODE=" + iRouteCode + " AND STATE_FLAG=1)";
            OleDbDataAdapter oda = new OleDbDataAdapter(sSql, oleConn);
            if (oleConn.State == ConnectionState.Closed)
                oleConn.Open();
            oda.Fill(dt);

            return dt;
        }

        public DataTable GetMainRoute(DataTable dtAll)
        {
            DataTable dt = new DataTable();
            var sRepair = (from t in dtAll.AsEnumerable()
                           where t.Field<Decimal>("STATE_FLAG") == 1
                           select t.Field<string>("GROUP_NEXT")).ToList();

            var sMain = from tt in dtAll.AsEnumerable()
                      where tt.Field<Decimal>("STATE_FLAG") == 0
                    && !sRepair.Contains(tt.Field<string>("GROUP_NAME"))
                      select tt;
            if(sMain.Count()>0)
                dt = sMain.CopyToDataTable();

            return dt;
        }

        public DataTable GetRepairStationsByRouteCode(DataTable dtAll)
        {
            DataTable dt = new DataTable();
            var sRepair = from t in dtAll.AsEnumerable()
                          where t.Field<Decimal>("STATE_FLAG") == 1
                          select t;
            if (sRepair.Count()>0)
                dt=sRepair.CopyToDataTable();
            return dt;
        }


        public DataTable GetReturnStationsByRouteCode(DataTable dtAll)
        {
            DataTable dt = new DataTable();

            var sRepair = (from t in dtAll.AsEnumerable()
                          where t.Field<Decimal>("STATE_FLAG") == 1
                          select t.Field<string>("GROUP_NEXT")
                          ).ToList();

            var sRetrun = from tt in dtAll.AsEnumerable()
                          where sRepair.Contains(tt.Field<string>("GROUP_NAME"))
                           select tt;
            if(sRetrun.Count()>0)
                dt = sRetrun.CopyToDataTable();
            return dt;
        }

        public DataTable GetAllRouteInfo(int iRouteCode)
        {
            DataTable dt = new DataTable();
            string sSql;
            sSql = "SELECT * FROM SFIS1.C_ROUTE_CONTROL_T WHERE ROUTE_CODE=" + iRouteCode + " ORDER BY STEP_SEQUENCE";
            OleDbDataAdapter oda = new OleDbDataAdapter(sSql, oleConn);
            if (oleConn.State == ConnectionState.Closed)
                oleConn.Open();
            oda.Fill(dt);

            return dt;
        }

        public DataTable GetRepairStationsByRouteCode(int iRouteCode)
        {
            DataTable dt = new DataTable();
            string sSql = "SELECT GROUP_NAME,GROUP_NEXT FROM SFIS1.C_ROUTE_CONTROL_T WHERE ROUTE_CODE=" + iRouteCode + "  AND STATE_FLAG=1 ORDER BY STEP_SEQUENCE";
            OleDbDataAdapter oda = new OleDbDataAdapter(sSql, oleConn);
            oleConn.Close();
            oleConn.Open();
            oda.Fill(dt);
            oleConn.Close();
            return dt;
        }

        public DataTable GetReturnStationsByRouteCode(int iRouteCode)
        {
            DataTable dt = new DataTable();
            string sSql = "SELECT GROUP_NAME,GROUP_NEXT  FROM SFIS1.C_ROUTE_CONTROL_T  WHERE ROUTE_CODE=" + iRouteCode + " AND GROUP_NAME IN ";
            sSql = sSql + " (SELECT DISTINCT GROUP_NEXT FROM SFIS1.C_ROUTE_CONTROL_T WHERE ROUTE_CODE = " + iRouteCode + " AND STATE_FLAG = 1) ORDER BY STEP_SEQUENCE";
            OleDbDataAdapter oda = new OleDbDataAdapter(sSql, oleConn);
            if (oleConn.State == ConnectionState.Closed)
                oleConn.Open();
            oda.Fill(dt);
            return dt;
        }

        public int GetNewRouteCode()
        {
            int iRouteCode = 0;
            string sSql = " SELECT MAX(ROUTE_CODE)AS ROUTE_CODE FROM SFIS1.C_ROUTE_NAME_T ";
            OleDbCommand odc = new OleDbCommand(sSql, oleConn);
            if (oleConn.State == ConnectionState.Closed)
                oleConn.Open();
            OleDbDataReader odr= odc.ExecuteReader();
            while (odr.Read())
            {
                iRouteCode = Convert.ToInt32(odr["ROUTE_CODE"])+1;
            }
            return iRouteCode;
        }

        public bool InsertMailDataToDB(DataTable dt)
        {
            bool bIsInsertDone = true;
            try
            {
                foreach (DataRow dr in dt.Rows)
                {
                    string sSql = "SELECT COUNT(*) AS CNT FROM SFISM4.R_MAIL_LOG_T WHERE MAIL_UID='" + dr["Uid"].ToString() + "' ";
                    oleConn.Close();
                    oleConn.Open();
                    OleDbCommand odc = new OleDbCommand(sSql, oleConn);
                    OleDbDataReader odr=odc.ExecuteReader();
                    odr.Read();
                    if (Convert.ToInt32(odr["CNT"].ToString()) == 0)
                    {
                        sSql = "INSERT INTO SFISM4.R_MAIL_LOG_T (MAIL_UID,MAIL_FROM,MAIL_CC,MAIL_SUB, MAIL_SIZE, MAIL_DATE, MAIL_FLAG_SEEN, MAIL_FLAG_SELECT, MAIL_FLAG_ATTACH,";
                        sSql += " MAIL_FLAG_TRANSFER,  MAIL_FLAG_IMPORTANT,  MAIL_TO/*,MAIL_BODY,*/MAIL_DIR_ID) ";
                        sSql += "  VALUES ('" + dr["Uid"].ToString() + "','" + dr["From"].ToString() + "','" + dr["CC"].ToString() + "','" + dr["Sub"].ToString().Replace("'", "’") + "','" + dr["CSize"].ToString() + "',TO_DATE('" + dr["MyDate"].ToString() + "','yyyy/mm/dd hh24:mi'), ";
                        sSql += " '" + dr["seenFlag"].ToString() + "','" + dr["selectFlag"].ToString() + "','" + dr["attachFlag"].ToString() + "','" + dr["transferFlag"].ToString() + "','" + dr["importantFlag"].ToString() + "','" + dr["CC"].ToString() + "',1) ";

                        OleDbCommand cmd = new OleDbCommand(sSql, oleConn);
                        //cmd.Connection = oleConn;
                        if (oleConn.State != ConnectionState.Open)
                        {
                            oleConn.Open();
                        }

                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                this.bRollBack();
                oleConn.Close();
                throw new Exception(ex.Message);

            }
            this.bCommit();
            return bIsInsertDone;
        }
    }//this is the end
}
