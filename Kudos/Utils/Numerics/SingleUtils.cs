using Kudos.Constants;
using Kudos.Utils.Numerics.Internals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kudos.Utils.Numerics
{
    public static class SingleUtils
    {
        private static readonly INTSingleUtils __;
        static SingleUtils() { __ = new INTSingleUtils(); }
        public static Single? Parse(Object? o) { Single? f; __.Parse(ref o, out f); return f; }
        public static Single NNParse(Object? o) { Single f; __.NNParse(ref o, out f); return f; }
    }
}