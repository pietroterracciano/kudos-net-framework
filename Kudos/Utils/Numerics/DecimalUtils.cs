using System;

namespace Kudos.Utils.Numerics
{
    public static class DecimalUtils
    {
        public static Decimal? Parse(Object? o) { return ObjectUtils.Parse<Decimal?>(o); }
        public static Decimal NNParse(Object? o) { return ObjectUtils.Parse<Decimal>(o); }
    }
}