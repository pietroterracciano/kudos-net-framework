using System;
using Kudos.Crypters.Hashes.Enums;
using Kudos.Crypters.Hashes.HashModule.Descriptors;
using Kudos.Crypters.Hashes.Interfaces;
using Kudos.Crypters.KryptoModule;
using Kudos.Crypters.KryptoModule.Builders;
using Kudos.Crypters.KryptoModule.Descriptors;

namespace Kudos.Crypters.Hashes.HashModule.Builders
{
	public class
        HashBuilder
	:
        AKryptoBuilder
        <
            HashBuilder,
            Hash,
            HashDescriptor
        >
	{
		private EHashAlgorithm _eha;

        public HashBuilder SetAlgorithm(EHashAlgorithm eha)
		{
			_eha = eha;
			return this;
        }

        protected override void OnBuild(ref HashDescriptor kd, out Hash h)
        {
            h = new Hash(ref kd);
        }
    }
}