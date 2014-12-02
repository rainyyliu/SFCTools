﻿//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本:4.0.30319.18444
//
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
// </auto-generated>
//------------------------------------------------------------------------------

namespace SFC_Tools.ServiceReference4 {
    using System.Data;
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="ServiceReference4.DataServiceSoap")]
    public interface DataServiceSoap {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/LoadMessage", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(Message[]))]
        SFC_Tools.ServiceReference4.ControlToken LoadMessage(string uid);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/GetOnlinePersons", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(Message[]))]
        string[] GetOnlinePersons();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/Notice", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(Message[]))]
        void Notice(string uid);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/SendMsg", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(Message[]))]
        void SendMsg(string uid, SFC_Tools.ServiceReference4.Message msg);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/SendSystemMsg", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(Message[]))]
        void SendSystemMsg(SFC_Tools.ServiceReference4.Message msg);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/NoticeAll", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(Message[]))]
        void NoticeAll();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/Login", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(Message[]))]
        SFC_Tools.ServiceReference4.ControlToken Login(string uid, string pwd);
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.34234")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://tempuri.org/")]
    public partial class ControlToken : object, System.ComponentModel.INotifyPropertyChanged {
        
        private bool dataFilledField;
        
        private bool hasErrorField;
        
        private int codeField;
        
        private string errorMessageField;
        
        private object dataField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=0)]
        public bool DataFilled {
            get {
                return this.dataFilledField;
            }
            set {
                this.dataFilledField = value;
                this.RaisePropertyChanged("DataFilled");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=1)]
        public bool HasError {
            get {
                return this.hasErrorField;
            }
            set {
                this.hasErrorField = value;
                this.RaisePropertyChanged("HasError");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=2)]
        public int Code {
            get {
                return this.codeField;
            }
            set {
                this.codeField = value;
                this.RaisePropertyChanged("Code");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=3)]
        public string ErrorMessage {
            get {
                return this.errorMessageField;
            }
            set {
                this.errorMessageField = value;
                this.RaisePropertyChanged("ErrorMessage");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=4)]
        public object Data {
            get {
                return this.dataField;
            }
            set {
                this.dataField = value;
                this.RaisePropertyChanged("Data");
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.34234")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://tempuri.org/")]
    public partial class Message : object, System.ComponentModel.INotifyPropertyChanged {
        
        private string senderField;
        
        private string senderNameField;
        
        private MsgType msgTypeField;
        
        private string contentField;
        
        private System.Data.DataSet meetingInfoField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=0)]
        public string Sender {
            get {
                return this.senderField;
            }
            set {
                this.senderField = value;
                this.RaisePropertyChanged("Sender");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=1)]
        public string SenderName {
            get {
                return this.senderNameField;
            }
            set {
                this.senderNameField = value;
                this.RaisePropertyChanged("SenderName");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=2)]
        public MsgType MsgType {
            get {
                return this.msgTypeField;
            }
            set {
                this.msgTypeField = value;
                this.RaisePropertyChanged("MsgType");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=3)]
        public string Content {
            get {
                return this.contentField;
            }
            set {
                this.contentField = value;
                this.RaisePropertyChanged("Content");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=4)]
        public System.Data.DataSet MeetingInfo {
            get {
                return this.meetingInfoField;
            }
            set {
                this.meetingInfoField = value;
                this.RaisePropertyChanged("MeetingInfo");
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.34234")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://tempuri.org/")]
    public enum MsgType {
        
        /// <remarks/>
        UserMsg,
        
        /// <remarks/>
        SystemMsg,
        
        /// <remarks/>
        Online,
        
        /// <remarks/>
        Offline,
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface DataServiceSoapChannel : SFC_Tools.ServiceReference4.DataServiceSoap, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class DataServiceSoapClient : System.ServiceModel.ClientBase<SFC_Tools.ServiceReference4.DataServiceSoap>, SFC_Tools.ServiceReference4.DataServiceSoap {
        
        public DataServiceSoapClient() {
        }
        
        public DataServiceSoapClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public DataServiceSoapClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public DataServiceSoapClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public DataServiceSoapClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public SFC_Tools.ServiceReference4.ControlToken LoadMessage(string uid) {
            return base.Channel.LoadMessage(uid);
        }
        
        public string[] GetOnlinePersons() {
            return base.Channel.GetOnlinePersons();
        }
        
        public void Notice(string uid) {
            base.Channel.Notice(uid);
        }
        
        public void SendMsg(string uid, SFC_Tools.ServiceReference4.Message msg) {
            base.Channel.SendMsg(uid, msg);
        }
        
        public void SendSystemMsg(SFC_Tools.ServiceReference4.Message msg) {
            base.Channel.SendSystemMsg(msg);
        }
        
        public void NoticeAll() {
            base.Channel.NoticeAll();
        }
        
        public SFC_Tools.ServiceReference4.ControlToken Login(string uid, string pwd) {
            return base.Channel.Login(uid, pwd);
        }
    }
}
