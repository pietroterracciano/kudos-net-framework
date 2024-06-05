using Kudos.Constants;
using Kudos.Reflection.Utils;
using Kudos.Utils.Conditionals;
using Kudos.Utils.Numerics;
using Kudos.Utils.Texts;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;

namespace Kudos.Utils
{
    public abstract class ObjectUtils
    {
        //#region public static Boolean IsNullable()

        //public static Boolean IsNullable(Object? o)
        //{
        //    return
        //        o == null
        //        || IsNullable(o.GetType());
        //}

        //public static Boolean IsNullable(Type? t)
        //{
        //    return
        //        t == null
        //        || !t.IsValueType
        //        || !t.IsEnum
        //        || Nullable.GetUnderlyingType(t) != null;
        //}

        //#endregion

        #region Cast(...)

        public static T? Cast<T>(Object? o) { return IsInstance<T>(o) ? (T?)o : default(T?); }
        public static Object? Cast(Object? o, Type? tOf) { return IsInstance(o, tOf) ? o : null; }

        #endregion

        #region public static Object? Parse(...)

        public static T? Parse<T>(Object? o, Boolean bForceNotNullable = false) { return Cast<T>(Parse(typeof(T), o, bForceNotNullable)); }
        public static Object? Parse(Type? t, Object? o, Boolean bForceNotNullable = false)
        {
            if (t == null)
                return null;

            Type? 
                t0 = Nullable.GetUnderlyingType(t);

            if (t0 != null)
                t = t0;

            if (o != null)
            {
                if (t.IsEnum) return EnumUtils.Parse(t, o);
                try { return Convert.ChangeType(o, t); } catch { }
            }

            Boolean
                bIsNullable = !bForceNotNullable && (!t.IsValueType || t0 != null);

            return !bIsNullable ? ReflectionUtils.CreateInstance(t) : o;
        }

        #endregion

        #region public static Boolean IsNumeric(...)

        public static Boolean IsNumeric(Object? o)
        {
            return
                IsNumeric(o as Type)
                ||
                (
                    o != null
                    && IsNumeric(o.GetType())
                );
        }

        public static Boolean IsNumeric(Type? t)
        {
            return
                t != null
                &&
                (
                    t == CType.UInt16
                    || t == CType.NullableUInt16
                    || t == CType.UInt32
                    || t == CType.NullableUInt32
                    || t == CType.UInt64
                    || t == CType.NullableUInt64
                    || t == CType.UInt128
                    || t == CType.NullableUInt128
                    || t == CType.Int16
                    || t == CType.NullableInt16
                    || t == CType.Int32
                    || t == CType.NullableInt32
                    || t == CType.Int64
                    || t == CType.NullableInt64
                    || t == CType.Int128
                    || t == CType.NullableInt128
                    || t == CType.Single
                    || t == CType.NullableSingle
                    || t == CType.Double
                    || t == CType.NullableDouble
                    || t == CType.Decimal
                    || t == CType.NullableDecimal
                );
        }

        #endregion

        #region public static Boolean IsInstance<...>(...)

        public static Boolean IsInstance<T>(Object? o) { return IsInstance(o, typeof(T)); }
        public static Boolean IsInstance(Object? o, Type? tOf)
        {
            return tOf != null && tOf.IsInstanceOfType(o);
        }

        #endregion

        #region public static Boolean IsSubclass(...)

        public static Boolean IsSubclass(Type? tSubclass, Type? tClass, Boolean bOrEquals = true)
        {
            return
                tSubclass != null
                && tClass != null
                &&
                (
                    (
                        bOrEquals
                        && tSubclass == tClass
                    )
                    || tSubclass.IsSubclassOf(tClass)
                );
        }

        #endregion
    }
}