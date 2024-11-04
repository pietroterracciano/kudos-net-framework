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
            if (o == null) return null;
            Type? t = o as Type; return t != null ? t : o.GetType();
        }

        public static Boolean IsPrimitive(Object? o) { return IsPrimitive(Get(o)); }
        public static Boolean IsPrimitive(Type? t) { return t != null && t.IsPrimitive; }

        public static Boolean IsGeneric(Object? o) { return IsGeneric(Get(o)); }
        public static Boolean IsGeneric(Type? t) { return t != null && t.IsGenericType; }

        public static Type[]? GetGenericArguments(Object? o) { return GetGenericArguments(Get(o)); }
        public static Type[]? GetGenericArguments(Type? t)
        {
            if (IsGeneric(t)) try { return t.GetGenericArguments(); } catch { }
            return null;
        }

        public static Type? GetUnderlying(Object? o) { return GetUnderlying(Get(o)); }
        public static Type? GetUnderlying(Type? t)
        {
            return t != null ? Nullable.GetUnderlyingType(t) : null;
        }
    }
}