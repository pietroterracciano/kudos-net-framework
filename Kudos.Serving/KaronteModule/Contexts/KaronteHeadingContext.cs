using System;
using System.Text;
using Kudos.Serving.KaronteModule.Middlewares;
using Kudos.Types;
using Kudos.Utils.Collections;
using Microsoft.Extensions.Primitives;
using Microsoft.Net.Http.Headers;
using static System.Net.WebRequestMethods;

namespace Kudos.Serving.KaronteModule.Contexts
{
	public sealed class KaronteHeadingContext : AKaronteChildContext
	{
        //public readonly String? HeaderName;
        //      public readonly StringValues HeaderValues;
        //      public readonly Boolean HasHeaderValues;

        //internal KaronteHeadingContext(ref String? sn, ref StringValues sv, ref KaronteContext kc) : base(ref kc)
        //{
        //          HeaderName = sn;
        //          HasHeaderValues = (HeaderValues = sv).Count > 0;
        //      }

        internal KaronteHeadingContext(ref KaronteContext kc) : base(ref kc) { }

        public String?[]? GetRequestHeaderValues(String? s)
        {
            if (s == null) return null;
            StringValues sv;
            KaronteContext.HttpContext.Request.Headers.TryGetValue(s, out sv);
            return sv.ToArray();
        }

        public String? GetSignificativeRequestHeaderValue(String? s)
        {
            String?[]? sa = GetRequestHeaderValues(s);
            if (sa != null) 
                for (int i = 0; i < sa.Length; i++)
                {
                    if (String.IsNullOrWhiteSpace(sa[i])) continue;
                    return sa[i];
                }

            return null;
        }

        public String? GetLastRequestHeaderValue(String? s)
        {
            String?[]? sa = GetRequestHeaderValues(s);
            return ArrayUtils.IsValidIndex(sa, 0) ? sa[sa.Length - 1] : null;
        }

        public String? GetFirstRequestHeaderValue(String? s)
        {
            String?[]? sa = GetRequestHeaderValues(s);
            return ArrayUtils.IsValidIndex(sa, 0) ? sa[0] : null;
        }
    }
}

