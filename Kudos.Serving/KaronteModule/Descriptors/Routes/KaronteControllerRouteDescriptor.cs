using System;
using Kudos.Reflection.Utils;
using Kudos.Serving.KaronteModule.Attributes;
using Kudos.Serving.KaronteModule.Constants;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Reflection;
using Kudos.Constants;
using Microsoft.Extensions.FileSystemGlobbing.Internal;
using System.Text.RegularExpressions;
using Kudos.Serving.KaronteModule.Controllers;
using Kudos.Types;
using Microsoft.AspNetCore.Http;
using System.Text;

namespace Kudos.Serving.KaronteModule.Descriptors.Routes
{
    public sealed class KaronteControllerRouteDescriptor
        : AKaronteRouteDescriptor
    {
        #region ... static ...

        private static readonly String
            __skcrd,
            __sfn;
        private static readonly StringBuilder
            __sb;

        // Type -> KaronteControllerRouteDescriptor
        private static readonly Dictionary<Type, KaronteControllerRouteDescriptor?>
            __d;

        static KaronteControllerRouteDescriptor()
        {
            __skcrd = "kcrd";
            __sfn = "fn";
            __sb = new StringBuilder();
            __d = new Dictionary<Type, KaronteControllerRouteDescriptor?>();
        }

        #region internal static void Get<...>(...)

        internal static void Get<T>(out KaronteControllerRouteDescriptor? kcrd)
            where T : AKaronteController
        {
            Type t = typeof(T); Get(ref t, out kcrd);
        }
        internal static void Get(ref Type? t, out KaronteControllerRouteDescriptor? kcrd)
        {
            if (t == null) { kcrd = null; return; }

            lock (__d)
            {
                if (__d.TryGetValue(t, out kcrd))
                    return;

                KaronteControllerAttribute?
                    kca = ReflectionUtils.GetCustomAttribute<KaronteControllerAttribute>(t, true);

                if (kca == null)
                {
                    __d[t] = kcrd = null;
                    return;
                }

                String
                    shk;

                #region HashKey

                __sb
                    .Clear()
                    .Append(__skcrd).Append(CCharacter.Dot).Append(__sfn).Append(CCharacter.DoubleDot).Append(t.FullName);

                shk = __sb.ToString();

                #endregion

                String
                    sp;

                #region Pattern

                sp = CKaronteRouteKey.Controller_Version + "/" + CKaronteRouteKey.Controller;

                #endregion

                String
                    srmn;

                #region ResolvedMemberName

                if (t.Name.EndsWith(CKaronteGenericKey.Controller, StringComparison.OrdinalIgnoreCase))
                    srmn = t.Name.Substring(0, t.Name.Length - CKaronteGenericKey.Controller.Length);
                else
                    srmn = t.Name;

                #endregion

                String
                    srp;

                #region ResolvedPattern

                srp = sp.Replace(CKaronteRouteKey.Controller, srmn);

                if (!kca.HasVersion)
                {
                    srp = srp.Replace(CKaronteRouteKey.Controller_Version, String.Empty);
                    srp = Regex.Replace(srp, @"\" + CCharacter.BackSlash + "{2,}", "");
                }
                else
                    srp = srp.Replace(CKaronteRouteKey.Controller_Version, "v" + kca.Version, StringComparison.OrdinalIgnoreCase);

                #endregion

                __d[t] =
                    kcrd =
                        new KaronteControllerRouteDescriptor
                        (
                            ref t,
                            ref shk,
                            ref sp,
                            ref srp,
                            ref srmn,
                            ref kca
                        );
            }
        }

        #endregion

        #endregion

        private KaronteControllerRouteDescriptor _this;
        public readonly KaronteMethodRouteDescriptor[]? MethodsRouteDescriptor;
        private readonly KaronteControllerAttribute _kca;
        public readonly Type DeclaringType;

        private
            KaronteControllerRouteDescriptor
            (
                ref Type t,
                ref String shk,
                ref String sp,
                ref String srp,
                ref String srmn,
                ref KaronteControllerAttribute kca
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
            _this = this;
            DeclaringType = t;

            MethodInfo[]?
                mia = ReflectionUtils.GetMethods(t, CBindingFlags.PublicInstance);

            if (mia == null) return;

            List<KaronteMethodRouteDescriptor> l = new List<KaronteMethodRouteDescriptor>(mia.Length);

            KaronteMethodRouteDescriptor kmrdi;
            for (int i=0; i<mia.Length; i++)
            {
                KaronteMethodRouteDescriptor.Get(ref _this, ref mia[i], out kmrdi);
                if (kmrdi == null) continue;
                l.Add(kmrdi);
            }

            MethodsRouteDescriptor = l.Count > 0 ? l.ToArray() : null;
        }
    }
}