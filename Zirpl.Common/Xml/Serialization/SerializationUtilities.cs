#if !SILVERLIGHT && !PORTABLE
using System;
using System.IO;
using System.Xml.Serialization;

namespace Zirpl.Xml.Serialization
{
    public static class SerializationUtilities
    {
        public static void SerializeToFile<T>(this T entity, String filePath, String rootNodeName = null)
        {
            var serializer = String.IsNullOrEmpty(rootNodeName)
                ? new XmlSerializer(typeof(T))
                : new XmlSerializer(typeof(T), new XmlRootAttribute(rootNodeName));

            if (!Directory.Exists(Path.GetDirectoryName(filePath)))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(filePath));
            }
            using (var fileStream = new FileStream(filePath, FileMode.Create, FileAccess.Write))
            {
                serializer.Serialize(fileStream, entity);
            }
        }

        public static T DeserializeFromFile<T>(String filePath, String rootNodeName = null)
        {
            var serializer = String.IsNullOrEmpty(rootNodeName) 
                ? new XmlSerializer(typeof(T)) 
                : new XmlSerializer(typeof(T), new XmlRootAttribute(rootNodeName));

            T entity;
            using (var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            {
                entity = (T) serializer.Deserialize(fileStream);
            }

            return entity;
        }
    }
}
#endif