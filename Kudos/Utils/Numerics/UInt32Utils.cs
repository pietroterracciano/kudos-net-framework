using System;
using System.Collections.Generic;
using System.Text;
using Kudos.Utils.Numerics.Internals;

namespace Kudos.Utils.Numerics
{
    public static class UInt32Utils
    {
        //private static readonly INTUInt32Utils __;
        //static UInt32Utils() { __ = new INTUInt32Utils(); }
        //public static UInt32? Parse(Object? o) { UInt32? i; __.Parse(ref o, out i); return i; }
        //public static UInt32 NNParse(Object? o) { UInt32 i; __.NNParse(ref o, out i); return i; }

        public static UInt32? Parse(Object? o) { return ObjectUtils.Parse<UInt32?>(o); }
        public static UInt32 NNParse(Object? o) { return ObjectUtils.Parse<UInt32>(o); }
    }
}
