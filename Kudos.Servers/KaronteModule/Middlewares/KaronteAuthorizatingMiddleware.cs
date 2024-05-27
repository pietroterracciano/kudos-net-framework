using System;
using Kudos.Servers.KaronteModule.Contexts;
using Kudos.Servers.KaronteModule.Enums;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using Kudos.Servers.KaronteModule.Attributes;

namespace Kudos.Servers.KaronteModule.Middlewares
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

