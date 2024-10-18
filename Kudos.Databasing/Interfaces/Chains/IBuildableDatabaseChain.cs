using Kudos.Databasing.Handlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kudos.Databasing.Interfaces.Chains
{
    public interface IBuildableDatabaseChain
    {
        IDatabaseHandler BuildHandler();
        IPoolizedDatabaseHandler BuildPoolizedHandler();
    }
}