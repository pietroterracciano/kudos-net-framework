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
        AContexizedKaronteMiddleware<KaronteAttributingContext>
    {
        public AKaronteAttributingMiddleware(ref RequestDelegate rd) : base(ref rd) { }

        protected override async Task<KaronteAttributingContext?> OnContextCreate(KaronteContext kc)
        {
            KaronteAttributingContext kac;
            kc.RequestObject<KaronteAttributingContext>(CKaronteKey.Attributing, out kac);
            return kac;
        }

        protected override async Task OnBounceEnd(KaronteContext kc) { }
    }
}