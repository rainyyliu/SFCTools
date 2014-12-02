namespace SFC_Tools.Classes
{
    using LumiSoft.Net.Mail;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class MailHeaderAndBody
    {
        private string allowTransfer = MailY;
        private string Approval_BG;
        private string Approval_BU;
        private string approvalIntSecondApp;
        private string banDownRead;
        private string bcc;
        private string bGApprover;
        private string bodyEncode;
        private string bodyText;
        private byte[] bsBody;
        private string calendarItemAddress = string.Empty;
        private string calendarItemEndTime = string.Empty;
        private string calendarItemRemindAgain = string.Empty;
        private string calendarItemRemindDesc = string.Empty;
        private string calendarItemRemindTime = string.Empty;
        private string calendarItemStartTime = string.Empty;
        private string calendarItemType = string.Empty;
        private string cc;
        private string ccList;
        private string ccType;
        private string confidentiality = "Normal";
        private static string Delete = @"\Deleted";
        private string deleteFlag;
        private string digitalSignContent = string.Empty;
        private string digitalSignPubKey = string.Empty;
        private string encrypt;
        private string from;
        private string fromDisplayName;
        private string hasAttachments = MailN;
        private string hasWhiteKey = string.Empty;
        private string importance = MailN;
        private string Internet_Approver;
        private bool isAttachedDigitalSignature = false;
        private bool isAuditEmail;
        private bool isHtml;
        private string isInternetMail;
        private string isInternetSenderWhiteList;
        private string letterMailCreatedTime = string.Empty;
        private string letterMailLetterName = string.Empty;
        private string localMeetingSentDate = string.Empty;
        private string mailClassification;
        public static string MailN = "N";
        private string mailType;
        public static string MailY = "Y";
        private string meetingAlarmDuration;
       // private Dictionary<string, SuperNotes.Entity.MeetingAttendee> meetingAttendee;
        private string meetingDescription;
        private string meetingEndTime;
        private string meetingLocation;
        private bool meetingNeedAlarm;
        private string meetingOrgnizer;
        private string meetingRule;
        private string meetingStartTime;
        private string meetingStatus;
        private string meetingSummary;
        private string meetingType;
        private string meetingUID;
        private Mail_Message mime;
        private string moodMark = "0";
        private string myDate;
        private bool needNotice;
        private bool notification;
        private string originSender;
        private string priority = "Normal";
        private string readFlag;
        private string received;
        private string ruleDealFlag;
        private string savEnterBlindTo;
        private string savEnterCopyToVal;
        private string savEnterSendToVal;
        private string savSubjectVal;
        private static string Seen = @"\Seen";
        private string size;
        private string srcBodyText;
        private List<string> state;
        private string sub;
        private string uid;
        private string whiteKeyApprover = string.Empty;
        private string whiteKeySecurityBcc = string.Empty;

        public string AllowTransfer
        {
            get
            {
                if ((from o in this.State
                    where o == "NoForward"
                    select o).Count<string>() > 0)
                {
                    this.allowTransfer = MailN;
                }
                return this.allowTransfer;
            }
            set
            {
                this.allowTransfer = value;
            }
        }

        public string ApprovalBG
        {
            get
            {
                return this.Approval_BG;
            }
            set
            {
                this.Approval_BG = value;
            }
        }

        public string ApprovalBU
        {
            get
            {
                return this.Approval_BU;
            }
            set
            {
                this.Approval_BU = value;
            }
        }

        public string ApprovalIntSecondApp
        {
            get
            {
                return this.approvalIntSecondApp;
            }
            set
            {
                this.approvalIntSecondApp = value;
            }
        }

        public string BanDownRead
        {
            get
            {
                return this.banDownRead;
            }
            set
            {
                this.banDownRead = value;
            }
        }

        public string Bcc
        {
            get
            {
                if (this.bcc == null)
                {
                    this.bcc = string.Empty;
                }
                return this.bcc;
            }
            set
            {
                this.bcc = value;
            }
        }

        public string BGApprover
        {
            get
            {
                return this.bGApprover;
            }
            set
            {
                this.bGApprover = value;
            }
        }

        public string BodyEncode
        {
            get
            {
                return this.bodyEncode;
            }
            set
            {
                this.bodyEncode = value;
            }
        }

        public string BodyText
        {
            get
            {
                if (this.bodyText == null)
                {
                    this.bodyText = "";
                }
                return this.bodyText;
            }
            set
            {
                this.bodyText = value;
            }
        }

        public byte[] BsBody
        {
            get
            {
                return this.bsBody;
            }
            set
            {
                this.bsBody = value;
            }
        }

        public string CalendarItemAddress
        {
            get
            {
                return this.calendarItemAddress;
            }
            set
            {
                this.calendarItemAddress = value;
            }
        }

        public string CalendarItemEndTime
        {
            get
            {
                return this.calendarItemEndTime;
            }
            set
            {
                this.calendarItemEndTime = value;
            }
        }

        public string CalendarItemRemindAgain
        {
            get
            {
                return this.calendarItemRemindAgain;
            }
            set
            {
                this.calendarItemRemindAgain = value;
            }
        }

        public string CalendarItemRemindDesc
        {
            get
            {
                return this.calendarItemRemindDesc;
            }
            set
            {
                this.calendarItemRemindDesc = value;
            }
        }

        public string CalendarItemRemindTime
        {
            get
            {
                return this.calendarItemRemindTime;
            }
            set
            {
                this.calendarItemRemindTime = value;
            }
        }

        public string CalendarItemStartTime
        {
            get
            {
                return this.calendarItemStartTime;
            }
            set
            {
                this.calendarItemStartTime = value;
            }
        }

        public string CalendarItemType
        {
            get
            {
                return this.calendarItemType;
            }
            set
            {
                this.calendarItemType = value;
            }
        }

        public string CC
        {
            get
            {
                if (this.cc == null)
                {
                    this.cc = string.Empty;
                }
                return this.cc;
            }
            set
            {
                this.cc = value;
            }
        }

        public string CCList
        {
            get
            {
                return this.ccList;
            }
            set
            {
                this.ccList = value;
            }
        }

        public string CCType
        {
            get
            {
                return this.ccType;
            }
            set
            {
                this.ccType = value;
            }
        }

        public string Confidentiality
        {
            get
            {
                return this.confidentiality;
            }
            set
            {
                this.confidentiality = value;
            }
        }

        public string DeleteState
        {
            get
            {
                if ((from o in this.State
                    where o == Delete
                    select o).Count<string>() > 0)
                {
                    this.deleteFlag = MailY;
                }
                else
                {
                    this.deleteFlag = MailN;
                }
                return this.deleteFlag;
            }
            set
            {
                this.deleteFlag = value;
            }
        }

        public string DigitalSignContent
        {
            get
            {
                return this.digitalSignContent;
            }
            set
            {
                this.digitalSignContent = value;
            }
        }

        public string DigitalSignPubKey
        {
            get
            {
                return this.digitalSignPubKey;
            }
            set
            {
                this.digitalSignPubKey = value;
            }
        }

        public string Encrypt
        {
            get
            {
                return this.encrypt;
            }
            set
            {
                this.encrypt = value;
            }
        }

        public string From
        {
            get
            {
                if (this.from == null)
                {
                    this.from = string.Empty;
                }
                return this.from;
            }
            set
            {
                this.from = value;
            }
        }

        public string FromDisplayName
        {
            get
            {
                return this.fromDisplayName;
            }
            set
            {
                this.fromDisplayName = value;
            }
        }

        public string HasAttachments
        {
            get
            {
                if ((from o in this.State
                    where o == "Attached"
                    select o).Count<string>() > 0)
                {
                    this.hasAttachments = MailY;
                }
                else
                {
                    this.hasAttachments = MailN;
                }
                return this.hasAttachments;
            }
            set
            {
                this.hasAttachments = value;
            }
        }

        public string HasWhiteKey
        {
            get
            {
                return this.hasWhiteKey;
            }
            set
            {
                this.hasWhiteKey = value;
            }
        }

        public string Importance
        {
            get
            {
                if ((from o in this.State
                    where o == "Important"
                    select o).Count<string>() > 0)
                {
                    this.importance = MailY;
                }
                else
                {
                    this.importance = MailN;
                }
                return this.importance;
            }
            set
            {
                this.importance = value;
            }
        }

        public string InternetApprover
        {
            get
            {
                return this.Internet_Approver;
            }
            set
            {
                this.Internet_Approver = value;
            }
        }

        public bool IsAttachedDigitalSignature
        {
            get
            {
                return this.isAttachedDigitalSignature;
            }
            set
            {
                this.isAttachedDigitalSignature = value;
            }
        }

        public bool IsAuditEmail
        {
            get
            {
                return this.isAuditEmail;
            }
            set
            {
                this.isAuditEmail = value;
            }
        }

        public bool IsHtml
        {
            get
            {
                return this.isHtml;
            }
            set
            {
                this.isHtml = value;
            }
        }

        public string IsInternetMail
        {
            get
            {
                return this.isInternetMail;
            }
            set
            {
                this.isInternetMail = value;
            }
        }

        public string IsInternetSenderWhiteList
        {
            get
            {
                return this.isInternetSenderWhiteList;
            }
            set
            {
                this.isInternetSenderWhiteList = value;
            }
        }

        public string LetterMailCreatedTime
        {
            get
            {
                return this.letterMailCreatedTime;
            }
            set
            {
                this.letterMailCreatedTime = value;
            }
        }

        public string LetterMailLetterName
        {
            get
            {
                return this.letterMailLetterName;
            }
            set
            {
                this.letterMailLetterName = value;
            }
        }

        public string LocalMeetingSentDate
        {
            get
            {
                return this.localMeetingSentDate;
            }
            set
            {
                this.localMeetingSentDate = value;
            }
        }

        public string MailClassification
        {
            get
            {
                return this.mailClassification;
            }
            set
            {
                this.mailClassification = value;
            }
        }

        public string MailType
        {
            get
            {
                return this.mailType;
            }
            set
            {
                this.mailType = value;
            }
        }

        public string MeetingAlarmDuration
        {
            get
            {
                return this.meetingAlarmDuration;
            }
            set
            {
                this.meetingAlarmDuration = value;
            }
        }

        //internal Dictionary<string, SuperNotes.Entity.MeetingAttendee> MeetingAttendee
        //{
        //    get
        //    {
        //        return this.meetingAttendee;
        //    }
        //    set
        //    {
        //        this.meetingAttendee = value;
        //    }
        //}

        public string MeetingDescription
        {
            get
            {
                return this.meetingDescription;
            }
            set
            {
                this.meetingDescription = value;
            }
        }

        public string MeetingEndTime
        {
            get
            {
                return this.meetingEndTime;
            }
            set
            {
                this.meetingEndTime = value;
            }
        }

        public string MeetingLocation
        {
            get
            {
                return this.meetingLocation;
            }
            set
            {
                this.meetingLocation = value;
            }
        }

        public bool MeetingNeedAlarm
        {
            get
            {
                if ((from o in this.State
                    where o == "NoNeedAlarm"
                    select o).Count<string>() > 0)
                {
                    this.meetingNeedAlarm = false;
                }
                else
                {
                    this.meetingNeedAlarm = true;
                }
                return this.meetingNeedAlarm;
            }
            set
            {
                this.meetingNeedAlarm = value;
            }
        }

        public string MeetingOrgnizer
        {
            get
            {
                return this.meetingOrgnizer;
            }
            set
            {
                this.meetingOrgnizer = value;
            }
        }

        public string MeetingRule
        {
            get
            {
                return this.meetingRule;
            }
            set
            {
                this.meetingRule = value;
            }
        }

        public string MeetingStartTime
        {
            get
            {
                return this.meetingStartTime;
            }
            set
            {
                this.meetingStartTime = value;
            }
        }

        public string MeetingStatus
        {
            get
            {
                return this.meetingStatus;
            }
            set
            {
                this.meetingStatus = value;
            }
        }

        public string MeetingSummary
        {
            get
            {
                return this.meetingSummary;
            }
            set
            {
                this.meetingSummary = value;
            }
        }

        public string MeetingType
        {
            get
            {
                return this.meetingType;
            }
            set
            {
                this.meetingType = value;
            }
        }

        public string MeetingUID
        {
            get
            {
                return this.meetingUID;
            }
            set
            {
                this.meetingUID = value;
            }
        }

        public Mail_Message Mime
        {
            get
            {
                return this.mime;
            }
            set
            {
                this.mime = value;
            }
        }

        public string MoodMark
        {
            get
            {
                return this.moodMark;
            }
            set
            {
                this.moodMark = value;
            }
        }

        public string MyDate
        {
            get
            {
                return this.myDate;
            }
            set
            {
                this.myDate = value;
            }
        }

        public bool NeedNotice
        {
            get
            {
                return this.needNotice;
            }
            set
            {
                this.needNotice = value;
            }
        }

        public bool Notification
        {
            get
            {
                if (this.needNotice && ((from o in this.State
                    where o == "Noticed"
                    select o).Count<string>() < 1))
                {
                    this.notification = true;
                }
                else
                {
                    this.notification = false;
                }
                return this.notification;
            }
            set
            {
                this.notification = value;
            }
        }

        public string OriginSender
        {
            get
            {
                return this.originSender;
            }
            set
            {
                this.originSender = value;
            }
        }

        public string Priority
        {
            get
            {
                return this.priority;
            }
            set
            {
                this.priority = value;
            }
        }

        public string ReadState
        {
            get
            {
                if ((from o in this.State
                    where o == Seen
                    select o).Count<string>() > 0)
                {
                    this.readFlag = MailY;
                }
                else
                {
                    this.readFlag = MailN;
                }
                return this.readFlag;
            }
            set
            {
                this.readFlag = value;
            }
        }

        public string Received
        {
            get
            {
                if (this.received == null)
                {
                    this.received = string.Empty;
                }
                return this.received;
            }
            set
            {
                this.received = value;
            }
        }

        public string RuleDealState
        {
            get
            {
                if ((from o in this.State
                    where o == "RuleDealed"
                    select o).Count<string>() > 0)
                {
                    this.ruleDealFlag = MailY;
                }
                else
                {
                    this.ruleDealFlag = MailN;
                }
                return this.ruleDealFlag;
            }
            set
            {
                this.ruleDealFlag = value;
            }
        }

        public string SavEnterBlindTo
        {
            get
            {
                return this.savEnterBlindTo;
            }
            set
            {
                this.savEnterBlindTo = value;
            }
        }

        public string SavEnterCopyToVal
        {
            get
            {
                return this.savEnterCopyToVal;
            }
            set
            {
                this.savEnterCopyToVal = value;
            }
        }

        public string SavEnterSendToVal
        {
            get
            {
                return this.savEnterSendToVal;
            }
            set
            {
                this.savEnterSendToVal = value;
            }
        }

        public string SavSubjectVal
        {
            get
            {
                return this.savSubjectVal;
            }
            set
            {
                this.savSubjectVal = value;
            }
        }

        public string Size
        {
            get
            {
                return this.size;
            }
            set
            {
                this.size = value;
            }
        }

        public string SrcBodyText
        {
            get
            {
                return this.srcBodyText;
            }
            set
            {
                this.srcBodyText = value;
            }
        }

        public List<string> State
        {
            get
            {
                if (this.state == null)
                {
                    this.state = new List<string>();
                }
                return this.state;
            }
            set
            {
                this.state = value;
            }
        }

        public string Sub
        {
            get
            {
                if (this.sub == null)
                {
                    this.sub = string.Empty;
                }
                return this.sub;
            }
            set
            {
                this.sub = value;
            }
        }

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

        public string WhiteKeyApprover
        {
            get
            {
                return this.whiteKeyApprover;
            }
            set
            {
                this.whiteKeyApprover = value;
            }
        }

        public string WhiteKeySecurityBcc
        {
            get
            {
                return this.whiteKeySecurityBcc;
            }
            set
            {
                this.whiteKeySecurityBcc = value;
            }
        }
    }
}

