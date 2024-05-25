using Kudos.Servers.KaronteModule.Attributes;
using Kudos.Servers.KaronteModule.Constants;
using Kudos.Servers.KaronteModule.Contexts;
using Kudos.Servers.KaronteModule.Enums;
using Kudos.Servers.KaronteModule.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using System;
using System.Threading.Tasks;

namespace Kudos.Servers.KaronteModule.Middlewares
{
    public abstract class AKaronteAuthorizatingMiddleware : AKaronteMiddleware
    {
        public AKaronteAuthorizatingMiddleware(ref RequestDelegate rd) : base(ref rd) { }

        protected override async Task<EKaronteBounce> OnBounce(KaronteContext kc)
        {
            kc.AuthorizatingContext = new KaronteAuthorizatingContext(ref kc);

            StringValues sv;

            String? st = null;
            EKaronteAuthorization eka = EKaronteAuthorization.None;

            if (kc.HttpContext.Request.Headers.TryGetValue(CKaronteHttpHeader.Authorization, out sv))
                for (int i = 0; i < sv.Count; i++)
                {
                    if (!FetchToken(sv[i], out st, out eka)) continue;
                    break;
                }

            kc.AuthorizatingContext.RequestToken = st;
            kc.AuthorizatingContext.RequestAuthorizationType = eka;

            KaronteAuthorizatingAttribute? 
                kaa = EndpointUtils.GetLastMetadata<KaronteAuthorizatingAttribute>
                    (
                        kc.RoutingContext != null 
                            ? kc.RoutingContext.Endpoint 
                            : kc.HttpContext.GetEndpoint()
                    );

            kc.AuthorizatingContext.EndpointAuthorizationType = kaa != null ? kaa.Value : EKaronteAuthorization.None;

            return await OnEndpointAuthorization(kc.AuthorizatingContext);
        }

        protected override async Task OnBounceReturn(KaronteContext cc) { }

        protected abstract Task<EKaronteBounce> OnEndpointAuthorization(KaronteAuthorizatingContext kac);

        private static Boolean FetchToken(String? s, out String? st, out EKaronteAuthorization eka)
        {
            if (s == null)
            {
                st = null;
                eka = EKaronteAuthorization.None;
                return false;
            }
            
            st = s.Trim();

            if (Normalize(ref st, CKaronteAuthorization.Bearer))
                eka = EKaronteAuthorization.Bearer;
            else if (Normalize(ref st, CKaronteAuthorization.Access))
                eka = EKaronteAuthorization.Access;
            else
                eka = EKaronteAuthorization.None;

            return true;
        }

        private static Boolean Normalize(ref String s, String s1)
        {
            if (!s.StartsWith(s1, StringComparison.OrdinalIgnoreCase)) return false;
            s = s.Substring(s1.Length).Trim(); return true;
            //while (s.StartsWith(CCharacter.Space)) s = s.Substring(1); return true;
        }
    }
}