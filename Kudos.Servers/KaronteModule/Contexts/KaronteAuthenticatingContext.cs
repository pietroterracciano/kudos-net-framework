using Azure.Core.GeoJson;
using Kudos.Servers.KaronteModule.Descriptors.Authenticatings;
using Kudos.Servers.KaronteModule.Descriptors.Authorizatings;
using Kudos.Utils;
using System;

namespace Kudos.Servers.KaronteModule.Contexts
{
    public sealed class KaronteAuthenticatingContext : AKaronteChildContext
    {
        public readonly Boolean NeedToAuthenticate;

        internal
            KaronteAuthenticatingContext
            (
                ref KaronteAttributingContext? kac,
                ref KaronteContext kc
            )
        :
            base
            (
                ref kc
            )
        {
            NeedToAuthenticate = kac != null && kac.HasAttribute;
        }

        //private void throwRequiredAuthenticationDataException(Type? t)
        //{
        //    throw new InvalidOperationException
        //    (
        //        "Required AuthenticationData " + (t != null ? t.Name + " " : String.Empty) + "not registered correctly in KaronteAuthenticatingContext"
        //    );
        //}

        //public T RequireAuthenticationData<T>()
        //{
        //    T? ad = GetAuthenticationData<T>();
        //    if (ad == null) throwRequiredAuthenticationDataException(typeof(T));
        //    return ad;
        //}

        //public T? GetAuthenticationData<T>()
        //{
        //    return
        //        HasAuthenticationData
        //            ? ObjectUtils.Cast<T>(AuthenticationData)
        //            : default(T);
        //}
    }
}