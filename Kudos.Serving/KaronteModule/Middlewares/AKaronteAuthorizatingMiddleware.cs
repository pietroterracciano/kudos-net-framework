using Kudos.Serving.KaronteModule.Contexts;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace Kudos.Serving.KaronteModule.Middlewares
{
    public abstract class
        AKaronteAuthorizatingMiddleware
    :
        AContexizedKaronteMiddleware<KaronteAuthorizatingContext>
    {
        protected AKaronteAuthorizatingMiddleware(ref RequestDelegate rd) : base(ref rd) { }

        protected override async Task<KaronteAuthorizatingContext?> OnContextFetch(KaronteContext kc)
        {
            return kc.AuthorizatingContext;
        }

        protected override async Task OnBounceEnd(KaronteContext kc) { }
    }
}