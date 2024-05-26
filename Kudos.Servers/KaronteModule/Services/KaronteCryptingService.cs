using System;
using Kudos.Crypters.KryptoModule.HashModule;
using Kudos.Crypters.KryptoModule.HashModule.Builders;
using Kudos.Crypters.KryptoModule.SymmetricModule;
using Kudos.Crypters.KryptoModule.SymmetricModule.Builders;
using Kudos.Types;
using Microsoft.Extensions.DependencyInjection;

namespace Kudos.Servers.KaronteModule.Services
{
	public class
		KaronteCryptingService
	:
		AKaronteService
	{
		internal readonly Metas Symmetrics, Hashes;

		internal KaronteCryptingService(ref IServiceCollection sc) : base(ref sc)
		{
			Symmetrics = new Metas(StringComparison.OrdinalIgnoreCase);
			Hashes = new Metas(StringComparison.OrdinalIgnoreCase);
		}

		public KaronteCryptingService RegisterSymmetric(string? sn, Action<SymmetricBuilder>? act)
		{
			if(sn != null && act != null)
			{
				SymmetricBuilder sb = Symmetric.RequestBuilder();
				act.Invoke(sb);
				Symmetrics.Set(sn, sb.Build());
            }

			return this;
		}

		public KaronteCryptingService RegisterHash(string? sn, Action<HashBuilder>? act)
		{
            if (sn != null && act != null)
            {
                HashBuilder sb = Hash.RequestBuilder();
                act.Invoke(sb);
                Hashes.Set(sn, sb.Build());
            }
            return this;
		}
    }
}