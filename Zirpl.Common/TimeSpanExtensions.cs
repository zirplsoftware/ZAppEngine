using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zirpl
{
    public static class TimeSpanExtensions
    {
        public static TimeSpanEstimation GetEstimation(this TimeSpan timeSpan)
        {
            return new TimeSpanEstimation(timeSpan);
        }

        public static double TotalWeeks(this TimeSpan timeSpan)
        {
            return timeSpan.TotalDays/7d;
        }

        public static double ApproximateMonths(this TimeSpan timeSpan)
        {
            return timeSpan.TotalDays/(365d/12d);
        }

        public static double ApproximateYears(this TimeSpan timeSpan)
        {
            return timeSpan.TotalDays / 365d;
        }

        public static double ApproximateCenturies(this TimeSpan timeSpan)
        {
            return timeSpan.TotalDays / (36524.25d);
        }
    }
}
