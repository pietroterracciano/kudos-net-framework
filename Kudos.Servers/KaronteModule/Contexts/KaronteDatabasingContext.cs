using Kudos.Databases.Interfaces;
using Kudos.Databases.Interfaces.Chains;
using Kudos.Servers.KaronteModule.Options;

namespace Kudos.Servers.KaronteModule.Contexts
{
    public sealed class KaronteDatabasingContext : AKaronteChildContext
    {
        public readonly IDatabaseHandler? DatabaseHandler;

        internal KaronteDatabasingContext(ref KaronteContext kc) : base(ref kc)
        {
            IBuildableDatabaseChain? bdbc = KaronteContext.GetRequiredService<KaronteDatabasingOptions>().BuildableDatabaseChain;
            DatabaseHandler = bdbc != null ? bdbc.BuildHandler() : null;
        }
    }
}
