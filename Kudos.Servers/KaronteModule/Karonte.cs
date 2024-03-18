using Kudos.Databases.Chainers;
using Kudos.Databases.Interfaces;
using Kudos.Databases.Interfaces.Chains;
using Kudos.Servers.KaronteModule.Attributes;
using Kudos.Servers.KaronteModule.Builders;
using Kudos.Servers.KaronteModule.Constants;
using Kudos.Servers.KaronteModule.Contexts;
using Kudos.Servers.KaronteModule.Descriptors.Routes;
using Kudos.Servers.KaronteModule.Enums;
using Kudos.Servers.KaronteModule.Interfaces;
using Kudos.Servers.KaronteModule.Middlewares;
using Kudos.Utils;
using Kudos.Utils.Members;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Runtime.Intrinsics.Arm;
using System.Text.RegularExpressions;

namespace Kudos.Servers.KaronteModule
{
    public static class Karonte
    {
        private static Type[]? __a;
        private static Func<IApplicationBuilder, IKaronteAuthorizationBuilder> __f0;
        private static Func<IApplicationBuilder, IKaronteDatabasingBuilder> __f1;
        private static Dictionary<IApplicationBuilder, KaronteApplicationBuilderContext> __d0;
        private static Dictionary<IApplicationBuilder, Dictionary<Type, IKaronteBuilder>> __d1;
        //private static Dictionary<IApplicationBuilder, IKaronteDatabaseHandlerBuilder> __d2;
        private static Boolean __bIsCoreAdded;// __bIsAuthorizationAdded, _bIsDatabasingAdded;

        static Karonte()
        {
            __f0 = ab => new KaronteAuthorizationBuilder(ref ab);
            __f1 = ab => new KaronteDatabasingBuilder(ref ab);
            __bIsCoreAdded = false;
            __d0 = new Dictionary<IApplicationBuilder, KaronteApplicationBuilderContext>();
            __d1 = new Dictionary<IApplicationBuilder, Dictionary<Type, IKaronteBuilder>>();
            //__d2 = new Dictionary<IApplicationBuilder, IKaronteDatabaseHandlerBuilder>();
        }

        private static EKaronteObjectStatus FetchApplicationBuilderContext(ref IApplicationBuilder ab, out KaronteApplicationBuilderContext kabc)
        {
            if (__d0.TryGetValue(ab, out kabc) && kabc != null) return EKaronteObjectStatus.InReuse;
            __d0[ab] = kabc = new KaronteApplicationBuilderContext(); return EKaronteObjectStatus.New;
        }

        private static EKaronteObjectStatus FetchAuthorizationBuilder(ref IApplicationBuilder ab, out IKaronteAuthorizationBuilder kab)
        {
            return FetchBuilder(ref ab, out kab, __f0);
        }

        private static EKaronteObjectStatus FetchDatabasingBuilder(ref IApplicationBuilder ab, out IKaronteDatabasingBuilder kab)
        {
            return FetchBuilder(ref ab, out kab, __f1);
        }

        private static EKaronteObjectStatus FetchBuilder<T>(ref IApplicationBuilder ab, out T ktb, Func<IApplicationBuilder, T> fnc) 
            where T : IKaronteBuilder
        {
            Dictionary<Type, IKaronteBuilder> d;
            if (!__d1.TryGetValue(ab, out d) || d == null) __d1[ab] = d = new Dictionary<Type, IKaronteBuilder>();
            Type t = typeof(T);
            IKaronteBuilder? kb;
            if (d.TryGetValue(t, out kb) && kb != null) { ktb = ObjectUtils.Cast<T>(kb); return EKaronteObjectStatus.InReuse; }
            d[t] = kb = ktb = fnc.Invoke(ab); return EKaronteObjectStatus.New;
        }

        //private static EKaronteObjectStatus FetchDatabaseHandlerBuilder(ref IApplicationBuilder ab, out IKaronteDatabaseHandlerBuilder kab)
        //{
        //    if (__d1.TryGetValue(ab, out kab) && kab != null) return EKaronteObjectStatus.InReuse;
        //    __d2[ab] = kab = new KaronteAuthorizationBuilder(ref ab); return EKaronteObjectStatus.New;
        //}

        private static void IsApplicationBuilderContextMetaEnabled(ref IApplicationBuilder ab, String s, Boolean o)
        {
            KaronteApplicationBuilderContext kabc;
            FetchApplicationBuilderContext(ref ab, out kabc);
            kabc.SetMeta(s, o);
        }

        private static Boolean IsApplicationBuilderContextMetaEnabled(ref IApplicationBuilder ab, String s)
        {
            KaronteApplicationBuilderContext kabc;
            FetchApplicationBuilderContext(ref ab, out kabc);
            Boolean? b = kabc.GetMeta<Boolean?>(s);
            return b != null && b.Value;
        }

