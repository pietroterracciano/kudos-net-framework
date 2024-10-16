using System;
using Kudos.Utils;
using Microsoft.AspNetCore.Http;

namespace Kudos.Serving.KaronteModule.Utils
{
	public static class HttpContextUtils
	{ 
		public static T? GetService<T>(HttpContext? httpc)
		{
            return ObjectUtils.Cast<T>(GetService(httpc, typeof(T)));
        }

        public static Object? GetService(HttpContext? httpc, Type? t)
        {
            if (httpc != null && t != null)
                try { return httpc.RequestServices.GetService(t); } catch { }

            return null;
        }

        public static Object RequireService(HttpContext? httpc, Type? t)
        {
            Object? o = GetService(httpc, t);
            if (o == null) throw new InvalidOperationException();
            return o;
        }

        public static T RequireService<T>(HttpContext? httpc)
        {
            T? o = GetService<T>(httpc);
            if (o == null) throw new InvalidOperationException();
            return o;
        }
    }
}