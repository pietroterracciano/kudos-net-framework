using Kudos.Databases.Chains;
using Kudos.Databases.Interfaces.Chains;

namespace Kudos.Databases.Chainers
{
    public static class DatabaseChainer
    {
        public static IDatabaseChain NewChain()
        {
            return new DatabaseChain();
        }
    }
}