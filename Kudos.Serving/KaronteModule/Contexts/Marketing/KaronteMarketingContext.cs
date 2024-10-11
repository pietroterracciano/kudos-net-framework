using Kudos.Serving.KaronteModule.Services.Marketing;

namespace Kudos.Serving.KaronteModule.Contexts.Marketing
{
    public sealed class KaronteMarketingContext
        : AKaronteChildContext
    {
        private readonly KaronteMarketingService _kms;
        public readonly KaronteMarketingBrevoContext BrevoContext;

        internal KaronteMarketingContext
            (
                ref KaronteMarketingService kms,
                ref KaronteContext kc
            )
        :
            base
            (
                ref kc
            )
        {
            _kms = kms;

            KaronteMarketingContext kmc = this;
            KaronteMarketingBrevoService kmbs = kms.BrevoService;
            BrevoContext = new KaronteMarketingBrevoContext(ref kmbs, ref kmc);
        }
    }
}