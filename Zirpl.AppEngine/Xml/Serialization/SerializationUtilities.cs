using System;
using System.IO;
using System.Xml.Serialization;

namespace Zirpl.AppEngine.Xml.Serialization
{
    public static class SerializationUtilities
    {
        public static void SerializeToFile<T>(this T entity, String filePath, String rootNodeName = null)
        {
            FileStream fileStream = null;

            try
            {
                //LogManager.GetLog(typeof(XmlSerializationUtilities)).DebugFormat("Serializing {0} to {1}", typeof(T).Name, filePath);

                XmlSerializer serializer = null;
                if (String.IsNullOrEmpty(rootNodeName))
                {
                    serializer = new XmlSerializer(typeof(T));
                }
                else
                {
                    serializer = new XmlSerializer(typeof(T), new XmlRootAttribute(rootNodeName));
                }

                if (!Directory.Exists(Path.GetDirectoryName(filePath)))
                {
                    //LogManager.GetLog(typeof(XmlSerializationUtilities)).DebugFormat("Creating directory for {0} - {1}", typeof(T).Name, Path.GetDirectoryName(filePath));
                    Directory.CreateDirectory(Path.GetDirectoryName(filePath));
                }
                fileStream = new FileStream(filePath, FileMode.Create, FileAccess.Write);
                serializer.Serialize(fileStream, entity);
            }
            catch (Exception ex)
            {
                //LogManager.GetLog(typeof(XmlSerializationUtilities)).TryDebugFormat(ex, "Error attempting to serialize {0} to {1}", typeof(T).Name, filePath);
                throw;
            }
            finally
            {
                if (fileStream != null)
                {
                    try
                    {
                        fileStream.Dispose();
                    }
                    catch (Exception ex)
                    {
                        //LogManager.GetLog(typeof(XmlSerializationUtilities)).TryDebug(ex, "Error attempting to close FileStream");
                    }
                    fileStream = null;
                }
            }
        }

        public static T DeserializeFromFile<T>(String filePath, String rootNodeName = null)
        {
            FileStream fileStream = null;
            T entity = default(T);

            try
            {
                //LogManager.GetLog(typeof(XmlSerializationUtilities)).DebugFormat("Deserializing {0} to {1}", typeof(T).Name, filePath);

                XmlSerializer serializer = null;
                if (String.IsNullOrEmpty(rootNodeName))
                {
                    serializer = new XmlSerializer(typeof(T));
                }
                else
                {
                    serializer = new XmlSerializer(typeof(T), new XmlRootAttribute(rootNodeName));
                }

                fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
                entity = (T)serializer.Deserialize(fileStream);
            }
            catch (FileNotFoundException)
            {
                // while handlable, this is something we want to be notified of
                //
                //LogManager.GetLog(typeof(XmlSerializationUtilities)).TryDebugFormat("File {0} was not found", filePath);
            }
            catch (Exception ex)
            {
                //LogManager.GetLog(typeof(XmlSerializationUtilities)).TryDebugFormat(ex, "Error attempting to deserialize {0} from {1}", typeof(T).Name, filePath);
                throw;
            }
            finally
            {
                if (fileStream != null)
                {
                    try
                    {
                        fileStream.Dispose();
                    }
                    catch (Exception ex)
                    {
                        //LogManager.GetLog(typeof(XmlSerializationUtilities)).TryDebug(ex, "Error attempting to close FileStream");
                    }
                    fileStream = null;
                }
            }

            return entity;
        }
    }
}
