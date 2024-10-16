using System;
using Kudos.Databases.Interfaces;
using Kudos.Databases.Interfaces.Chains;
using Kudos.Pooling.Policies;
using Microsoft.Extensions.ObjectPool;

namespace Kudos.Databases.Policies
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