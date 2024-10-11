using Kudos.Serving.KaronteModule.Enums;
using System;

namespace Kudos.Serving.KaronteModule.Attributes
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
    public sealed class KaronteServiceAttribute : Attribute
    {
        public readonly EKaronteServiceType Type;

        public KaronteServiceAttribute(EKaronteServiceType e)
        {
            Type = e;
        }
    }
}