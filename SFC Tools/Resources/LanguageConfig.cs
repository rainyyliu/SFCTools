using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Data;
using System.Collections;
using System.IO;
using System.Windows.Forms;
using System.Configuration;
using System.Globalization;

namespace SFC_Tools.Resources
{
    public static class LanguageConfig
    {
        public static string ReadDefaultLanguage()
        {
            XmlReader myReader = new XmlTextReader("Resources/LanguageDefine.xml");
            XmlDocument doc = new XmlDocument();
            doc.Load(myReader);
            XmlNode nodeRoot = doc.DocumentElement;
            XmlNode nodeChild = nodeRoot.SelectSingleNode("DefaultLanguage");
            string strRes = "EN";
            if (nodeChild != null)
            {
                strRes = nodeChild.InnerText;
            }
            myReader.Close();
            return strRes;
        }
        public static void WriteDefaultLanguage(string strLang)
        {
            DataSet ds = new DataSet();
            ds.ReadXml("Resources/LanguageDefine.xml");
            DataTable dt = ds.Tables["Language"];

            dt.Rows[0]["DefaultLanguage"] = strLang;
            ds.AcceptChanges();
            ds.WriteXml("Resources/LanguageDefine.xml");
        }

        public static IList GetLanguageList(string lang)
        {
            IList result = new ArrayList();

            XmlReader reader = new XmlTextReader("Resources/AppConfig.xml");
            XmlDocument doc = new XmlDocument();
            doc.Load(reader);

            XmlNode root = doc.DocumentElement;
            string strTemp = "Area[Language='" + lang + "']";
            XmlNodeList nodelist = root.SelectNodes(strTemp+@"/List/Item");
            ///List/Item
            foreach (XmlNode node in nodelist)
            {
                result.Add(node.InnerText);
            }
            reader.Close();

            return result;
        }
        /// <summary> 
        /// 读取多语言的资源文件 
        /// </summary> 
        /// <param name="frmName">窗体的Name</param> 
        /// <param name="lang">要显示的语言(如ZH或EN)</param> 
        /// <returns></returns> 
        public static Hashtable ReadResource(string frmName, string lang)
        {
            Hashtable result = new Hashtable();

            XmlReader reader = null;
            FileInfo fi = new FileInfo("Resources/AppResource_" + lang + ".xml");
            if (!fi.Exists)
                reader = new XmlTextReader("Resources/AppResource.xml");
            else
                reader = new XmlTextReader("Resources/AppResource_" + lang + ".xml");

            XmlDocument doc = new XmlDocument();
            doc.Load(reader);

            XmlNode root = doc.DocumentElement;
            XmlNodeList nodelist = root.SelectNodes("Form[Name='" + frmName + "']/Controls/Control");
            foreach (XmlNode node in nodelist)
            {
                try
                {
                    XmlNode node1 = node.SelectSingleNode("@name");
                    XmlNode node2 = node.SelectSingleNode("@text");
                    if (node1 != null)
                    {
                        result.Add(node1.InnerText.ToLower(), node2.InnerText);
                    }
                }
                catch (FormatException fe)
                {
                    Console.WriteLine(fe.ToString());
                }
            }
            reader.Close();

            return result;
        }
        /// <summary> 
        /// 获取控件的名称 
        /// </summary> 
        /// <param name="form"></param> 
        public static void getNames(Form form,string strType)
        {
            //根据用户选择的语言获得表的显示文字 
            Hashtable table = LanguageConfig.ReadResource(form.Name, strType/*Global.GetValue("lang").ToString()*/);

            Control.ControlCollection controlNames = form.Controls;
            //可以在这里设置窗体的一些统一的属性，这样所有的窗体都会应用该设置 
            // form.KeyPreview = true; 
            // form.MaximizeBox = false; 
            // form.MinimizeBox = false; 
            // form.ControlBox = false; 
            // form.FormBorderStyle = FormBorderStyle.FixedDialog; 
            // form.StartPosition = FormStartPosition.CenterScreen; 
            // form.TopMost = true; 
            // form.KeyDown += new KeyEventHandler(OnEnter); 
            try
            {
                foreach (Control control in controlNames)
                {
                    if (control.GetType() == typeof(System.Windows.Forms.Panel))
                        GetSubControls(control.Controls, table);

                    if (control.GetType() == typeof(System.Windows.Forms.GroupBox))
                        GetSubControls(control.Controls, table);

                    if (control.GetType() == typeof(System.Windows.Forms.Button))
                        GetSubControls(control.Controls, table);
                    //   if(control.GetType() == typeof(System.Windows.Forms.TextBox) && control.Enabled) 
                    //    control.GotFocus += new EventHandler(Txt_Focus); 

                    if (table.Contains(control.Name.ToLower()))
                        control.Text = (string)table[control.Name.ToLower()];
                }
                if (table.Contains(form.Name.ToLower()))
                    form.Text = (string)table[form.Name.ToLower()];
            }
            catch (Exception ex)
            { }
        }
        /// <summary> 
        /// 获得子控件的显示名 
        /// </summary> 
        /// <param name="controls"></param> 
        /// <param name="table"></param> 
        private static void GetSubControls(Control.ControlCollection controls,Hashtable table) 
        { 
            foreach(Control control in controls) 
            { 
                if(control.GetType() == typeof(System.Windows.Forms.Panel)) 
                 GetSubControls(control.Controls,table); 

                if(control.GetType() == typeof(System.Windows.Forms.GroupBox)) 
                    GetSubControls(control.Controls,table);

                if (control.GetType() == typeof(System.Windows.Forms.Button))
                    GetSubControls(control.Controls, table); 

                if(table.Contains(control.Name.ToLower())) 
                    control.Text = (string)table[control.Name.ToLower()]; 
            } 
        }
    }
}
