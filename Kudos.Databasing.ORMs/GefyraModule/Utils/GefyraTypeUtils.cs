using System;
using System.Collections.Generic;
using Kudos.Constants;
using Kudos.Databasing.ORMs.GefyraModule.Constants;

namespace Kudos.Databasing.ORMs.GefyraModule.Utils
{
    internal class GefyraTypeUtils
    {
        private static readonly Dictionary<Type, String>
            __d;

        static GefyraTypeUtils()
        {
            __d = new Dictionary<Type, String>()
            {
                { CType.Object, CGefyraConventionalPrefix.Member.Object },

                { CType.Boolean, CGefyraConventionalPrefix.Member.Boolean },
                { CType.String, CGefyraConventionalPrefix.Member.String },

                { CType.UInt16, CGefyraConventionalPrefix.Member.UInt16 },
                { CType.UInt32, CGefyraConventionalPrefix.Member.UInt32 },
                { CType.UInt64, CGefyraConventionalPrefix.Member.UInt64 },

                { CType.Int16, CGefyraConventionalPrefix.Member.Int16 },
                { CType.Int32, CGefyraConventionalPrefix.Member.Int32 },
                { CType.Int64, CGefyraConventionalPrefix.Member.Int64 },

                { CType.Single, CGefyraConventionalPrefix.Member.Single },
                { CType.Double, CGefyraConventionalPrefix.Member.Double },
                { CType.Decimal, CGefyraConventionalPrefix.Member.Decimal },
                { CType.Char, CGefyraConventionalPrefix.Member.Char }
            };
        }

        internal static void GetConventionalPrefix(ref Type? o, out String sConventionalPrefix)
        {
            if (o == null) o = CType.Object;
            __d.TryGetValue(o, out sConventionalPrefix);
            if (sConventionalPrefix != null) return;
            sConventionalPrefix = CGefyraConventionalPrefix.Member.Object;
        }
    }
}
