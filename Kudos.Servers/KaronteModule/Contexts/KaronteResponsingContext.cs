using System;

namespace Kudos.Servers.KaronteModule.Contexts
{
    public sealed class KaronteResponsingContext : AKaronteChildContext
    {
        //public HttpStatusCode? StatusCode;
        internal Object? NonActionResult { get; private set; }

        internal KaronteResponsingContext(ref KaronteContext kc) : base(ref kc) { }

        public KaronteResponsingContext SetNonActionResult(Object? o)
        {
            NonActionResult = o;
            return this;
        }
    }
}