using System;
using System.Linq;
using System.Runtime.Intrinsics.Arm;
using System.Security.Cryptography;
using System.Text;
using Kudos.Constants;
using Kudos.Crypters.KryptoModule.HashModule.Builders;
using Kudos.Crypters.KryptoModule.HashModule.Descriptors;
using Kudos.Crypters.KryptoModule.HashModule.Enums;
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
            if (s == null)
                return false;

            String?
                sSALT;

            if (s.Contains(CCharacter.Dollar))
            {
                String[]
                    sa = s.Split(CCharacter.Dollar);

                if (ArrayUtils.IsValidIndex(sa, 1))
                {
                    sSALT = sa[0];
                    s = sa[1];
                }
                else if (ArrayUtils.IsValidIndex(sa, 0))
                    sSALT = sa[0];
                else
                    sSALT = null;
            }
            else
                sSALT = null;

            Byte[]? baObject;
            _Compute(ref o, ref sSALT, out baObject);

            if (baObject == null)
                return false;

            Byte[]? baHash;
            _ConvertToBaseXBytesArray(ref s, out baHash);

            if (baHash == null)
                return false;

            return baObject.SequenceEqual(baHash);
        }

        public String? Compute(Object? o)
        {
            Byte[]? baSALT;
            _RandomSALT(out baSALT);

            Byte[]? baOut;
            _Compute(ref o, ref baSALT, out baOut);

            String? sOut;
            _ConvertToBaseXString(ref baOut, out sOut);

            if (sOut == null)
                return null;

            String? sSALT;
            if (baSALT != null)
                _ParseToString(ref baSALT, out sSALT);
            else
                sSALT = null;

            return
                (
                    sSALT != null
                        ? sSALT + CCharacter.Dollar
                        : String.Empty
                )
                +
                sOut;
        }

        private void _Compute(ref Object? oIn, ref String? sSALT, out Byte[]? baOut)
        {
            Byte[]? baSALT; _ParseToBytesArray(ref sSALT, out baSALT);
            _Compute(ref oIn, ref baSALT, out baOut);
        }

        private void _Compute(ref Object? oIn, ref Byte[]? baSALT, out Byte[]? baOut)
        {
            Byte[]? baIn; String? s = JSONUtils.Serialize(oIn); _ParseToBytesArray(ref s, out baIn);
            _Compute(ref baIn, ref baSALT, out baOut);
        }

        private void _Compute(ref Byte[]? baIn, ref Byte[]? baSALT, out Byte[]? baOut)
        {
            _PrependSALT(ref baIn, ref baSALT, out baIn);
            _Compute(ref baIn, out baOut);
        }

        private void _Compute(ref Byte[]? baIn, out Byte[]? baOut)
        {
            if (_ha != null && baIn != null)
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