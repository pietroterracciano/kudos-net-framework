using System;
using Kudos.Databasing.ORMs.GefyraModule.Constants;
using Kudos.Databasing.ORMs.GefyraModule.Enums;
using Kudos.Utils.Collections;

namespace Kudos.Databasing.ORMs.GefyraModule.Types
{
    internal class GefyraLazyLoad
    {
        internal readonly EGefyraClausole Clausole;
        internal readonly Object? PayLoad;

        internal GefyraLazyLoad(ref EGefyraClausole eClausole, Object? o)
        {
            Clausole = eClausole;
            PayLoad = o;
        }
    }
}