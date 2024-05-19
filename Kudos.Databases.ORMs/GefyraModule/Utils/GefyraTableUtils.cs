using Kudos.Constants;
using Kudos.Databases.ORMs.GefyraModule.Constants;
using Kudos.Utils.Collections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kudos.Databases.ORMs.GefyraModule.Utils
{
    internal static class GefyraTableUtils
    {
        internal static void OverrideNamesIfPossible(ref String? s, ref String? ssn, ref String? stn)
        {
            if 
            (
                s == null 
                || !s.Contains(CCharacter.Dot)
            ) 
                return;

            string[] 
                a = s.Split(CCharacter.Dot);

            if (ArrayUtils.IsValidIndex(a, 1))
            {
                ssn = a[0]; stn = a[1];
            }
            else
                ssn = a[0];
        }
    }
}