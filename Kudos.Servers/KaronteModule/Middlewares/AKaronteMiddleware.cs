using Kudos.Servers.KaronteModule.Attributes;
using Kudos.Servers.KaronteModule.Contexts;
using Kudos.Servers.KaronteModule.Enums;
using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;

namespace Kudos.Servers.KaronteModule.Middlewares
{
    [KaronteMiddleware]
    public abstract class AKaronteMiddleware
    {
        private readonly RequestDelegate _rd;

        public AKaronteMiddleware(ref RequestDelegate rd)
        {
            _rd = rd;
        }

        public virtual async Task Invoke(HttpContext hc, KaronteContext kc)
        {
            EKaronteBounce ekb = await OnBounceStart(kc);

            if (ekb == EKaronteBounce.MoveForward)
                await _rd.Invoke(hc);

            await OnBounceEnd(kc);
        }

        protected abstract Task<EKaronteBounce> OnBounceStart(KaronteContext kc);
        protected abstract Task OnBounceEnd(KaronteContext kc);
    }
}