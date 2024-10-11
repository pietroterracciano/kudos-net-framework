using System;

namespace Kudos.Serving.KaronteModule.Attributes
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
    public sealed class KaronteMiddlewareAttribute : Attribute { }
}
