using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.Net.Security;
using System.IO;
using System.Xml;
using System.Web.Services.Description;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using System.CodeDom;
using System.CodeDom.Compiler;
using Microsoft.CSharp;
using LumiSoft.Net;
using LumiSoft.Net.IMAP;
using LumiSoft.Net.IMAP.Client;
using LumiSoft.Net.Mail;
using LumiSoft.Net.Mime;
using LumiSoft.Net.MIME;
using System.Collections;
using System.Globalization;
using SFC_Tools.Classes;
using System.Threading;

namespace SFC_Tools.Forms
{
    public partial class ucMailTest : UserControl
    {
        private string strA = string.Empty;
        private string b = string.Empty;
        //rain.yy.liu@mail.foxconn.net
        private string c = string.Empty;
        private string d = string.Empty;
        private string account= string.Empty;
        private string Urlam = string.Empty;
        private string WebUrl = "http://fmail.efoxconn.com:8080/superNotesWS/accounts/";

        private IMAP_Client imap = new IMAP_Client();
        private List<MailHeaderAndBody> listMail = new List<MailHeaderAndBody>();
        List<MailHeaderAndBody> targetListMail=new List<MailHeaderAndBody>();
        private long iStart=0;
        private long iEnd = 0;
        private int iThreadNum = 0;
        private bool []bThreadStatues { get;set;}

        public ucMailTest()
        {
            InitializeComponent();
        }

        public DataTable CopyMailToDataTable( List<MailHeaderAndBody> mListMail)
        {
            DataTable dtTable = new DataTable();
            //dtTable.Columns.Add("dirImage", typeof(byte[]));
            //dtTable.Columns.Add("selectImage", typeof(byte[]));
            //dtTable.Columns.Add("attachImage", typeof(byte[]));
            //dtTable.Columns.Add("seenImage", typeof(byte[]));
            //dtTable.Columns.Add("moodImage", typeof(byte[]));
            dtTable.Columns.Add("Cc", typeof(string));
            dtTable.Columns.Add("From", typeof(string));
            dtTable.Columns.Add("Sub", typeof(string));
            dtTable.Columns.Add("Received", typeof(string));
            dtTable.Columns.Add("CSize", typeof(int));
            dtTable.Columns.Add("Mime", typeof(string));
            dtTable.Columns.Add("Uid", typeof(string));
            dtTable.Columns.Add("MyDate", typeof(string));
            dtTable.Columns.Add("seenFlag", typeof(string));
            dtTable.Columns.Add("selectFlag", typeof(string));
            dtTable.Columns.Add("attachFlag", typeof(string));
            dtTable.Columns.Add("transferFlag", typeof(string));
            dtTable.Columns.Add("uidForSearchMax", typeof(long));
            dtTable.Columns.Add("importantFlag", typeof(string));
            DataRow dr;

            try{
                foreach(MailHeaderAndBody mhb in mListMail)
                {
                    dr = dtTable.NewRow();
                    //dr["dirImage"] = null;
                    //dr["selectImage"] = null;
                    //dr["attachImage"] = null;
                    //dr["seenImage"] = null;
                    //dr["moodImage"] = null;
                    dr["Cc"] = mhb.CC;
                    dr["From"] = mhb.From;
                    dr["Sub"] = mhb.Sub;
                    dr["Received"] = mhb.Received;
                    dr["CSize"] = mhb.Size;
                    dr["Mime"] = mhb.Mime;
                    dr["Uid"] = mhb.Uid;
                    dr["MyDate"] = (DateTime.Parse(mhb.MyDate.Trim())).ToString("yyyy/MM/dd HH:mm");
                    dr["seenFlag"] = "Y";
                    dr["selectFlag"] = "0";
                    dr["attachFlag"] =mhb.HasAttachments;
                    dr["transferFlag"] = mhb.AllowTransfer;
                    dr["uidForSearchMax"] = mhb.Uid;
                    dr["importantFlag"] = mhb.Importance.ToString();
                   // dr["fromName"] = "N";//mhb.FromDisplayName.ToString();
                    dtTable.Rows.Add(dr);
                }
            }
            catch (Exception exception2)
            {
                throw new Exception(exception2.Message);
            }
            return dtTable; 
        }

