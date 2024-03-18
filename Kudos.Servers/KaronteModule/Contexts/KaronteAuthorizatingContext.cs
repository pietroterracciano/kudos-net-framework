using Kudos.Constants;
using Kudos.Servers.KaronteModule.Constants;
using Kudos.Servers.KaronteModule.Enums;
using System;

namespace Kudos.Servers.KaronteModule.Contexts
{
    public sealed class KaronteAuthorizatingContext : AKaronteChildContext
    {
        public String? RequestToken { get; internal set; }
        public EKaronteAuthorization RequestAuthorizationType { get; internal set; }
        public EKaronteAuthorization EndpointAuthorizationType { get; internal set; }

        internal KaronteAuthorizatingContext(ref KaronteContext kc) : base(ref kc) { }

        public Boolean IsEndpointAuthorized()
        {
            return RequestAuthorizationType.HasFlag(EndpointAuthorizationType);
        }

        //public KaronteAuthorizationContext(String? s)
        //{
        //    if (s == null) s = String.Empty;
        //    else s = s.Trim();

        //    if (Normalize(ref s, EKaronteAuthorization.Access))
        //        Type = EKaronteAuthorization.Access;
        //    else if (Normalize(ref s, EKaronteAuthorization.Bearer))
        //        Type = EKaronteAuthorization.Bearer;
        //    else
        //        Type = EKaronteAuthorization.None;

        //    Token = s;
        //}

        //private static Boolean Normalize(ref String s, EKaronteAuthorization e)
        //{
        //    String? s1;
        //    switch(e)
        //    {
        //        case EKaronteAuthorization.Access:
        //            s1 = CKaronteAuthorization.Access;
        //            break;
        //        case EKaronteAuthorization.Bearer:
        //            s1 = CKaronteAuthorization.Bearer;
        //            break;
        //        default:
        //            s1 = null;
        //            break;
        //    }

        //    if (s1 == null || !s.StartsWith(s1, StringComparison.OrdinalIgnoreCase)) return false;
        //    s = s.Substring(s1.Length).Trim(); return true;//while (s.StartsWith(CCharacter.Space)) s = s.Substring(1); return true;
        //}
    }
}
