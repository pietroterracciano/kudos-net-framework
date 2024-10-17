using System;
using Kudos.Serving.KaronteModule.Contexts;
using Kudos.Serving.KaronteModule.Contexts.Crypting;
using Kudos.Serving.KaronteModule.Contexts.Databasing;
using Microsoft.AspNetCore.Http;

namespace Kudos.Serving.KaronteModule.Constants
{
	public static class CKaronteType
	{
        public static readonly Type
			KaronteContext = typeof(KaronteContext),
            KaronteCryptingContext = typeof(KaronteCryptingContext),
            KaronteCryptingSymmetricingContext = typeof(KaronteCryptingSymmetricingContext),
            KaronteCryptingHashingContext = typeof(KaronteCryptingHashingContext),
            KaronteAuthorizatingContext = typeof(KaronteAuthorizatingContext),
            KarontePoolizedDatabasingContext = typeof(KarontePoolizedDatabasingContext),
            KaronteAuthenticatingContext = typeof(KaronteAuthenticatingContext),
            KaronteResponsingContext = typeof(KaronteResponsingContext),
            KaronteDatabasingContext = typeof(KaronteDatabasingContext),
            HttpContext = typeof(HttpContext);
	}
}

