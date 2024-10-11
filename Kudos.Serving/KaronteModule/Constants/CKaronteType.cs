using System;
using Kudos.Serving.KaronteModule.Contexts;
using Microsoft.AspNetCore.Http;

namespace Kudos.Serving.KaronteModule.Constants
{
	internal static class CKaronteType
	{
		internal static readonly Type
			KaronteContext = typeof(KaronteContext),
			HttpContext = typeof(HttpContext);
	}
}

