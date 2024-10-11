using System;
using Kudos.Marketing.BrevoModule;
using Kudos.Marketing.BrevoModule.TransactionalEmailsApiModule.Builders;
using Kudos.Marketing.BrevoModule.TransactionalSMSApiModule.Builders;
using Kudos.Types;
using Microsoft.Extensions.DependencyInjection;

namespace Kudos.Serving.KaronteModule.Services.Marketing
{
    public sealed class
        KaronteMarketingService
    :
        AKaronteService
    {
        public readonly KaronteMarketingBrevoService BrevoService;

        internal KaronteMarketingService(ref IServiceCollection sc) : base(ref sc)
        {
            BrevoService = new KaronteMarketingBrevoService(ref sc);
        }
    }
}