using Kudos.Constants;
using Kudos.Enums;
using Kudos.Utils.Numerics;
using Kudos.Utils.Texts.Internals;
using System;
using System.Text;

namespace Kudos.Utils.Texts
{
    public static class StringUtils
    {
        private static readonly INTStringUtils __;
        static StringUtils() { __ = new INTStringUtils(); }

        #region public static String? Join()

        public static string? Join(string s, string[]? a)
        {
            if (s != null && a != null)
                try { return string.Join(s, a); } catch { }

            return null;
        }

        public static string? Join(char? c, string[]? a)
        {
            if (c != null && a != null)
                try { return string.Join(c.Value, a); } catch { }

            return null;
        }

        public static string? Join(char? c, object[]? a)
        {
            if (c != null && a != null)
                try { return string.Join(c.Value, a); } catch { }

            return null;
        }

        #endregion

        #region public static String? Reverse()

        public static string? Reverse(string? s)
        {
            if (s == null) return null;
            char[] a = s.ToCharArray();
            Array.Reverse(a);
            return new string(a);
        }

        #endregion

        #region public static String? Parse()

        public static String? Parse(Object? o, Encoding? enc = null) { String? s; __.Parse(ref o, out s); return s; }

        #endregion

        #region public static String NNParse()

        public static String NNParse(Object? o, Encoding? enc = null) { String s; __.NNParse(ref o, out s); return s; }

        #endregion

        #region public static String? Truncate()

        public static string? Truncate(string s, int i)
        {
            if (s == null)
                return null;
            else if (s.Length > i)
                return i > 0
                    ? s.Substring(0, i)
                    : string.Empty;
            else
                return s;
        }

        #endregion

        #region Base16

        #region public static String? ConvertToBase16()

        public static string? ConvertToBase16(string? s) { return ConvertToBase16(s, Encoding.UTF8); }
        public static string? ConvertToBase16(string? s, Encoding? enc) { return ConvertToBase16(BytesUtils.Parse(s, enc)); }
        public static string? ConvertToBase16(byte[]? a)
        {
            if (a != null)
                try { return Convert.ToHexString(a); } catch { }

            return null;
        }

        #endregion

        #region public static String? ConvertFromBase16()

        public static string? ConvertFromBase16(string? s) { return ConvertFromBase16(s, Encoding.UTF8); }
        public static string? ConvertFromBase16(string? s, Encoding? enc) { return Parse(BytesUtils.ConvertFromBase16(s), enc); }
        public static string? ConvertFromBase16(byte[]? a) { return ConvertFromBase16(a, Encoding.UTF8); }
        public static string? ConvertFromBase16(byte[]? a, Encoding? enc) { return Parse(BytesUtils.ConvertFromBase16(a, enc), enc); }

        #endregion

        #endregion

        #region Base64

        #region public static String? ConvertToBase64()

        public static string? ConvertToBase64(string? s) { return ConvertToBase64(s, Encoding.UTF8); }
        public static string? ConvertToBase64(string? s, Encoding? enc) { return ConvertToBase64(BytesUtils.Parse(s, enc)); }
        public static string? ConvertToBase64(byte[]? a)
        {
            if (a != null)
                try { return Convert.ToBase64String(a); } catch { }

            return null;
        }

        #endregion

        #region public static String? ConvertFromBase64()

        public static string? ConvertFromBase64(string? s) { return ConvertFromBase64(s, Encoding.UTF8); }
        public static string? ConvertFromBase64(string? s, Encoding? enc) { return Parse(BytesUtils.ConvertFromBase64(s), enc); }
        public static string? ConvertFromBase64(byte[]? a) { return ConvertFromBase64(a, Encoding.UTF8); }
        public static string? ConvertFromBase64(byte[]? a, Encoding? enc) { return Parse(BytesUtils.ConvertFromBase64(a, enc), enc); }

        #endregion

        #endregion
    }
}
