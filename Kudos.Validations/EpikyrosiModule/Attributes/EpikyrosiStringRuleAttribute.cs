using System;
using Kudos.Validations.EpikyrosiModule.Rules;
using System.Numerics;

namespace Kudos.Validations.EpikyrosiModule.Attributes
{
    public class EpikyrosiStringRuleAttribute : AEpikyrosiRuleAttribute
    {
        internal Boolean IsMinLengthSetted { get; private set; }
        private UInt16 _iMinLength;
        public UInt16 MinLength { get { return _iMinLength; } set { _iMinLength = value; IsMinLengthSetted = true; } }

        internal Boolean IsMaxLengthSetted { get; private set; }
        private UInt16 _iMaxLength;
        public UInt16 MaxLength { get { return _iMaxLength; } set { _iMaxLength = value; IsMaxLengthSetted = true; } }

        protected override void _OnParseToRule(out AEpikyrosiRule rt)
        {
            EpikyrosiStringRule esr = new EpikyrosiStringRule();

            if (IsMinLengthSetted)
                esr.MinLength = MinLength;
            if (IsMaxLengthSetted)
                esr.MaxLength = MaxLength;

            rt = esr;
        }
    }
}

