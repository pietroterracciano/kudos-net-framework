using Kudos.Servers.KaronteModule.Enums;
using System;

namespace Kudos.Servers.KaronteModule.Attributes
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
    public class KaronteServiceAttribute : Attribute
    {
        public readonly EKaronteServiceType Type;

        public KaronteServiceAttribute(EKaronteServiceType e)
        {
            Type = e;
        }
    }
}