using System;

namespace Kudos.Utils.Numerics
{
    public static class Int64Utils
    {
        public static Int64? Parse(String? s) { Int64 i; return Int64.TryParse(s, out i) ? i : null; }
        public static Int64? Parse(Object? o) { return ObjectUtils.Parse<Int64?>(o); }
        public static Int64 NNParse(Object? o) { return ObjectUtils.Parse<Int64>(o); }
    }
}

