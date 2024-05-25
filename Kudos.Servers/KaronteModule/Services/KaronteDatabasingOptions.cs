using System;
using System.Text.Json;
using Kudos.Databases.Interfaces.Chains;

namespace Kudos.Servers.KaronteModule.Options
{
    internal class KaronteDatabasingOptions
    {
        public readonly IBuildableDatabaseChain?
            BuildableDatabaseChain;

        internal KaronteDatabasingOptions(ref IBuildableDatabaseChain? bdbc)
        {
            BuildableDatabaseChain = bdbc;
        }
    }
}

