using System;
using Kudos.Serving.KaronteModule.Contexts;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using Kudos.Serving.KaronteModule.Contexts.Databasing;

namespace Kudos.Serving.KaronteModule.Middlewares
{
    public abstract class AKarontePoolizedDatabasingMiddleware
        : AContexizedKaronteMiddleware<KarontePoolizedDatabasingContext>
    {
        protected AKarontePoolizedDatabasingMiddleware(ref RequestDelegate rd) : base(ref rd) { }

        protected override async Task<KarontePoolizedDatabasingContext?> OnContextFetch(KaronteContext kc)
        {
            return kc.PoolizedDatabasingContext;
        }

        protected override async Task OnBounceEnd(KaronteContext kc) { }
    }
}

