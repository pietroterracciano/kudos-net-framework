﻿using Kudos.Constants;
using Kudos.Databases.Chainers;
using Kudos.Databases.Interfaces;
using Kudos.Databases.Interfaces.Chains;
using Kudos.Databases.Results;
using Kudos.Servers.KaronteModule.Constants;
using Kudos.Servers.KaronteModule.Contexts;
using Kudos.Servers.KaronteModule.Enums;
using Kudos.Servers.KaronteModule.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Primitives;
using System;
using System.Net;
using System.Threading.Tasks;

namespace Kudos.Servers.KaronteModule.Middlewares
{
    public abstract class AKaronteDatabasingMiddleware
        : AContexizedKaronteMiddleware<KaronteDatabasingContext>
    {
        protected AKaronteDatabasingMiddleware(ref RequestDelegate rd) : base(ref rd) { }

        protected override async Task<KaronteDatabasingContext> OnContextFetch(KaronteContext kc)
        {
            return kc.DatabasingContext;
        }

        protected override async Task OnBounceEnd(KaronteContext kc)
        {
            if (!kc.DatabasingContext.HasDatabaseHandler) return;
            await kc.DatabasingContext.DatabaseHandler.CloseConnectionAsync();
        }
    }
}