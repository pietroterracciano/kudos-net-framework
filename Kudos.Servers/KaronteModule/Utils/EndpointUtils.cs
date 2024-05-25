using Kudos.Servers.KaronteModule.Contexts;
using Kudos.Servers.KaronteModule.Enums;
using Kudos.Utils;
using Microsoft.AspNetCore.Http;
using Org.BouncyCastle.Asn1.Crmf;
using System;

namespace Kudos.Servers.KaronteModule.Utils
{
    public static class EndpointUtils
    {
        public static Boolean IsFallbackRoute(Endpoint? end)
        {
            if (end == null) return false;
            String? sdn = end.DisplayName;
            return sdn != null && sdn.StartsWith("Fallback", StringComparison.OrdinalIgnoreCase);
        }

        public static Boolean IsRootRoute(Endpoint? end)
        {
            if (end == null) return false;
            String? sdn = end.DisplayName;
            return sdn != null && sdn.Equals("/", StringComparison.OrdinalIgnoreCase);
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
    }
}
