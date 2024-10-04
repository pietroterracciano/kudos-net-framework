using System;
using Kudos.Databases.ORMs.GefyraModule.Constants;
using Kudos.Databases.ORMs.GefyraModule.Enums;
using System.Collections.Generic;

namespace Kudos.Databases.ORMs.GefyraModule.Utils
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
            __d.TryGetValue(e, out s);
        }
    }
}

