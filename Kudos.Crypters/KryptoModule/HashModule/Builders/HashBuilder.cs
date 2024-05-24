using Kudos.Crypters.KryptoModule.Builders;
using Kudos.Crypters.KryptoModule.HashModule.Descriptors;
using Kudos.Crypters.KryptoModule.HashModule.Enums;

namespace Kudos.Crypters.KryptoModule.HashModule.Builders
{
	public sealed class
        HashBuilder
	:
        AKryptoBuilder
        <
            HashBuilder,
            Hash,
            HashDescriptor
        >
	{
        private readonly HashDescriptor _hd;
        internal HashBuilder(ref HashDescriptor hd) : base(ref hd) { _hd = hd; }

        public HashBuilder SetAlgorithm(EHashAlgorithm eha)
		{
            _hd.Algorithm = eha;
			return this;
        }

        protected override void OnBuild(ref HashDescriptor kd, out Hash h)
        {
            h = new Hash(ref kd);
        }

    }
}