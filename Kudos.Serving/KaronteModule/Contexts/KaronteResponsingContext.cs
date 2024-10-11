using System;

namespace Kudos.Serving.KaronteModule.Contexts
{
    public sealed class KaronteResponsingContext : AKaronteChildContext
    {
        //public HttpStatusCode? StatusCode;
        internal Object? NonActionResult { get; private set; }

        internal KaronteResponsingContext(ref KaronteContext kc) : base(ref kc) { }

        public KaronteResponsingContext SetNonActionResult(Object? nar)
        {
            NonActionResult = nar;
            return this;
        }
    }
}