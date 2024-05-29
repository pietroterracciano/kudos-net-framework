using System;
using Kudos.Servers.KaronteModule.Contexts;
using Kudos.Servers.KaronteModule.Enums;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Kudos.Servers.KaronteModule.Middlewares
{
    public abstract class AContexizedKaronteMiddleware<ContextType>
        : AKaronteMiddleware
    {
        protected AContexizedKaronteMiddleware(ref RequestDelegate rd) : base(ref rd) { }

        protected override async Task<EKaronteBounce> OnBounceStart(KaronteContext kc)
        {
            ContextType? ct = await OnContextCreate(kc);
            return ct != null
                ? await OnContextReceive(ct)
                : EKaronteBounce.MoveBackward;
        }

        protected abstract Task<ContextType?> OnContextCreate(KaronteContext kc);
        protected abstract Task<EKaronteBounce> OnContextReceive(ContextType ct);
    }
}