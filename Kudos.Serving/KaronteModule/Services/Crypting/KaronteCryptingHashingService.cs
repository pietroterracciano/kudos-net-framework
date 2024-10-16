using System;
using Kudos.Crypters.KryptoModule.HashModule;
using Kudos.Crypters.KryptoModule.HashModule.Builders;
using Microsoft.Extensions.DependencyInjection;

namespace Kudos.Serving.KaronteModule.Services.Crypting
{
    public sealed class KaronteCryptingHashingService
        : AKaronteMetizedService
    {
        internal KaronteCryptingHashingService(ref IServiceCollection sc) : base(ref sc) { }

        public KaronteCryptingHashingService Register(String? sn, Action<HashBuilder>? act)
        {
            if (act == null) return this;
            HashBuilder hb = Hash.RequestBuilder();
            act.Invoke(hb);
            Hash hsh = hb.Build();
            _RegisterMeta(ref sn, ref hsh);
            return this;
        }

        internal Hash Require(String? sn)
        {
            Hash hsh;
            _RequireMeta(ref sn, out hsh);
            return hsh;
        }

        internal Hash? Get(String? sn)
        {
            Hash? hsh;
            _GetMeta(ref sn, out hsh);
            return hsh;
        }
    }
}

