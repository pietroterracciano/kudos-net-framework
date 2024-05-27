using System;
using Kudos.Servers.KaronteModule.Contexts;
using Kudos.Servers.KaronteModule.Enums;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using Kudos.Servers.KaronteModule.Constants;

namespace Kudos.Servers.KaronteModule.Middlewares
{
    public abstract class
        AKaronteAttributingMiddleware<AttributeType>
    :
        AKaronteMiddleware
    {
        public AKaronteAttributingMiddleware(ref RequestDelegate rd) : base(ref rd) { }

        protected override async Task<EKaronteBounce> OnBounce(KaronteContext kc)
        {
            KaronteAttributingContext kac;
            kc.RequestObject<KaronteAttributingContext>(CKaronteKey.Attributing, out kac);
            return await OnReceiveContext(kc.RequestService<KaronteAttributingContext>());
        }

        protected override async Task OnBounceReturn(KaronteContext cc) { }

        protected abstract Task<EKaronteBounce> OnReceiveContext(KaronteAttributingContext kac);
    }
}

