﻿using Kudos.Servers.KaronteModule.Attributes;
using Kudos.Servers.KaronteModule.Contexts;
using Kudos.Utils.Members;
using Microsoft.AspNetCore.Mvc;
using static Mysqlx.Crud.Find.Types;
using System.Collections.Generic;
using System.Reflection;
using System;
using Kudos.Servers.KaronteModule.Enums;
using Microsoft.AspNetCore.Http;

namespace Kudos.Servers.KaronteModule.Controllers
{
    [KaronteController]
    public abstract class AKaronteController
    {
        protected readonly KaronteContext KaronteContext;

        public AKaronteController(ref KaronteContext kc)
        {
            KaronteContext = kc;

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
    }
}