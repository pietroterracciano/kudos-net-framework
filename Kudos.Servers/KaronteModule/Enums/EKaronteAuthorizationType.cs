using Kudos.Constants;
using System;

namespace Kudos.Servers.KaronteModule.Enums
{
    [Flags]
    public enum EKaronteAuthorizationType
    {
        None = CBinaryFlag._0,
        Access = None | CBinaryFlag._1,
        Bearer = None | Access | CBinaryFlag._2
    }
}
