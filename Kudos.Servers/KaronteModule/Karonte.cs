using Kudos.Crypters.KryptoModule.SymmetricModule;
using Kudos.Databases.Chainers;
using Kudos.Databases.Chains;
using Kudos.Databases.Interfaces.Chains;
using Kudos.Reflection.Utils;
using Kudos.Servers.KaronteModule.Attributes;
using Kudos.Servers.KaronteModule.Constants;
using Kudos.Servers.KaronteModule.Contexts;
using Kudos.Servers.KaronteModule.Controllers;
using Kudos.Servers.KaronteModule.Descriptors.Routes;
using Kudos.Servers.KaronteModule.Enums;
using Kudos.Servers.KaronteModule.Middlewares;
using Kudos.Servers.KaronteModule.Services;
using Kudos.Servers.KaronteModule.Utils;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Kudos.Servers.KaronteModule
{
    public static class Karonte
    {
        static Karonte()
        {
            __hsRegisteredServices = new HashSet<string>();
            __lRegisteredServices = new List<Type>();
            __hsRegistedApplications = new HashSet<string>();
            __lRegisteredEndpoints = new List<Type>();
        }

        #region private static ...

        private static Type[]? __aRegisteredServices;
        private static readonly List<Type> __lRegisteredServices, __lRegisteredEndpoints;
        private static readonly HashSet<String> __hsRegisteredServices, __hsRegistedApplications;

        private static void __RegisterService<T>() { __RegisterService(typeof(T)); }
        private static void __RegisterService(Type? t) { if (t == null || __lRegisteredServices.Contains(t)) return; __lRegisteredServices.Add(t); }
        private static Boolean __IsServiceRegistered<T>() { return __IsServiceRegistered(typeof(T)); }
        private static Boolean __IsServiceRegistered(Type? t) { return t != null && __lRegisteredServices.Contains(t); }

        private static void __RegisterService(String? s) { if (s == null) return; __hsRegisteredServices.Add(s); }
        private static Boolean __IsServiceRegistered(String? s) { return s != null && __hsRegisteredServices.Contains(s); }

        private static void __RegisterApplication(String? s) { if (s == null) return; __hsRegistedApplications.Add(s); }
        private static Boolean __IsApplicationRegistered(String? s) { return s != null && __hsRegistedApplications.Contains(s); }

        private static void __RegisterEndpoint<T>() { __RegisterEndpoint(typeof(T)); }
        private static void __RegisterEndpoint(Type? t) { if (t == null || __lRegisteredEndpoints.Contains(t)) return; __lRegisteredEndpoints.Add(t); }
        private static Boolean __IsEndpointRegistered<T>() { return __IsEndpointRegistered(typeof(T)); }
        private static Boolean __IsEndpointRegistered(Type? t) { return t != null && __lRegisteredEndpoints.Contains(t); }

        private static void __MapKaronteController
        (
            ref IEndpointRouteBuilder erb,
            ref Type? t
        )
        {
            KaronteControllerRouteDescriptor? kcrd;
            KaronteControllerRouteDescriptor.Get(ref t, out kcrd);

            if (kcrd == null || kcrd.MethodsRouteDescriptor == null)
                return;
            else if (__IsEndpointRegistered(t))
                return;

            __RegisterEndpoint(t);

            int k = 0;
            for(int i=0; i< kcrd.MethodsRouteDescriptor.Length; i++)
            {
                erb.MapControllerRoute
                (
                    kcrd.MethodsRouteDescriptor[i].FullHashKey,
                    kcrd.MethodsRouteDescriptor[i].ResolvedFullPattern,
                    new { controller = kcrd.ResolvedMemberName, action = kcrd.MethodsRouteDescriptor[i].ResolvedMemberName }
                );

                k++;
            }
        }

        #endregion

        #region public static ...

        #region public static IServiceCollection AddKaronteCore(...)

        public static IServiceCollection AddKaronteCore(this IServiceCollection sc)
        {
            if (__IsServiceRegistered<KaronteContext>()) return sc;
            __RegisterService<KaronteContext>();

            sc
                .TryAddScoped<KaronteContext>
                (
                    (sp) =>
                    {
                        return new KaronteContext();
                    }
                );

            Assembly?
                ass = Assembly.GetEntryAssembly();

            if (ass == null)
                return sc;

            Type[]? at;
            try { at = ass.GetTypes(); } catch { at = null; }
            if (at == null)
                return sc;

            for (int i = 0; i < at.Length; i++)
            {
                KaronteServiceAttribute?
                    ksa = ReflectionUtils.GetCustomAttribute<KaronteServiceAttribute>(at[i], true);

                if (ksa == null)
                    continue;

                switch (ksa.Type)
                {
                    case EKaronteServiceType.Scoped:
                        sc.TryAddScoped(at[i]);
                        __RegisterService(at[i]);
                        break;
                    case EKaronteServiceType.Transient:
                        sc.TryAddTransient(at[i]);
                        __RegisterService(at[i]);
                        break;
                    case EKaronteServiceType.Singleton:
                        sc.TryAddSingleton(at[i]);
                        __RegisterService(at[i]);
                        break;
                }
            }

            return sc;
        }

        #endregion

        #region public static IServiceCollection AddKaronteRoutingCore(...)

        public static IServiceCollection AddKaronteRoutingCore(this IServiceCollection sc)
        {
            if (!__IsServiceRegistered<KaronteContext>())
                throw new InvalidOperationException();
            else if (__IsServiceRegistered(CKaronteKey.Routing))
                return sc;

            __RegisterService(CKaronteKey.Routing);

            sc
                .AddRoutingCore()
                .AddControllers();

            return sc;
        }

        #endregion

        #region public static IServiceCollection AddKaronteRouting(...)

        public static IServiceCollection AddKaronteRouting(this IServiceCollection sc)
        {
            if (!__IsServiceRegistered<KaronteContext>())
                throw new InvalidOperationException();
            else if (__IsServiceRegistered(CKaronteKey.Routing))
                return sc;

            __RegisterService(CKaronteKey.Routing);

            sc
                .AddRouting()
                .AddControllers();

            return sc;
        }

        #endregion

        #region public static IServiceCollection AddKaronteScoped<T>(..)

        public static IServiceCollection AddKaronteScoped<T>(this IServiceCollection sc)
        {
            if (!__IsServiceRegistered<KaronteContext>())
                throw new InvalidOperationException();

            sc.AddScoped(typeof(T));
            __RegisterService<T>();
            return sc;
        }

        #endregion

        #region public static IServiceCollection TryAddKaronteScoped<T>(..)

        public static IServiceCollection TryAddKaronteScoped<T>(this IServiceCollection sc)
        {
            if (!__IsServiceRegistered<KaronteContext>())
                throw new InvalidOperationException();
            else if (__IsServiceRegistered<T>())
                return sc;

            sc.TryAddScoped(typeof(T));
            __RegisterService<T>();
            return sc;
        }

        #endregion

        #region public static IServiceCollection AddKaronteSingleton<T>(..)

        public static IServiceCollection AddKaronteSingleton<T>(this IServiceCollection sc)
        {
            if (!__IsServiceRegistered<KaronteContext>())
                throw new InvalidOperationException();

            sc.AddSingleton(typeof(T));
            __RegisterService<T>();
            return sc;
        }

        #endregion

        #region public static IServiceCollection TryAddKaronteSingleton<T>(..)

        public static IServiceCollection TryAddKaronteSingleton<T>(this IServiceCollection sc)
        {
            if (!__IsServiceRegistered<KaronteContext>())
                throw new InvalidOperationException();
            else if (__IsServiceRegistered<T>())
                return sc;

            sc.TryAddSingleton(typeof(T));
            return sc;
        }

        #endregion

        #region public static IServiceCollection AddKaronteTransient<T>(..)

        public static IServiceCollection AddKaronteTransient<T>(this IServiceCollection sc)
        {
            if (!__IsServiceRegistered<KaronteContext>())
                throw new InvalidOperationException();

            sc.AddTransient(typeof(T));
            return sc;
        }

        #endregion

        #region public static IServiceCollection TryAddKaronteTransient<T>(..)

        public static IServiceCollection TryAddKaronteTransient<T>(this IServiceCollection sc)
        {
            if (!__IsServiceRegistered<KaronteContext>())
                throw new InvalidOperationException();
            else if (__IsServiceRegistered<T>())
                return sc;

            sc.TryAddTransient(typeof(T));
            return sc;
        }

        #endregion

        #region public static IServiceCollection AddKaronteJSONing(..)

        public static IServiceCollection AddKaronteJSONing(this IServiceCollection sc, Action<KaronteJSONingService>? act)
        {
            if (!__IsServiceRegistered<KaronteContext>())
                throw new InvalidOperationException();
            else if (__IsServiceRegistered<KaronteJSONingService>())
                return sc;

            __RegisterService<KaronteJSONingService>();

            KaronteJSONingService kjsons = new KaronteJSONingService(ref sc);
            if (act != null) act.Invoke(kjsons);
            sc.TryAddSingleton<KaronteJSONingService>(kjsons);

            return sc;
        }

        #endregion

        #region public static IServiceCollection AddKaronteCrypting(..)

        public static IServiceCollection AddKaronteCrypting(this IServiceCollection sc, Action<KaronteCryptingService>? act)
        {
            if (!__IsServiceRegistered<KaronteContext>())
                throw new InvalidOperationException();
            else if (__IsServiceRegistered<KaronteCryptingService>())
                return sc;

            __RegisterService<KaronteCryptingService>();

            KaronteCryptingService kcs = new KaronteCryptingService(ref sc);
            if (act != null) act.Invoke(kcs);
            sc.TryAddSingleton<KaronteCryptingService>(kcs);

            return sc;
        }

        #endregion

        #region public static IServiceCollection AddKaronteClouding(..)

        public static IServiceCollection AddKaronteClouding(this IServiceCollection sc, Action<KaronteCloudingService>? act)
        {
            if (!__IsServiceRegistered<KaronteContext>())
                throw new InvalidOperationException();
            else if (__IsServiceRegistered<KaronteCloudingService>())
                return sc;

            __RegisterService<KaronteCloudingService>();

            KaronteCloudingService kcs = new KaronteCloudingService(ref sc);
            if (act != null) act.Invoke(kcs);
            sc.TryAddSingleton<KaronteCloudingService>(kcs);

            return sc;
        }

        #endregion

        #region public static IServiceCollection AddKaronteDatabasing(..)

        public static IServiceCollection AddKaronteDatabasing(this IServiceCollection sc, Action<KaronteDatabasingService>? act)
        {
            if (!__IsServiceRegistered<KaronteContext>())
                throw new InvalidOperationException();
            else if (__IsServiceRegistered<KaronteDatabasingService>())
                return sc;

            __RegisterService<KaronteDatabasingService>();

            KaronteDatabasingService kdbs = new KaronteDatabasingService(ref sc);
            if (act != null) act.Invoke(kdbs);
            sc.TryAddSingleton<KaronteDatabasingService>(kdbs);

            return sc;
        }

        #endregion

        #region public static IServiceCollection AddKaronteHeading(..)

        public static IServiceCollection AddKaronteHeading(this IServiceCollection sc)
        {
            if (!__IsServiceRegistered<KaronteContext>())
                throw new InvalidOperationException();

            __RegisterService(CKaronteKey.Heading);
            return sc;
        }

        #endregion

        #region public static IServiceCollection AddKaronteCapabiliting(..)

        public static IServiceCollection AddKaronteCapabiliting(this IServiceCollection sc)
        {
            sc
                .AddKaronteRouting();

            if (!__IsServiceRegistered<KaronteContext>())
                throw new InvalidOperationException();

            __RegisterService(CKaronteKey.Capabiliting);
            return sc;
        }

        #endregion

        #region public static IServiceCollection AddKaronteBenchmarking(..)

        public static IServiceCollection AddKaronteBenchmarking(this IServiceCollection sc)
        {
            if (!__IsServiceRegistered<KaronteContext>())
                throw new InvalidOperationException();

            __RegisterService(CKaronteKey.Benchmarking);
            return sc;
        }

        #endregion

        #region public static IServiceCollection AddKaronteAuthorizating(..)

        public static IServiceCollection AddKaronteAuthorizating(this IServiceCollection sc)
        {
            sc
                .AddKaronteHeading()
                .AddKaronteRoutingCore();

            if (!__IsServiceRegistered<KaronteContext>())
                throw new InvalidOperationException();

            __RegisterService(CKaronteKey.Authorizating);
            return sc;
        }

        #endregion

        #region public static IServiceCollection AddKaronteAuthenticating(..)

        public static IServiceCollection AddKaronteAuthenticating(this IServiceCollection sc)
        {
            sc
                .AddKaronteRoutingCore();

            if (!__IsServiceRegistered<KaronteContext>())
                throw new InvalidOperationException();

            __RegisterService(CKaronteKey.Authenticating);
            return sc;
        }

        #endregion

        #region public static IServiceCollection AddKaronteResponsing(..)

        public static IServiceCollection AddKaronteResponsing(this IServiceCollection sc)
        {
            if (!__IsServiceRegistered<KaronteContext>())
                throw new InvalidOperationException();

            __RegisterService(CKaronteKey.Responsing);
            return sc;
        }

        #endregion

        #region public static IApplicationBuilder UseKaronteCore(...)

        public static IApplicationBuilder UseKaronteCore(this IApplicationBuilder ab)
        {
            if (!__IsServiceRegistered<KaronteContext>())
                throw new InvalidOperationException();
            else if (__IsApplicationRegistered(CKaronteKey.Core))
                return ab;

            __RegisterApplication(CKaronteKey.Core);

            __aRegisteredServices = __lRegisteredServices.ToArray();

            Boolean
                bIsKaronteBenchmarkingServiceRegistered = __IsServiceRegistered(CKaronteKey.Benchmarking),
                bIsKaronteJSONingServiceRegistered = __IsServiceRegistered<KaronteJSONingService>(),
                bIsKaronteCloudingServiceRegistered = __IsServiceRegistered<KaronteCloudingService>(),
                bIsKaronteDatabasingServiceRegistered = __IsServiceRegistered<KaronteDatabasingService>(),
                bIsKaronteCryptingServiceRegistered = __IsServiceRegistered<KaronteCryptingService>(),
                bIsKaronteResponsingServiceRegistered = __IsServiceRegistered(CKaronteKey.Responsing),
                bIsKaronteCapabilitingServiceRegistered = __IsServiceRegistered(CKaronteKey.Capabiliting),
                bIsKaronteRoutingServiceRegistered = __IsServiceRegistered(CKaronteKey.Routing),
                bIsKaronteAuthorizatingServiceRegistered = __IsServiceRegistered(CKaronteKey.Authorizating),
                bIsKaronteAuthenticatingServiceRegistered = __IsServiceRegistered(CKaronteKey.Authenticating),
                bIsKaronteHeadingServiceRegistered = __IsServiceRegistered(CKaronteKey.Heading);

            return 
                ab.Use
                (
                    async (httpc, rd) =>
                    {
                        KaronteContext kc = httpc.RequestServices.GetRequiredService<KaronteContext>();
                        kc.RegisteredServices = __aRegisteredServices;
                        kc.HttpContext = httpc;

                        httpc.Response.StatusCode = CKaronteHttpStatusCode.MethodNotAllowed;

                        if (bIsKaronteJSONingServiceRegistered)
                        {
                            KaronteJSONingService kjsons = 
                                httpc.RequestServices.GetRequiredService<KaronteJSONingService>();

                            kc.JSONingContext = new KaronteJSONingContext(ref kjsons, ref kc);
                        }

                        if (bIsKaronteDatabasingServiceRegistered)
                        {
                            KaronteDatabasingService kdbs =
                                httpc.RequestServices.GetRequiredService<KaronteDatabasingService>();

                            kc.DatabasingContext = new KaronteDatabasingContext(ref kdbs, ref kc);
                        }

                        if (bIsKaronteCloudingServiceRegistered)
                        {
                            KaronteCloudingService kcs =
                                httpc.RequestServices.GetRequiredService<KaronteCloudingService>();

                            kc.CloudingContext = new KaronteCloudingContext(ref kcs, ref kc);
                        }

                        if (bIsKaronteCryptingServiceRegistered)
                        {
                            KaronteCryptingService kcs =
                                httpc.RequestServices.GetRequiredService<KaronteCryptingService>();

                            kc.CryptingContext = new KaronteCryptingContext(ref kcs, ref kc);
                        }

                        if(bIsKaronteCapabilitingServiceRegistered)
                            kc.CapabilitingContext = new KaronteCapabilitingContext(ref kc);

                        if(bIsKaronteRoutingServiceRegistered)
                            kc.RoutingContext = new KaronteRoutingContext(ref kc);

                        if (bIsKaronteAuthorizatingServiceRegistered)
                            kc.AuthorizatingContext = new KaronteAuthorizatingContext(ref kc);

                        if (bIsKaronteAuthenticatingServiceRegistered)
                            kc.AuthenticatingContext = new KaronteAuthenticatingContext(ref kc);

                        if (bIsKaronteHeadingServiceRegistered)
                            kc.HeadingContext = new KaronteHeadingContext(ref kc);

                        if (bIsKaronteResponsingServiceRegistered)
                            kc.ResponsingContext = new KaronteResponsingContext(ref kc);

                        if (bIsKaronteBenchmarkingServiceRegistered)
                            kc.BenchmarkingContext = new KaronteBenchmarkingContext(ref kc);

                        await rd.Invoke(httpc);
                    }
                );
        }

        #endregion

        #region public static IApplicationBuilder UseKaronteMiddleware...)

        public static IApplicationBuilder UseKaronteMiddleware<MiddlewareType>(this IApplicationBuilder ab)
            where MiddlewareType : AKaronteMiddleware
        {
            return ab.__UseKaronteMiddleware<MiddlewareType>(false);
        }

        private static IApplicationBuilder __UseKaronteMiddleware<MiddlewareType>(this IApplicationBuilder ab, Boolean bUseBenchmarking)
            where MiddlewareType : AKaronteMiddleware
        {
            if (!__IsApplicationRegistered(CKaronteKey.Core))
                throw new InvalidOperationException();

            String? s = typeof(MiddlewareType).FullName;

            if (bUseBenchmarking && __IsServiceRegistered(CKaronteKey.Benchmarking))
                ab
                    .Use
                    (
                        async (httpc, rd) =>
                        {
                            httpc.RequestServices.GetRequiredService<KaronteContext>()
                                .BenchmarkingContext.StartBenchmark(s);

                            await rd.Invoke();
                        }
                    );

            ab
                .UseMiddleware<MiddlewareType>();

            if (bUseBenchmarking && __IsServiceRegistered(CKaronteKey.Benchmarking))
                ab
                    .Use
                    (
                        async (httpc, rd) =>
                        {
                            httpc.RequestServices.GetRequiredService<KaronteContext>()
                                .BenchmarkingContext.StopBenchmark(s);

                            await rd.Invoke();
                        }
                    );

            return ab;
        }

        #endregion

        #region public static IApplicationBuilder UseKaronteRouting(...)

        public static IApplicationBuilder UseKaronteRouting<MiddlewareType>(this IApplicationBuilder ab)
            where MiddlewareType : AKaronteRoutingMiddleware
        {
            return
                ab
                    .UseKaronteRouting()
                    .UseKaronteMiddleware<MiddlewareType>();
        }

        public static IApplicationBuilder UseKaronteRouting(this IApplicationBuilder ab)
        {
            if
            (
                !__IsApplicationRegistered(CKaronteKey.Core)
                || !__IsServiceRegistered(CKaronteKey.Routing)
            )
                throw new InvalidOperationException();
            else if (__IsApplicationRegistered(CKaronteKey.Routing))
                return ab;

            __RegisterApplication(CKaronteKey.Routing);

            return
                ab
                    .UseRouting();
        }

        #endregion

        #region public static IApplicationBuilder UseKaronteAuthorizating(...)

        public static IApplicationBuilder UseKaronteAuthorizating<MiddlewareType>(this IApplicationBuilder ab)
           where MiddlewareType : AKaronteAuthorizatingMiddleware
        {
            if
            (
                !__IsServiceRegistered(CKaronteKey.Authorizating)
            )
                throw new InvalidOperationException();
            else if (__IsApplicationRegistered(CKaronteKey.Authorizating))
                return ab;

            __RegisterApplication(CKaronteKey.Authorizating);

            return
                ab.__UseKaronteMiddleware<MiddlewareType>(true);
        }

        #endregion

        #region public static IApplicationBuilder UseKaronteDatabasing(...)

        public static IApplicationBuilder UseKaronteDatabasing<MiddlewareType>(this IApplicationBuilder ab)
            where MiddlewareType : AKaronteDatabasingMiddleware
        {
            if
            (
               !__IsServiceRegistered<KaronteDatabasingService>()
            )
                throw new InvalidOperationException();
            else if (__IsApplicationRegistered(CKaronteKey.Databasing))
                return ab;

            __RegisterApplication(CKaronteKey.Databasing);

            return
                ab.__UseKaronteMiddleware<MiddlewareType>(true);
        }

        #endregion

        #region public static IApplicationBuilder UseKaronteResponsing(...)

        public static IApplicationBuilder UseKaronteResponsing<MiddlewareType, NonActionResultType>(this IApplicationBuilder ab)
            where MiddlewareType : AKaronteResponsingMiddleware<NonActionResultType>
        {
            if
            (
               !__IsServiceRegistered(CKaronteKey.Responsing)
            )
                throw new InvalidOperationException();
            else if (__IsApplicationRegistered(CKaronteKey.Responsing))
                return ab;

            __RegisterApplication(CKaronteKey.Responsing);

            return
                ab.__UseKaronteMiddleware<MiddlewareType>(true);
        }

        #endregion

        #region public static IApplicationBuilder UseKaronteAuthenticating(...)

        public static IApplicationBuilder UseKaronteAuthenticating<MiddlewareType>(this IApplicationBuilder ab)
            where MiddlewareType : AKaronteAuthenticatingMiddleware
        {
            if
            (
               !__IsServiceRegistered(CKaronteKey.Authenticating)
            )
                throw new InvalidOperationException();
            else if (__IsApplicationRegistered(CKaronteKey.Authenticating))
                return ab;

            __RegisterApplication(CKaronteKey.Authenticating);

            return
                ab.__UseKaronteMiddleware<MiddlewareType>(true);
        }

        #endregion

        #region public static IApplicationBuilder UseKaronteCapabiliting(...)

        public static IApplicationBuilder UseKaronteCapabiliting<MiddlewareType>(this IApplicationBuilder ab)
            where
                MiddlewareType : AKaronteCapabilitingMiddleware
        {
            if
            (
               !__IsServiceRegistered(CKaronteKey.Capabiliting)
            )
                throw new InvalidOperationException();
            else if (__IsApplicationRegistered(CKaronteKey.Capabiliting))
                return ab;

            __RegisterApplication(CKaronteKey.Capabiliting);

            return
                ab.__UseKaronteMiddleware<MiddlewareType>(true);
        }

        #endregion

        #region public static IEndpointRouteBuilder MapKaronteControllers(...)

        public static IEndpointRouteBuilder MapKaronteControllers(this IEndpointRouteBuilder erb)
        {
            Assembly?
                ass = Assembly.GetEntryAssembly();

            if (ass == null)
                return erb;

            Type[]? at;
            try { at = ass.GetTypes(); } catch { at = null; }
            if (at == null)
                return erb;

            for(int i=0; i<at.Length; i++)
                __MapKaronteController(ref erb, ref at[i]);

            return erb;
        }

        #endregion

        #region public static IEndpointRouteBuilder MapKaronteController<T>(...)

        public static IEndpointRouteBuilder MapKaronteController<T>(this IEndpointRouteBuilder erb)
            where T : AKaronteController
        {
            if
            (
               !__IsApplicationRegistered(CKaronteKey.Core)
               || !__IsServiceRegistered(CKaronteKey.Routing)
            )
                throw new InvalidOperationException();

            Type t = typeof(T);
            __MapKaronteController(ref erb, ref t);
            return erb;
        }

        #endregion

        #endregion
    }
}
