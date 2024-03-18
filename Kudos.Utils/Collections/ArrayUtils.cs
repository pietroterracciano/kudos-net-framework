using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kudos.Utils.Collections
{
    public abstract class ArrayUtils : CollectionUtils
    {
        #region Cast(...)

        public static new Array Cast(Object? o) { return o as Array; }

        #endregion

        #region CreateInstance(...)

        public static Type[]? CreateInstance<Type>(int i) { return ObjectUtils.Cast<Type[]>(CreateInstance(typeof(Type), i)); }
        public static Array? CreateInstance(Type? t, int i) { if (t != null && i > -1) try { return Array.CreateInstance(t, i); } catch { } return null; }

        #endregion

        #region GetValue(...)

        public static Object? GetFirstValue(Array? a) { return GetValue(a, 0); }
        public static Object? GetLastValue(Array? a) { return a != null ? GetValue(a, a.Length -1) : null; }
        public static Object? GetValue(Array? a, int i) { return IsValidIndex(a, i) ? a.GetValue(i) : null; }

        #endregion

        #region Shift(...)

        public static ObjectType? Shift<ObjectType>(IList<ObjectType>? l, out ObjectType[]? a)
        {
            if (l == null)
            {
                a = null;
                return default(ObjectType?);
            }

            return Shift(l.ToArray(), out a);
        }

        public static ObjectType? Shift<ObjectType>(ObjectType[]? a0, out ObjectType[]? a1)
        {
            if (a0 == null || a0.Length < 1)
            {
                a1 = null;
                return default(ObjectType?);
            }

            a1 = new ObjectType[a0.Length - 1];

            if (a1.Length > 0)
                Array.Copy(a0, 1, a1, 0, a1.Length);

            return a0[0];
        }


        #endregion

        #region UnShift(...)

        public static ObjectType[] UnShift<ObjectType>(ObjectType? o, ObjectType[]? a)
        {
            int i = a != null ? a.Length : 0;

            ObjectType[] a1 = new ObjectType[1 + i];
            a1[0] = o;

            if (i > 0)
                Array.Copy(a, 0, a1, 1, a.Length);

            return a1;
        }

        #endregion
    }
}
