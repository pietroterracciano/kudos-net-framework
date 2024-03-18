using Kudos.Servers.KaronteModule.Enums;
using System;

namespace Kudos.Servers.KaronteModule.Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public class KaronteAuthorizatingAttribute : Attribute
    {
        public readonly EKaronteAuthorization Value;

        public KaronteAuthorizatingAttribute(EKaronteAuthorization e)
        {
            Value = e;
        }
    }
}
