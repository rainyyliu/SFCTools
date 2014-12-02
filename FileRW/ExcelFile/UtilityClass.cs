using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Management;
using System.Net;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using SFC_Tools.FileWR.ExcelFile;

namespace SFC_Tools.FileWR.ExcelFile
{
    public class UtilityClass
    {
        const string IMAGES = @"\Images\";
        static ConfigInfo configInfo = new ConfigInfo();
        static DataSet dsResourceInfo = null;
        static string LanguageID = "";//Language ID
        static string DefaultLanguageID = "";//Language ID
        static string HostServiceName = "";
        static string HostName = "";
        static string HostPort = "";
        static string HostServiceType = "";
        static string LogonUserID = "";
        static string LogonUserName = "";
        static string ProcessID = "";
        static string SessionID = "";
        public static Color SystemBackColor = Color.Empty;
        public static Color SystemForeColor = Color.Empty;
        public static Color SystemLineColor = Color.Empty;
        public static Font SystemFont = null;

        public UtilityClass()
        {
            //configInfo = new ConfigInfo();
        }

        public static string GetAppPath()
        {
            return Application.StartupPath;
        }

        public static string GetImagesPath()
        {
            return GetAppPath() + IMAGES;
        }

        public static string GetClientIPAddress()
        {
            string hostName = Dns.GetHostName();
            return Dns.GetHostEntry(hostName).AddressList[0].ToString();
        }

        [DllImport("Iphlpapi.dll")]
        static extern int SendARP(Int32 DestIP, Int32 SrcIP, ref Int64 MacAddr, ref Int32 PhyAddrLen);

        [DllImport("Ws2_32.dll")]
        static extern Int32 inet_addr(string ipaddr);

        public static string GetClientMacAddress()
        {
            /*if (MacAddress == "")
            {
                MacAddress = GetClientMacAddress(GetClientIPAddress());
            }
            return MacAddress;*/
            string str = "";
            ManagementClass mcMAC = new ManagementClass("Win32_NetworkAdapterConfiguration");
            ManagementObjectCollection mocMAC = mcMAC.GetInstances();
            foreach (ManagementObject m in mocMAC)
            {
                if ((bool)m["IPEnabled"])
                {
                    str = m["MacAddress"].ToString();
                    break;
                }
            }
            return str;
        }

        public static string GetClientMacAddress(string clientIP)
        {
            StringBuilder strReturn = new StringBuilder();
            try
            {
                Int32 remote = inet_addr(clientIP);
                Int64 macinfo = new Int64();
                Int32 length = 6;
                SendARP(remote, 0, ref macinfo, ref length);

                string temp = System.Convert.ToString(macinfo, 16).PadLeft(12, '0').ToUpper();

                int x = 12;
                for (int i = 0; i < 6; i++)
                {
                    if (i == 5) 
                    { 
                        strReturn.Append(temp.Substring(x - 2, 2)); 
                    }
                    else 
                    { 
                        strReturn.Append(temp.Substring(x - 2, 2) + ":"); 
                    }
                    x -= 2;
                }

                return strReturn.ToString();
            }
            catch
            {
                return strReturn.ToString();
            }
        }

        public static string GetClientHostName()
        {
            return Dns.GetHostName();
        }

        public static string GetClientOSVersion()
        {
            return Environment.OSVersion.ToString();
        }

        public static string GetClientOSUserName()
        {
            return Environment.UserDomainName + @"\" + Environment.UserName;
        }

        public static string GetClientHardDiskID()
        {
            String HDid = "";
            ManagementClass mc = new ManagementClass("Win32_DiskDrive");
            ManagementObjectCollection moc = mc.GetInstances();
            foreach (ManagementObject mo in moc)
            {
                HDid = (string)mo.Properties["Model"].Value;
                break;
            }
            return HDid;
        }

        public static string GetClientLogicHardDiskID(string disk /*  "C:"*/)
        {
            String HDid = "";
            ManagementClass mcHD = new ManagementClass("win32_logicaldisk");
            ManagementObjectCollection mocHD = mcHD.GetInstances();
            foreach (ManagementObject m in mocHD)
            {
                if (m["DeviceID"].ToString() == disk)
                {
                    HDid = m["VolumeSerialNumber"].ToString();
                    HDid = HDid.Substring(0, 4) + "-" + HDid.Substring(HDid.Length-4, 4);
                    break;
                }
            }
            return HDid;
        }
        
        public static DateTime GetClientDateTime()
        {
            return DateTime.Now;
        }

