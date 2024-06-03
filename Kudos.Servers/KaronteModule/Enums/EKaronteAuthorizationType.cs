using Kudos.Constants;
using System;

namespace Kudos.Servers.KaronteModule.Enums
{
    [Flags]
    public enum EKaronteAuthorizationType
    {
        Access = CBinaryFlag._0,
        Bearer = Access | CBinaryFlag._1
    }
}
