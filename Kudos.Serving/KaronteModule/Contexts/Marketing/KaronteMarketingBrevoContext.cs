using System;
using Kudos.Marketing.BrevoModule.TransactionalEmailsApiModule;
using Kudos.Marketing.BrevoModule.TransactionalSMSApiModule;
using Kudos.Serving.KaronteModule.Contexts.Crypting;
using Kudos.Serving.KaronteModule.Services.Crypting;
using Kudos.Serving.KaronteModule.Services.Marketing;

namespace Kudos.Serving.KaronteModule.Contexts.Marketing
{
    public sealed class KaronteMarketingBrevoContext
        : AKaronteMarketingChildContext
    {
        private readonly KaronteMarketingBrevoService _kmbs;

        internal KaronteMarketingBrevoContext
            (
                ref KaronteMarketingBrevoService kmbs,
                ref KaronteMarketingContext kmc
            )
        :
            base
            (
                ref kmc
            )
        {
            _kmbs = kmbs;
        }

        #region TransactionalEmailsApi

        public BrevoTransactionalEmailsApi? GetTransactionalEmailsApi(String? sn) { return _kmbs.GetTransactionalEmailsApi(sn); }
        public BrevoTransactionalEmailsApi? RequireTransactionalEmailsApi(String? sn) { return _kmbs.RequireTransactionalEmailsApi(sn); }

        #endregion

        #region TransactionalSMSApi

        public BrevoTransactionalSMSApi? GetTransactionalSMSApi(String? sn) { return _kmbs.GetTransactionalSMSApi(sn); }
        public BrevoTransactionalSMSApi? RequireTransactionalSMSApi(String? sn) { return _kmbs.RequireTransactionalSMSApi(sn); }

        #endregion
    }
}