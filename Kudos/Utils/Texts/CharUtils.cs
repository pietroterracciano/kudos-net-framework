using Kudos.Utils.Texts.Internals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kudos.Utils.Texts
{
    public static class CharUtils
    {
        private static readonly INTCharUtils __;
        static CharUtils() { __ = new INTCharUtils(); }

        public static Char? Parse(Object? o) { Char? c; __.Parse(ref o, out c); return c; }
        public static Char NNParse(Object? o) { Char c; __.NNParse(ref o, out c); return c; }
    }
}