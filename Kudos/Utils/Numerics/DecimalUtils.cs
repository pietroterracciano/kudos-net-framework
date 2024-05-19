using Kudos.Utils.Numerics.Internals;
using System;

namespace Kudos.Utils.Numerics
{
    public static class DecimalUtils
    {
        private static readonly INTDecimalUtils __;
        static DecimalUtils() { __ = new INTDecimalUtils(); }
        public static Decimal? Parse(Object? o) { Decimal? dcm;  __.Parse(ref o, out dcm); return dcm; }
        public static Decimal NNParse(Object? o) { Decimal dcm; __.NNParse(ref o, out dcm); return dcm; }
    }
}