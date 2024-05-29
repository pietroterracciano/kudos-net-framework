using System;
using Kudos.Constants;
using Kudos.Servers.KaronteModule.Attributes;
using Kudos.Servers.KaronteModule.Constants;
using Kudos.Servers.KaronteModule.Contexts;
using Kudos.Servers.KaronteModule.Enums;
using Kudos.Servers.KaronteModule.Utils;
using Kudos.Utils;
using Kudos.Utils.Collections;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using System.Threading.Tasks;

namespace Kudos.Servers.KaronteModule.Middlewares
{
    public abstract class
        AKaronteHeadingMiddleware
    :
        AContexizedKaronteMiddleware<KaronteHeadingContext>
    {
        public AKaronteHeadingMiddleware(ref RequestDelegate rd) : base(ref rd) { }

        protected override async Task<KaronteHeadingContext> OnContextCreate(KaronteContext kc)
        {
            KaronteHeadingContext khc;
            kc.RequestObject<KaronteHeadingContext>(CKaronteKey.Heading, out khc);
            return khc;
        }
    }
}