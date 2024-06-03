using System;
using Kudos.Databases.Interfaces;
using Kudos.Servers.KaronteModule.Services;

namespace Kudos.Servers.KaronteModule.Contexts
{
    public sealed class KaronteDatabasingContext : AKaronteChildContext
    {
        public readonly IDatabaseHandler? DatabaseHandler;
        public readonly Boolean HasDatabaseHandler;

        internal
            KaronteDatabasingContext
            (
                ref KaronteDatabasingService kdbs,
                ref KaronteContext kc
            )
        :
            base
            (
                ref kc
            )
        {
            if (kdbs.BuildableDatabaseChain == null) return;
            HasDatabaseHandler = (DatabaseHandler = kdbs.BuildableDatabaseChain.BuildHandler()) != null;
        }
    }
}