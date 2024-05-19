using Kudos.Databases.ORMs.GefyraModule.Constants;
using Kudos.Databases.ORMs.GefyraModule.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Kudos.Databases.ORMs.GefyraModule.Utils
{
    internal static class GefyraMethodUtils
    {
        public static EGefyraMethod? From(MethodInfo? oMethodInfo)
        {
            if(oMethodInfo != null)
            {
                String sMethodInfo = oMethodInfo.ToString();

                if (sMethodInfo.Contains(CGefyraMethod.Contains))
                    return EGefyraMethod.Contains;
                else if (sMethodInfo.Contains(CGefyraMethod.Equals))
                    return EGefyraMethod.Equals;
            }

            return null;
        }
    }
}
