using Kudos.Databasing.ORMs.GefyraModule.Entities;
using Kudos.Databasing.ORMs.GefyraModule.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kudos.Databasing.ORMs.GefyraModule.Models.Contexts
{
    internal class GCParameterModel
    {
        internal readonly GefyraColumn Key;
        internal readonly EGefyraComparator Comparator;
        internal readonly Object Value;

        internal GCParameterModel(ref GefyraColumn oColumn, EGefyraComparator eComparator, ref Object oValue)
        {
            Key = oColumn;
            Comparator = eComparator;
            Value = oValue;
        }
    }
}