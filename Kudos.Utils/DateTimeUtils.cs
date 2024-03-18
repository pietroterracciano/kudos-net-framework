using System;

namespace Kudos.Utils
{
    public static class DateTimeUtils
    {
        public static DateTime GetOrigin()
        {
            return GetOrigin(DateTimeKind.Utc);
        }

        public static DateTime GetOrigin(DateTimeKind dtk)
        {
            return new DateTime(1970, 1, 1, 0, 0, 0, 0, dtk);
        }

        public static DateTime GetCurrent()
        {
            return GetCurrent(DateTimeKind.Utc);
        }

        public static DateTime GetCurrent(DateTimeKind dtk)
        {
            return
                dtk == DateTimeKind.Local
                    ? DateTime.Now.ToLocalTime()
                    : DateTime.Now.ToUniversalTime();
        }
    }
}