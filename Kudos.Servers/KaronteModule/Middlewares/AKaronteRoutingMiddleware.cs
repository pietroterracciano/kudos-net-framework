using Kudos.Databases.Chainers;
using Kudos.Databases.Interfaces.Chains;
using Kudos.Databases.Interfaces;
using Kudos.Servers.KaronteModule.Contexts;
using Kudos.Servers.KaronteModule.Enums;
using Kudos.Servers.Utils;
using Microsoft.AspNetCore.Http;
using System.Net;
using System.Threading.Tasks;
using System;
using Kudos.Constants;
using Kudos.Servers.KaronteModule.Constants;

namespace Kudos.Servers.KaronteModule.Middlewares
{
    public abstract class AKaronteRoutingMiddleware : AKaronteMiddleware
    {
        public AKaronteRoutingMiddleware(ref RequestDelegate rd) : base(ref rd) { }

        protected override async Task<EKaronteBounce> OnBounce(KaronteContext kc)
        {
            Endpoint? end = kc.HttpContext.GetEndpoint();
            String? sdn = end?.DisplayName;

            EKaronteRoute ekr;

            if (sdn == null)
                ekr = EKaronteRoute.NotRegistered;
            else if (end.Metadata == null || end.Metadata.Count < 1)
                ekr = EKaronteRoute.NotSupported;
            else if (sdn.StartsWith("Fallback", StringComparison.OrdinalIgnoreCase))
                ekr = EKaronteRoute.OnFallback;
            else if (sdn.Equals("/", StringComparison.OrdinalIgnoreCase))
                ekr = EKaronteRoute.OnRoot;
            else
            {
                ekr = EKaronteRoute.Registered;
                kc.HttpContext.Response.StatusCode = CKaronteHttpStatusCode.MethodNotAllowed;
            }

            kc.RoutingContext = new KaronteRoutingContext(ref kc, ref end, ref ekr);
            return await OnRoute(kc.RoutingContext);
        }

        protected override async Task OnBounceReturn(KaronteContext kc)
        {
            //if (kc.RoutingContext.Type != EKaronteRoute.Registered)
            //    return;
            //else if (kc.ResponsingContext != null)
            //    kc.ResponsingContext.SetStatusCode(HttpStatusCode.MethodNotAllowed); 
            //else
            //    kc.HttpContext.Response.StatusCode = (int)HttpStatusCode.MethodNotAllowed;
        }

        protected abstract Task<EKaronteBounce> OnRoute(KaronteRoutingContext krc);
    }
}