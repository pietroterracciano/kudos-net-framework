using Kudos.Databases.Interfaces;
using Kudos.Servers.KaronteModule.Attributes;
using Kudos.Servers.KaronteModule.Constants;
using Kudos.Servers.KaronteModule.Contexts;
using Kudos.Servers.KaronteModule.Descriptors.Authenticatings;
using Kudos.Servers.KaronteModule.Enums;
using Kudos.Servers.KaronteModule.Utils;
using Kudos.Utils;
using Microsoft.AspNetCore.Http;
using System;
using System.Net;
using System.Threading.Tasks;
using static Google.Protobuf.Reflection.FeatureSet.Types;

namespace Kudos.Servers.KaronteModule.Middlewares
{
    public abstract class AKaronteAuthenticatingMiddleware<AuthenticationDataType, AttributeType>
        : AContexizedKaronteMiddleware<KaronteAuthenticatingContext>
    where
        AttributeType : Attribute
    {
        public AKaronteAuthenticatingMiddleware(ref RequestDelegate rd) : base(ref rd) {}

        protected override async Task<KaronteAuthenticatingContext?> OnContextCreate(KaronteContext kc)
        {
            KaronteAttributingContext kac;
            kc.RequestObject<KaronteAttributingContext>(CKaronteKey.Attributing, out kac);
            return kc.AuthenticatingContext = new KaronteAuthenticatingContext(ref kac, ref kc);
        }

        protected override async Task OnBounceEnd(KaronteContext kc) { }
    }
}