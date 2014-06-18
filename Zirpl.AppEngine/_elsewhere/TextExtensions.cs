using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Zirpl.AppEngine._elsewhere
{
    public static class TextExtensions
    {
        private const String DEFAULT_EMAIL_SEPARATOR = ", ";

        public static String ToEmailListString(this IEnumerable<String> emailAddresses, String separator = null)
        {
            StringBuilder sb = new StringBuilder();
            if (emailAddresses != null)
            {
                foreach (String emailAddress in emailAddresses)
                {
                    if (sb.Length > 0)
                    {
                        sb.Append(separator ?? DEFAULT_EMAIL_SEPARATOR);
                    }
                    sb.Append(emailAddress);
                }
            }
            return sb.ToString();
        }

        public static IEnumerable<String> ToEmailList(this String emailAddresses, String separator = null)
        {
            var list = new List<string>();
            if (!String.IsNullOrEmpty(emailAddresses))
            {
                var addresses = emailAddresses.Split(new string[] { separator ?? DEFAULT_EMAIL_SEPARATOR }, StringSplitOptions.RemoveEmptyEntries);
                foreach (var address in addresses)
                {
                    list.Add(address);
                }
            }
            return list;
        }
        public static List<Int16> ParseToInt16List(this String ids, char tokenizer)
        {
            List<Int16> idList = new List<Int16>();
            if (!String.IsNullOrEmpty(ids))
            {
                String[] tokens = ids.Split(tokenizer);
                foreach (String token in tokens)
                {
                    idList.Add(Int16.Parse(token));
                }
            }
            return idList;
        }

        public static List<int> ParseToInt32List(String ids, char tokenizer)
        {
            List<int> idList = new List<int>();
            if (!String.IsNullOrEmpty(ids))
            {
                String[] tokens = ids.Split(tokenizer);
                foreach (String token in tokens)
                {
                    idList.Add(Int32.Parse(token));
                }
            }
            return idList;
        }

        public static List<Int64> ParseToInt64List(String ids, char tokenizer)
        {
            List<Int64> idList = new List<Int64>();
            if (!String.IsNullOrEmpty(ids))
            {
                String[] tokens = ids.Split(tokenizer);
                foreach (String token in tokens)
                {
                    idList.Add(Int64.Parse(token));
                }
            }
            return idList;
        }
    }
}
