using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using System.Collections;
using SFC_Tools.Model;

namespace SFC_Tools.Classes
{
    class XmlFileRW
    {
        public XmlFileRW()
        { 
        
        }

        public string GetXmlFileInfo(string sPath)
        {
            string strRes = "N/A";
            try
            {
                XmlReader myReader = new XmlTextReader(sPath);
                XmlDocument doc = new XmlDocument();
                doc.Load(myReader);
                myReader.Read();
                strRes=myReader.Value;
                /*XmlNode nodeRoot = doc.DocumentElement;
                XmlNode nodeChild = nodeRoot.SelectSingleNode(@"TestRun");
                XmlNode node2 = nodeChild.SelectSingleNode("@grade");
                strRes = node2.InnerText;*/
                myReader.Close();
            }
            catch (Exception ex)
            {
                throw new Exception("Error Happen" + ex.Message.ToString());
            }
            return strRes;
        }

        public string GetXmlFileGrade(string sPath)
        {
            string strRes = "N/A";
            try
            {
                XmlReader myReader = new XmlTextReader(sPath);
                XmlDocument doc = new XmlDocument();
                doc.Load(myReader);
                
                XmlNode nodeRoot = doc.DocumentElement;
                XmlNode nodeChild = nodeRoot.SelectSingleNode(@"TestRun");
                //XmlNode node1 = nodeChild.SelectSingleNode("@name");
                XmlNode node2 = nodeChild.SelectSingleNode("@grade");
                strRes = node2.InnerText;
                myReader.Close();
            }
            catch (Exception ex)
            {
                throw new Exception("Error Happen" + ex.Message.ToString() + " System will use EN for default language!");
            }
            return strRes;
        }

        private XmlElement SetNodeValue(XmlElement xmlNode, ArrayList arrAttribute,string sValue)
        {
            foreach (Hashtable sAtt in arrAttribute)
            {
                xmlNode.SetAttribute(sAtt.Keys.ToString(),sAtt.Values.ToString());
            }
            xmlNode.InnerText = sValue;
            return xmlNode;
        }

