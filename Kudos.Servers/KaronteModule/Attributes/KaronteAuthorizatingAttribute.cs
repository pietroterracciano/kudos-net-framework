using System;
using Kudos.Servers.KaronteModule.Enums;

namespace Kudos.Servers.KaronteModule.Attributes
{
    public class KaronteAuthorizatingAttribute : AKaronteAuthorizatingAttribute<EKaronteAuthorization>
    {
        public KaronteAuthorizatingAttribute(EKaronteAuthorization e) : base(e) { }
    }
}

