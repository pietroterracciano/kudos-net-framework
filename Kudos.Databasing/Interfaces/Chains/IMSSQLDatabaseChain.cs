using System;

namespace Kudos.Databasing.Interfaces.Chains
{
    public interface IMSSQLDatabaseChain : IBuildableDatabaseChain
    {
        IMSSQLDatabaseChain SetSource(String? s);
    }
}