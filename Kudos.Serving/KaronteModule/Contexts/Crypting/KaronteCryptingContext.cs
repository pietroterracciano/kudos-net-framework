using System;
using Kudos.Crypters.KryptoModule.HashModule;
using Kudos.Crypters.KryptoModule.SymmetricModule;
using Kudos.Serving.KaronteModule.Services.Crypting;

namespace Kudos.Serving.KaronteModule.Contexts.Crypting
{
	public sealed class KaronteCryptingContext
        : AKaronteChildContext
	{
		private readonly KaronteCryptingService _kcs;

        public readonly KaronteCryptingHashingContext HashingContext;
        public readonly KaronteCryptingSymmetricingContext SymmetricingContext;

		internal KaronteCryptingContext
            (
                ref KaronteCryptingService kcs,
                ref KaronteContext kc
            )
        :
            base
            (
                ref kc
            )
		{
            _kcs = kcs;
            KaronteCryptingContext kcc = this;
            KaronteCryptingHashingService kchs = kcs.HashingService;
            HashingContext = new KaronteCryptingHashingContext(ref kchs, ref kcc);
            KaronteCryptingSymmetricingService kcss = kcs.SymmetricingService;
            SymmetricingContext = new KaronteCryptingSymmetricingContext(ref kcss, ref kcc);
        }
    }
}