using Kudos.Reflection.Utils;
using Kudos.Servers.KaronteModule.Attributes;
using Kudos.Servers.KaronteModule.Constants;
using Kudos.Servers.KaronteModule.Contexts;
using Kudos.Servers.KaronteModule.Contexts.Options;
using Kudos.Servers.KaronteModule.Descriptors.Routes;
using Kudos.Servers.KaronteModule.Enums;
using Kudos.Servers.KaronteModule.Middlewares;
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
        private static HashSet<String> __hsConsumedServices, __hsConsumedApplications;

        static Karonte()
        {
            __hsConsumedServices = new HashSet<string>();
            __hsConsumedApplications = new HashSet<string>();
            //__f0 = ab => new KaronteAuthorizationBuilder(ref ab);
            //__f1 = ab => new KaronteDatabasingBuilder(ref ab);
            //__bIsCoreAdded = __bIsJSONingAdded = false;
            //__d0 = new Dictionary<IApplicationBuilder, KaronteApplicationBuilderContext>();
            //__d1 = new Dictionary<IApplicationBuilder, Dictionary<Type, IKaronteBuilder>>();
            //__d2 = new Dictionary<IApplicationBuilder, IKaronteDatabaseHandlerBuilder>();
        }

        private static Boolean ConsumeService(String? s)
        {
            if (s == null) return false;
            __hsConsumedServices.Add(s); return true;
        }

        private static Boolean IsServiceConsumed(String? s)
        {
            return
                s != null
                && __hsConsumedServices.Contains(s);
        }

        private static Boolean ConsumeApplication(String? s)
        {
            if (s == null) return false;
            __hsConsumedApplications.Add(s); return true;
        }

        private static Boolean IsApplicationConsumed(String? s)
        {
            return
                s != null
                && __hsConsumedApplications.Contains(s);
        }

        #region public static IServiceCollection AddKaronteCore(...)

        public static IServiceCollection AddKaronteCore(this IServiceCollection sc)
        {
            if (IsServiceConsumed(CKaronteKey.Core)) return sc;
            ConsumeService(CKaronteKey.Core);

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

        public static IServiceCollection AddKaronteJSONing(this IServiceCollection sc, JsonSerializerOptions? jso)
        {
            if (IsServiceConsumed(CKaronteKey.JSONing)) return sc;
            ConsumeService(CKaronteKey.JSONing);

            if (jso == null)
                jso
                     = new JsonSerializerOptions()
                     {
                         PropertyNameCaseInsensitive = false,
                         DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
                         Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
                         IncludeFields = true
                     };

            sc.TryAddSingleton<KaronteJSONingOptionsContext>(new KaronteJSONingOptionsContext(ref jso)); return sc;
        }

        #endregion

        #region public static IApplicationBuilder UseKaronteCore(...)

        public static IApplicationBuilder UseKaronteCore(this IApplicationBuilder ab)
        {
            if (!IsServiceConsumed(CKaronteKey.Core))
                throw new InvalidOperationException();
            else if (IsApplicationConsumed(CKaronteKey.Core))
                return ab;

            ConsumeApplication(CKaronteKey.Core);

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
            if (!IsServiceConsumed(CKaronteKey.Core))
                throw new InvalidOperationException();
            else if (IsApplicationConsumed(CKaronteKey.Routing))
                return ab;

            ConsumeApplication(CKaronteKey.Routing);

            //IKaronteAuthorizationBuilder kab;
            //if( FetchAuthorizationBuilder(ref ab, out kab) == EKaronteObjectStatus.New )
            return
                ab
                    .UseRouting()
                    .UseMiddleware<RoutingMiddlewareType>();
        }

        #endregion

        #region public static IApplicationBuilder UseKaronteAuthorizating(...)

        public static IApplicationBuilder UseKaronteAuthorizating<AuthorizatingMiddlewareType>(this IApplicationBuilder ab)
            where AuthorizatingMiddlewareType : AKaronteAuthorizatingMiddleware
        {
            if (!IsServiceConsumed(CKaronteKey.Core))
                throw new InvalidOperationException();
            else if (IsApplicationConsumed(CKaronteKey.Authorizating))
                return ab;

            ConsumeApplication(CKaronteKey.Authorizating);


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
            if (!IsServiceConsumed(CKaronteKey.Core))
                throw new InvalidOperationException();
            else if (IsApplicationConsumed(CKaronteKey.Databasing))
                return ab;

            ConsumeApplication(CKaronteKey.Databasing);


            //IKaronteDatabasingBuilder kdbb;
            //if (FetchDatabasingBuilder(ref ab, out kdbb) == EKaronteObjectStatus.New)

            return
                ab
                    .UseMiddleware<DatabasingMiddlewareType>();

            //return kdbb;
        }

        #endregion

        #region public static IApplicationBuilder UseKaronteJSONing(...)

        public static IApplicationBuilder UseKaronteJSONing(this IApplicationBuilder ab)
        {
            if (!IsServiceConsumed(CKaronteKey.JSONing))
                throw new InvalidOperationException();
            else if (IsApplicationConsumed(CKaronteKey.JSONing))
                return ab;

            ConsumeApplication(CKaronteKey.JSONing);


            return ab.UseMiddleware<KaronteJSONingMiddleware>();
        }

        #endregion

        #region public static IApplicationBuilder UseKaronteResponsing(...)

        public static IApplicationBuilder UseKaronteResponsing<ResponsingMiddlewareType, NonActionResultType>(this IApplicationBuilder ab)
            where ResponsingMiddlewareType : AKaronteResponsingMiddleware<NonActionResultType>
        {
            if (!IsServiceConsumed(CKaronteKey.Core))
                throw new InvalidOperationException();
            else if (IsApplicationConsumed(CKaronteKey.Responsing))
                return ab;

            ConsumeApplication(CKaronteKey.Responsing);


            return ab.UseMiddleware<ResponsingMiddlewareType>();
        }

        #endregion

        #region public static IApplicationBuilder UseKaronteAuthenticating(...)

        public static IApplicationBuilder UseKaronteAuthenticating<AuthenticatingMiddlewareType, ObjectType>(this IApplicationBuilder ab)
            where
                AuthenticatingMiddlewareType : AKaronteAuthenticatingMiddleware<ObjectType>
        {
            if (!IsServiceConsumed(CKaronteKey.Core))
                throw new InvalidOperationException();
            else if (IsApplicationConsumed(CKaronteKey.Authenticating))
                return ab;

            ConsumeApplication(CKaronteKey.Authenticating);


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
