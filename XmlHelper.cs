using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.IO;
using System.Xml.Serialization;
namespace LoopClient
{
    class XmlHelper
    {
        private static XmlDocument xmlDoc = new XmlDocument();
         
        public static void objToXml<T>(T obj,string filename)
        {
            XmlDocument xd = new XmlDocument();
            using (StringWriter sw = new StringWriter())
            {
                XmlSerializer xz = new XmlSerializer(obj.GetType());
                xz.Serialize(sw, obj);
                //       Console.WriteLine(sw.ToString());
                xd.LoadXml(sw.ToString());

                XmlTextWriter xmlWriter;
                xmlWriter = new XmlTextWriter(filename, Encoding.UTF8);//creat ;
                xmlWriter.Formatting = Formatting.Indented;//自动缩进格式
                //保存xml文件 
                xd.Save(xmlWriter);
                xmlWriter.Close();
            }
        }
  
        public static data xmlToData(string filePath)
        {
            if (!File.Exists(filePath)) return null;
            data obj = new data();
            XmlSerializer serializer = new XmlSerializer(obj.GetType());
            FileStream stream = new FileStream(filePath, FileMode.Open);
            obj = (data)serializer.Deserialize(stream);
            stream.Close();
            return obj;
        }

        public static List<T> xmlToList<T>(string xml)
        {

            List<T> lst = new List<T>();
            if (!File.Exists(xml)) return lst; 
            using (XmlReader reader = XmlReader.Create(xml))
            {
                XmlSerializer xz = new XmlSerializer(lst.GetType());
                lst = (List<T>)xz.Deserialize(reader);
            }
            return lst;
        }


    }
}
