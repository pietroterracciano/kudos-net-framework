using System;

namespace Kudos.Utils.Numerics
{
    public static class UInt128Utils
    {
        public static UInt128? Parse(String? s) { UInt128 i; return UInt128.TryParse(s, out i) ? i : null; }
        public static UInt128? Parse(Object? o) { return ObjectUtils.Parse<UInt128?>(o); }
        public static UInt128 NNParse(Object? o) { return ObjectUtils.Parse<UInt128>(o); }
    }
}