        private static Boolean CanApplicationBuilderContextContinue(ref IApplicationBuilder ab, String s)
        {
            if (IsApplicationBuilderContextMetaEnabled(ref ab, s)) return false;
            IsApplicationBuilderContextMetaEnabled(ref ab, s, true); return true;
        }

        #region public static IServiceCollection AddKaronteCore(...)

        public static IServiceCollection AddKaronteCore(this IServiceCollection sc)
        {
            if (!__bIsCoreAdded)
            {
                __bIsCoreAdded = true;

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
                        ksa = MemberUtils.GetAttribute<KaronteServiceAttribute>(ts1[i], true);

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
            }

            return sc;
        }

        #endregion

        #region public static IApplicationBuilder UseKaronteCore(...)

        public static IApplicationBuilder UseKaronteCore(this IApplicationBuilder ab)
        {
            if (!__bIsCoreAdded)
                throw new InvalidOperationException();
            else if (!CanApplicationBuilderContextContinue(ref ab, CKaronteApplicationBuilderContextMetaKey.UseCore))
                return ab;

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
            if (!__bIsCoreAdded || !IsApplicationBuilderContextMetaEnabled(ref ab, CKaronteApplicationBuilderContextMetaKey.UseCore))
                throw new InvalidOperationException();
            else if (!CanApplicationBuilderContextContinue(ref ab, CKaronteApplicationBuilderContextMetaKey.UseRouting))
                return ab;

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
            if (!__bIsCoreAdded || !IsApplicationBuilderContextMetaEnabled(ref ab, CKaronteApplicationBuilderContextMetaKey.UseCore))
                throw new InvalidOperationException();
            else if (!CanApplicationBuilderContextContinue(ref ab, CKaronteApplicationBuilderContextMetaKey.UseAuthorizating))
                return ab;

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
            if (!__bIsCoreAdded || !IsApplicationBuilderContextMetaEnabled(ref ab, CKaronteApplicationBuilderContextMetaKey.UseCore))
                throw new InvalidOperationException();
            else if (!CanApplicationBuilderContextContinue(ref ab, CKaronteApplicationBuilderContextMetaKey.UseDatabasing))
                return ab;

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
            if (!__bIsCoreAdded)
                throw new InvalidOperationException();
            else if (!CanApplicationBuilderContextContinue(ref ab, CKaronteApplicationBuilderContextMetaKey.UseJSONing))
                return ab;

            return ab.UseMiddleware<KaronteJSONingMiddleware>();
        }

        #endregion

        #region public static IApplicationBuilder UseKaronteResponsing(...)

        public static IApplicationBuilder UseKaronteResponsing<ResponsingMiddlewareType, NonActionResultType>(this IApplicationBuilder ab)
            where ResponsingMiddlewareType : AKaronteResponsingMiddleware<NonActionResultType>
        {
            if (!__bIsCoreAdded || !IsApplicationBuilderContextMetaEnabled(ref ab, CKaronteApplicationBuilderContextMetaKey.UseCore))
                throw new InvalidOperationException();
            else if (!CanApplicationBuilderContextContinue(ref ab, CKaronteApplicationBuilderContextMetaKey.UseResponsing))
                return ab;

            return ab.UseMiddleware<ResponsingMiddlewareType>();
        }

        #endregion

        #region public static IApplicationBuilder UseKaronteAuthenticating(...)

        public static IApplicationBuilder UseKaronteAuthenticating<AuthenticatingMiddlewareType, ObjectType>(this IApplicationBuilder ab)
            where
                AuthenticatingMiddlewareType : AKaronteAuthenticatingMiddleware<ObjectType>
        {
            if (!__bIsCoreAdded || !IsApplicationBuilderContextMetaEnabled(ref ab, CKaronteApplicationBuilderContextMetaKey.UseCore))
                throw new InvalidOperationException();
            else if(!CanApplicationBuilderContextContinue(ref ab, CKaronteApplicationBuilderContextMetaKey.UseAuthenticating))
                return ab;

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
                kca = MemberUtils.GetAttribute<KaronteControllerAttribute>(t, true);

            if (kca == null)
                return;

            KaronteRouteDescriptor[]? krds0;
            if (!__MapKaronteControllers(/*ref e,*/ t, out krds0))
                return;

            KaronteRouteDescriptor[]? krds1;
            if (!__MapKaronteControllers(/*ref e, */MethodUtils.Get(t), out krds1))
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
                naa = MemberUtils.GetAttribute<NonActionAttribute>(mi, true);

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

            return __MapKaronteControllers(ref mi, MemberUtils.GetAttributes<RouteAttribute>(mi, true), out krds);
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
