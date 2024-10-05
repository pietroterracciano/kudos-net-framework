using System;
using Kudos.Servers.KaronteModule.Contexts;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace Kudos.Servers.KaronteModule.Middlewares
{
    public abstract class AKaronteCapabilitingMiddleware
        : AContexizedKaronteMiddleware<KaronteCapabilitingContext>
    {
        protected AKaronteCapabilitingMiddleware(ref RequestDelegate rd) : base(ref rd) { }

        protected override async Task<KaronteCapabilitingContext?> OnContextFetch(KaronteContext kc)
        {
            return kc.CapabilitingContext;
        }

        protected override async Task OnBounceEnd(KaronteContext kc) { }
    }
}