        private void btnGo_Click(object sender, EventArgs e)
        {
           
            string text = "rain.yy.liu@mail.foxconn.com";
          
            bool bIsConn=this.CmdConnect("chnlh07.efoxconn.com",993);

            bool bIsLogin = this.CmdLogOn("rain.yy.liu@mail.foxconn.com","Rain1436");
            bool bIsFolderOk=CmdSelectFolder("INBOX");
            int iTotalCnt = CmdGetCurrentFolderMailTotalCount();
            long iUid = CmdGetCurrentFolderNextMailId();
            listMail.Clear();
     

            //mylistMail.AddRange(mylistMail);
            //DataTable dt = CopyMailToDataTable(mylistMail);
            //this.dataGridView1.DataSource = dt;

            iStart = 1;
            iEnd = iTotalCnt;
            List<MailHeaderAndBody> mylistMail = this.CmdReceiveHead(1, iTotalCnt);
/*
          this.iThreadNum = iTotalCnt / 100;
            if (iTotalCnt % 200 > 0)
                iThreadNum = iThreadNum + 1;
            iThreadNum = iThreadNum - 1;
            bThreadStatues = new bool[iThreadNum];
            GetMailContent(1, iTotalCnt);
            while (true)
            {
                foreach (bool bStatues in bThreadStatues)
                {
                    if (bStatues == false)
                        System.Threading.Thread.Sleep(100);
                    else
                    {
                        if (this.targetListMail.Count > 0)
                        {
                            DataTable dt = CopyMailToDataTable(targetListMail);
                            this.dataGridView1.DataSource = dt;
                            this.lblTotalCnt.Text = dt.Rows.Count.ToString();
                            break;
                        } 
                    }
                }
            }
             //mylistMail.AddRange(mylistMail);
             //DataTable dt = CopyMailToDataTable(mylistMail);
             //this.dataGridView1.DataSource = dt;
        */
            
            DataTable dt = CopyMailToDataTable(mylistMail);
            this.dataGridView1.DataSource = dt;
            this.lblTotalCnt.Text = dt.Rows.Count.ToString();
            SFCStartup.dba.InsertMailDataToDB(dt);
            this.CmdDisconnect();
        }
        private bool GetMailContent(long startID,long endID)
        {
            
            System.Threading.Thread[] myTrd=new System.Threading.Thread[this.iThreadNum];
            for (int i = 0; i < this.iThreadNum; i++)
            {
                myTrd[i] = new System.Threading.Thread(new System.Threading.ParameterizedThreadStart(GetMailContentByMultiThread));
                myTrd[i].Start((i+1)*100);
                bThreadStatues[i] = false;
            }

            return true;
        }
        private void GetMailContentByMultiThread(object a)
        {
            long uEnd = Convert.ToInt64(a);
            long uStart = uEnd - 99;
            List<MailHeaderAndBody> mylistMail = this.CmdReceiveHeadA(uStart, uEnd);
            //targetListMail.AddRange(mylistMail);
            targetListMail = mylistMail;
            bThreadStatues[uEnd / 100 - 1] = true;
        }
        public int CmdGetCurrentFolderMailTotalCount()
        {
            return this.imap.SelectedFolder.MessagesCount;
        }

        public long CmdGetCurrentFolderNextMailId()
        {
            return this.imap.SelectedFolder.UidNext;
        }

        public List<MailHeaderAndBody> CmdReceiveHead(long startId, long endId)
        {
           
            IMAP_SequenceSet seqSet = new IMAP_SequenceSet();
            MailHeaderAndBody mail = null;
           
            seqSet.Parse(startId.ToString() + ":" + endId.ToString());
            mail = this.CmdReceive(false, seqSet, mail);
            return this.listMail;
        }
        
         public List<MailHeaderAndBody> CmdReceiveHeadA(long startId, long endId)
        {
            IMAP_SequenceSet seqSet = new IMAP_SequenceSet();
            MailHeaderAndBody mail = null;
            List<MailHeaderAndBody> myTempListMail=new List<MailHeaderAndBody>();
            seqSet.Parse(startId.ToString() + ":" + endId.ToString());
            myTempListMail = this.CmdReceiveA(false, seqSet, mail);
            return myTempListMail;
        }

