using System;
using Kudos.Serving.KaronteModule.Contexts;
using Kudos.Serving.KaronteModule.Enums;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace Kudos.Serving.KaronteModule.Middlewares
{
    public abstract class AKaronteAbortingMiddleware
    : AKaronteMiddleware
    {
        protected AKaronteAbortingMiddleware(ref RequestDelegate rd) : base(ref rd) { }

        protected override async Task<EKaronteBounce> OnBounceStart(KaronteContext kc)
        {
            await OnAbortingHandle(kc);
            return EKaronteBounce.MoveBackward;
        }

        protected override async Task OnBounceEnd(KaronteContext kc) { }

        protected abstract Task OnAbortingHandle(KaronteContext kc);
    }
}