        public static string GetFrameworkVersion()
        {
            return Environment.Version.ToString();
        }

        public static string GetSystemVersion()
        {
            return configInfo.GetSytstemVersion();
        }

        public static string GetClassName()
        {
            return "";
        }

        public static string GetLogonUserID()
        {
            if (LogonUserID == "")
            {
                LogonUserID = configInfo.ReadConfigInfo("userid", "userid");
            }
            return LogonUserID;
        }

        public static string GetLogonUserName()
        {
            if (LogonUserName == "")
            {
                LogonUserName = configInfo.ReadConfigInfo("username", "username");
            }
            return LogonUserName;
        }

        public static string GetSessionID()
        {
            if (SessionID == "")
            {
                SessionID = configInfo.ReadConfigInfo("sessionid", "sessionid");
            }
            return SessionID;
        }

        public static string GetProcessID()
        {
            if (ProcessID == "")
            {
                Process process = Process.GetCurrentProcess();
                ProcessID = process.Id.ToString();
            }
            return ProcessID;
        }

        public static string GetHostServiceName()
        {
            if (HostServiceName == "")
            {
                HostServiceName = configInfo.ReadConfigInfo("servicename", "servicename");
            }
            return HostServiceName;
        }

        public static string GetHostServiceType()
        {
            if (HostServiceType == "")
            {
                HostServiceType = configInfo.ReadConfigInfo("servicetype", "servicetype");
            }
            return HostServiceType;
        }

        public static string GetHostName()
        {
            if (HostName == "")
            {
                HostName = configInfo.ReadConfigInfo("hostname", "hostname");
            }
            return HostName;
        }

        public static string GetHostPort()
        {
            if (HostPort == "")
            {
                HostPort = configInfo.ReadConfigInfo("hostport", "hostport");
            }
            return HostPort;
        }

        public static byte[] ReadFile(string filePath)
        {
            try
            {
                FileStream fileStream = File.Open(filePath, FileMode.Open, FileAccess.Read);
                if (fileStream == null)
                    return null;
                long length = fileStream.Length;
                if (length <= 0)
                    return null;
                byte[] fileBytes = new byte[length];
                fileStream.Position = 0;
                fileStream.Read(fileBytes, 0, fileBytes.Length);
                fileStream.Close();
                return fileBytes;
            }
            catch
            {
                return null;
            }
        }

        public static void WriteFile(string filePath, byte[] dllBytes)
        {
            try
            {
                if (dllBytes.Length <= 0)
                    return;
                FileStream fileStream;
                fileStream = File.Open(filePath, FileMode.OpenOrCreate, FileAccess.ReadWrite);
                fileStream.Write(dllBytes, 0, dllBytes.Length);
                fileStream.Close();
            }
            catch
            {

            }
        }

        public static int AddImage(ImageList image, string imageName)
        {
            image.Images.Add(Image.FromFile(GetImagesPath() + imageName));
            return image.Images.Count - 1;
        }

        public static void SetDefault()
        {
            HostServiceName = "";
            HostName = "";
            HostPort = "";
            HostServiceType = "";
        }

        //DataSet and DataTable
        public static bool ExistDataInDataTable(DataTable dtData, string fieldName, string operators, object values)
        {
            DataRow[] drCheck = null;
            if (values.GetType().Name == "System.String")
            {
                drCheck = dtData.Select(fieldName + operators + "'" + values + "'");
            }
            else
            {
                drCheck = dtData.Select(fieldName + operators + values);
            }
            if (drCheck.Length > 0)
                return true;
            return false;
        }

        public static bool ExistDataInDataTable(DataTable dtData, string fieldName, object values)
        {
            return ExistDataInDataTable(dtData, fieldName, "=", values);
        }

        public static bool ExistDataInDataTable(DataTable dtData)
        {
            if (dtData == null)
                return false;
            if (dtData.Rows.Count <= 0)
                return false;
            return true;
        }

        public static DataRow[] GetDataRowsFromDataTable(DataTable dtData, string fieldName, string operators, object values)
        {
            DataRow[] drCheck = null;
            if (values.GetType().Name == "System.String")
            {
                drCheck = dtData.Select(fieldName + operators + "'" + values + "'");
            }
            else
            {
                drCheck = dtData.Select(fieldName + operators + values);
            }
            if (drCheck.Length > 0)
                return drCheck;
            return null;
        }

        public static DataRow[] GetDataRowsFromDataTable(DataTable dtData, string fieldName, object values)
        {
            return GetDataRowsFromDataTable(dtData, fieldName, "=", values);
        }

