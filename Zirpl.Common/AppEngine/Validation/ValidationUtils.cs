using System;
using System.Collections.ObjectModel;
using System.Text;
using System.Text.RegularExpressions;

namespace Zirpl.AppEngine.Validation
{
    public static class ValidationUtils
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
        public static ValidationException ToValidationException(this String exceptionMessage, String propertyName = null, String errorMessage = null, Object entity = null)
        {
            ValidationException exception = null;
            if (entity != null)
            {
                exception = ToValidationException(exceptionMessage, new EntityValidationError(propertyName, errorMessage, entity));
            }
            else
            {
                exception = ToValidationException(exceptionMessage, new ValidationError(propertyName, errorMessage));
            }
            return exception;
        }

        public static ValidationException ToValidationException(this String exceptionMessage, ValidationError error)
        {
            var errors = new Collection<ValidationError>();
            errors.Add(error);
            return new ValidationException(exceptionMessage, errors);
        }

        public static ValidationException ToValidationException(this ValidationError error, String exceptionMessage)
        {
            var errors = new Collection<ValidationError>();
            errors.Add(error);
            return new ValidationException(exceptionMessage, errors);
        }

        public static String ToValidationErrorString(this ValidationException e)
        {
            StringBuilder sb = new StringBuilder();
            foreach (var error in e.ValidationErrors)
            {
                if (sb.Length > 0)
                {
                    sb.AppendLine();
                }
                sb.Append(error.ErrorMessage);
            }
            return sb.ToString();
        }
    }
}
