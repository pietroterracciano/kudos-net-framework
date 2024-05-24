using System;

namespace Kudos.Utils.Numerics
{
    public static class UInt16Utils
    {
        public static UInt16? Parse(Object? o) { return ObjectUtils.Parse<UInt16?>(o); }
        public static UInt16 NNParse(Object? o) { return ObjectUtils.Parse<UInt16>(o); }
    }
}

