using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kudos.Utils
{
    public static class StreamUtils
    {
        #region From

        public static Stream From(String oString, Int32 iBufferSize = 4096, Boolean bIsWritable = false)
        {
            return From(oString, Encoding.UTF8, iBufferSize, bIsWritable);
        }

        public static Stream From(String oString, Encoding oEncoding, Int32 iBufferSize = 4096, Boolean bIsWritable = false)
        {
            Byte[] aBytes = BytesUtils.From(oString, oEncoding);
            Stream oStream;
            From(ref aBytes, ref iBufferSize, ref bIsWritable, out oStream);
            return oStream;
        }

        public static Stream From(Byte[] aBytes, Int32 iBufferSize = 4096, Boolean bIsWritable = false)
        {
            Stream oStream;
            From(ref aBytes, ref iBufferSize, ref bIsWritable, out oStream);
            return oStream;
        }

        private static void From(ref Byte[] aBytes, ref Int32 iBufferSize , ref Boolean bIsWritable, out Stream oStream)
        {
            if(aBytes != null)
                try
                {
                    oStream = new BufferedStream(new MemoryStream(aBytes, 0, aBytes.Length, bIsWritable, false), iBufferSize > 0 ? iBufferSize : 4096);
                    return;
                }
                catch
                {
                }

            oStream = null;
        }

        #endregion

        #region Compress/Decompress

        #region GZip

        //public static Stream CreateGZipCompression(Stream oStream, CompressionLevel eCompressionLevel = CompressionLevel.Optimal, Int32 iBufferSize = 4096)
        //{
        //    try { return new BufferedStream(new GZipStream(oStream, eCompressionLevel), iBufferSize > 0 ? iBufferSize : 4096); }
        //    catch { return null;  }
        //}

        //public static Stream CreateGZipDecompression(Stream oStream, Int32 iBufferSize = 4096)
        //{
        //    try { return new BufferedStream(new GZipStream(oStream, CompressionMode.Decompress), iBufferSize > 0 ? iBufferSize : 4096); }
        //    catch { return null; }
        //}

        public static Stream CompressUsingGZip(String oString, CompressionLevel eCompressionLevel = CompressionLevel.Optimal, Int32 iBufferSize = 4096)
        {
            return StreamUtils.CompressUsingGZip(From(oString), eCompressionLevel, iBufferSize);
        }

        public static Stream CompressUsingGZip(String oString, Encoding oEncoding, CompressionLevel eCompressionLevel = CompressionLevel.Optimal, Int32 iBufferSize = 4096)
        {
            return StreamUtils.CompressUsingGZip(From(oString, oEncoding), eCompressionLevel, iBufferSize);
        }

        public static Stream CompressUsingGZip(Byte[] aBytes, CompressionLevel eCompressionLevel = CompressionLevel.Optimal, Int32 iBufferSize = 4096)
        {
            return StreamUtils.CompressUsingGZip(From(aBytes), eCompressionLevel, iBufferSize);
        }

        public static Stream CompressUsingGZip(Stream oStream, CompressionLevel eCompressionLevel = CompressionLevel.Optimal, Int32 iBufferSize = 4096)
        {
            Stream oCompressedStream;
            CompressDecompress(ref oStream, true, true, ref eCompressionLevel, ref iBufferSize, out oCompressedStream);
            return oCompressedStream;
        }

        public static Stream DecompressUsingGZip(Stream oStream, Int32 iBufferSize = 4096)
        {
            CompressionLevel eCompressionLevel = CompressionLevel.NoCompression;
            Stream oDecompressedStream;
            CompressDecompress(ref oStream, true, false, ref eCompressionLevel, ref iBufferSize, out oDecompressedStream);
            return oDecompressedStream;
        }

        #endregion

        #region Deflate

        //public static Stream CreateDeflateCompression(Stream oStream, CompressionLevel eCompressionLevel = CompressionLevel.Optimal, Int32 iBufferSize = 4096)
        //{
        //    try { return new BufferedStream(new DeflateStream(oStream, eCompressionLevel), iBufferSize > 0 ? iBufferSize : 4096); }
        //    catch { return null; }
        //}

        //public static Stream CreateDeflateDecompression(Stream oStream, Int32 iBufferSize = 4096)
        //{
        //    try { return new BufferedStream(new DeflateStream(oStream, CompressionMode.Decompress), iBufferSize > 0 ? iBufferSize : 4096); }
        //    catch { return null; }
        //}
        public static Stream CompressUsingDeflate(String oString, CompressionLevel eCompressionLevel = CompressionLevel.Optimal, Int32 iBufferSize = 4096)
        {
            return StreamUtils.CompressUsingDeflate(From(oString), eCompressionLevel, iBufferSize);
        }

        public static Stream CompressUsingDeflate(String oString, Encoding oEncoding, CompressionLevel eCompressionLevel = CompressionLevel.Optimal, Int32 iBufferSize = 4096)
        {
            return StreamUtils.CompressUsingDeflate(From(oString, oEncoding), eCompressionLevel, iBufferSize);
        }

        public static Stream CompressUsingDeflate(Byte[] aBytes, CompressionLevel eCompressionLevel = CompressionLevel.Optimal, Int32 iBufferSize = 4096)
        {
            return StreamUtils.CompressUsingDeflate(From(aBytes), eCompressionLevel, iBufferSize);
        }

        public static Stream CompressUsingDeflate(Stream oStream, CompressionLevel eCompressionLevel = CompressionLevel.Optimal, Int32 iBufferSize = 4096)
        {
            Stream oCompressedStream;
            CompressDecompress(ref oStream, false, true, ref eCompressionLevel, ref iBufferSize, out oCompressedStream);
            return oCompressedStream;
        }

        public static Stream DecompressUsingDeflate(Stream oStream, Int32 iBufferSize = 4096)
        {
            CompressionLevel eCompressionLevel = CompressionLevel.NoCompression;
            Stream oDecompressedStream;
            CompressDecompress(ref oStream, false, false, ref eCompressionLevel, ref iBufferSize, out oDecompressedStream);
            return oDecompressedStream;
        }

        #endregion

        private static void CompressDecompress(
            ref Stream oInputStream,
            Boolean bGZip,
            Boolean bCompress,
            ref CompressionLevel eCompressionLevel,
            ref Int32 iBufferSize,
            out Stream oOutputStream
        )
        {
            if (oInputStream == null || !oInputStream.CanRead)
            {
                oOutputStream = null;
                return;
            }

            if (iBufferSize < 1)
                iBufferSize = 4096;

            try
            {
                oOutputStream = new BufferedStream(new MemoryStream(), iBufferSize);
            }
            catch
            {
                oOutputStream = null;
                return;
            }

            Stream
                oCDStream;

            if(bGZip)
            {
                if (bCompress)
                    try { oCDStream = new GZipStream(oOutputStream, eCompressionLevel, true); }
                    catch { oCDStream= null; }
                else
                    try { oCDStream= new GZipStream(oOutputStream, CompressionMode.Decompress, true); }
                    catch { oCDStream = null; }
            }
            else
            {
                if (bCompress)
                    try { oCDStream = new DeflateStream(oOutputStream, eCompressionLevel, true); }
                    catch { oCDStream = null; }
                else
                    try { oCDStream= new DeflateStream(oOutputStream, CompressionMode.Decompress, true); }
                    catch { oCDStream = null; }
            }

            if(oCDStream == null)
            {
                try { oOutputStream.Dispose(); } catch { }
                return;
            }

            Byte[]
                aBuffer = new Byte[iBufferSize];

            Int32
                iBytesRead;

            if (oInputStream.CanSeek)
                try { oInputStream.Position = 0; } catch { }

            try
            {
                while ((iBytesRead = oInputStream.Read(aBuffer, 0, aBuffer.Length)) > 0)
                    oCDStream.Write(aBuffer, 0, iBytesRead);
            }
            catch
            {
                try { oOutputStream.Dispose(); } catch { }
            }

            try { oCDStream.Dispose(); } catch { }
        }

        #endregion
    }
}