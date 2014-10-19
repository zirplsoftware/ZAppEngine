using System;

namespace Zirpl
{
    public class TimeAgo
    {
        public TimeAgo()
        {

        }

        public TimeAgo(int value, TimeAgoType timeAgoType)
        {
            this.Value = value;
            this.TimeAgoType = timeAgoType;
        }

        public int Value { get; set; }
        public TimeAgoType TimeAgoType { get; set; }

        public static TimeAgo From(DateTime dateTime)
        {
            DateTime now = DateTime.Now;
            if (dateTime > now)
            {
                return new TimeAgo(0, TimeAgoType.Seconds);
            }
            else if (dateTime.AddMinutes(1) > now)
            {
                return new TimeAgo((int)Math.Round(now.Subtract(dateTime).TotalSeconds, 0), TimeAgoType.Seconds);
            }
            else if (dateTime.AddHours(1) > now)
            {
                return new TimeAgo((int)Math.Round(now.Subtract(dateTime).TotalMinutes, 0), TimeAgoType.Minutes);
            }
            else if (dateTime.AddDays(1) > now)
            {
                return new TimeAgo((int)Math.Round(now.Subtract(dateTime).TotalHours, 0), TimeAgoType.Hours);
            }
            else if (dateTime.AddDays(7) > now)
            {
                return new TimeAgo((int)Math.Round(now.Subtract(dateTime).TotalDays, 0), TimeAgoType.Days);
            }
            else if (dateTime.AddMonths(1) > now)
            {
                return new TimeAgo((int)Math.Round(now.Subtract(dateTime).TotalDays / 7, 0), TimeAgoType.Weeks);
            }
            else if (dateTime.AddYears(1) > now)
            {
                // NOTE: only an estimate since could be 31 or 28 also
                return new TimeAgo((int)Math.Round(now.Subtract(dateTime).TotalDays / 30, 0), TimeAgoType.Months);
            }
            else
            {
                // NOTE: only an estimate since could be 366 also
                return new TimeAgo((int)Math.Round(now.Subtract(dateTime).TotalDays / 365, 0), TimeAgoType.Years);
            }
        }
    }
}