        public bool WriteXmlFile(string sPath, BrFenixDell dellDataFeed)
        {
            bool bIsWriteOk = false;
            try
            {
                XmlDocument doc = new XmlDocument();
                XmlNode xmlHead = doc.CreateXmlDeclaration("1.0", "utf-8", "yes");
                doc.PrependChild(xmlHead);
                //doc.AppendChild(xmlHead);
                XmlElement nodeRoot = doc.CreateElement("UnitReport");
                nodeRoot.SetAttribute("quantity", /*"1"*/dellDataFeed.QUANTITY.ToString());

                if (bCheckDataValid(dellDataFeed.ENDTIME))
                    nodeRoot.SetAttribute("end-time", /*"2013年7月8日11:09:27"*/dellDataFeed.ENDTIME);
                if (bCheckDataValid(dellDataFeed.STARTTIME))
                    nodeRoot.SetAttribute("start-time", /*"2013-7-8 11:09:37"*/dellDataFeed.STARTTIME);
                doc.AppendChild(nodeRoot);
                XmlElement nodeStation = doc.CreateElement("Station");
                nodeStation.SetAttribute("guid", /*"8ae52b8b-130e-452b-94d3-dc3cf25d1100"*/dellDataFeed.GUID);
                nodeRoot.AppendChild(nodeStation);
                XmlElement nodePropertyStation = doc.CreateElement("Property");

                nodeStation.AppendChild(nodePropertyStation);
                XmlElement nodeValueString = doc.CreateElement("ValueString");
                nodeValueString.SetAttribute("name", "StationType");

                nodeValueString.InnerText = /*"FINAL INSPECT"*/dellDataFeed.STATIONNAME;
                nodePropertyStation.AppendChild(nodeValueString);

                //oerator
                XmlElement nodeOperator = doc.CreateElement("Operator");
                nodeOperator.SetAttribute("name", /*"F1038723"*/dellDataFeed.EMP);
                nodeRoot.AppendChild(nodeOperator);

                //Category
                XmlElement nodeCategory = doc.CreateElement("Category");
                nodeCategory.SetAttribute("name", /*"MB"*/dellDataFeed.CATEGORY);
                nodeRoot.AppendChild(nodeCategory);

                //Category
                XmlElement nodeProduct = doc.CreateElement("Product");
                nodeProduct.SetAttribute("part-no", /*"8HPGT"*/dellDataFeed.MFPN);
                nodeProduct.SetAttribute("serial-no", /*"BR08HPGT1081935D0051A01"*/dellDataFeed.MFSN);
                nodeCategory.AppendChild(nodeProduct);

                //Property
                XmlElement nodeProperty = doc.CreateElement("Property");
                XmlElement nodeSubvalue;
                nodeSubvalue = doc.CreateElement("ValueString");
                nodeSubvalue.SetAttribute("name", "ManuFacturer Name");
                nodeSubvalue.InnerText = /*"Foxconn"*/dellDataFeed.MFNAME;
                nodeProperty.AppendChild(nodeSubvalue);

                nodeSubvalue = doc.CreateElement("ValueString");
                nodeSubvalue.SetAttribute("name", "Manufacturing Site");
                nodeSubvalue.InnerText = /*"Jundiai"*/dellDataFeed.MFSITE;
                nodeProperty.AppendChild(nodeSubvalue);
                nodeSubvalue = doc.CreateElement("ValueString");
                nodeSubvalue.SetAttribute("name", "Manufacturers Part Number");
                nodeSubvalue.InnerText = /*"8HPGT"*/dellDataFeed.MFPN;
                nodeProperty.AppendChild(nodeSubvalue);
                nodeSubvalue = doc.CreateElement("ValueString");
                nodeSubvalue.SetAttribute("name", "Manufacturers Serial Number");
                nodeSubvalue.InnerText = /*"BR08HPGT1081935D0051A01"*/dellDataFeed.MFSN;
                nodeProperty.AppendChild(nodeSubvalue);

                nodeRoot.AppendChild(nodeProperty);

                //TestRun
                XmlElement nodeTestRun = doc.CreateElement("TestRun");
                nodeTestRun.SetAttribute("name", /*"FINAL INSPECT"*/dellDataFeed.STATIONNAME);
                nodeTestRun.SetAttribute("grade", /*"PASS"*/dellDataFeed.TESTGRADE);
                if (bCheckDataValid(dellDataFeed.TESTSTARTTIME))
                    nodeTestRun.SetAttribute("start-time", /*"2013-7-8 15:07:33"*/dellDataFeed.TESTSTARTTIME);
                if (bCheckDataValid(dellDataFeed.TESTENDTIME))
                    nodeTestRun.SetAttribute("end-time", /*"2013-7-8 15:07:48"*/dellDataFeed.TESTENDTIME);
                nodeRoot.AppendChild(nodeTestRun);

                //Category
                XmlElement nodeTestProperty = doc.CreateElement("Property");

                XmlElement nodeTestValueString = doc.CreateElement("ValueString");
                nodeTestValueString.SetAttribute("name", "Assembly Line");
                nodeTestValueString.InnerText = /*"FINAL INSPECT4"*/dellDataFeed.TESTLINE;
                nodeTestProperty.AppendChild(nodeTestValueString);
                nodeTestRun.AppendChild(nodeTestProperty);

                doc.Save(sPath);
                bIsWriteOk = true;
            }
            catch (Exception ex)
            {
                throw new Exception("Error Happen" + ex.Message.ToString());
            }
            return bIsWriteOk;
        }

