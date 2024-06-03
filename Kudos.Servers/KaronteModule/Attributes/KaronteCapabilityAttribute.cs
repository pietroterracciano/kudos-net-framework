using System;
using System.Collections.Generic;
using Kudos.Constants;
using Kudos.Servers.KaronteModule.Enums;

namespace Kudos.Servers.KaronteModule.Attributes
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public sealed class KaronteCapabilityAttribute : Attribute
    {
        public readonly HashSet<String>? Routes;
        public readonly Boolean HasRoutes;
        public readonly EKaronteCapabilityValidationRule ValidationRule;

        public KaronteCapabilityAttribute() : this(EKaronteCapabilityValidationRule.OnlyOneValidRoute, null) { }
        public KaronteCapabilityAttribute(params String[]? sa) : this(EKaronteCapabilityValidationRule.OnlyOneValidRoute, sa) { }
        public KaronteCapabilityAttribute(EKaronteCapabilityValidationRule e, params String[]? sa)
        {
            ValidationRule = e;

            if (sa == null || sa.Length < 1) return;

            List<String> l = new List<string>(sa.Length);

            for(int i=0; i<sa.Length; i++)
            {
                if (String.IsNullOrWhiteSpace(sa[i])) continue;
                l.Add(sa[i]);
            }

            if (l.Count < 1) return;

            Routes = new HashSet<string>(l);
            HasRoutes = true;
        }
    }
}