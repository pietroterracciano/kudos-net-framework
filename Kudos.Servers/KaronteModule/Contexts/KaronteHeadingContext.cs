using System;
using System.Text;
using Kudos.Servers.KaronteModule.Middlewares;
using Kudos.Types;
using Microsoft.Extensions.Primitives;
using Microsoft.Net.Http.Headers;

namespace Kudos.Servers.KaronteModule.Contexts
{
	public sealed class KaronteHeadingContext : AKaronteChildContext
	{
		public readonly String? HeaderName;
        public readonly StringValues HeaderValues;
        public readonly Boolean HasHeaderValues;

		internal KaronteHeadingContext(ref String? sn, ref StringValues sv, ref KaronteContext kc) : base(ref kc)
		{
            HeaderName = sn;
            HasHeaderValues = (HeaderValues = sv).Count > 0;
        }
	}
}

