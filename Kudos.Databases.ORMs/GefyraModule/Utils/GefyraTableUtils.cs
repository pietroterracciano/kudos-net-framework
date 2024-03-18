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
        internal static void ExtrapolateNames(ref string? s2Analyze, ref string? sSchemaName, ref string? sTableName)
        {
            if (s2Analyze == null || !s2Analyze.Contains(CGefyraSeparator.Dot)) return;
            string[] aTNPieces = s2Analyze.Split(CGefyraSeparator.Dot);
            if (ArrayUtils.IsValidIndex(aTNPieces, 0)) sSchemaName = aTNPieces[0];
            if (ArrayUtils.IsValidIndex(aTNPieces, 1)) sTableName = aTNPieces[1];
        }
    }
}