using Kudos.Databases.ORMs.GefyraModule.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kudos.Databases.ORMs.GefyraModule.Utils
{
    internal static class GefyraOrderingUtils
    {
        private static readonly String
            __sAsc = "ASC",
            __sDesc = "DESC";

        private static readonly Dictionary<EGefyraOrdering, String>
          __dEnums2Strings = new Dictionary<EGefyraOrdering, String>()
          {
                { EGefyraOrdering.Asc, __sAsc },
                { EGefyraOrdering.Desc, __sDesc }
          };

        private static readonly Dictionary<String, EGefyraOrdering>
          __dStrings2Enums = new Dictionary<String, EGefyraOrdering>()
          {
                { __sAsc, EGefyraOrdering.Asc },
                { __sDesc, EGefyraOrdering.Desc }
          };

        internal static String? ToString(EGefyraOrdering o)
        {
            String oString;
            __dEnums2Strings.TryGetValue(o, out oString);
            return oString;
        }

        internal static EGefyraOrdering? From(String oString)
        {
            if (oString == null) return null;
            EGefyraOrdering o;
            return __dStrings2Enums.TryGetValue(oString.ToUpper(), out o)
                ? o
                : null;
        }
    }
}
