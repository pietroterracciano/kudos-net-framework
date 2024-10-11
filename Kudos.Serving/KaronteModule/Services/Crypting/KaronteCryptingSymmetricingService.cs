using System;
using Kudos.Crypters.KryptoModule.SymmetricModule;
using Kudos.Crypters.KryptoModule.SymmetricModule.Builders;
using Microsoft.Extensions.DependencyInjection;

namespace Kudos.Serving.KaronteModule.Services.Crypting
{
    public sealed class KaronteCryptingSymmetricingService
        : AKaronteMetizedService
    {
        internal KaronteCryptingSymmetricingService(ref IServiceCollection sc) : base(ref sc) { }

        public KaronteCryptingSymmetricingService Register(String? sn, Action<SymmetricBuilder>? act)
        {
            if (act == null) return this;
            SymmetricBuilder sb = Symmetric.RequestBuilder();
            act.Invoke(sb);
            Symmetric smm = sb.Build();
            _RegisterMeta(ref sn, ref smm);
            return this;
        }

        internal Symmetric? Require(String? sn)
        {
            Symmetric? smm;
            _RequireMeta<Symmetric>(ref sn, out smm);
            return smm;
        }

        internal Symmetric? Get(String? sn)
        {
            Symmetric? smm;
            _GetMeta<Symmetric>(ref sn, out smm);
            return smm;
        }
    }
}

