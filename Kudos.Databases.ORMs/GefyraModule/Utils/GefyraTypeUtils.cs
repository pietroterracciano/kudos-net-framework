using Kudos.Constants;
using Kudos.Databases.ORMs.GefyraModule.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Kudos.Databases.ORMs.GefyraModule.Utils
{
    internal class GefyraTypeUtils
    {
        private static readonly Dictionary<Type, String>
            __dTypes2ConventionsPrefixes = new Dictionary<Type, String>()
            {
                { CType.Object, "o" },

                { CType.Boolean, "b" },
                { CType.String, "s" },

                { CType.UInt16, "ui" },
                { CType.UInt32, "ui" },
                { CType.UInt64, "ui" },

                { CType.Int16, "i" },
                { CType.Int32, "i" },
                { CType.Int64, "i" },

                { CType.Single, "f" },
                { CType.Double, "d" },
                { CType.Decimal, "d" }
            };

        internal static Boolean GetConventionPrefix(ref Type? o, out String? sConventionPrefix)
        {
            if (o != null)
            {
                if (__dTypes2ConventionsPrefixes.TryGetValue(o, out sConventionPrefix))
                    return true;
            }
            else
                sConventionPrefix = null;

            return false;
        }
    }
}