        private MailHeaderAndBody CmdReceive(bool isID, IMAP_SequenceSet seqSet, MailHeaderAndBody mail)
        {
            IMAP_Client_FetchHandler handler = new IMAP_Client_FetchHandler();
            handler.NextMessage += delegate(object s, EventArgs e)
            {
                mail = new MailHeaderAndBody();
            };  
            handler.InternalDate += delegate(object s, EventArgs<DateTime> e)
            {
                try
                {
                    mail.MyDate = e.Value.ToString();
                }
                catch (Exception exception)
                {
                   
                }
            };
            handler.Rfc822Size += delegate(object s, EventArgs<int> e)
            {
                try
                {
                    mail.Size = e.Value.ToString(CultureInfo.CurrentCulture);
                }
                catch (Exception exception)
                {
                   
                }
            };
            handler.UID += delegate(object s, EventArgs<long> e)
            {
                try
                {
                    mail.Uid = e.Value.ToString(CultureInfo.CurrentCulture);
                    if (!this.listMail.Contains(mail))
                    {
                        this.listMail.Add(mail);
                    }
                }
                catch (Exception exception)
                {
                   
                }
            };
            handler.Rfc822Header += delegate(object sender, EventArgs<string> e)
            {
                Exception exception;
                try
                {
                    string s = e.Value;
                    FileEncrypt instance = null;
                    if (s != null)
                    {
                        s = s.Replace("?MS950?", "?big5?");
                        Mail_Message message = Mail_Message.ParseFromByte(Encoding.UTF8.GetBytes(s));
                        string originSubject = message.Subject;
                        mail.IsAuditEmail = PublicMethod.ModifyAuditMailSubject(ref originSubject);
                        mail.Sub = originSubject;
                        mail.Sub = PublicMethod.RemoveAtEnd(mail.Sub, "\0");
                        if (message.Sender != null)
                        {
                            mail.From = message.Sender.Address;
                            mail.OriginSender = mail.From;
                            mail.FromDisplayName = message.Sender.DisplayName;
                        }
                        if ((message.From != null) && (message.From.Count > 0))
                        {
                            mail.OriginSender = message.From[0].Address;
                            if (string.IsNullOrEmpty(mail.From))
                            {
                                mail.From = message.From[0].Address;
                                mail.FromDisplayName = message.From[0].DisplayName;
                            }
                        }
                        MIME_h[] _hArray = message.Header.ToArray();
                        if (_hArray != null)
                        {
                            foreach (MIME_h _h in _hArray)
                            {
                                MIME_Encoding_EncodedWord word;
                                if (((_h.Name == "ImportantFlag") || (_h.Name == "Importance")) && !mail.State.Contains("Important"))
                                {
                                    mail.State.Add("Important");
                                }
                                if (((_h.Name == "AttachmentFlag") && string.Equals(_h.ValueToString().Trim(), "Attached", StringComparison.OrdinalIgnoreCase)) && !mail.State.Contains("Attached"))
                                {
                                    mail.State.Add("Attached");
                                }
                                if ((_h.Name == "Content-Type") && _h.ValueToString().Trim().Contains("text/calendar"))
                                {
                                    mail.MailType = "Calendar";
                                }
                                else
                                {
                                    mail.MailType = "Mail";
                                }
                                if (((_h.Name == "Sensitivity") && "Private".Equals(_h.ValueToString().Trim())) && !mail.State.Contains("NoForward"))
                                {
                                    mail.State.Add("NoForward");
                                }
                                if (_h.Name == "StartTime")
                                {
                                    mail.State.Add(_h.ValueToString().Trim());
                                    mail.MeetingStartTime = _h.ValueToString().Trim();
                                    mail.MailType = "Calendar";
                                }
                                if (_h.Name == "EndTime")
                                {
                                    mail.State.Add(_h.ValueToString().Trim());
                                    mail.MeetingEndTime = _h.ValueToString().Trim();
                                }
                                if (_h.Name == "Address")
                                {
                                    mail.State.Add(_h.ValueToString().Trim());
                                    try
                                    {
                                        byte[] bytes = Convert.FromBase64String(_h.ValueToString().Trim());
                                        mail.MeetingLocation = Encoding.UTF8.GetString(bytes);
                                    }
                                    catch
                                    {
                                        mail.MeetingLocation = _h.ValueToString().Trim();
                                    }
                                }
                                if (_h.Name == "LocalMeetingSendDate")
                                {
                                    mail.LocalMeetingSentDate = _h.ValueToString().Trim();
                                }
                                if ((_h.Name == "X-Notes-Item") && _h.ValueToString().Trim().Contains("name=$NoPurge"))
                                {
                                    mail.MailType = "Calendar";
                                }
                                if (_h.Name == "Status")
                                {
                                    mail.State.Add(_h.ValueToString().Trim());
                                    mail.MeetingStatus = _h.ValueToString().Trim();
                                }
                                if (_h.Name == "MailClassification")
                                {
                                    mail.State.Add(_h.ValueToString().Trim());
                                    mail.MailClassification = _h.ValueToString().Trim();
                                }
                                if ((_h.Name != null) && (_h.Name.ToUpperInvariant() == "DATE"))
                                {
                                    string str5 = "";
                                    try
                                    {
                                        str5 = _h.ValueToString().Trim();
                                        mail.LetterMailCreatedTime = Convert.ToDateTime(str5).ToString();
                                    }
                                    catch (Exception exception1)
                                    {
                                        bool flag;
                                        exception = exception1;
                                        string str6 = PublicMethod.UTCCST2LocalDatetime(str5, out flag);
                                        if (flag)
                                        {
                                            mail.LetterMailCreatedTime = str6;
                                        }
                                    }
                                }
                                instance = FileEncrypt.GetInstance();
                                if (instance.EncryptCode("BanDownReadTag").Equals(_h.Name))
                                {
                                    if (instance.EncryptCode("1").Equals(_h.ValueToString().Trim()))
                                    {
                                        mail.BanDownRead = instance.EncryptCode("1");
                                    }
                                    else
                                    {
                                        mail.BanDownRead = instance.EncryptCode("0");
                                    }
                                }
                                if (string.Equals("MoodMark", _h.Name))
                                {
                                    mail.MoodMark = _h.ValueToString().Trim();
                                }
                                if (string.Equals("letterPaperLetterName", _h.Name))
                                {
                                    string text = _h.ValueToString().Trim();
                                    try
                                    {
                                        word = new MIME_Encoding_EncodedWord(MIME_EncodedWordEncoding.B, Encoding.UTF8);
                                        text = word.Decode(text);
                                        mail.LetterMailLetterName = text;
                                    }
                                    catch
                                    {
                                    }
                                }
                                if (string.Equals("CalendarItemType", _h.Name))
                                {
                                    mail.CalendarItemType = _h.ValueToString().Trim();
                                }
                                if (string.Equals("CalendarItemAddress", _h.Name))
                                {
                                    string str8 = _h.ValueToString().Trim();
                                    try
                                    {
                                        word = new MIME_Encoding_EncodedWord(MIME_EncodedWordEncoding.B, Encoding.UTF8);
                                        str8 = word.Decode(str8);
                                        mail.CalendarItemAddress = str8;
                                    }
                                    catch
                                    {
                                    }
                                }
                                if (string.Equals("CalendarItemStartTime", _h.Name))
                                {
                                    string str9 = _h.ValueToString().Trim();
                                    mail.CalendarItemStartTime = str9;
                                    try
                                    {
                                        word = new MIME_Encoding_EncodedWord(MIME_EncodedWordEncoding.B, Encoding.UTF8);
                                        str9 = word.Decode(str9);
                                        mail.CalendarItemStartTime = str9;
                                    }
                                    catch
                                    {
                                    }
                                }
                                if (string.Equals("CalendarItemEndTime", _h.Name))
                                {
                                    string str10 = _h.ValueToString().Trim();
                                    mail.CalendarItemEndTime = str10;
                                    try
                                    {
                                        word = new MIME_Encoding_EncodedWord(MIME_EncodedWordEncoding.B, Encoding.UTF8);
                                        str10 = word.Decode(str10);
                                        mail.CalendarItemEndTime = str10;
                                    }
                                    catch
                                    {
                                    }
                                }
                                if (string.Equals("CalendarItemRemindTime", _h.Name))
                                {
                                    mail.CalendarItemRemindTime = _h.ValueToString().Trim();
                                    string str11 = _h.ValueToString().Trim();
                                    try
                                    {
                                        str11 = new MIME_Encoding_EncodedWord(MIME_EncodedWordEncoding.B, Encoding.UTF8).Decode(str11);
                                        mail.CalendarItemRemindTime = str11;
                                    }
                                    catch
                                    {
                                    }
                                }
                                if (string.Equals("CalendarItemRemindDesc", _h.Name))
                                {
                                    mail.CalendarItemRemindDesc = _h.ValueToString().Trim();
                                    string str12 = _h.ValueToString().Trim();
                                    try
                                    {
                                        str12 = new MIME_Encoding_EncodedWord(MIME_EncodedWordEncoding.B, Encoding.UTF8).Decode(str12);
                                        mail.CalendarItemRemindDesc = str12;
                                    }
                                    catch
                                    {
                                    }
                                }
                            }
                        }
                        if (!string.IsNullOrEmpty(s))
                        {
                            instance = FileEncrypt.GetInstance();
                            int startIndex = s.IndexOf(instance.EncryptCode("EncryptHelperTag") + ":", StringComparison.Ordinal);
                            try
                            {
                                if (startIndex > 0)
                                {
                                    string[] strArray = s.Substring(startIndex, 0x42).Split(new char[] { ':' });
                                    if (!string.IsNullOrEmpty(strArray[0]))
                                    {
                                        mail.Encrypt = strArray[1].Trim();
                                    }
                                }
                            }
                            catch (Exception exception2)
                            {
                                exception = exception2;
                                
                            }
                        }
                    }
                }
                catch (Exception exception3)
                {
                    exception = exception3;
                   
                }
            };
            handler.Flags += delegate(object sender, EventArgs<string[]> e)
            {
                try
                {
                    mail.State.Clear();
                    if ((e.Value != null) && (e.Value.Count<string>() > 0))
                    {
                        mail.State.AddRange(e.Value);
                    }
                }
                catch (Exception exception)
                {
                  
                }
            };
            handler.Envelope += delegate(object s, EventArgs<IMAP_Envelope> e)
            {
                try
                {
                    IMAP_Envelope envelope = e.Value;
                    string str = "";
                    if (string.IsNullOrEmpty(str))
                    {
                    }
                    string address = "";
                    address = getAddress(envelope.Cc, address);
                    string str3 = "";
                    str3 = getAddress(envelope.To, str3);
                    string str4 = "";
                    str4 = getAddress(envelope.Bcc, str4);
                    FullInfo(mail, envelope, str, address, str3, str4);
                }
                catch (Exception exception)
                {
                    
                }
            };
            IMAP_Fetch_DataItem[] items = new IMAP_Fetch_DataItem[] { new IMAP_Fetch_DataItem_Envelope(), new IMAP_Fetch_DataItem_InternalDate(), new IMAP_Fetch_DataItem_Rfc822Size(), new IMAP_Fetch_DataItem_Flags(), new IMAP_Fetch_DataItem_Uid(), new IMAP_Fetch_DataItem_Rfc822Header() };
            try
            {
                this.imap.Fetch(isID, seqSet, items, handler);
            }
            catch (IOException exception)
            {
                throw new Exception(exception.Message);
            }
     
            finally
            {
                this.CmdReceiveBodyStruct(isID, seqSet);
            }
            return mail;
        }

