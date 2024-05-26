using Kudos.Crypters.KryptoModule.SymmetricModule;
using Kudos.Databases.Chainers;
using Kudos.Databases.Chains;
using Kudos.Databases.Interfaces.Chains;
using Kudos.Reflection.Utils;
using Kudos.Servers.KaronteModule.Attributes;
using Kudos.Servers.KaronteModule.Constants;
using Kudos.Servers.KaronteModule.Contexts;
using Kudos.Servers.KaronteModule.Descriptors.Routes;
using Kudos.Servers.KaronteModule.Enums;
using Kudos.Servers.KaronteModule.Middlewares;
using Kudos.Servers.KaronteModule.Options;
using Kudos.Servers.KaronteModule.Services;
using Kudos.Types;
using Kudos.Utils;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Kudos.Servers.KaronteModule
{
    public static class Karonte
    {
        private static Type[]? __a;
        private static HashSet<String> __hsRegisteredApplications;
        private static readonly Metas __mRegisteredServices;

        static Karonte()
        {
            __mRegisteredServices = new Metas(StringComparison.OrdinalIgnoreCase);
            __hsRegisteredApplications = new HashSet<string>();
            //__f0 = ab => new KaronteAuthorizationBuilder(ref ab);
            //__f1 = ab => new KaronteDatabasingBuilder(ref ab);
            //__bIsCoreAdded = __bIsJSONingAdded = false;
            //__d0 = new Dictionary<IApplicationBuilder, KaronteApplicationBuilderContext>();
            //__d1 = new Dictionary<IApplicationBuilder, Dictionary<Type, IKaronteBuilder>>();
            //__d2 = new Dictionary<IApplicationBuilder, IKaronteDatabaseHandlerBuilder>();
        }

        private static void RegisterService(String? s) { RegisterService(s, null); }
        private static void RegisterService(String? s, AKaronteService? ks) { __mRegisteredServices.Set(s, ks); }
        private static Boolean IsServiceRegistered(String? s) { return __mRegisteredServices.Contains(s); }

        private static T RequestRegisteredService<T>(String? s)
        {
            T? t = __mRegisteredServices.Get<T>(s);
            if(t == null) throw new InvalidOperationException();
            return t;
        }

        private static Boolean RegisterApplication(String? s)
        {
            if (s == null) return false;
            __hsRegisteredApplications.Add(s); return true;
        }

        private static Boolean IsApplicationRegistered(String? s)
        {
            return
                s != null
                && __hsRegisteredApplications.Contains(s);
        }

        #region public static IServiceCollection AddKaronteCore(...)

        public static IServiceCollection AddKaronteCore(this IServiceCollection sc)
        {
            if (IsServiceRegistered(CKaronteKey.Core)) return sc;
            RegisterService(CKaronteKey.Core);

            sc
                .TryAddScoped<KaronteContext>();
            sc
                .AddRouting()
                .AddControllers();

            Assembly?
                ass = Assembly.GetEntryAssembly();

            if (ass == null)
                return sc;

                
            Type[]? ts1;
            try { ts1 = ass.GetTypes(); } catch { ts1 = null; }
            if (ts1 == null)
                return sc;

            List<Type> l = new List<Type>();
            for (int i = 0; i < ts1.Length; i++)
            {
                KaronteServiceAttribute? 
                    ksa = ReflectionUtils.GetCustomAttribute<KaronteServiceAttribute>(ts1[i], true);

                if (ksa == null)
                    continue;

                switch(ksa.Type)
                {
                    case EKaronteServiceType.Scoped:
                        sc.TryAddScoped(ts1[i]);
                        break;
                    case EKaronteServiceType.Transient:
                        sc.TryAddTransient(ts1[i]);
                        break;
                    case EKaronteServiceType.Singleton:
                        sc.TryAddSingleton(ts1[i]);
                        break;
                }

                l.Add(ts1[i]);
            }

            __a = l.ToArray();

            return sc;
        }

        #endregion

        #region public static IServiceCollection AddKaronteJSONing(..)

        public static IServiceCollection AddKaronteJSONing(this IServiceCollection sc, Action<KaronteJSONingService>? act)
        {
            if (!IsServiceRegistered(CKaronteKey.Core))
                throw new InvalidOperationException();

            KaronteJSONingService kjsons;

            if (!IsServiceRegistered(CKaronteKey.JSONing))
            {
                kjsons = new KaronteJSONingService(ref sc);
                RegisterService(CKaronteKey.JSONing, kjsons);
                sc.TryAddSingleton<KaronteJSONingService>(kjsons);
            }
            else
                kjsons = RequestRegisteredService<KaronteJSONingService>(CKaronteKey.JSONing);

            if (act != null)
                act.Invoke(kjsons);

            return sc;
        }

        #endregion

        #region public static IServiceCollection AddKaronteCrypting(..)

        public static IServiceCollection AddKaronteCrypting(this IServiceCollection sc, Action<KaronteCryptingService>? act)
        {
            if (!IsServiceRegistered(CKaronteKey.Core))
                throw new InvalidOperationException();

            KaronteCryptingService kcs;

            if (!IsServiceRegistered(CKaronteKey.Crypting))
            {
                kcs = new KaronteCryptingService(ref sc);
                RegisterService(CKaronteKey.Crypting, kcs);
                sc.TryAddSingleton<KaronteCryptingService>(kcs);
            }
            else
                kcs = RequestRegisteredService<KaronteCryptingService>(CKaronteKey.Crypting);

            if (act != null)
                act.Invoke(kcs);

            return sc;
        }

        #endregion

        #region public static IServiceCollection AddKaronteDatabasing(..)

        public static IServiceCollection AddKaronteDatabasing(this IServiceCollection sc, Action<KaronteDatabasingService>? act)
        {
            if (!IsServiceRegistered(CKaronteKey.Core))
                throw new InvalidOperationException();

            KaronteDatabasingService kdbs;

            if (!IsServiceRegistered(CKaronteKey.Databasing))
            {
                kdbs = new KaronteDatabasingService(ref sc);
                RegisterService(CKaronteKey.Databasing, kdbs);
                sc.TryAddSingleton<KaronteDatabasingService>(kdbs);
            }
            else
                kdbs = RequestRegisteredService<KaronteDatabasingService>(CKaronteKey.Databasing);

            if (act != null)
                act.Invoke(kdbs);

            return sc;
        }

        #endregion

        #region public static IApplicationBuilder UseKaronteCore(...)

        public static IApplicationBuilder UseKaronteCore(this IApplicationBuilder ab)
        {
            if (!IsServiceRegistered(CKaronteKey.Core))
                throw new InvalidOperationException();
            else if (IsApplicationRegistered(CKaronteKey.Core))
                return ab;

            RegisterApplication(CKaronteKey.Core);

            return 
                ab.Use
                (
                    async (httpc, rd) =>
                    {
                        KaronteContext kc = httpc.RequestServices.GetRequiredService<KaronteContext>();
                        kc.RegisteredServices = __a;
                        kc.HttpContext = httpc;
                        await rd.Invoke(httpc);
                    }
                );
        }

        #endregion

        #region public static IApplicationBuilder UseKaronteRouting(...)

        public static IApplicationBuilder UseKaronteRouting<RoutingMiddlewareType>(this IApplicationBuilder ab)
            where RoutingMiddlewareType : AKaronteRoutingMiddleware
        {
            if (!IsServiceRegistered(CKaronteKey.Core))
                throw new InvalidOperationException();
            else if (IsApplicationRegistered(CKaronteKey.Routing))
                return ab;

            RegisterApplication(CKaronteKey.Routing);

            //IKaronteAuthorizationBuilder kab;
            //if( FetchAuthorizationBuilder(ref ab, out kab) == EKaronteObjectStatus.New )
            return
                ab
                    .UseRouting()
                    .UseMiddleware<RoutingMiddlewareType>();
        }

        #endregion

        #region public static IApplicationBuilder UseKaronteCrypting(...)

        public static IApplicationBuilder UseKaronteCrypting(this IApplicationBuilder ab)
        {
            if (!IsServiceRegistered(CKaronteKey.Crypting))
                throw new InvalidOperationException();
            else if (IsApplicationRegistered(CKaronteKey.Crypting))
                return ab;

            RegisterApplication(CKaronteKey.Crypting);

            return
                ab
                    .UseMiddleware<KaronteCryptingMiddleware>();
        }

        #endregion

        #region public static IApplicationBuilder UseKaronteAuthorizating(...)

        public static IApplicationBuilder UseKaronteAuthorizating<AuthorizatingMiddlewareType, AuthorizatingAttributeType, EAuthorizationType>(this IApplicationBuilder ab)
            where AuthorizatingMiddlewareType : AKaronteAuthorizatingMiddleware<AuthorizatingAttributeType, EAuthorizationType>
            where AuthorizatingAttributeType : AKaronteAuthorizatingAttribute<EAuthorizationType>
            where EAuthorizationType : Enum
        {
            if (!IsServiceRegistered(CKaronteKey.Core))
                throw new InvalidOperationException();
            else if (IsApplicationRegistered(CKaronteKey.Authorizating))
                return ab;

            RegisterApplication(CKaronteKey.Authorizating);


            //IKaronteAuthorizationBuilder kab;
            //if( FetchAuthorizationBuilder(ref ab, out kab) == EKaronteObjectStatus.New )
            return
                ab
                    .UseMiddleware<AuthorizatingMiddlewareType>();
        }

        #endregion

        #region public static IApplicationBuilder UseKaronteDatabasing(...)

        public static IApplicationBuilder UseKaronteDatabasing<DatabasingMiddlewareType>(this IApplicationBuilder ab)
            where DatabasingMiddlewareType : AKaronteDatabasingMiddleware
        {
            if (!IsServiceRegistered(CKaronteKey.Databasing))
                throw new InvalidOperationException();
            else if (IsApplicationRegistered(CKaronteKey.Databasing))
                return ab;

            RegisterApplication(CKaronteKey.Databasing);

            return
                ab
                    .UseMiddleware<DatabasingMiddlewareType>();
        }

        #endregion

        #region public static IApplicationBuilder UseKaronteJSONing(...)

        public static IApplicationBuilder UseKaronteJSONing(this IApplicationBuilder ab)
        {
            if (!IsServiceRegistered(CKaronteKey.JSONing))
                throw new InvalidOperationException();
            else if (IsApplicationRegistered(CKaronteKey.JSONing))
                return ab;

            RegisterApplication(CKaronteKey.JSONing);

            return ab.UseMiddleware<KaronteJSONingMiddleware>();
        }

        #endregion

        #region public static IApplicationBuilder UseKaronteResponsing(...)

        public static IApplicationBuilder UseKaronteResponsing<ResponsingMiddlewareType, NonActionResultType>(this IApplicationBuilder ab)
            where ResponsingMiddlewareType : AKaronteResponsingMiddleware<NonActionResultType>
        {
            if (!IsServiceRegistered(CKaronteKey.Core))
                throw new InvalidOperationException();
            else if (IsApplicationRegistered(CKaronteKey.Responsing))
                return ab;

            RegisterApplication(CKaronteKey.Responsing);


            return ab.UseMiddleware<ResponsingMiddlewareType>();
        }

        #endregion

        #region public static IApplicationBuilder UseKaronteAuthenticating(...)

        public static IApplicationBuilder UseKaronteAuthenticating<AuthenticatingMiddlewareType, ObjectType>(this IApplicationBuilder ab)
            where
                AuthenticatingMiddlewareType : AKaronteAuthenticatingMiddleware<ObjectType>
        {
            if (!IsServiceRegistered(CKaronteKey.Core))
                throw new InvalidOperationException();
            else if (IsApplicationRegistered(CKaronteKey.Authenticating))
                return ab;

            RegisterApplication(CKaronteKey.Authenticating);


            return
                ab
                    .UseMiddleware<AuthenticatingMiddlewareType>();
        }

        #endregion

        public static IEndpointRouteBuilder MapKaronteControllers(this IEndpointRouteBuilder erb)
        {
            __MapKaronteControllers(erb/*, EKaronteAuthorization.None*/);
            return erb;
        }

        private static void __MapKaronteControllers(IEndpointRouteBuilder erb/*, EKaronteAuthorization e*/)
        {
            Assembly? ass = Assembly.GetEntryAssembly();

            if (ass == null) return;

            Type[]? a;
            try { a = ass.GetTypes(); } catch { a = null; }

            __MapKaronteControllers(ref erb, /*ref e,*/ ref a);
        }

        private static void __MapKaronteControllers
        (
            ref IEndpointRouteBuilder erb,
            //ref EKaronteAuthorization e,
            ref Type[]? a
        )
        {
            if (a == null)
                return;

            for (int i = 0; i < a.Length; i++)
                __MapKaronteControllers(ref erb, /*ref e,*/ a[i]);
        }

        private static void __MapKaronteControllers
        (
            ref IEndpointRouteBuilder erb,
            //ref EKaronteAuthorization e,
            Type? t
        )
        {
            KaronteControllerAttribute?
                kca = ReflectionUtils.GetCustomAttribute<KaronteControllerAttribute>(t, true);

            if (kca == null)
                return;

            KaronteRouteDescriptor[]? krds0;
            if (!__MapKaronteControllers(/*ref e,*/ t, out krds0))
                return;

            KaronteRouteDescriptor[]? krds1;
            if (!__MapKaronteControllers(/*ref e, */ReflectionUtils.GetMethods(t), out krds1))
                return;

            int k = 0;
            String s0i, s1i, s0j, s1j;
            for (int i = 0; i < krds1.Length; i++)
            {
                s0i = krds1[i].ResolvePattern(ref kca);
                s1i = krds1[i].ResolveMemberName();

                for (int j = 0; j < krds0.Length; j++)
                {
                    s0j = krds0[j].ResolvePattern(ref kca);
                    s1j = krds0[j].ResolveMemberName();

                    erb.MapControllerRoute
                    (
                        "C:" + t.FullName + "|R:" + k,
                        s0j + s0i,
                        new { controller = s1j, action = s1i }
                    );

                    k++;
                }
            }
        }

        private static Boolean __MapKaronteControllers
        (
            //ref EKaronteAuthorization e,
            MemberInfo[] ? mis,
            out KaronteRouteDescriptor[]? krds
        )
        {
            if(mis == null)
            {
                krds = null;
                return false;
            }

            List<KaronteRouteDescriptor>
                l = new List<KaronteRouteDescriptor>(mis.Length);

            KaronteRouteDescriptor[]? krdsi;
            for (int i = 0; i < mis.Length; i++)
            {
                if (!__MapKaronteControllers(/*ref e,*/ mis[i], out krdsi)) continue;
                l.AddRange(krdsi);
            }

            krds = l.ToArray();

            return krds.Length > 0;
        }

        private static Boolean __MapKaronteControllers
        (
            //ref EKaronteAuthorization e0,
            MemberInfo mi,
            out KaronteRouteDescriptor[]? krds
        )
        {
            NonActionAttribute?
                naa = ReflectionUtils.GetCustomAttribute<NonActionAttribute>(mi, true);

            if (naa != null)
            {
                krds = null;
                return false;
            }

            //KaronteAuthorizationAttribute?
            //    kaa = MemberUtils.GetAttribute<KaronteAuthorizationAttribute>(mi, true);

            //EKaronteAuthorization
            //    e2 =
            //        kaa != null
            //            ? kaa.Value
            //            : EKaronteAuthorization.None;

            //if(!e0.HasFlag(e2))
            //{
            //    krds = null;
            //    return false;
            //}

            return __MapKaronteControllers(ref mi, ReflectionUtils.GetCustomAttributes<RouteAttribute>(mi, true), out krds);
        }

        private static Boolean __MapKaronteControllers
        (
            ref MemberInfo mi,
            RouteAttribute[]? ras,
            out KaronteRouteDescriptor[]? krds
        )
        {
            List<KaronteRouteDescriptor>
                l;

            if (ras != null)
            {
                l = new List<KaronteRouteDescriptor>(ras.Length);

                KaronteRouteDescriptor? krdi;
                for (int i = 0; i < ras.Length; i++)
                {
                    if (!__MapKaronteControllers(ref mi, ref ras[i], out krdi)) continue;
                    l.Add(krdi);
                }
            }
            else
                l = new List<KaronteRouteDescriptor>();

            if (l.Count < 1)
                switch(mi.MemberType)
                {
                    case MemberTypes.TypeInfo:
                        l.Add(new KaronteRouteDescriptor(mi, CKaronteRouteKey.Controller_Version+ "/"+ CKaronteRouteKey.Controller));
                        break;
                    case MemberTypes.Method:
                        l.Add(new KaronteRouteDescriptor(mi, CKaronteRouteKey.Action));
                        break;
                }

            krds = l.ToArray();

            return krds.Length > 0;
        }

        private static Boolean __MapKaronteControllers
        (
            ref MemberInfo mi,
            ref RouteAttribute? ra,
            out KaronteRouteDescriptor? krd
        )
        {
            String? s;
            Boolean b = __Configure(ref ra, out s);
            krd = b ? new KaronteRouteDescriptor(mi, s) : null;
            return b;
        }

        private static Boolean __Configure
        (
            ref RouteAttribute? ra,
            out String? s
        )
        {
            Boolean b = ra != null && !String.IsNullOrWhiteSpace(ra.Template);
            s = b ? ra.Template : null;
            return b;
        }
    }
}
