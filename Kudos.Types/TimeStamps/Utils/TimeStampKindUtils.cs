using Kudos.Types.TimeStamps.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kudos.Types.TimeStamps.Utils
{
    internal static class TimeStampKindUtils
    {
        private static readonly String
            __sLocal = "LCL",
            __sUniversal = "UNV";

        private static readonly Dictionary<ETimeStampKind, String>
            __d0 = new Dictionary<ETimeStampKind, String>()
            {
                { ETimeStampKind.Local, __sLocal },
                { ETimeStampKind.Universal, __sUniversal }
            };

        private static readonly Dictionary<ETimeStampKind, DateTimeKind>
            __d1 = new Dictionary<ETimeStampKind, DateTimeKind>()
            {
                { ETimeStampKind.Local, DateTimeKind.Local },
                { ETimeStampKind.Universal, DateTimeKind.Utc },
                { ETimeStampKind.Unspecified, DateTimeKind.Unspecified }
            };

        private static readonly Dictionary<DateTimeKind, ETimeStampKind>
            __d2 = new Dictionary<DateTimeKind, ETimeStampKind>()
            {
                { DateTimeKind.Local, ETimeStampKind.Local },
                { DateTimeKind.Utc, ETimeStampKind.Universal },
                { DateTimeKind.Unspecified, ETimeStampKind.Unspecified }
            };


        internal static String? ToString(ETimeStampKind e)
        {
            String s;
            __d0.TryGetValue(e, out s);
            return s;
        }

        internal static DateTimeKind ToDateTimeKind(ETimeStampKind e)
        {
            DateTimeKind e1;
            if (!__d1.TryGetValue(e, out e1))
                e1 = DateTimeKind.Unspecified;
            return e1;
        }

        internal static ETimeStampKind ToEnum(DateTimeKind e)
        {
            ETimeStampKind e1;
            if (!__d2.TryGetValue(e, out e1))
                e1 = ETimeStampKind.Unspecified;
            return e1;
        }
    }
}