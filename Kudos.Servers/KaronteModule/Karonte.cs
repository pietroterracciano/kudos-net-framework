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
using Kudos.Servers.KaronteModule.Options;
using Kudos.Servers.KaronteModule.Services;
using Kudos.Servers.KaronteModule.Utils;
using Kudos.Types;
using Kudos.Utils;
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
        private static Type[]? __aRegisteredServices;
        private static readonly List<Type> __lRegisteredServices;
        private static readonly HashSet<String> __hsRegistedApplications;
        private static readonly List<Type> __lRegisteredControllers;

        static Karonte()
        {
            __lRegisteredServices = new List<Type>();
            __lRegisteredControllers = new List<Type>();
            __hsRegistedApplications = new HashSet<string>();
        }

        private static void __RegisterService<T>() { __RegisterService(typeof(T)); }
        private static void __RegisterService(Type? t) { if (t == null || __lRegisteredServices.Contains(t)) return; __lRegisteredServices.Add(t); }
        private static Boolean __IsServiceRegistered<T>() { return __IsServiceRegistered(typeof(T)); }
        private static Boolean __IsServiceRegistered(Type? t) { return t != null && __lRegisteredServices.Contains(t); }

        private static void __RegisterApplication(String? s) { if (s == null) return; __hsRegistedApplications.Add(s); }
        private static Boolean __IsApplicationRegistered(String? s) { return s != null && __hsRegistedApplications.Contains(s); }

        internal static void RegisterController(ref Type? t)
        {
            if (t == null) return;
            __lRegisteredControllers.Add(t);
        }

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
            sc
                .AddRouting()
                .AddControllers();

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

        #region public static IApplicationBuilder UseKaronteCore(...)

        public static IApplicationBuilder UseKaronteCore(this IApplicationBuilder ab)
        {
            if (!__IsServiceRegistered<KaronteContext>())
                throw new InvalidOperationException();
            else if (__IsApplicationRegistered(CKaronteKey.Core))
                return ab;

            __RegisterApplication(CKaronteKey.Core);

            __aRegisteredServices = __lRegisteredServices.ToArray();

            return 
                ab.Use
                (
                    async (httpc, rd) =>
                    {
                        KaronteContext kc = httpc.RequestServices.GetRequiredService<KaronteContext>();
                        kc.RegisteredServices = __aRegisteredServices;
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
            if (!__IsServiceRegistered<KaronteContext>())
                throw new InvalidOperationException();
            else if (__IsApplicationRegistered(CKaronteKey.Routing))
                return ab;

            __RegisterApplication(CKaronteKey.Routing);

            return
                ab
                    .UseRouting()
                    .UseMiddleware<RoutingMiddlewareType>();
        }

        #endregion

        #region public static IApplicationBuilder UseKaronteCrypting(...)

        public static IApplicationBuilder UseKaronteCrypting(this IApplicationBuilder ab)
        {
            if (!__IsServiceRegistered<KaronteCryptingService>())
                throw new InvalidOperationException();
            else if (__IsApplicationRegistered(CKaronteKey.Crypting))
                return ab;

            __RegisterApplication(CKaronteKey.Crypting);

            return
                ab
                    .Use
                    (
                        async (httpc, rd) =>
                        {
                            KaronteContext kc = httpc.RequestServices.GetRequiredService<KaronteContext>();
                            kc.CryptingContext = new KaronteCryptingContext(ref kc);
                            await rd.Invoke();
                        }
                    );
        }

        #endregion

        #region public static IApplicationBuilder UseKaronteAuthorizating(...)

        public static IApplicationBuilder UseKaronteAuthorizating<MiddlewareType, AttributeType, EnumType>(this IApplicationBuilder ab)
            where MiddlewareType : AKaronteAuthorizatingMiddleware<AttributeType, EnumType>
            where AttributeType : AKaronteEnumizedAttribute<EnumType>
            where EnumType : Enum
        {
            if (!__IsServiceRegistered<KaronteContext>())
                throw new InvalidOperationException();
            else if (__IsApplicationRegistered(CKaronteKey.Authorizating))
                return ab;

            __RegisterApplication(CKaronteKey.Authorizating);

            return
                ab
                    .UseKaronteHeading(CKaronteHttpHeader.Authorization)
                    .UseKaronteAttributing<AttributeType>()
                    .UseMiddleware<MiddlewareType>();
        }

        #endregion

        #region public static IApplicationBuilder UseKaronteDatabasing(...)

        public static IApplicationBuilder UseKaronteDatabasing<MiddlewareType>(this IApplicationBuilder ab)
            where MiddlewareType : AKaronteDatabasingMiddleware
        {
            if (!__IsServiceRegistered<KaronteDatabasingService>())
                throw new InvalidOperationException();
            else if (__IsApplicationRegistered(CKaronteKey.Databasing))
                return ab;

            __RegisterApplication(CKaronteKey.Databasing);

            return
                ab
                    .UseMiddleware<MiddlewareType>();
        }

        #endregion

        #region public static IApplicationBuilder UseKaronteHeading(...)

        public static IApplicationBuilder UseKaronteHeading<MiddlewareType>(this IApplicationBuilder ab, String? s)
            where MiddlewareType : AKaronteHeadingMiddleware
        {
            return
                ab
                    .UseKaronteHeading(s)
                    .UseMiddleware<MiddlewareType>();
        }

        internal static IApplicationBuilder UseKaronteHeading(this IApplicationBuilder ab, String? s)
        {
            if (!__IsServiceRegistered<KaronteContext>())
                throw new InvalidOperationException();

            __RegisterApplication(CKaronteKey.Heading);

            return
                ab
                    .Use
                    (
                        async (httpc, rq) =>
                        {
                            KaronteContext
                                kc = httpc.RequestServices.GetRequiredService<KaronteContext>();

                            StringValues sv;

                            if (s != null)
                                httpc.Request.Headers.TryGetValue(s, out sv);
                            else
                                sv = new StringValues();

                            KaronteHeadingContext khc = new KaronteHeadingContext(ref s, ref sv, ref kc);

                            kc.RegisterObject(CKaronteKey.Heading, ref khc);

                            await rq.Invoke();
                        }
                    );
        }

        #endregion

        #region public static IApplicationBuilder UseKaronteAttributing(...)

        public static IApplicationBuilder UseKaronteAttributing<MiddlewareType, AttributeType>(this IApplicationBuilder ab)
            where MiddlewareType : AKaronteAttributingMiddleware<AttributeType>
            where AttributeType : Attribute
        {
            return
                ab
                    .UseKaronteAttributing<AttributeType>()
                    .UseMiddleware<MiddlewareType>();
        }

        internal static IApplicationBuilder UseKaronteAttributing<AttributeType>(this IApplicationBuilder ab)
            where AttributeType : Attribute
        {
            if (!__IsServiceRegistered<KaronteContext>())
                throw new InvalidOperationException();

            __RegisterApplication(CKaronteKey.Attributing);

            return
                ab
                    .Use
                    (
                        async (httpc, rq) =>
                        {
                            KaronteContext
                                kc = httpc.RequestServices.GetRequiredService<KaronteContext>();

                            Attribute?
                                att = EndpointUtils.GetLastMetadata<AttributeType>(httpc.GetEndpoint());

                            KaronteAttributingContext hac = new KaronteAttributingContext(ref att, ref kc);

                            kc.RegisterObject(CKaronteKey.Attributing, ref hac);

                            await rq.Invoke();
                        }
                    );
        }

        #endregion

        #region public static IApplicationBuilder UseKaronteJSONing(...)

        public static IApplicationBuilder UseKaronteJSONing(this IApplicationBuilder ab)
        {
            if (!__IsServiceRegistered<KaronteJSONingService>())
                throw new InvalidOperationException();
            else if (__IsApplicationRegistered(CKaronteKey.JSONing))
                return ab;

            __RegisterApplication(CKaronteKey.JSONing);

            return
                ab
                    .Use
                    (
                        async (httpc, rd) =>
                        {
                            KaronteContext kc = httpc.RequestServices.GetRequiredService<KaronteContext>();
                            kc.JSONingContext = new KaronteJSONingContext(ref kc);
                            await rd.Invoke();
                        }
                    );
        }

        #endregion

        #region public static IApplicationBuilder UseKaronteResponsing(...)

        public static IApplicationBuilder UseKaronteResponsing<ResponsingMiddlewareType, NonActionResultType>(this IApplicationBuilder ab)
            where ResponsingMiddlewareType : AKaronteResponsingMiddleware<NonActionResultType>
        {
            if (!__IsServiceRegistered<KaronteContext>())
                throw new InvalidOperationException();
            else if (__IsApplicationRegistered(CKaronteKey.Responsing))
                return ab;

            __RegisterApplication(CKaronteKey.Responsing);

            return ab.UseMiddleware<ResponsingMiddlewareType>();
        }

        #endregion

        #region public static IApplicationBuilder UseKaronteAuthenticating(...)

        public static IApplicationBuilder UseKaronteAuthenticating<MiddlewareType, AuthenticationDataType, AttributeType, EnumType>(this IApplicationBuilder ab)
            where
                MiddlewareType : AKaronteAuthenticatingMiddleware<AuthenticationDataType, AttributeType, EnumType>
            where
                AttributeType : AKaronteEnumizedAttribute<EnumType>
            where
                EnumType : Enum
        {
            if (!__IsServiceRegistered<KaronteContext>())
                throw new InvalidOperationException();
            else if (__IsApplicationRegistered(CKaronteKey.Authenticating))
                return ab;

            __RegisterApplication(CKaronteKey.Authenticating);

            return
                ab
                    .UseKaronteAttributing<AttributeType>()
                    .UseMiddleware<MiddlewareType>();
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
            Type t = typeof(T);
            __MapKaronteController(ref erb, ref t);
            return erb;
        }

        #endregion

        private static void __MapKaronteController
        (
            ref IEndpointRouteBuilder erb,
            ref Type? t
        )
        {
            KaronteControllerAttribute?
                kca = ReflectionUtils.GetCustomAttribute<KaronteControllerAttribute>(t, true);

            if (kca == null)
                return;

            KaronteRouteDescriptor[]? krds0;
            if (!__MapKaronteController(t, out krds0))
                return;

            KaronteRouteDescriptor[]? krds1;
            if (!__MapKaronteController(ReflectionUtils.GetMethods(t), out krds1))
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

        private static Boolean __MapKaronteController
        (
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
                if (!__MapKaronteController(mis[i], out krdsi)) continue;
                l.AddRange(krdsi);
            }

            krds = l.ToArray();

            return krds.Length > 0;
        }

        private static Boolean __MapKaronteController
        (
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

            return __MapKaronteController(ref mi, ReflectionUtils.GetCustomAttributes<RouteAttribute>(mi, true), out krds);
        }

        private static Boolean __MapKaronteController
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
                    if (!__MapKaronteController(ref mi, ref ras[i], out krdi)) continue;
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

        private static Boolean __MapKaronteController
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
