
using System;
using System.Collections.Generic;
using Kudos.Servers.KaronteModule.Attributes;
using Kudos.Servers.KaronteModule.Descriptors.Routes;
using Kudos.Servers.KaronteModule.Enums;
using Kudos.Types;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Metadata;
using Microsoft.AspNetCore.Routing;
using Org.BouncyCastle.Pqc.Crypto.Lms;

namespace Kudos.Servers.KaronteModule.Contexts
{
    public sealed class KaronteCapabilitingContext : AKaronteChildContext
    {
        private static readonly Object
            _o;

        static KaronteCapabilitingContext()
        {
            _o = new object();
        }

        private Boolean
            _bIsEndpointAnalyzed,
            _bIsCapabilityRequired;

        private EKaronteCapabilityValidationRule?
            _ekcvr;

        private Metas
            _m;

        internal KaronteCapabilitingContext(ref KaronteContext kc) : base(ref kc) { }

        public Boolean IsCapabilityRequired() { _AnalyzeEndpoint(); return _bIsCapabilityRequired; }

        private void _AnalyzeEndpoint()
        {
            if (_bIsEndpointAnalyzed) return;
            _bIsEndpointAnalyzed = true;

            KaronteNoCapabilityAttribute?
                knca = KaronteContext.RoutingContext.GetLastEndpointMetadata<KaronteNoCapabilityAttribute>();

            if (knca != null)
            {
                _bIsCapabilityRequired = false;
                return;
            }

            KaronteCapabilityAttribute?
                kca = KaronteContext.RoutingContext.GetLastEndpointMetadata<KaronteCapabilityAttribute>();

            if (kca == null)
            {
                _bIsCapabilityRequired = false;
                return;
            }

            _ekcvr = kca.ValidationRule;

            if (kca.HasRoutes)
            {
                _m = new Metas(kca.Routes.Count, StringComparison.OrdinalIgnoreCase);
                foreach (String s in kca.Routes) _m.Set(s, _o);
                _bIsCapabilityRequired = true;
                return;
            }

            KaronteMethodRouteDescriptor?
                kmrd = KaronteContext.RoutingContext.GetEndpointMethodRouteDescriptor();

            if (kmrd == null)
            {
                _bIsCapabilityRequired = false;
                return;
            }

            _m = new Metas(2, StringComparison.OrdinalIgnoreCase);
            _m.Set(kmrd.ResolvedFullPattern, _o);
            _m.Set(kmrd.FullHashKey, _o);
            _bIsCapabilityRequired = true;
        }

        public Boolean HasCapability(params String[] sa)
        {
            if (!IsCapabilityRequired())
                return true;

            Int32
                i = _m != null ? _m.Count : 0;

            if (i < 1)
                return true;

            Int32
                j =
                    _ekcvr == EKaronteCapabilityValidationRule.NeedAllValidRoutes
                        ? i
                        : 1,
                k =
                    sa != null
                        ? sa.Length
                        : 0;

            if (j > k)
                return false;

            Int32
                m = 0;

            Object? on;

            for (int n = 0; n < sa.Length; n++)
            {
                on = _m.Get(sa[n]);
                if (on != _o) continue;
                m += 1;
                if (m >= j)
                    return true;
            }

            return false;
        }


        //        KaronteAttributingContext? kac;
        //        kc.RequestObject<KaronteAttributingContext>(CKaronteKey.Attributing, out kac);

        //            KaronteCapabilityAttribute? kca = kac.GetAttribute<KaronteCapabilityAttribute>();

        //        String[]? sa;
        //        EKaronteCapabilityValidationRule? ekcvr;

        //            if (kca == null)
        //            {
        //                ekcvr = null;
        //                sa = null;
        //            }
        //            else
        //            {
        //                ekcvr = kca.ValidationRule;

        //                if (!kca.HasRoutes)
        //                {
        //                    KaronteMethodRouteDescriptor? kmrd =
        //                        kc.RoutingContext != null
        //                            ? kc.RoutingContext.MethodRouteDescriptor
        //                            : null;

        //                    if (kmrd == null)
        //                    {
        //                        Endpoint? end = kc.HttpContext.GetEndpoint();
        //    KaronteMethodRouteDescriptor.Request(ref end, out kmrd);
        //                    }

        //sa =
        //    kmrd != null
        //    ?
        //        new string[]
        //        {
        //                                kmrd.ResolvedFullPattern,
        //                                kmrd.FullHashKey
        //        }
        //    :
        //        null;

        //                }
        //                else
        //    sa = kca.Routes;
        //            }


        //internal
        //    KaronteCapabilitingContext
        //    (
        //        ref EKaronteCapabilityValidationRule? ekcvr,
        //        ref String[]? sa,
        //        ref KaronteContext kc
        //    )
        //:
        //    base
        //    (
        //        ref kc
        //    )
        //{
        //    _ekcvr = ekcvr;

        //    if (sa == null) return;

        //    _hss = new HashSet<string>(sa.Length);

        //    String? sai;
        //    for(int i=0; i<sa.Length; i++)
        //    {
        //        sai = Normalize(sa[i]);
        //        if (sai == null) return;
        //        _hss.Add(sai);
        //    }
        //}

        //private String? Normalize(String? s)
        //{
        //    return !String.IsNullOrWhiteSpace(s) ? s.ToLower().Trim() : null;
        //}

        //public Boolean HasPermission(params String[]? sa)
        //{
        //    if (_hss == null || _hss.Count < 1 || _ekcvr == null)
        //        return true;
        //    else if (sa == null)
        //        return false;

        //    Int32
        //        j = 0,
        //        k = _ekcvr == EKaronteCapabilityValidationRule.NeedAllValidRoutes
        //            ? _hss.Count
        //            : 1;

        //    if (k > sa.Length)
        //        return false;

        //    String? sai;
        //    for(int i=0; i< sa.Length;i++)
        //    {
        //        sai = Normalize(sa[i]);
        //        if (sai == null || !_hss.Contains(sai)) continue;
        //        j += 1;
        //        if (j >= k)
        //            return true;
        //    }

        //    return false;
        //}
    }
}

