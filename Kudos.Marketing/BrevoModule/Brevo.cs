using System;
using brevo_csharp.Api;
using brevo_csharp.Client;
using brevo_csharp.Model;
using Kudos.Marketing.BrevoModule.TransactionalEmailsApiModule.Builders;
using Kudos.Marketing.BrevoModule.TransactionalSMSApiModule.Builders;

namespace Kudos.Marketing.BrevoModule
{
	public static class Brevo
	{
		public static BrevoTransactionalEmailsApiBuilder RequestTransactionalEmailsApiBuilder()
		{
			return new BrevoTransactionalEmailsApiBuilder();
		}

        public static BrevoTransactionalSMSApiBuilder RequestTransactionalSMSApiBuilder()
        {
            return new BrevoTransactionalSMSApiBuilder();
        }
    }
}

