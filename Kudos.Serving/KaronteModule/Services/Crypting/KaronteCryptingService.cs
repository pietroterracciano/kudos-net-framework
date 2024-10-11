using System;
using Kudos.Crypters.KryptoModule.HashModule;
using Kudos.Crypters.KryptoModule.HashModule.Builders;
using Kudos.Crypters.KryptoModule.SymmetricModule;
using Kudos.Crypters.KryptoModule.SymmetricModule.Builders;
using Kudos.Types;
using Microsoft.Extensions.DependencyInjection;

namespace Kudos.Serving.KaronteModule.Services.Crypting
{
    public sealed class
		KaronteCryptingService
	:
		AKaronteService
	{
		public readonly KaronteCryptingSymmetricingService
			SymmetricingService;

        public readonly KaronteCryptingHashingService
			HashingService;

		internal KaronteCryptingService(ref IServiceCollection sc) : base(ref sc)
		{
            SymmetricingService = new KaronteCryptingSymmetricingService(ref sc);
            HashingService = new KaronteCryptingHashingService(ref sc);
		}
    }
}