using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kudos.Utils.Numerics.Internals;

namespace Kudos.Utils.Numerics
{
    public static class Int128Utils
    {
        private static readonly INTInt128Utils __;
        static Int128Utils() { __ = new INTInt128Utils(); }
        public static Int128? Parse(Object? o) { Int128? i; __.Parse(ref o, out i); return i; }
        public static Int128 NNParse(Object? o) { Int128 i; __.NNParse(ref o, out i); return i; }
    }
}
