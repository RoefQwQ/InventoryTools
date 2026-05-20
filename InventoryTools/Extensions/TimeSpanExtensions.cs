using System;

namespace InventoryTools.Extensions
{
    public static class TimespanExtensions
    {
        public static string ToHumanReadableString (this TimeSpan t)
        {
            if (t.TotalMinutes <= 1) {
                return $@"{t.TotalSeconds} 秒";
            }
            if (t.TotalHours <= 1) {
                return $@"{t.TotalMinutes} 分钟";
            }
            if (t.TotalDays <= 1) {
                return $@"{t.TotalHours} 小时";
            }

            return $@"{t.TotalDays} 天";
        }
    }
}