using System;
using Kudos.Servers.KaronteModule.Enums;

namespace Kudos.Servers.KaronteModule.Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public sealed class KaronteAuthenticatingAttribute : Attribute { }
}