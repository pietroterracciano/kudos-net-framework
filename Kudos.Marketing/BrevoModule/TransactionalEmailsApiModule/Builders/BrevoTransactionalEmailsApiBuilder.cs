using System;
using brevo_csharp.Api;
using brevo_csharp.Client;
using Kudos.Marketing.BrevoModule.Builders;

namespace Kudos.Marketing.BrevoModule.TransactionalEmailsApiModule.Builders
{
    public sealed class
        BrevoTransactionalEmailsApiBuilder
    :
        ABrevoBuilder<TransactionalEmailsApi, BrevoTransactionalEmailsApi>
    {
        protected override void OnApiAccessorTypeBuild(ref Configuration? cnf, out TransactionalEmailsApi apiat)
        {
            apiat = new TransactionalEmailsApi(cnf);
        }

        protected override void OnBuild(ref TransactionalEmailsApi? teapi, out BrevoTransactionalEmailsApi bteapi)
        {
            bteapi = new BrevoTransactionalEmailsApi(ref teapi);
        }
    }
}