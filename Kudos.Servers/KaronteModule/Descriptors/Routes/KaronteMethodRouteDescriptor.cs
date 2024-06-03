﻿using System;
using Kudos.Reflection.Utils;
using Kudos.Servers.KaronteModule.Attributes;
using Kudos.Servers.KaronteModule.Constants;
using System.Collections.Generic;
using System.Reflection;
using Microsoft.AspNetCore.Mvc;
using Kudos.Constants;
using System.Text.RegularExpressions;
using Kudos.Types;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Metadata;
using Kudos.Servers.KaronteModule.Utils;
using System.Text;
using Kudos.Servers.KaronteModule.Controllers;
using Microsoft.AspNetCore.Routing;

namespace Kudos.Servers.KaronteModule.Descriptors.Routes
{
    public class KaronteMethodRouteDescriptor
        : AKaronteRouteDescriptor
    {
        #region ... static ...

        private static readonly String
            __skmrd,
            __sn;
        private static readonly StringBuilder
            __sb;

        // MethodInfo -> KaronteMethodRouteDescriptor
        private static readonly Dictionary<MethodInfo, KaronteMethodRouteDescriptor?>
            __d;
        private static readonly Metas
            __m;

        static KaronteMethodRouteDescriptor()
        {
            __skmrd = "kmrd";
            __sn = "n";
            __sb = new StringBuilder();
            __m = new Metas(StringComparison.OrdinalIgnoreCase);
            __d = new Dictionary<MethodInfo, KaronteMethodRouteDescriptor?>();
        }

        #region internal static void Request<...>(...)

        internal static void Request(ref KaronteControllerRouteDescriptor? kcrd, ref MethodInfo? mi, out KaronteMethodRouteDescriptor? kmrd)
        {
            if (kcrd == null || mi == null) { kmrd = null; return; }

            lock (__d)
            {
                if (__d.TryGetValue(mi, out kmrd))
                    return;

                KaronteActionAttribute?
                   kaa = ReflectionUtils.GetCustomAttribute<KaronteActionAttribute>(mi, true);

                if(kaa == null)
                {
                    __d[mi] = kmrd = null;
                    return;
                }

                //KaronteNonActionAttribute?
                //    knaa = ReflectionUtils.GetCustomAttribute<KaronteNonActionAttribute>(mi, true);

                //if(knaa != null)
                //{
                //    __d[mi] = kmrd = null;
                //    return;
                //}

                NonActionAttribute?
                    naa = ReflectionUtils.GetCustomAttribute<NonActionAttribute>(mi, true);

                if (naa != null)
                {
                    __d[mi] = kmrd = null;
                    return;
                }

                String
                    shk;

                #region HashKey

                __sb
                    .Clear()
                    .Append(__skmrd).Append(CCharacter.Dot).Append(__sn).Append(CCharacter.DoubleDot).Append(mi.Name);

                shk = __sb.ToString();

                #endregion

                String
                    sp;

                #region Pattern

                sp = CKaronteRouteKey.Action;

                #endregion

                String
                    srmn;

                #region ResolvedMemberName

                srmn = mi.Name;

                #endregion

                String
                    srp;

                #region ResolvedPattern

                srp = sp.Replace(CKaronteRouteKey.Action, srmn);

                #endregion

                __d[mi] =
                    kmrd =
                        new KaronteMethodRouteDescriptor
                        (
                            ref kcrd,
                            ref mi,
                            ref shk,
                            ref sp,
                            ref srp,
                            ref srmn
                        );

                lock(__m)
                {
                    __m.Set(kmrd.FullHashKey, kmrd);
                    __m.Set(kmrd.ResolvedFullPattern, kmrd);
                }
            }
        }

        //internal static void Request(ref Endpoint? end, out KaronteMethodRouteDescriptor? kmrd)
        //{
        //    String? shkorfp;

        //    IRouteDiagnosticsMetadata? rdmd = EndpointUtils.GetLastMetadata<IRouteDiagnosticsMetadata>(end);
        //    shkorfp = rdmd != null ? shkorfp = rdmd.Route : null;
           
        //    lock (__m)
        //    {
        //        kmrd = __m.Get<KaronteMethodRouteDescriptor>(shkorfp);
        //    }

        //    if (kmrd != null) return;

        //    RouteNameMetadata? rnmd = EndpointUtils.GetLastMetadata<RouteNameMetadata>(end);
        //    shkorfp = rnmd != null ? rnmd.RouteName : null;

        //    lock (__m)
        //    {
        //        kmrd = __m.Get<KaronteMethodRouteDescriptor>(shkorfp);
        //    }
        //}

        #endregion

        #endregion

        public readonly KaronteControllerRouteDescriptor DeclaringControllerRouteDescriptor;
        public readonly MethodInfo DeclaringMethod;
        public readonly String FullPattern, ResolvedFullPattern, FullHashKey;

        private
            KaronteMethodRouteDescriptor
            (
                ref KaronteControllerRouteDescriptor kcrd,
                ref MethodInfo mi,
                ref String shk,
                ref String sp,
                ref String srp,
                ref String srmn
            )
        :
            base
            (
                ref shk,
                ref sp,
                ref srp,
                ref srmn
            )
        {
            DeclaringControllerRouteDescriptor = kcrd;
            DeclaringMethod = mi;
            FullPattern = kcrd.Pattern + Pattern;
            ResolvedFullPattern = kcrd.ResolvedPattern + ResolvedPattern;
            FullHashKey = kcrd.HashKey + CCharacter.Pipe + HashKey;
        }
    }
}