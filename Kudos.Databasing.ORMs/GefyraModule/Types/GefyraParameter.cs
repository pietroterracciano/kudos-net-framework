using System;
using Kudos.Databasing.ORMs.GefyraModule.Enums;
using Kudos.Databasing.ORMs.GefyraModule.Interfaces.Entities;
using Kudos.Databasing.ORMs.GefyraModule.Types.Entities;
using Kudos.Types;

namespace Kudos.Databasing.ORMs.GefyraModule.Types
{
    internal class 
        GefyraParameter 
    : 
        TokenizedObject
    {
        internal readonly IGefyraColumn Column;
        internal readonly Int32 Index;
        internal readonly String Alias;
        internal readonly Object? Value;

        internal GefyraParameter(ref IGefyraColumn gc, ref Int32 i, ref String s, ref Object? o)
        {
            Column = gc;
            Index = i;
            Alias = s;
            Value = o;
        }
    }
}