using System;
using Kudos.Validations.EpikyrosiModule.Rules;
using System.Numerics;

namespace Kudos.Validations.EpikyrosiModule.Attributes
{
    public sealed class EpikyrosiStringRuleAttribute : AEpikyrosiStringRuleAttribute<EpikyrosiStringRule>
    {
        protected override void _OnParseToRule(ref EpikyrosiStringRule rt) { }
        protected override EpikyrosiStringRule _OnRuleCreate() { return new EpikyrosiStringRule(); }
    }
}

