using Kudos.Databasing.Chains;
using Kudos.Databasing.Interfaces.Chains;

namespace Kudos.Databasing.Chainers
{
    public static class DatabaseChainer
    {
        public static IDatabaseChain NewChain()
        {
            return new DatabaseChain();
        }
    }
}