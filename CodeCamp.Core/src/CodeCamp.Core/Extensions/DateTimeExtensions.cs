using System;

namespace CodeCamp.Core.Extensions
{
    public static class DateTimeExtensions
    {
        public static string FormatTime(this DateTime utcTime)
        {
            return utcTime.ToLocalTime().ToString("t");
        }
    }
}