        public static bool ExistDataInDataSet(DataSet dataSet, int index)
        {
            if (dataSet == null)
                return false;
            if (dataSet.Tables.Count <= index)
                return false;
            if (dataSet.Tables[index].Rows.Count <= 0)
                return false;
            return true;
        }

        public static bool ExistDataInDataSet(DataSet dataSet)
        {
            return ExistDataInDataSet(dataSet, 0);
        }

        public static bool ExistDataInList<T>(IList<T> list)
        {
            if (list == null)
            {
                return false;
            }
            if (list.Count <= 0)
            {
                return false;
            }
            return true;
        }

        public static void CopyDataForDataSet(DataSet fromDataSet, DataSet toDataSet)
        {
            if(!ExistDataInDataSet(fromDataSet))
            {
                return;
            }
            if(toDataSet == null)
            {
                return;
            }
            CopyDataForDataTable(fromDataSet.Tables[0], toDataSet.Tables[0]);
        }

        public static void CopyDataForDataTable(DataTable fromDataTable, DataTable toDataTable)
        {
            try
            {
                if(!ExistDataInDataTable(fromDataTable))
                {
                    return;
                }
                if(toDataTable == null)
                {
                    return;
                }
                foreach (DataRow drData in fromDataTable.Rows)
                {
                    DataRow drNew = toDataTable.NewRow();
                    foreach (DataColumn dcData in fromDataTable.Columns)
                    {
                        drNew[dcData.ColumnName] = drData[dcData.ColumnName];
                    }
                }
            }
            catch
            {

            }
        }

        //Resources
        #region Resource
        public static string GetLanguageID()
        {
            if (LanguageID == "")
            {
                LanguageID = configInfo.ReadConfigInfo("language", "language");
                if (LanguageID == "")
                {
                    LanguageID = GetDefaultLanguageID();
                    if (LanguageID == "")
                    {
                        LanguageID = Application.CurrentCulture.Name.ToLower();
                    }
                }
            }
            return LanguageID;
        }

        public static void SetLanguageID(string languageID)
        {
            LanguageID = languageID;
        }

        public static string GetDefaultLanguageID()
        {
            if (DefaultLanguageID == "")
            {
                DefaultLanguageID = configInfo.ReadConfigInfo("defaultlanguage", "defaultlanguage");
            }
            return DefaultLanguageID;
        }

        private static void GetResourceInfo()
        {
            configInfo.ExecuteDataSet("SELECT RESOURCE_NAME, LANGUAGE_ID, RESOURCE_VALUE, RESOURCE_TYPE FROM SYS_RESOURCE_T", out dsResourceInfo);
        }

        public static string GetResourceString(string resourceName,string languageID)
        {
            string defaultLanguageID = "";
            if (languageID == "")
            {
                languageID = GetLanguageID();
            }

            if (languageID == "")
            {
                defaultLanguageID = GetDefaultLanguageID();
                if (defaultLanguageID != "")
                {
                    languageID = defaultLanguageID;
                }
                else
                {
                    return resourceName;
                }
            }

            if (dsResourceInfo == null)
            {
                GetResourceInfo();
            }

            if (ExistDataInDataSet(dsResourceInfo))
            {
                DataRow[] drLanguage = dsResourceInfo.Tables[0].Select("RESOURCE_NAME = '" + resourceName + "' AND LANGUAGE_ID = '" + languageID + "'");
                if (drLanguage.Length > 0)
                {
                    return drLanguage[0]["RESOURCE_VALUE"].ToString();
                }
                else
                {
                    drLanguage = dsResourceInfo.Tables[0].Select("RESOURCE_NAME = '" + resourceName + "' AND LANGUAGE_ID = '" + defaultLanguageID + "'");
                    if (drLanguage.Length > 0)
                    {
                        return drLanguage[0]["RESOURCE_VALUE"].ToString();
                    }
                }
            }
            return resourceName;

        }

        public static string GetResourceString(string resourceName)
        {
            return GetResourceString(resourceName, "");
        }

        public static Icon GetResourceIcon(string resourceName, string languageID)
        {
            string resourceValue = GetResourceString(resourceName, languageID);
            //if (resourceValue == resourceName)
            //    return null;
            if (File.Exists(GetFullFilePath(GetImagesPath(), resourceValue)))
            {
                return new Icon(GetImagesPath() + resourceValue);
            }
            
            return null;
        }

        public static Icon GetResourceIcon(string resourceName)
        {
            return GetResourceIcon(resourceName, "");
        }

