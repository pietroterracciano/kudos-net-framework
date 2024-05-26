using Kudos.Servers.KaronteModule.Enums;
using System;

namespace Kudos.Servers.KaronteModule.Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public abstract class AKaronteAuthorizatingAttribute<Enum> : Attribute
    {
        public readonly Enum Value;

        public AKaronteAuthorizatingAttribute(Enum e)
        {
            Value = e;
        }
    }
}