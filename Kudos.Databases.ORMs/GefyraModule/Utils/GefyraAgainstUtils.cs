using System;
using Kudos.Databases.ORMs.GefyraModule.Constants;
using Kudos.Databases.ORMs.GefyraModule.Enums;
using System.Collections.Generic;

namespace Kudos.Databases.ORMs.GefyraModule.Utils
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