using Kudos.Databases.ORMs.GefyraModule.Constants;
using Kudos.Databases.ORMs.GefyraModule.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kudos.Databases.ORMs.GefyraModule.Utils
{
    internal static class GefyraOrderUtils
    {
        private static readonly Dictionary<EGefyraOrder, String>
            __d;

        static GefyraOrderUtils()
        {
            __d = new Dictionary<EGefyraOrder, String>()
            {
                { EGefyraOrder.Asc, CGefyraClausole.Asc },
                { EGefyraOrder.Desc, CGefyraClausole.Desc }
            };
        }

        internal static void GetString(ref EGefyraOrder e, out String? s)
        {
            __d.TryGetValue(e, out s);
        }
    }
}
