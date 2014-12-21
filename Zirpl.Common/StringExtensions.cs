using System;
using System.Collections.Generic;
using System.Globalization;
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
        public static string Or(this string text, string alternative)
        {
            return string.IsNullOrWhiteSpace(text) ? alternative : text;
        }
        public static string OrEmpty(this string text)
        {
            return string.IsNullOrWhiteSpace(text) ? String.Empty : text;
        }

        public static bool IsNull(this string text)
        {
            return text == null;
        }

        public static bool IsNullOrEmpty(this string text)
        {
            return String.IsNullOrEmpty(text);
        }

        public static bool IsNullOrWhiteSpace(this string text)
        {
            return String.IsNullOrWhiteSpace(text);
        }

        public static bool HasContent(this string text)
        {
            return !text.IsNullOrWhiteSpace();
        }

        public static String SubstringUntilNthInstanceOf(this string originalText, String search, int nthInstance,      
#if !PORTABLE
            StringComparison stringComparison = StringComparison.InvariantCulture)
#else
            StringComparison stringComparison = StringComparison.CurrentCulture)
#endif
        {
            if (nthInstance < 1)
            {
                throw new ArgumentOutOfRangeException("nthInstance", "Must be 1 or greater");
            }
            int startIndex = 0;
            int foundIndex = -1;
            for (int i = 1; i <= nthInstance; i++)
            {
                if (startIndex >= originalText.Length)
                {
                    foundIndex = -1;
                    break;
                }
                foundIndex = originalText.IndexOf(search, startIndex, stringComparison);
                if (foundIndex == -1)
                {
                    break;
                }
                startIndex = foundIndex + 1;
            }

            if (foundIndex == -1)
            {
                return originalText;
            }
            else if (foundIndex == 0)
            {
                return String.Empty;
            }
            return originalText.Substring(0, foundIndex);
        }
        public static String SubstringThroughNthInstanceOf(this string originalText, String search, int nthInstance,
#if !PORTABLE
            StringComparison stringComparison = StringComparison.InvariantCulture)
#else
 StringComparison stringComparison = StringComparison.CurrentCulture)
#endif
        {
            if (nthInstance < 1)
            {
                throw new ArgumentOutOfRangeException("nthInstance", "Must be 1 or greater");
            }
            int startIndex = 0;
            int foundIndex = -1;
            for (int i = 1; i <= nthInstance; i++)
            {
                if (startIndex >= originalText.Length)
                {
                    foundIndex = -1;
                    break;
                }
                foundIndex = originalText.IndexOf(search, startIndex, stringComparison);
                if (foundIndex == -1)
                {
                    break;
                }
                startIndex = foundIndex + 1;
            }

            if (foundIndex == -1)
            {
                return originalText;
            }
            return originalText.Substring(0, foundIndex + search.Length);
        }
        public static String SubstringFromNthInstanceOf(this string originalText, String search, int nthInstance, 
#if !PORTABLE
            StringComparison stringComparison = StringComparison.InvariantCulture)
#else
            StringComparison stringComparison = StringComparison.CurrentCulture)
#endif
        {
            if (nthInstance < 1)
            {
                throw new ArgumentOutOfRangeException("nthInstance", "Must be 1 or greater");
            }
            int startIndex = 0;
            int foundIndex = -1;
            for (int i = 1; i <= nthInstance; i++)
            {
                if (startIndex >= originalText.Length)
                {
                    foundIndex = -1;
                    break;
                }
                foundIndex = originalText.IndexOf(search, startIndex, stringComparison);
                if (foundIndex == -1)
                {
                    break;
                }
                startIndex = foundIndex + 1;
            }

            if (foundIndex == -1)
            {
                return String.Empty;
            }
            return originalText.Substring(foundIndex);
        }
        public static String SubstringAfterNthInstanceOf(this string originalText, String search, int nthInstance,
#if !PORTABLE
            StringComparison stringComparison = StringComparison.InvariantCulture)
#else
 StringComparison stringComparison = StringComparison.CurrentCulture)
