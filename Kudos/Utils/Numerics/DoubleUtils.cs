using System;

namespace Kudos.Utils.Numerics
{
    public static class DoubleUtils
    {
        public static Double? Parse(String? s) { Double d; return Double.TryParse(s, out d) ? d : null; }
        public static Double? Parse(Object? o) { return ObjectUtils.Parse<Double?>(o); }
        public static Double NNParse(Object? o) { return ObjectUtils.Parse<Double>(o); }
    }
}
