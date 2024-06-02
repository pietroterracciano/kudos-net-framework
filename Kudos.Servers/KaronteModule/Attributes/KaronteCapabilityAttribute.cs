using System;
using Kudos.Constants;
using Kudos.Servers.KaronteModule.Enums;

namespace Kudos.Servers.KaronteModule.Attributes
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public sealed class KaronteCapabilityAttribute : Attribute
    {
        public readonly String[]? Values;
        public readonly Boolean HasValues;
        public readonly EKaronteCapabilityValidationRule ValidationRule;

        public KaronteCapabilityAttribute() : this(EKaronteCapabilityValidationRule.OnlyOneValid, null) { }
        public KaronteCapabilityAttribute(params String[]? sa) : this(EKaronteCapabilityValidationRule.OnlyOneValid, sa) { }
        public KaronteCapabilityAttribute(EKaronteCapabilityValidationRule e, params String[]? sa)
        {
            ValidationRule = e;
            Values = sa != null && sa.Length > 0 ? sa : null;
            HasValues = Values != null;
        }
    }
}