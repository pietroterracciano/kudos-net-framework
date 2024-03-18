using Kudos.Constants;
using Kudos.Utils.Enums;
using Kudos.Utils.Members;
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
        #region Cast(...)

        public static ObjectType? Cast<ObjectType>(Object? o)
        {
            return
                o != null
                && o is ObjectType?
                    ? (ObjectType?)o
                    : default(ObjectType?);
        }

        #endregion

        #region ChangeType(...)

        public static Object? ChangeType(Object? o, Type? t)
        {
            if (o != null && t != null)
            {
                Type t0 = Nullable.GetUnderlyingType(t);
                if (t0 != null) t = t0;
                if (t.IsEnum) return EnumUtils.From(t, o);
                try { return Convert.ChangeType(o, t); } catch { }
            }

            return null;
        }

        #endregion

        public static Boolean Copy<T>(T? oFrom, T? oTo, BindingFlags bf = CBindingFlags.Public, MemberTypes mt = MemberTypes.All)
        {
            if (oFrom == null || oTo == null)
                return false;

            MemberInfo[]
                a = MemberUtils.Get<T>(bf, mt);

            if (a == null)
                return false;

            for(int i=0; i<a.Length; i++)
                MemberUtils.SetValue(oTo, a[i], MemberUtils.GetValue(oFrom, a[i]));

            return true;
        }
    }
}