using System;
using Kudos.Serving.KaronteModule.Contexts;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace Kudos.Serving.KaronteModule.Middlewares
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