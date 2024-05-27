using System;
using Kudos.Constants;
using Kudos.Servers.KaronteModule.Attributes;
using Kudos.Servers.KaronteModule.Constants;
using Kudos.Servers.KaronteModule.Contexts;
using Kudos.Servers.KaronteModule.Descriptors.Tokens;
using Kudos.Servers.KaronteModule.Enums;
using Kudos.Servers.KaronteModule.Utils;
using Kudos.Utils;
using Kudos.Utils.Collections;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using System.Threading.Tasks;

namespace Kudos.Servers.KaronteModule.Middlewares
{
    public abstract class
        AKaronteHeadingMiddleware
    :
        AKaronteMiddleware
    {
        public AKaronteHeadingMiddleware(ref RequestDelegate rd) : base(ref rd) { }

        protected override async Task<EKaronteBounce> OnBounce(KaronteContext kc)
        {
            KaronteHeadingContext khc;
            kc.RequestObject<KaronteHeadingContext>(CKaronteKey.Heading, out khc);
            return await OnReceiveContext(khc);
        }

        protected override async Task OnBounceReturn(KaronteContext cc) { }

        protected abstract Task<EKaronteBounce> OnReceiveContext(KaronteHeadingContext khc);
    }
}