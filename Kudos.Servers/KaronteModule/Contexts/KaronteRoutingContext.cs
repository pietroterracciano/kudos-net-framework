﻿using System;
using System.Collections.Generic;
using Kudos.Databases.Interfaces;
using Kudos.Servers.KaronteModule.Constants;
using Kudos.Servers.KaronteModule.Descriptors.Routes;
using Kudos.Servers.KaronteModule.Enums;
using Kudos.Servers.KaronteModule.Utils;
using Kudos.Utils;
using Kudos.Utils.Collections;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Kudos.Servers.KaronteModule.Contexts
{
    public sealed class KaronteRoutingContext : AKaronteChildContext
    {
        private readonly Object _lck;
        private EKaronteEndpointStatus? _ekes;

        internal KaronteRoutingContext(ref KaronteContext kc) : base(ref kc) { _lck = new object(); }

        public Endpoint? GetEndpoint()
        {
            return KaronteContext.HttpContext.GetEndpoint();
        }

        public EKaronteEndpointStatus GetEndpointStatus()
        {
            lock (_lck)
            {
                if (_ekes == null)
                {
                    Endpoint? end = GetEndpoint();

                    if (end == null || end.DisplayName == null)
                        _ekes = EKaronteEndpointStatus.NotRegistered;
                    else if (end.Metadata == null || end.Metadata.Count < 1)
                        _ekes = EKaronteEndpointStatus.NotSupported;
                    else if (end.DisplayName.StartsWith("Fallback", StringComparison.OrdinalIgnoreCase))
                        _ekes = EKaronteEndpointStatus.OnFallback;
                    else if (end.DisplayName.Equals("/", StringComparison.OrdinalIgnoreCase))
                        _ekes = EKaronteEndpointStatus.OnRoot;
                    else
                        _ekes = EKaronteEndpointStatus.Registered;
                }

                return _ekes.Value;
            }
        }

        public T? GetLastEndpointMetadata<T>()
        {
            T[]? ta;
            GetEndpointMetadatas(out ta, true, true);
            return ArrayUtils.IsValidIndex(ta, 0) ? ta[0] : default(T);
        }

        public T? GetFirstEndpointMetadata<T>()
        {
            T[]? ta;
            GetEndpointMetadatas(out ta, true, false);
            return ArrayUtils.IsValidIndex(ta, 0) ? ta[0] : default(T);
        }

        public T[]? GetEndpointMetadata<T>(Boolean bOnAscendingOrder)
        {
            T[]? ta;
            GetEndpointMetadatas(out ta, false, !bOnAscendingOrder);
            return ta;
        }

        private void GetEndpointMetadatas<T>(out T[]? ta, Boolean bBreakOnFirst, Boolean bOnDescendingOrder)
        {
            Endpoint? ep = GetEndpoint();
            if (ep == null) { ta = null; return; }

            EndpointMetadataCollection emc = ep.Metadata;

            List<T> l = new List<T>(emc.Count);

            T? ti; int j = 0;
            for (int i = 0; i < emc.Count; i++)
            {
                j =
                    bOnDescendingOrder
                        ? emc.Count - 1 - i
                        : 0;

                ti = ObjectUtils.Cast<T>(emc[j]);
                if (ti == null) continue;
                l.Add(ti);
                if (bBreakOnFirst) break;
            }

            ta = l.Count > 0 ? l.ToArray() : null;
        }
    }
}