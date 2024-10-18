using System;
using Kudos.Databasing.Interfaces;
using Kudos.Databasing.Interfaces.Chains;
using Kudos.Pooling.Policies;
using Microsoft.Extensions.ObjectPool;

namespace Kudos.Databasing.Policies
{
    internal sealed class DatabaseHandlerPoolPolicy
        : ILazyLoadPoolPolicy<IDatabaseHandler>
    {
        private readonly IBuildableDatabaseChain _bdc;
        internal DatabaseHandlerPoolPolicy(ref IBuildableDatabaseChain bdc) { _bdc = bdc; }

        public IDatabaseHandler OnCreateObject() { return _bdc.BuildHandler(); }
        public bool OnReturnObject(IDatabaseHandler? dh) { return dh != null; }
    }
}