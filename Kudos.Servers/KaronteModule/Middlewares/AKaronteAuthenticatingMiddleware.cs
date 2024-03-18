using Kudos.Databases.Interfaces;
using Kudos.Servers.KaronteModule.Attributes;
using Kudos.Servers.KaronteModule.Constants;
using Kudos.Servers.KaronteModule.Contexts;
using Kudos.Servers.KaronteModule.Enums;
using Kudos.Servers.Utils;
using Kudos.Utils;
using Microsoft.AspNetCore.Http;
using System;
using System.Net;
using System.Threading.Tasks;

namespace Kudos.Servers.KaronteModule.Middlewares
{
    public abstract class AKaronteAuthenticatingMiddleware<ObjectType> : AKaronteMiddleware
    {
        public AKaronteAuthenticatingMiddleware(ref RequestDelegate rd) : base(ref rd) {}

        protected override async Task<EKaronteBounce> OnBounce(KaronteContext kc)
        {
            kc.AuthenticatingContext = new KaronteAuthenticatingContext(ref kc);

            KaronteAuthenticatingAttribute?
                kaa = EndpointUtils.GetLastMetadata<KaronteAuthenticatingAttribute>(kc.HttpContext.GetEndpoint());

            kc.AuthenticatingContext.EndpointHasAuthentication = kaa != null;

            if (!kc.AuthenticatingContext.EndpointHasAuthentication)
                return EKaronteBounce.MoveForward;
            //else if (await OnEndpointAuthentication(kc.AuthenticatingContext) == EKaronteBounce.MoveBackward)
            //    return EKaronteBounce.MoveBackward;

            ObjectType? o = await OnAuthenticationDataFetch(kc.AuthenticatingContext);

            kc.AuthenticatingContext.AuthenticationData = o;
            
            return await OnAuthenticationDataValidation(kc.AuthenticatingContext, o);
        }

        protected override async Task OnBounceReturn(KaronteContext kc) {}

        //protected abstract Task<EKaronteBounce> OnEndpointAuthentication(KaronteAuthenticatingContext kac);
        protected abstract Task<ObjectType?> OnAuthenticationDataFetch(KaronteAuthenticatingContext kac);
        protected abstract Task<EKaronteBounce> OnAuthenticationDataValidation(KaronteAuthenticatingContext kac, ObjectType? o);
    }
}
