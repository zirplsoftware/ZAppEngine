using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zirpl
{
    public static class DateTimeExtensions
    {
        public static TimeSpan FromNow(this DateTime dateTime)
        {
            var now = DateTime.Now;
            return now.Subtract(dateTime);
        }
    }
}
