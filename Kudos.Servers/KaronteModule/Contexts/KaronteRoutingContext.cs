using Kudos.Databases.Interfaces;
using Kudos.Servers.KaronteModule.Enums;
using Microsoft.AspNetCore.Http;

namespace Kudos.Servers.KaronteModule.Contexts
{
    public sealed class KaronteRoutingContext : AKaronteChildContext
    {
        public readonly Endpoint? Endpoint;
        public readonly EKaronteRoute Type;

        internal KaronteRoutingContext(ref KaronteContext kc, ref Endpoint? end, ref EKaronteRoute ekr) : base(ref kc)
        {
            Endpoint = end;
            Type = ekr;
        }
    }
}