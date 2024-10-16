using System;
using System.Threading.Tasks;
using Kudos.Databases.Chainers;
using Kudos.Databases.Interfaces;
using Kudos.Databases.Interfaces.Chains;
using Kudos.Serving.KaronteModule.Services.Crypting;
using Microsoft.Extensions.DependencyInjection;

namespace Kudos.Serving.KaronteModule.Services.Databasing
{
    public sealed class
        KarontePoolizedDatabasingService
    :
        AKaronteMetizedService
    {
        internal KarontePoolizedDatabasingService(ref IServiceCollection sc) : base(ref sc) { }

        public KarontePoolizedDatabasingService Register
        (
            String? sn,
            Func<IDatabaseChain, IBuildableDatabaseChain>? fnc,
            UInt16 iMinPoolSize,
            UInt16 iMaxPoolSize
        )
        {
            if (fnc == null) return this;
            IDatabaseChain dc = DatabaseChainer.NewChain();
            IBuildableDatabaseChain? bdc = fnc.Invoke(dc);
            dc
                .SetMinimumPoolSize(iMinPoolSize)
                .SetMaximumPoolSize(iMaxPoolSize);
            if (bdc == null) return this;
            IPoolizedDatabaseHandler pdh = bdc.BuildPoolizedHandler();
            _RegisterMeta(ref sn, ref pdh);
            return this;
        }

        internal IPoolizedDatabaseHandler Require(String? sn)
        {
            IPoolizedDatabaseHandler? pdh;
            _RequireMeta<IPoolizedDatabaseHandler>(ref sn, out pdh);
            return pdh;
        }

        internal IPoolizedDatabaseHandler? Get(String? sn)
        {
            IPoolizedDatabaseHandler? pdh;
            _GetMeta<IPoolizedDatabaseHandler>(ref sn, out pdh);
            return pdh;
        }

        public KarontePoolizedDatabasingService Dispose(String? sn)
        {
            IPoolizedDatabaseHandler? pdh = Get(sn);
            if (pdh != null) pdh.Dispose();
            return this;
        }

        public async Task DisposeAsync(String? sn)
        {
            IPoolizedDatabaseHandler? pdh = Get(sn);
            if (pdh != null) await pdh.DisposeAsync();
        }
    }
}
