using System;
using System.Text;

namespace Kudos.Utils
{
    public static class BytesUtils
    {
        #region public static Byte[] From()

        /// <summary>Nullable</summary>
        public static Byte[] From(String oString)
        {
            return From(oString, Encoding.UTF8);
        }

        /// <summary>Nullable</summary>
        public static Byte[] From(String oString, Encoding oEncoding)
        {
            if(oString != null && oEncoding != null)
                try
                {
                    return oEncoding.GetBytes(oString);
                }
                catch
                {

                }

            return null;
        }

        #endregion

        #region Base64

        #region public static Byte[] ToBase64()

        /// <summary>Nullable</summary>
        public static Byte[] ToBase64(Byte[] aBytes, Encoding oEncoding = null)
        {
            return From( StringUtils.ToBase64(aBytes), oEncoding);
        }

        /// <summary>Nullable</summary>
        public static Byte[] ToBase64( String oString, Encoding oEncoding = null )
        {
            return From( StringUtils.ToBase64(oString, oEncoding), oEncoding );
        }

        #endregion

        #region public static Byte[] FromBase64()

        /// <summary>Nullable</summary>
        public static Byte[] FromBase64( Byte[] aBytes )
        {
            return FromBase64( StringUtils.From(aBytes) );
        }

        /// <summary>Nullable</summary>
        public static Byte[] FromBase64( Byte[] aBytes, Encoding oEncoding )
        {
            return FromBase64( StringUtils.From(aBytes, oEncoding) );
        }

        /// <summary>Nullable</summary>
        public static Byte[] FromBase64(String oString)
        {
            if (oString != null)
                try
                {
                    return Convert.FromBase64String(oString);
                }
                catch
                {
                }

            return null;
        }

        #endregion

        #endregion
    }
}
