using Kudos.Databasing.ORMs.GefyraModule.Constants;
using Kudos.Databasing.ORMs.GefyraModule.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Kudos.Databasing.ORMs.GefyraModule.Utils
{
    internal static class GefyraJoinUtils
    {
        private static readonly Dictionary<EGefyraJoin, String>
          __d;

        static GefyraJoinUtils()
        {
            __d = new Dictionary<EGefyraJoin, String>()
            {
                { EGefyraJoin.Left, CGefyraClausole.Left },
                { EGefyraJoin.Right, CGefyraClausole.Right },
                { EGefyraJoin.Inner, CGefyraClausole.Inner },
                { EGefyraJoin.Full, CGefyraClausole.Full }
            };
        }

        internal static void GetString(ref EGefyraJoin egj, out String? s)
        {
            if (!__d.TryGetValue(egj, out s)) s = null;
        }
    }
}