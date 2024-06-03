using Kudos.Servers.KaronteModule.Attributes;
using Kudos.Servers.KaronteModule.Enums;
using Kudos.Utils;
using System;

namespace Kudos.Servers.KaronteModule.Contexts
{
    public sealed class KaronteAuthenticatingContext : AKaronteChildContext
    {
        private Boolean _bIsEndpointAnalyzed, _bIsAuthenticationRequired;
        internal Object? AuthenticationData;

        internal KaronteAuthenticatingContext(ref KaronteContext kc) : base(ref kc) { }

        public Boolean IsAuthenticationRequired() { _AnalyzeEndpoint(); return _bIsAuthenticationRequired; }
        private void _AnalyzeEndpoint()
        {
            if (_bIsEndpointAnalyzed) return;
            _bIsEndpointAnalyzed = true;

            KaronteNoAuthenticatingAttribute?
                knaa = KaronteContext.RoutingContext.GetLastEndpointMetadata<KaronteNoAuthenticatingAttribute>();

            if (knaa != null)
            {
                _bIsAuthenticationRequired = false;
                return;
            }

            KaronteAuthenticatingAttribute?
                kaa = KaronteContext.RoutingContext.GetLastEndpointMetadata<KaronteAuthenticatingAttribute>();

            _bIsAuthenticationRequired = kaa != null;
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
            return ObjectUtils.Cast<T>(AuthenticationData);
        }
    }
}