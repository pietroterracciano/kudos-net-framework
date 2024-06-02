using Kudos.Constants;
using Kudos.Enums;
using Kudos.Utils.Collections;
using Kudos.Utils.Numerics;
using Kudos.Utils.Texts;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;

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
        public static String[]? GetKeys(Enum? e)
        {
            Enum[]? ea = GetFlags(e);
            String[]? sa; __GetKeys(ref ea, out sa);
            return sa;
        }
        public static String[]? GetKeys(Type? t)
        {
            Enum[]? ea = GetFlags(t);
            String[]? sa; __GetKeys(ref ea, out sa);
            return sa;
        }

        #endregion

        #region public static Int32[]? GetValues<...>(...)

        public static Int32[]? GetValues<T>() where T : Enum { return GetValues(typeof(T)); }
        public static Int32[]? GetValues(Enum? e)
        {
            Enum[]? ea = GetFlags(e);
            int[]? ia; __GetValues(ref ea, out ia);
            return ia;
        }
        public static Int32[]? GetValues(Type? t)
        {
            Enum[]? ea = GetFlags(t);
            int[]? ia; __GetValues(ref ea, out ia);
            return ia;
        }

        #endregion

        #region public static ... GetFlags<...>(...)

        public static T[]? GetFlags<T>() where T : Enum { return ObjectUtils.Cast<T[]?>(GetFlags(typeof(T))); }
        public static T[]? GetFlags<T>(T? t) where T : Enum { return ObjectUtils.Cast<T[]?>(GetFlags(t as Enum)); }
        public static Enum[]? GetFlags(Enum? e) 
        {
            if (e == null)
                return null;

            Enum[]? a = GetFlags(e.GetType());

            if (a == null)
                return null;

            List<Enum> l = new List<Enum>(a.Length);

            for(int i=0; i<a.Length; i++)
            {
                if (!e.HasFlag(a[i])) continue;
                l.Add(a[i]);
            }

            return l.Count > 0 ? l.ToArray() : null;
        }
        public static Enum[]? GetFlags(Type? t)
        {
            if (t == null || !t.IsEnum) return null;
            Array? a;
            try { a = Enum.GetValues(t); } catch { a = null; }
            if (a == null) return null;
            List<Enum> l = new List<Enum>(a.Length);
            Enum? ei;
            for(int i=0; i<a.Length; i++)
            {
                ei = a.GetValue(i) as Enum;
                if (ei == null) continue;
                l.Add(ei);
            }

            return l.Count > 0 ? l.ToArray() : null;
        }

        #endregion

        //#region public static Boolean IsDefined(...)

        //public static Boolean IsDefined<T>(Object? e) where T: Enum { return e != null && IsDefined(typeof(T), e); }
        //public static Boolean IsDefined(Enum? e, Object? o) { return e != null && IsDefined(e.GetType(), o); }
        //public static Boolean IsDefined(Type? t, Object? o)
        //{
        //    if (t != null && t.IsEnum && o != null)
        //        try { return Enum.IsDefined(t, o); } catch { }

        //    return false;
        //}

        //#endregion

        #region public static Boolean IsValid(...)

        public static Boolean IsValid<T>(Object? e) where T : Enum { return e != null && IsValid(typeof(T), e); }
        public static Boolean IsValid(Enum? e, Object? o) { return e != null && IsValid(e.GetType(), o); }
        public static Boolean IsValid(Type? t, Object? o) { return Parse(t, o) != null; }

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
            return StringUtils.Parse(e);
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

        #region public static ... Parse<...>(...)

        public static T Parse<T>(string? s, bool bIgnoreCase = true) where T : Enum { return ObjectUtils.Cast<T>(Parse(typeof(T), s, bIgnoreCase)); }
        public static T Parse<T>(object? o) where T : Enum { return ObjectUtils.Cast<T>(Parse(typeof(T), o)); }
        public static Enum? Parse(Type? t, string? s, bool bIgnoreCase = true)
        {
            Enum? e;
            __TryParse(ref t, ref s, ref bIgnoreCase, out e);
            return e;
        }
        public static Enum? Parse(Type? t, Object? o)
        {
            if (o == null)
                return null;

            Type ot = o.GetType();

            if (ot == CType.String)
                return Parse(t, o as String, true);

            Enum? e;
            __ToObject(ref t, ref o, out e);
            return e;
        }

        #endregion

        //#region public static T NNParse<T>(...)

        //public static T NNParse<T>(object? o) where T : Enum
        //{
        //    return __NNParse<T>(t);
        //}
        //public static T NNParse<T>(string? s, bool bIgnoreCase = true) where T : Enum
        //{
        //    return __NNParse<T>(Parse<T>(s, bIgnoreCase));
        //}
        //private static T __NNParse<T>(T? o) where T : Enum
        //{
        //    return o != null ? o : default(T);
        //}

        //#endregion

        #region private ...

        #region private static void __IsValid(...)

        private static void __IsValid(ref Enum? e, out Boolean b)
        {
            String? s = GetKey(e);
            b = Int32Utils.Parse(s) == null;
        }

        #endregion

        #region private static void ToObject(...)

        private static void __ToObject(ref Type? t, ref Object? oIn, out Enum? e)
        {
            if (t == null || !t.IsEnum || oIn == null) { e = null; return; }
            try
            {
                e = Enum.ToObject(t, oIn) as Enum;
                Boolean b;
                __IsValid(ref e, out b);
                if (!b) e = null;
            }
            catch
            {
                e = null;
            }
        }

        #endregion

        #region private static Object? __TryParse(...)

        private static void __TryParse(ref Type? t, ref String? s, ref Boolean bIgnoreCase, out Enum? e)
        {
            if (t == null || !t.IsEnum || s == null) { e = null; return; }

            try
            {
                Object? o;
                Enum.TryParse(t, s, bIgnoreCase, out o);
                e = o as Enum;
                Boolean b;
                __IsValid(ref e, out b);
                if (!b) e = null;
            }
            catch
            {
                e = null;
            }
        }

        #endregion

        #region private static __GetKeys(...)

        private static void __GetKeys(ref Enum[]? ea, out String[]? sa)
        {
            if (ea == null) { sa = null; return; }

            List<String> l = new List<String>(ea.Length);
            String? s;
            for (int i = 0; i < ea.Length; i++)
            {
                s = StringUtils.Parse(ea[i]);
                if (s == null) continue;
                l.Add(s);
            }

            sa = l.Count > 0 ? l.ToArray() : null;
        }

        #endregion

        #region private static __GetValues(...)

        private static void __GetValues(ref Enum[]? ea, out Int32[]? ia)
        {
            if (ea == null) { ia = null; return; }

            List<Int32> l = new List<int>(ea.Length);
            Int32? j;
            for (int i = 0; i < ea.Length; i++)
            {
                j = Int32Utils.Parse(ea[i]);
                if (j == null) continue;
                l.Add(j.Value);
            }

            ia = l.Count > 0 ? l.ToArray() : null;
        }

        #endregion

        #endregion
    }
}