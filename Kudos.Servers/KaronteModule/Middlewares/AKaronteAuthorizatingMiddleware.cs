using Kudos.Constants;
using Kudos.Servers.KaronteModule.Attributes;
using Kudos.Servers.KaronteModule.Constants;
using Kudos.Servers.KaronteModule.Contexts;
using Kudos.Servers.KaronteModule.Descriptors.Authorizatings;
using Kudos.Servers.KaronteModule.Enums;
using Kudos.Servers.KaronteModule.Utils;
using Kudos.Utils;
using Kudos.Utils.Collections;
using Kudos.Utils.Texts;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using System;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Kudos.Servers.KaronteModule.Middlewares
{
    public abstract class
        AKaronteAuthorizatingMiddleware<AttributeType, EnumType>
    :
        AContexizedKaronteMiddleware<KaronteAuthorizatingContext>
    where
        AttributeType : AKaronteEnumizedAttribute<EnumType>
    where
        EnumType : Enum
    {
        public AKaronteAuthorizatingMiddleware(ref RequestDelegate rd) : base(ref rd) { }

        protected override async Task<KaronteAuthorizatingContext?> OnContextCreate(KaronteContext kc)
        {
            KaronteHeadingContext khc;
            kc.RequestObject<KaronteHeadingContext>(CKaronteKey.Heading, out khc);

            KaronteAttributingContext kac;
            kc.RequestObject<KaronteAttributingContext>(CKaronteKey.Attributing, out kac);

            KaronteAuthorizationDescriptor? rd = null;
            if(khc.HasHeaderValues)
                for (int i = 0; i < khc.HeaderValues.Count; i++)
                {
                    if (!OnAuthorizationRequestDescriptorFetch(khc.HeaderValues[i], out rd)) continue;
                    break;
                }

            AttributeType? at = kac.GetAttribute<AttributeType>();
            KaronteAuthorizationDescriptor? ed = at != null
                ? new KaronteAuthorizationDescriptor(null, at.Enum)
                : null;

            return kc.AuthorizatingContext = new KaronteAuthorizatingContext(ref rd, ref ed, ref kc);
        }

        protected override async Task OnBounceEnd(KaronteContext kc) { }

        private static Boolean OnAuthorizationRequestDescriptorFetch(String? s, out KaronteAuthorizationDescriptor? kad)
        {
            if (s == null)
            {
                kad = null;
                return false;
            }

            String[]
                sa = CRegex.Spaces1toN.Split(s);

            if
            (
                !ArrayUtils.IsValidIndex(sa, 1)
                || String.IsNullOrWhiteSpace(sa[1])
            )
            {
                kad = null;
                return false;
            }

            EnumType e = EnumUtils.Parse<EnumType>(sa[0]);

            if (!EnumUtils.IsValid<EnumType>(e))
            {
                kad = null;
                return false;
            }

            kad = new KaronteAuthorizationDescriptor(sa[1], e);
            return true;
        }
    }
}