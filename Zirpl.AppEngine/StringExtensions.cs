using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Zirpl.AppEngine
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
        public static String Hash(this String text, HashAlgorithm algorithm, Encoding encoding)
        {
            // http://weblogs.sqlteam.com/mladenp/archive/2009/04/28/Comparing-SQL-Server-HASHBYTES-function-and-.Net-hashing.aspx

            // dont use these 2 for comparing to SQL Server hashing:
            //byte[] bs = System.Text.Encoding.ASCII.GetBytes(input);
            //byte[] bs = System.Text.Encoding.UTF7.GetBytes(input);

            // these ones are fine
            //byte[] bs = System.Text.Encoding.UTF8.GetBytes(input); // best for varchar when comparing to SQL Server hashes
            //byte[] bs = System.Text.Encoding.UTF16.GetBytes(input); // best for nchar when comparing to SQL Server hashes
            //byte[] bs = System.Text.Encoding.UTF32.GetBytes(input); // UTF32 or Unicode required for complex chars
            //byte[] bs = System.Text.Encoding.Unicode.GetBytes(input); // UTF32 or Unicode required for complex chars

            byte[] bs = encoding.GetBytes(text);
            bs = algorithm.ComputeHash(bs);

            // TODO: this MAY be required... unsure
            //StringBuilder s = new StringBuilder();
            //foreach (byte b in bs)
            //{
            //    s.Append(b.ToString("x2").ToLower());
            //}

            return encoding.GetString(bs, 0, text.Length);
        }
    }
}
