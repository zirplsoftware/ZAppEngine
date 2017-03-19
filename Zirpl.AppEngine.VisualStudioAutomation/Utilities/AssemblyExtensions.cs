using System;
using System.IO;
using System.Reflection;

namespace Zirpl.AppEngine.VisualStudioAutomation.Utilities
{
    internal static class AssemblyExtensions
    {
        internal static byte[] GetManifestResourceBytes(this Assembly assembly, String resourcePath)
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

        internal static String GetManifestResourceText(this Assembly assembly, String resourcePath)
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
#if !PORTABLE
        internal static FileInfo WriteEmbeddedResourceToFile(this Assembly assembly, String resourcePath, String targetFilePath)
        {
            PathUtilities.EnsureDirectoryExists(targetFilePath);
            using (Stream input = assembly.GetManifestResourceStream(resourcePath))
            {
                using (var output = File.Create(targetFilePath))
                {
                    input.CopyStream(output);
                }
            }

            return new FileInfo(targetFilePath);
        }
#endif
    }
}
