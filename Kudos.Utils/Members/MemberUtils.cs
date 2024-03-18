using Kudos.Constants;
using Kudos.Enums;
using Kudos.Types;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Kudos.Utils.Members
{
    public abstract class MemberUtils
    {
        private static readonly String
            __sSeparator = ":";

        private static readonly Dictionary<String, Dictionary<String, MemberInfo>>
            __d0;

        private static readonly Dictionary<String, Dictionary<MemberTypes, Dictionary<BindingFlags, MemberInfo[]>>>
            __d1;

        private static readonly SmartDictionary<String, Type?>
            __d2;

        private static Object
            __oLock0,
            __oLock1;

        static MemberUtils()
        {
            __d2 = new SmartDictionary<String, Type?>();
            __d0 = new Dictionary<String, Dictionary<String, MemberInfo>>();
            __d1 = new Dictionary<String, Dictionary<MemberTypes, Dictionary<BindingFlags, MemberInfo[]>>>();
            __oLock0 = new Object();
            __oLock1 = new Object();
        }

        #region Cast(...)

        public static MemberInfo Cast(Object? o)
        {
            return o as MemberInfo;
        }

        #endregion

        #region public static MemberInfo Get(...)

        public static MemberInfo Get<Type>(String? s, BindingFlags bf = CBindingFlags.Public, MemberTypes mt = MemberTypes.All)
        {
            return Get(typeof(Type), s, bf, mt);
        }
        public static MemberInfo Get(Type? t, String? s, BindingFlags bf = CBindingFlags.Public, MemberTypes mt = MemberTypes.All)
        {
            if (t == null || s == null)
                return null;

            lock (__oLock0)
            {
                Dictionary<String, MemberInfo> d;
                if (!__d0.TryGetValue(t.FullName, out d) || d == null)
                    __d0[t.FullName] = d = new Dictionary<String, MemberInfo>();

                String k = s.Trim().ToUpper();

                MemberInfo m;
                if (!d.TryGetValue(k, out m))
                {
                    MemberInfo[] a;
                    try
                    {
                        a = t.GetMember(s, mt, bf);
                    }
                    catch
                    {
                        a = null;
                    }

                    m = d[k] =
                        a != null && a.Length > 0
                            ? a[0]
                            : null;
                }

                return m;
            }           
        }

        #endregion

        #region public static MemberInfo[]? Get(...)

        public static MemberInfo[]? Get<Type>(BindingFlags bf = CBindingFlags.Public, MemberTypes mt = MemberTypes.All)
        {
            return Get(typeof(Type), bf, mt);
        }
        public static MemberInfo[]? Get(Type? t, BindingFlags bf = CBindingFlags.Public, MemberTypes mt = MemberTypes.All)
        {
            if (t == null)
                return null;

            lock (__oLock1)
            {
                Dictionary<MemberTypes, Dictionary<BindingFlags, MemberInfo[]>> d0;

                if (!__d1.TryGetValue(t.FullName, out d0) || d0 == null)
                    __d1[t.FullName] = d0 = new Dictionary<MemberTypes, Dictionary<BindingFlags, MemberInfo[]>>();

                Dictionary<BindingFlags, MemberInfo[]> d1;

                if (!d0.TryGetValue(mt, out d1) || d1 == null)
                    d0[mt] = d1 = new Dictionary<BindingFlags, MemberInfo[]>();

                MemberInfo[] a;

                if (!d1.TryGetValue(bf, out a))
                {
                    try
                    {
                        a = t.GetMembers(bf);
                    }
                    catch
                    {
                        a = null;
                    }

                    if (a != null)
                    {
                        List<MemberInfo> l = new List<MemberInfo>(a.Length);

                        for (int i = 0; i < a.Length; i++)
                        {
                            if (a[i] == null)
                                continue;

                            if(
                                !mt.HasFlag(MemberTypes.All)
                                && !mt.HasFlag(a[i].MemberType)
                            )
                                continue;

                            Get(t, a[i].Name, bf, mt);
                            l.Add(a[i]);
                        }

                        a = l.ToArray();
                    }

                    d1[bf] = a;
                }

                return a;
            }
        }

        #endregion

        #region public static Type? GetValueType(...)

        public static Type? GetValueType(MemberInfo? m)
        {
            //MemberTypes? e = GetTypes(m);
            //if (e == null) return null;

            //lock (__oLock1)
            //{
            //    String s = GetPseudoHashCode(m);
            //    Type t;
            //    EDictionaryTryGetValueResult r;

            //    if (__d1.TryGetValue(s, out t, out r))
            //        return t;
            //    else if (r == EDictionaryTryGetValueResult.NullKey)
            //        return null;
            //    else if (e.Value == MemberTypes.Property)
            //        t = ((PropertyInfo)m).PropertyType;
            //    else if (e.Value == MemberTypes.Field)
            //        t = ((FieldInfo)m).FieldType;

            //    return __d1[s] = t;
            //}

            if(m != null)
                switch(m.MemberType)
                {
                    case MemberTypes.Property:
                        return ((PropertyInfo)m).PropertyType;
                    case MemberTypes.Field:
                        return ((FieldInfo)m).FieldType;
                }

            return null;
        }

        #endregion

        #region public static Type? GetDeclaringType(...)

        public static Type? GetDeclaringType(MemberInfo? m)
        {
            return m != null ? m.DeclaringType : null;
        }

        #endregion

        #region private static String? GetPseudoHashCode(...)

        private static String? GetPseudoHashCode(MemberInfo? m)
        {
            return
                m != null
                ? m.DeclaringType.FullName + __sSeparator + MemberTypesUtils.ToString(m.MemberType) + __sSeparator + m.ToString()
                : null;
        }

        #endregion

        #region public static Boolean SetValue(...)

        public static Boolean SetValue(Object? o, MemberInfo? m, Object? v)
        {
            if (m != null)
                switch (m.MemberType)
                {
                    case MemberTypes.Property:
                        return PropertyUtils.SetValue(o, (PropertyInfo)m, v);
                    case MemberTypes.Field:
                        return FieldUtils.SetValue(o, (FieldInfo)m, v);
                }

            return false;
        }

        #endregion

        #region public static Boolean GetValue(...)

        public static Object? GetValue(Object? o, MemberInfo? m)
        {
            if (m != null)
                switch (m.MemberType)
                {
                    case MemberTypes.Property:
                        return PropertyUtils.GetValue(o, (PropertyInfo)m);
                    case MemberTypes.Field:
                        return FieldUtils.GetValue(o, (FieldInfo)m);
                }

            return null;
        }

        #endregion

        #region public static Boolean GetTypes(...)

        public static MemberTypes? GetTypes(MemberInfo? m)
        {
            return m != null ? m.MemberType : null;
        }

        #endregion

        #region public static Attribute[]? GetAttribute()

        public static AttributeType? GetAttribute<AttributeType>(MemberInfo? m, Boolean bFromInheritance = false) where AttributeType : Attribute
        {
            return GetAttribute(typeof(AttributeType), m, bFromInheritance) as AttributeType;
        }

        public static Attribute GetAttribute(Type? t, MemberInfo? m, Boolean bFromInheritance = false)
        {
            if (m != null && t != null)
                try { return m.GetCustomAttribute(t, bFromInheritance); } catch { }

            return null;
        }

        #endregion

        #region public static Attribute[]? GetAttributes()

        public static AttributeType[]? GetAttributes<AttributeType>(MemberInfo? m, Boolean bFromInheritance = false) where AttributeType : Attribute
        {
            return GetAttributes(typeof(AttributeType), m, bFromInheritance) as AttributeType[];
        }

        public static Attribute[]? GetAttributes(Type? t, MemberInfo? m, Boolean bFromInheritance = false)
        {
            if (m != null && t != null)
                try { return m.GetCustomAttributes(t, bFromInheritance) as Attribute[]; } catch { }

            return null;
        }

        #endregion
    }
}