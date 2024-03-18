using Kudos.Constants;
using System;

namespace Kudos.Servers.KaronteModule.Enums
{
    [Flags]
    public enum EKaronteServiceType
    {
        Singleton = CBinaryFlag._0,
        Scoped = CBinaryFlag._1,
        Transient = CBinaryFlag._2
    }
}
