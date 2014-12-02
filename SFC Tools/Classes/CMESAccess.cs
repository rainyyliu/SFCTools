using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Configuration;
using SFC_Tools.Model;
using System.Data;

namespace SFC_Tools.Classes
{
    static class CMESAccess
    {
        private static SqlConnection conn = new SqlConnection(ConfigurationManager.AppSettings["MsSQLCMESConn"].ToString());
        public static mdHostInfo GetHostInfoByName(string pName)
        {
            mdHostInfo hostInfo=new mdHostInfo();;
            string sSql = "SELECT hostid,mdh.[host_name] host_name,mdh.host_status,mdh.host_desc,isnull(mdh.terminal_protocol,0) terminal_protocol   FROM md_device_host AS mdh WHERE mdh.[host_name] LIKE '" + pName + "%'";
            conn.Close();
            conn.Open();
            SqlCommand cmd = new SqlCommand(sSql, conn);
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            { 
                hostInfo.HostID=dr["hostid"].ToString();
                hostInfo.HostName=dr["host_name"].ToString();
                hostInfo.HostStatus=Convert.ToInt32(dr["host_status"].ToString());
                hostInfo.HostDesc=dr["host_desc"].ToString();
                hostInfo.TerminalProtocol=Convert.ToInt32(dr["terminal_protocol"].ToString());
            }
            conn.Close();
            return hostInfo;
        }

        public static DataSet GetStationInfoByHostID(string pHostId)
        {
            DataSet ds=new DataSet();
            string sSql = "SELECT hostid,deviceid,nodeid,input_type,output_type,socketip,socket_port,lineid,sectionid,groupid,stationid ";
            sSql += " FROM md_device_station_config";
            sSql += " WHERE hostid='"+pHostId+"' and hostid='1' AND isactive='Y' ORDER BY lineid,nodeid,sectionid,stationid";

            SqlCommand sc = new SqlCommand(sSql, conn);
            conn.Close();
            conn.Open();
            SqlDataAdapter sda = new SqlDataAdapter(sc);
            sda.Fill(ds);
            return ds;
        }

        public static void SaveBomMainInfo(MDBomMainInfo mainInfo)
        {
            string sSql = "INSERT INTO md_bom_main(materialno,plantid,source_type,useful_life,supplier_name,supplier_materialno,length,width, ";
            sSql += "  height,unit_price,gross_weight,net_weight,[description],isactive,created,createdby,updated,updatedby,clientid,bomno)";
            sSql += " VALUES( '" + mainInfo.MaterialNo + "','" + mainInfo.PlantID + "','" + mainInfo.SourceType + "','" + mainInfo.UsefulLife + "','" + mainInfo.SupplierName + "','" + mainInfo.SupplierMaterialNo + "'," + mainInfo.Length + "," + mainInfo.Width + ", ";
            sSql += " " + mainInfo.Height + "," + mainInfo.UnitPrice + "," + mainInfo.GrossWeight + "," + mainInfo.NetWeight + ",N'" + mainInfo.Description + "','" + mainInfo.IsActive + "',GETDATE(),'" + mainInfo.CreatedBy + "',GETDATE(),'" + mainInfo.UpdatedBy + "' ";
            sSql += " ,'" + mainInfo.ClientID + "','" + mainInfo.BomNo + "' )";
            
            SqlCommand sc = new SqlCommand(sSql, conn);
            conn.Close();
            conn.Open();
            sc.ExecuteNonQuery();
        }

        public static void SaveBomDetailInfo(MDBomDetailInfo detailInfo)
        {
            string sSql = "INSERT INTO md_bom_detail(materialno,plantid,parent_materialno,seqno,material_level,unitqty, ";
            sSql += "  [description],isactive,created,createdby,updated,updatedby) ";
            sSql += " VALUES( '" + detailInfo.MaterialNo + "','" + detailInfo.PlantID + "','" + detailInfo.ParentMaterialNo + "','" + detailInfo.SeqNo + "','" + detailInfo.MaterialLevel + "','" + detailInfo.UnityQty + "' , ";
            sSql += " '" + detailInfo.Description + "','" + detailInfo.IsActive + "',GETDATE(),'" + detailInfo.CreatedBy + "',GETDATE(),'" + detailInfo.UpdatedBy + "' )";

            SqlCommand sc = new SqlCommand(sSql, conn);
            conn.Close();
            conn.Open();
            sc.ExecuteNonQuery();
        }

        public static void SaveBomAltInfo(MDBomAltInfo altInfo)
        {
            string sSql = " INSERT INTO md_bom_alt(clientid,plantid,bomno,materialno,altmaterialno,seqno,usage,altrate, ";
            sSql += "  isactive,created,createdby,updated,updatedby) ";
            sSql += "VALUES( '" + altInfo.ClientID + "','" + altInfo.PlantID + "','" + altInfo.BomNo + "','" + altInfo.MaterialNo + "','" + altInfo.AltMaterialNo + "'," + altInfo.SeqNo + "," + altInfo.Usage + "," + altInfo.Altrate + ", ";
            sSql += " '" + altInfo.IsActive + "',GETDATE(),'" + altInfo.CreatedBy + "',GETDATE(),'" + altInfo.UpdatedBy + "') ";

            SqlCommand sc = new SqlCommand(sSql, conn);
            conn.Close();
            conn.Open();
            sc.ExecuteNonQuery();
        }
    }
}
