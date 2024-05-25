using System;
using Kudos.Crypters.KryptoModule.HashModule;
using Kudos.Crypters.KryptoModule.SymmetricModule;
using Microsoft.Extensions.DependencyInjection;

namespace Kudos.Servers.KaronteModule.Interfaces
{
	public interface
		IKaronteCryptingServiceCollection
	:
		IKaronteServiceCollection
	{
		IKaronteCryptingServiceCollection RegisterSymmetric(String? sn, Func<Symmetric>? fnc);
        IKaronteCryptingServiceCollection RegisterHash(String? sn, Func<Hash>? fnc);
    }
}