#endif
        {
            if (nthInstance < 1)
            {
                throw new ArgumentOutOfRangeException("nthInstance", "Must be 1 or greater");
            }
            int startIndex = 0;
            int foundIndex = -1;
            for (int i = 1; i <= nthInstance; i++)
            {
                if (startIndex >= originalText.Length)
                {
                    foundIndex = -1;
                    break;
                }
                foundIndex = originalText.IndexOf(search, startIndex, stringComparison);
                if (foundIndex == -1)
                {
                    break;
                }
                startIndex = foundIndex + 1;
            }

            if (foundIndex == -1)
            {
                return String.Empty;
            }
            else if (foundIndex + search.Length == originalText.Length)
            {
                return String.Empty;
            }
            return originalText.Substring(foundIndex + search.Length);
        }




        //public static String SubstringUntilLastNthInstanceOf(this string originalText, String search, int nthInstance, 
#if !PORTABLE
//            StringComparison stringComparison = StringComparison.InvariantCulture)
#else
//            StringComparison stringComparison = StringComparison.CurrentCulture)
#endif
        //{
        //    if (nthInstance < 1)
        //    {
        //        throw new ArgumentOutOfRangeException("nthInstance", "Must be 1 or greater");
        //    }
        //    int startIndex = 0;
        //    int foundIndex = -1;
        //    for (int i = 1; i <= nthInstance; i++)
        //    {
        //        if (startIndex >= originalText.Length)
        //        {
        //            foundIndex = -1;
        //            break;
        //        }
        //        foundIndex = originalText.LastIndexOf(search, startIndex, stringComparison);
        //        if (foundIndex == -1)
        //        {
        //            break;
        //        }
        //        startIndex = foundIndex + 1;
        //    }

        //    if (foundIndex == -1)
        //    {
        //        return originalText;
        //    }
        //    else if (foundIndex == 0)
        //    {
        //        return String.Empty;
        //    }
        //    return originalText.Substring(0, foundIndex);
        //}
        //public static String SubstringThroughLastNthInstanceOf(this string originalText, String search, int nthInstance
#if !PORTABLE
//            StringComparison stringComparison = StringComparison.InvariantCulture)
#else
        //            StringComparison stringComparison = StringComparison.CurrentCulture)
#endif
        //{
        //    if (nthInstance < 1)
        //    {
        //        throw new ArgumentOutOfRangeException("nthInstance", "Must be 1 or greater");
        //    }
        //    int startIndex = 0;
        //    int foundIndex = -1;
        //    for (int i = 1; i <= nthInstance; i++)
        //    {
        //        if (startIndex >= originalText.Length)
        //        {
        //            foundIndex = -1;
        //            break;
        //        }
        //        foundIndex = originalText.LastIndexOf(search, startIndex, stringComparison);
        //        if (foundIndex == -1)
        //        {
        //            break;
        //        }
        //        startIndex = foundIndex + 1;
        //    }

        //    if (foundIndex == -1)
        //    {
        //        return originalText;
        //    }
        //    return originalText.Substring(0, foundIndex + search.Length);
        //}
        //public static String SubstringFromLastNthInstanceOf(this string originalText, String search, int nthInstance
#if !PORTABLE
//            StringComparison stringComparison = StringComparison.InvariantCulture)
#else
        //            StringComparison stringComparison = StringComparison.CurrentCulture)
#endif
        //{
        //    if (nthInstance < 1)
        //    {
        //        throw new ArgumentOutOfRangeException("nthInstance", "Must be 1 or greater");
        //    }
        //    int startIndex = 0;
        //    int foundIndex = -1;
        //    for (int i = 1; i <= nthInstance; i++)
        //    {
        //        if (startIndex >= originalText.Length)
        //        {
        //            foundIndex = -1;
        //            break;
        //        }
        //        foundIndex = originalText.LastIndexOf(search, startIndex, stringComparison);
        //        if (foundIndex == -1)
        //        {
        //            break;
        //        }
        //        startIndex = foundIndex + 1;
        //    }

        //    if (foundIndex == -1)
        //    {
        //        return String.Empty;
        //    }
        //    return originalText.Substring(foundIndex);
        //}
        //public static String SubstringAfterLastNthInstanceOf(this string originalText, String search, int nthInstance
#if !PORTABLE
//            StringComparison stringComparison = StringComparison.InvariantCulture)
#else
        //            StringComparison stringComparison = StringComparison.CurrentCulture)