        /// <summary>
        /// theory test
        /// </summary>
        /// <param name="isID"></param>
        /// <param name="seqSet"></param>
        /// <param name="mail"></param>
        /// <returns></returns>
        private List<MailHeaderAndBody> CmdReceiveA(bool isID, IMAP_SequenceSet seqSet, MailHeaderAndBody mail)
        {
            List<MailHeaderAndBody> TempList=new List<MailHeaderAndBody>();

            IMAP_Client_FetchHandler handler = new IMAP_Client_FetchHandler();
            handler.NextMessage += delegate(object s, EventArgs e)
            {
                mail = new MailHeaderAndBody();
            };
            handler.InternalDate += delegate(object s, EventArgs<DateTime> e)
            {
                try
                {
                    mail.MyDate = e.Value.ToString();
                }
                catch (Exception exception)
                {

                }
            };
            handler.Rfc822Size += delegate(object s, EventArgs<int> e)
            {
                try
                {
                    mail.Size = e.Value.ToString(CultureInfo.CurrentCulture);
                }
                catch (Exception exception)
                {

                }
            };
            handler.UID += delegate(object s, EventArgs<long> e)
            {
                try
                {
                    mail.Uid = e.Value.ToString(CultureInfo.CurrentCulture);
                    if (!TempList.Contains(mail))
                    {
                        TempList.Add(mail);
                    }
                }
                catch (Exception exception)
                {

                }
            };
            handler.Rfc822Header += delegate(object sender, EventArgs<string> e)
            {
                Exception exception;
                try
                {
                    string s = e.Value;
                    FileEncrypt instance = null;
                    if (s != null)
                    {
                        s = s.Replace("?MS950?", "?big5?");
                        Mail_Message message = Mail_Message.ParseFromByte(Encoding.UTF8.GetBytes(s));
                        string originSubject = message.Subject;
                        mail.IsAuditEmail = PublicMethod.ModifyAuditMailSubject(ref originSubject);
                        mail.Sub = originSubject;
                        mail.Sub = PublicMethod.RemoveAtEnd(mail.Sub, "\0");
                        if (message.Sender != null)
                        {
                            mail.From = message.Sender.Address;
                            mail.OriginSender = mail.From;
                            mail.FromDisplayName = message.Sender.DisplayName;
                        }
                        if ((message.From != null) && (message.From.Count > 0))
                        {
                            mail.OriginSender = message.From[0].Address;
                            if (string.IsNullOrEmpty(mail.From))
                            {
                                mail.From = message.From[0].Address;
                                mail.FromDisplayName = message.From[0].DisplayName;
                            }
                        }
                        MIME_h[] _hArray = message.Header.ToArray();
                        if (_hArray != null)
                        {
                            foreach (MIME_h _h in _hArray)
                            {
                                MIME_Encoding_EncodedWord word;
                                if (((_h.Name == "ImportantFlag") || (_h.Name == "Importance")) && !mail.State.Contains("Important"))
                                {
                                    mail.State.Add("Important");
                                }
                                if (((_h.Name == "AttachmentFlag") && string.Equals(_h.ValueToString().Trim(), "Attached", StringComparison.OrdinalIgnoreCase)) && !mail.State.Contains("Attached"))
                                {
                                    mail.State.Add("Attached");
                                }
                                if ((_h.Name == "Content-Type") && _h.ValueToString().Trim().Contains("text/calendar"))
                                {
                                    mail.MailType = "Calendar";
                                }
                                else
                                {
                                    mail.MailType = "Mail";
                                }
                                if (((_h.Name == "Sensitivity") && "Private".Equals(_h.ValueToString().Trim())) && !mail.State.Contains("NoForward"))
                                {
                                    mail.State.Add("NoForward");
                                }
                                if (_h.Name == "StartTime")
                                {
                                    mail.State.Add(_h.ValueToString().Trim());
                                    mail.MeetingStartTime = _h.ValueToString().Trim();
                                    mail.MailType = "Calendar";
                                }
                                if (_h.Name == "EndTime")
                                {
                                    mail.State.Add(_h.ValueToString().Trim());
                                    mail.MeetingEndTime = _h.ValueToString().Trim();
                                }
                                if (_h.Name == "Address")
                                {
                                    mail.State.Add(_h.ValueToString().Trim());
                                    try
                                    {
                                        byte[] bytes = Convert.FromBase64String(_h.ValueToString().Trim());
                                        mail.MeetingLocation = Encoding.UTF8.GetString(bytes);
                                    }
                                    catch
                                    {
                                        mail.MeetingLocation = _h.ValueToString().Trim();
                                    }
                                }
                                if (_h.Name == "LocalMeetingSendDate")
                                {
                                    mail.LocalMeetingSentDate = _h.ValueToString().Trim();
                                }
                                if ((_h.Name == "X-Notes-Item") && _h.ValueToString().Trim().Contains("name=$NoPurge"))
                                {
                                    mail.MailType = "Calendar";
                                }
                                if (_h.Name == "Status")
                                {
                                    mail.State.Add(_h.ValueToString().Trim());
                                    mail.MeetingStatus = _h.ValueToString().Trim();
                                }
                                if (_h.Name == "MailClassification")
                                {
                                    mail.State.Add(_h.ValueToString().Trim());
                                    mail.MailClassification = _h.ValueToString().Trim();
                                }
                                if ((_h.Name != null) && (_h.Name.ToUpperInvariant() == "DATE"))
                                {
                                    string str5 = "";
                                    try
                                    {
                                        str5 = _h.ValueToString().Trim();
                                        mail.LetterMailCreatedTime = Convert.ToDateTime(str5).ToString();
                                    }
                                    catch (Exception exception1)
                                    {
                                        bool flag;
                                        exception = exception1;
                                        string str6 = PublicMethod.UTCCST2LocalDatetime(str5, out flag);
                                        if (flag)
                                        {
                                            mail.LetterMailCreatedTime = str6;
                                        }
                                    }
                                }
                                instance = FileEncrypt.GetInstance();
                                if (instance.EncryptCode("BanDownReadTag").Equals(_h.Name))
                                {
                                    if (instance.EncryptCode("1").Equals(_h.ValueToString().Trim()))
                                    {
                                        mail.BanDownRead = instance.EncryptCode("1");
                                    }
                                    else
                                    {
                                        mail.BanDownRead = instance.EncryptCode("0");
                                    }
                                }
                                if (string.Equals("MoodMark", _h.Name))
                                {
                                    mail.MoodMark = _h.ValueToString().Trim();
                                }
                                if (string.Equals("letterPaperLetterName", _h.Name))
                                {
                                    string text = _h.ValueToString().Trim();
                                    try
                                    {
                                        word = new MIME_Encoding_EncodedWord(MIME_EncodedWordEncoding.B, Encoding.UTF8);
                                        text = word.Decode(text);
                                        mail.LetterMailLetterName = text;
                                    }
                                    catch
                                    {
                                    }
                                }
                                if (string.Equals("CalendarItemType", _h.Name))
                                {
                                    mail.CalendarItemType = _h.ValueToString().Trim();
                                }
                                if (string.Equals("CalendarItemAddress", _h.Name))
                                {
                                    string str8 = _h.ValueToString().Trim();
                                    try
                                    {
                                        word = new MIME_Encoding_EncodedWord(MIME_EncodedWordEncoding.B, Encoding.UTF8);
                                        str8 = word.Decode(str8);
                                        mail.CalendarItemAddress = str8;
                                    }
                                    catch
                                    {
                                    }
                                }
                                if (string.Equals("CalendarItemStartTime", _h.Name))
                                {
                                    string str9 = _h.ValueToString().Trim();
                                    mail.CalendarItemStartTime = str9;
                                    try
                                    {
                                        word = new MIME_Encoding_EncodedWord(MIME_EncodedWordEncoding.B, Encoding.UTF8);
                                        str9 = word.Decode(str9);
                                        mail.CalendarItemStartTime = str9;
                                    }
                                    catch
                                    {
                                    }
                                }
                                if (string.Equals("CalendarItemEndTime", _h.Name))
                                {
                                    string str10 = _h.ValueToString().Trim();
                                    mail.CalendarItemEndTime = str10;
                                    try
                                    {
                                        word = new MIME_Encoding_EncodedWord(MIME_EncodedWordEncoding.B, Encoding.UTF8);
                                        str10 = word.Decode(str10);
                                        mail.CalendarItemEndTime = str10;
                                    }
                                    catch
                                    {
                                    }
                                }
                                if (string.Equals("CalendarItemRemindTime", _h.Name))
                                {
                                    mail.CalendarItemRemindTime = _h.ValueToString().Trim();
                                    string str11 = _h.ValueToString().Trim();
                                    try
                                    {
                                        str11 = new MIME_Encoding_EncodedWord(MIME_EncodedWordEncoding.B, Encoding.UTF8).Decode(str11);
                                        mail.CalendarItemRemindTime = str11;
                                    }
                                    catch
                                    {
                                    }
                                }
                                if (string.Equals("CalendarItemRemindDesc", _h.Name))
                                {
                                    mail.CalendarItemRemindDesc = _h.ValueToString().Trim();
                                    string str12 = _h.ValueToString().Trim();
                                    try
                                    {
                                        str12 = new MIME_Encoding_EncodedWord(MIME_EncodedWordEncoding.B, Encoding.UTF8).Decode(str12);
                                        mail.CalendarItemRemindDesc = str12;
                                    }
                                    catch
                                    {
                                    }
                                }
                            }
                        }
                        if (!string.IsNullOrEmpty(s))
                        {
                            instance = FileEncrypt.GetInstance();
                            int startIndex = s.IndexOf(instance.EncryptCode("EncryptHelperTag") + ":", StringComparison.Ordinal);
                            try
                            {
                                if (startIndex > 0)
                                {
                                    string[] strArray = s.Substring(startIndex, 0x42).Split(new char[] { ':' });
                                    if (!string.IsNullOrEmpty(strArray[0]))
                                    {
                                        mail.Encrypt = strArray[1].Trim();
                                    }
                                }
                            }
                            catch (Exception exception2)
                            {
                                exception = exception2;

                            }
                        }
                    }
                }
                catch (Exception exception3)
                {
                    exception = exception3;

                }
            };
            handler.Flags += delegate(object sender, EventArgs<string[]> e)
            {
                try
                {
                    mail.State.Clear();
                    if ((e.Value != null) && (e.Value.Count<string>() > 0))
                    {
                        mail.State.AddRange(e.Value);
                    }
                }
                catch (Exception exception)
                {

                }
            };
            handler.Envelope += delegate(object s, EventArgs<IMAP_Envelope> e)
            {
                try
                {
                    IMAP_Envelope envelope = e.Value;
                    string str = "";
                    if (string.IsNullOrEmpty(str))
                    {
                    }
                    string address = "";
                    address = getAddress(envelope.Cc, address);
                    string str3 = "";
                    str3 = getAddress(envelope.To, str3);
                    string str4 = "";
                    str4 = getAddress(envelope.Bcc, str4);
                    FullInfo(mail, envelope, str, address, str3, str4);
                }
                catch (Exception exception)
                {

                }
            };
            IMAP_Fetch_DataItem[] items = new IMAP_Fetch_DataItem[] { new IMAP_Fetch_DataItem_Envelope(), new IMAP_Fetch_DataItem_InternalDate(), new IMAP_Fetch_DataItem_Rfc822Size(), new IMAP_Fetch_DataItem_Flags(), new IMAP_Fetch_DataItem_Uid(), new IMAP_Fetch_DataItem_Rfc822Header() };
            try
            {
                this.imap.Fetch(isID, seqSet, items, handler);
            }
            catch (IOException exception)
            {
                throw new Exception(exception.Message);
            }

            finally
            {
                this.CmdReceiveBodyStruct(isID, seqSet);
            }
            return TempList;
        }
        private void FullInfo(MailHeaderAndBody mail, IMAP_Envelope envelope, string from, string cc, string to, string bcc)
        {
            string str = to;
            string str2 = cc;
            string str3 = bcc;
            //if (!string.IsNullOrEmpty(str))
            //{
            //    str = PublicMethod.ResumeExtraAddOrReplaceContentOfLocalGrpName(str);
            //}
            //if (!string.IsNullOrEmpty(str2))
            //{
            //    str2 = PublicMethod.ResumeExtraAddOrReplaceContentOfLocalGrpName(str2);
            //}
            //if (!string.IsNullOrEmpty(str3))
            //{
            //    str3 = PublicMethod.ResumeExtraAddOrReplaceContentOfLocalGrpName(str3);
            //}
            mail.Received = str;
            mail.CC = str2;
            mail.Bcc = str3;
        }

