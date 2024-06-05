using System;

namespace Kudos.Utils.Numerics
{
    public static class Int16Utils
    {
        public static Int16? Parse(String? s) { Int16 i; return Int16.TryParse(s, out i) ? i : null; }
        public static Int16? Parse(Object? o) { return ObjectUtils.Parse<Int16?>(o); }
        public static Int16 NNParse(Object? o) { return ObjectUtils.Parse<Int16>(o); }
    }
}