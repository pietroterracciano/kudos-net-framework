using Kudos.Databasing.Controllers;
using Kudos.Databasing.Handlers;
using Kudos.Databasing.Interfaces.Chains;
using Kudos.Databasing.Interfaces;

namespace Kudos.Databasing.Chains
{
    public abstract class ABuildableDatabaseChain : DatabaseChain, IBuildableDatabaseChain
    {
        internal ABuildableDatabaseChain(DatabaseChain? o) : base(o) { }

        public abstract IDatabaseHandler BuildHandler();

        public IPoolizedDatabaseHandler BuildPoolizedHandler()
        {
            return new PoolizedDatabaseHandler(this);
        }
    }
}