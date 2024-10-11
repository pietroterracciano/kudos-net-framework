using System;
using Kudos.Serving.KaronteModule.Contexts;
using Kudos.Serving.KaronteModule.Enums;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using Kudos.Serving.KaronteModule.Attributes;

namespace Kudos.Serving.KaronteModule.Middlewares
{
    internal sealed class KaronteAuthorizatingMiddleware : AKaronteAuthorizatingMiddleware<KaronteAuthorizatingAttribute, EKaronteAuthorizationType>
    {
        public KaronteAuthorizatingMiddleware(RequestDelegate rd) : base(ref rd) { }

        protected override async Task<EKaronteBounce> OnReceiveContext(KaronteAuthorizatingContext kac)
        {
            return kac.IsEndpointAuthorized
                ? EKaronteBounce.MoveForward
                : EKaronteBounce.MoveBackward;
        }
    }
}

