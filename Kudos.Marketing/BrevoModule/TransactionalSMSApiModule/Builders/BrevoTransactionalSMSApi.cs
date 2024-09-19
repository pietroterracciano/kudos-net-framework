using System;
using brevo_csharp.Api;
using brevo_csharp.Client;
using Kudos.Marketing.BrevoModule.Builders;

namespace Kudos.Marketing.BrevoModule.TransactionalSMSApiModule.Builders
{
    public sealed class
        BrevoTransactionalSMSApiBuilder
    :
        ABrevoBuilder<TransactionalSMSApi, BrevoTransactionalSMSApi>
    {
        protected override void OnApiAccessorTypeBuild(ref Configuration? cnf, out TransactionalSMSApi tsmsapi)
        {
            tsmsapi = new TransactionalSMSApi(cnf);
        }

        protected override void OnBuild(ref TransactionalSMSApi? tsmsapi, out BrevoTransactionalSMSApi btsmsapi)
        {
            btsmsapi = new BrevoTransactionalSMSApi(ref tsmsapi);
        }
    }
}