using System;
using System.Security.Cryptography;
using Kudos.Crypters.KryptoModule.Descriptors;
using Kudos.Crypters.KryptoModule.HashModule.Enums;
using Kudos.Crypters.KryptoModule.SymmetricModule.Enums;
using Kudos.Crypters.KryptoModule.SymmetricModule.Utils;

namespace Kudos.Crypters.KryptoModule.SymmetricModule.Descriptors
{
    public sealed class
        SymmetricDescriptor
    :
        AKryptoDescriptor<SymmetricDescriptor>
    {
        internal ESymmetricKeySize? KeySize;
        internal ESymmetricAlgorithm? Algorithm;
        internal CipherMode? CipherMode;
        internal PaddingMode? PaddingMode;
        internal String? SKey;
        internal Byte[]? BAKey;

        protected override void OnInject(ref SymmetricDescriptor dsc)
        {
            KeySize = dsc.KeySize;
            Algorithm = dsc.Algorithm;
            CipherMode = dsc.CipherMode;
            PaddingMode = dsc.PaddingMode;
            SKey = dsc.SKey;
            BAKey = dsc.BAKey;
        }
    }
}

