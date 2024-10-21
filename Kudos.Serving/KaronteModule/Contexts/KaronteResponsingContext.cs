using System;

namespace Kudos.Serving.KaronteModule.Contexts
{
    public sealed class KaronteResponsingContext : AKaronteChildContext
    {
        //public HttpStatusCode? StatusCode;
        internal Object? Response { get; private set; }

        internal KaronteResponsingContext(ref KaronteContext kc) : base(ref kc) { }

        public KaronteResponsingContext SetResponse(Object? nar)
        {
            Response = nar;
            return this;
        }
    }
}