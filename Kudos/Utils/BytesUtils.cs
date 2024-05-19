using Kudos.Enums;
using Kudos.Utils.Collections;
using Kudos.Utils.Texts;
using System;
using System.IO;
using System.IO.Compression;
using System.Text;

namespace Kudos.Utils
{
    public static class BytesUtils
    {
        #region public static Byte[]? Parse()

        public static Byte[]? Parse(String? s) { return Parse(s, Encoding.UTF8); }

        public static Byte[]? Parse(String? s, Encoding? enc)
        {
            if (s != null && enc != null) 
                try { return enc.GetBytes(s); } catch { }

            return null;
        }

        public static Byte[]? Parse(Stream? str, Boolean bDisposeStream = false, Int32 iBufferSize = 4096)
        {
            if (str == null || !str.CanRead)
                return null;

            if (iBufferSize < 1)
                iBufferSize = 4096;

            Byte[]
                aBuffer = new Byte[iBufferSize];
            Byte[]?
                aBytes = new Byte[str.Length];

            Int32
                iBytesRead,
                iTotalBytesRead = 0;

            if (str.CanSeek)
                try { str.Position = 0; } catch { }

            try
            {
                while ((iBytesRead = str.Read(aBuffer, 0, aBuffer.Length)) > 0)
                {
                    Buffer.BlockCopy(aBuffer, 0, aBytes, iTotalBytesRead, iBytesRead);
                    iTotalBytesRead += iBytesRead;
                }
            }
            catch
            {
                aBytes = null;
            }

            if (bDisposeStream)
                try { str.Dispose(); } catch { }

            return aBytes;
        }


        #endregion

        #region Base16

        #region public static Byte[]? ConvertToBase16()

        public static Byte[]? ConvertToBase16(Byte[]? a) { return ConvertToBase16(a, Encoding.UTF8); }
        public static Byte[]? ConvertToBase16(Byte[]? a, Encoding? enc) { return Parse(StringUtils.ConvertToBase16(a), enc); }
        public static Byte[]? ConvertToBase16(String? s) { return ConvertToBase16(s, Encoding.UTF8); }
        public static Byte[]? ConvertToBase16(String? s, Encoding? enc) { return Parse(StringUtils.ConvertToBase16(s, enc), enc); }

        #endregion

        #region public static Byte[]? ConvertFromBase16()

        public static Byte[]? ConvertFromBase16(Byte[]? a) { return ConvertFromBase16(a, Encoding.UTF8); }
        public static Byte[]? ConvertFromBase16(Byte[]? a, Encoding? enc) { return ConvertFromBase16(StringUtils.Parse(a, enc)); }
        public static Byte[]? ConvertFromBase16(String? s)
        {
            if (s != null) try { return Convert.FromHexString(s); } catch { }
            return null;
        }

        #endregion

        #endregion

        #region Base16

        #region public static Byte[]? ConvertToBase64()

        public static Byte[]? ConvertToBase64(Byte[]? a) { return ConvertToBase64(a, Encoding.UTF8); }
        public static Byte[]? ConvertToBase64(Byte[]? a, Encoding? enc) { return Parse(StringUtils.ConvertToBase64(a), enc); }
        public static Byte[]? ConvertToBase64(String? s) { return ConvertToBase64(s, Encoding.UTF8); }
        public static Byte[]? ConvertToBase64(String? s, Encoding? enc) { return Parse(StringUtils.ConvertToBase64(s, enc), enc); }

        #endregion

        #region public static Byte[]? ConvertFromBase64()

        public static Byte[]? ConvertFromBase64(Byte[]? a) { return ConvertFromBase64(a, Encoding.UTF8); }
        public static Byte[]? ConvertFromBase64(Byte[]? a, Encoding? enc) { return ConvertFromBase64(StringUtils.Parse(a, enc)); }
        public static Byte[]? ConvertFromBase64(String? s)
        {
            if (s != null) try { return Convert.FromBase64String(s); } catch { }
            return null;
        }

        #endregion

        #endregion
    }
}