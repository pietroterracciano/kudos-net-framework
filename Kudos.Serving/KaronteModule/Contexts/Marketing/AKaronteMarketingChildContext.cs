using System;
using Kudos.Serving.KaronteModule.Contexts.Crypting;

namespace Kudos.Serving.KaronteModule.Contexts.Marketing
{
    public abstract class AKaronteMarketingChildContext
    {
        public readonly KaronteMarketingContext KaronteMarketingContext;

        internal AKaronteMarketingChildContext(ref KaronteMarketingContext kmc)
        {
            KaronteMarketingContext = kmc;
        }
    }
}