        public static Image GetResourceImage(string resourceName, string languageID)
        {
            string resourceValue = GetResourceString(resourceName, languageID);
            //if (resourceValue == resourceName)
            //    return null;
            if (File.Exists(GetFullFilePath(GetImagesPath(), resourceValue)))
            {
                return Image.FromFile(GetImagesPath() + resourceValue);
            }
            return null;
        }

        public static Image GetResourceImage(string resourceName)
        {
            return GetResourceImage(resourceName, "");
        }

        public static string GetSystemVariableValue(string variableID)
        {
            switch (variableID.ToUpper().Trim())
            {
                case "[@]USER_ID":
                    return GetLogonUserID();
                case "[@]USER_NAME":
                    return GetLogonUserName();
                case "[@]SYSTEM_VERSION":
                    return GetSystemVersion();
                case "[@]FRAMEWORK_VERSION":
                    return GetFrameworkVersion();
                case "[@]LANGUAGE_ID":
                    return GetLanguageID();
                case "[@]DEFAULT_LANGUAGE_ID":
                    return GetDefaultLanguageID();
                case "[@]CLIENT_DATE_TIME":
                    return GetClientDateTime().ToString();
                case "[@]SESSION_ID":
                    return GetSessionID();
                case "[@]IP_ADDRESS":
                    return GetClientIPAddress();
                case "[@]CLIENT_HOST_NAME":
                    return GetClientHostName();
                case "[@]CLIENT_OS_USER_NAME":
                    return GetClientOSUserName();
                case "[@]CLIENT_OS_VERSION":
                    return GetClientOSVersion();
                case "[@]APP_PATH":
                    return GetAppPath();
                case "[@]MAC_ADDRESS":
                    return GetClientMacAddress();
                //case "[@]FUNCTION_ID":
                //    return "";
                default:
                    return "";
            }
        }

        public static string ResolveSystemVariableValue(string source, bool addQuotation)
        {
            int pIndex = -1;
            int sIndex = -1;
            string subStr = "";
            string replaceStr = "";
            bool isDateTime = false;
            bool isConstant = true;
            if (source.IndexOf("[&") > -1)
            {
                while (source.IndexOf("[&") > -1)
                {
                    pIndex = source.IndexOf("[&");
                    sIndex = source.IndexOf("&]");
                    subStr = source.Substring(pIndex + 2, sIndex - pIndex - 2).ToUpper().Trim();
                    if (subStr == "[@]CLIENT_DATE_TIME")
                    {
                        isDateTime = true;
                    }
                    replaceStr = GetSystemVariableValue(subStr);
                    if (replaceStr != "")
                    {
                        source = source.Replace("[&" + subStr + "&]", replaceStr);
                        isConstant = false;
                    }
                }
            }
            else
            {
                replaceStr = GetSystemVariableValue(source.ToUpper());
                if (replaceStr != "")
                {
                    if (source.ToUpper() == "[@]CLIENT_DATE_TIME")
                    {
                        isDateTime = true;
                    }
                    source = replaceStr;
                    isConstant = false;
                }
            }

            if (addQuotation)
            {
                if (!isDateTime)
                {
                    if (!isConstant)
                    {
                        if (source.IndexOf("'") != 0)
                        {
                            source = "'" + source + "'";
                        }
                    }
                }
            }
            return source;
        }

        public static string ResolveSystemVariableValue(string source)
        {
            return ResolveSystemVariableValue(source, true);
        }
        #endregion Resource

        //MessageBox
        #region MessageBox
        public static void MsgBox(string text)
        {
            MsgBox(text, GetResourceString("Information"));
        }

