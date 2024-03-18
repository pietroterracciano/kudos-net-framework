using System;

namespace Kudos.Databases.Interfaces.Chains
{
    public interface IMSSQLDatabaseChain : IBuildableDatabaseChain
    {
        IMSSQLDatabaseChain SetSource(String? s);
    }
}