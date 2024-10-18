using Kudos.Databasing.Chainers;
using Kudos.Databasing.Interfaces.Chains;
using Kudos.Databasing.Interfaces;
using Kudos.Serving.KaronteModule.Contexts;
using Kudos.Serving.KaronteModule.Enums;
using Kudos.Serving.KaronteModule.Utils;
using Microsoft.AspNetCore.Http;
using System.Net;
using System.Threading.Tasks;
using System;
using Kudos.Constants;
using Kudos.Serving.KaronteModule.Constants;
using Microsoft.AspNetCore.Http.Metadata;
using Kudos.Serving.KaronteModule.Descriptors.Routes;

namespace Kudos.Serving.KaronteModule.Middlewares
{
    public abstract class AKaronteRoutingMiddleware
        : AContexizedKaronteMiddleware<KaronteRoutingContext>
    {
        protected AKaronteRoutingMiddleware(ref RequestDelegate rd) : base(ref rd) { }

        protected override async Task<KaronteRoutingContext> OnContextFetch(KaronteContext kc)
        {
            return kc.RoutingContext;
        }

        protected override async Task OnBounceEnd(KaronteContext kc) { }
    }
}