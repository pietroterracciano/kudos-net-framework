using Kudos.Servers.KaronteModule.Contexts;
using Kudos.Servers.KaronteModule.Enums;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace Kudos.Servers.KaronteModule.Middlewares
{
    public class AKaronteAcceptMiddleware : AKaronteMiddleware
    {
        public AKaronteAcceptMiddleware(ref RequestDelegate rd) : base(ref rd) { }

        protected override Task<EKaronteBounce> OnBounce(KaronteContext kc)
        {
            throw new System.NotImplementedException();
        }

        protected override Task OnBounceReturn(KaronteContext kc)
        {
            throw new System.NotImplementedException();
        }
    }
}
