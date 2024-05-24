using System;

namespace Kudos.Utils.Numerics
{
    public static class DoubleUtils
    {
        public static Double? Parse(Object? o) { return ObjectUtils.Parse<Double?>(o); }
        public static Double NNParse(Object? o) { return ObjectUtils.Parse<Double>(o); }
    }
}
