using System;
using Kudos.Utils.Numerics.Internals;

namespace Kudos.Utils.Numerics
{
    public static class DoubleUtils
    {
        private static readonly INTDoubleUtils __;
        static DoubleUtils() { __ = new INTDoubleUtils(); }
        public static Double? Parse(Object? o) { Double? d; __.Parse(ref o, out d); return d; }
        public static Double NNParse(Object? o) { Double d; __.NNParse(ref o, out d); return d; }
    }
}
