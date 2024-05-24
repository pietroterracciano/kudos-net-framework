using System;

namespace Kudos.Utils.Texts
{
    public static class CharUtils
    {
        public static Char? Parse(Object? o) { return ObjectUtils.Parse<Char?>(o); }
        public static Char NNParse(Object? o) { return ObjectUtils.Parse<Char>(o); }
    }
}