using System;

namespace Kudos.Servers.KaronteModule.Constants
{
    internal static class CKaronteApplicationBuilderContextMetaKey
    {
        public static readonly String
            UseRouting = "UseRouting",
            UseCore = "UseCore",
            UseAuthenticating = "UseAuthenticating",
            UseAuthorizating = "UseAuthorizating",
            UseDatabasing = "UseDatabasing",
            UseJSONing = "UseJSONing",
            UseResponsing = "UseResponsing";
    }
}
