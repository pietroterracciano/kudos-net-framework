using System;
using System.Text;
using Kudos.Constants;
using Kudos.Enums;
using Kudos.Utils.Numerics;

namespace Kudos.Utils.Texts
{
    public static class StringUtils
    {
        #region public static String? Join()

        public static string? Join(string? s, string[]? a)
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

        public static String? Parse(Object? o) { return ObjectUtils.Parse<String>(o); }
        public static String? Parse(Byte[]? ba) { return Parse(ba, Encoding.UTF8); }
        public static String? Parse(Byte[]? ba, Encoding? enc)
        {
            if (ba != null && enc != null)
                try { return enc.GetString(ba); } catch { }

            return null;
        }

        #endregion

        #region public static String NNParse()

        public static String NNParse(Object? o) { return ObjectUtils.Parse<String>(o, true); }
        public static String NNParse(Byte[]? ba) { return NNParse(Parse(ba)); }
        public static String NNParse(Byte[]? ba, Encoding? enc) { return NNParse(Parse(ba, enc)); }

        #endregion

        #region public static String? Truncate()

        public static string? Truncate(string? s, int i)
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

        #region public static String? Random()

        public static String Random(Int32 i, ECharType ect)
        {
            if (i < 1)
                return String.Empty;

            StringBuilder sb = new StringBuilder();

            if (ect.HasFlag(ECharType.StandardLowerCase))
                sb.Append(CCharacters.StandardLowerCase);

            if (ect.HasFlag(ECharType.StandardUpperCase))
                sb.Append(CCharacters.StandardUpperCase);

            if (ect.HasFlag(ECharType.AccentedLowerCase))
                sb.Append(CCharacters.AccentedLowerCase);

            if (ect.HasFlag(ECharType.AccentedUpperCase))
                sb.Append(CCharacters.AccentedUpperCase);

            if (ect.HasFlag(ECharType.Punctuation))
                sb.Append(CCharacters.Punctuation);

            if (ect.HasFlag(ECharType.Numeric))
                sb.Append(CCharacters.Numeric);

            if (ect.HasFlag(ECharType.Mathematical))
                sb.Append(CCharacters.Mathematical);

            if (ect.HasFlag(ECharType.Special))
                sb.Append(CCharacters.Special);

            String s = sb.ToString();

            if (s.Length < 1)
                return String.Empty;

            sb.Clear();

            for (Int32 j = 0; j < i; j++)
                sb.Append(s[Int32Utils.Random(0, s.Length) % s.Length]);

            return sb.ToString();
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
