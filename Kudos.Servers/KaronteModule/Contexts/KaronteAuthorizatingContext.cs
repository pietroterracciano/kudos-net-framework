using Kudos.Servers.KaronteModule.Descriptors.Authorizatings;
using Kudos.Utils;
using System;

namespace Kudos.Servers.KaronteModule.Contexts
{
    public sealed class KaronteAuthorizatingContext : AKaronteChildContext
    {
        public readonly KaronteAuthorizationDescriptor? AuthorizationRequestDescriptor, AuthorizationEndpointDescriptor;
        public readonly Boolean HasAuthorizationRequestDescriptor, HasAuthorizationEndpointDescriptor;
        public readonly Boolean IsAuthorized;

        internal
            KaronteAuthorizatingContext
            (
                ref KaronteAuthorizationDescriptor? ard,
                ref KaronteAuthorizationDescriptor? aed,
                ref KaronteContext kc
            )
        :
            base
            (
                ref kc
            )
        {
            HasAuthorizationRequestDescriptor = (AuthorizationRequestDescriptor = ard) != null;
            HasAuthorizationEndpointDescriptor = (AuthorizationEndpointDescriptor = aed) != null;
            IsAuthorized =
                !HasAuthorizationEndpointDescriptor
                ||
                (
                    HasAuthorizationRequestDescriptor
                    && EnumUtils.HasFlag<Enum>(AuthorizationRequestDescriptor.Type, AuthorizationEndpointDescriptor.Type)
                    && AuthorizationRequestDescriptor.HasCode
                );
        }
    }
}