        private static string getAddress(Mail_t_Address[] addressList, string address)
        {
            if (addressList != null)
            {
                for (int i = 0; i < addressList.Length; i++)
                {
                    if (i == (addressList.Length - 1))
                    {
                        address = address + addressList[i].ToString();
                    }
                    else
                    {
                        address = address + addressList[i].ToString() + ";";
                    }
                }
            }
            return address;
        }

        private void CmdReceiveBodyStruct(bool isID, IMAP_SequenceSet seqSet)
        {
            EventHandler<EventArgs<IMAP_r_u>> handler = new EventHandler<EventArgs<IMAP_r_u>>(this.struc);
            IMAP_t_Fetch_i[] _iArray = new IMAP_t_Fetch_i[] { new IMAP_t_Fetch_i_Body() };
            try
            {
                IMAP_t_SeqSet set = IMAP_t_SeqSet.Parse(seqSet.ToSequenceSetString());
            }
            catch (Exception exception)
            {
                //Log4Net.Log.Error(base.GetType() + exception.Message + exception.StackTrace);
                //StaticVariable.logOnInfo.Disconnect();
                //StaticVariable.logOnInfo.Connect();
                //StaticVariable.logOnInfo.Login();
                throw new Exception(exception.Message);
            }
        }

        private void struc(object s, EventArgs<IMAP_r_u> u)
        {
            if (u.Value is IMAP_r_u_Fetch)
            {
                IMAP_r_u_Fetch fetch = (IMAP_r_u_Fetch)u.Value;
                int num = 0;
                num++;
            }
        }

