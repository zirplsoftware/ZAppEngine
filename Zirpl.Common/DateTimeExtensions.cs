using System;

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
