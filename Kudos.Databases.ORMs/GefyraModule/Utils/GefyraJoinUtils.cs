using Kudos.Databases.ORMs.GefyraModule.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Kudos.Databases.ORMs.GefyraModule.Utils
{
    internal static class GefyraJoinUtils
    {
        private static readonly String
            __sLeft = "LEFT",
            __sRight = "RIGHT",
            __sInner = "INNER",
            __sOuter = "OUTER";

        private static readonly Dictionary<EGefyraJoin, String>
          __dEnums2Strings = new Dictionary<EGefyraJoin, String>()
          {
                { EGefyraJoin.Left, __sLeft },
                { EGefyraJoin.Right, __sRight },
                { EGefyraJoin.Inner, __sInner },
                { EGefyraJoin.Outer, __sOuter },
          };

        private static readonly Dictionary<String, EGefyraJoin>
          __dStrings2Enums = new Dictionary<String, EGefyraJoin>()
          {
                { __sLeft, EGefyraJoin.Left },
                { __sRight, EGefyraJoin.Right },
                { __sInner, EGefyraJoin.Inner },
                { __sOuter, EGefyraJoin.Outer },
          };

        internal static String? ToString(EGefyraJoin o)
        {
            String oString;
            __dEnums2Strings.TryGetValue(o, out oString);
            return oString;
        }

        internal static EGefyraJoin? From(String oString)
        {
            if (oString == null) return null;
            EGefyraJoin o;
            return __dStrings2Enums.TryGetValue(oString.ToUpper(), out o)
                ? o
                : null;
        }
    }
}
