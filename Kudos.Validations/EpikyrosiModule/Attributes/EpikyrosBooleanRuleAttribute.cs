using System;
using Kudos.Validations.EpikyrosiModule.Rules;

namespace Kudos.Validations.EpikyrosiModule.Attributes
{
    public sealed class EpikyrosBooleanRuleAttribute : AEpikyrosiRuleAttribute
    {
        private Boolean _bIsExpectedValueSetted;
        private Boolean? _bExpectedValue;
        public Boolean? ExpectedValue { get { return _bExpectedValue; } set { _bExpectedValue = value; _bIsExpectedValueSetted = true; } }

        protected override void _OnParseToRule(out AEpikyrosiRule rt)
        {
            EpikyrosiBooleanRule ebr = new EpikyrosiBooleanRule();

            if (_bIsExpectedValueSetted)
                ebr.ExpectedValue = ExpectedValue;

            rt = ebr;
        }
    }
}