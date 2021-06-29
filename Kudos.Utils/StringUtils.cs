using Kudos.Constants;
using Kudos.Enums;
using Kudos.Utils.Integers;
using System;
using System.Text;

namespace Kudos.Utils
{
    public static class StringUtils
    {
        #region public static String Random()

        /// <summary>Nullable</summary>
        public static String Random(Int32 i32Length, ECharType eCType = ECharType.StandardLowerCase | ECharType.StandardUpperCase | ECharType.Numeric)
        {
            if (i32Length < 1)
                return null;

            StringBuilder sbAlphabet = new StringBuilder();

            if (eCType.HasFlag(ECharType.StandardLowerCase))
                sbAlphabet.Append(CCharacters.STANDARD_LOWER_CASE);

            if (eCType.HasFlag(ECharType.StandardUpperCase))
                sbAlphabet.Append(CCharacters.STANDARD_UPPER_CASE);

            if (eCType.HasFlag(ECharType.AccentedLowerCase))
                sbAlphabet.Append(CCharacters.ACCENTED_LOWER_CASE);

            if (eCType.HasFlag(ECharType.AccentedUpperCase))
                sbAlphabet.Append(CCharacters.ACCENTED_UPPER_CASE);

            if (eCType.HasFlag(ECharType.Punctuation))
                sbAlphabet.Append(CCharacters.PUNCTUATION);

            if (eCType.HasFlag(ECharType.Numeric))
                sbAlphabet.Append(CCharacters.NUMERIC);

            if (eCType.HasFlag(ECharType.Math))
                sbAlphabet.Append(CCharacters.MATH);

            if (eCType.HasFlag(ECharType.Special))
                sbAlphabet.Append(CCharacters.SPECIAL);

            String sAlphabet = sbAlphabet.ToString();

            if (sAlphabet.Length < 1)
                return null;

            StringBuilder sbRandom = new StringBuilder();

            for (Int32 i = 0; i < i32Length; i++)
                sbRandom.Append(sAlphabet[Int32Utils.Random(0, sAlphabet.Length) % sAlphabet.Length]);

            return sbRandom.ToString();
        }

        #endregion

        #region public static String[] Split()

        /// <summary>Nullable</summary>
        public static String[] Split(String oString, Int32 i32EveryNChars)
        {
            if (oString == null || i32EveryNChars == 0)
                return null;

            Boolean bIsPositiveLoop = i32EveryNChars > 0;

            if (!bIsPositiveLoop)
                i32EveryNChars = -i32EveryNChars;

            Int32 i32SPieces = Int32Utils.Ceiling(oString.Length, i32EveryNChars);
            if (i32SPieces < 1)
                return null;

            Int32[] aSPLength = new Int32[i32SPieces];

            Int32 j = 0;
            for (Int32 i = 0; i < oString.Length; i++)
            {
                if (i > 0 && i % i32EveryNChars == 0)
                    j++;

                aSPLength[j]++;
            }

            if (!bIsPositiveLoop)
            {
                Int32 i32SPL0 = aSPLength[0];
                aSPLength[0] = aSPLength[j];
                aSPLength[j] = i32SPL0;
            }

            j = 0;

            String[] aSPieces = new String[i32SPieces];
            Int32 k = 0;
            StringBuilder oStringBuilder = new StringBuilder(i32EveryNChars);
            for (Int32 i = 0; i < oString.Length; i++)
            {
                if (k > 0 && k % aSPLength[j] == 0)
                {
                    k = 0;
                    aSPieces[j++] = oStringBuilder.ToString();
                    oStringBuilder.Clear();
                }

                oStringBuilder.Append(oString[i]);
                k++;
            }

            aSPieces[j++] = oStringBuilder.ToString();

            return aSPieces;
        }

        #endregion

        #region public static String Reverse()

        /// <summary>Nullable</summary>
        public static String Reverse(String oString)
        {
            if (oString == null)
                return null;

            Char[] aSChars = oString.ToCharArray();
            Array.Reverse(aSChars);
            return new String(aSChars);
        }

        #endregion

        #region public static String From()

        public static String From(Object oObject)
        {
            if (oObject != null)
                try
                {
                    return Convert.ToString(oObject);
                }
                catch
                {
                }

            return null;
        }

        /// <summary>Nullable</summary>
        public static String From(Byte[] aBytes)
        {
            return From(aBytes, Encoding.UTF8);
        }

        /// <summary>Nullable</summary>
        public static String From(Byte[] aBytes, Encoding oEncoding)
        {
            if(aBytes != null && oEncoding != null)
                try
                {
                    return oEncoding.GetString(aBytes);
                }
                catch
                {

                }

            return null;
        }

        #endregion

        #region public static ToNotNullable()

        public static String ToNotNullable(String oString)
        {
            return oString != null ? oString : "";
        }

        #endregion

        #region Base16

        #region public static String ToBase16()

        /// <summary>Nullable</summary>
        public static String ToBase16(String oString)
        {
            return ToBase16(BytesUtils.From(oString));
        }

        /// <summary>Nullable</summary>
        public static String ToBase16(String oString, Encoding oEncoding)
        {
            return ToBase16(BytesUtils.From(oString, oEncoding));
        }

        /// <summary>Nullable</summary>
        public static String ToBase16(Byte[] aBytes)
        {
            if (aBytes != null)
                try
                {
                    return Convert.ToHexString(aBytes);
                }
                catch
                {
                }

            return null;
        }

        #endregion

        #region public static String FromBase16()

        /// <summary>Nullable</summary>
        public static String FromBase16(String oString)
        {
            return From(BytesUtils.FromBase16(oString));
        }

        /// <summary>Nullable</summary>
        public static String FromBase16(String oString, Encoding oEncoding)
        {
            return From(BytesUtils.FromBase16(oString), oEncoding);
        }

        /// <summary>Nullable</summary>
        public static String FromBase16(Byte[] aBytes)
        {
            return From(BytesUtils.FromBase16(aBytes));
        }

        /// <summary>Nullable</summary>
        public static String FromBase16(Byte[] aBytes, Encoding oEncoding)
        {
            return From(BytesUtils.FromBase16(aBytes, oEncoding), oEncoding);
        }

        #endregion

        #endregion

        #region Base64

        #region public static String ToBase64()

        /// <summary>Nullable</summary>
        public static String ToBase64(String oString)
        {
            return ToBase64( BytesUtils.From(oString) );
        }

        /// <summary>Nullable</summary>
        public static String ToBase64(String oString, Encoding oEncoding)
        {
            return ToBase64( BytesUtils.From(oString, oEncoding) );
        }

        /// <summary>Nullable</summary>
        public static String ToBase64(Byte[] aBytes)
        {
            if (aBytes != null)
                try
                {
                    return Convert.ToBase64String(aBytes);
                }
                catch
                {
                }

            return null;
        }

        #endregion

        #region public static String FromBase64()

        /// <summary>Nullable</summary>
        public static String FromBase64(String oString)
        {
            return From( BytesUtils.FromBase64(oString) );
        }

        /// <summary>Nullable</summary>
        public static String FromBase64(String oString, Encoding oEncoding)
        {
            return From( BytesUtils.FromBase64(oString), oEncoding);
        }

        /// <summary>Nullable</summary>
        public static String FromBase64(Byte[] aBytes)
        {
            return From(BytesUtils.FromBase64(aBytes));
        }

        /// <summary>Nullable</summary>
        public static String FromBase64(Byte[] aBytes, Encoding oEncoding)
        {
            return From( BytesUtils.FromBase64(aBytes, oEncoding), oEncoding );
        }

        #endregion

        #endregion
    }
}
