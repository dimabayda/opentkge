using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text;
using System.Xml;

namespace DB.GameEngine.Utils
{
    public static class XmlHelper
    {
        private static Type[] subclasses;
        private static XmlWriterSettings settings;

        static XmlHelper()
        {
            subclasses = GetSubclasses();
            settings = new XmlWriterSettings { Indent = true, OmitXmlDeclaration = true };
        }

        public static void SaveToXml<T>(T obj, string fileName)
        {        
            DataContractSerializer formatter = new DataContractSerializer(typeof(T), subclasses);

            using (XmlWriter writer = XmlWriter.Create(fileName, settings))
            {
                formatter.WriteObject(writer, obj);
            }
        }

        public static T LoadFromXml<T>(string fileName)
        {
            DataContractSerializer formatter = new DataContractSerializer(typeof(T), subclasses);

            using (XmlReader reader = XmlReader.Create(fileName))
            {
                return (T)formatter.ReadObject(reader);
            }
        }

        private static Type[] GetSubclasses()
        {
            if (subclasses == null)
            {
                Assembly assembly = Assembly.GetExecutingAssembly();
                Type[] types = assembly.GetTypes();
                subclasses = types.Where(t =>
                    t.IsDefined(typeof(DataContractAttribute))
                ).ToArray();
            }
            return subclasses;
        }
    }
}
