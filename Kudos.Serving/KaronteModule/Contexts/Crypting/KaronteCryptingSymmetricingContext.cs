using System;
using Kudos.Crypters.KryptoModule.SymmetricModule;
using Kudos.Serving.KaronteModule.Services.Crypting;

namespace Kudos.Serving.KaronteModule.Contexts.Crypting
{
    public sealed class KaronteCryptingSymmetricingContext
        : AKaronteCryptingChildContext
    {
        private readonly KaronteCryptingSymmetricingService _kcss;

        internal KaronteCryptingSymmetricingContext
            (
                ref KaronteCryptingSymmetricingService kcss,
                ref KaronteCryptingContext kcc
            )
        :
            base
            (
                ref kcc
            )
        {
            _kcss = kcss;
        }

        public Symmetric? Get(String? sn) { return _kcss.Get(sn); }
        public Symmetric Require(String? sn) { return _kcss.Require(sn); }
    }
}