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

        public static T? Cast<T>(Object? o) { return IsInstance<T>(o) ? (T?)o : default(T); }
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

            //if (t.IsEnum) return bIsNullable ? EnumUtils.Parse(t, o) : EnumUtils.NNParse(t, o);
            //else if (t == CType.UInt16) return bIsNullable ? UInt16Utils.Parse(o) : UInt16Utils.NNParse(o);
            //else if (t == CType.UInt32) return bIsNullable ? UInt32Utils.Parse(o) : UInt32Utils.NNParse(o);
            //else if (t == CType.UInt64) return bIsNullable ? UInt64Utils.Parse(o) : UInt64Utils.NNParse(o);
            //else if (t == CType.UInt128) return bIsNullable ? UInt128Utils.Parse(o) : UInt128Utils.NNParse(o);
            //else if (t == CType.Int16) return bIsNullable ? Int16Utils.Parse(o) : Int16Utils.NNParse(o);
            //else if (t == CType.Int32) return bIsNullable ? Int32Utils.Parse(o) : Int32Utils.NNParse(o);
            //else if (t == CType.Int64) return bIsNullable ? Int64Utils.Parse(o) : Int64Utils.NNParse(o);
            //else if (t == CType.Int128) return bIsNullable ? Int128Utils.Parse(o) : Int128Utils.NNParse(o);
            //else if (t == CType.Single) return bIsNullable ? SingleUtils.Parse(o) : SingleUtils.NNParse(o);
            //else if (t == CType.Decimal) return bIsNullable ? DecimalUtils.Parse(o) : DecimalUtils.NNParse(o);
            //else if (t == CType.Double) return bIsNullable ? DoubleUtils.Parse(o) : DoubleUtils.NNParse(o);
            //else if (t == CType.Char) return bIsNullable ? CharUtils.Parse(o) : CharUtils.NNParse(o);
            //else if (t == CType.Boolean) return bIsNullable ? BooleanUtils.Parse(o) : BooleanUtils.NNParse(o);
            //else if (t == CType.String) return bIsNullable ? StringUtils.Parse(o) : StringUtils.NNParse(o);
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