using System;
using Kudos.Servers.KaronteModule.Contexts;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using Kudos.Servers.KaronteModule.Enums;
using Kudos.Servers.KaronteModule.Constants;
using Kudos.Servers.KaronteModule.Attributes;
using Kudos.Servers.KaronteModule.Descriptors.Routes;
using Kudos.Constants;
using Kudos.Servers.KaronteModule.Descriptors.Authorizatings;
using Kudos.Utils;
using Kudos.Utils.Collections;
using static Google.Protobuf.Reflection.FeatureSet.Types;
using Org.BouncyCastle.Pqc.Crypto.Lms;
using System.Collections.Generic;

namespace Kudos.Servers.KaronteModule.Middlewares
{
    public abstract class AKaronteCapabilitingMiddleware
        : AContexizedKaronteMiddleware<KaronteCapabilitingContext>
    {
        public AKaronteCapabilitingMiddleware(ref RequestDelegate rd) : base(ref rd) { }

        protected override async Task<KaronteCapabilitingContext?> OnContextCreate(KaronteContext kc)
        {
            KaronteAttributingContext? kac;
            kc.RequestObject<KaronteAttributingContext>(CKaronteKey.Attributing, out kac);

            KaronteCapabilityAttribute? kca = kac.GetAttribute<KaronteCapabilityAttribute>();

            String[]? sa;
            EKaronteCapabilityValidationRule? ekcvr;

            if (kca == null)
            {
                ekcvr = null;
                sa = null;
            }
            else
            {
                ekcvr = kca.ValidationRule;

                if (!kca.HasValues)
                {
                    KaronteMethodRouteDescriptor? kmrd =
                        kc.RoutingContext != null
                            ? kc.RoutingContext.MethodRouteDescriptor
                            : null;

                    if (kmrd == null)
                    {
                        Endpoint? end = kc.HttpContext.GetEndpoint();
                        KaronteMethodRouteDescriptor.Request(ref end, out kmrd);
                    }

                    sa =
                        kmrd != null
                        ?
                            new string[]
                            {
                            kmrd.ResolvedFullPattern,
                            kmrd.FullHashKey
                            }
                        :
                            null;

                }
                else
                    sa = kca.Values;
            }

            return kc.CapabilitingContext = new KaronteCapabilitingContext(ref ekcvr, ref sa, ref kc);
        }

        protected override async Task OnBounceEnd(KaronteContext kc) { }
    }
}