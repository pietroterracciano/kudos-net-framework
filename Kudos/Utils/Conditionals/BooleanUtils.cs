using Kudos.Utils.Conditionals.Internals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kudos.Utils.Conditionals
{
    public static class BooleanUtils
    {
        private static readonly INTBooleanUtils __;
        static BooleanUtils() { __ = new INTBooleanUtils(); }

        public static Boolean? Parse(Object? o) { Boolean? b; __.Parse(ref o, out b); return b; }
        public static Boolean NNParse(Object? o) { Boolean b; __.NNParse(ref o, out b); return b; }
    }
}