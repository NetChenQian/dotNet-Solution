using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using System.Xml.Linq;

namespace XMLOperation
{
    class Program
    {
        static void Main(string[] args)
        {
            #region Read XML
            string readPath = "http://www.fastrender.cn/fastrenderversion/version.xml";
            XmlDocument readXmlDocument = new XmlDocument();
            readXmlDocument.Load(readPath);
            List<string> list = RecursionXmlElement(readXmlDocument.DocumentElement);
            Console.WriteLine(string.Join(Environment.NewLine, list));
            #endregion

            #region Write XML
            string path = "Students.xml";

            #region Tradition Style 
            XmlDocument xmlDocument = new XmlDocument();

            XmlNode rootNode = xmlDocument.CreateElement("Students");

            XmlNode studentNode = xmlDocument.CreateElement("Student");
            XmlAttribute idAttribute = xmlDocument.CreateAttribute("ID");
            idAttribute.Value = "1001";
            studentNode.Attributes.Append(idAttribute);

            XmlNode nameNode = xmlDocument.CreateElement("Name");
            XmlText nameText = xmlDocument.CreateTextNode("张三");
            nameNode.AppendChild(nameText);
            XmlNode ageNode = xmlDocument.CreateElement("Age");
            XmlText ageText = xmlDocument.CreateTextNode("21");
            ageNode.AppendChild(ageText);
            XmlNode genderNode = xmlDocument.CreateElement("Gender");
            XmlText genderText = xmlDocument.CreateTextNode("男");
            genderNode.AppendChild(genderText);

            studentNode.AppendChild(nameNode);
            studentNode.AppendChild(ageNode);
            studentNode.AppendChild(genderNode);
            rootNode.AppendChild(studentNode);
            xmlDocument.AppendChild(rootNode);

            xmlDocument.Save(path);
            #endregion

            #region Chain Programming
            XmlDocument chainXmlDocument = new XmlDocument();
            chainXmlDocument.AppendChild(
                chainXmlDocument.CreateElement("Students").
                    AppendChild(chainXmlDocument.CreateElement("Student").
                            AppendChild(chainXmlDocument.CreateElement("Name").
                                AppendChild(chainXmlDocument.CreateTextNode("ZhangSan")
                                    ).ParentNode
                                        ).ParentNode.
                            AppendChild(chainXmlDocument.CreateElement("Age").
                                AppendChild(chainXmlDocument.CreateTextNode("21")
                                    ).ParentNode
                                        ).ParentNode.
                            AppendChild(chainXmlDocument.CreateElement("Gender").
                                AppendChild(chainXmlDocument.CreateTextNode("Male")
                                    ).ParentNode
                                        ).ParentNode
                    ).ParentNode.
                    AppendChild(chainXmlDocument.CreateElement("Student").
                            AppendChild(chainXmlDocument.CreateElement("Name").
                                AppendChild(chainXmlDocument.CreateTextNode("LiSi")
                                    ).ParentNode
                                        ).ParentNode.
                            AppendChild(chainXmlDocument.CreateElement("Age").
                                AppendChild(chainXmlDocument.CreateTextNode("19")
                                    ).ParentNode
                                        ).ParentNode.
                            AppendChild(chainXmlDocument.CreateElement("Gender").
                                AppendChild(chainXmlDocument.CreateTextNode("Female")
                                    ).ParentNode
                                        ).ParentNode
                    ).ParentNode
                );

            XmlNodeList xmlNodeList = chainXmlDocument.DocumentElement.SelectNodes("Student");

            int i = 1;
            string suffix = "00";
            foreach (XmlNode xmlNode in xmlNodeList)
            {
                string id = suffix + i;
                id = "1" + id.Substring(id.Length - suffix.Length - 1);
                XmlAttribute xmlAttribute = chainXmlDocument.CreateAttribute("ID");
                xmlAttribute.Value = id;
                xmlNode.Attributes.Append(xmlAttribute);
                i++;
            }

            chainXmlDocument.Save(path);
            #endregion
            #endregion
        }
        private static List<string> RecursionXmlElement(XmlNode rootNode)
        {
            List<string> list = new List<string>();
            XmlNodeList xmlNodeList = rootNode.ChildNodes;
            if (xmlNodeList != null && xmlNodeList.Count > 0)
            {
                foreach (XmlNode xmlNode in xmlNodeList)
                {
                    list.AddRange(RecursionXmlElement(xmlNode));
                }
            }
            else
            {
                list.Add(rootNode.ParentNode.Name + ", " + rootNode.Value);
            }
            return list;
        }
        static void Main2(string[] args)
        {
            #region Read XML
            string readPath = "http://www.fastrender.cn/fastrenderversion/version.xml";
            XDocument readXdocument = XDocument.Load(readPath);
            XElement readRootElement = readXdocument.Root;
            List<string> list = RecursionXElement(readRootElement);
            Console.WriteLine(string.Join(Environment.NewLine, list));
            #endregion

            #region Write XML
            string writepath = "Students.xml";

            #region Tradition Style
            XDocument docWriter = new XDocument();

            XElement rootWriter = new XElement("Students");

            XElement studentWriter = new XElement("Student");
            studentWriter.SetAttributeValue("id", "1001");

            XElement nameWriter = new XElement("name");
            nameWriter.Value = "张三";

            XElement ageWriter = new XElement("age");
            ageWriter.Value = "20";

            XElement genderWriter = new XElement("gender");
            genderWriter.Value = "男";

            studentWriter.Add(nameWriter);
            studentWriter.Add(ageWriter);
            studentWriter.Add(genderWriter);
            rootWriter.Add(studentWriter);
            docWriter.Add(rootWriter);

            docWriter.Save(writepath);
            #endregion

            #region Chain Programming
            XDocument xDocument = new XDocument(
                new XComment("This is a XML Document"),
                new XElement("Students",
                        new XElement("Student",
                            new XElement("Name", "ZhangSan"),
                            new XElement("Age", "20"),
                            new XElement("Gender", "Male")
                        ),
                        new XElement("Student",
                            new XElement("Name", "LiSi"),
                            new XElement("Age", "19"),
                            new XElement("Gender", "Female")
                        )
                    )
                );

            IEnumerable<XElement> rootElements = from es in xDocument.Root.Elements()
                                                 where es.Name.LocalName.Equals("Student")
                                                 select es;

            int i = 1;
            string suffix = "00";
            foreach (XElement xElement in rootElements)
            {
                string id = suffix + i;
                id = "1" + id.Substring(id.Length - suffix.Length - 1);
                xElement.SetAttributeValue("ID", id);
                i++;
            }

            xDocument.Save(writepath);
            #endregion
            #endregion
        }
        private static List<string> RecursionXElement(XElement rootElement)
        {
            List<string> list = new List<string>();
            IEnumerable<XElement> elements = rootElement.Elements();
            if (elements != null && elements.Count() > 0)
            {
                foreach (XElement xElement in elements)
                {
                    list.AddRange(RecursionXElement(xElement));
                }
            }
            else
            {
                list.Add(rootElement.Name + ", " + rootElement.Value);
            }
            return list;
        }
    }
}
