using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kudos.Utils.Numerics.Internals;

namespace Kudos.Utils.Numerics
{
    public static class UInt128Utils
    {
        //private static readonly INTUInt128Utils __;
        //static UInt128Utils() { __ = new INTUInt128Utils(); }
        //public static UInt128? Parse(Object? o) { UInt128? i; __.Parse(ref o, out i); return i; }
        //public static UInt128 NNParse(Object? o) { UInt128 i; __.NNParse(ref o, out i); return i; }

        public static UInt128? Parse(Object? o) { return ObjectUtils.Parse<UInt128?>(o); }
        public static UInt128 NNParse(Object? o) { return ObjectUtils.Parse<UInt128>(o); }
    }
}
