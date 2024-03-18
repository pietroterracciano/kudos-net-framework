using Kudos.Servers.KaronteModule.Contexts;
using Kudos.Servers.KaronteModule.Enums;
using Kudos.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using System.Net;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.Threading.Tasks;
using System.Text.Encodings.Web;

namespace Kudos.Servers.KaronteModule.Middlewares
{
    public sealed class KaronteJSONingMiddleware : AKaronteMiddleware
    {
        public KaronteJSONingMiddleware(RequestDelegate rd) : base(ref rd) { }

        protected override async Task<EKaronteBounce> OnBounce(KaronteContext kc)
        {
            kc.JSONingContext = new KaronteJSONingContext(ref kc);
            return EKaronteBounce.MoveForward;
        }

        protected override async Task OnBounceReturn(KaronteContext kc)
        {
        }
    }
}
