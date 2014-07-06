using System;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
#if !PORTABLE
using System.Security.Cryptography;
#endif
using System.Text;

namespace Zirpl
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
#if !PORTABLE
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
#endif

        /// <summary>
        /// Converts to Camel casing.
        /// "FooBar" becomes "fooBar"
        /// "Foobar becomes "foobar"
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static string ToCamelCase(this String source)
        {
            return source.First().ToString().ToLower() + String.Join("", source.Skip(1));
        }
        /// <summary>
        /// Converts to pascal casing.
        /// "fooBar" becomes "FooBar"
        /// "foobar" becomes "Foobar"
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static string ToPascalCase(this String source)
        {
            return source.First().ToString().ToUpper() + String.Join("", source.Skip(1));
        }

        /// <summary>
        /// Parses a camel cased or pascal cased string and returns an array of the words within the string.
        /// </summary>
        /// <example>
        /// The string "PascalCasing" will return an array with two elements, "Pascal" and "Casing".
        /// </example>
        /// <param name="source">The string that is camel cased that needs to be split</param>
        /// <returns>An arry of each word part</returns>
        public static string[] SplitCamelOrPascalCase(this string source)
        {
            if (source == null)
                return new string[] { }; //Return empty array.

            if (source.Length == 0)
                return new string[] { "" };

            StringCollection words = new StringCollection();
            int wordStartIndex = 0;

            char[] letters = source.ToCharArray();
            // Skip the first letter. we don't care what case it is.
            for (int i = 1; i < letters.Length; i++)
            {
                if (char.IsUpper(letters[i]))
                {
                    //Grab everything before the current index.
                    words.Add(new String(letters, wordStartIndex, i - wordStartIndex));
                    wordStartIndex = i;
                }
            }

            //We need to have the last word.
            words.Add(new String(letters, wordStartIndex, letters.Length - wordStartIndex));

            //Copy to a string array.
            string[] wordArray = new string[words.Count];
            words.CopyTo(wordArray, 0);
            return wordArray;
        }

        ///// <summary>
        ///// Parses a camel cased or pascal cased string and returns a new string with spaces between the words in the string.
        ///// </summary>
        ///// <example>
        ///// The string "PascalCasing" will return an array with two elements, "Pascal" and "Casing".
        ///// </example>
        ///// <param name="source">The string that is camel cased that needs to be split</param>
        ///// <returns>A string with spaces between each word part</returns>
        //public static string ToCamelCase(this string[] source)
        //{
        //    return string.Join("", SplitCamelOrPascalCase(source));
        //}
    }
}
