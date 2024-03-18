using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kudos.Utils.Numerics.Integers
{
    public static class UInt128NUtils
    {
        public static UInt128? From(object o) { if (o != null) try { return o as UInt128?; } catch { } return null; }
    }
}
