using Kudos.Serving.KaronteModule.Constants;
using Kudos.Serving.KaronteModule.Contexts;
using Kudos.Serving.KaronteModule.Enums;
using Kudos.Serving.KaronteModule.Utils;
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

namespace Kudos.Serving.KaronteModule.Middlewares
{
    public abstract class AKaronteResponsingMiddleware<NonActionResultType>
        : AContexizedKaronteMiddleware<KaronteResponsingContext>
    {
        protected AKaronteResponsingMiddleware(ref RequestDelegate rd) : base(ref rd) { }

        protected override async Task<KaronteResponsingContext> OnContextFetch(KaronteContext kc)
        {
            return kc.ResponsingContext = new KaronteResponsingContext(ref kc);
        }

        protected override async Task<EKaronteBounce> OnContextReceive(KaronteResponsingContext krc)
        {
            return EKaronteBounce.MoveForward;
        }

        protected override async Task OnBounceEnd(KaronteContext kc)
        {
            NonActionResultType?
                nar = ObjectUtils.Cast<NonActionResultType>(kc.ResponsingContext.NonActionResult);

            Int32?
                ihttpsc =
                    OnNonActionResultTransformationToHttpResponseStatusCode(kc.ResponsingContext, nar);

            if (ihttpsc != null)
                kc.HttpContext.Response.StatusCode = ihttpsc.Value;

            Object? o =
                OnNonActionResultTransformationToHttpResponseBody(kc.ResponsingContext, nar);

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

        protected abstract Int32? OnNonActionResultTransformationToHttpResponseStatusCode(KaronteResponsingContext krc, NonActionResultType? nar);

        protected abstract Object? OnNonActionResultTransformationToHttpResponseBody(KaronteResponsingContext krc, NonActionResultType? nar);
    }
}