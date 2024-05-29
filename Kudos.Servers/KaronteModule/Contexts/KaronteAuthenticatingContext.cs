using Azure.Core.GeoJson;
using Kudos.Servers.KaronteModule.Descriptors.Authenticatings;
using Kudos.Servers.KaronteModule.Descriptors.Authorizatings;
using Kudos.Utils;
using System;

namespace Kudos.Servers.KaronteModule.Contexts
{
    public sealed class KaronteAuthenticatingContext : AKaronteChildContext
    {
        public readonly KaronteAuthenticationDescriptor? AuthenticationEndpointDescriptor;
        public readonly Boolean HasAuthenticationEndpointDescriptor, HasAuthenticationData, IsAuthenticated;
        public readonly Object AuthenticationData;

        internal
            KaronteAuthenticatingContext
            (
                ref KaronteAuthenticationDescriptor? aed,
                ref Object? ad,
                ref KaronteContext kc
            )
        :
            base
            (
                ref kc
            )
        {
            HasAuthenticationEndpointDescriptor = (AuthenticationEndpointDescriptor = aed) != null;
            HasAuthenticationData = ( AuthenticationData = ad ) != null;
            IsAuthenticated =
                !HasAuthenticationEndpointDescriptor
                || HasAuthenticationData;
        }

        private void throwRequiredAuthenticationDataException(Type? t)
        {
            throw new InvalidOperationException
            (
                "Required AuthenticationData " + (t != null ? t.Name + " " : String.Empty) + "not registered correctly in KaronteAuthenticatingContext"
            );
        }

        public T RequireAuthenticationData<T>()
        {
            T? ad = GetAuthenticationData<T>();
            if (ad == null) throwRequiredAuthenticationDataException(typeof(T));
            return ad;
        }

        public T? GetAuthenticationData<T>()
        {
            return
                HasAuthenticationData
                    ? ObjectUtils.Cast<T>(AuthenticationData)
                    : default(T);
        }
    }
}