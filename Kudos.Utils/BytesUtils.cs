using System;
using System.Text;

namespace Kudos.Utils
{
    public static class BytesUtils
    {
        #region public static Byte[] ParseFrom()

        /// <summary>Nullable</summary>
        public static Byte[] ParseFrom(String oString)
        {
            return ParseFrom(oString, Encoding.UTF8);
        }

        /// <summary>Nullable</summary>
        public static Byte[] ParseFrom(String oString, Encoding oEncoding)
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
            return ParseFrom( StringUtils.ToBase64(aBytes), oEncoding);
        }

        /// <summary>Nullable</summary>
        public static Byte[] ToBase64( String oString, Encoding oEncoding = null )
        {
            return ParseFrom( StringUtils.ToBase64(oString, oEncoding), oEncoding );
        }

        #endregion

        #region public static Byte[] ParseFromBase64()

        /// <summary>Nullable</summary>
        public static Byte[] ParseFromBase64( Byte[] aBytes )
        {
            return ParseFromBase64( StringUtils.ParseFrom(aBytes) );
        }

        /// <summary>Nullable</summary>
        public static Byte[] ParseFromBase64( Byte[] aBytes, Encoding oEncoding )
        {
            return ParseFromBase64( StringUtils.ParseFrom(aBytes, oEncoding) );
        }

        /// <summary>Nullable</summary>
        public static Byte[] ParseFromBase64(String oString)
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
