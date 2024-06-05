using System;
using System.Collections;
using System.Reflection;
using Kudos.Constants;
using Kudos.Reflection.Utils;

namespace Kudos.Utils.Collections
{
    public abstract class CollectionUtils
    {
        #region public static Boolean IsValidIndex(...)

        public static Boolean IsValidIndex(ICollection? o, Int32 i)
        {
            return
                o != null
                && i > -1
                && i < o.Count;
        }

        #endregion

        #region public static Type? GetArgumentType(...)

        public static Type? GetArgumentType(ICollection? o)
        {
            Array? a = o as Array;
            if (a != null) return ArrayUtils.GetArgumentType(a);
            IList? l = o as IList;
            if (l != null) return ListUtils.GetArgumentType(l);
            return null;
        }

        public static Type? GetArgumentType<T>() { return GetArgumentType(typeof(T)); }
        public static Type? GetArgumentType(Type? t)
        {
            if (t == null) return null;
            else if (t.IsArray) return ArrayUtils.GetArgumentType(t);
            Type? ti = ReflectionUtils.GetInterface(t, CInterface.IList);
            if (ti != null) return ListUtils.GetArgumentType(t);
            return null;
        }

        #endregion
    }
}