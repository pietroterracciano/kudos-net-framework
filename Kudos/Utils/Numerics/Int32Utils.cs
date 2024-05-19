using System;
using Kudos.Utils.Numerics.Internals;

namespace Kudos.Utils.Numerics
{
    public static class Int32Utils
    {
        private static readonly INTInt32Utils __;
        static Int32Utils() { __ = new INTInt32Utils(); }
        public static Int32? Parse(Object? o) { Int32? i; __.Parse(ref o, out i); return i; }
        public static Int32 NNParse(Object? o) { Int32 i; __.NNParse(ref o, out i); return i; }
    }
}