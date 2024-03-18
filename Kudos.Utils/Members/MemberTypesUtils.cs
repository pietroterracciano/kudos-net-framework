using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Kudos.Utils.Members
{
    internal static class MemberTypesUtils
    {
        private static readonly String
            __sProperty = "PRP",
            __sConstructor = "CNS",
            __sMethod = "MTH",
            __sField = "FLD";

        private static readonly Dictionary<MemberTypes, String>
            __dEnums2Strings = new Dictionary<MemberTypes, String>()
            {
                { MemberTypes.Property, __sProperty },
                { MemberTypes.Constructor, __sConstructor },
                { MemberTypes.Method, __sMethod },
                { MemberTypes.Field, __sField },
            };

        internal static String? ToString(MemberTypes e)
        {
            String s;
            __dEnums2Strings.TryGetValue(e, out s);
            return s;
        }
    }
}