        public bool WriteXmlFileLot(string sPath,BrFenixDell dellDataFeed)
        {
            bool bIsWriteOk = false;
            try
            {
                XmlDocument doc = new XmlDocument();
                XmlNode xmlHead = doc.CreateXmlDeclaration("1.0", "utf-8", "yes");
                doc.PrependChild(xmlHead);
                //doc.AppendChild(xmlHead);
                XmlElement nodeRoot = doc.CreateElement("UnitReport");
                  nodeRoot.SetAttribute("quantity", /*"1"*/dellDataFeed.QUANTITY.ToString());

                if (bCheckDataValid(dellDataFeed.ENDTIME))
                     nodeRoot.SetAttribute("end-time", /*"2013年7月8日11:09:27"*/dellDataFeed.ENDTIME);
                if (bCheckDataValid(dellDataFeed.STARTTIME))
                    nodeRoot.SetAttribute("start-time", /*"2013-7-8 11:09:37"*/dellDataFeed.STARTTIME);
                doc.AppendChild(nodeRoot);
                XmlElement nodeStation = doc.CreateElement("Station");
                nodeStation.SetAttribute("guid", /*"8ae52b8b-130e-452b-94d3-dc3cf25d1100"*/dellDataFeed.GUID);
                nodeRoot.AppendChild(nodeStation);
                XmlElement nodePropertyStation = doc.CreateElement("Property");
                
                nodeStation.AppendChild(nodePropertyStation);
                XmlElement nodeValueString = doc.CreateElement("ValueString");
                nodeValueString.SetAttribute("name", "StationType");

                nodeValueString.InnerText = /*"FINAL INSPECT"*/dellDataFeed.STATIONNAME;
                nodePropertyStation.AppendChild(nodeValueString);

                //oerator
                XmlElement nodeOperator = doc.CreateElement("Operator");
                nodeOperator.SetAttribute("name", /*"F1038723"*/dellDataFeed.EMP);
                nodeRoot.AppendChild(nodeOperator);

                //Category
                XmlElement nodeCategory = doc.CreateElement("Category");
                nodeCategory.SetAttribute("name", /*"MB"*/dellDataFeed.CATEGORY);
                nodeRoot.AppendChild(nodeCategory);

                //Category
                XmlElement nodeProduct = doc.CreateElement("Product");
                nodeProduct.SetAttribute("part-no", /*"8HPGT"*/dellDataFeed.MFPN);
                nodeProduct.SetAttribute("serial-no", /*"BR08HPGT1081935D0051A01"*/dellDataFeed.MFSN);
                nodeCategory.AppendChild(nodeProduct);

                //Property
                XmlElement nodeProperty = doc.CreateElement("Property");
                XmlElement nodeSubvalue;
                nodeSubvalue = doc.CreateElement("ValueString");
                nodeSubvalue.SetAttribute("name", "ManuFacturer Name");
                nodeSubvalue.InnerText = /*"Foxconn"*/dellDataFeed.MFNAME;
                nodeProperty.AppendChild(nodeSubvalue);

                nodeSubvalue = doc.CreateElement("ValueString");
                nodeSubvalue.SetAttribute("name", "Manufacturing Site");
                nodeSubvalue.InnerText = /*"Jundiai"*/dellDataFeed.MFSITE;
                nodeProperty.AppendChild(nodeSubvalue);
                nodeSubvalue = doc.CreateElement("ValueString");
                nodeSubvalue.SetAttribute("name", "Manufacturers Part Number");
                nodeSubvalue.InnerText = /*"8HPGT"*/dellDataFeed.MFPN;
                nodeProperty.AppendChild(nodeSubvalue);
                nodeSubvalue = doc.CreateElement("ValueString");
                nodeSubvalue.SetAttribute("name", "Product Description");
                nodeSubvalue.InnerText = /*"BR08HPGT1081935D0051A01"*/dellDataFeed.MFSN;
                nodeProperty.AppendChild(nodeSubvalue);

                nodeRoot.AppendChild(nodeProperty);

                //TestRun
                XmlElement nodeTestRun = doc.CreateElement("TestRun");
                nodeTestRun.SetAttribute("name", /*"FINAL INSPECT"*/dellDataFeed.STATIONNAME);
                nodeTestRun.SetAttribute("grade", /*"PASS"*/dellDataFeed.TESTGRADE);
                if (bCheckDataValid(dellDataFeed.TESTSTARTTIME))
                    nodeTestRun.SetAttribute("start-time", /*"2013-7-8 15:07:33"*/dellDataFeed.TESTSTARTTIME);
                if (bCheckDataValid(dellDataFeed.TESTENDTIME))
                    nodeTestRun.SetAttribute("end-time", /*"2013-7-8 15:07:48"*/dellDataFeed.TESTENDTIME);
                nodeRoot.AppendChild(nodeTestRun);

                //Category
                XmlElement nodeTestProperty = doc.CreateElement("Property");

                XmlElement nodeTestValueString = doc.CreateElement("ValueString");
                nodeTestValueString.SetAttribute("name", "Assembly Line");
                nodeTestValueString.InnerText = /*"FINAL INSPECT4"*/dellDataFeed.TESTLINE;
                nodeTestProperty.AppendChild(nodeTestValueString);
                nodeTestRun.AppendChild(nodeTestProperty);

                doc.Save(sPath);
                bIsWriteOk = true;
            }
            catch (Exception ex)
            {
                throw new Exception("Error Happen" + ex.Message.ToString());
            }
            return bIsWriteOk;
        }
        private bool bCheckDataValid(string sData)
        {
            bool bIsDataValid = false;
            if (sData != null)
            {
                if (sData.Trim().Length != 0)
                    bIsDataValid = true;
            }
            return bIsDataValid;
        }
        public bool WriteXmlFileA(string sPath)
        {
            bool bIsWriteOk = false;
            try 
            {
                XmlSerializer ser = new XmlSerializer(Type.GetType("SFC_Tools.Classes.Truck"));
                Truck tr = new Truck();
                tr.ID = 1;
                tr.cheID = "粤B T34923";
                XmlTextWriter xtw = new XmlTextWriter(sPath, Encoding.UTF8);
                ser.Serialize(xtw,tr);
                
                bIsWriteOk=true;
            }
            catch (Exception ex)
            {
                throw new Exception("Error Happen" + ex.Message.ToString());
            }
            return bIsWriteOk;
        }
    }

    public class Truck
    {
        private int _id = 0;
        private string _cheID = ";";
        public Truck()
        { }
        public int ID
        {
            set { this._id = value; }
            get { return this._id; }
        }

        public string  cheID
        {
            set { this._cheID = value; }
            get { return this._cheID; }
        }
    }

}
