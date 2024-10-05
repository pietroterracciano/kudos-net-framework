using System;
using Kudos.Servers.KaronteModule.Contexts;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using Kudos.Servers.KaronteModule.Enums;

namespace Kudos.Servers.KaronteModule.Middlewares
{
    public abstract class AKaronteExceptioningMiddleware
        : AKaronteMiddleware
    {
        protected AKaronteExceptioningMiddleware(ref RequestDelegate rd) : base(ref rd) { }

        protected override async Task<EKaronteBounce> OnBounceStart(KaronteContext kc)
        {
            await OnExceptionHandle(kc);
            return EKaronteBounce.MoveBackward;
        }

        protected override async Task OnBounceEnd(KaronteContext kc) { }

        protected abstract Task OnExceptionHandle(KaronteContext kc);
    }
}