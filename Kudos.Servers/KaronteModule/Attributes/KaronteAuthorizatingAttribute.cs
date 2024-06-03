using System;
using Kudos.Servers.KaronteModule.Enums;

namespace Kudos.Servers.KaronteModule.Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public sealed class KaronteAuthorizatingAttribute : Attribute
    {
        public readonly EKaronteAuthorizationType AuthorizationType;

        public KaronteAuthorizatingAttribute(EKaronteAuthorizationType e)
        {
            AuthorizationType = e;
        }
    }
}