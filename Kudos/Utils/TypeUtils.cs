using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kudos.Utils
{
    public static class TypeUtils
    {
        public static Type? Get(Object? o)
        {
            Type? to = o as Type;
            return to != null ? to : (o != null ? o.GetType() : null);
        }

        public static Boolean IsPrimitive(Object? o)
        {
            Type? t = Get(o);
            return t != null && t.IsPrimitive;
        }
    }
}