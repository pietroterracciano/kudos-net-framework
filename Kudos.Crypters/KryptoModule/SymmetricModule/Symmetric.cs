using System;
using Kudos.Constants;
using Kudos.Utils;
using Kudos.Utils.Collections;
using System.Linq;
using System.Security.Cryptography;
using Kudos.Crypters.KryptoModule.SymmetricModule.Descriptors;
using Kudos.Crypters.KryptoModule.SymmetricModule.Builders;
using Kudos.Crypters.KryptoModule.HashModule.Enums;
using Kudos.Crypters.KryptoModule.SymmetricModule.Enums;
using System.Text;
using Kudos.Crypters.KryptoModule.SymmetricModule.Utils;
using Kudos.Utils.Numerics;
using Kudos.Enums;
using Kudos.Crypters.KryptoModule.Enums;

namespace Kudos.Crypters.KryptoModule.SymmetricModule
{
    public sealed class
        Symmetric
    :
        AKrypto<SymmetricDescriptor>

    {
        #region ... static ...

        private static readonly SymmetricAlgorithm? __sa;

        static Symmetric()
        {
            try { __sa = Aes.Create(); } catch { __sa = null; }
        }

        public static Byte[]? GenerateKey(ESymmetricKeySize ess)
        {
            if (__sa == null)
                return null;

            Int32? i;
            SymmetricKeySizeUtils.GetAsInt32(ref ess, out i);

            if (i == null)
                return null;

            lock (__sa)
            {
                try
                {
                    __sa.KeySize = i.Value;
                    __sa.GenerateKey();
                    return __sa.Key;
                }
                catch
                {
                    return null;
                }
            }
        }

        public static Byte[]? GenerateIV()
        {
            if (__sa == null)
                return null;

            lock (__sa)
            {
                try
                {
                    __sa.GenerateIV();
                    return __sa.IV;
                }
                catch
                {
                    return null;
                }
            }
        }

        public static SymmetricBuilder RequestBuilder()
        {
            SymmetricDescriptor hd = new SymmetricDescriptor();
            return new SymmetricBuilder(ref hd);
        }

        private static SymmetricBuilder __RequestPreconfiguredBuilder()
        {
            return
                RequestBuilder()
                    .SetBinaryEncoding(EBinaryEncoding.Base64)
                    .SetKeySize(ESymmetricKeySize._256bit)
                    .SetEncoding(Encoding.UTF8)
                    .SetPaddingMode(PaddingMode.PKCS7);
        }

        public static SymmetricBuilder RequestPreconfiguredAesBuilder()
        {
            return
                __RequestPreconfiguredBuilder()
                    .SetAlgorithm(ESymmetricAlgorithm.Aes)
                    .SetCipherMode(CipherMode.CFB);
        }

        public static SymmetricBuilder RequestPreconfiguredAesCngBuilder()
        {
            return
                __RequestPreconfiguredBuilder()
                    .SetAlgorithm(ESymmetricAlgorithm.AesCng);
        }

        #endregion

        private readonly Int32 _iKeySizeInBytes;
        private Int32 _iIVSizeInBytes;//, BlockSizeInBytes;
        private readonly SymmetricAlgorithm? _sa;
        private readonly EInternalSerialization? _eis;

        internal Symmetric(ref SymmetricDescriptor sd) : base(ref sd)
        {
            _iIVSizeInBytes = 16;

            if (sd.InternalSerialization == null) return;
            _eis = sd.InternalSerialization;

            if (sd.KeySize == null) return;
            ESymmetricKeySize eKeySize = sd.KeySize.Value;
            Int32? iKeySize;
            SymmetricKeySizeUtils.GetAsInt32(ref eKeySize, out iKeySize);
            if (iKeySize == null) return;
            _iKeySizeInBytes = iKeySize.Value / 8;

            Byte[]?
                baKey;

            if (sd.BAKey != null)
                baKey = sd.BAKey;
            else if (sd.SKey != null)
            {
                _ParseToBytesArray(ref sd.SKey, out baKey);
                if (baKey == null) return;
            }
            else
                return;

            Byte[]
                baPaddedKey;

            if(baKey.Length != _iKeySizeInBytes)
            {
                baPaddedKey = new Byte[_iKeySizeInBytes];

                Array.Copy(
                    baKey,
                    0,
                    baPaddedKey,
                    0,
                    baKey.Length > _iKeySizeInBytes
                        ? _iKeySizeInBytes
                        : baKey.Length
                );
            }
            else
                baPaddedKey = baKey;

            switch (sd.Algorithm)
            {
                case ESymmetricAlgorithm.Aes:
                    try { _sa = Aes.Create(); } catch { }
                    break;
                //case ESymmetricAlgorithm.AesCcm:
                //    try { _sa = AesCcm.Create(); } catch { }
                //    break;
                case ESymmetricAlgorithm.AesCng:
                    sd.CipherMode = null;
                    try { _sa = AesCng.Create(); } catch { }
                    break;
                    //case ESymmetricAlgorithm.AesGcm:
                    //    try { _sa = AesGcm.Create(); } catch { }
                    //    break;
            }

            if (_sa == null)
                return;

            try
            {
                if (sd.CipherMode != null) _sa.Mode = sd.CipherMode.Value;
                if (sd.PaddingMode != null) _sa.Padding = sd.PaddingMode.Value;
                _sa.KeySize = iKeySize.Value;
                _sa.Key = baPaddedKey;
            }
            catch
            {
                _sa = null;
            }
        }

        public String? Encrypt(Object? o)
        {
            if (_sa == null)
                return null;

            String?
                s = _eis == EInternalSerialization.JSON
                    ? JSONUtils.Serialize(o)
                    : ObjectUtils.Parse<String>(o);

            Byte[]? baIn;
            _ParseToBytesArray(ref s, out baIn);
            if (baIn == null)
                return null;

            Byte[]? baSALT;
            _RandomSALT(true, out baSALT);
            _PrependBytes(ref baIn, ref baSALT, out baIn);

            Byte[]? baOut, baIV;
            ICryptoTransform ct;

            lock (_sa)
            {
                try
                {
                    _sa.GenerateIV();
                    ct = _sa.CreateEncryptor();
                    baIV = _sa.IV;
                }
                catch
                {
                    return null;
                }
            }

            _Transform(ref baIn, ref ct, out baOut);
            if (baOut == null)
                return null;

            _PrependBytes(ref baOut, ref baIV, out baOut);
            _ConvertToBaseXString(ref baOut, out s);

            return s;
        }

        public T? Decrypt<T>(String? s)
        {
            if (_sa == null)
                return default(T);

            Byte[]? baOut;
            _ConvertToBaseXBytesArray(ref s, out baOut);

            Byte[]? baIV;
            _SplitBytes(ref baOut, ref _iIVSizeInBytes, out baIV, out baOut);

            if (baIV == null)
                return default(T);

            ICryptoTransform ct;

            lock (_sa)
            {
                try
                {
                    _sa.IV = baIV;
                    ct = _sa.CreateDecryptor();
                }
                catch
                {
                    return default(T);
                }
            }
                
            Byte[]? baIn;
            _Transform(ref baOut, ref ct, out baIn);
            if (baIn == null)
                return default(T);

            Int32 iSALTLength;
            _FindSALTLength(ref baIn, out iSALTLength);
            Byte[]? baSALT;
            _SplitBytes(ref baIn, ref iSALTLength, out baSALT, out baIn);
            _ParseToString(ref baIn, out s);

            return
                _eis == EInternalSerialization.JSON
                    ? JSONUtils.Deserialize<T>(s)
                    : ObjectUtils.Parse<T>(s);
        }

        private void _Transform(ref Byte[]? baIn, ref ICryptoTransform ct, out Byte[]? baOut)
        {
            if (baIn != null)
                try { baOut = ct.TransformFinalBlock(baIn, 0, baIn.Length); return; } catch { }

            baOut = null;
        }

        public override void Dispose()
        {
            if (_sa == null) return;
            try { _sa.Dispose(); } catch { }
        }
    }
}

