using Kudos.Constants;
using System;

namespace Kudos.Servers.KaronteModule.Enums
{
    [Flags]
    public enum EKaronteRoute
    {
        NotRegistered = CBinaryFlag._0,
        OnFallback = CBinaryFlag._1,
        Registered = CBinaryFlag._2,
        OnRoot = CBinaryFlag._3,
        NotSupported = CBinaryFlag._4
    }
}
