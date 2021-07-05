using Kudos.Enums;
using System;
using System.IO;
using System.IO.Compression;
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

        /// <summary>Nullable</summary>
        public static Byte[] From(Stream oStream, Boolean bDisposeStream = false, Int32 iBufferSize = 4096)
        {
            if (oStream == null || !oStream.CanRead)
                return null;

            if (iBufferSize < 1)
                iBufferSize = 4096;

            Byte[]
                aBuffer = new Byte[iBufferSize],
                aBytes = new Byte[oStream.Length];

            Int32
                iBytesRead,
                iTotalBytesRead = 0;

            if (oStream.CanSeek)
                try { oStream.Position = 0; } catch { }

            try
            {
                while ((iBytesRead = oStream.Read(aBuffer, 0, aBuffer.Length)) > 0)
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
                try { oStream.Dispose(); } catch { }

            return aBytes;
        }


        #endregion

        #region Base16

        #region public static Byte[] ToBase16()

        /// <summary>Nullable</summary>
        public static Byte[] ToBase16(Byte[] aBytes, Encoding oEncoding = null)
        {
            return From(StringUtils.ToBase16(aBytes), oEncoding);
        }

        /// <summary>Nullable</summary>
        public static Byte[] ToBase16(String oString, Encoding oEncoding = null)
        {
            return From(StringUtils.ToBase16(oString, oEncoding), oEncoding);
        }

        #endregion

        #region public static Byte[] FromBase16()

        /// <summary>Nullable</summary>
        public static Byte[] FromBase16(Byte[] aBytes)
        {
            return FromBase16(StringUtils.From(aBytes));
        }

        /// <summary>Nullable</summary>
        public static Byte[] FromBase16(Byte[] aBytes, Encoding oEncoding)
        {
            return FromBase16(StringUtils.From(aBytes, oEncoding));
        }

        /// <summary>Nullable</summary>
        public static Byte[] FromBase16(String oString)
        {
            if (oString != null)
                try
                {
                    return Convert.FromHexString(oString);
                }
                catch
                {
                }

            return null;
        }

        #endregion

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

        #region public static Byte[] Append()
        
        public static Byte[] Append(Byte[] aBytes0, Byte[] aBytes1)
        {
            Int32
                iBLength = 0,
                iBOffset = 0;

            if (aBytes0 != null)
                iBLength += aBytes0.Length;

            if (aBytes1 != null)
                iBLength += aBytes1.Length;

            Byte[]
                aBytes = new Byte[iBLength];

            if (aBytes0 != null)
            {
                Array.Copy(
                    aBytes0,
                    0,
                    aBytes,
                    0,
                    aBytes0.Length
                );

                iBOffset = aBytes0.Length;
            }

            if(aBytes1 != null)
                Array.Copy(
                    aBytes1,
                    0,
                    aBytes,
                    iBOffset,
                    aBytes1.Length
                );

            return aBytes;
        }

        #endregion

        #region public static Byte[] Prepend()

        public static Byte[] Prepend(Byte[] aBytes0, Byte[] aBytes1)
        {
            return Append(aBytes1, aBytes0);
        }

        #endregion

        #region  public static Byte[] SplitIn2()

        public static void SplitIn2(Byte[] aBytes, Int32 iLength, out Byte[] aBytes0, out Byte[] aBytes1)
        {
            aBytes0 = null;
            aBytes1 = null;

            if (
                aBytes == null 
                || !ArrayUtils.IsValidIndex(aBytes, iLength)
            )
                return;

            aBytes0 = new Byte[iLength];
            aBytes1 = new Byte[aBytes.Length - aBytes0.Length];

            Array.Copy(
                aBytes,
                0,
                aBytes0,
                0,
                aBytes0.Length
            );

            Array.Copy(
                aBytes,
                aBytes0.Length,
                aBytes1,
                0,
                aBytes1.Length
            );
        }

        #endregion

        #region Random

        public static Byte[] Random(
            Int32 iLength,
            ECharType eCharType = ECharType.StandardLowerCase | ECharType.StandardUpperCase | ECharType.Numeric
        )
        {
            return Random(Encoding.UTF8, iLength, eCharType);
        }

        public static Byte[] Random(
            Encoding eEncoding,
            Int32 iLength,
            ECharType eCharType = ECharType.StandardLowerCase | ECharType.StandardUpperCase | ECharType.Numeric
        )
        {
            return From(StringUtils.Random(iLength, eCharType), eEncoding);
        }

        #endregion

        #region Compress/Decompress

        #region GZip

        public static Byte[] CompressUsingGZip(String oString, CompressionLevel eCompressionLevel = CompressionLevel.Optimal, Int32 iBufferSize = 4096)
        {
            return From(StreamUtils.CompressUsingGZip(oString, eCompressionLevel, iBufferSize), true, iBufferSize);
        }

        public static Byte[] CompressUsingGZip(String oString, Encoding oEncoding, CompressionLevel eCompressionLevel = CompressionLevel.Optimal, Int32 iBufferSize = 4096)
        {
            return From(StreamUtils.CompressUsingGZip(oString, oEncoding, eCompressionLevel, iBufferSize), true, iBufferSize);
        }

        public static Byte[] CompressUsingGZip(Byte[] aBytes, CompressionLevel eCompressionLevel = CompressionLevel.Optimal, Int32 iBufferSize = 4096)
        {
            return From(StreamUtils.CompressUsingGZip(aBytes, eCompressionLevel, iBufferSize), true, iBufferSize);
        }

        public static Byte[] CompressUsingGZip(Stream oStream, CompressionLevel eCompressionLevel = CompressionLevel.Optimal, Int32 iBufferSize = 4096)
        {
            return From(StreamUtils.CompressUsingGZip(oStream, eCompressionLevel, iBufferSize), true, iBufferSize);
        }

        public static Byte[] DecompressUsingGZip(Stream oStream, Int32 iBufferSize = 4096)
        {
            return From(StreamUtils.DecompressUsingGZip(oStream, iBufferSize), true, iBufferSize);
        }


        #endregion

        #region Deflate

        public static Byte[] CompressUsingDeflate(String oString, CompressionLevel eCompressionLevel = CompressionLevel.Optimal, Int32 iBufferSize = 4096)
        {
            return From(StreamUtils.CompressUsingDeflate(oString, eCompressionLevel, iBufferSize), true, iBufferSize);
        }

        public static Byte[] CompressUsingDeflate(String oString, Encoding oEncoding, CompressionLevel eCompressionLevel = CompressionLevel.Optimal, Int32 iBufferSize = 4096)
        {
            return From(StreamUtils.CompressUsingDeflate(oString, oEncoding, eCompressionLevel, iBufferSize), true, iBufferSize);
        }

        public static Byte[] CompressUsingDeflate(Byte[] aBytes, CompressionLevel eCompressionLevel = CompressionLevel.Optimal, Int32 iBufferSize = 4096)
        {
            return From(StreamUtils.CompressUsingDeflate(aBytes, eCompressionLevel, iBufferSize), true, iBufferSize);
        }

        public static Byte[] CompressUsingDeflate(Stream oStream, CompressionLevel eCompressionLevel = CompressionLevel.Optimal, Int32 iBufferSize = 4096)
        {
            return From(StreamUtils.CompressUsingDeflate(oStream, eCompressionLevel, iBufferSize), true, iBufferSize);
        }

        public static Byte[] DecompressUsingDeflate(Stream oStream, Int32 iBufferSize = 4096)
        {
            return From(StreamUtils.DecompressUsingDeflate(oStream, iBufferSize), true, iBufferSize);
        }

        #endregion

        #endregion
    }
}