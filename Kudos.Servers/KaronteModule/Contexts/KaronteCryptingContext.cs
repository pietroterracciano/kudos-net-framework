using System;
using Kudos.Crypters.KryptoModule.HashModule;
using Kudos.Crypters.KryptoModule.SymmetricModule;
using Kudos.Servers.KaronteModule.Services;

namespace Kudos.Servers.KaronteModule.Contexts
{
	public sealed class KaronteCryptingContext
        : AKaronteChildContext
	{
		private readonly KaronteCryptingService _kcs;

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
        }

		public Symmetric? GetSymmetric(String? sn) { return _kcs.Symmetrics.Get<Symmetric>(sn); }
        public Hash? GetHash(String? sn) { return _kcs.Hashes.Get<Hash>(sn); }

        public Symmetric RequireSymmetric(String? sn)
        {
            Symmetric? smm = GetSymmetric(sn);
            if (smm == null) throw new InvalidOperationException();
            return smm;
        }
        public Hash RequireHash(String? sn)
        {
            Hash? hsh = GetHash(sn);
            if (hsh == null) throw new InvalidOperationException();
            return hsh;
        }
    }
}