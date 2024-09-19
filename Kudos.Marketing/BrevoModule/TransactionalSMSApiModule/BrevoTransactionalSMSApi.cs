using System;
using brevo_csharp.Api;
using brevo_csharp.Model;

namespace Kudos.Marketing.BrevoModule.TransactionalSMSApiModule
{
    public sealed class BrevoTransactionalSMSApi
    {
        private readonly TransactionalSMSApi? _tsmsapi;

        internal BrevoTransactionalSMSApi(ref TransactionalSMSApi? tsmsapi)
        {
            _tsmsapi = tsmsapi;
        }

        public System.Threading.Tasks.Task SendTransacEmailAsync(SendTransacSms? stsms)
        {
            return System.Threading.Tasks.Task.Run(() => SendTransacEmail(stsms));
        }

        public SendSms? SendTransacEmail(SendTransacSms? stsms)
        {
            if (stsms != null && _tsmsapi != null)
                try { return _tsmsapi.SendTransacSms(stsms); } catch (Exception e) { Exception prova = e; }

            return null;
        }
    }
}

