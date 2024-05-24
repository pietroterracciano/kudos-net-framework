using System;
using System.Security.Cryptography;
using Kudos.Crypters.KryptoModule.Builders;
using Kudos.Crypters.KryptoModule.HashModule.Builders;
using Kudos.Crypters.KryptoModule.HashModule.Enums;
using Kudos.Crypters.KryptoModule.SymmetricModule.Descriptors;
using Kudos.Crypters.KryptoModule.SymmetricModule.Enums;

namespace Kudos.Crypters.KryptoModule.SymmetricModule.Builders
{
    public sealed class
        SymmetricBuilder
    :
        AKryptoBuilder
        <
            SymmetricBuilder,
            Symmetric,
            SymmetricDescriptor
        >
    {
        private readonly SymmetricDescriptor _sd;
        internal SymmetricBuilder(ref SymmetricDescriptor sd) : base(ref sd) { _sd = sd; }

        public SymmetricBuilder SetBlockSize(ESymmetricSize ess)
        {
            _sd.BlockSize = ess;
            return this;
        }

        public SymmetricBuilder SetKeySize(ESymmetricSize ess)
        {
            _sd.KeySize = ess;
            return this;
        }

        public SymmetricBuilder SetAlgorithm(ESymmetricAlgorithm esa)
        {
            _sd.Algorithm = esa;
            return this;
        }

        public SymmetricBuilder SetPaddingMode(PaddingMode epm)
        {
            _sd.PaddingMode = epm;
            return this;
        }

        public SymmetricBuilder SetKey(String s)
        {
            _sd.SKey = s;
            _sd.BAKey = null;
            return this;
        }

        public SymmetricBuilder SetKey(Byte[] ba)
        {
            _sd.SKey = null;
            _sd.BAKey = ba;
            return this;
        }

        protected override void OnBuild(ref SymmetricDescriptor sd, out Symmetric smm)
        {
            smm = new Symmetric(ref sd);
        }

    }
}