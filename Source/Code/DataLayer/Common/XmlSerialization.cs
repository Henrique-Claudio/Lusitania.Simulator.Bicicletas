using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.IO;
using System.Xml.Serialization;
using System.Xml;
using System.Text.RegularExpressions;

namespace Lusitania.Simuladores.DataLayer.Common
{
    public static class XmlSerialization
    {
        private const string ENCODING = "utf-8";

        public static string ObjectToXml<T>(this T obj)
        {
            string xml = string.Empty;
            //Create our own namespaces for the output
            XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
            //Add an empty namespace and empty value
            ns.Add("", "");

            XmlWriterSettings xmlWriterSettings = new XmlWriterSettings();
            xmlWriterSettings.Indent = true;
            xmlWriterSettings.CheckCharacters = false;
            //string encoding = "utf-8";//"ISO-8859-1";
            xmlWriterSettings.Encoding = Encoding.GetEncoding(ENCODING);

            using (MemoryStream memoryStream = new MemoryStream())
            using (XmlWriter xmlWriter = XmlWriter.Create(memoryStream, xmlWriterSettings))
            {
                XmlSerializer x = new System.Xml.Serialization.XmlSerializer(typeof(T));
                x.Serialize(xmlWriter, obj, ns);

                // we just output back to the console for this demo.
                memoryStream.Position = 0; // rewind the stream before reading back.
                using (StreamReader sr = new StreamReader(memoryStream, Encoding.UTF8, true, 1024, true))
                {
                    xml = sr.ReadToEnd();
                } // note memory stream disposed by StreamReaders Dispose()
            }
           
            return xml;
        }
        //http://stackoverflow.com/questions/1879395/how-to-generate-a-stream-from-a-string
        public static Stream GenerateStreamFromString(string s)
        {
            MemoryStream stream = new MemoryStream();
            StreamWriter writer = new StreamWriter(stream);
            writer.Write(s);
            writer.Flush();
            stream.Position = 0;
            return stream;
        }

        public static T XMLToObject<T>(string Xml)
        {
            if (!string.IsNullOrEmpty(Xml))
            {

                //Remove Tags Vazias para evitar erro na desserialização
                Xml = Regex.Replace(Xml, @"<[a-zA-Z].[^(><.)]+/>","");
                
                T serviceMap = default(T);
                XmlSerializer serializer = new XmlSerializer(typeof(T));
                //string encoding = "utf-8";//"ISO-8859-1";
                using (StreamReader streamReader = new StreamReader(GenerateStreamFromString(Xml), Encoding.GetEncoding(ENCODING), true))
                {
                    XmlReaderSettings settings = new XmlReaderSettings();
                    settings.IgnoreComments = true;

                    using (XmlReader reader = XmlReader.Create(streamReader, settings))
                    {
                        serviceMap = (T)serializer.Deserialize(reader);
                        return serviceMap;
                    }
                }
                
            }

            return default(T);
        }

    }
}
