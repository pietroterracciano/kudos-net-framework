using Kudos.Constants;
using System;

namespace Kudos.Servers.KaronteModule.Enums
{
    [Flags]
    public enum EKaronteBounce
    {
        MoveBackward = CBinaryFlag._0,
        MoveForward = CBinaryFlag._1
    }
}
