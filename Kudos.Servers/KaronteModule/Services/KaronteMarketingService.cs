using System;
using Kudos.Clouds.AmazonWebServiceModule.PinpointModule;
using Kudos.Clouds.AmazonWebServiceModule.PinpointModule.Builders;
using Kudos.Clouds.AmazonWebServiceModule.S3Module;
using Kudos.Clouds.AmazonWebServiceModule.S3Module.Builders;
using Kudos.Marketing.BrevoModule;
using Kudos.Marketing.BrevoModule.TransactionalEmailsApiModule.Builders;
using Kudos.Marketing.BrevoModule.TransactionalSMSApiModule.Builders;
using Kudos.Types;
using Microsoft.Extensions.DependencyInjection;

namespace Kudos.Servers.KaronteModule.Services
{
    public class
        KaronteMarketingService
    :
        AKaronteService
    {
        internal readonly Metas BrevoTransactionalEmailsApiBuilders;
        internal readonly Metas BrevoTransactionalSMSApiBuilders;

        internal KaronteMarketingService(ref IServiceCollection sc) : base(ref sc)
        {
            BrevoTransactionalEmailsApiBuilders = new Metas(StringComparison.OrdinalIgnoreCase);
            BrevoTransactionalSMSApiBuilders = new Metas(StringComparison.OrdinalIgnoreCase);
        }

        public KaronteMarketingService RegisterBrevoTransactionalEmailsApiBuilder(string? sn, Action<BrevoTransactionalEmailsApiBuilder>? act)
        {
            if (sn != null && act != null)
            {
                BrevoTransactionalEmailsApiBuilder bteapib = Brevo.RequestTransactionalEmailsApiBuilder();
                act.Invoke(bteapib);
                BrevoTransactionalEmailsApiBuilders.Set(sn, bteapib);
            }

            return this;
        }

        public KaronteMarketingService RegisterBrevoTransactionalSMSApiBuilder(string? sn, Action<BrevoTransactionalSMSApiBuilder>? act)
        {
            if (sn != null && act != null)
            {
                BrevoTransactionalSMSApiBuilder btsmsapib = Brevo.RequestTransactionalSMSApiBuilder();
                act.Invoke(btsmsapib);
                BrevoTransactionalSMSApiBuilders.Set(sn, btsmsapib);
            }

            return this;
        }
    }
}