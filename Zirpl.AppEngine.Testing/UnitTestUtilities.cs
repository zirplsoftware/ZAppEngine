using System;
using System.Text;

namespace Zirpl.Testing
{
    public static class UnitTestUtilities
    {
        public static String CreateStringLongerThan(this Object context, long length)
        {

            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < length; i++)
            {
                sb.Append('a');
            }
            sb.Append('a'); // 1 more to be longer than
            return sb.ToString();
        }

        public static String CreateStringShorterThan(this Object context, long length)
        {
            if (length == 0)
            {
                throw new ArgumentOutOfRangeException("length");
            }

            long stringLength = length - 1;
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < stringLength; i++)
            {
                sb.Append('a');
            }
            return sb.ToString();
        }
    }
}
