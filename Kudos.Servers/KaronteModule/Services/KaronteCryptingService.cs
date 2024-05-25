using System;
using Kudos.Crypters.KryptoModule.HashModule;
using Kudos.Crypters.KryptoModule.SymmetricModule;
using Kudos.Servers.KaronteModule.Interfaces;
using Kudos.Types;
using Microsoft.Extensions.DependencyInjection;

namespace Kudos.Servers.KaronteModule.Services
{
	internal class KaronteCryptingService
		: AKaronteService, IKaronteCryptingServiceCollection
	{
		private readonly Metas
			_mSymmetrics,
			_mHashes;

		internal KaronteCryptingService(ref IServiceCollection sc) : base(ref sc)
		{
			_mSymmetrics = new Metas(StringComparison.OrdinalIgnoreCase);
			_mHashes = new Metas(StringComparison.OrdinalIgnoreCase);
		}

		public IKaronteCryptingServiceCollection RegisterSymmetric(string? sn, Func<Symmetric>? fnc)
		{
			_mSymmetrics.Set(sn, fnc != null ? fnc.Invoke() : null);
			return this;
		}

		public IKaronteCryptingServiceCollection RegisterHash(string? sn, Func<Hash>? fnc)
		{
			_mHashes.Set(sn, fnc != null ? fnc.Invoke() : null);
			return this;
		}

		internal Symmetric? GetSymmetric(String? sn) { return _mSymmetrics.Get<Symmetric>(sn); }
        internal Hash? GetHash(String? sn) { return _mHashes.Get<Hash>(sn); }

    }
}
