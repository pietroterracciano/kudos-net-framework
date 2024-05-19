using Kudos.Servers.KaronteModule.Constants;
using Kudos.Servers.KaronteModule.Contexts;
using Kudos.Servers.KaronteModule.Enums;
using Kudos.Servers.Utils;
using Kudos.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Kudos.Servers.KaronteModule.Middlewares
{
    public abstract class AKaronteResponsingMiddleware<NonActionResultType> : AKaronteMiddleware
    {
        public AKaronteResponsingMiddleware(ref RequestDelegate rd) : base(ref rd) { }

        protected override async Task<EKaronteBounce> OnBounce(KaronteContext kc)
        {
            kc.ResponsingContext = new KaronteResponsingContext(ref kc);
            //kc.ResponsingContext.StatusCode = HttpStatusCode.OK;
            //kc.ResponsingContext.SetNonActionResult = OnNonActionResultCreation();
            return EKaronteBounce.MoveForward;
        }

        protected override async Task OnBounceReturn(KaronteContext kc)
        {
            NonActionResultType?
                nar = ObjectUtils.Cast<NonActionResultType>(kc.ResponsingContext.NonActionResult);

            HttpStatusCode?
                httpsc =
                    OnNonActionResultTransformationToHttpResponseStatusCode(nar);

            Int32? 
                ihttpsc = EnumUtils.GetValue(httpsc);

            if (ihttpsc != null)
                kc.HttpContext.Response.StatusCode = ihttpsc.Value;

            Object? o =
                OnNonActionResultTransformationToActionResult(nar);

            if (kc.JSONingContext == null) return;

            kc.HttpContext.Response.Headers.ContentType = 
                CKaronteContentType.application_json;

            kc.HttpContext.Response.Headers.Accept = 
                StringValuesUtils.Concat
                (
                    CKaronteContentType.application_json,
                    CKaronteContentType.multipart_form_data,
                    CKaronteContentType.application_x_www_form_urlencoded
                );

            String? s = kc.JSONingContext.Serialize(o);

            if (s == null) return;
                
            await kc.HttpContext.Response.WriteAsync(s);
        }

        //protected abstract NonActionResultType? OnNonActionResultCreation();

        protected abstract HttpStatusCode? OnNonActionResultTransformationToHttpResponseStatusCode(NonActionResultType? nar);

        protected abstract Object? OnNonActionResultTransformationToActionResult(NonActionResultType? nar);
    }
}