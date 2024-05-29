using Kudos.Servers.KaronteModule.Enums;
using System;

namespace Kudos.Servers.KaronteModule.Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public abstract class AKaronteEnumizedAttribute<EnumType> : Attribute
        where EnumType : Enum
    {
        public readonly EnumType Enum;

        public AKaronteEnumizedAttribute(EnumType e)
        {
            Enum = e;
        }
    }
}