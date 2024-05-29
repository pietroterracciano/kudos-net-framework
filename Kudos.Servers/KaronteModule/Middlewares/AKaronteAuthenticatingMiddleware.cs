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
    public abstract class AKaronteAuthenticatingMiddleware<AuthenticationDataType, AttributeType, EnumType>
        : AContexizedKaronteMiddleware<KaronteAuthenticatingContext>
    where
        AttributeType : AKaronteEnumizedAttribute<EnumType>
    where
        EnumType : Enum
    {
        public AKaronteAuthenticatingMiddleware(ref RequestDelegate rd) : base(ref rd) {}

        protected override async Task<KaronteAuthenticatingContext?> OnContextCreate(KaronteContext kc)
        {
            KaronteAttributingContext kac;
            kc.RequestObject<KaronteAttributingContext>(CKaronteKey.Attributing, out kac);

            AttributeType?
                at = kac.GetAttribute<AttributeType>();

            KaronteAuthenticationDescriptor?
                kad = at != null
                    ? new KaronteAuthenticationDescriptor(at.Enum)
                    : null;

            Object?
                ad = kad != null
                    ? await OnAuthenticationDataFetch(kc, kad)
                    : null;

            return kc.AuthenticatingContext = new KaronteAuthenticatingContext(ref kad, ref ad, ref kc);
        }

        protected abstract Task<AuthenticationDataType?> OnAuthenticationDataFetch(KaronteContext kc, KaronteAuthenticationDescriptor kad);

        protected override async Task OnBounceEnd(KaronteContext kc) { }
    }
}