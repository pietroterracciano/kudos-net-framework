using System;
using Kudos.Databases.Interfaces;
using Kudos.Databases.Interfaces.Chains;
using Kudos.Servers.KaronteModule.Options;
using Kudos.Servers.KaronteModule.Services;

namespace Kudos.Servers.KaronteModule.Contexts
{
    public sealed class KaronteDatabasingContext : AKaronteChildContext
    {
        private readonly KaronteDatabasingService _kdbs;

        public readonly IDatabaseHandler? DatabaseHandler;
        public readonly Boolean HasDatabaseHandler;

        internal KaronteDatabasingContext(ref KaronteContext kc) : base(ref kc)
        {
            _kdbs = kc.RequestService<KaronteDatabasingService>();
            DatabaseHandler = _kdbs.BuildableDatabaseChain != null ? _kdbs.BuildableDatabaseChain.BuildHandler() : null;
            HasDatabaseHandler = DatabaseHandler != null;
        }

        //public IDatabaseHandler? GetDatabaseHandler() { return _dbh; }
        //public IDatabaseHandler RequestDatabaseHandler()
        //{
        //    if (_dbh == null) throw new InvalidOperationException();
        //    return _dbh;
        //}
    }
}