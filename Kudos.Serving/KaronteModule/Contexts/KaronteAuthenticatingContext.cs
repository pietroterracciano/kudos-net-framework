using Kudos.Serving.KaronteModule.Attributes;
using Kudos.Serving.KaronteModule.Enums;
using Kudos.Utils;
using System;

namespace Kudos.Serving.KaronteModule.Contexts
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

            KaronteNoAuthenticationAttribute?
                knaa = KaronteContext.RoutingContext.GetLastEndpointMetadata<KaronteNoAuthenticationAttribute>();

            if (knaa != null)
            {
                _bIsAuthenticationRequired = false;
                return;
            }

            KaronteAuthenticationAttribute?
                kaa = KaronteContext.RoutingContext.GetLastEndpointMetadata<KaronteAuthenticationAttribute>();

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