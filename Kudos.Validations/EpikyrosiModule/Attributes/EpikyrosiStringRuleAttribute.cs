using System;
using Kudos.Validations.EpikyrosiModule.Rules;
using System.Numerics;

namespace Kudos.Validations.EpikyrosiModule.Attributes
{
    public sealed class EpikyrosiStringRuleAttribute : AEpikyrosiRuleAttribute
    {
        internal Boolean _bIsMinLengthSetted;
        private UInt16 _iMinLength;
        public UInt16 MinLength { get { return _iMinLength; } set { _iMinLength = value; _bIsMinLengthSetted = true; } }

        internal Boolean _bIsMaxLengthSetted;
        private UInt16 _iMaxLength;
        public UInt16 MaxLength { get { return _iMaxLength; } set { _iMaxLength = value; _bIsMaxLengthSetted = true; } }

        private Boolean _bIsCanBeWhitespaceSetted, _bCanBeWhitespace;
        public Boolean CanBeWhitespace { get { return _bCanBeWhitespace; } set { _bCanBeWhitespace = value; _bIsCanBeWhitespaceSetted = true; } }

        protected override void _OnParseToRule(out AEpikyrosiRule rt)
        {
            EpikyrosiStringRule esr = new EpikyrosiStringRule();

            if (_bIsMinLengthSetted)
                esr.MinLength = MinLength;
            if (_bIsMaxLengthSetted)
                esr.MaxLength = MaxLength;
            //if (IsCanBeEmptySetted)
            //    esr.CanBeEmpty = CanBeEmpty;
            if (_bIsCanBeWhitespaceSetted)
                esr.CanBeWhitespace = CanBeWhitespace;

            rt = esr;
        }
    }
}

