using System;

namespace Kudos.Utils.Numerics
{
    public static class UInt32Utils
    {
        public static UInt32? Parse(Object? o) { return ObjectUtils.Parse<UInt32?>(o); }
        public static UInt32 NNParse(Object? o) { return ObjectUtils.Parse<UInt32>(o); }
    }
}
