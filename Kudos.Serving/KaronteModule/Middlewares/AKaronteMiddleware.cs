using Kudos.Serving.KaronteModule.Attributes;
using Kudos.Serving.KaronteModule.Contexts;
using Kudos.Serving.KaronteModule.Enums;
using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;

namespace Kudos.Serving.KaronteModule.Middlewares
{
    [KaronteMiddleware]
    public abstract class AKaronteMiddleware
    {
        private readonly RequestDelegate _rd;

        protected AKaronteMiddleware(ref RequestDelegate rd)
        {
            _rd = rd;
        }

        public virtual async Task Invoke(HttpContext hc, KaronteContext kc)
        {
            EKaronteBounce ekb = await OnBounceStart(kc);

            if
            (
                ekb == EKaronteBounce.MoveForward
                && !hc.RequestAborted.IsCancellationRequested
            )
                await _rd.Invoke(hc);

            await OnBounceEnd(kc);
        }

        protected abstract Task<EKaronteBounce> OnBounceStart(KaronteContext kc);
        protected abstract Task OnBounceEnd(KaronteContext kc);
    }
}