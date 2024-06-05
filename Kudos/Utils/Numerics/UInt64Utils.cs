using System;

namespace Kudos.Utils.Numerics
{
    public static class UInt64Utils
    {
        public static UInt64? Parse(String? s) { UInt64 i; return UInt64.TryParse(s, out i) ? i : null; }
        public static UInt64? Parse(Object? o) { return ObjectUtils.Parse<UInt64?>(o); }
        public static UInt64 NNParse(Object? o) { return ObjectUtils.Parse<UInt64>(o); }
    }
}