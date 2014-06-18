using System;
using System.IO;
using System.Reflection;
using Zirpl.AppEngine.Core.IO;

namespace Zirpl.AppEngine.Core.Reflection
{
    public static class AssemblyExtensions
    {
        public static byte[] GetManifestResourceBytes(this Assembly assembly, String resourcePath)
        {
            byte[] bytes = null;
            using (var stream = assembly.GetManifestResourceStream(resourcePath))
            {
                if (stream == null)
                {
                    throw new ArgumentException("Could not find embedded resource " + resourcePath, "resourcePath");
                }
                bytes = stream.ReadAllToBytes();
            }
            return bytes;
        }

        public static String GetManifestResourceText(this Assembly assembly, String resourcePath)
        {
            string text = null;
            using (var stream = assembly.GetManifestResourceStream(resourcePath))
            {
                if (stream == null)
                {
                    throw new ArgumentException("Could not find embedded resource " + resourcePath, "resourcePath");
                }
                using (var reader = new StreamReader(stream))
                {
                    text = reader.ReadToEnd();
                }
            }
            return text;
        }
    }
}
