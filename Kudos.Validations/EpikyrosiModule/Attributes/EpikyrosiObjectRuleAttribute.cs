using System;
using Kudos.Validations.EpikyrosiModule.Rules;
using System.Numerics;

namespace Kudos.Validations.EpikyrosiModule.Attributes
{
    public sealed class EpikyrosiObjectRuleAttribute : AEpikyrosiRuleAttribute
    {
        protected override void _OnParseToRule(out AEpikyrosiRule rt)
        {
            rt = new EpikyrosiObjectRule();
        }
    }
}

