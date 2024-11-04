using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Kudos.Databasing.ORMs.GefyraModule.Constants;
using Kudos.Databasing.ORMs.GefyraModule.Enums;
using Kudos.Utils;

namespace Kudos.Databasing.ORMs.GefyraModule.Utils
{
    internal static class GefyraCompareUtils
    {
        private static readonly Dictionary<EGefyraCompare, String>
            __degc2s;

        private static readonly Dictionary<ExpressionType, EGefyraCompare>
            __det2egc;

        static GefyraCompareUtils()
        {
            __degc2s = new Dictionary<EGefyraCompare, String>()
            {
                { EGefyraCompare.Equal, CGefyraClausole.Equal },
                { EGefyraCompare.NotEqual, CGefyraClausole.NotEqual },
                { EGefyraCompare.GreaterThan, CGefyraClausole.GreaterThan },
                { EGefyraCompare.GreaterThanOrEqual, CGefyraClausole.GreaterThanOrEqual },
                { EGefyraCompare.LessThan, CGefyraClausole.LessThan },
                { EGefyraCompare.LessThanOrEqual, CGefyraClausole.LessThanOrEqual },
                { EGefyraCompare.Like, CGefyraClausole.Like },
                { EGefyraCompare.NotLike, CGefyraClausole.NotLike },
                { EGefyraCompare.In, CGefyraClausole.In },
                { EGefyraCompare.NotIn, CGefyraClausole.NotIn }
            };

            __det2egc = new Dictionary<ExpressionType, EGefyraCompare>()
            {
                { ExpressionType.Not, EGefyraCompare.NotEqual },
                { ExpressionType.Equal, EGefyraCompare.Equal },
                { ExpressionType.NotEqual, EGefyraCompare.NotEqual },
                { ExpressionType.GreaterThan, EGefyraCompare.GreaterThan },
                { ExpressionType.GreaterThanOrEqual, EGefyraCompare.GreaterThanOrEqual },
                { ExpressionType.LessThan, EGefyraCompare.LessThan },
                { ExpressionType.LessThanOrEqual, EGefyraCompare.LessThanOrEqual }
            };
        }

        internal static void GetString(ref EGefyraCompare e, out String? s)
        {
            __degc2s.TryGetValue(e, out s);
        }

        internal static void GetEnum(ref ExpressionType expt, out EGefyraCompare? egc)
        {
            EGefyraCompare egc0;
            egc = __det2egc.TryGetValue(expt, out egc0) ? egc0 : null;
        }
    }
}