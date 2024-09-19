using System;
using Kudos.Clouds.AmazonWebServiceModule.PinpointModule;
using Kudos.Clouds.AmazonWebServiceModule.PinpointModule.Builders;
using Kudos.Clouds.AmazonWebServiceModule.S3Module;
using Kudos.Clouds.AmazonWebServiceModule.S3Module.Builders;
using Kudos.Marketing.BrevoModule.TransactionalEmailsApiModule;
using Kudos.Marketing.BrevoModule.TransactionalEmailsApiModule.Builders;
using Kudos.Marketing.BrevoModule.TransactionalSMSApiModule;
using Kudos.Marketing.BrevoModule.TransactionalSMSApiModule.Builders;
using Kudos.Servers.KaronteModule.Services;

namespace Kudos.Servers.KaronteModule.Contexts
{
    public sealed class KaronteMarketingContext
        : AKaronteChildContext
    {
        private readonly KaronteMarketingService _kms;

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
        }

        public BrevoTransactionalEmailsApi? GetBrevoTransactionalEmailsApi(String? sn)
        {
            BrevoTransactionalEmailsApiBuilder? bteapib = _kms.BrevoTransactionalEmailsApiBuilders.Get<BrevoTransactionalEmailsApiBuilder>(sn);
            return bteapib != null ? bteapib.Build() : null;
        }

        public BrevoTransactionalEmailsApi RequireBrevoTransactionalEmailsApi(String? sn)
        {
            BrevoTransactionalEmailsApi? bteapib = GetBrevoTransactionalEmailsApi(sn);
            if (bteapib == null) throw new InvalidOperationException();
            return bteapib;
        }

        public BrevoTransactionalSMSApi? GetBrevoTransactionalSMSApi(String? sn)
        {
            BrevoTransactionalSMSApiBuilder? btsmsapib = _kms.BrevoTransactionalSMSApiBuilders.Get<BrevoTransactionalSMSApiBuilder>(sn);
            return btsmsapib != null ? btsmsapib.Build() : null;
        }

        public BrevoTransactionalSMSApi RequireBrevoTransactionalSMSApi(String? sn)
        {
            BrevoTransactionalSMSApi? btsmsapi = GetBrevoTransactionalSMSApi(sn);
            if (btsmsapi == null) throw new InvalidOperationException();
            return btsmsapi;
        }
    }
}