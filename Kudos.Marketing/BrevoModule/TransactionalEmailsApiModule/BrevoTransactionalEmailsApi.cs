using System;
using brevo_csharp.Api;
using brevo_csharp.Model;

namespace Kudos.Marketing.BrevoModule.TransactionalEmailsApiModule
{
    public sealed class BrevoTransactionalEmailsApi
    {
        private readonly TransactionalEmailsApi? _teapi;

        internal BrevoTransactionalEmailsApi(ref TransactionalEmailsApi? teapi)
        {
            _teapi = teapi;
        }

        public System.Threading.Tasks.Task SendTransacEmailAsync(SendSmtpEmail? ssmtpe)
        {
            return System.Threading.Tasks.Task.Run(() => SendTransacEmail(ssmtpe));
        }

        public CreateSmtpEmail? SendTransacEmail(SendSmtpEmail? ssmtpe)
        {
            if (ssmtpe != null && _teapi != null)
                try { return _teapi.SendTransacEmail(ssmtpe); } catch(Exception e) { Exception prova = e; }

            return null;
        }
    }
}

