using Kudos.Constants;
using System;

namespace Kudos.Servers.KaronteModule.Enums
{
    [Flags]
    public enum EKaronteObjectStatus
    {
        New = CBinaryFlag._0,
        InReuse = CBinaryFlag._1
    }
}