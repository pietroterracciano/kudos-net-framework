using Kudos.Servers.KaronteModule.Contexts;
using Kudos.Servers.KaronteModule.Enums;
using Microsoft.AspNetCore.Http;
using Org.BouncyCastle.Crypto.Macs;
using System;
using System.Threading.Tasks;

namespace Kudos.Servers.KaronteModule.Middlewares
{
    public abstract class AKaronteAuthenticatingMiddleware
        : AContexizedKaronteMiddleware<KaronteAuthenticatingContext>
    {
        protected AKaronteAuthenticatingMiddleware(ref RequestDelegate rd) : base(ref rd) {}

        protected override async Task<KaronteAuthenticatingContext?> OnContextFetch(KaronteContext kc)
        {
            return kc.AuthenticatingContext;
        }

        protected override async Task OnBounceEnd(KaronteContext kc) { }
    }
}