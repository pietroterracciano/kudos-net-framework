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

        public static Byte[]? GenerateKey(ESymmetricSize ess)
        {
            if (__sa == null)
                return null;

            Int32? i;
            SymmetricSizeUtils.GetInBits(ref ess, out i);

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

        public static Byte[]? GenerateIV(ESymmetricSize ess)
        {
            if (__sa == null)
                return null;

            Int32? i;
            SymmetricSizeUtils.GetInBits(ref ess, out i);

            if (i == null)
                return null;

            lock (__sa)
            {
                try
                {
                    __sa.BlockSize = i.Value;
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

        #endregion

        private readonly SymmetricAlgorithm? _sa;

        internal Symmetric(ref SymmetricDescriptor sd) : base(ref sd)
        {
            if (sd.KeySize == null)
                return;

            ESymmetricSize eKeySize = sd.KeySize.Value;
            Int32? iKeySize;
            SymmetricSizeUtils.GetInBits(ref eKeySize, out iKeySize);

            if (iKeySize == null)
                return;

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

            Int32
                iKeySizeInBytes = iKeySize.Value / 8;

            Byte[]
                baPaddedKey;

            if(baKey.Length != iKeySizeInBytes)
            {
                baPaddedKey = new Byte[iKeySizeInBytes];

                Array.Copy(
                    baKey,
                    0,
                    baPaddedKey,
                    0,
                    baKey.Length > iKeySizeInBytes
                        ? iKeySizeInBytes
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
            return null;
        }

        public T? Decrypt<T>(String? s)
        {
            return default(T);
        }

        public override void Dispose()
        {
            if (_sa == null) return;
            try { _sa.Dispose(); } catch { }
        }
    }
}

