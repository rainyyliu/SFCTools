namespace SFC_Tools.Classes
{
    using LumiSoft.Net.IMAP;
    using LumiSoft.Net.MIME;
    using Microsoft.Win32;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Data;
    using System.Diagnostics;
    using System.Drawing;
    using System.Drawing.Imaging;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Runtime.InteropServices;
    using System.Security.Cryptography;
    using System.Text;
    using System.Text.RegularExpressions;
    using System.Windows.Forms;
    using System.Xml;

    internal class PublicMethod
    {
        public static void AddAttachment(string name, IDictionary<string, byte[]> dicAttachment, MIME_Entity entity)
        {
            MIME_b_MessageRfc822 body = entity.Body as MIME_b_MessageRfc822;
            MIME_b_SinglepartBase base2 = entity.Body as MIME_b_SinglepartBase;
            if (base2 != null)
            {
                dicAttachment.Add(ProcessFileName(Guid.NewGuid() + "_" + name), ((MIME_b_SinglepartBase) entity.Body).Data);
            }
            else if (body != null)
            {
                MIME_EncodedWordEncoding q = MIME_EncodedWordEncoding.Q;
                MIME_Encoding_EncodedWord headerWordEncoder = new MIME_Encoding_EncodedWord(q, Encoding.UTF8);
                string key = ProcessFileName(Guid.NewGuid() + "_" + entity.ContentDisposition.Param_FileName);
                dicAttachment.Add(key, body.Message.ToByte(headerWordEncoder, Encoding.UTF8));
            }
        }

        //public static string AppendSpecifySuffixForLocalGrpName(string inputContent)
        //{
        //    if (string.IsNullOrEmpty(inputContent))
        //    {
        //        return inputContent;
        //    }
        //    string str = inputContent;
        //    string[] strArray = str.Replace(';', ',').Split(new char[] { ',' });
        //    str = string.Empty;
        //    GroupsListXml xml = new GroupsListXml();
        //    foreach (string str2 in strArray)
        //    {
        //        if ((!string.IsNullOrEmpty(str2) && !str2.Contains<char>('@')) && xml.IsGroupNameExistInXml(str2))
        //        {
        //            string str3 = str2.Replace(" ", "%20_%");
        //            str = str + str3 + "@SNLocalGroup.sn.com,";
        //        }
        //        else
        //        {
        //            str = str + str2 + ",";
        //        }
        //    }
        //    return str.TrimEnd(new char[] { ',' });
        //}

        public List<string> BindMimeEncoding()
        {
            List<string> list = new List<string>();
            try
            {
                XmlDocument document = new XmlDocument();
                string filename = AppDomain.CurrentDomain.BaseDirectory + @"XML\SystemSet\MailEncoding.xml";
                document.Load(filename);
                XmlNodeList list2 = document.SelectNodes("root/encoding");
                foreach (XmlElement element in list2)
                {
                    list.Add(element.Attributes["name"].Value);
                }
            }
            catch (Exception exception)
            {
               // //Log4Net.Log.Error(exception.Message + exception.StackTrace);
                NetWorkMessage();
            }
            return list;
        }

        public string ChangeArchivePath(string path)
        {
            string[] strArray = path.Split(new char[] { '.' });
            if ((strArray[0] == "Archive") && ((strArray.Length > 1) && (strArray[1] != "blackbox")))
            {
                path = path.Replace("Archive", "Archive.blackbox");
            }
            return path;
        }

        public static string ChangeHeader(string header)
        {
            if (string.IsNullOrEmpty(header))
            {
                header = " ";
                return header;
            }
            header = Convert.ToBase64String(Encoding.UTF8.GetBytes(header));
            return header;
        }

        public static string ChangeRuleToCurrentLanguage(string strRule)
        {
            string str = strRule;
            switch (strRule)
            {
                case "From":
                    return ConstData.MailRuleTypes.FromText;

                case "Sub":
                    return ConstData.MailRuleTypes.SubjectText;

                case "Size":
                    return ConstData.MailRuleTypes.SizeText;

                case "CC":
                    return ConstData.MailRuleTypes.CCText;

                case "Bcc":
                    return ConstData.MailRuleTypes.BCCText;

                case "Received":
                    return ConstData.MailRuleTypes.ReceivedText;

                case "ReceivedOrCc":
                    return ConstData.MailRuleTypes.ReceivedOrFromText;

                case "Contains":
                    return ConstData.MailRuleFilters.ContainsText;

                case "NoContains":
                    return ConstData.MailRuleFilters.NoContainsText;

                case "Is":
                    return ConstData.MailRuleFilters.IsText;

                case "NotIs":
                    return ConstData.MailRuleFilters.NotIsText;

                case "Greater Than":
                    return ConstData.MailRuleFilters.GreaterThanText;

                case "Less Than":
                    return ConstData.MailRuleFilters.LessThanText;

                case "And":
                    return ConstData.MailRuleJudge.AndText;

                case "Or":
                    return ConstData.MailRuleJudge.OrText;

                case "Copy":
                    return ConstData.MailRuleActions.CopyText;

                case "Delete":
                    return ConstData.MailRuleActions.DeleteText;

                case "Move":
                    return ConstData.MailRuleActions.MoveText;
            }
            return str;
        }

        //private static bool CheckDomainAndInternetAddress(string mailaddress)
        //{
        //    string str = "@" + mailaddress.Split(new char[] { '@' })[1];
        //    try
        //    {
        //        XmlHelper helper = new XmlHelper(AppDomain.CurrentDomain.BaseDirectory + @"XML\SystemSet\DomainAndInternetAddress.xml");
        //        string[] strArray = helper.GetAXmlElementValue("mailAdress", "", "").Split(new char[] { ',' });
        //        foreach (string str4 in strArray)
        //        {
        //            if (!(string.IsNullOrEmpty(str4) || !str4.Equals(str)))
        //            {
        //                return true;
        //            }
        //        }
        //    }
        //    catch (Exception exception)
        //    {
        //        //Log4Net.Log.Error("讀取MailAddress" + exception.GetType() + exception.Message);
        //    }
        //    return false;
        //}

        public static bool CheckFileMD5(string strMD5, string zipFile)
        {
            bool flag = false;
            if (string.Equals(strMD5, GetFilesMD5Hash(zipFile), StringComparison.OrdinalIgnoreCase))
            {
                flag = true;
            }
            return flag;
        }

        //public static bool CheckInternalMail(string mailAddr)
        //{
        //    if (!string.IsNullOrEmpty(mailAddr))
        //    {
        //        string[] strArray = mailAddr.Split(new char[] { ',' });
        //        for (int i = 0; i < strArray.Length; i++)
        //        {
        //            if (!string.IsNullOrEmpty(strArray[i]) && !((strArray[i].Contains("@foxconn.com") || strArray[i].Contains("@mail.foxconn.com")) || CheckDomainAndInternetAddress(strArray[i])))
        //            {
        //                return false;
        //            }
        //        }
        //    }
        //    return true;
        //}

        public static string CheckMailEncode(string inputEncode)
        {
            string str = string.Empty;
            try
            {
                if (string.IsNullOrEmpty(inputEncode))
                {
                    return str;
                }
                str = inputEncode.Trim().ToLower().Replace("-", "").Replace(" ", "").Replace("_", "");
                if ("utf8".Equals(str, StringComparison.OrdinalIgnoreCase))
                {
                    str = "utf-8";
                }
            }
            catch (Exception exception)
            {
                ////Log4Net.Log.Error(exception.Message + exception.StackTrace);
            }
            return str;
        }

        //public static bool CheckMailHaveEncode(ref string oldBody, string mailId)
        //{
        //    bool flag = false;
        //    try
        //    {
        //        if (string.IsNullOrEmpty(mailId))
        //        {
        //            return flag;
        //        }
        //        if (StaticVariable.SaveMailEncodeContent.Count <= 0)
        //        {
        //            return flag;
        //        }
        //        if (!StaticVariable.SaveMailEncodeContent.ContainsKey(mailId))
        //        {
        //            return flag;
        //        }
        //        if (!string.IsNullOrEmpty(StaticVariable.SaveMailEncodeContent[mailId]))
        //        {
        //            oldBody = StaticVariable.SaveMailEncodeContent[mailId];
        //        }
        //    }
        //    catch (Exception exception)
        //    {
        //        //Log4Net.Log.Error(exception.Message + exception.StackTrace);
        //    }
        //    return flag;
        //}

        //public static string ChoseFromCurAndNotesEmailMaybeLDAPSearch(string cur_Email, string notes_Email)
        //{
        //    if ((string.IsNullOrEmpty(cur_Email) || (cur_Email.Trim() == "@@foxconn.com")) || (cur_Email.Trim() == "%@foxconn.com"))
        //    {
        //        return NotesEmailAddressTransforWithLdapSearch(notes_Email);
        //    }
        //    return cur_Email;
        //}

        public static int Compare(string str1, string str2)
        {
            if (string.IsNullOrEmpty(str1) && string.IsNullOrEmpty(str2))
            {
                return 0;
            }
            if (!(!string.IsNullOrEmpty(str1) || string.IsNullOrEmpty(str2)))
            {
                return 1;
            }
            if (!(string.IsNullOrEmpty(str1) || !string.IsNullOrEmpty(str2)))
            {
                return -1;
            }
            return str1.CompareTo(str2);
        }

        public static bool ContainNotesFormatEmailAddress(string inputContent)
        {
            if (!string.IsNullOrEmpty(inputContent))
            {
                string str = inputContent;
                string[] strArray = str.Replace(';', ',').Split(new char[] { ',' });
                foreach (string str2 in strArray)
                {
                    if (!((string.IsNullOrEmpty(str2) || !str2.Contains("/")) || str2.Contains("@")))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public static string ConvertToBase64ForHeaderValue(string headerValue)
        {
            string str = string.Empty;
            if (!string.IsNullOrEmpty(headerValue))
            {
                headerValue = headerValue.Trim();
                try
                {
                    str = Convert.ToBase64String(Encoding.UTF8.GetBytes(headerValue));
                    str = "=?utf-8?B?" + str + "?=";
                }
                catch (Exception exception)
                {
                    ////Log4Net.Log.Error("轉換為Base64編碼時出現異常：" + exception.Message + exception.StackTrace);
                }
            }
            return str;
        }

        //public static void CreateANewTabPage(string tabText, string tabTag, Control newControl)
        //{
        //    string text = tabText;
        //    if (tabText.Length > 0x16)
        //    {
        //        text = tabText.Substring(0, 0x16);
        //    }
        //    TabPage curAddNewTbPage = new TabPage(text) {
        //        ToolTipText = tabText,
        //        Tag = tabTag
        //    };
        //    curAddNewTbPage.Controls.Add(newControl);
        //    newControl.Dock = DockStyle.Fill;
        //    ModifyTabpagelableToFitImgShow(curAddNewTbPage);
        //    Main.stac_TabControlInClient.TabPages.Add(curAddNewTbPage);
        //    Main.stac_TabControlInClient.SelectedTab = curAddNewTbPage;
        //    if (tabTag != "tagNewMeeting")
        //    {
        //        if (newControl is NewMail)
        //        {
        //            NewMail mail = (NewMail) newControl;
        //            mail.InitialCursorFocus(mail.NewMailType);
        //        }
        //    }
        //    else if (newControl is SuperNotes.View.UIControl.NewMeeting)
        //    {
        //        SuperNotes.View.UIControl.NewMeeting meeting = (SuperNotes.View.UIControl.NewMeeting) newControl;
        //        meeting.InitialCursorFocus(meeting.NewMailType);
        //    }
        //}


        public static string DecodeFromBase64ForHeaderValue(string headerValue)
        {
            string str = headerValue;
            if (!string.IsNullOrEmpty(headerValue))
            {
                try
                {
                    str = new MIME_Encoding_EncodedWord(MIME_EncodedWordEncoding.B, Encoding.UTF8).Decode(headerValue);
                }
                catch (Exception exception)
                {
                   // //Log4Net.Log.Error("從Base64解碼時出現異常：" + exception.Message);
                }
            }
            return str;
        }

        public static bool DeleteFileByPath(string path)
        {
            try
            {
                if (File.Exists(path))
                {
                    File.SetAttributes(path, FileAttributes.Normal);
                    File.Delete(path);
                    ////Log4Net.Log.Debug("刪除指定文件完成");
                    return true;
                }
            }
            catch (Exception exception)
            {
                ////Log4Net.Log.Error(exception.Message + exception.StackTrace);
            }
            return false;
        }

        public static void DeleteFolder(string dir)
        {
            if (!string.IsNullOrEmpty(dir))
            {
                Exception exception2;
                if (Directory.Exists(dir))
                {
                    foreach (string str2 in Directory.GetFileSystemEntries(dir))
                    {
                        if (File.Exists(str2))
                        {
                            try
                            {
                                File.SetAttributes(str2, FileAttributes.Normal);
                                File.Delete(str2);
                                ////Log4Net.Log.Debug("直接删除其中的文件");
                            }
                            catch (Exception)
                            {
                               // //Log4Net.Log.Error("删除其中的文件失敗:");
                            }
                        }
                        else
                        {
                            DeleteFolder(str2);
                        }
                    }
                    try
                    {
                        Directory.Delete(dir);
                    }
                    catch (Exception exception3)
                    {
                        exception2 = exception3;
                       // //Log4Net.Log.Error("删除文件夹失敗");
                    }
                }
                else
                {
                    try
                    {
                        Directory.Delete(dir);
                    }
                    catch (Exception exception4)
                    {
                        exception2 = exception4;
                        //Log4Net.Log.Error("删除已空文件夹失敗：");
                    }
                }
            }
        }

        public static void DeletMidFile()
        {
            try
            {
                PublicMethod method = new PublicMethod();
                //DeleteFolder(method.GetDataPath("mid"));
                DeleteFolder(method.GetAttFileTempPath());
                //Log4Net.Log.Debug("刪除零時文件完成。");
            }
            catch (Exception exception)
            {
                //Log4Net.Log.Error("刪除失敗：" + exception.Message + exception.StackTrace);
            }
        }

        //public static string ExchangeLocalGrpToMailAddress(string allInputContent)
        //{
        //    if (string.IsNullOrEmpty(allInputContent))
        //    {
        //        return string.Empty;
        //    }
        //    if (allInputContent.Trim() == "")
        //    {
        //        return string.Empty;
        //    }
        //    if (allInputContent.EndsWith(",", StringComparison.Ordinal))
        //    {
        //        allInputContent = allInputContent.Remove(allInputContent.LastIndexOf(',')).Trim();
        //    }
        //    string[] strArray = allInputContent.Split(new char[] { ',' });
        //    string str2 = string.Empty;
        //    string str3 = "@SNLocalGroup.sn.com";
        //    foreach (string str4 in strArray)
        //    {
        //        bool flag;
        //        string str5 = str4;
        //        if (!(string.IsNullOrEmpty(str5) || !str5.Contains(str3)))
        //        {
        //            str5 = str5.Replace("%20_%", " ").Replace(str3, "");
        //        }
        //        string allMembEmailsFromLocalGrp = GetAllMembEmailsFromLocalGrp(str5, out flag);
        //        if (flag)
        //        {
        //            if (!(string.IsNullOrEmpty(allMembEmailsFromLocalGrp) || !(allMembEmailsFromLocalGrp.Trim() != "")))
        //            {
        //                str2 = str2 + allMembEmailsFromLocalGrp + ",";
        //            }
        //        }
        //        else
        //        {
        //            str2 = str2 + str5 + ",";
        //        }
        //    }
        //    if (str2.EndsWith(",", StringComparison.Ordinal))
        //    {
        //        str2 = str2.Substring(0, str2.Length - 1);
        //    }
        //    return str2;
        //}

        public static string ExchangeRNToCommaInMailAddress(string inputEmails)
        {
            string str = inputEmails;
            if (!string.IsNullOrEmpty(inputEmails))
            {
                str = inputEmails.Replace("\r\n", ",");
            }
            return str;
        }

        public static string ExtractMailAddressFromOuter(string inputEmails)
        {
            string str = string.Empty;
            if (string.IsNullOrEmpty(inputEmails) || !(inputEmails.Trim() != ""))
            {
                return str;
            }
            if (inputEmails.Contains<char>('<'))
            {
                inputEmails = inputEmails.Replace(';', ',');
                if (inputEmails.EndsWith(",", StringComparison.Ordinal))
                {
                    inputEmails = inputEmails.Substring(0, inputEmails.Length - 1);
                }
                string[] strArray = inputEmails.Split(new char[] { ',' });
                string str2 = string.Empty;
                foreach (string str3 in strArray)
                {
                    if (((!string.IsNullOrEmpty(str3) && str3.Contains<char>('<')) && str3.Contains<char>('>')) && (str3.IndexOf('<') < str3.IndexOf('>')))
                    {
                        str2 = str3.Substring(str3.IndexOf('<') + 1, (str3.IndexOf('>') - str3.IndexOf('<')) - 1);
                    }
                    else
                    {
                        str2 = str3;
                    }
                    str = str + str2 + ",";
                }
                if (str.EndsWith(",", StringComparison.Ordinal))
                {
                    str = str.Substring(0, str.Length - 1);
                }
                return str;
            }
            return inputEmails;
        }

        public static string ExtractRightInputContent(string inputStr)
        {
            string str = "";
            if (!string.IsNullOrEmpty(inputStr))
            {
                inputStr = inputStr.Trim();
                inputStr = inputStr.Replace(';', ',');
                string[] strArray = inputStr.Split(new char[] { ',' });
                str = strArray[strArray.Length - 1].Trim();
            }
            return str;
        }

        public static string ExtractXmlFlagValue(string xmlContent, string xmlFlagName, out bool hasThisNode)
        {
            string strA = string.Empty;
            hasThisNode = false;
            string str2 = "<" + xmlFlagName + ">";
            string str3 = "</" + xmlFlagName + ">";
            if (((!string.IsNullOrEmpty(xmlContent) && !string.IsNullOrEmpty(xmlFlagName)) && xmlContent.Contains(str2)) && xmlContent.Contains(str3))
            {
                try
                {
                    hasThisNode = true;
                    int startIndex = xmlContent.IndexOf(str2) + str2.Length;
                    int index = xmlContent.IndexOf(str3);
                    strA = xmlContent.Substring(startIndex, index - startIndex);
                    strA = strA.Trim();
                    if (string.Compare(strA, "null", true) == 0)
                    {
                        strA = string.Empty;
                    }
                }
                catch (Exception exception)
                {
                    //Log4Net.Log.Error("提取Xml節點的內容出現異常：" + exception.Message);
                }
            }
            return strA;
        }

        public static void FilterAuditExchgeKeyWordInSubj(ref string originSubject)
        {
            string str = originSubject;
            if (!string.IsNullOrEmpty(str) && str.Contains("<Approve Required>"))
            {
                str = str.Replace("<Approve Required>", "");
                originSubject = str;
            }
        }

        public static string FilterAuditKeyWordInSubj(string originSubject)
        {
            string str = originSubject;
            if (!string.IsNullOrEmpty(str))
            {
                if (str.Contains("[Approve Required]"))
                {
                    str = str.Replace("[Approve Required]", "");
                }
                if (str.Contains("[BG審核]"))
                {
                    str = str.Replace("[BG審核]", "");
                }
            }
            return str;
        }

        public static string FilterExtraStrInHeaderVal(string headerVal)
        {
            string oldValue = "\r\n ";
            string str2 = "\r\n";
            string str3 = headerVal;
            if (!string.IsNullOrEmpty(headerVal))
            {
                str3 = headerVal.Replace(oldValue, "");
                if (!string.IsNullOrEmpty(str3))
                {
                    str3 = str3.Replace(str2, "");
                }
            }
            return str3;
        }

        public static void FilterMailAddressBank(ref string mailAddress)
        {
            if (!string.IsNullOrEmpty(mailAddress))
            {
                string[] strArray = mailAddress.Trim().Replace(';', ',').TrimEnd(new char[] { ',' }).Split(new char[] { ',' });
                string str = "";
                foreach (string str2 in strArray)
                {
                    if (!string.IsNullOrEmpty(str2) && (str2.Trim() != ""))
                    {
                        if (strArray.Length > 1)
                        {
                            str = str + str2.Trim() + ",";
                        }
                        else
                        {
                            str = str2.Trim();
                        }
                    }
                }
                mailAddress = str;
            }
        }

        public static string FilterSpecifiedAddressFromAddressList(string ToDeletedAddress, string AddressList)
        {
            string str = string.Empty;
            string[] strArray = AddressList.Split(new char[] { ',' });
            foreach (string str2 in strArray)
            {
                if (!str2.ToLower().Equals(ToDeletedAddress))
                {
                    str = str + str2 + ",";
                }
            }
            if (str.EndsWith(",", StringComparison.Ordinal))
            {
                str = str.Substring(0, str.Length - 1);
            }
            return str;
        }

        public static List<string> GetAllDefaultFoldersInServerOfCalendar()
        {
            return new List<string> { "Calendar", "Calendar.meetings", "Calendar.items", "Calendar.trash" };
        }

        //public static string GetAllMembEmailsFromLocalGrp(string grpName, out bool isGrpExist)
        //{
        //    List<string> list;
        //    isGrpExist = false;
        //    string allLdapPath = string.Empty;
        //   // isGrpExist = new GroupsListXml().GetAllMembEmailsFromLocalGrp(grpName, out list);
        //    if (!isGrpExist)
        //    {
        //        return allLdapPath;
        //    }
        //    //foreach (string str2 in list)
        //    //{
        //    //    allLdapPath = allLdapPath + str2 + ",";
        //    //}
        //    if (allLdapPath.EndsWith(",", StringComparison.Ordinal))
        //    {
        //        allLdapPath = allLdapPath.Substring(0, allLdapPath.Length - 1);
        //    }
        //    return LdapPathToEmailInReceiverControl(allLdapPath);
        //}

        private string GetAppDataPath()
        {
            string path = string.Empty;
            path = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + @"\SuperNotes";
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            return path;
        }

        //public string GetAppDataPath(string DirectoryName)
        //{
        //    string appDataPath = string.Empty;
        //    string userName = string.Empty;
        //    try
        //    {
        //        userName = StaticVariable.logOnInfo.UserName;
        //        appDataPath = this.GetAppDataPath(DirectoryName, userName);
        //    }
        //    catch (Exception exception)
        //    {
        //        //Log4Net.Log.Error(exception.Message + exception.StackTrace);
        //    }
        //    return appDataPath;
        //}

        public string GetAppDataPath(string DirectoryName, string userName)
        {
            string path = string.Empty;
            try
            {
                path = this.GetAppDataPath() + @"\" + userName + @"\" + DirectoryName;
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                path = path + @"\";
                //Log4Net.Log.Debug(@"取得指定帶有\目錄成功。");
            }
            catch (Exception exception)
            {
                //Log4Net.Log.Error(exception.Message + exception.StackTrace);
            }
            return path;
        }

        public string GetAppDirPath(string dirPath)
        {
            string path = dirPath.Trim();
            try
            {
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                //Log4Net.Log.Debug("創建目錄成功。");
            }
            catch (Exception exception)
            {
                //Log4Net.Log.Error(exception.Message + exception.StackTrace);
            }
            return path;
        }

        public static List<string> GetArchiveList(List<string> list)
        {
            List<string> list2 = new List<string>();
            try
            {
                foreach (string str in list)
                {
                    if (!string.IsNullOrEmpty(str) && str.Split(new char[] { '.' })[0].Equals("Archive"))
                    {
                        list2.Add(str);
                    }
                }
            }
            catch (Exception exception)
            {
                //Log4Net.Log.Error(exception.Message);
            }
            return list2;
        }

        public string[] GetArray(string tempStr)
        {
            tempStr = tempStr.Replace(';', ',');
            if (tempStr.EndsWith(",", StringComparison.Ordinal))
            {
                tempStr = tempStr.Remove(tempStr.LastIndexOf(',')).Trim();
            }
            return tempStr.Split(new char[] { ',' });
        }

        //public string GetAttFilePath(string fileName, ref string guId)
        //{
        //    string path = string.Empty;
        //    if (string.IsNullOrEmpty(guId))
        //    {
        //        guId = Guid.NewGuid().ToString();
        //    }
        //    path = this.GetAppDataPath("mid") + guId;
        //    if (!Directory.Exists(path))
        //    {
        //        Directory.CreateDirectory(path);
        //    }
        //    return (path + @"\" + fileName);
        //}

        public string GetAttFileTempPath()
        {
            string path = string.Empty;
            try
            {
                path = Environment.GetFolderPath(Environment.SpecialFolder.InternetCache) + @"\temp\~1";
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                //Log4Net.Log.Debug("創建臨時根目錄成功。");
            }
            catch (Exception exception)
            {
                //Log4Net.Log.Error(exception.Message + exception.StackTrace);
            }
            return path;
        }

        public string GetAttFileTempPath(ref string guId)
        {
            string path = string.Empty;
            if (string.IsNullOrEmpty(guId))
            {
                guId = Guid.NewGuid().ToString();
            }
            try
            {
                path = this.GetAttFileTempPath() + @"\" + guId;
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                //Log4Net.Log.Debug("創建臨時目錄成功。");
            }
            catch (Exception exception)
            {
                //Log4Net.Log.Error(exception.Message + exception.StackTrace);
            }
            return path;
        }

        public string GetAttFileTempPath(string fileName, ref string guId)
        {
            if (string.IsNullOrEmpty(guId))
            {
                guId = Guid.NewGuid().ToString();
            }
            return (this.GetAttFileTempPath(ref guId) + @"\" + fileName);
        }

        //public static string GetAttrValue(string parentNodePath, string matchName, string matchValue, string returnAttrValue)
        //{
        //    string xmlPath = string.Empty;
        //    new PublicMethod().GetCustomXmlPath(ConstData.RuleXmlFileName, out xmlPath, "rule");
        //    return XmlHelper.GetAElementAttributeValue(xmlPath, parentNodePath, matchName, matchValue, returnAttrValue);
        //}

        public static string GetAutoUpdNewVersion()
        {
            string str = string.Empty;
            string filename = string.Empty;
            XmlDocument document = null;
            try
            {
                filename = ConstData.ApplicationPath + @"\XML\SystemSet\AutoVersion.xml";
                document = new XmlDocument();
                document.Load(filename);
                str = document.SelectSingleNode("//version").InnerText.ToString().Trim();
            }
            catch (Exception exception)
            {
                //Log4Net.Log.Error("取得更新軟件的版本號失敗：" + exception.Message);
            }
            return str;
        }

        public static string GetAutoUpdVersion(string path, string name)
        {
            string fileVersion = string.Empty;
            try
            {
                fileVersion = FileVersionInfo.GetVersionInfo(path + @"\" + name).FileVersion;
            }
            catch (Exception exception)
            {
                //Log4Net.Log.Error("取得更新軟件的版本號失敗：" + exception.Message);
            }
            return fileVersion;
        }

        public static Encoding GetCanUseEncoding(string charSet)
        {
            Encoding encoding = Encoding.Default;
            try
            {
                if (string.IsNullOrEmpty(charSet))
                {
                    charSet = "BIG5";
                }
                charSet = charSet.Replace(" ", "");
                charSet = charSet.Replace("-", "");
                charSet = charSet.Replace("_", "");
                charSet = charSet.ToUpper();
                charSet = charSet.Replace("MS950", "BIG5");
                encoding = Encoding.GetEncoding(charSet);
            }
            catch
            {
            }
            return encoding;
        }

        //public bool GetCustomXmlPath(string xmlFileName, out string XmlPath, string subDirName)
        //{
        //    bool flag = true;
        //    XmlPath = string.Empty;
        //    string appDataPath = string.Empty;
        //    string sourceFileName = "";
        //    try
        //    {
        //        appDataPath = this.GetAppDataPath(subDirName);
        //        string fileName = appDataPath + xmlFileName;
        //        FileInfo info = new FileInfo(fileName);
        //        switch (subDirName)
        //        {
        //            case "rule":
        //                sourceFileName = AppDomain.CurrentDomain.SetupInformation.ApplicationBase + @"XML\MailRuleSet\" + xmlFileName;
        //                XmlPath = fileName;
        //                break;

        //            case "sign":
        //                sourceFileName = SignatureInfoXml.SignatureXmlFullPathInDebug;
        //                XmlPath = appDataPath;
        //                break;

        //            case "language":
        //                sourceFileName = LanguageSetXml.LanguageXmlPathInDebug;
        //                XmlPath = fileName;
        //                LanguageSetXml.LanguageXmlPathActual = fileName;
        //                break;

        //            case "address":
        //                sourceFileName = AppDomain.CurrentDomain.SetupInformation.ApplicationBase + @"XML\" + xmlFileName;
        //                XmlPath = fileName;
        //                break;

        //            case "SystemUI":
        //                sourceFileName = SystemUIInfoXml.SystemUIInforXmlPathInDebug;
        //                XmlPath = fileName;
        //                break;

        //            case "AttPath":
        //                sourceFileName = AttPathXml.AttPathXmlInDebug;
        //                XmlPath = fileName;
        //                break;

        //            case "Select":
        //                sourceFileName = SelectInfoXml.SelectInforXmlPathInDebug;
        //                XmlPath = fileName;
        //                break;

        //            case @"address\calendar":
        //                sourceFileName = MeetingInfoXml.MeetingInforXmlPathInDebug;
        //                XmlPath = fileName;
        //                break;

        //            case " Archive":
        //                sourceFileName = ArchiveInfoXml.ArchivePathXmlInDebug;
        //                XmlPath = fileName;
        //                break;

        //            case "SecuritySet":
        //                sourceFileName = SecurityInforXmlStruct.SecurityInforXmlInDebug;
        //                XmlPath = fileName;
        //                break;
        //        }
        //        if (info.Exists)
        //        {
        //            return flag;
        //        }
        //        File.Copy(sourceFileName, fileName);
        //        if (subDirName.Equals("language"))
        //        {
        //            SysSetXml.SetSelectCultureInfor(Program.GetCurrentOSLanguage());
        //        }
        //    }
        //    catch (Exception exception)
        //    {
        //        flag = false;
        //        //Log4Net.Log.Error(exception.Message + exception.StackTrace);
        //    }
        //    return flag;
        //}

        //public string GetDataPath(string DirectoryName)
        //{
        //    string path = string.Empty;
        //    string userName = string.Empty;
        //    try
        //    {
        //        userName = StaticVariable.logOnInfo.UserName;
        //        path = this.GetAppDataPath() + @"\" + userName + @"\" + DirectoryName;
        //        if (!Directory.Exists(path))
        //        {
        //            Directory.CreateDirectory(path);
        //        }
        //        //Log4Net.Log.Debug("創建目錄成功。");
        //    }
        //    catch (Exception exception)
        //    {
        //        //Log4Net.Log.Error("取得附件失敗：" + exception.Message + exception.StackTrace);
        //    }
        //    return path;
        //}

        //public static string GetDayOfWeekWithLanguage(DateTime curDateTime)
        //{
        //    string str = string.Empty;
        //    switch (curDateTime.DayOfWeek)
        //    {
        //        case DayOfWeek.Sunday:
        //            return SuperNotes.Resources.Sunday_MultyLanguage;

        //        case DayOfWeek.Monday:
        //            return SuperNotes.Resources.Monday_MultyLanguage;

        //        case DayOfWeek.Tuesday:
        //            return SuperNotes.Resources.Tuesday_MultyLanguage;

        //        case DayOfWeek.Wednesday:
        //            return SuperNotes.Resources.Wednesday_MultyLanguage;

        //        case DayOfWeek.Thursday:
        //            return SuperNotes.Resources.Thursday_MultyLanguage;

        //        case DayOfWeek.Friday:
        //            return SuperNotes.Resources.Friday_MultyLanguage;

        //        case DayOfWeek.Saturday:
        //            return SuperNotes.Resources.Saturday_MultyLanguage;
        //    }
        //    return str;
        //}

        public static string GetExtName(string fileName)
        {
            string extension = string.Empty;
            try
            {
                try
                {
                    extension = Path.GetExtension(fileName);
                }
                catch (Exception)
                {
                }
                if (extension == "")
                {
                    int startIndex = fileName.LastIndexOf(".");
                    if (startIndex > -1)
                    {
                        extension = fileName.Substring(startIndex);
                    }
                }
                //Log4Net.Log.Error("取得文件扩展名成功。");
            }
            catch (Exception exception2)
            {
                //Log4Net.Log.Error("取得文件扩展名失敗:" + exception2.Message + exception2.StackTrace);
            }
            return extension;
        }

        private static string GetExtNames(string fileName)
        {
            string extension = string.Empty;
            try
            {
                try
                {
                    extension = Path.GetExtension(fileName);
                }
                catch (Exception)
                {
                }
                if (!(extension == ""))
                {
                    return extension;
                }
                int startIndex = fileName.LastIndexOf(".");
                if (startIndex > -1)
                {
                    extension = fileName.Substring(startIndex);
                }
            }
            catch (Exception)
            {
            }
            return extension;
        }

        public static string GetFileName(string path)
        {
            string fileName = string.Empty;
            try
            {
                if (!string.IsNullOrEmpty(path))
                {
                    fileName = Path.GetFileName(path);
                }
            }
            catch (Exception exception)
            {
                //Log4Net.Log.Error(exception.Message + exception.StackTrace);
            }
            return fileName;
        }

        public static string GetFileNameByPath(string path)
        {
            string str = string.Empty;
            try
            {
                string fileName = GetFileName(path);
                int length = fileName.Length;
                int num2 = path.Length;
                string str3 = Guid.NewGuid().ToString();
                str = path.Substring(0, (num2 - length) - 1) + str3 + fileName;
            }
            catch (Exception exception)
            {
                //Log4Net.Log.Error("文件名稱修改失敗：" + exception.Message + exception.StackTrace);
            }
            return str;
        }

        public static string GetFileNameKey(string path)
        {
            string str = string.Empty;
            string fileName = string.Empty;
            string extName = string.Empty;
            int length = 0;
            int num2 = 0;
            if (!string.IsNullOrEmpty(path))
            {
                try
                {
                    fileName = GetFileName(path);
                    extName = GetExtName(path);
                    length = fileName.Length;
                    num2 = extName.Length;
                    str = fileName.Substring(0, length - num2);
                }
                catch (Exception exception)
                {
                    //Log4Net.Log.Error("根據文件全路徑取得文件名稱，并且去掉文件擴展名失敗：" + exception.StackTrace + exception.Message);
                }
            }
            return str;
        }

        public static string GetFilesMD5Hash(string file)
        {
            StringBuilder builder = null;
            FileStream inputStream = null;
            try
            {
                using (MD5CryptoServiceProvider provider = new MD5CryptoServiceProvider())
                {
                    inputStream = new FileStream(file, FileMode.Open, FileAccess.Read, FileShare.Read, 0x2000);
                    provider.ComputeHash(inputStream);
                    byte[] hash = provider.Hash;
                    builder = new StringBuilder();
                    foreach (byte num in hash)
                    {
                        builder.Append(string.Format(CultureInfo.CurrentCulture, "{0:X2}", new object[] { num }));
                    }
                }
            }
            catch (Exception exception)
            {
                //Log4Net.Log.Error("取得壓縮包的md5失敗：" + exception.Message);
            }
            finally
            {
                if (inputStream != null)
                {
                    inputStream.Dispose();
                }
            }
            return builder.ToString();
        }

        public static string GetFolderPathByDirId(string dirID)
        {
            //DataTable stacDTTree = Main.StacDTTree;
            //for (int i = 0; i < stacDTTree.Rows.Count; i++)
            //{
            //    if (stacDTTree.Rows[i]["name"].ToString().Trim() == dirID)
            //    {
            //        return stacDTTree.Rows[i]["path"].ToString().Trim();
            //    }
            //}
            return "";
        }

        public static int GetHeightByExtname(string path)
        {
            int num = 0;
            string extName = GetExtName(path);
            try
            {
                switch (extName)
                {
                    case ".cs":
                        return 0;

                    case ".css":
                        return 0;

                    case ".htm":
                        return 0;

                    case ".html":
                        return 0;

                    case ".mht":
                        return 0;

                    case ".js":
                        return 0;

                    case ".pdf":
                        return 0;

                    case ".ppt":
                        return 0;

                    case ".pptx":
                        return 0;

                    case ".txt":
                        return 0;

                    case ".xls":
                        return 0;

                    case ".xlsx":
                        return 0;

                    case ".csv":
                        return 0;

                    case ".vsd":
                        return 0;

                    case ".msg":
                        return 0;

                    case ".ini":
                        return 0;

                    case ".doc":
                        return 0;

                    case ".docx":
                        return 0;

                    case ".jpeg":
                        return 0;

                    case ".jpg":
                        return 0;

                    case ".gif":
                        return 0;

                    case ".tif":
                        return 0;

                    case ".png":
                        return 0;

                    case ".bmp":
                        return 0;

                    case ".mpp":
                        return 0;

                    case ".pps":
                        return 0;
                }
                return num;
            }
            catch (Exception exception)
            {
                //Log4Net.Log.Error(exception.Message + exception.StackTrace);
            }
            return num;
        }

        public static string GetInternetAdress(string ToAndCcAndBcc)
        {
            string str = string.Empty;
            StringBuilder builder = new StringBuilder();
            int num = 0;
            if (string.IsNullOrEmpty(ToAndCcAndBcc))
            {
                return str;
            }
            foreach (string str2 in ToAndCcAndBcc.Split(new char[] { ',' }))
            {
                if (!string.IsNullOrEmpty(str2) && (!Regex.IsMatch(str2, "@mail.foxconn.com", RegexOptions.IgnoreCase) && !Regex.IsMatch(str2, "@foxconn.com", RegexOptions.IgnoreCase)))
                {
                    if (num == 0)
                    {
                        builder.Append(str2);
                    }
                    else
                    {
                        builder.Append(",");
                        builder.Append(str2);
                    }
                    num++;
                }
            }
            return builder.ToString();
        }

        //public static DockStyle GetLblNameDockStyle(AlphaBlendTextBox lblName)
        //{
        //    try
        //    {
        //        bool flag = new Regex(@"[\u4e00-\u9fa5]").IsMatch(lblName.Text.Trim());
        //        if ((flag && (lblName.Text.Length > 0x11)) || (!flag && (lblName.Text.Length > 30)))
        //        {
        //            return DockStyle.Fill;
        //        }
        //    }
        //    catch (Exception exception)
        //    {
        //        //Log4Net.Log.Error("獲取用戶姓名欄位AlphaBlendTextBox的DockStyle時出現異常：" + exception.Message + exception.StackTrace);
        //    }
        //    return DockStyle.Bottom;
        //}

        public bool GetLoginInforXmlPath(string orgFilePath, string loginInforFileName, out string finalFilePath)
        {
            bool flag = true;
            finalFilePath = string.Empty;
            try
            {
                string fileName = this.GetAppDataPath() + @"\" + loginInforFileName;
                FileInfo info = new FileInfo(fileName);
                if (!info.Exists)
                {
                    File.Copy(orgFilePath, fileName);
                }
                //this.PersonalLoginInfo(fileName, orgFilePath);
                finalFilePath = fileName;
            }
            catch (Exception exception)
            {
                flag = false;
                //Log4Net.Log.Error(exception.Message + exception.StackTrace);
            }
            return flag;
        }

        public static bool GetOldArchive()
        {
            //DataTable stacDTTree = Main.StacDTTree;
            //for (int i = 0; i < stacDTTree.Rows.Count; i++)
            //{
            //    if (stacDTTree.Rows[i]["text"].ToString().Trim() == "oldArchive")
            //    {
            //        return true;
            //    }
            //}
            return false;
        }

        public static List<string> GetOldArchiveList(List<string> list)
        {
            string[] strArray = null;
            List<string> list2 = new List<string>();
            try
            {
                foreach (string str in list)
                {
                    if (!string.IsNullOrEmpty(str))
                    {
                        strArray = str.Split(new char[] { '.' });
                        if ((strArray.Length > 2) && (strArray[0] + "." + strArray[1]).Equals("oldArchive.blackbox"))
                        {
                            list2.Add(str);
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                //Log4Net.Log.Error(exception.Message);
            }
            return list2;
        }

        public static string GetOverlengthFileName(string FileName, int Len)
        {
            string str = string.Empty;
            str = FileName;
            if (FileName.Length > Len)
            {
                str = FileName.Substring(0, Len - 10) + "~" + GetExtName(FileName);
            }
            return str;
        }

        public static string GetRecivedFileName(string name)
        {
            string str = name;
            try
            {
                str = name.Replace("\r\n", "");
                str = str.Replace(@"\", "_");
                str = str.Replace("/", "_");
                str = str.Replace(":", "_");
                str = str.Replace("*", "_");
                str = str.Replace("?", "_");
                str = str.Replace("<", "_");
                str = str.Replace(">", "_");
                str = str.Replace("!", "_");
                str = str.Replace("\"", "_");
                str = str.Replace("'", "_");
                str = str.Replace("\t", "_");
                str = str.Replace("\n", "_");
                str = str.Replace("\r", "_");
                str = str.Replace("|", "_");
            }
            catch (Exception exception)
            {
                //Log4Net.Log.Error(exception.Message);
            }
            return str;
        }

        public static string GetRegistry()
        {
            string str = string.Empty;
            try
            {
                str = Registry.ClassesRoot.OpenSubKey(@"MAILTO\SHELL\OPEN\COMMAND").GetValue("").ToString();
            }
            catch (Exception exception)
            {
                //Log4Net.Log.Error("取得系統为默认邮箱失败：" + exception.Message);
            }
            return str;
        }

        protected static string GetRightFilename(string FileName, int Len)
        {
            string str = string.Empty;
            str = FileName;
            if (FileName.Length > Len)
            {
                str = FileName.Substring(0, Len - 10) + "~" + GetExtNames(FileName);
            }
            return str;
        }

        public static DataTable GetRuleList()
        {
            DataTable table = new DataTable {
                Locale = CultureInfo.InvariantCulture
            };
            MemoryStream stream = new MemoryStream();
            MemoryStream stream2 = new MemoryStream();
            MemoryStream stream3 = new MemoryStream();
            //SuperNotes.Properties.Resources.blank.Save(stream, ImageFormat.Bmp);
            stream.Position = 0L;
            //SuperNotes.Properties.Resources.openFlag.Save(stream2, ImageFormat.Bmp);
            stream2.Position = 0L;
            //SuperNotes.Properties.Resources.selectFlag.Save(stream3, ImageFormat.Bmp);
            stream3.Position = 0L;
            table.Columns.Add("SelectFlag", typeof(byte[]));
            table.Columns.Add("OpenFlag", typeof(byte[]));
            table.Columns.Add("IsSelected", typeof(string));
            table.Columns.Add("RuleID", typeof(string));
            table.Columns.Add("RuleContent", typeof(string));
            table.Columns.Add("RuleState", typeof(string));
            DataRow row = null;
            XmlDataDocument document = new XmlDataDocument();
            string xmlPath = string.Empty;
           // new PublicMethod().GetCustomXmlPath(ConstData.RuleXmlFileName, out xmlPath, "rule");
            document.Load(xmlPath);
            XmlNodeList list = document.SelectSingleNode("Root").SelectNodes("Rule");
            foreach (XmlElement element in list)
            {
                string str26;
                string str2 = element.Attributes["Id"].Value.ToString();
                string str3 = element.Attributes["Open"].Value.ToString();
                string str4 = "";
                //string str5 = SuperNotes.Resources.MailRule_When + " ";
                //string str6 = SuperNotes.Resources.MailRule_Exception + " ";
                //string str7 = SuperNotes.Resources.MailRule_Time;
                string str8 = "";
                string str9 = "";
                string str10 = "";
                XmlNodeList list2 = element.SelectNodes(element.Name.ToString());
                XmlNodeList list3 = element.SelectNodes("Condition");
                foreach (XmlElement element2 in list3)
                {
                    string str11 = element2.Attributes["Id"].Value.ToString();
                    string strRule = element2.Attributes["Judge"].Value.ToString();
                    string str13 = element2.Attributes["Type"].Value.ToString();
                    string str14 = element2.Attributes["Filter"].Value.ToString();
                    string str15 = element2.Attributes["Value"].Value.ToString();
                    str26 = str8;
                    str8 = str26 + " " + ChangeRuleToCurrentLanguage(strRule) + " " + ChangeRuleToCurrentLanguage(str13) + " " + ChangeRuleToCurrentLanguage(str14) + " " + str15 + " ";
                }
                XmlNodeList list4 = element.SelectNodes("Exception");
                foreach (XmlElement element2 in list4)
                {
                    string str16 = element2.Attributes["Id"].Value.ToString();
                    string str17 = element2.Attributes["Judge"].Value.ToString();
                    string str18 = element2.Attributes["Type"].Value.ToString();
                    string str19 = element2.Attributes["Filter"].Value.ToString();
                    string str20 = element2.Attributes["Value"].Value.ToString();
                    str26 = str10;
                    str10 = str26 + " " + ChangeRuleToCurrentLanguage(str17) + " " + ChangeRuleToCurrentLanguage(str18) + " " + ChangeRuleToCurrentLanguage(str19) + " " + str20;
                }
                XmlNodeList list5 = element.SelectNodes("Action");
                foreach (XmlElement element2 in list5)
                {
                    string str21 = element2.Attributes["Id"].Value.ToString();
                    string str22 = element2.Attributes["Judge"].Value.ToString();
                    string str23 = element2.Attributes["Type"].Value.ToString();
                    string strDir = element2.Attributes["FoderName"].Value.ToString();
                    string str25 = element2.Attributes["FolderNum"].Value.ToString();
                    str26 = str9;
                    //str9 = str26 + " " + ChangeRuleToCurrentLanguage(str22) + " " + ChangeRuleToCurrentLanguage(str23) + " " + BLLMain.ChangeFolderNameToCurrentLanguage(strDir) + " ";
                }
                if (str10 == string.Empty)
                {
                    //str4 = str5 + str8 + str7 + str9;
                }
                else
                {
                   // str4 = str5 + str8 + str7 + str9 + str6 + str10;
                }
                row = table.NewRow();
                row["SelectFlag"] = stream.ToArray();
                row["OpenFlag"] = stream2.ToArray();
                row["IsSelected"] = "0";
                row["RuleID"] = str2;
                row["RuleContent"] = str4;
                row["RuleState"] = str3;
                table.Rows.Add(row);
            }
            return table;
        }

        public static List<string> GetRuleList(string rootNodeName, string ruleNodeName, string idAttrName, string idValue, string searchNodeName)
        {
            List<string> list = new List<string>();
            XmlDataDocument document = new XmlDataDocument();
            string xmlPath = string.Empty;
           // new PublicMethod().GetCustomXmlPath(ConstData.RuleXmlFileName, out xmlPath, "rule");
            document.Load(xmlPath);
            XmlNodeList list2 = document.SelectSingleNode(rootNodeName).SelectNodes(ruleNodeName);
            foreach (XmlElement element in list2)
            {
                if (element.Attributes[idAttrName].Value == idValue)
                {
                    XmlNodeList list3 = element.SelectNodes(searchNodeName);
                    for (int i = 0; i < list3.Count; i++)
                    {
                        StringBuilder builder = new StringBuilder();
                        string str2 = ChangeRuleToCurrentLanguage(list3[i].Attributes["Judge"].Value);
                        string str3 = ChangeRuleToCurrentLanguage(list3[i].Attributes["Type"].Value);
                        string str4 = string.Empty;
                        string str5 = string.Empty;
                        if (list3[i].Name != "Action")
                        {
                            str4 = ChangeRuleToCurrentLanguage(list3[i].Attributes["Filter"].Value);
                            str5 = list3[i].Attributes["Value"].Value;
                            builder.Append(str2).Append(" ").Append(str3).Append(" ").Append(str4).Append(" ").Append(str5);
                        }
                        else
                        {
                            str5 = list3[i].Attributes["FoderName"].Value;
                           // builder.Append(str2).Append(" ").Append(str3).Append(" ").Append(BLLMain.ChangeFolderNameToCurrentLanguage(str5));
                        }
                        list.Add(builder.ToString());
                    }
                    return list;
                }
            }
            return list;
        }

        public static List<string> GetRuleList(string rootNodeName, string ruleNodeName, string idAttrName, string idValue, string searchNodeName, ref List<ConstData.Actions> tempList)
        {
            List<string> list = new List<string>();
            XmlDataDocument document = new XmlDataDocument();
            string xmlPath = string.Empty;
            //new PublicMethod().GetCustomXmlPath(ConstData.RuleXmlFileName, out xmlPath, "rule");
            document.Load(xmlPath);
            XmlNodeList list2 = document.SelectSingleNode(rootNodeName).SelectNodes(ruleNodeName);
            foreach (XmlElement element in list2)
            {
                if (element.Attributes[idAttrName].Value == idValue)
                {
                    XmlNodeList list3 = element.SelectNodes(searchNodeName);
                    for (int i = 0; i < list3.Count; i++)
                    {
                        StringBuilder builder = new StringBuilder();
                        string str2 = ChangeRuleToCurrentLanguage(list3[i].Attributes["Judge"].Value);
                        string str3 = ChangeRuleToCurrentLanguage(list3[i].Attributes["Type"].Value);
                        string strDir = list3[i].Attributes["FoderName"].Value;
                        //builder.Append(str2).Append(" ").Append(str3).Append(" ").Append(BLLMain.ChangeFolderNameToCurrentLanguage(strDir));
                        list.Add(builder.ToString());
                        ConstData.Actions item = new ConstData.Actions {
                            ActionJudge = list3[i].Attributes["Judge"].Value,
                            ActionType = list3[i].Attributes["Type"].Value,
                            ActionValue = strDir
                        };
                        tempList.Add(item);
                    }
                    return list;
                }
            }
            return list;
        }

        public static List<string> GetRuleList(string rootNodeName, string ruleNodeName, string idAttrName, string idValue, string searchNodeName, ref List<ConstData.Conditions> tempList)
        {
            List<string> list = new List<string>();
            XmlDataDocument document = new XmlDataDocument();
            string xmlPath = string.Empty;
            //new PublicMethod().GetCustomXmlPath(ConstData.RuleXmlFileName, out xmlPath, "rule");
            document.Load(xmlPath);
            XmlNodeList list2 = document.SelectSingleNode(rootNodeName).SelectNodes(ruleNodeName);
            foreach (XmlElement element in list2)
            {
                if (element.Attributes[idAttrName].Value == idValue)
                {
                    XmlNodeList list3 = element.SelectNodes(searchNodeName);
                    for (int i = 0; i < list3.Count; i++)
                    {
                        StringBuilder builder = new StringBuilder();
                        string str2 = ChangeRuleToCurrentLanguage(list3[i].Attributes["Judge"].Value);
                        string str3 = ChangeRuleToCurrentLanguage(list3[i].Attributes["Type"].Value);
                        string str4 = string.Empty;
                        string str5 = string.Empty;
                        str4 = ChangeRuleToCurrentLanguage(list3[i].Attributes["Filter"].Value);
                        str5 = list3[i].Attributes["Value"].Value;
                        builder.Append(str2).Append(" ").Append(str3).Append(" ").Append(str4).Append(" ").Append(str5);
                        list.Add(builder.ToString());
                        ConstData.Conditions item = new ConstData.Conditions {
                            Judge = list3[i].Attributes["Judge"].Value,
                            Type = list3[i].Attributes["Type"].Value,
                            Filter = list3[i].Attributes["Filter"].Value,
                            Value = str5
                        };
                        tempList.Add(item);
                    }
                    return list;
                }
            }
            return list;
        }

        private string GetSystemPath(string systemFlag)
        {
            string str = string.Empty;
            try
            {
                IDictionary environmentVariables = Environment.GetEnvironmentVariables();
                foreach (DictionaryEntry entry in environmentVariables)
                {
                    if (systemFlag.Equals(entry.Key.ToString()))
                    {
                        str = entry.Value.ToString();
                        break;
                    }
                }
                //Log4Net.Log.Debug("取得Windows系統下的指定目錄成功。");
            }
            catch (Exception exception)
            {
                //Log4Net.Log.Error("取得Windows系統下的指定目錄失敗：" + exception.Message + exception.StackTrace);
            }
            return str;
        }

        public static string GetSystemVersion()
        {
            string str = string.Empty;
            try
            {
                str = Environment.OSVersion.Version.Major.ToString() + Environment.OSVersion.Version.Minor.ToString();
            }
            catch (Exception exception)
            {
                //Log4Net.Log.Error("系統版本號錯誤" + exception.Message);
            }
            return str;
        }

        //public static ToolStripItemDisplayStyle GetToolbarDisplayStyle(ref TableLayoutPanel tableLayoutPanel, ref bool imageAndText)
        //{
        //    ToolStripItemDisplayStyle none = ToolStripItemDisplayStyle.None;
        //    string toolBarItemDisplayWay = new SysUISetXml().GetToolBarItemDisplayWay();
        //    if ((toolBarItemDisplayWay != null) && !(toolBarItemDisplayWay == "Image"))
        //    {
        //        if (toolBarItemDisplayWay == "Literal")
        //        {
        //            return ToolStripItemDisplayStyle.Text;
        //        }
        //        if (toolBarItemDisplayWay == "ImageWithLiteral")
        //        {
        //            none = ToolStripItemDisplayStyle.ImageAndText;
        //            imageAndText = true;
        //            return none;
        //        }
        //    }
        //    return ToolStripItemDisplayStyle.Image;
        //}

        //public static string GetTreeNodeTextByDirId(string dirID)
        //{
        //    DataTable stacDTTree = Main.StacDTTree;
        //    for (int i = 0; i < stacDTTree.Rows.Count; i++)
        //    {
        //        if (stacDTTree.Rows[i]["name"].ToString().Trim() == dirID)
        //        {
        //            return stacDTTree.Rows[i]["text"].ToString().Trim();
        //        }
        //    }
        //    return "";
        //}

        public static string GetUserFactoryServerID()
        {
            string str = string.Empty;
            try
            {
                string filename = AppDomain.CurrentDomain.BaseDirectory + @"XML\SystemSet\FactoryAddress.xml";
                XmlDocument document = new XmlDocument();
                document.Load(filename);
                XmlNodeList list = document.SelectNodes("root/Factory");
                foreach (XmlElement element in list)
                {
                    if ("1".Equals(element.Attributes["Selected"].Value) && CultureInfo.CurrentUICulture.ToString().Equals(element.Attributes["CultureInfor"].Value))
                    {
                        ////StaticVariable.UserFactoryName = element.Attributes["FactoryName"].Value;
                        return element.Attributes["ServerID"].Value;
                    }
                }
                return str;
            }
            catch (Exception exception)
            {
                //Log4Net.Log.Error("獲取登錄用戶所在廠區名稱：" + exception.Message);
            }
            return str;
        }

        public static string GetUserFactoryWebServiceAddress()
        {
            string str = string.Empty;
            string userFactoryServerID = GetUserFactoryServerID();
            try
            {
                string filename = AppDomain.CurrentDomain.BaseDirectory + @"XML\SystemSet\ServerAddress.xml";
                XmlDocument document = new XmlDocument();
                document.Load(filename);
                XmlNodeList list = document.SelectNodes("Server/ServerPath");
                foreach (XmlElement element in list)
                {
                    if (userFactoryServerID.Equals(element.Attributes["ServerID"].Value))
                    {
                        return (element.Attributes["WebType"].Value + "://" + element.Attributes["WebService"].Value);
                    }
                }
                return str;
            }
            catch (Exception exception)
            {
                //Log4Net.Log.Error("獲取登錄用戶所在廠區的WebService地址時出錯：" + exception.Message);
            }
            return str;
        }

        //public static bool HasInternetAdress(string ToAndCcAndBcc)
        //{
        //    if (!string.IsNullOrEmpty(ToAndCcAndBcc))
        //    {
        //        foreach (string str in ToAndCcAndBcc.Split(new char[] { ',' }))
        //        {
        //            if (!string.IsNullOrEmpty(str) && !((Regex.IsMatch(str, "@mail.foxconn.com", RegexOptions.IgnoreCase) || Regex.IsMatch(str, "@foxconn.com", RegexOptions.IgnoreCase)) || CheckDomainAndInternetAddress(str)))
        //            {
        //                return true;
        //            }
        //        }
        //    }
        //    return false;
        //}

        //public bool HasInternetAdress(string ToAndCcAndBcc, ref bool isinternet)
        //{
        //    bool flag = false;
        //    if (!string.IsNullOrEmpty(ToAndCcAndBcc))
        //    {
        //        foreach (string str in ToAndCcAndBcc.Split(new char[] { ',' }))
        //        {
        //            if (!string.IsNullOrEmpty(str))
        //            {
        //                if ((!Regex.IsMatch(str, "@mail.foxconn.com", RegexOptions.IgnoreCase) && !Regex.IsMatch(str, "@foxconn.com", RegexOptions.IgnoreCase)) && !CheckDomainAndInternetAddress(str))
        //                {
        //                    flag = true;
        //                    if (isinternet)
        //                    {
        //                        return flag;
        //                    }
        //                }
        //                isinternet = true;
        //            }
        //        }
        //    }
        //    return flag;
        //}

        //public bool HasKeywords(MailInfo mailInfo, string forbitKeywords, out string containedKeyWords)
        //{
        //    containedKeyWords = string.Empty;
        //    bool flag = false;
        //    string[] strArray = null;
        //    try
        //    {
        //        if (!string.IsNullOrEmpty(forbitKeywords))
        //        {
        //            strArray = forbitKeywords.Split(new char[] { ',' });
        //        }
        //        if (strArray == null)
        //        {
        //            return flag;
        //        }
        //        foreach (string str in strArray)
        //        {
        //            if (!(string.IsNullOrEmpty(str) || (!mailInfo.MailSubject.ToUpperInvariant().Contains(str.ToUpperInvariant()) && !mailInfo.MailBody.ToUpperInvariant().Contains(str.ToUpperInvariant()))))
        //            {
        //                if (string.IsNullOrEmpty(containedKeyWords))
        //                {
        //                    containedKeyWords = containedKeyWords + str + ",";
        //                }
        //                else if (!containedKeyWords.Contains(str))
        //                {
        //                    containedKeyWords = containedKeyWords + str + ",";
        //                }
        //            }
        //        }
        //        if (!string.IsNullOrEmpty(containedKeyWords))
        //        {
        //            flag = true;
        //            containedKeyWords = containedKeyWords.TrimEnd(new char[] { ',' });
        //        }
        //    }
        //    catch (Exception exception)
        //    {
        //        //Log4Net.Log.Error("判斷郵件主旨及內容是否包含關鍵字失敗：" + exception.Message + exception.StackTrace);
        //    }
        //    return flag;
        //}

        public static bool IsNoIntPrOrNull(IntPtr ip)
        {
            if (ip == IntPtr.Zero)
            {
                return false;
            }
            return true;
        }

        //protected bool IsRecipientLimit(int num)
        //{
        //    bool flag = true;
        //    string recipientLimit = StaticVariable.RecipientLimit;
        //    try
        //    {
        //        if ("-9".Equals(recipientLimit))
        //        {
        //            flag = false;
        //            MessageBox.Show(SuperNotes.Resources.printMail_msgReceiverOverNum9, SuperNotes.Resources.messageTitle, MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
        //            return flag;
        //        }
        //        int num2 = int.Parse(recipientLimit, CultureInfo.InvariantCulture);
        //        if (num <= num2)
        //        {
        //            return flag;
        //        }
        //        flag = false;
        //        if (num2 == 0)
        //        {
        //            MessageBox.Show(SuperNotes.Resources.printMail_msgReceiverOverNum3, SuperNotes.Resources.messageTitle, MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
        //            return flag;
        //        }
        //        MessageBox.Show(SuperNotes.Resources.printMail_msgReceiverOverNum + num2 + SuperNotes.Resources.printMail_msgReceiverOverNum2, SuperNotes.Resources.messageTitle, MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
        //    }
        //    catch (Exception exception)
        //    {
        //        flag = false;
        //        MessageBox.Show(SuperNotes.Resources.printMail_msgReceiverOverNumProblem, SuperNotes.Resources.messageTitle, MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
        //        //Log4Net.Log.Error(exception.Message + exception.StackTrace);
        //    }
        //    return flag;
        //}

        //public static string LdapPathToEmailInReceiverControl(string allLdapPath)
        //{
        //    string str = string.Empty;
        //    if (string.IsNullOrEmpty(allLdapPath) || !(allLdapPath.Trim() != ""))
        //    {
        //        return str;
        //    }
        //    allLdapPath = allLdapPath.Replace(';', ',');
        //    allLdapPath = allLdapPath.TrimEnd(new char[] { ',' });
        //    string[] strArray = allLdapPath.Split(new char[] { ',' });
        //    string input = string.Empty;
        //    string s = string.Empty;
        //    foreach (string str4 in strArray)
        //    {
        //        input = str4;
        //        if (!str4.Contains<char>('@'))
        //        {
        //            input = Regex.Replace(Regex.Replace(Regex.Replace(input, "cn=", "", RegexOptions.IgnoreCase), "ou=", "", RegexOptions.IgnoreCase), "o=", "", RegexOptions.IgnoreCase);
        //        }
        //        s = s + input + ",";
        //    }
        //    return NotesEmailAddressTransforWithLdapSearch(s);
        //}

        public static string LdapPathToEmailWithDirectlyTransform(string allLdapPath)
        {
            string str = string.Empty;
            if (string.IsNullOrEmpty(allLdapPath) || !(allLdapPath.Trim() != ""))
            {
                return str;
            }
            allLdapPath = allLdapPath.Replace(';', ',');
            allLdapPath = allLdapPath.TrimEnd(new char[] { ',' });
            string[] strArray = allLdapPath.Split(new char[] { ',' });
            string input = string.Empty;
            string multiplateEmails = string.Empty;
            foreach (string str4 in strArray)
            {
                input = str4;
                if (!str4.Contains<char>('@'))
                {
                    input = Regex.Replace(Regex.Replace(Regex.Replace(input, "cn=", "", RegexOptions.IgnoreCase), "ou=", "", RegexOptions.IgnoreCase), "o=", "", RegexOptions.IgnoreCase);
                }
                multiplateEmails = multiplateEmails + input + ",";
            }
            return NotesEmailAddressTransforDirectly(multiplateEmails);
        }

        public static bool ModifyAuditMailSubject(ref string originSubject)
        {
            bool flag = false;
            string str = originSubject;
            if (!string.IsNullOrEmpty(str) && str.Contains("[Approve Required]"))
            {
                str = str.Replace("[Approve Required]", "<Approve Required>");
                originSubject = str;
                flag = true;
            }
            return flag;
        }

        public static void ModifyTabpagelableToFitImgShow(TabPage curAddNewTbPage)
        {
            curAddNewTbPage.Text = curAddNewTbPage.Text + "    ";
        }

        public static void NetWorkMessage()
        {
            try
            {
            }
            catch (Exception exception)
            {
                //Log4Net.Log.Debug("判斷服務器鏈接是否斷開(首次登錄此異常可忽視)：" + exception.Message + exception.StackTrace);
            }
        }

        public static string NotesEmailAddressTransforDirectly(string multiplateEmails)
        {
            string str = "";
            if (string.IsNullOrEmpty(multiplateEmails))
            {
                return str;
            }
            string[] strArray = multiplateEmails.Trim().Replace(';', ',').TrimEnd(new char[] { ',' }).Split(new char[] { ',' });
            string str2 = "";
            for (int i = 0; i < strArray.Length; i++)
            {
                str2 = strArray[i];
                if (!string.IsNullOrEmpty(str2))
                {
                    str2 = str2.Trim();
                    if (!(!str2.Contains("/") || str2.Contains("@")))
                    {
                        str2 = (str2 + "@foxconn.com").Replace(" ", "_");
                        str = str + str2.ToLower(CultureInfo.CurrentCulture) + ",";
                    }
                    else
                    {
                        str = str + str2 + ",";
                    }
                }
            }
            return str.TrimEnd(new char[] { ',' });
        }

        //public static string NotesEmailAddressTransforWithLdapSearch(string s)
        //{
        //    int num;
        //    string str = "";
        //    if (string.IsNullOrEmpty(s))
        //    {
        //        return str;
        //    }
        //    string[] strArray = s.Trim().Replace(';', ',').TrimEnd(new char[] { ',' }).Split(new char[] { ',' });
        //    string str2 = "";
        //    Dictionary<int, string> dictionary = new Dictionary<int, string>();
        //    string str3 = Guid.NewGuid().ToString();
        //    List<string> allNotesEmails = new List<string>();
        //    for (num = 0; num < strArray.Length; num++)
        //    {
        //        str2 = strArray[num];
        //        if (!string.IsNullOrEmpty(str2))
        //        {
        //            str2 = str2.Trim();
        //            if (!(!str2.Contains("/") || str2.Contains("@")))
        //            {
        //                allNotesEmails.Add(str2);
        //                dictionary.Add(num, str3);
        //            }
        //            else
        //            {
        //                dictionary.Add(num, str2);
        //            }
        //        }
        //    }
        //    List<string> list2 = new List<string>();
        //    if (allNotesEmails.Count > 0)
        //    {
        //        List<string> list3 = new BlllDapAddrlst().SearchInternetEmailBaseNotesEmail(allNotesEmails);
        //        for (num = 0; num < list3.Count; num++)
        //        {
        //            StringBuilder builder;
        //            string str4 = list3[num];
        //            if (!(!str4.Contains("/") || str4.Contains("@")))
        //            {
        //                builder = new StringBuilder(str4);
        //                builder.Append("@foxconn.com").Replace(" ", "_");
        //                str4 = builder.ToString().ToLower(CultureInfo.CurrentCulture);
        //            }
        //            else if ((str4.Trim() == "@@foxconn.com") || (str4.Trim() == "%@foxconn.com"))
        //            {
        //                string str5 = allNotesEmails[num];
        //                builder = new StringBuilder(str5);
        //                builder.Append("@foxconn.com").Replace(" ", "_");
        //                str4 = builder.ToString().ToLower(CultureInfo.CurrentCulture);
        //            }
        //            list2.Add(str4);
        //        }
        //    }
        //    int num2 = 0;
        //    foreach (KeyValuePair<int, string> pair in dictionary)
        //    {
        //        if (!string.Equals(pair.Value, str3))
        //        {
        //            str = str + pair.Value + ",";
        //        }
        //        else if ((list2.Count > 0) && (num2 < list2.Count))
        //        {
        //            str = str + list2[num2] + ",";
        //            num2++;
        //        }
        //    }
        //    return str.TrimEnd(new char[] { ',' });
        //}

        //private void PersonalLoginInfo(string FolderPath, string orgFilePath)
        //{
        //    try
        //    {
        //        DataSet set = XmlHelper.ReadToDS(FolderPath);
        //        string str = string.Empty;
        //        for (int i = 0; i < set.Tables[0].Columns.Count; i++)
        //        {
        //            str = str + set.Tables[0].Columns[i].ToString() + ",";
        //        }
        //        if (str.Contains("version"))
        //        {
        //            XmlHelper helper = new XmlHelper(FolderPath);
        //            helper.DeleteAllChildNode("root", "version", "", "");
        //        }
        //        if (!str.Contains("SelectedFactoryId"))
        //        {
        //            new XmlHelper(FolderPath).AppendRootChildNode("SelectedFactoryId", "24");
        //        }
        //        else if (!((str.Contains("IP") && str.Contains("Name")) && str.Contains("display")))
        //        {
        //            File.Copy(orgFilePath, FolderPath);
        //        }
        //    }
        //    catch (Exception exception)
        //    {
        //        //Log4Net.Log.Error("處理個人文件下LoginInfo出錯：" + exception.Message + exception.StackTrace);
        //    }
        //}

        public static string ProcessFileName(string fileName)
        {
            string str=null;
            try
            {
                char[] chArray = new char[] { '/', '\\', ':', '*', '?', '<', '>', '|', '"', '\t', '\r', '\n', '!' };
                foreach (char ch in chArray)
                {
                    fileName = fileName.Replace(ch, '_');
                }
                fileName = GetRightFilename(fileName, 140);
                str = fileName;
            }
            catch
            {
            }
            return str;
        }

        //public static bool RefreshGridViewAfterMultiMailsMovedOrCopyed(bool isMove)
        //{
        //    if (MailListView.stac_currentSeleteRowsCount >= 1)
        //    {
        //        for (int i = 0; i < MailListView.stac_dataGridView.Rows.Count; i++)
        //        {
        //            if (MailListView.stac_dataGridView.Rows[i].Cells["selectFlag"].Value.ToString().Trim() == "1")
        //            {
        //                if (isMove)
        //                {
        //                    MailListView.stac_dataGridView.Rows.RemoveAt(i);
        //                    i--;
        //                }
        //                else
        //                {
        //                    MailListView.stac_dataGridView.Rows[i].Cells["selectFlag"].Value = "0";
        //                    MailListView.stac_dataGridView.Rows[i].Cells["selectImage"].Value = DataGridViewIcons.MSBlankIcon.ToArray();
        //                }
        //            }
        //        }
        //        MailListView.stac_currentSeleteRowsCount = 0;
        //    }
        //    return true;
        //}

        public static string RemoveAtEnd(string sourceString, string deletingString)
        {
            int length = 0;
            int count = 0;
            int startIndex = 0;
            string str = string.Empty;
            try
            {
                str = sourceString;
                if (sourceString.IndexOf(deletingString) <= 0)
                {
                    return str;
                }
                length = sourceString.Length;
                count = deletingString.Length;
                startIndex = length - count;
                if (string.Equals(sourceString.Substring(startIndex), deletingString, StringComparison.OrdinalIgnoreCase))
                {
                    str = sourceString.Remove(startIndex, count);
                }
            }
            catch (Exception exception)
            {
                //Log4Net.Log.Error(exception.StackTrace + exception.Message);
            }
            return str;
        }

        public static string RenameByRandAndGuid(string path)
        {
            string str = string.Empty;
            string fileName = string.Empty;
            string extName = string.Empty;
            string str5 = string.Empty;
            int length = 0;
            int num2 = 0;
            try
            {
                Random random = new Random(0x1869f);
                str5 = random.Next().ToString().Substring(0, 4);
                fileName = GetFileName(path);
                extName = GetExtName(path);
                length = fileName.Length;
                num2 = extName.Length;
                str = fileName.Substring(0, length - num2) + str5 + ConstData.CONST_GUID + extName;
            }
            catch (Exception exception)
            {
                //Log4Net.Log.Error("唯一命名失败：" + exception.Message);
            }
            return str;
        }

        public string ReplaceAllDoublQuotationMarksContent(string inputEmailAddress)
        {
            string str = inputEmailAddress;
            if (!string.IsNullOrEmpty(inputEmailAddress))
            {
                str = this.ReplaceDoublQuotationMarksContent(inputEmailAddress);
                for (int i = str.IndexOf("\"", StringComparison.Ordinal); i >= 0; i = str.IndexOf("\"", StringComparison.Ordinal))
                {
                    str = this.ReplaceDoublQuotationMarksContent(str);
                }
            }
            return str;
        }

        private string ReplaceDoublQuotationMarksContent(string inputEmailAddress)
        {
            if (!string.IsNullOrEmpty(inputEmailAddress))
            {
                int index = inputEmailAddress.IndexOf("\"", StringComparison.Ordinal);
                if (index < 0)
                {
                    return inputEmailAddress;
                }
                int num2 = inputEmailAddress.IndexOf("\"", index + 1, StringComparison.Ordinal);
                if ((num2 > 0) && (num2 > index))
                {
                    string oldValue = inputEmailAddress.Substring(index, (num2 - index) + 1);
                    inputEmailAddress = inputEmailAddress.Replace(oldValue, "");
                    return inputEmailAddress;
                }
                if (num2 < 0)
                {
                    inputEmailAddress = inputEmailAddress.Replace("\"", "");
                }
            }
            return inputEmailAddress;
        }

        public static string ResumeExtraAddOrReplaceContentOfLocalGrpName(string inputContent)
        {
            string str = inputContent;
            if (string.IsNullOrEmpty(inputContent) || !inputContent.Contains("@SNLocalGroup.sn.com"))
            {
                return str;
            }
            str = string.Empty;
            inputContent = inputContent.Replace(';', ',');
            string[] strArray = inputContent.Split(new char[] { ',' });
            foreach (string str2 in strArray)
            {
                if (!string.IsNullOrEmpty(str2))
                {
                    if (str2.Contains("@SNLocalGroup.sn.com"))
                    {
                        string str3 = str2;
                        str3 = str3.Replace("%20_%", " ").Replace("@SNLocalGroup.sn.com", "");
                        str = str + str3 + ",";
                    }
                    else
                    {
                        str = str + str2 + ",";
                    }
                }
            }
            return str.TrimEnd(new char[] { ',' });
        }

        //public static void SetMenubarItemSize(ref TableLayoutPanel tableLayoutPanel, ref MenuStrip menuStrip)
        //{
        //    int num = 0;
        //    string menubarItemSizeSet;//= new SysUISetXml().GetMenubarItemSizeSet();
        //    if (menubarItemSizeSet != null)
        //    {
        //        if (!(menubarItemSizeSet == "small"))
        //        {
        //            if (!(menubarItemSizeSet == "middle") && (menubarItemSizeSet == "big"))
        //            {
        //                num = 11;
        //                tableLayoutPanel.RowStyles[0].Height = 30f;
        //                goto Label_007C;
        //            }
        //        }
        //        else
        //        {
        //            num = 8;
        //            goto Label_007C;
        //        }
        //    }
        //    num = 9;
        //    tableLayoutPanel.RowStyles[0].Height = 25f;
        //Label_007C:
        //    menuStrip.Font = new Font(menuStrip.Font.Name, (float) num, menuStrip.Font.Style);
        //    menuStrip.Dock = DockStyle.Fill;
        //}

        public static bool SetSupernotesRegistry(string strExe)
        {
            bool flag = false;
            try
            {
                Registry.ClassesRoot.OpenSubKey(@"MAILTO\SHELL\OPEN\COMMAND", true).SetValue("", "\"" + strExe + "\"");
                flag = true;
            }
            catch (Exception exception)
            {
                flag = false;
                //Log4Net.Log.Error("設置SuperNotes為默認郵箱失敗：" + exception.Message);
            }
            return flag;
        }

        //public static void SetToolbarDisplayStyle(ref TableLayoutPanel tableLayoutPanel, ref ToolStrip toolStrip)
        //{
        //    bool imageAndText = false;
        //    ToolStripItemDisplayStyle toolbarDisplayStyle = GetToolbarDisplayStyle(ref tableLayoutPanel, ref imageAndText);
        //    for (int i = 0; i < toolStrip.Items.Count; i++)
        //    {
        //        if ((toolStrip.Items[i].GetType() == typeof(ToolStripButton)) || (toolStrip.Items[i].GetType() == typeof(ToolStripSplitButton)))
        //        {
        //            toolStrip.Items[i].DisplayStyle = toolbarDisplayStyle;
        //            toolStrip.Items[i].TextImageRelation = TextImageRelation.ImageAboveText;
        //        }
        //    }
        //    SetToolbarSize(ref tableLayoutPanel, ref toolStrip, imageAndText);
        //}

        //public static void SetToolbarSize(ref TableLayoutPanel tableLayoutPanel, ref ToolStrip toolStrip, bool imageAndText)
        //{
        //    string toolBarItemSizeSet = new SysUISetXml().GetToolBarItemSizeSet();
        //    int width = 0;
        //    int num2 = 0;
        //    string str2 = toolBarItemSizeSet;
        //    if (((str2 == null) || (str2 == "small")) || (str2 != "big"))
        //    {
        //        width = 0x10;
        //        num2 = 9;
        //        tableLayoutPanel.RowStyles[1].Height = imageAndText ? ((float) 40) : ((float) 30);
        //        toolStrip.Dock = DockStyle.Fill;
        //    }
        //    else
        //    {
        //        width = 0x15;
        //        num2 = 11;
        //        tableLayoutPanel.RowStyles[1].Height = imageAndText ? ((float) 50) : ((float) 0x23);
        //        toolStrip.Dock = DockStyle.Fill;
        //    }
        //    toolStrip.ImageScalingSize = new Size(width, width);
        //    toolStrip.Font = new Font(toolStrip.Font.Name, (float) num2, toolStrip.Font.Style);
        //}

        //public bool SpecialAddressTransforLimit(MailInfo mailInfo)
        //{
        //    bool flag = true;
        //    int num = 0;
        //    if (!(string.IsNullOrEmpty(mailInfo.MailGeter) || !(mailInfo.MailGeter.Trim() != "")))
        //    {
        //        string[] array = this.GetArray(mailInfo.MailGeter.Trim());
        //        num += array.Length;
        //    }
        //    if (!(string.IsNullOrEmpty(mailInfo.MailBcc) || !(mailInfo.MailBcc.Trim() != "")))
        //    {
        //        string[] strArray2 = this.GetArray(mailInfo.MailBcc.Trim());
        //        num += strArray2.Length;
        //    }
        //    if (!(string.IsNullOrEmpty(mailInfo.MailCC) || !(mailInfo.MailCC.Trim() != "")))
        //    {
        //        string[] strArray3 = this.GetArray(mailInfo.MailCC.Trim());
        //        num += strArray3.Length;
        //    }
        //    if (num > 0)
        //    {
        //        flag = this.IsRecipientLimit(num);
        //    }
        //    return flag;
        //}

        public static string TempPath()
        {
            string str = string.Empty;
            DateTime now = DateTime.Now;
            try
            {
                str = Environment.GetFolderPath(Environment.SpecialFolder.Personal) + @"\Downloads\SuperNotes";
                if (string.IsNullOrEmpty(str))
                {
                    return str;
                }
                if (!Directory.Exists(str))
                {
                    Directory.CreateDirectory(str);
                }
            }
            catch (Exception exception)
            {
                //Log4Net.Log.Error("取得下載安裝包的零時目錄：" + exception.Message + exception.StackTrace);
            }
            return str;
        }

        public static bool UpdateStateByDel(MailHeaderAndBody mail)
        {
            bool flag = false;
            try
            {
                if ((mail != null) && (mail.State != null))
                {
                    foreach (string str in mail.State)
                    {
                        if (string.Equals(IMAP_t_MsgFlags.Answered, str, StringComparison.OrdinalIgnoreCase))
                        {
                            mail.State.Remove(str);
                        }
                        if (string.Equals(IMAP_t_MsgFlags.Deleted, str, StringComparison.OrdinalIgnoreCase))
                        {
                            mail.State.Remove(str);
                        }
                        if (string.Equals(IMAP_t_MsgFlags.Flagged, str, StringComparison.OrdinalIgnoreCase))
                        {
                            mail.State.Remove(str);
                        }
                        if (string.Equals(IMAP_t_MsgFlags.Recent, str, StringComparison.OrdinalIgnoreCase))
                        {
                            mail.State.Remove(str);
                        }
                    }
                }
                flag = true;
            }
            catch (Exception exception)
            {
                //Log4Net.Log.Error("取得用于刪除附件的State：" + exception.Message);
            }
            return flag;
        }

        public static string UTCCST2LocalDatetime(string utcStr, out bool isConvertSucess)
        {
            string str = utcStr;
            isConvertSucess = false;
            if (!string.IsNullOrEmpty(utcStr))
            {
                if (utcStr.ToUpper(CultureInfo.InvariantCulture).Contains("(UTC)"))
                {
                    try
                    {
                        str = Convert.ToDateTime(utcStr.ToUpper(CultureInfo.InvariantCulture).Replace("(UTC)", "")).AddHours(-8.0).ToString();
                        isConvertSucess = true;
                    }
                    catch
                    {
                    }
                    return str;
                }
                if (!utcStr.ToUpper(CultureInfo.InvariantCulture).Contains("(CST)"))
                {
                    return str;
                }
                try
                {
                    str = Convert.ToDateTime(utcStr.ToUpper(CultureInfo.InvariantCulture).Replace("(CST)", "")).ToString();
                    isConvertSucess = true;
                }
                catch
                {
                }
            }
            return str;
        }
    }
}

