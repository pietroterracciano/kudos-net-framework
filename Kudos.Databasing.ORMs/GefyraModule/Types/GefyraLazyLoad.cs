using System;
using Kudos.Databasing.ORMs.GefyraModule.Constants;
using Kudos.Databasing.ORMs.GefyraModule.Enums;
using Kudos.Utils.Collections;

namespace Kudos.Databasing.ORMs.GefyraModule.Types
{
    internal class GefyraLazyLoad
    {
        internal readonly EGefyraClausole Clausole;
        private readonly Object?[]? _oa;

        //internal GefyraLazyLoad(EGefyraClausole egc) : this(egc, null) { }
        internal GefyraLazyLoad(EGefyraClausole egc, Object? o, params Object?[]? oa)
        {
            Clausole = egc;
            _oa = ArrayUtils.Append(o, oa);
        }

        internal T? GetPayLoad<T>(Int32 i)
        {
            return ArrayUtils.GetValue<T>(_oa, i);
        }
    }
}