        public string GetWebSer(string A_0, ref string A_1)
        {
            string text = string.Empty;
            if (string.IsNullOrEmpty(A_1))
            {
                A_1 = Guid.NewGuid().ToString();
            }
            //text = this.ar("mid") + A_1;
            if (!Directory.Exists(text))
            {
                Directory.CreateDirectory(text);
            }
            return text + "\\" + A_0;
        }

        public bool CmdConnect(string serverIP, int serverPort)
        {
            try
            {
              
                this.imap.Connect(serverIP, serverPort, true);
                return true;
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
            return false;
        }

        public void CmdDisconnect()
        {
            this.imap.Disconnect();
        }

        public bool CmdLogOn(string userName, string passWord)
        {
            try
            {
                if (!this.imap.IsAuthenticated)
                {
                    this.imap.Login(userName, passWord);
                    return true;
                }
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
            return false;
        }

        public bool CmdSelectFolder(string folderFullPath)
        {
            bool flag = false;
            try
            {
                try
                {
                    this.imap.SelectFolder(folderFullPath);
                    flag = true;
                }
                catch (Exception exception)
                {
                    throw new Exception(exception.Message);
                }
            }
            finally
            {
            }
            return flag;
        }

        public string XMLB(string A_0, string A_1)
        {
            string result = "";
            try
            {
                XmlDocument xmlDocument = new XmlDocument();
                xmlDocument.Load(A_0);
                XmlNodeList xmlNodeList = xmlDocument.SelectNodes(A_1);
                foreach (XmlElement xmlElement in xmlNodeList)
                {
                    if (xmlElement.Attributes["Selected"].Value.Equals("1"))
                    {
                        result = xmlElement.Attributes["ServerID"].Value;
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                //d6.a().Error(ex.Message + ex.StackTrace);
                //ck.h();
            }
            return result;
        }

        public string XMLA(string A_0, string A_1)
        {
            string result = string.Empty;
            string filename = AppDomain.CurrentDomain.BaseDirectory + "XML\\SystemSet\\ServerAddress.xml";
            string text;
            if (string.IsNullOrEmpty(A_0))
            {
                text = this.XMLB(AppDomain.CurrentDomain.BaseDirectory + "XML\\SystemSet\\FactoryAddress.xml", "root/Factory");
            }
            else
            {
                text = A_0;
            }
            try
            {
                XmlDocument xmlDocument = new XmlDocument();
                xmlDocument.Load(filename);
                XmlNodeList xmlNodeList = xmlDocument.SelectNodes("Server/ServerPath");
                foreach (XmlElement xmlElement in xmlNodeList)
                {
                    if (text.Equals(xmlElement.Attributes["ServerID"].Value))
                    {
                        result = string.Concat(new string[]
					{
						xmlElement.Attributes["WebType"].Value,
						"://",
						xmlElement.Attributes["WebService"].Value,
						"/superNotesWS/",
						A_1
					});
                        this.c = xmlElement.Attributes["WebService"].Value;
                        break;
                    }
                    if (text.Length > 10)
                    {
                        result = string.Concat(new string[]
					{
						xmlElement.Attributes["WebType"].Value,
						"://",
						A_0,
						"/superNotesWS/",
						A_1
					});
                        this.d = A_0;
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                //d6.a().Error(ex.Message + ex.StackTrace);
                throw new Exception(ex.Message.ToString());
            }
            return result;
        }

        public bool TestURL(string A_0)
        {
            bool result;
            try
            {
                HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(A_0);
                httpWebRequest.Proxy = GlobalProxySelection.GetEmptyWebProxy();
                using ((HttpWebResponse)httpWebRequest.GetResponse())
                {
                    result = true;
                    return result;
                }
            }
            catch (Exception ex)
            {
                //d6.a().Error(ex.Message + ex.StackTrace);
            }
            result = false;
            return result;
        }

       
        private void btnMyTest_Click(object sender, EventArgs e)
        {
             //FileStream log = new FileStream(@"C:\log.txt", FileMode.OpenOrCreate, FileAccess.ReadWrite);
            //StreamWriter sw = new StreamWriter(log);
          
            //int iCount = client.SelectedFolder.MessagesCount;
            arrMyMail.Clear();

            Thread mytrd = new Thread(new ParameterizedThreadStart(TestGetMail));
            mytrd.Start("1:200");
          

           Thread mytrdA = new Thread(new ParameterizedThreadStart(TestGetMail));
            mytrdA.Start("201:250");
            
            /*
            Thread mytrdB= new Thread(new ParameterizedThreadStart(TestGetMail));
            mytrdB.Start("101:150");

            Thread mytrdC = new Thread(new ParameterizedThreadStart(TestGetMail));
            mytrdC.Start("151:200");*/

            Thread tt = new Thread(new ParameterizedThreadStart(TestGetMail));
            tt.Start("251:364");
           
            while (true)
            {
                if(!mytrd.IsAlive)
                    if (!mytrdA.IsAlive)
                        if (!tt.IsAlive)
                        {
                            this.dataGridView1.DataSource = arrMyMail;
                            this.lblTotalCnt.Text = arrMyMail.Count.ToString();
                            break;
                        }
            }

        }

        ArrayList arrMyMail = new ArrayList();
        private object objMailLogck = new object();
        private void TestGetMail(object a)
        {
            
            try
            {
                IMAP_Client client = new IMAP_Client();
                //连接邮件服务器通过传入邮件服务器地址和用于IMAP协议的端口号
                if (!client.IsConnected)
                {
                    client.Connect("chnlh07.efoxconn.com", 993, true);
                    client.Login("rain.yy.liu@mail.foxconn.com", "Rain1436");
                }
                client.SelectFolder("INBOX");

                IMAP_Client_FetchHandler fetchHandler = new IMAP_Client_FetchHandler();
               /* fetchHandler.Rfc822 += new EventHandler<IMAP_Client_Fetch_Rfc822_EArgs>(delegate(object s, IMAP_Client_Fetch_Rfc822_EArgs ea)
                {
                    MemoryStream storeStream = new MemoryStream();
                    ea.Stream = storeStream;
                    ea.StoringCompleted += new EventHandler(delegate(object s1, EventArgs e1)
                    {
                        storeStream.Position = 0;
                        Mail_Message mime = Mail_Message.ParseFromStream(storeStream);
                        //sw.WriteLine(mime.BodyText);
                        lock (objMailLogck)
                        {
                            arrMyMail.Add(mime);
                        }
                    });
                });
                */
                fetchHandler.Rfc822Size += new EventHandler<EventArgs<int>>(delegate(object s, EventArgs<int> ea)
                {
                    string iSize = ea.Value.ToString();
                  
                        arrMyMail.Add(iSize);
                  
                });

                //获取邮件
                IMAP_SequenceSet seqSet = new IMAP_SequenceSet();
                string target = Convert.ToString(a);
                seqSet.Parse(target);
               
                  IMAP_r_u_List[] list = client.GetFolders(null);
                  foreach (IMAP_r_u_List l in list)
                  {
                      client.SelectFolder(l.FolderName);

                      client.Fetch(
                              false,
                              seqSet,
                              new IMAP_Fetch_DataItem[]{
                                  new IMAP_Fetch_DataItem_Rfc822(),
                                   new IMAP_Fetch_DataItem_Uid()
                                              },
                              fetchHandler
                          );
                  }
               
                /*
                client.Fetch(
                        false,
                        seqSet,
                        new IMAP_Fetch_DataItem[]{
                               // new IMAP_Fetch_DataItem_Rfc822(),
                                new IMAP_Fetch_DataItem_Rfc822Size()
                                            },
                        fetchHandler
                    );*/


            }
            catch
            {

            }
            finally
            {
                // sw.Flush();
                // sw.Close();
                //log.Close();
                //this.dataGridView1.DataSource = arrMyMail;
            }
        }


        private void TestGetMailA(object a)
        {

            try
            {
                IMAP_Client client = new IMAP_Client();
                //连接邮件服务器通过传入邮件服务器地址和用于IMAP协议的端口号
                if (!client.IsConnected)
                {
                    client.Connect("chnlh07.efoxconn.com", 993, true);
                    client.Login("rain.yy.liu@mail.foxconn.com", "Rain1432");
                }
                client.SelectFolder("INBOX");

                IMAP_Client_FetchHandler fetchHandler = new IMAP_Client_FetchHandler();
                /* fetchHandler.Rfc822 += new EventHandler<IMAP_Client_Fetch_Rfc822_EArgs>(delegate(object s, IMAP_Client_Fetch_Rfc822_EArgs ea)
                 {
                     MemoryStream storeStream = new MemoryStream();
                     ea.Stream = storeStream;
                     ea.StoringCompleted += new EventHandler(delegate(object s1, EventArgs e1)
                     {
                         storeStream.Position = 0;
                         Mail_Message mime = Mail_Message.ParseFromStream(storeStream);
                         //sw.WriteLine(mime.BodyText);
                         lock (objMailLogck)
                         {
                             arrMyMail.Add(mime);
                         }
                     });
                 });
                 */
                fetchHandler.Rfc822Size += new EventHandler<EventArgs<int>>(delegate(object s, EventArgs<int> ea)
                {
                    string iSize = ea.Value.ToString();

                    arrMyMail.Add(iSize);

                });

                //获取邮件
               var set= IMAP_t_SeqSet.Parse(a.ToString());

               EventHandler<EventArgs<IMAP_r_u>> lumisoftHandler=new EventHandler<EventArgs<IMAP_r_u>>(fetchcallback);
              // client.FetchAsync();
                    

                /*client.Fetch(
                        false,
                        seqSet,
                        new IMAP_Fetch_DataItem[]{
                               // new IMAP_Fetch_DataItem_Rfc822(),
                                new IMAP_Fetch_DataItem_Rfc822Size()
                                            },
                        fetchHandler
                    );*/


            }
            catch
            {

            }
            finally
            {
                // sw.Flush();
                // sw.Close();
                //log.Close();
                //this.dataGridView1.DataSource = arrMyMail;
            }
        }
        private void fetchcallback(object sender, EventArgs<IMAP_r_u> e)
        { 
        
        }
            
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

      

    }
}
