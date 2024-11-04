using Kudos.Databasing.ORMs.GefyraModule.Constants;
using Kudos.Databasing.ORMs.GefyraModule.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Kudos.Databasing.ORMs.GefyraModule.Utils
{
    internal static class GefyraMethodUtils
    {
        public static EGefyraMethod? GetEnum(MethodInfo? mi)
        {
            if (mi == null)
                return null;

            String?
                s = mi.ToString();

            if (s != null)
            {     
                if (s.Contains(CGefyraMethod.Contains, StringComparison.OrdinalIgnoreCase))
                    return EGefyraMethod.Contains;
                else if (s.Contains(CGefyraMethod.Equals, StringComparison.OrdinalIgnoreCase))
                    return EGefyraMethod.Equals;
            }

            return null;
        }
    }
}
