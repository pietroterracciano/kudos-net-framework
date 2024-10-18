using Kudos.Databasing.ORMs.GefyraModule.Entities;
using Kudos.Databasing.ORMs.GefyraModule.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kudos.Databasing.ORMs.GefyraModule.Models.Commands.Builders
{
    internal class GCBParameterModel
    {
        internal readonly string Alias;
        internal readonly GefyraColumn ReferencedColumn;
        internal readonly int Index;
        internal Object Value;

        internal GCBParameterModel(ref string sAlias, ref int iIndex, ref GefyraColumn clmReferenced, ref object oValue)
        {
            Alias = sAlias; 
            Index = iIndex; 
            ReferencedColumn = clmReferenced;
            Value = oValue;
        }
    }
}
