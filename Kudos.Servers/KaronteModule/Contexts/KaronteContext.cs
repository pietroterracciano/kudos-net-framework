﻿using Kudos.Servers.KaronteModule.Controllers;
using Kudos.Types;
using Kudos.Utils.Members;
using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Kudos.Servers.KaronteModule.Attributes;
using Kudos.Servers.KaronteModule.Enums;
using Kudos.Utils;

namespace Kudos.Servers.KaronteModule.Contexts
{
    [KaronteService(EKaronteServiceType.Scoped)]
    public sealed class KaronteContext
    {
        private readonly Object _lck0, _lck1;
        private readonly Metas _mts;

        internal Type[]? RegisteredServices;

        public HttpContext HttpContext { get; internal set; }
        public KaronteResponsingContext? ResponsingContext { get; internal set; }
        public KaronteRoutingContext? RoutingContext { get; internal set; }
        public KaronteDatabasingContext? DatabasingContext { get; internal set; }
        public KaronteAuthorizatingContext? AuthorizatingContext { get; internal set; }
        public KaronteAuthenticatingContext? AuthenticatingContext { get; internal set; }
        public KaronteJSONingContext? JSONingContext { get; internal set; }

        public KaronteContext()
        {
            _lck0 = new Object();
            _lck1 = new Object();
            _mts = new Metas();
        }

        private void throwRequiredServiceException(Type? t)
        {
            throw new InvalidOperationException
            (
                "Required Service " + (t != null ? t.Name + " " : String.Empty) + "not registered with KaronteScopedAttribute || KaronteSingletonAttribute || KaronteTransientAttribute"
            );
        }

        private void throwRequiredControllerException(Type? t)
        {
            throw new InvalidOperationException
            (
                "Required Controller " + (t != null ? t.Name + " " : String.Empty) + "can't istantiate, there is a Service not registered with KaronteScopedAttribute || KaronteSingletonAttribute || KaronteTransientAttribute"
            );
        }

        public ServiceType GetRequiredService<ServiceType>()
        {
            Type 
                t = typeof(ServiceType);

            ServiceType?
                srv = ObjectUtils.Cast<ServiceType>(GetService(t));

            if (srv == null)
                throwRequiredServiceException(t);

            return srv;
        }

        private Object GetRequiredService(Type? t)
        {
            Object?
                srv = GetService(t);

            if (srv == null)
                throwRequiredServiceException(t);

            return srv;
        }

        public ServiceType? GetService<ServiceType>()
        {
            return ObjectUtils.Cast<ServiceType>(GetService(typeof(ServiceType)));
        }

        private Object? GetService(Type? t)
        {
            lock (_lck0)
            {
                if (t == null) 
                    return null;

                Object? 
                    cnt = _mts.Get(t.FullName);

                if (cnt != null) 
                    return cnt;

                HttpContext
                    httpc = this.HttpContext;

                if (httpc == null)
                    return null;

                Object?
                    o = HttpContext.RequestServices.GetService(t);

                _mts.Set(t.FullName, o);
                return o;
            }
        }

        public ControllerType GetRequiredController<ControllerType>()
        {
            Type
                t = typeof(ControllerType);

            ControllerType?
                cnt = ObjectUtils.Cast<ControllerType>(GetController(t));

            if (cnt == null)
                throwRequiredControllerException(t);

            return cnt;
        }

        private Object GetRequiredController(Type? t)
        {
            Object?
                cnt = GetController(t);

            if (cnt == null)
                throwRequiredControllerException(t);

            return cnt;
        }

        public ControllerType? GetController<ControllerType>()
        {
            return ObjectUtils.Cast<ControllerType>(GetController(typeof(ControllerType)));
        }

        private Object? GetController(Type? t)//, params Object[]? os)
        {
            lock (_lck1)
            {
                if (t == null)
                    return null;

                Object? 
                    cnt = _mts.Get(t.FullName);

                if (cnt != null) 
                    return cnt;

                Type[]? 
                    rss = RegisteredServices;

                HttpContext 
                    httpc = this.HttpContext;

                if (httpc == null)
                    return null;

                List<Object?> l = new List<Object?>();
                l.Add(this);
                l.Add(httpc);

                if (rss != null)
                    for (int i = 0; i < rss.Length; i++)
                    {
                        l.Add(GetService(rss[i]));
                    }

                //if (os != null)
                //    l.AddRange(os);

                ConstructorInfo[]? 
                    cis = ConstructorUtils.Gets(t);

                if (cis == null)
                    return null;

                Object?
                    cti = null;

                for (int i = 0; i < cis.Length; i++)
                {
                    Object[]? os1;
                    if (!ConstructorUtils.CheckAndGenerateCompatibleParametersArray(cis[i], out os1, l.ToArray()))
                        continue;

                    cti = ConstructorUtils.Invoke(cis[i], os1);
                    break;
                }

                _mts.Set(t.FullName, cti);
                return cti;
            }
        }
    }
}
