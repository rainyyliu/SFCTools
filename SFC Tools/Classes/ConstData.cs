namespace SFC_Tools.Classes
{
   // using SuperNotes;
    using System;
    using System.Collections.Generic;
    using System.Runtime.InteropServices;
    using System.Windows.Forms;

    public class ConstData
    {
        //public static string AllowTransferSign = Resources.data_forbitForward;
        public const string AllowTransferSignName = "Sensitivity";
        public const string AllowTransferSignValue = "Private";
        public static readonly string ApplicationPath = Application.StartupPath;
        public const string ApproveFlagSplitInBody = "{~#approve#~}";
        public const string AttachmentSignName = "AttachmentFlag";
        public const string AttachmentSignValue = "Attached";
       // public static string Confidentiality = Resources.sendMail_confidentiality;
        public const string ConfidentialitySignName = "ConfidentialityFlag";
        public static readonly string CONST_GUID = "A1137251-0D2D-4B25-83FA-861B9E8D0207";
        public const int DefaultLblNameMaxLenWithChineseWords = 0x11;
        public const int DefaultLblNameMaxLenWithoutChineseWords = 30;
        public const string DefaultPassword = "supernotes";
        public static readonly string FlashPrinterPath = (ToolsPath + @"\FlashPaper2.2");
        //public static string HighImportant = Resources.data_veryImportant;
        public static string htmlDefault = "<html><body></body></html>";
        public const string ImportantSignName = "ImportantFlag";
        public const string InnerAddress = "@mail.foxconn.com";
        public const bool IsRmsRight = false;
        public const char KeywordsSplit = ',';
        public static readonly string LONGHUAFTP = "10.134.35.239:5978";
        public static readonly string LONGHUAFTPPASSWORD = "PsQlUSEjWsMaXo`u";
        public static readonly string LONGHUAFTPUSER = "XsQlUSEjWsMaXl";
        public const int MaxLimitSaveCount = 3;
        public const string MeetingAddressHeaderName = "Address";
        public const string MeetingEndTimeHeaderName = "EndTime";
        public const string MeetingNoNeedAlamrState = "NoNeedAlarm";
        public const string MeetingStartTimeHeaderName = "StartTime";
        public const string MeetingStatusHeaderName = "Status";
        public const string MoodSignName = "MoodMark";
        //public static string MoreConfidentiality = Resources.sendMail_moreConfidentiality;
        //public static string MorePriority = Resources.sendMail_urgeFile;
        //public static string MostConfidentiality = Resources.sendMail_mostConfidentiality;
        //public static string MostPriority = Resources.sendMail_mostUrgeFile;
        public const string NotesAddress = "@foxconn.com";
        public const string NotificationSignName = "ReturnReceipt";
        public const string NotificationSignValue = "1";
        public const string NotSetImapServer = "N";
        public static string picPathAsInsertPicFailedInWbbs = (AppDomain.CurrentDomain.BaseDirectory + "picCantShow_icon.GIF");
        public const string PrioritySignName = "PriorityFlag";
        public const string RuleDealed = "RuleDealed";
        public static string RuleXmlFileName = "NewMailRule.xml";
        public const string SecurityAdvocacyInChinese = "本電子郵件及附件所載信息均為保密信息，受合同保護或依法不得洩漏。其內容僅供指定收件人按限定範圍或特殊目的使用。未經授權者收到此信息均無權閱讀、 使用、 複製、洩漏或散佈。若您因為誤傳而收到本郵件或者非本郵件之指定收件人，請即刻回覆郵件或致電Super Notes郵件客服熱線 560-104，並永久刪除此郵件及其附件和銷毀所有複印件。謝謝您的合作！";
        public const string SecurityAdvocacyInEnglish = "This e-mail message together with any attachments thereto (if any) is confidential, protected under an enforceable non-disclosure agreement, intended only for the use of the named recipient(s) above and may contain information that is privileged, belonging to professional work products or exempt from disclosure under applicable laws.Any unauthorized review, use, copying, disclosure, or distribution of any information contained in or attached to this transmission is STRICTLY PROHIBITED and may be against the laws. If you have received this message in error, or are not the named recipient(s), please immediately notify the sender by e-mail or telephone at Super Notes support hotline 560-104 and delete this e-mail message and any attached documentation from your computer. Receipt by anyone other than the intended recipient(s) is not a waiver of any attorney-client or work product privilege. Thank you!";
        public const string SelectedFactoryStatusValue = "1";
        //public static string SendFailed = Resources.sendMailFailed;
        public const string SystemVersonWin2k = "50";
        public static readonly string ToolsPath = (ApplicationPath + @"\tool");
        public const string XMailer = "X-Mailer";
        public static string xmlRootPath = (AppDomain.CurrentDomain.BaseDirectory + @"XML\");

        [StructLayout(LayoutKind.Sequential)]
        public struct Actions
        {
            private string actionJudge;
            private string ationType;
            private string actionValue;
            public string ActionJudge
            {
                get
                {
                    return this.actionJudge;
                }
                set
                {
                    this.actionJudge = value;
                }
            }
            public string ActionType
            {
                get
                {
                    return this.ationType;
                }
                set
                {
                    this.ationType = value;
                }
            }
            public string ActionValue
            {
                get
                {
                    return this.actionValue;
                }
                set
                {
                    this.actionValue = value;
                }
            }
        }

        [StructLayout(LayoutKind.Sequential, Size=1)]
        public struct AddressListSearchResultType
        {
            public const string LocalAddressListFriend = "LocalFriend";
            public const string LocalAddressListGroup = "LocalGroup";
            public const string LDAPAddressListFriend = "LDAPFriend";
            public const string LDAPAddressListGroup = "LDAPGroup";
        }

        [StructLayout(LayoutKind.Sequential, Size=1)]
        public struct AgentSettingWebServiceRespose
        {
            public const int SetAgentSuccess = 0;
            public const int SetAgentFail = 1;
            public const int SetAgentNotApprover = 2;
            public const int SetAgentWrongFormat = 3;
        }

        [StructLayout(LayoutKind.Sequential, Size=1)]
        public struct AppDataPath
        {
            public const string Mid = "mid";
            public const string Log = "log";
            public const string Address = "address";
            public const string MeetingAddress = @"address\calendar\meeting";
            public const string MeetingAddressXml = @"address\calendar";
            public const string Rule = "rule";
            public const string Sign = "sign";
            public const string Language = "language";
            public const string Podn = "POFLAG";
            public const string Timeslip = "TIMESLIPFLAG";
            public const string SystemUI = "SystemUI";
            public const string SysAttment = "AttPath";
            public const string Select = "Select";
            public const string Archive = " Archive";
            public const string SecurityInfor = "SecuritySet";
        }

        [StructLayout(LayoutKind.Sequential, Size=1)]
        public struct ApprovalWebsvsCommands
        {
            public const string SendCommand = "send";
            public const string RejectCommand = "reject";
            public const string CCCommand = "cc";
            public const string ForwardCommand = "forward";
        }

        [StructLayout(LayoutKind.Sequential, Size=1)]
        public struct AttachmentSize
        {
            public const string Max = "5";
        }

        [StructLayout(LayoutKind.Sequential, Size=1)]
        public struct BanDownRead
        {
            public const string BanDownReadTag = "BanDownReadTag";
            public const string EnableBan = "0";
            public const string DisableBan = "1";
            public const string Enable = "BanDownRead";
        }

        [StructLayout(LayoutKind.Sequential, Size=1)]
        public struct CalendarArrangementsNeedHeader
        {
            public const string ItemTypeHeaderName = "CalendarItemType";
            public const string ItemAddressHeaderName = "CalendarItemAddress";
            public const string ItemStartTimeHeaderName = "CalendarItemStartTime";
            public const string ItemEndTimeHeaderName = "CalendarItemEndTime";
            public const string ItemRemindMeHeaderName = "CalendarItemRemindMe";
            public const string ItemRemindTimeHeaderName = "CalendarItemRemindTime";
            public const string ItemRemindDescHeaderName = "CalendarItemRemindDesc";
            public const string ItemBelongedMonthKeyForSearch = "MonthKeyOfCalendarItem";
        }

        [StructLayout(LayoutKind.Sequential, Size=1)]
        public struct CalendarDefaultFoldersNameInServer
        {
            public const string CalendarFolderName = "Calendar";
            public const string MeetingFolderNameOfCalendar = "Calendar.meetings";
            public const string OtherItemsFolderNameOfCalendar = "Calendar.items";
            public const string TrashFolderNameOfCalendar = "Calendar.trash";
        }

        [StructLayout(LayoutKind.Sequential, Size=1)]
        public struct CalendarType
        {
            public const string Appointment = "Appointment";
            public const string Anniversary = "Anniversary";
            public const string Memo = "Memo";
            public const string Event = "Event";
            public const string Meeting = "Meeting";
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct Conditions
        {
            private string judge;
            private string type;
            private string filter;
            private string value;
            public string Judge
            {
                get
                {
                    return this.judge;
                }
                set
                {
                    this.judge = value;
                }
            }
            public string Type
            {
                get
                {
                    return this.type;
                }
                set
                {
                    this.type = value;
                }
            }
            public string Filter
            {
                get
                {
                    return this.filter;
                }
                set
                {
                    this.filter = value;
                }
            }
            public string Value
            {
                get
                {
                    return this.value;
                }
                set
                {
                    this.value = value;
                }
            }
        }

        [StructLayout(LayoutKind.Sequential, Size=1)]
        public struct CurrentCultureInfo
        {
            public const string ChineseTW = "zh-TW";
            public const string English = "en-US";
            public const string ChineseCN = "zh-CN";
        }

        [StructLayout(LayoutKind.Sequential, Size=1)]
        public struct CustomMailSigns
        {
            public const string Important = "Important";
            public const string VeryUrgent = "VeryUrgent";
            public const string Urgent = "Urgent";
            public const string Confidential = "Confidential";
            public const string More = "MoreConfidential";
            public const string Most = "MostConfidential";
            public const string Attached = "Attached";
            public const string NoForward = "NoForward";
            public const string Noticed = "Noticed";
            public const string NoDownRead = "NoDownRead";
        }

        [StructLayout(LayoutKind.Sequential, Size=1)]
        public struct DefaultDir
        {
            public const string MailBoxId = "1";
            public const string MailBoxTextInServer = "INBOX";
            public const string DraftId = "2";
            public const string DraftTextInServer = "Drafts";
            public const string SendId = "3";
            public const string SendTextInServer = "Sent";
            public const string BinId = "4";
            public const string BinTextInServer = "Trash";
            public const string ApprovedFolderId = "5";
            public const string ApprovedFolderTxtInServer = "Approved";
            public const string RefusedFolderId = "6";
            public const string RefusedFolderTxtInServer = "Refused";
            public const string RuleId = "7";
            public const string RuleTextInServer = "Rule";
            public const string ArchiveId = "8";
            public const string ArchiveTextInServer = "Archive";
            public const string WaitAuditId = "9";
            public const string WaitAuditInServer = "WaitAudit";
            public const string LetterPaperId = "10";
            public const string LetterPaperInServer = "LetterPaper";
            public const string ArchiveActualPath = "Archive.blackbox";
            public const string ArchiveSentActualPath = "Archive.blackbox.sentbox";
            public const string OldArchiveActualPath = "oldArchive.blackbox";
            public const string OldArchiveSentActualPath = "oldArchive.blackbox.sentbox";
            public const string CalendarActualPath = "calendar";
            public const string OldArchiveTextInServer = "oldArchive";
            public const string MailSeverAddress = "Postmaster@foxconn.com";
            public static string MailBoxText;
            public static string DraftText;
            public static string SendText;
            public static string BinText;
            public static string ApprovedFolderText;
            public static string RefusedFolderText;
            public static string RuleText;
            public static string ArchiveText;
            public static string WaitAuditText;
            public static string LetterPaperText;
            public static string AddNewFolderTreeRootText;
            static DefaultDir()
            {
                //MailBoxText = Resources.data_dirMailBox;
                //DraftText = Resources.data_dirDrafts;
                //SendText = Resources.data_dirSend;
                //BinText = Resources.data_dirBin;
                //ApprovedFolderText = Resources.data_dirApproved;
                //RefusedFolderText = Resources.data_dirRefused;
                //RuleText = Resources.data_dirRule;
                //ArchiveText = Resources.data_dirArchive;
                //WaitAuditText = Resources.data_dirWaitAudit;
                //LetterPaperText = Resources.data_dirLetterPaper;
                //AddNewFolderTreeRootText = Resources.AddNewFolderTreeRootText;
            }
        }

        [StructLayout(LayoutKind.Sequential, Size=1)]
        public struct DefaultInfoInAnyLanguage
        {
            public static List<string> listDefaultDirEnglish;
            public static List<string> listDefaultDirTraditionalChinese;
            public static List<string> listDefaultDirModernChinese;
            public static List<string> listDefaultAllowTransferSign;
            public static List<string> listDefaultSendedFailSign;
            static DefaultInfoInAnyLanguage()
            {
                listDefaultDirEnglish = new List<string> { "Inbox", "Drafts", "Sent", "Trash", "Approved", "Refused", "Accepted", "Rejected", "Rule", "Archive", "WaitAudit", "Pending", "LetterPaper" };
                listDefaultDirTraditionalChinese = new List<string> { "信箱", "草稿", "傳送", "垃圾桶", "已核準", "已退件", "規則", "Archive", "待審核", "信箋" };
                listDefaultDirModernChinese = new List<string> { "信箱", "草稿", "传送", "垃圾桶", "已核准", "已退件", "规则", "Archive", "待审核", "信笺" };
                listDefaultAllowTransferSign = new List<string> { "[禁止轉呈]", "[Forward prohibited]", "[禁止转寄]" };
                listDefaultSendedFailSign = new List<string> { "[傳送失敗]", "[Sending failed]", "[传送失败]" };
            }
        }

        [StructLayout(LayoutKind.Sequential, Size=1)]
        public struct DigitalSignInfor
        {
            public const string DigitalSignCertificatePublisher = "SuperNotesCA";
        }

        [StructLayout(LayoutKind.Sequential, Size=1)]
        public struct DirType
        {
            public const string MailBox = "mailbox";
            public const string Draft = "draft";
            public const string Send = "send";
            public const string Bin = "bin";
            public const string Rule = "rule";
            public const string Archive = "archive";
            public const string Letter = "letter";
        }

        [StructLayout(LayoutKind.Sequential, Size=1)]
        public struct EmailAuditingConstDefine
        {
            public const string EmailAuditingSign = "[Approve Required]";
            public const string EmailAuditSignReplaceValue = "<Approve Required>";
            public const string BGAuditingSign = "[BG審核]";
        }

        [StructLayout(LayoutKind.Sequential, Size=1)]
        public struct EncryptHelper
        {
            public const string EncryptHelperTag = "EncryptHelperTag";
            public const string EnableEncrypt = "110";
            public const string DisableEncrypt = "1";
        }

        [StructLayout(LayoutKind.Sequential, Size=1)]
        public struct ImportanceLevel
        {
            public static string ImportanceLow;
            public static string ImportanceNormal;
            public static string ImportanceHigh;
            static ImportanceLevel()
            {
                //ImportanceLow = Resources.data_low;
                //ImportanceNormal = Resources.data_normal;
                //ImportanceHigh = Resources.data_high;
            }
        }

        [StructLayout(LayoutKind.Sequential, Size=1)]
        public struct MailClassificationType
        {
            public const string MailClassification = "MailClassification";
            public const string Customer = "Customer";
            public const string Manufacturer = "Manufacturer";
            public const string Other = "Other";
            public const string Internal = "Internal";
            public const string Group = "Span Group";
            public const string Competent = "Competent";
            public const string ParallelUnit = "Parallel Unit";
            public const string Subordinate = "Subordinate";
        }

        [StructLayout(LayoutKind.Sequential, Size=1)]
        public struct MailConfidentialityLevel
        {
            public const string Normal = "Normal";
            public const string Confidential = "Confidential";
            public const string More = "MoreConfidential";
            public const string Most = "MostConfidential";
        }

        [StructLayout(LayoutKind.Sequential, Size=1)]
        public struct MailContentType
        {
            public const string Mail = "Mail";
            public const string Calendar = "Calendar";
        }

        [StructLayout(LayoutKind.Sequential, Size=1)]
        public struct MailHeaderExtraSign
        {
            public const string AuditMailOrgToHeaderSign = "savEnterSendTo";
            public const string AuditMailOrgCCHeaderSign = "savEnterCopyTo";
            public const string AuditMailOrgBccHeaderSign = "savEnterBlindTo";
            public const string AuditMailOrgSubjHeaderSign = "savSubject";
            public const string CctypeHeaderSignName = "CCType";
            public const string CCListHeaderSignName = "CCList";
            public const string CCTypeHeaderSignValue = "1";
            public const string BGApprover = "csnewbg";
            public const string IsInternetMail = "csflag";
            public const string IsInternetSenderWhiteList = "cswhite";
            public const string ApprovalBG = "csbg";
            public const string ApprovalBU = "csbu";
            public const string InternetApprover = "internetapprover";
            public const string InternetApproverInNotesBody = "csinternetapp";
            public const string LetterPaperLetterNameHeaderSign = "letterPaperLetterName";
            public const string ApprovalIntSecondApp = "intSecondApp";
            public const string HasWhiteKey = "CsWhiteKeyFlag";
            public const string WhiteKeyApprover = "whiteKeyApprover";
            public const string WhiteKeySecurityBcc = "whiteKeySecurityBcc";
            public const string DigitalSignaturePublicKey = "DigitalSignPubKey";
            public const string DigitalSignatureContent = "DigitalSignContent";
        }

        [StructLayout(LayoutKind.Sequential, Size=1)]
        public struct MailImportantLevel
        {
            public static string NormalDes;
            public static string HighDes;
            static MailImportantLevel()
            {
                NormalDes = "Normal";
                HighDes = "High";
            }
        }

        [StructLayout(LayoutKind.Sequential, Size=1)]
        public struct MailPriorityLevel
        {
            public const string VeryUrgent = "VeryUrgent";
            public const string Urgent = "Urgent";
            public const string Normal = "Normal";
        }

        [StructLayout(LayoutKind.Sequential, Size=1)]
        public struct MailRuleActions
        {
            public const string MoveInXml = "Move";
            public const string CopyInXml = "Copy";
            public const string DeleteInXml = "Delete";
            public static string MoveText;
            public static string CopyText;
            public static string DeleteText;
            static MailRuleActions()
            {
                MoveText = "移入資料夾";
                CopyText = "複製到資料夾";
                DeleteText = "刪除";
            }
        }

        [StructLayout(LayoutKind.Sequential, Size=1)]
        public struct MailRuleFilters
        {
            public const string ContainsInXml = "Contains";
            public const string NoContainsInXml = "NoContains";
            public const string IsInXml = "Is";
            public const string NotIsInXml = "NotIs";
            public const string GreaterThanInXml = "Greater Than";
            public const string LessThanInXml = "Less Than";
            public static string ContainsText;
            public static string NoContainsText;
            public static string IsText;
            public static string NotIsText;
            public static string GreaterThanText;
            public static string LessThanText;
            static MailRuleFilters()
            {
                ContainsText = "包含";
                NoContainsText = "不包含";
                IsText = "是";
                NotIsText = "不是";
                GreaterThanText = "大於";
                LessThanText = "小於";
            }
        }

        [StructLayout(LayoutKind.Sequential, Size=1)]
        public struct MailRuleJudge
        {
            public const string AndInXml = "And";
            public const string OrInXml = "Or";
            public static string AndText;
            public static string OrText;
            static MailRuleJudge()
            {
                AndText = "和";
                OrText = "或";
            }
        }

        [StructLayout(LayoutKind.Sequential, Size=1)]
        public struct MailRuleTypes
        {
            public const string FromInXml = "From";
            public const string SubjectInXml = "Sub";
            public const string SizeInXml = "Size";
            public const string CCInXml = "CC";
            public const string BccInXml = "Bcc";
            public const string ReceivedInXml = "Received";
            public const string ReceivedOrFromInXml = "ReceivedOrCc";
            public static string FromText;
            public static string SubjectText;
            public static string SizeText;
            public static string CCText;
            public static string BCCText;
            public static string ReceivedText;
            public static string ReceivedOrFromText;
            static MailRuleTypes()
            {
                FromText = "寄件人";
                SubjectText = "主旨";
                SizeText = "大小 ";
                CCText = "副本抄送";
                BCCText = "副本密送";
                ReceivedText = "收件人";
                ReceivedOrFromText = "收件人或副本抄送";
            }
        }

        public enum MailSaveType
        {
            Draft,
            Sended,
            InBox
        }

        [StructLayout(LayoutKind.Sequential, Size=1)]
        public struct MailSend
        {
            public const int NewMail = 1;
            public const int WriteBack = 2;
            public const int WriteBackWithContent = 3;
            public const int WriteBackWithOutFile = 4;
            public const int WriteAll = 5;
            public const int WriteAllWithContent = 6;
            public const int WriteAllWithOutFile = 7;
            public const int MailTranser = 8;
            public const int MailEdit = 9;
            public const int CopyToNew = 10;
            public const int MailView = 11;
            public const int MailViewPage = 12;
            public const int EditDraftMail = 13;
            public const int DoubleInEditStateMail = 14;
            public const int NewLetterPaper = 15;
            public const int EditLetterPaper = 0x10;
        }

        [StructLayout(LayoutKind.Sequential, Size=1)]
        public struct MailSendContentFormatSet
        {
            public const string TextFormatSetVal = "TEXT";
            public const string HtmlFormatSetVal = "HTML";
        }

        [StructLayout(LayoutKind.Sequential, Size=1)]
        public struct MailState
        {
            public const string Draft = @"\Draft";
        }

        [StructLayout(LayoutKind.Sequential, Size=1)]
        public struct MeetingOperationStatus
        {
            public const string NewMeeting = "0";
            public const string AcceptMeeting = "6";
            public const string RefuseMeeting = "7";
            public const string ViewMeeting = "3";
            public const string CancelMeeting = "8";
            public const string MeetingRequest = "REQUEST";
            public const string MeetingReply = "REPLY";
            public const string NewMeetingType = "NEEDS-ACTION";
            public const string MeetingAccepted = "ACCEPTED";
            public const string MeetingDeclined = "DECLINED";
            public const string MeetingTentative = "TENTATIVE";
            public const string MeetingDelegted = "DELEGATED";
            public const string MeetingCancelled = "Cancelled";
        }

        [StructLayout(LayoutKind.Sequential, Size=1)]
        public struct MeetingSaveToServerType
        {
            public const string Import = "Import";
            public const string Save = "Save";
        }

        [StructLayout(LayoutKind.Sequential, Size=1)]
        public struct MoodStamp
        {
            public static string MoodNormal;
            public static string MoodPeronal;
            public static string MoodConfidential;
            public static string MoodPrivate;
            public static string MoodThankYou;
            public static string MoodFlame;
            public static string MoodGoodJob;
            public static string MoodJoke;
            public static string MoodFYI;
            public static string MoodQuestion;
            public static string MoodReminder;
            static MoodStamp()
            {
                //MoodNormal = Resources.mood_Normal;
                //MoodPeronal = Resources.mood_Peronal;
                //MoodConfidential = Resources.mood_Confidential;
                //MoodPrivate = Resources.mood_Private;
                //MoodThankYou = Resources.mood_ThankYou;
                //MoodFlame = Resources.mood_Flame;
                //MoodGoodJob = Resources.mood_GoodJob;
                //MoodJoke = Resources.mood_Joke;
                //MoodFYI = Resources.mood_FYI;
                //MoodQuestion = Resources.mood_Question;
                //MoodReminder = Resources.mood_Reminder;
            }
        }

        [StructLayout(LayoutKind.Sequential, Size=1)]
        public struct NeedToGetNotesEmailAddr
        {
            public const string EmailAddrWith2At = "@@foxconn.com";
            public const string EmailAddrWith1At1Percent = "%@foxconn.com";
        }

        [StructLayout(LayoutKind.Sequential, Size=1)]
        public struct NewCalendarStatus
        {
            public const int NewCalendarModel = 9;
            public const int NewCalendarEditModel = 10;
            public const int NewCalendarOnlyReadModel = 11;
        }

        [StructLayout(LayoutKind.Sequential, Size=1)]
        public struct NewMailStatus
        {
            public const int NewMailEditModel = 10;
            public const int NewMailOnlyReadModel = 11;
        }

        [StructLayout(LayoutKind.Sequential, Size=1)]
        public struct NewMeeting
        {
            public const string StartTime = "StartTime";
            public const string EndTime = "EndTime";
            public const string Address = "Address";
            public const string IsNewMeeting = "IsNewMeeting";
            public const string Status = "Status";
            public const string XNotesItem = "X-Notes-Item";
            public const string LocalMeetingSendDate = "LocalMeetingSendDate";
        }

        [StructLayout(LayoutKind.Sequential, Size=1)]
        public struct NewMeetingStatus
        {
            public const int NewMeetingEditModel = 0;
            public const int NewMeetingReadOnlyModel = 1;
        }

        [StructLayout(LayoutKind.Sequential, Size=1)]
        public struct OutOfOfficePage
        {
            public static string LeaveOffice;
            public static string MailContent;
            public static string LeaveTimePeriod;
            public static string Enable;
            public static string Disable;
            public static string Confirmation;
            public static string ReturnWrongPrompt1;
            public static string ReturnWrongPrompt2;
            static OutOfOfficePage()
            {
                //LeaveOffice = Resources.OutOfOffice_Text;
                //MailContent = Resources.OutOfOffice_MailContent;
                //LeaveTimePeriod = Resources.OutOfOffice_LeaveTimePeriod;
                //Enable = Resources.OutOfOffiec_Enable;
                //Disable = Resources.OutOfOffice_Disable;
                //Confirmation = Resources.OutOfOffice_Confirmation;
                //ReturnWrongPrompt1 = Resources.OutOfOffice_ReturnWrongPrompt1;
                //ReturnWrongPrompt2 = Resources.OutOfOffice_ReturnWrongPrompt2;
            }
        }

        [StructLayout(LayoutKind.Sequential, Size=1)]
        public struct PersonalLoginInfo
        {
            public const string LoginInfoIP = "IP";
            public const string LoginName = "Name";
            public const string LoginDisplay = "display";
            public const string LoginSelectedFactoryId = "SelectedFactoryId";
        }

        [StructLayout(LayoutKind.Sequential, Size=1)]
        public struct ProjectName
        {
            public const string SN = "SuperNotes.exe";
            public const string UP = "AutoUpdSuper.exe";
            public const string UpPath = "AutoUpdSuper";
            public const string UpZip = "AutoUpdSuper.7z";
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct RemindMeetings
        {
            private string uid;
            private string startTime;
            private string endTime;
            private string subject;
            private string location;
            public string Uid
            {
                get
                {
                    return this.uid;
                }
                set
                {
                    this.uid = value;
                }
            }
            public string StartTime
            {
                get
                {
                    return this.startTime;
                }
                set
                {
                    this.startTime = value;
                }
            }
            public string EndTime
            {
                get
                {
                    return this.endTime;
                }
                set
                {
                    this.endTime = value;
                }
            }
            public string Subject
            {
                get
                {
                    return this.subject;
                }
                set
                {
                    this.subject = value;
                }
            }
            public string Location
            {
                get
                {
                    return this.location;
                }
                set
                {
                    this.location = value;
                }
            }
        }

        [StructLayout(LayoutKind.Sequential, Size=1)]
        public struct RuleElements
        {
            public const string RuleElementOfConditions = "Condition";
            public const string RuleElementOfExceptions = "Exception";
            public const string RuleElementOfActions = "Action";
        }

        [StructLayout(LayoutKind.Sequential, Size=1)]
        public struct RuleStatus
        {
            public const string RuleStatusOfOpen = "Yes";
            public const string RuleStatusOfNo = "No";
        }

        public enum SearchPageType
        {
            Address,
            MailBox
        }

        [StructLayout(LayoutKind.Sequential, Size=1)]
        public struct SendAudit
        {
            public const string Send = "0";
            public const string Auditpass = "1";
            public const string Auditrefuse = "2";
            public const string AuditNoticeback = "3";
            public const string AuditPassCCType = "4";
            public const string AuditSendToHigherAuditor = "5";
            public const string WhiteKeyAuditSend = "6";
        }

        [StructLayout(LayoutKind.Sequential, Size=1)]
        public struct ShowFileType
        {
            public const string Doc = ".doc";
            public const string Docx = ".docx";
            public const string Mpp = ".mpp";
            public const string Xls = ".xls";
            public const string Xlsx = ".xlsx";
            public const string Csv = ".csv";
            public const string Txt = ".txt";
            public const string Html = ".html";
            public const string Htm = ".htm";
            public const string Mht = ".mht";
            public const string CS = ".cs";
            public const string Ini = ".ini";
            public const string JS = ".js";
            public const string Css = ".css";
            public const string Ppt = ".ppt";
            public const string Pptx = ".pptx";
            public const string Pdf = ".pdf";
            public const string Zip = ".zip";
            public const string Z7Z = ".7z";
            public const string Rar = ".rar";
            public const string Iso = ".iso";
            public const string Bin = ".bin";
            public const string Img = ".img";
            public const string Tao = ".tao";
            public const string Dao = ".dao";
            public const string Cif = ".cif";
            public const string Fcd = ".fcd";
            public const string Exe = ".exe";
            public const string Dll = ".dll";
            public const string Rpm = ".rpm";
            public const string Tar = ".tar";
            public const string GZ = ".gz";
            public const string BZ2 = ".bz2";
            public const string Msg = ".msg";
            public const string Vsd = ".vsd";
            public const string Jpg = ".jpg";
            public const string Png = ".png";
            public const string Gif = ".gif";
            public const string Tif = ".tif";
            public const string Jpeg = ".jpeg";
            public const string Bmp = ".bmp";
            public const string Dwg = ".dwg";
            public const string Dxf = ".dxf";
            public const string Gtz = ".gtz";
            public const string Art = ".art";
            public const string Bak = ".bak";
            public const string Dwt = ".dwt";
            public const string Cad = ".cad";
            public const string Xml = ".xml";
            public const string PMimport = ".pmimport";
            public const string Tsv = ".tsv";
            public const string Sgn = ".sgn";
            public const string Wts = ".wts";
            public const string Pps = ".pps";
        }

        [StructLayout(LayoutKind.Sequential, Size=1)]
        public struct SystemLogOnState
        {
            public const int ExchangeIdSysState = 0;
            public const int HaveLogOnSysState = 1;
            public const int ExitSysState = 2;
        }

        [StructLayout(LayoutKind.Sequential, Size=1)]
        public struct TabPageTags
        {
            public const string NewMail = "tagNewMail";
            public const string WriteBack = "tagReply";
            public const string MailTranser = "tagForward";
            public const string MailEdit = "tagEdit";
            public const string MeetingView = "tagMeetingView";
            public const string MailView = "tagMailView";
            public const string MailBox = "tagInbox";
            public const string NewMeeting = "tagNewMeeting";
            public const string Calendar = "CalendarTabPage_tag";
            public const string Appoint = "tagNewAppointment";
            public const string Anniversary = "tagNewAnniversary";
            public const string Memo = "tagNewMemo";
            public const string Event = "tagEvent";
            public const string LetterPaperDraft = "letterPaperDraftTbpg_tag";
            public const string LetterPaper = "letterPaperTbpa_tag";
        }

        [StructLayout(LayoutKind.Sequential, Size=1)]
        public struct TabpageTagVal
        {
            public const string AddressListTabpageTagValue = "AddressListTabPage_tag";
            public const string AddNewFriendTbpgType = "AddNewFriend";
            public const string EditOldFriendTbpgType = "EditFriend";
            public const string AddNewGroupTbpgType = "AddNewGrp";
            public const string EditOldGroupTbpgType = "EditGrp";
        }

        [StructLayout(LayoutKind.Sequential, Size=1)]
        public struct TabPageTexts
        {
            public static string NewMail;
            public static string WriteBack;
            public static string MailTranser;
            public static string AddressList;
            public static string Calendar;
            public static string NewContact;
            public static string NewGroup;
            public static string NewMeeting;
            public static string NewAppointment;
            public static string NewAnniversary;
            public static string NewMemo;
            public static string NewEvent;
            static TabPageTexts()
            {
                //NewMail = Resources.data_tabNewMail;
                //WriteBack = Resources.data_tabReply;
                //MailTranser = Resources.data_tabTransferMail;
                //AddressList = Resources.data_tabAddressBook;
                //Calendar = Resources.data_tabCalendar;
                //NewContact = Resources.data_tabNewContact;
                //NewGroup = Resources.data_tabNewGroup;
                //NewMeeting = Resources.data_tabNewMeeting;
                //NewAppointment = Resources.CalendarItemTypeAppointment;
                //NewAnniversary = Resources.CalendarItemTypeMemorialDay;
                //NewMemo = Resources.CalendarItemTypeMemo;
                //NewEvent = Resources.CalendarItemTypeEvent;
            }
        }

        [StructLayout(LayoutKind.Sequential, Size=1)]
        public struct ToolBarMenubarUISet
        {
            public const string MenuItemSmallSizeType = "small";
            public const int MenuItemSmallSize = 8;
            public const string MenuItemMiddleSizeType = "middle";
            public const int MenuItemMiddleSize = 9;
            public const string MenuItemBigSizeType = "big";
            public const int MenuItemBigSize = 11;
            public const string ToolBarItemDisplayOnlyImage = "Image";
            public const string ToolBarItemDisplayOnlyChar = "Literal";
            public const string ToolBarItemDisplayImageAndChar = "ImageWithLiteral";
            public const string ToolBarItemSmallSizeType = "small";
            public const int ToolBarItemSmallSizePic = 0x10;
            public const int ToolBarItemSmallSizeChar = 9;
            public const string ToolBarItemBigSizeType = "big";
            public const int ToolBarItemBigSizePic = 0x15;
            public const int ToolBarItemBigSizeChar = 11;
        }
    }
}

