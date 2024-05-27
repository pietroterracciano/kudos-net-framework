using Kudos.Constants;
using Kudos.Servers.KaronteModule.Attributes;
using Kudos.Servers.KaronteModule.Constants;
using Kudos.Servers.KaronteModule.Contexts;
using Kudos.Servers.KaronteModule.Descriptors.Tokens;
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
        AKaronteMiddleware
    where
        AttributeType : AKaronteAuthorizatingAttribute<EnumType>
    where
        EnumType : Enum
    {
        public AKaronteAuthorizatingMiddleware(ref RequestDelegate rd) : base(ref rd) { }

        protected override async Task<EKaronteBounce> OnBounce(KaronteContext kc)
        {
            KaronteHeadingContext khc;
            kc.RequestObject<KaronteHeadingContext>(CKaronteKey.Heading, out khc);

            KaronteAttributingContext kac;
            kc.RequestObject<KaronteAttributingContext>(CKaronteKey.Attributing, out kac);

            KaronteAuthorizatingDescriptor? rd = null;
            if(khc.HasHeaderValues)
                for (int i = 0; i < khc.HeaderValues.Count; i++)
                {
                    if (!FetchRequestDescriptor(khc.HeaderValues[i], out rd)) continue;
                    break;
                }

            AttributeType? at = kac.GetAttribute<AttributeType>();
            KaronteAuthorizatingDescriptor? ed = at != null
                ? new KaronteAuthorizatingDescriptor(null, at.Value)
                : null;

            kc.AuthorizatingContext = new KaronteAuthorizatingContext(ref rd, ref ed, ref kc);
            return await OnReceiveContext(kc.AuthorizatingContext);
        }

        protected override async Task OnBounceReturn(KaronteContext cc) { }

        protected abstract Task<EKaronteBounce> OnReceiveContext(KaronteAuthorizatingContext kac);

        private static Boolean FetchRequestDescriptor(String? s, out KaronteAuthorizatingDescriptor? kad)
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

            EnumType?
                et = EnumUtils.Parse<EnumType>(sa[0]);

            if (!EnumUtils.IsValid<EnumType>(et))
            {
                kad = null;
                return false;
            }

            kad = new KaronteAuthorizatingDescriptor(sa[1], et);
            return true;
        }
    }
}