using Kudos.Constants;
using Kudos.Databasing.Chainers;
using Kudos.Databasing.Interfaces;
using Kudos.Databasing.Interfaces.Chains;
using Kudos.Databasing.Results;
using Kudos.Serving.KaronteModule.Constants;
using Kudos.Serving.KaronteModule.Contexts;
using Kudos.Serving.KaronteModule.Contexts.Databasing;
using Kudos.Serving.KaronteModule.Enums;
using Kudos.Serving.KaronteModule.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Primitives;
using System;
using System.Net;
using System.Threading.Tasks;

namespace Kudos.Serving.KaronteModule.Middlewares
{
    public abstract class AKaronteDatabasingMiddleware
        : AContexizedKaronteMiddleware<KaronteDatabasingContext>
    {
        protected AKaronteDatabasingMiddleware(ref RequestDelegate rd) : base(ref rd) { }

        protected override async Task<KaronteDatabasingContext> OnContextFetch(KaronteContext kc)
        {
            return kc.DatabasingContext;
        }
    }
}