#endif
        //{
        //    if (nthInstance < 1)
        //    {
        //        throw new ArgumentOutOfRangeException("nthInstance", "Must be 1 or greater");
        //    }
        //    int startIndex = 0;
        //    int foundIndex = -1;
        //    for (int i = 1; i <= nthInstance; i++)
        //    {
        //        if (startIndex >= originalText.Length)
        //        {
        //            foundIndex = -1;
        //            break;
        //        }
        //        foundIndex = originalText.LastIndexOf(search, startIndex, stringComparison);
        //        if (foundIndex == -1)
        //        {
        //            break;
        //        }
        //        startIndex = foundIndex + 1;
        //    }

        //    if (foundIndex == -1)
        //    {
        //        return String.Empty;
        //    }
        //    else if (foundIndex + search.Length == originalText.Length)
        //    {
        //        return String.Empty;
        //    }
        //    return originalText.Substring(foundIndex + search.Length);
        //}





        public static String SubstringUntilLastInstanceOf(this string originalText, String search,
#if !PORTABLE
            StringComparison stringComparison = StringComparison.InvariantCulture)
#else
 StringComparison stringComparison = StringComparison.CurrentCulture)
#endif
        {
            var index = originalText.LastIndexOf(search, stringComparison);
            if (index == -1)
            {
                return originalText;
            }
            else if (index == 0)
            {
                return String.Empty;
            }
            return originalText.Substring(0, index);
        }
        public static String SubstringThroughLastInstanceOf(this string originalText, String search,
#if !PORTABLE
            StringComparison stringComparison = StringComparison.InvariantCulture)
#else
 StringComparison stringComparison = StringComparison.CurrentCulture)
#endif
        {
            var index = originalText.LastIndexOf(search, stringComparison);
            if (index == -1)
            {
                return originalText;
            }
            else if (index + search.Length == originalText.Length)
            {
                return originalText;
            }
            return originalText.Substring(0, index + search.Length);
        }
        public static String SubstringFromLastInstanceOf(this string originalText, String search,
#if !PORTABLE
            StringComparison stringComparison = StringComparison.InvariantCulture)
#else
 StringComparison stringComparison = StringComparison.CurrentCulture)
#endif
        {
            var index = originalText.LastIndexOf(search, stringComparison);
            if (index == -1)
            {
                return String.Empty;
            }
            else
            {
                return originalText.Substring(index);
            }
        }
        public static String SubstringAfterLastInstanceOf(this string originalText, String search,
#if !PORTABLE
            StringComparison stringComparison = StringComparison.InvariantCulture)
#else
 StringComparison stringComparison = StringComparison.CurrentCulture)
#endif
        {
            var index = originalText.LastIndexOf(search, stringComparison);
            if (index == -1)
            {
                return String.Empty;
            }
            else if (index + search.Length == originalText.Length)
            {
                return String.Empty;
            }
            return originalText.Substring(index + search.Length);
        }
        public static String SubstringUntilFirstInstanceOf(this string originalText, String search,
#if !PORTABLE
            StringComparison stringComparison = StringComparison.InvariantCulture)
#else
 StringComparison stringComparison = StringComparison.CurrentCulture)
#endif
        {
            var index = originalText.IndexOf(search, stringComparison);
            if (index == -1)
            {
                return originalText;
            }
            else if (index == 0)
            {
                return String.Empty;
            }
            return originalText.Substring(0, index);
        }
        public static String SubstringThroughFirstInstanceOf(this string originalText, String search,
#if !PORTABLE
            StringComparison stringComparison = StringComparison.InvariantCulture)
#else
 StringComparison stringComparison = StringComparison.CurrentCulture)
#endif
        {
            var index = originalText.IndexOf(search, stringComparison);
            if (index == -1)
            {
                return originalText;
            }
            else if (index + search.Length == originalText.Length)
            {
                return originalText;
            }
            return originalText.Substring(0, index + search.Length);
        }
        public static String SubstringFromFirstInstanceOf(this string originalText, String search,
#if !PORTABLE
            StringComparison stringComparison = StringComparison.InvariantCulture)
#else
 StringComparison stringComparison = StringComparison.CurrentCulture)
#endif
        {
            var index = originalText.IndexOf(search, stringComparison);
            if (index == -1)
            {
                return String.Empty;
            }
            else
            {
                return originalText.Substring(index);
            }
        }
        public static String SubstringAfterFirstInstanceOf(this string originalText, String search,
#if !PORTABLE
            StringComparison stringComparison = StringComparison.InvariantCulture)
#else
 StringComparison stringComparison = StringComparison.CurrentCulture)
#endif
        {
            var index = originalText.IndexOf(search, stringComparison);
            if (index == -1)
            {
                return String.Empty;
            }
            else if (index + search.Length == originalText.Length)
            {
                return String.Empty;
            }
            return originalText.Substring(index + search.Length);
        }
        public static String Replace(this String originalText, int startIndex, int length, String newToken)
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

        public static String ReplaceAtStart(this String originalText, String searchToken, String replacementText,
#if !PORTABLE
            StringComparison stringComparison = StringComparison.InvariantCulture)
