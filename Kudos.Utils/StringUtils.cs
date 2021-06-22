using Kudos.Defines;
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
        public static String Random(Int32 i32Length)
        {
            return Random(i32Length, ECharType.StandardLowerCase | ECharType.StandardUpperCase | ECharType.Numeric);
        }

        /// <summary>Nullable</summary>
        public static String Random(Int32 i32Length, ECharType eCType)
        {
            if (i32Length < 1)
                return null;

            StringBuilder sbAlphabet = new StringBuilder();

            if (eCType.HasFlag(ECharType.StandardLowerCase))
                sbAlphabet.Append(DCharacters.STANDARD_LOWER_CASE);

            if (eCType.HasFlag(ECharType.StandardUpperCase))
                sbAlphabet.Append(DCharacters.STANDARD_UPPER_CASE);

            if (eCType.HasFlag(ECharType.AccentedLowerCase))
                sbAlphabet.Append(DCharacters.ACCENTED_LOWER_CASE);

            if (eCType.HasFlag(ECharType.AccentedUpperCase))
                sbAlphabet.Append(DCharacters.ACCENTED_UPPER_CASE);

            if (eCType.HasFlag(ECharType.Punctuation))
                sbAlphabet.Append(DCharacters.PUNCTUATION);

            if (eCType.HasFlag(ECharType.Numeric))
                sbAlphabet.Append(DCharacters.NUMERIC);

            if (eCType.HasFlag(ECharType.Math))
                sbAlphabet.Append(DCharacters.MATH);

            if (eCType.HasFlag(ECharType.Special))
                sbAlphabet.Append(DCharacters.SPECIAL);

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

        #region Base64

        #region public static String ToBase64()

        /// <summary>Nullable</summary>
        public static String ToBase64(String oString)
        {
            return ToBase64( BytesUtils.ParseFrom(oString) );
        }

        /// <summary>Nullable</summary>
        public static String ToBase64(String oString, Encoding oEncoding)
        {
            return ToBase64( BytesUtils.ParseFrom(oString, oEncoding) );
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
            return From( BytesUtils.ParseFromBase64(oString) );
        }

        /// <summary>Nullable</summary>
        public static String FromBase64(String oString, Encoding oEncoding)
        {
            return From( BytesUtils.ParseFromBase64(oString), oEncoding);
        }

        /// <summary>Nullable</summary>
        public static String FromBase64(Byte[] aBytes)
        {
            return From(BytesUtils.ParseFromBase64(aBytes));
        }

        /// <summary>Nullable</summary>
        public static String FromBase64(Byte[] aBytes, Encoding oEncoding)
        {
            return From( BytesUtils.ParseFromBase64(aBytes, oEncoding), oEncoding );
        }

        #endregion

        #endregion

        #region Hexadecimal

        #region public static String ToHexadecimal()

        /// <summary>Nullable</summary>
        public static String ToHexadecimal(String oString)
        {
            return ToHexadecimal(oString, Encoding.UTF8);
        }

        /// <summary>Nullable</summary>
        public static String ToHexadecimal(String oString, Encoding oEncoding)
        {
            if (oString == null || oEncoding == null)
                return null;

            Byte[] aBytes = BytesUtils.ParseFrom(oString, oEncoding);
            if (aBytes == null)
                return null;

            StringBuilder oStringBuilder = new StringBuilder();

            for (int i=0; i<aBytes.Length; i++)
                try { oStringBuilder.Append(aBytes[i].ToString("X2")); } catch { return null; }

            return oStringBuilder.ToString();
        }

        #endregion

        #region public static String FromHexadecimal()

        /// <summary>Nullable</summary>
        public static String FromHexadecimal(String sHexadecimal)
        {
            return FromHexadecimal(sHexadecimal, Encoding.UTF8);
        }

        /// <summary>Nullable</summary>
        public static String FromHexadecimal(String sHexadecimal, Encoding oEncoding)
        {
            if (sHexadecimal == null ||  oEncoding == null)
                return null;

            Byte[] aBytes = new Byte[sHexadecimal.Length / 2];

            for (Int32 i = 0; i < aBytes.Length; i++)
                try { aBytes[i] = Convert.ToByte(sHexadecimal.Substring(i * 2, 2), 16); } catch { return null; }

            return From(aBytes, oEncoding);
        }

        #endregion

        #endregion
    }
}
