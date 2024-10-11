using System;
using Kudos.Marketing.BrevoModule;
using Kudos.Marketing.BrevoModule.TransactionalEmailsApiModule;
using Kudos.Marketing.BrevoModule.TransactionalEmailsApiModule.Builders;
using Kudos.Marketing.BrevoModule.TransactionalSMSApiModule;
using Kudos.Marketing.BrevoModule.TransactionalSMSApiModule.Builders;
using Microsoft.Extensions.DependencyInjection;

namespace Kudos.Serving.KaronteModule.Services.Marketing
{
	public sealed class KaronteMarketingBrevoService
        : AKaronteMetizedService
	{
        #region ... static ...

        private static String
            __teapi,
            __tsmsapi;

        static KaronteMarketingBrevoService()
        {
            __teapi = "TransactionalEmailsApi";
            __tsmsapi = "TransactionalSMSApi";
        }

        #endregion

        internal KaronteMarketingBrevoService(ref IServiceCollection sc) : base(ref sc) { }

        #region TransactionalEmailsApi

        public KaronteMarketingBrevoService RegisterTransactionalEmailsApi(String? sn, Action<BrevoTransactionalEmailsApiBuilder>? act)
        {
            if (act == null) return this;
            BrevoTransactionalEmailsApiBuilder bteapib = Brevo.RequestTransactionalEmailsApiBuilder();
            act.Invoke(bteapib);
            BrevoTransactionalEmailsApi bteapi = bteapib.Build();
            _RegisterMeta(ref __teapi, ref sn, ref bteapi);
            return this;
        }

        internal BrevoTransactionalEmailsApi? RequireTransactionalEmailsApi(String? sn)
        {
            BrevoTransactionalEmailsApi? bteapi;
            _RequireMeta(ref __teapi, ref sn, out bteapi);
            return bteapi;
        }

        internal BrevoTransactionalEmailsApi? GetTransactionalEmailsApi(String? sn)
        {
            BrevoTransactionalEmailsApi? bteapi;
            _GetMeta(ref __teapi, ref sn, out bteapi);
            return bteapi;
        }

        #endregion

        #region TransactionalSMSApi

        public KaronteMarketingBrevoService RegisterTransactionalSMSApi(String? sn, Action<BrevoTransactionalSMSApiBuilder>? act)
        {
            if (act == null) return this;
            BrevoTransactionalSMSApiBuilder btsmsapib = Brevo.RequestTransactionalSMSApiBuilder();
            act.Invoke(btsmsapib);
            BrevoTransactionalSMSApi btsmsapi = btsmsapib.Build();
            _RegisterMeta(ref __tsmsapi, ref sn, ref btsmsapi);
            return this;
        }

        internal BrevoTransactionalSMSApi? RequireTransactionalSMSApi(String? sn)
        {
            BrevoTransactionalSMSApi? btsmsapi;
            _RequireMeta(ref __tsmsapi, ref sn, out btsmsapi);
            return btsmsapi;
        }

        internal BrevoTransactionalSMSApi? GetTransactionalSMSApi(String? sn)
        {
            BrevoTransactionalSMSApi? btsmsapi;
            _GetMeta(ref __tsmsapi, ref sn, out btsmsapi);
            return btsmsapi;
        }

        #endregion
    }
}