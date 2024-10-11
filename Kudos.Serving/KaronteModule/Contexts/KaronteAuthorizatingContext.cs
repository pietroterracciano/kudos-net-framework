using Kudos.Constants;
using Kudos.Serving.KaronteModule.Constants;
using Kudos.Utils;
using Kudos.Utils.Collections;
using System;
using Kudos.Serving.KaronteModule.Enums;
using Kudos.Serving.KaronteModule.Attributes;

namespace Kudos.Serving.KaronteModule.Contexts
{
    public sealed class KaronteAuthorizatingContext : AKaronteChildContext
    {
        private Boolean _bIsRequestAnalyzed, _bIsEndpointAnalyzed;
        private EKaronteAuthorizationType? _ekatr, _ekatep;
        private String? _srat;

        internal KaronteAuthorizatingContext(ref KaronteContext kc) : base(ref kc) { }

        public EKaronteAuthorizationType? GetRequestAuthorizationType()
        {
            _AnalyzeRequest(); return _ekatr;
        }

        public Boolean HasRequestAuthorizationType()
        {
            return GetRequestAuthorizationType() != null;
        }

        public String? GetRequestAuthorizationToken()
        {
            _AnalyzeRequest(); return _srat;
        }

        public Boolean HasRequestAuthorizationToken()
        {
            return GetRequestAuthorizationToken() != null;
        }

        public EKaronteAuthorizationType? GetEndpointAuthorizationType()
        {
            _AnalyzeEndpoint(); return _ekatep;
        }

        public Boolean HasEndpointAuthorizationType()
        {
            return GetEndpointAuthorizationType() != null;
        }

        public Boolean IsPreAuthorized()
        {
            EKaronteAuthorizationType?
                ekatep = GetEndpointAuthorizationType();

            if (ekatep == null)
                return true;

            EKaronteAuthorizationType?
                ekatr = GetRequestAuthorizationType();

            return
                ekatr != null
                && ekatr.Value.HasFlag(ekatep);
        }

        public Boolean IsAuthorizationRequired()
        {
            return GetEndpointAuthorizationType() != null;
        }

        private void _AnalyzeEndpoint()
        {
            if (_bIsEndpointAnalyzed) return;
            _bIsEndpointAnalyzed = true;

            KaronteNoAuthorizationAttribute?
                knaa = KaronteContext.RoutingContext.GetLastEndpointMetadata<KaronteNoAuthorizationAttribute>();

            if (knaa != null) return;

            KaronteAuthorizationAttribute?
                kaa = KaronteContext.RoutingContext.GetLastEndpointMetadata<KaronteAuthorizationAttribute>();

            if (kaa == null) return;

            _ekatep = kaa.AuthorizationType;
        }

        private void _AnalyzeRequest()
        {
            if (_bIsRequestAnalyzed) return;
            _bIsRequestAnalyzed = true;

            String?
                s = KaronteContext.HeadingContext
                        .GetSignificativeRequestHeaderValue(CKaronteHttpHeader.Authorization);

            OnAuthorizationDataFetch(ref s, out _srat, out _ekatr);
        }

        private static void OnAuthorizationDataFetch(ref String? s, out String? st, out EKaronteAuthorizationType? e)
        {
            if (s == null)
            {
                st = null;
                e = null;
                return;
            }

            String[]
                sa = CRegex.Spaces1toN.Split(s);

            if (!ArrayUtils.IsValidIndex(sa, 1))
            {
                st = null;
                e = null;
                return;
            }

            e = EnumUtils.Parse<EKaronteAuthorizationType>(sa[0]);

            if (!EnumUtils.IsValid<EKaronteAuthorizationType>(e))
            {
                st = null;
                e = null;
                return;
            }

            st = sa[1];

            if (String.IsNullOrWhiteSpace(sa[1]))
            {
                st = null;
                e = null;
                return;
            }
        }
    }
}