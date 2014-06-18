using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Zirpl.AppEngine._elsewhere
{
    public static class TextExtensions
    {
        //public const String VALID_EMAIL_ADDRESS_REGULAR_EXPRESSION = @"\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*";
        //public const String VALID_EMAIL_ADDRESS_REGULAR_EXPRESSION = @"^(?("")(""[^""]+?""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9]{2,17}))$";

        // FROM: http://hexillion.com/samples/#Regex
        public const String VALID_EMAIL_ADDRESS_REGULAR_EXPRESSION = @"^(?:[\w\!\#\$\%\&\'\*\+\-\/\=\?\^\`\{\|\}\~]+\.)*[\w\!\#\$\%\&\'\*\+\-\/\=\?\^\`\{\|\}\~]+@(?:(?:(?:[a-zA-Z0-9](?:[a-zA-Z0-9\-](?!\.)){0,61}[a-zA-Z0-9]?\.)+[a-zA-Z0-9](?:[a-zA-Z0-9\-](?!$)){0,61}[a-zA-Z0-9]?)|(?:\[(?:(?:[01]?\d{1,2}|2[0-4]\d|25[0-5])\.){3}(?:[01]?\d{1,2}|2[0-4]\d|25[0-5])\]))$";
        // FROM: http://www.itsalif.info/content/canada-postal-code-us-zip-code-regex
        public const String VALID_US_POSTAL_CODE_REGULAR_EXPRESSION = @"^\d{5}(-\d{4})?$";
        public const String VALID_CANADIAN_POSTAL_CODE_REGULAR_EXPRESSION = @"^[ABCEGHJKLMNPRSTVXY]\d[ABCEGHJKLMNPRSTVWXYZ]( )?\d[ABCEGHJKLMNPRSTVWXYZ]\d$";
        public const String VALID_US_OR_CANADIAN_POSTAL_CODE_REGULAR_EXPRESSION = @"(" + VALID_CANADIAN_POSTAL_CODE_REGULAR_EXPRESSION + ")|(" + VALID_US_POSTAL_CODE_REGULAR_EXPRESSION + ")";
        public const String VALID_US_POSTAL_CODE_REGULAR_EXPRESSION_EMPTY_PASSES = @"(^$)|(^\d{5}(-\d{4})?$)";
        private const String DEFAULT_EMAIL_SEPARATOR = ", ";

        public static bool IsValidEmailAddress(this String emailAddress)
        {
            //http://msdn.microsoft.com/en-us/library/01escwtf.aspx
            bool isValid = false;
            if (!String.IsNullOrEmpty(emailAddress))
            {
                // Return true if strIn is in valid e-mail format.
                isValid = Regex.IsMatch(emailAddress,
                    VALID_EMAIL_ADDRESS_REGULAR_EXPRESSION,
                    RegexOptions.IgnoreCase);
            }
            return isValid;
        }

        public static bool IsValidUSPostalCode(this String postalCode)
        {
            bool isValid = false;
            if (!String.IsNullOrEmpty(postalCode))
            {
                // Return true if strIn is in valid e-mail format.
                isValid = Regex.IsMatch(postalCode,
                    VALID_US_POSTAL_CODE_REGULAR_EXPRESSION,
                    RegexOptions.IgnoreCase);
            }
            return isValid;
        }

        public static bool IsValidCanadianPostalCode(this String postalCode)
        {
            bool isValid = false;
            if (!String.IsNullOrEmpty(postalCode))
            {
                // Return true if strIn is in valid e-mail format.
                isValid = Regex.IsMatch(postalCode,
                    VALID_CANADIAN_POSTAL_CODE_REGULAR_EXPRESSION,
                    RegexOptions.IgnoreCase);
            }
            return isValid;
        }

        public static bool IsValidUSOrCanadianPostalCode(this String postalCode)
        {
            bool isValid = false;
            if (!String.IsNullOrEmpty(postalCode))
            {
                // Return true if strIn is in valid e-mail format.
                isValid = Regex.IsMatch(postalCode,
                    VALID_US_OR_CANADIAN_POSTAL_CODE_REGULAR_EXPRESSION,
                    RegexOptions.IgnoreCase);
            }
            return isValid;
        }
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
