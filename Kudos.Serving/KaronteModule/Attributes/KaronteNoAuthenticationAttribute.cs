using System;
namespace Kudos.Serving.KaronteModule.Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public sealed class KaronteNoAuthenticationAttribute : Attribute { }
}

