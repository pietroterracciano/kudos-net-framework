using Kudos.Constants;
using Kudos.Servers.KaronteModule.Constants;
using Kudos.Servers.KaronteModule.Descriptors.Tokens;
using Kudos.Servers.KaronteModule.Enums;
using Kudos.Utils;
using System;

namespace Kudos.Servers.KaronteModule.Contexts
{
    public sealed class KaronteAuthorizatingContext : AKaronteChildContext
    {
        public readonly KaronteAuthorizatingDescriptor? RequestDescriptor, EndpointDescriptor;
        public readonly Boolean HasRequestDescriptor, HasEndpointDescriptor;
        public readonly Boolean IsEndpointAuthorized;

        internal
            KaronteAuthorizatingContext
            (
                ref KaronteAuthorizatingDescriptor? rd,
                ref KaronteAuthorizatingDescriptor? ed,
                ref KaronteContext kc
            )
        :
            base
            (
                ref kc
            )
        {
            HasRequestDescriptor = (RequestDescriptor = rd) != null;
            HasEndpointDescriptor = (EndpointDescriptor = ed) != null;
            IsEndpointAuthorized =
                !HasEndpointDescriptor
                ||
                (
                    HasRequestDescriptor
                    && EnumUtils.HasFlag<Enum>(RequestDescriptor.Type, EndpointDescriptor.Type)
                );
        }
    }
}
