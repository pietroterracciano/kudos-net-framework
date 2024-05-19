using Kudos.Databases.Interfaces;

namespace Kudos.Servers.KaronteModule.Contexts
{
    public sealed class KaronteDatabasingContext : AKaronteChildContext
    {
        public IDatabaseHandler? Handler { get; internal set; }

        internal KaronteDatabasingContext(ref KaronteContext kc) : base(ref kc) { }
    }
}
