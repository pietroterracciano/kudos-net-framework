
using Kudos.Servers.KaronteModule.Attributes;
using Kudos.Servers.KaronteModule.Descriptors.Routes;
using Kudos.Servers.KaronteModule.Enums;

namespace Kudos.Servers.KaronteModule.Contexts
{
    public sealed class KaronteCapabilitingContext : AKaronteChildContext
    {
        internal KaronteCapabilitingContext(ref KaronteContext kc) : base(ref kc) { }


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

