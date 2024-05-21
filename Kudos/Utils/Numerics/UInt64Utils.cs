using System;
using Kudos.Utils.Numerics.Internals;

namespace Kudos.Utils.Numerics
{
    public static class UInt64Utils
    {
        //private static readonly INTUInt64Utils __;
        //static UInt64Utils() { __ = new INTUInt64Utils(); }
        //public static UInt64? Parse(Object? o) { UInt64? i; __.Parse(ref o, out i); return i; }
        //public static UInt64 NNParse(Object? o) { UInt64 i; __.NNParse(ref o, out i); return i; }

        public static UInt64? Parse(Object? o) { return ObjectUtils.Parse<UInt64?>(o); }
        public static UInt64 NNParse(Object? o) { return ObjectUtils.Parse<UInt64>(o); }
    }
}