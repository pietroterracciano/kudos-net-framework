using Kudos.Constants;
using Kudos.Databases.Chainers;
using Kudos.Databases.Interfaces;
using Kudos.Databases.Interfaces.Chains;
using Kudos.Databases.Results;
using Kudos.Servers.KaronteModule.Constants;
using Kudos.Servers.KaronteModule.Contexts;
using Kudos.Servers.KaronteModule.Enums;
using Kudos.Servers.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Primitives;
using System;
using System.Net;
using System.Threading.Tasks;

namespace Kudos.Servers.KaronteModule.Middlewares
{
    public abstract class AKaronteDatabasingMiddleware : AKaronteMiddleware
    {
        public AKaronteDatabasingMiddleware(ref RequestDelegate rd) : base(ref rd) { }

        protected override async Task<EKaronteBounce> OnBounce(KaronteContext kc)
        {
            kc.DatabasingContext = new KaronteDatabasingContext(ref kc);

            IBuildableDatabaseChain? bdbc = await OnDatabaseChainBuild(DatabaseChainer.NewChain());
            IDatabaseHandler? dbh = bdbc != null ? bdbc.BuildHandler() : null;

            kc.DatabasingContext.Handler = dbh;
            return await OnDatabaseHandlerCreate(kc.DatabasingContext);
        }

        protected override async Task OnBounceReturn(KaronteContext kc)
        {
            if (kc.DatabasingContext == null) return;
            await kc.DatabasingContext.Handler.CloseConnectionAsync();
        }

        protected abstract Task<IBuildableDatabaseChain?> OnDatabaseChainBuild(IDatabaseChain dbc);
        protected abstract Task<EKaronteBounce> OnDatabaseHandlerCreate(KaronteDatabasingContext kdbc);
    }
}
