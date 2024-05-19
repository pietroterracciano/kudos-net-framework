using System;
using Kudos.Utils.Numerics.Internals;

namespace Kudos.Utils.Numerics
{
    public static class Int64Utils
    {
        private static readonly INTInt64Utils __;
        static Int64Utils() { __ = new INTInt64Utils(); }
        public static Int64? Parse(Object? o) { Int64? i; __.Parse(ref o, out i); return i; }
        public static Int64 NNParse(Object? o) { Int64 i; __.NNParse(ref o, out i); return i; }
    }
}

