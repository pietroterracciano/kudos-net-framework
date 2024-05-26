using Kudos.Constants;
using Kudos.Servers.KaronteModule.Attributes;
using Kudos.Servers.KaronteModule.Constants;
using Kudos.Servers.KaronteModule.Contexts;
using Kudos.Servers.KaronteModule.Descriptors.Tokens;
using Kudos.Servers.KaronteModule.Enums;
using Kudos.Servers.KaronteModule.Utils;
using Kudos.Utils;
using Kudos.Utils.Collections;
using Kudos.Utils.Texts;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using System;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Kudos.Servers.KaronteModule.Middlewares
{
    public abstract class
        AKaronteAuthorizatingMiddleware<AuthorizatingAttribute, EAuthorizationType>
    :
        AKaronteMiddleware
    where
        AuthorizatingAttribute : AKaronteAuthorizatingAttribute<EAuthorizationType>
    where
        EAuthorizationType : Enum
    {
        public AKaronteAuthorizatingMiddleware(ref RequestDelegate rd) : base(ref rd) { }

        protected override async Task<EKaronteBounce> OnBounce(KaronteContext kc)
        {
            KaronteAuthorizatingDescriptor? rd = null;
            StringValues sv;
            if (kc.HttpContext.Request.Headers.TryGetValue(CKaronteHttpHeader.Authorization, out sv))
                for (int i = 0; i < sv.Count; i++)
                {
                    if (!FetchRequestDescriptor(sv[i], out rd)) continue;
                    break;
                }

            KaronteAuthorizatingDescriptor? ed;
            FetchEndpointDescriptor(ref kc, out ed);

            kc.AuthorizatingContext = new KaronteAuthorizatingContext(ref rd, ref ed, ref kc);
            return await OnReceiveContext(kc.AuthorizatingContext);
        }

        protected override async Task OnBounceReturn(KaronteContext cc) { }

        protected abstract Task<EKaronteBounce> OnReceiveContext(KaronteAuthorizatingContext kac);

        private static Boolean FetchRequestDescriptor(String? s, out KaronteAuthorizatingDescriptor? kad)
        {
            if (s == null)
            {
                kad = null;
                return false;
            }

            String[]
                sa = CRegex.Spaces1toN.Split(s);

            if
            (
                !ArrayUtils.IsValidIndex(sa, 1)
                || String.IsNullOrWhiteSpace(sa[1])
            )
            {
                kad = null;
                return false;
            }

            EAuthorizationType?
                eat = EnumUtils.Parse<EAuthorizationType>(sa[0]);

            if (!EnumUtils.IsValid<EAuthorizationType>(eat))
            {
                kad = null;
                return false;
            }

            kad = new KaronteAuthorizatingDescriptor(sa[1], eat);
            return true;
        }

        private static void FetchEndpointDescriptor(ref KaronteContext kc, out KaronteAuthorizatingDescriptor? kad)
        {
            AuthorizatingAttribute?
                aa = EndpointUtils.GetLastMetadata<AuthorizatingAttribute>
                    (
                        kc.RoutingContext != null
                            ? kc.RoutingContext.Endpoint
                            : kc.HttpContext.GetEndpoint()
                    );

            kad = aa != null
                ? new KaronteAuthorizatingDescriptor(null, aa.Value)
                : null;
        }
    }
}