        public static void MsgBox(string text, string caption)
        {
            MessageBox.Show(text, caption, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public static void MsgErrBox(string text)
        {
            MsgErrBox(text, GetResourceString("Error"));
        }

        public static void MsgErrBox(string text, string caption)
        {
            MessageBox.Show(text, caption, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public static DialogResult MsgQBox(string text, string caption)
        {
            return MessageBox.Show(text, caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
        }

        public static DialogResult MsgQBox(string text)
        {
            return MessageBox.Show(text, GetResourceString("Question"), MessageBoxButtons.YesNo, MessageBoxIcon.Question);
        }

        public static string GetFullFilePath(string directoryPath, string fileName)
        {
            string filePath = "";
            System.IO.DirectoryInfo directoryInfo = new DirectoryInfo(directoryPath);
            filePath = directoryInfo.FullName + @"\" +fileName;
            if (File.Exists(filePath))
            {
                return filePath;
            }

            foreach (DirectoryInfo dc in directoryInfo.GetDirectories())
            {
                filePath = dc.FullName + @"\" + fileName;
                if (File.Exists(filePath))
                {
                    return filePath;
                }
                if (dc.GetDirectories().Length > 0)
                {
                    return GetFullFilePath(dc.FullName, fileName);
                }
            }
            return "";
        }
        #endregion MessageBox
        public static bool IsInt32(string data)
        {
            try
            {
                object obj = Convert.ToInt32(data);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static bool IsDateTime(string data)
        {
            try
            {
                object obj = Convert.ToDateTime(data);
                return true;
            }
            catch
            {
                return false;
            }
        }
        
        //Font and Color
        #region Font and Color
        public static void ResetSystemColorAndFont()
        {
            SystemFont = null;
            SystemForeColor = Color.Empty;
            SystemBackColor = Color.Empty;
            SystemLineColor = Color.Empty;
        }

        public static Color GetSystemForeColor()
        {
            try
            {
                if (SystemForeColor != Color.Empty)
                {
                    return SystemForeColor;
                }
                SystemForeColor = Color.Black;
                string fontColorR = configInfo.ReadConfigInfo("font", "fontcolorr");
                string fontColorG = configInfo.ReadConfigInfo("font", "fontcolorg");
                string fontColorB = configInfo.ReadConfigInfo("font", "fontcolorb");
                if (fontColorR != "")
                {
                    if (fontColorG != "")
                    {
                        if (fontColorB != "")
                        {
                            SystemForeColor = Color.FromArgb(Convert.ToInt32(fontColorR), Convert.ToInt32(fontColorG), Convert.ToInt32(fontColorB));
                        }
                    }
                }
                return SystemForeColor;
            }
            catch
            {

                return Color.Black; ;
            }

        }

        public static Font GetSystemFont()
        {
            try
            {
                if (SystemFont != null)
                {
                    return SystemFont;
                }
                string fontName = configInfo.ReadConfigInfo("font", "fontname");
                string fontSize = configInfo.ReadConfigInfo("font", "fontsize");
                string fontStyle = configInfo.ReadConfigInfo("font", "fontstyle");


                if (fontName == "")
                {
                    fontName = "PMingLiU";
                }
                if (fontSize == "")
                {
                    fontSize = "9";
                }
                if (fontStyle == "")
                {
                    fontStyle = "Regular";
                }
                float fontSizeF = 9;
                try
                {
                    fontSizeF = (float)Convert.ToDouble(fontSize);
                }
                catch
                {
                    fontSizeF = 9;
                }
                SystemFont = new Font(fontName, fontSizeF, (FontStyle)Enum.Parse(typeof(FontStyle), fontStyle));
                return SystemFont;
            }
            catch
            {
                return null;
            }
        }

        public static Color GetSystemBackColor()
        {
            try
            {
                if (SystemBackColor != Color.Empty)
                {
                    return SystemBackColor;
                }
                SystemBackColor = Color.White;
                string backColorR = configInfo.ReadConfigInfo("backcolor", "backcolorr");
                string backColorG = configInfo.ReadConfigInfo("backcolor", "backcolorg");
                string backColorB = configInfo.ReadConfigInfo("backcolor", "backcolorb");
                if (backColorR != "")
                {
                    if (backColorG != "")
                    {
                        if (backColorB != "")
                        {
                            SystemBackColor = Color.FromArgb(Convert.ToInt32(backColorR), Convert.ToInt32(backColorG), Convert.ToInt32(backColorB));
                        }
                    }
                }
                return SystemBackColor;
            }
            catch
            {
                return Color.White;
            }
        }

        public static Color GetSystemLineColor()
        {
            
            try
            {
                if (SystemLineColor != Color.Empty)
                {
                    return SystemLineColor;
                }
                SystemLineColor = Color.FromArgb(127, 157, 185);
                string lineColorR = configInfo.ReadConfigInfo("linecolor", "linecolorr");
                string lineColorG = configInfo.ReadConfigInfo("linecolor", "linecolorg");
                string lineColorB = configInfo.ReadConfigInfo("linecolor", "linecolorb");
                if (lineColorR != "")
                {
                    if (lineColorG != "")
                    {
                        if (lineColorB != "")
                        {
                            SystemLineColor = Color.FromArgb(Convert.ToInt32(lineColorR), Convert.ToInt32(lineColorG), Convert.ToInt32(lineColorB));
                        }
                    }
                }
                return SystemLineColor;
            }
            catch
            {
                return Color.FromArgb(127, 157, 185);
            }
        }
        #endregion Font and Color
    }
}
