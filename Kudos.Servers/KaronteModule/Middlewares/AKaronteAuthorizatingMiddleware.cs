using Kudos.Servers.KaronteModule.Contexts;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace Kudos.Servers.KaronteModule.Middlewares
{
    public abstract class
        AKaronteAuthorizatingMiddleware
    :
        AContexizedKaronteMiddleware<KaronteAuthorizatingContext>
    {
        public AKaronteAuthorizatingMiddleware(ref RequestDelegate rd) : base(ref rd) { }

        protected override async Task<KaronteAuthorizatingContext?> OnContextFetch(KaronteContext kc)
        {
            return kc.AuthorizatingContext;
        }

        protected override async Task OnBounceEnd(KaronteContext kc) { }
    }
}