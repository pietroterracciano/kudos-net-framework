using System;
using Kudos.Databases.Interfaces;
using Kudos.Databases.Interfaces.Chains;
using Kudos.Serving.KaronteModule.Services.Databasing;

namespace Kudos.Serving.KaronteModule.Contexts.Databasing
{
    public sealed class KaronteDatabasingContext : AKaronteChildContext
    {
        private readonly KaronteDatabasingService _kds;

        internal
            KaronteDatabasingContext
            (
                ref KaronteDatabasingService kds,
                ref KaronteContext kc
            )
        :
            base
            (
                ref kc
            )
        {
            _kds = kds;
        }

        public IBuildableDatabaseChain? Get(String? sn) { return _kds.Get(sn); }
        public IBuildableDatabaseChain Require(String? sn) { return _kds.Require(sn); }
    }
}

