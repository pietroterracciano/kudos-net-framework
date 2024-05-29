using System;

namespace Kudos.Utils.Numerics
{
    public static class UInt32Utils
    {
        public static UInt32? Parse(Object? o) { return ObjectUtils.Parse<UInt32?>(o); }
        public static UInt32 NNParse(Object? o) { return ObjectUtils.Parse<UInt32>(o); }

        public static UInt32 Random(UInt32 iMax) { return UInt32Utils.Random(iMax, iMax); }
        public static UInt32 Random(UInt32 iMin, UInt32 iMax)
        {
            return
                UInt32Utils.NNParse
                (
                    Int32Utils.Random
                    (
                        Int32Utils.NNParse(iMin),
                        Int32Utils.NNParse(iMax)
                    )
                );
        }
    }
}
