using System;
using Kudos.Serving.KaronteModule.Enums;

namespace Kudos.Serving.KaronteModule.Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public sealed class KaronteAuthenticationAttribute : Attribute { }
}