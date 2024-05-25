using System;
using System.Linq;
using System.Runtime.Intrinsics.Arm;
using System.Security.Cryptography;
using System.Text;
using Kudos.Constants;
using Kudos.Crypters.KryptoModule.HashModule.Builders;
using Kudos.Crypters.KryptoModule.HashModule.Descriptors;
using Kudos.Crypters.KryptoModule.HashModule.Enums;
using Kudos.Enums;
using Kudos.Utils;
using Kudos.Utils.Collections;

namespace Kudos.Crypters.KryptoModule.HashModule
{
	public sealed class
        Hash
	:
		AKrypto<HashDescriptor>
	
	{
        #region ... static ...

        public static HashBuilder RequestBuilder()
        {
            HashDescriptor hd = new HashDescriptor();
            return new HashBuilder(ref hd);
        }

        private static HashBuilder __RequestPreconfiguredBuilder()
        {
            return
                RequestBuilder()
                    .SetBinaryEncoding(EBinaryEncoding.Base64)
                    .SetEncoding(Encoding.UTF8);
        }

        public static HashBuilder RequestPreconfiguredMD5Builder()
        {
            return
                __RequestPreconfiguredBuilder()
                    .SetAlgorithm(EHashAlgorithm.MD5);
        }

        public static HashBuilder RequestPreconfiguredSHA1Builder()
        {
            return
                __RequestPreconfiguredBuilder()
                    .SetAlgorithm(EHashAlgorithm.SHA1);
        }


        public static HashBuilder RequestPreconfiguredSHA256Builder()
        {
            return
                __RequestPreconfiguredBuilder()
                    .SetAlgorithm(EHashAlgorithm.SHA256);
        }

        public static HashBuilder RequestPreconfiguredSHA384Builder()
        {
            return
                __RequestPreconfiguredBuilder()
                    .SetAlgorithm(EHashAlgorithm.SHA384);
        }

        public static HashBuilder RequestPreconfiguredSHA512Builder()
        {
            return
                __RequestPreconfiguredBuilder()
                    .SetAlgorithm(EHashAlgorithm.SHA512);
        }

        #endregion

        private readonly HashAlgorithm? _ha;

        internal Hash(ref HashDescriptor hd) : base(ref hd)
        {
            switch (hd.Algorithm)
            {
                case EHashAlgorithm.MD5:
                    try { _ha = System.Security.Cryptography.MD5.Create(); } catch { }
                    break;
                case EHashAlgorithm.SHA1:
                    try { _ha = System.Security.Cryptography.SHA1.Create(); } catch { }
                    break;
                case EHashAlgorithm.SHA256:
                    try { _ha = System.Security.Cryptography.SHA256.Create(); } catch { }
                    break;
                case EHashAlgorithm.SHA384:
                    try { _ha = System.Security.Cryptography.SHA384.Create(); } catch { }
                    break;
                case EHashAlgorithm.SHA512:
                    try { _ha = System.Security.Cryptography.SHA512.Create(); } catch { }
                    break;
            }
        }

        public Boolean Verify(Object? o, String? s)
        {
            if (_ha == null)
                return false;

            Byte[]? baOut;
            _ParseToBytesArray(ref s, out baOut);

            Int32 iSALTLength;
            _FindSALTLength(ref baOut, out iSALTLength);

            Byte[]? baSALT;
            _SplitBytes(ref baOut, ref iSALTLength, out baSALT, out baOut);
            if (baOut == null)
                return false;

            Byte[]? baObject;
            _Compute(ref o, ref baSALT, out baObject);
            String? sObject;
            _ConvertToBaseXString(ref baObject, out sObject);
            _ParseToBytesArray(ref sObject, out baObject);

            return
                baObject != null
                && baObject.SequenceEqual(baOut);
        }

        public String? Compute(Object? o)
        {
            if (_ha == null)
                return null;

            Byte[]? baSALT;
            _RandomSALT(true, out baSALT);

            Byte[]? baOut;
            _Compute(ref o, ref baSALT, out baOut);
            String? sOut;
            _ConvertToBaseXString(ref baOut, out sOut);

            if (sOut == null)
                return null;

            String? sSALT;
            _ParseToString(ref baSALT, out sSALT);

            return
                (
                    sSALT != null
                        ? sSALT
                        : String.Empty
                )
                +
                sOut;
        }

        private void _Compute(ref Object? oIn, ref Byte[]? baSALT, out Byte[]? baOut)
        {
            String? s = JSONUtils.Serialize(oIn);
            Byte[]? baIn;
            _ParseToBytesArray(ref s, out baIn);
            _Compute(ref baIn, ref baSALT, out baOut);
        }

        private void _Compute(ref Byte[]? baIn, ref Byte[]? baSALT, out Byte[]? baOut)
        {
            if (baIn == null) { baOut = null; return; }
            _PrependBytes(ref baIn, ref baSALT, out baIn);
            _Compute(ref baIn, out baOut);
        }

        private void _Compute(ref Byte[]? baIn, out Byte[]? baOut)
        {
            if (baIn != null)
                try { baOut = _ha.ComputeHash(baIn); return; } catch { }

            baOut = null;
        }

        public override void Dispose()
        {
            if (_ha == null) return;
            try { _ha.Dispose(); } catch { }
        }
    }
}