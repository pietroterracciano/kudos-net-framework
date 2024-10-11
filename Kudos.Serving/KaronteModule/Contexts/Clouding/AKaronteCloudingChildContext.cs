using System;

namespace Kudos.Serving.KaronteModule.Contexts.Clouding
{
    public abstract class AKaronteCloudingChildContext
    {
        public readonly KaronteCloudingContext KaronteCloudingContext;

        internal AKaronteCloudingChildContext(ref KaronteCloudingContext kcc)
        {
            KaronteCloudingContext = kcc;
        }
    }
}