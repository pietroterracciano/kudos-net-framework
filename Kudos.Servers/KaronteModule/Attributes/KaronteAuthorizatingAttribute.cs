using System;
using Kudos.Servers.KaronteModule.Enums;

namespace Kudos.Servers.KaronteModule.Attributes
{
    public sealed class KaronteAuthorizatingAttribute : AKaronteAuthorizatingAttribute<EKaronteAuthorizationType>
    {
        public KaronteAuthorizatingAttribute(EKaronteAuthorizationType e) : base(e) { }
    }
}

