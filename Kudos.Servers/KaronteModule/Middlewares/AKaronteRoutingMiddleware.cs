using Kudos.Databases.Chainers;
using Kudos.Databases.Interfaces.Chains;
using Kudos.Databases.Interfaces;
using Kudos.Servers.KaronteModule.Contexts;
using Kudos.Servers.KaronteModule.Enums;
using Kudos.Servers.KaronteModule.Utils;
using Microsoft.AspNetCore.Http;
using System.Net;
using System.Threading.Tasks;
using System;
using Kudos.Constants;
using Kudos.Servers.KaronteModule.Constants;
using Microsoft.AspNetCore.Http.Metadata;
using Kudos.Servers.KaronteModule.Descriptors.Routes;

namespace Kudos.Servers.KaronteModule.Middlewares
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