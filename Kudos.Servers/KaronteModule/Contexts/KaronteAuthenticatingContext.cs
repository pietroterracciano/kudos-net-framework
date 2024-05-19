using Azure.Core.GeoJson;
using Kudos.Utils;
using System;

namespace Kudos.Servers.KaronteModule.Contexts
{
    public sealed class KaronteAuthenticatingContext : AKaronteChildContext
    {
        internal Object? AuthenticationData;
        public Boolean EndpointHasAuthentication { get; internal set; }

        internal KaronteAuthenticatingContext(ref KaronteContext kc) : base(ref kc) { }

        //internal KaronteAuthenticatingContext(ref KaronteContext kc, Object o, Boolean b) : base(ref kc)
        //{
        //    _oAuthenticationData = o;
        //    EndpointHasAuthetication = b;
        //}
        private void throwRequiredAuthenticationDataException(Type? t)
        {
            throw new InvalidOperationException
            (
                "Required AuthenticationData " + (t != null ? t.Name + " " : String.Empty) + "not registered correctly in AKaronteAuthenticatingMiddleware"
            );
        }

        public ObjectType GetRequiredAuthenticationData<ObjectType>()
        {
            ObjectType?
                ad = GetAuthenticationData<ObjectType>();

            if (ad == null)
                throwRequiredAuthenticationDataException(typeof(ObjectType));

            return ad;
        }

        public ObjectType? GetAuthenticationData<ObjectType>()
        {
            return ObjectUtils.Cast<ObjectType>(AuthenticationData);
        }
    }
}