using System;
using System.Threading.Tasks;
using Kudos.Servers.KaronteModule.Contexts;
using Kudos.Servers.KaronteModule.Enums;
using Microsoft.AspNetCore.Http;

namespace Kudos.Servers.KaronteModule.Middlewares
{
    internal sealed class KaronteCryptingMiddleware
        : AKaronteMiddleware
    {
        public KaronteCryptingMiddleware( RequestDelegate rd) : base(ref rd) { }

        protected override async Task<EKaronteBounce> OnBounce(KaronteContext kc)
        {
            kc.CryptingContext = new KaronteCryptingContext(ref kc);
            return EKaronteBounce.MoveForward;
        }

        protected override async Task OnBounceReturn(KaronteContext kc) { }
    }
}

