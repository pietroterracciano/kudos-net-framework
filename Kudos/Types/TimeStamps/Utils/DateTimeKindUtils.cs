using Kudos.Types.TimeStamps.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kudos.Types.TimeStamps.Utils
{
    internal class DateTimeKindUtils
    {
        private static readonly Dictionary<ETimeStampKind, DateTimeKind>
            __d;

        static DateTimeKindUtils()
        {
            __d = new Dictionary<ETimeStampKind, DateTimeKind>()
            {
                { ETimeStampKind.Local, DateTimeKind.Local },
                { ETimeStampKind.Universal, DateTimeKind.Utc }//,
                //{ ETimeStampKind.Unspecified, DateTimeKind.Unspecified }
            };
        }


        internal static DateTimeKind? Parse(ETimeStampKind e)
        {
            DateTimeKind e1;
            __d.TryGetValue(e, out e1);
            return e1;
        }
    }
}
