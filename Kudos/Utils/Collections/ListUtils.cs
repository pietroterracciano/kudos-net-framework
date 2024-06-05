using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kudos.Constants;
using Kudos.Reflection.Utils;

namespace Kudos.Utils.Collections
{
    public abstract class ListUtils : CollectionUtils
    {
        #region public static T? Get(...)

        public static T? Get<T>(List<T>? l, Int32 i)
        {
            return IsValidIndex(l, i) ? l[i] : default(T);
        }

        #endregion

        #region public static Type? GetArgumentType(...)

        public new static Type? GetArgumentType(ICollection? o)
        {
            return o != null
                ? GetArgumentType(o.GetType())
                : null;
        }

        public new static Type? GetArgumentType<T>() where T : IList { return GetArgumentType(typeof(T)); }
        public new static Type? GetArgumentType(Type? t)
        {
            return ReflectionUtils.GetMemberValueType(ReflectionUtils.GetMethod(t.GetType(), "get_Item"));
        }

        #endregion

        #region public static List<T>? CreateInstance

        public static List<T> CreateInstance<T>()
        {
            return CreateInstance(typeof(T)) as List<T>;
        }
        public static IList? CreateInstance(Type? t)
        {
            return ReflectionUtils.CreateInstance(ReflectionUtils.MakeGenericType(CType.List, t)) as IList;
        }

        #endregion

    }
}