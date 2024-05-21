using System;
using Kudos.Utils.Numerics.Internals;

namespace Kudos.Utils.Numerics
{
    public static class UInt16Utils
    {
        //private static readonly INTUInt16Utils __;
        //static UInt16Utils() { __ = new INTUInt16Utils(); }
        //public static UInt16? Parse(Object? o) { UInt16? i; __.Parse(ref o, out i); return i; }
        //public static UInt16 NNParse(Object? o) { UInt16 i; __.NNParse(ref o, out i); return i; }

        public static UInt16? Parse(Object? o) { return ObjectUtils.Parse<UInt16?>(o); }
        public static UInt16 NNParse(Object? o) { return ObjectUtils.Parse<UInt16>(o); }
    }
}

