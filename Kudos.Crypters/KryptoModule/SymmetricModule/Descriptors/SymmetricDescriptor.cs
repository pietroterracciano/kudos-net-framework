using System;
using System.Security.Cryptography;
using Kudos.Crypters.KryptoModule.Descriptors;
using Kudos.Crypters.KryptoModule.SymmetricModule.Enums;

namespace Kudos.Crypters.KryptoModule.SymmetricModule.Descriptors
{
    public sealed class
        SymmetricDescriptor
    :
        AKryptoDescriptor<SymmetricDescriptor>
    {
        internal ESymmetricSize? KeySize, BlockSize;
        internal ESymmetricAlgorithm? Algorithm;
        internal CipherMode? CipherMode;
        internal PaddingMode? PaddingMode;
        internal String? SKey;
        internal Byte[]? BAKey;

        internal override void OnInject(ref SymmetricDescriptor dsc)
        {
            KeySize = dsc.KeySize;
            BlockSize = dsc.BlockSize;
            Algorithm = dsc.Algorithm;
            CipherMode = dsc.CipherMode;
            PaddingMode = dsc.PaddingMode;
            SKey = dsc.SKey;
            BAKey = dsc.BAKey;
        }
    }
}

