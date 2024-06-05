using System;

namespace Kudos.Utils.Numerics
{
    public static class UInt16Utils
    {
        public static UInt16? Parse(String? s) { UInt16 i; return UInt16.TryParse(s, out i) ? i : null; }
        public static UInt16? Parse(Object? o) { return ObjectUtils.Parse<UInt16?>(o); }
        public static UInt16 NNParse(Object? o) { return ObjectUtils.Parse<UInt16>(o); }
    }
}

