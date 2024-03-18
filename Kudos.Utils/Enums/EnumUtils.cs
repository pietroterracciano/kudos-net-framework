using Kudos.Utils.Numerics.Integers;
using System;

namespace Kudos.Utils.Enums
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

        #region public static String[]? GetKeys(...)

        public static string[] GetKeys(Type t)
        {
            if (t != null)
                try { return Enum.GetNames(t); } catch { }

            return null;
        }

        public static string[] GetKeys(Enum e)
        {
            return
                e != null
                    ? GetKeys(e.GetType())
                    : null;
        }

        #endregion

        #region public static String? GetKey(...)

        public static string GetKey(Enum e)
        {
            if (e != null)
                try { return Enum.GetName(e.GetType(), e); } catch { }

            return null;
        }

        #endregion

        #region public static Int32[]? GetValues(...)

        public static int[] GetValues(Type t)
        {
            if (t == null)
                return null;

            Array a0 = GetValue(t);
            if (a0 == null)
                return null;

            int[] a1 = new int[a0.Length];
            for (int i = 0; i < a1.Length; i++)
                a1[i] = Int32Utils.From(a0.GetValue(i));

            return a1;
        }

        #endregion

        #region private static Array GetValues()

        private static Array GetValue(Type e)
        {
            if (e != null)
                try { return Enum.GetValues(e); } catch { }

            return null;
        }

        #endregion

        #region public static Int32? GetValue()

        public static int? GetValue(Enum e)
        {
            return Int32NUtils.From(e);
        }

        #endregion

        #region public static Object? From(...)

        public static EnumType? From<EnumType>(object o) where EnumType : Enum
        {
            return ObjectUtils.Cast<EnumType>(From(typeof(EnumType), o));
        }
        public static object From(Type t, object o)
        {
            if (t != null && o != null)
                try { return Enum.ToObject(t, o); } catch { }

            return null;
        }
        public static EnumType? From<EnumType>(String? s, bool bIgnoreCase = true) where EnumType : Enum
        {
            return ObjectUtils.Cast<EnumType>(From(typeof(EnumType), s, bIgnoreCase));
        }
        public static object From(Type? t, String? s, bool bIgnoreCase = true)
        {
            if (t != null && s != null)
                try { return Enum.Parse(t, s, bIgnoreCase); } catch { }

            return null;
        }

        #endregion
    }
}