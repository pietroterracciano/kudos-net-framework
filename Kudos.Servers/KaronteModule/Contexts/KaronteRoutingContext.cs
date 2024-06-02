using System;
using Kudos.Databases.Interfaces;
using Kudos.Servers.KaronteModule.Constants;
using Kudos.Servers.KaronteModule.Descriptors.Routes;
using Kudos.Servers.KaronteModule.Enums;
using Kudos.Servers.KaronteModule.Utils;
using Microsoft.AspNetCore.Http;

namespace Kudos.Servers.KaronteModule.Contexts
{
    public sealed class KaronteRoutingContext : AKaronteChildContext
    {
        public readonly Endpoint? Endpoint;
        public readonly EKaronteRouteStatus RouteStatus;
        public readonly KaronteMethodRouteDescriptor? MethodRouteDescriptor;
        public readonly Boolean HasMethodRouteDescriptor;

        internal KaronteRoutingContext(ref KaronteContext kc, ref Endpoint? end) : base(ref kc)
        {
            Endpoint = end;
            RouteStatus = EndpointUtils.GetRouteStatus(end);

            if
            (
                RouteStatus == EKaronteRouteStatus.NotRegistered
                || RouteStatus == EKaronteRouteStatus.NotSupported
            )
                return;

            kc.HttpContext.Response.StatusCode = CKaronteHttpStatusCode.MethodNotAllowed;
            KaronteMethodRouteDescriptor.Request(ref end, out MethodRouteDescriptor);

            HasMethodRouteDescriptor = MethodRouteDescriptor != null;
        }
    }
}