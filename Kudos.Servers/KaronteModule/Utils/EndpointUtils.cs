using Kudos.Reflection.Utils;
using Kudos.Servers.KaronteModule.Attributes;
using Kudos.Servers.KaronteModule.Constants;
using Kudos.Servers.KaronteModule.Contexts;
using Kudos.Servers.KaronteModule.Descriptors.Routes;
using Kudos.Servers.KaronteModule.Enums;
using Kudos.Utils;
using Kudos.Utils.Collections;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Metadata;
using Org.BouncyCastle.Asn1.Crmf;
using Org.BouncyCastle.Asn1.X500;
using System;
using System.Reflection;

namespace Kudos.Servers.KaronteModule.Utils
{
    public static class EndpointUtils
    {
        public static EKaronteRouteStatus GetRouteStatus(Endpoint? end)
        {
            if (end == null || end.DisplayName == null)
                return EKaronteRouteStatus.NotRegistered;
            else if (end.Metadata == null || end.Metadata.Count < 1)
                return EKaronteRouteStatus.NotSupported;
            else if (end.DisplayName.StartsWith("Fallback", StringComparison.OrdinalIgnoreCase))
                return EKaronteRouteStatus.OnFallback;
            else if (end.DisplayName.Equals("/", StringComparison.OrdinalIgnoreCase))
                return EKaronteRouteStatus.OnRoot;
            else
                return EKaronteRouteStatus.Registered;
        }

        public static ObjectType? GetLastMetadata<ObjectType>(Endpoint? end)
        {
            ObjectType? 
                o = default(ObjectType);

            if (end != null)
            {
                EndpointMetadataCollection emc = end.Metadata;
                for (int i = emc.Count - 1; i > -1; i--)
                {
                    o = ObjectUtils.Cast<ObjectType>(emc[i]);
                    if (o != null) break;
                }
            }

            return o;
        }

        //public static Object? GetLastMetadata(Endpoint? end)
        //{
        //    return
        //        end != null && end.Metadata.Count > 0
        //            ? end.Metadata[end.Metadata.Count - 1]
        //            : null;
        //}
    }
}
