using System;
using Kudos.Databasing.ORMs.GefyraModule.Constants;
using Kudos.Databasing.ORMs.GefyraModule.Enums;
using Kudos.Utils;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Kudos.Databasing.ORMs.GefyraModule.Utils
{
    internal static class GefyraJunctionUtils
    {
        private static readonly Dictionary<EGefyraJunction, String>
            __degj2s;

        private static readonly Dictionary<ExpressionType, EGefyraJunction>
            __det2egj;

        static GefyraJunctionUtils()
        {
            __degj2s = new Dictionary<EGefyraJunction, String>()
            {
                { EGefyraJunction.And, CGefyraClausole.And },
                { EGefyraJunction.Or, CGefyraClausole.Or }
            };

            __det2egj = new Dictionary<ExpressionType, EGefyraJunction>()
            {
                { ExpressionType.AndAlso, EGefyraJunction.And },
                { ExpressionType.OrElse, EGefyraJunction.Or }
            };
        }

        internal static void GetString(ref EGefyraJunction egj, out String? s)
        {
            if (!__degj2s.TryGetValue(egj, out s)) s = null;
        }

        internal static void GetEnum(ref ExpressionType expt, out EGefyraJunction? egj)
        {
            EGefyraJunction egj0;
            egj = __det2egj.TryGetValue(expt, out egj0) ? egj0 : null;
        }
    }
}