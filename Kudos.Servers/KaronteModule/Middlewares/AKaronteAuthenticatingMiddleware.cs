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
        public AKaronteAuthenticatingMiddleware(ref RequestDelegate rd) : base(ref rd) {}

        protected override async Task<KaronteAuthenticatingContext?> OnContextFetch(KaronteContext kc)
        {
            kc.AuthenticatingContext.AuthenticationData = await OnAuthenticationDataFetch(kc);
            return kc.AuthenticatingContext;
        }

        protected abstract Task<Object?> OnAuthenticationDataFetch(KaronteContext kc);

        protected override async Task OnBounceEnd(KaronteContext kc) { }
    }
}