using System;
namespace Kudos.Utils.Numerics
{
    public static class Int128Utils
    {
        public static Int128? Parse(Object? o) { return ObjectUtils.Parse<Int128?>(o); }
        public static Int128 NNParse(Object? o) { return ObjectUtils.Parse<Int128>(o); }
    }
}
