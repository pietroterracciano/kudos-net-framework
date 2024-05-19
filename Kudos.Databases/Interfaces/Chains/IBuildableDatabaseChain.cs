using Kudos.Databases.Handlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kudos.Databases.Interfaces.Chains
{
    public interface IBuildableDatabaseChain
    {
        IDatabaseHandler BuildHandler();
        DatabasePoolizedHandler BuildPoolizedHandler(Int32 i);
    }
}