#else
 StringComparison stringComparison = StringComparison.CurrentCulture)
#endif
        {
            if (!String.IsNullOrEmpty(originalText)
                && originalText.StartsWith(searchToken, stringComparison))
            {
                return replacementText + (originalText.Length > searchToken.Length ? originalText.Substring(searchToken.Length) : null);
            }
            return originalText;
        }
        public static String ReplaceFirstInstanceOf(this String originalText, String searchToken, String replacementText,
#if !PORTABLE
 StringComparison stringComparison = StringComparison.InvariantCulture)
#else
 StringComparison stringComparison = StringComparison.CurrentCulture)
#endif
        {
            if (!String.IsNullOrEmpty(originalText))
            {
                var index = originalText.IndexOf(searchToken, stringComparison);
                if (index == -1)
                {
                    return originalText;
                }
                else if (index == 0)
                {
                    return ReplaceAtStart(originalText, searchToken, replacementText, stringComparison);
                }
                else if (index + searchToken.Length == originalText.Length)
                {
                    return ReplaceAtEnd(originalText, searchToken, replacementText, stringComparison);
                }
                else
                {
                    return originalText.Substring(0, index) + replacementText +
                           originalText.Substring(index + searchToken.Length);
                }
            }
            return originalText;
        }

        public static String ReplaceAtEnd(this String originalText, String searchToken, String replacementText,
#if !PORTABLE
 StringComparison stringComparison = StringComparison.InvariantCulture)
#else
 StringComparison stringComparison = StringComparison.CurrentCulture)
#endif
        {
            if (!String.IsNullOrEmpty(originalText)
                && originalText.EndsWith(searchToken, stringComparison))
            {
                return (originalText.Length > searchToken.Length ? originalText.Substring(0, originalText.Length - searchToken.Length) : null) + replacementText;
            }
            return originalText;
        }

        public static String JoinToString(this IEnumerable<String> tokens, String token)
        {
            if (tokens == null)
            {
                return null;
            }
            else if (tokens.Count() == 1)
            {
                return tokens.Single();
            }
            else
            {
                var sb = new StringBuilder();
                var tokensArray = tokens.ToArray();
                for (int i = 0; i < tokensArray.Length; i++)
                {
                    if (i > 0)
                    {
                        sb.Append(token);
                    }
                    sb.Append(tokensArray[i]);
                }
                return sb.ToString();
            }
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
            if (String.IsNullOrEmpty(source))
            {
                return source;
            }
            else if (source.Length == 1)
            {
                return source.ToLower();
            }
            else
            {
                return source[0].ToString().ToLower() + String.Join("", source.Substring(1));    
            }
        }
        
#if !PORTABLE
        /// <summary>
        /// Converts to Camel casing.
        /// "FooBar" becomes "fooBar"
        /// "Foobar becomes "foobar"
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static string ToCamelCase(this String source, CultureInfo cultureInfo)
        {
            if (String.IsNullOrEmpty(source))
            {
                return source;
            }
            else if (source.Length == 1)
            {
                return source.ToLower(cultureInfo);
            }
            else
            {
                return source[0].ToString().ToLower(cultureInfo) + String.Join("", source.Substring(1));
            }
        }

#endif
        /// <summary>
        /// Converts to pascal casing.
        /// "fooBar" becomes "FooBar"
        /// "foobar" becomes "Foobar"
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static string ToPascalCase(this String source)
        {
            if (String.IsNullOrEmpty(source))
            {
                return source;
            }
            else if (source.Length == 1)
            {
                return source.ToUpper();
            }
            else
            {
                return source[0].ToString().ToUpper() + String.Join("", source.Substring(1));
            }
        }

#if !PORTABLE
        /// <summary>
        /// Converts to pascal casing.
        /// "fooBar" becomes "FooBar"
        /// "foobar" becomes "Foobar"
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static string ToPascalCase(this String source, CultureInfo cultureInfo)
        {
            if (String.IsNullOrEmpty(source))
            {
                return source;
            }
            else if (source.Length == 1)
            {
                return source.ToUpper(cultureInfo);
            }
            else
            {
                return source[0].ToString().ToUpper(cultureInfo) + String.Join("", source.Substring(1));
            }
        }
#endif
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

            List<String> words = new List<String>();
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
