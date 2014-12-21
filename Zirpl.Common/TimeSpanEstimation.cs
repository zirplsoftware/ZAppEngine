using System;

namespace Zirpl
{
    public struct TimeSpanEstimation
    {
        private readonly TimeSpan _timeSpan;
        private readonly long _value;
        private readonly TimeSpanEstimationType _type;

        public TimeSpanEstimation(TimeSpan timeSpan)
        {
            _timeSpan = timeSpan;
            _value = 0;
            if (timeSpan.TotalMilliseconds < 1000)
            {
                _value = 0;
                _type = TimeSpanEstimationType.Seconds;
            }
            else if (timeSpan.TotalSeconds <= 60)
            {
                _value = Convert.ToInt64(timeSpan.TotalSeconds);
                _type = TimeSpanEstimationType.Seconds;
            }
            else if (timeSpan.TotalMinutes <= 60)
            {
                _value = Convert.ToInt64(timeSpan.TotalMinutes);
                _type = TimeSpanEstimationType.Minutes;
            }
            else if (timeSpan.TotalHours <= 24)
            {
                _value = Convert.ToInt64(timeSpan.TotalHours);
                _type = TimeSpanEstimationType.Hours;
            }
            else if (timeSpan.TotalDays <= 7)
            {
                _value = Convert.ToInt64(timeSpan.TotalDays);
                _type = TimeSpanEstimationType.Days;
            }
            else if (timeSpan.TotalDays <= 30)
            {
                _value = Convert.ToInt64(timeSpan.TotalDays) / 7;
                _type = TimeSpanEstimationType.Weeks;
            }
            else if (timeSpan.TotalDays <= 365)
            {
                _value = Convert.ToInt64(timeSpan.TotalDays) / 30;
                _type = TimeSpanEstimationType.Months;
            }
            else
            {
                _value = Convert.ToInt64(timeSpan.TotalDays) / 365;
                _type = TimeSpanEstimationType.Years;
            }
        }

        public long Value { get { return this._value; } }
        public TimeSpanEstimationType Type { get { return this._type; } }
        public TimeSpan TimeSpan { get { return this._timeSpan; } }
    }
}
