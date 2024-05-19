using Kudos.Databases.ORMs.GefyraModule.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Kudos.Databases.ORMs.GefyraModule.Utils
{
    internal static class GefyraActuatorUtils
    {
        private static readonly String
            __sIncremental = "+",
            __sDecremental = "-";

        private static readonly Dictionary<EGefyraActuator, String>
            __dEnums2Strings = new Dictionary<EGefyraActuator, String>()
            {
                { EGefyraActuator.Incremental, __sIncremental },
                { EGefyraActuator.Decremental, __sDecremental }
            };

        private static readonly Dictionary<String, EGefyraActuator>
            __dStrings2Enums = new Dictionary<String, EGefyraActuator>()
            {
                { __sIncremental, EGefyraActuator.Incremental },
                { __sDecremental, EGefyraActuator.Decremental }
            };

        internal static String? ToString(EGefyraActuator o)
        {
            String oString;
            __dEnums2Strings.TryGetValue(o, out oString);
            return oString;
        }

        internal static EGefyraActuator? From(String oString)
        {
            if (oString == null) return null;
            EGefyraActuator o;
            return __dStrings2Enums.TryGetValue(oString.ToUpper(), out o)
                ? o
                : null;
        }
    }
}
