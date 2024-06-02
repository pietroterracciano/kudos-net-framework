using Kudos.Constants;
using System;

namespace Kudos.Servers.KaronteModule.Enums
{
    [Flags]
    public enum EKaronteRouteStatus
    {
        NotRegistered,
        OnFallback,
        Registered,
        OnRoot,
        NotSupported
    }
}
