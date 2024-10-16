using System;
using System.Text.Json;
using Kudos.Databases.Chainers;
using Kudos.Databases.Interfaces;
using Kudos.Databases.Interfaces.Chains;
using Kudos.Types;
using Microsoft.Extensions.DependencyInjection;

namespace Kudos.Serving.KaronteModule.Services.Databasing
{
    public sealed class
        KaronteDatabasingService
    :
        AKaronteMetizedService
    {
        internal KaronteDatabasingService(ref IServiceCollection sc) : base(ref sc) { }

        public KaronteDatabasingService Register
        (
            String? sn,
            Func<IDatabaseChain, IBuildableDatabaseChain>? fnc
        )
        {
            if (fnc == null) return this;
            IDatabaseChain dc = DatabaseChainer.NewChain();
            IBuildableDatabaseChain? bdc = fnc.Invoke(dc);
            if (bdc == null) return this;
            _RegisterMeta(ref sn, ref bdc);
            return this;
        }

        internal IBuildableDatabaseChain Require(String? sn)
        {
            IBuildableDatabaseChain pdh;
            _RequireMeta<IBuildableDatabaseChain>(ref sn, out pdh);
            return pdh;
        }

        internal IBuildableDatabaseChain? Get(String? sn)
        {
            IBuildableDatabaseChain? pdh;
            _GetMeta<IBuildableDatabaseChain>(ref sn, out pdh);
            return pdh;
        }
    }
}