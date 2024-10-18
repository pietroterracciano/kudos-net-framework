using Kudos.Databasing.ORMs.GefyraModule.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Collections.Specialized.BitVector32;

namespace Kudos.Databasing.ORMs.GefyraModule.Utils
{
    internal static class GefyraActionUtils
    {
        internal static EGefyraAction? From(EGefyraClausole eClausole)
        {
            switch (eClausole)
            {
                case EGefyraClausole.Insert:
                    return EGefyraAction.Insert;
                case EGefyraClausole.Update:
                    return EGefyraAction.Update;
                case EGefyraClausole.Delete:
                    return EGefyraAction.Delete;
                case EGefyraClausole.Select:
                    return EGefyraAction.Select;
                case EGefyraClausole.Count:
                    return EGefyraAction.Count;
            }

            return null;
        }
    }
}
