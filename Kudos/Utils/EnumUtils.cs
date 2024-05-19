using Kudos.Constants;
using Kudos.Enums;
using Kudos.Utils.Collections;
using Kudos.Utils.Numerics;
using Kudos.Utils.Texts;
using System;

namespace Kudos.Utils
{
    public static class EnumUtils
    {
        #region public static HasFlag()

        public static bool HasFlag<EnumType>(EnumType? e0, EnumType? e1) where EnumType : Enum
        {
            return e0 != null && e1 != null && e0.HasFlag(e1);
        }

        public static bool HasNotFlag<EnumType>(EnumType? e0, EnumType? e1) where EnumType : Enum
        {
            return e0 != null && e1 != null && !e0.HasFlag(e1);
        }

        #endregion

        #region public static String[]? GetKeys<...>(...)
        
        public static String[]? GetKeys<T>() where T : Enum { return GetKeys(typeof(T)); }
        public static String[]? GetKeys(Enum? e) { return e != null ? GetKeys(e.GetType()) : null; }
        public static String[]? GetKeys(Type? t)
        {
            if (t != null && t.IsEnum)
                try { return Enum.GetNames(t); } catch { }

            return null;
        }

        #endregion

        #region public static Int32[]? GetValues<...>(...)

        public static Int32?[]? GetValues<T>() where T : Enum { return GetValues(typeof(T)); }
        public static Int32?[]? GetValues(Enum? e) { return e != null ? GetValues(e.GetType()) : null; }
        public static Int32?[]? GetValues(Type? t)
        {
            if (t == null || !t.IsEnum)
                return null;

            Array? a0;
            try { a0 = Enum.GetValues(t); } catch { a0 = null; }

            if (a0 == null)
                return null;

            int?[]? a1 = new int?[a0.Length];
            for (int i = 0; i < a1.Length; i++)
                a1[i] = Int32Utils.Parse(a0.GetValue(i));

            return a1;
        }

        #endregion

        #region public static String[]? GetNNKey<...>(...)

        public static String GetNNKey(Enum? e)
        {
            return StringUtils.NNParse(GetKey(e));
        }

        #endregion

        #region public static String[]? GetKey<...>(...)

        public static String? GetKey(Enum? e)
        {
            if (e != null)
                try { return Enum.GetName(e.GetType(), e); } catch { }

            return null;
        }

        #endregion

        #region public static Int32? GetNNValue<...>(...)

        public static Int32 GetNNValue(Enum? e)
        {
            return Int32Utils.NNParse(e);
        }

        #endregion

        #region public static Int32? GetValue<...>(...)

        public static Int32? GetValue(Enum? e)
        {
            return Int32Utils.Parse(e);
        }

        #endregion

        #region public static T? Parse<T>(...)

        public static T? Parse<T>(object o) where T : Enum { return ObjectUtils.Cast<T>(Parse(typeof(T), o)); }
        public static T? Parse<T>(string? s, bool bIgnoreCase = true) where T : Enum { return ObjectUtils.Cast<T>(Parse(typeof(T), s, bIgnoreCase)); }

        #endregion

        #region public static T NNParse<T>(...)

        public static T NNParse<T>(object o) where T : Enum { return __NNParse<T>(Parse<T>(o)); }
        public static T NNParse<T>(string? s, bool bIgnoreCase = true) where T : Enum { return __NNParse<T>(Parse<T>(s, bIgnoreCase)); }
        private static T __NNParse<T>(T? o) where T : Enum { return o != null ? o : default(T); }

        #endregion

        #region public static Object? Parse(...)

        public static Object? Parse(Type? t, Object? o)
        {
            if (o != null)
            {
                Type ot = o.GetType();
                if (ot == CType.String) return __TryParse(t, (String)o, true);
                return __ToObject(t, o);
            }

            return null;
        }
        public static Object? Parse(Type? t, string? s, bool bIgnoreCase = true)
        {
            return __TryParse(t, s, bIgnoreCase);
        }

        #endregion

        #region public static Object NNParse(...)

        public static Object NNParse(Type? t, Object? o) { return __NNParse(t, Parse(t, o)); }
        public static Object NNParse(Type? t, string? s, bool bIgnoreCase = true) { return __NNParse(t, Parse(t, s, bIgnoreCase)); }
        private static Object __NNParse(Type? t, Object? o) 
        {
            if (o != null) return o;
            int?[]? a = GetValues(t);
            Object? a0 = ArrayUtils.IsValidIndex(a, 0) ? a.GetValue(0) : null;
            return a0 != null ? a0 : EEnum.None;
        }

        #endregion

        #region private static Object? ToObject(...)

        private static Object? __ToObject(Type? t, Object? o)
        {
            if(t != null && t.IsEnum && o != null)
                try { return Enum.ToObject(t, o); } catch { }

            return null;
        }

        #endregion

        #region private static Object? __TryParse(...)

        private static Object? __TryParse(Type? t, String? s, Boolean bIgnoreCase = true)
        {
            if (t == null || !t.IsEnum || s == null)
                return null;

            Object? o;
            try { Enum.TryParse(t, s, bIgnoreCase, out o); } catch { o = null; }
            return o;
        }

        #endregion
    }
}