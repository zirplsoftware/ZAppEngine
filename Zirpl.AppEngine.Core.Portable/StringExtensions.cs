using System;
using System.IO;
using System.Text;

namespace Zirpl.AppEngine.Core
{
    public static class StringExtensions
    {
        public static String Substitute(this String originalText, int startIndex, int length, String newToken)
        {
            var sb = new StringBuilder();
            if (startIndex > 0)
            {
                sb.Append(originalText.Substring(0, startIndex));
            }
            sb.Append(newToken);
            if (startIndex + length < originalText.Length)
            {
                sb.Append(originalText.Substring(startIndex + length));
            }
            return sb.ToString();
        }

        public static string Base64Encode(this string toEncode)
        {
            var stream = new MemoryStream();
            var writer = new StreamWriter(stream);
            writer.Write(toEncode);
            writer.Flush();

            var buffer = new byte[stream.Length];
            stream.Position = 0;
            stream.Read(buffer, 0, (int)stream.Length);
            var base64Value = Convert.ToBase64String(buffer);

            return base64Value;
        }

        public static String LastXSubstring(this String text, int lengthX)
        {
            if (text == null)
            {
                throw new ArgumentNullException("text");
            }

            var actualLength = text.Length;
            var startIndex = actualLength - lengthX; // if actualLength = 5 (indices 0-4), lastXlength = 3, startIndex = 2
            if (startIndex < 0)
            {
                throw new ArgumentOutOfRangeException("lengthX", "lengthX specified is longer than string");
            }

            return text.Substring(startIndex, lengthX);
        }
    }
}
