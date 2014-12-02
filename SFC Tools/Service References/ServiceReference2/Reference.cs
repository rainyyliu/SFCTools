﻿//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本:4.0.30319.18444
//
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
// </auto-generated>
//------------------------------------------------------------------------------

namespace SFC_Tools.ServiceReference2 {
    using System.Data;
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(Namespace="http://ddnp.org/", ConfigurationName="ServiceReference2.AuthenticateServiceSoap")]
    public interface AuthenticateServiceSoap {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://ddnp.org/Authenticate", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        string Authenticate(string userName, string password, int cerValidMinutes);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://ddnp.org/GetPostCerName", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        string GetPostCerName();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://ddnp.org/ChangePassword", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        bool ChangePassword(string username, string oldPwd, string newPwd);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://ddnp.org/CreateUser", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        SFC_Tools.ServiceReference2.MembershipUser CreateUser(out SFC_Tools.ServiceReference2.MembershipCreateStatus status, string username, string password, string email, string passwordQuestion, string passwordAnswer, bool isApproved, object providerUserKey);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://ddnp.org/GetUser", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        SFC_Tools.ServiceReference2.MembershipUser GetUser(string username);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://ddnp.org/DeleteUser", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        bool DeleteUser(string username);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://ddnp.org/ResetPassword", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        string ResetPassword(string username);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://ddnp.org/LockUser", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        System.Data.DataSet LockUser(System.Data.DataSet dst);
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.34234")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://ddnp.org/")]
    public partial class MembershipUser : object, System.ComponentModel.INotifyPropertyChanged {
        
        private string emailField;
        
        private string commentField;
        
        private bool isApprovedField;
        
        private System.DateTime lastLoginDateField;
        
        private System.DateTime lastActivityDateField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=0)]
        public string Email {
            get {
                return this.emailField;
            }
            set {
                this.emailField = value;
                this.RaisePropertyChanged("Email");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=1)]
        public string Comment {
            get {
                return this.commentField;
            }
            set {
                this.commentField = value;
                this.RaisePropertyChanged("Comment");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=2)]
        public bool IsApproved {
            get {
                return this.isApprovedField;
            }
            set {
                this.isApprovedField = value;
                this.RaisePropertyChanged("IsApproved");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=3)]
        public System.DateTime LastLoginDate {
            get {
                return this.lastLoginDateField;
            }
            set {
                this.lastLoginDateField = value;
                this.RaisePropertyChanged("LastLoginDate");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=4)]
        public System.DateTime LastActivityDate {
            get {
                return this.lastActivityDateField;
            }
            set {
                this.lastActivityDateField = value;
                this.RaisePropertyChanged("LastActivityDate");
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
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://ddnp.org/")]
    public enum MembershipCreateStatus {
        
        /// <remarks/>
        Success,
        
        /// <remarks/>
        InvalidUserName,
        
        /// <remarks/>
        InvalidPassword,
        
        /// <remarks/>
        InvalidQuestion,
        
        /// <remarks/>
        InvalidAnswer,
        
        /// <remarks/>
        InvalidEmail,
        
        /// <remarks/>
        DuplicateUserName,
        
        /// <remarks/>
        DuplicateEmail,
        
        /// <remarks/>
        UserRejected,
        
        /// <remarks/>
        InvalidProviderUserKey,
        
        /// <remarks/>
        DuplicateProviderUserKey,
        
        /// <remarks/>
        ProviderError,
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface AuthenticateServiceSoapChannel : SFC_Tools.ServiceReference2.AuthenticateServiceSoap, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class AuthenticateServiceSoapClient : System.ServiceModel.ClientBase<SFC_Tools.ServiceReference2.AuthenticateServiceSoap>, SFC_Tools.ServiceReference2.AuthenticateServiceSoap {
        
        public AuthenticateServiceSoapClient() {
        }
        
        public AuthenticateServiceSoapClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public AuthenticateServiceSoapClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public AuthenticateServiceSoapClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public AuthenticateServiceSoapClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public string Authenticate(string userName, string password, int cerValidMinutes) {
            return base.Channel.Authenticate(userName, password, cerValidMinutes);
        }
        
        public string GetPostCerName() {
            return base.Channel.GetPostCerName();
        }
        
        public bool ChangePassword(string username, string oldPwd, string newPwd) {
            return base.Channel.ChangePassword(username, oldPwd, newPwd);
        }
        
        public SFC_Tools.ServiceReference2.MembershipUser CreateUser(out SFC_Tools.ServiceReference2.MembershipCreateStatus status, string username, string password, string email, string passwordQuestion, string passwordAnswer, bool isApproved, object providerUserKey) {
            return base.Channel.CreateUser(out status, username, password, email, passwordQuestion, passwordAnswer, isApproved, providerUserKey);
        }
        
        public SFC_Tools.ServiceReference2.MembershipUser GetUser(string username) {
            return base.Channel.GetUser(username);
        }
        
        public bool DeleteUser(string username) {
            return base.Channel.DeleteUser(username);
        }
        
        public string ResetPassword(string username) {
            return base.Channel.ResetPassword(username);
        }
        
        public System.Data.DataSet LockUser(System.Data.DataSet dst) {
            return base.Channel.LockUser(dst);
        }
    }
}
