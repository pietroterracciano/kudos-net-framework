using System;
using System.Threading.Tasks;
using Kudos.Servers.KaronteModule.Attributes;
using Kudos.Servers.KaronteModule.Contexts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Kudos.Servers.KaronteModule.Controllers
{
    [KaronteController]
    public abstract class AKaronteController
    {
        protected readonly KaronteContext KaronteContext;
        //protected readonly KaronteDatabasingContext? KaronteDatabasingContext;
        //protected readonly Boolean HasKaronteDatabasingContext;
        //protected readonly KaronteJSONingContext? KaronteJSONingContext;
        //protected readonly Boolean HasJSONingContext;
        //protected readonly KaronteAuthenticatingContext? KaronteAuthenticatingContext;
        //protected readonly Boolean HasKaronteAuthenticatingContext;
        //protected readonly KaronteAuthorizatingContext? KaronteAuthorizatingContext;
        //protected readonly Boolean HasKaronteAuthorizatingContext;
        //protected readonly KaronteCryptingContext? KaronteCryptingContext;
        //protected readonly Boolean HasKaronteCryptingContext;
        //protected readonly KaronteResponsingContext? KaronteResponsingContext;
        //protected readonly Boolean HasKaronteResponsingContext;
        //protected readonly HttpContext HttpContext;

        public AKaronteController(ref KaronteContext kc)
        {
            //HttpContext = kc.HttpContext;
            KaronteContext = kc;
            //HasKaronteDatabasingContext = (KaronteDatabasingContext = kc.DatabasingContext) != null;
            //HasJSONingContext = (KaronteJSONingContext = kc.JSONingContext) != null;
            //HasKaronteAuthenticatingContext = (KaronteAuthenticatingContext = kc.AuthenticatingContext) != null;
            //HasKaronteAuthorizatingContext = (KaronteAuthorizatingContext = kc.AuthorizatingContext) != null;
            //HasKaronteCryptingContext = (KaronteCryptingContext = kc.CryptingContext) != null;
            //HasKaronteResponsingContext = (KaronteResponsingContext = kc.ResponsingContext) != null;

            //if
            //(
            //    KaronteContext.RoutingContext == null
            //    || KaronteContext.RoutingContext.Type == EKaronteRoute.OnEnd
            //)
            //    return;

            //EKaronteRoute ekr = EKaronteRoute.OnEnd;
            //Endpoint? end = KaronteContext.RoutingContext.Endpoint;
            //KaronteContext.RoutingContext = new KaronteRoutingContext(ref kc, ref end, ref ekr);
        }

        [NonAction]
        public override string? ToString()
        {
            return base.ToString();
        }

        [NonAction]
        public override bool Equals(object? obj)
        {
            return base.Equals(obj);
        }

        [NonAction]
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        [NonAction]
        public new Type GetType()
        {
            return base.GetType();
        }
    }
}