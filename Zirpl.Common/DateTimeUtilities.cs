using System;

namespace Zirpl
{
    public static class DateTimeUtilities
    {
        public static DateTime GetEpochBase()
        {
            return new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        }
    }
}
