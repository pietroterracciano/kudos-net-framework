using System;
using Kudos.Utils.Numerics.Internals;

namespace Kudos.Utils.Numerics
{
    public static class Int16Utils
    {
        private static readonly INTInt16Utils __;
        static Int16Utils() { __ = new INTInt16Utils(); }
        public static Int16? Parse(Object? o) { Int16? i; __.Parse(ref o, out i); return i; }
        public static Int16 NNParse(Object? o) { Int16 i; __.NNParse(ref o, out i); return i; }
    }
}