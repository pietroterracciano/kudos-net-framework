using System;

namespace Kudos.Utils.Conditionals
{
    public static class BooleanUtils
    {
        public static Boolean? Parse(Object? o) { return ObjectUtils.Parse<Boolean?>(o); }
        public static Boolean NNParse(Object? o) { return ObjectUtils.Parse<Boolean>(o); }
    }
}