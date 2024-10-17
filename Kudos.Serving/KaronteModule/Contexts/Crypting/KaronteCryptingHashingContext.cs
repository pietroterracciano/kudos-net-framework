using System;
using Kudos.Crypters.KryptoModule.HashModule;
using Kudos.Serving.KaronteModule.Services.Crypting;

namespace Kudos.Serving.KaronteModule.Contexts.Crypting
{
	public sealed class KaronteCryptingHashingContext
        : AKaronteCryptingChildContext
    {
        private readonly KaronteCryptingHashingService _kchs;

        internal KaronteCryptingHashingContext
            (
                ref KaronteCryptingHashingService kchs,
                ref KaronteCryptingContext kcc
            )
        :
            base
            (
                ref kcc
            )
        {
            _kchs = kchs;
        }

        public Hash? Get(String? sn) { return _kchs.Get(sn); }
        public Hash Require(String? sn) { return _kchs.Require(sn); }
	}
}

