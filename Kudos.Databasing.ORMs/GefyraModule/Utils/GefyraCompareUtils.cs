using System;
using System.Collections.Generic;
using Kudos.Databasing.ORMs.GefyraModule.Constants;
using Kudos.Databasing.ORMs.GefyraModule.Enums;

namespace Kudos.Databasing.ORMs.GefyraModule.Utils
{
    internal static class GefyraCompareUtils
    {
        private static readonly Dictionary<EGefyraCompare, String>
            __d;

        static GefyraCompareUtils()
        {
            __d = new Dictionary<EGefyraCompare, String>()
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
        }

        internal static void GetString(ref EGefyraCompare e, out String? s)
        {
            __d.TryGetValue(e, out s);
        }
    }
}