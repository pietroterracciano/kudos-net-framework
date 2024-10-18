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
            //__sUnspecified = "UNS";

        private static readonly Dictionary<ETimeStampKind, String>
            __d0 = new Dictionary<ETimeStampKind, String>()
            {
                { ETimeStampKind.Local, __sLocal },
                { ETimeStampKind.Universal, __sUniversal }//,
                //{ ETimeStampKind.Unspecified, __sUnspecified }
            };

        private static readonly Dictionary<DateTimeKind, ETimeStampKind>
            __d1 = new Dictionary<DateTimeKind, ETimeStampKind>()
            {
                { DateTimeKind.Local, ETimeStampKind.Local },
                { DateTimeKind.Utc, ETimeStampKind.Universal }//,
                //{ DateTimeKind.Unspecified, ETimeStampKind.Unspecified }
            };


        internal static String? ToString(ETimeStampKind e)
        {
            String? s;
            __d0.TryGetValue(e, out s);
            return s;
        }

        internal static ETimeStampKind? Parse(DateTimeKind e)
        {
            ETimeStampKind e1;
            __d1.TryGetValue(e, out e1);
                //e1 = ETimeStampKind.Unspecified;
            return e1;
        }
    }
}