using System;

namespace Kudos.Utils.Numerics
{
    public static class UInt128Utils
    {
        public static UInt128? Parse(Object? o) { return ObjectUtils.Parse<UInt128?>(o); }
        public static UInt128 NNParse(Object? o) { return ObjectUtils.Parse<UInt128>(o); }
    }
}
