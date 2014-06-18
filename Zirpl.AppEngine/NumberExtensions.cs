using System;
using System.Globalization;

namespace Zirpl.AppEngine
{
    public static class NumberExtensions
    {
        public static string ConvertToAlternateBase(this long value, char[] baseChars)
        {
            if (value <= 0)
            {
                throw new ArgumentOutOfRangeException("value", "Must be greater than 0");
            }

            string result = string.Empty;
            int targetBase = baseChars.Length;

            long newValue = value;
            do
            {
                result = baseChars[newValue % targetBase] + result;
                newValue = newValue / targetBase;
            }
            while (newValue > 0);

            return result;
        }

        public static string ConvertToAlternateBase(this int value, char[] baseChars)
        {
            if (value <= 0)
            {
                throw new ArgumentOutOfRangeException("value", "Must be greater than 0");
            }

            string result = string.Empty;
            int targetBase = baseChars.Length;

            long newValue = value;
            do
            {
                result = baseChars[newValue % targetBase] + result;
                newValue = newValue / targetBase;
            }
            while (newValue > 0);

            return result;
        }

        public static string ConvertToAlternateBase(this short value, char[] baseChars)
        {
            if (value <= 0)
            {
                throw new ArgumentOutOfRangeException("value", "Must be greater than 0");
            }

            string result = string.Empty;
            int targetBase = baseChars.Length;

            long newValue = value;
            do
            {
                result = baseChars[newValue % targetBase] + result;
                newValue = newValue / targetBase;
            }
            while (newValue > 0);

            return result;
        }

        public static int DecimalPlacesCount(this decimal value)
        {
            //Get the decimal separator the specified culture
            char[] sep = CultureInfo.InvariantCulture.NumberFormat.NumberDecimalSeparator.ToCharArray();

            //Split the string on the separator 
            string[] segments = value.ToString(CultureInfo.InvariantCulture).Split(sep);

            switch (segments.Length)
            {
                //Only one segment, so there was not fractional value - return 0
                case 1:
                    return 0;
                //Two segments, so return the length of the second segment
                case 2:
                    return segments[1].TrimEnd('0').Length;

                //More than two segments means it's invalid, so throw an exception
                default:
                    throw new Exception("Something bad happened!");
            }
        }

        public static int DecimalPlacesCount(this double value)
        {
            //Get the decimal separator the specified culture
            char[] sep = CultureInfo.InvariantCulture.NumberFormat.NumberDecimalSeparator.ToCharArray();

            //Split the string on the separator 
            string[] segments = value.ToString(CultureInfo.InvariantCulture).Split(sep);

            switch (segments.Length)
            {
                //Only one segment, so there was not fractional value - return 0
                case 1:
                    return 0;
                //Two segments, so return the length of the second segment
                case 2:
                    return segments[1].TrimEnd('0').Length;

                //More than two segments means it's invalid, so throw an exception
                default:
                    throw new Exception("Something bad happened!");
            }
        }

        public static int DecimalPlacesCount(this float value)
        {
            //Get the decimal separator the specified culture
            char[] sep = CultureInfo.InvariantCulture.NumberFormat.NumberDecimalSeparator.ToCharArray();

            //Split the string on the separator 
            string[] segments = value.ToString(CultureInfo.InvariantCulture).Split(sep);

            switch (segments.Length)
            {
                //Only one segment, so there was not fractional value - return 0
                case 1:
                    return 0;
                //Two segments, so return the length of the second segment
                case 2:
                    return segments[1].TrimEnd('0').Length;

                //More than two segments means it's invalid, so throw an exception
                default:
                    throw new Exception("Something bad happened!");
            }
        }

        public static bool IsWholeNumber(this decimal value)
        {
            return value % 1 == 0;
        }
        public static decimal RoundUpCurrency(this decimal value)
        {
            return Decimal.Divide(Decimal.Ceiling(Decimal.Multiply(value, 100)), 100);
        }

        public static decimal RoundDownCurrency(this decimal value)
        {
            return Decimal.Divide(Decimal.Floor(Decimal.Multiply(value, 100)), 100);
        }

        public static int ToCents(this decimal value)
        {
            return Convert.ToInt32(Decimal.Multiply(value, 100));
        }

        public static decimal FromCentsToCurrency(this int cents)
        {
            return decimal.Divide(cents, 100);
        }
    }
}
