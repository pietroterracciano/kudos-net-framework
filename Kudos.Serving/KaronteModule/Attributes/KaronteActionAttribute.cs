using System;
namespace Kudos.Serving.KaronteModule.Attributes
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public sealed class KaronteActionAttribute : Attribute { }
}

