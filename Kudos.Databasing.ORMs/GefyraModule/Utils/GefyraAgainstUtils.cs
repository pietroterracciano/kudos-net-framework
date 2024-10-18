using System;
using Kudos.Databasing.ORMs.GefyraModule.Constants;
using Kudos.Databasing.ORMs.GefyraModule.Enums;
using System.Collections.Generic;

namespace Kudos.Databasing.ORMs.GefyraModule.Utils
{
    internal static class GefyraAgainstUtils
    {
        private static readonly Dictionary<EGefyraAgainst, String>
            __d;

        static GefyraAgainstUtils()
        {
            __d = new Dictionary<EGefyraAgainst, String>()
            {
                { EGefyraAgainst.InBooleanMode, CGefyraClausole.InBooleanMode },
                { EGefyraAgainst.InNaturalLanguageMode, CGefyraClausole.InNaturalLanguageMode },
                { EGefyraAgainst.InNaturalLanguageModeWithQueryExpansion, CGefyraClausole.InNaturalLanguageWithQueryExpansion},
                { EGefyraAgainst.WithQueryExpansion, CGefyraClausole.WithQueryExpansion}
            };
        }

        internal static void GetString(ref EGefyraAgainst e, out String? s)
        {
            __d.TryGetValue(e, out s);
        }
    }
}