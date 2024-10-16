using System;
using Kudos.Databases.Interfaces;
using Kudos.Serving.KaronteModule.Services.Databasing;

namespace Kudos.Serving.KaronteModule.Contexts.Databasing
{
    public sealed class KarontePoolizedDatabasingContext : AKaronteChildContext
    {
        private readonly KarontePoolizedDatabasingService _kpds;

        internal
            KarontePoolizedDatabasingContext
            (
                ref KarontePoolizedDatabasingService kpds,
                ref KaronteContext kc
            )
        :
            base
            (
                ref kc
            )
        {
            _kpds = kpds;
        }

        public IPoolizedDatabaseHandler? Get(String? sn) { return _kpds.Get(sn); }
        public IPoolizedDatabaseHandler Require(String? sn) { return _kpds.Require(sn); }
    }
}