using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Zirpl.AppEngine.Text
{
    public static class TextUtilities
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
    }
}
