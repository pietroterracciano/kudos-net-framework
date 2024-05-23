using System;
using Kudos.Servers.KaronteModule.Contexts;
using Microsoft.AspNetCore.Http;

namespace Kudos.Servers.KaronteModule.Constants
{
	internal static class CKaronteType
	{
		internal static readonly Type
			KaronteContext = typeof(KaronteContext),
			HttpContext = typeof(HttpContext);
	}
}

