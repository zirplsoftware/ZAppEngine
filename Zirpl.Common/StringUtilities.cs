using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zirpl
{
    public static class StringUtilities
    {
        public static String CreateStringLongerThan(long length)
        {

            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < length; i++)
            {
                sb.Append('a');
            }
            sb.Append('a'); // 1 more to be longer than
            return sb.ToString();
        }

        public static String CreateStringShorterThan(long length)
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
