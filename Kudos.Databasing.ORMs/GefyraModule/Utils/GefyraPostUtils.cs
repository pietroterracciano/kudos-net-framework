using System;
using Kudos.Databasing.ORMs.GefyraModule.Constants;
using Kudos.Databasing.ORMs.GefyraModule.Enums;
using System.Collections.Generic;

namespace Kudos.Databasing.ORMs.GefyraModule.Utils
{
    internal static class GefyraPostUtils
    {
        private static readonly Dictionary<EGefyraPost, String>
            __d;

        static GefyraPostUtils()
        {
            __d = new Dictionary<EGefyraPost, String>()
            {
                { EGefyraPost.Equal, CGefyraClausole.Equal },
                { EGefyraPost.Addition, CGefyraClausole.Addition },
                { EGefyraPost.Subtraction, CGefyraClausole.Subtraction }
            };
        }

        internal static void GetString(ref EGefyraPost e, out String? s)
        {
            if (!__d.TryGetValue(e, out s)) s = null;
        }
    }
}

