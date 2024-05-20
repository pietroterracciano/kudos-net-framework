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

        //internal Boolean IsCanBeEmptySetted { get; private set; }
        //private Boolean _bCanBeEmpty;
        //public Boolean CanBeEmpty { get { return _bCanBeEmpty; } set { _bCanBeEmpty = value; IsCanBeEmptySetted = true; } }

        internal Boolean IsCanBeWhitespaceSetted { get; private set; }
        private Boolean _bCanBeWhitespace;
        public Boolean CanBeWhitespace { get { return _bCanBeWhitespace; } set { _bCanBeWhitespace = value; IsCanBeWhitespaceSetted = true; } }

        protected override void _OnParseToRule(out AEpikyrosiRule rt)
        {
            EpikyrosiStringRule esr = new EpikyrosiStringRule();

            if (IsMinLengthSetted)
                esr.MinLength = MinLength;
            if (IsMaxLengthSetted)
                esr.MaxLength = MaxLength;
            //if (IsCanBeEmptySetted)
            //    esr.CanBeEmpty = CanBeEmpty;
            if (IsCanBeWhitespaceSetted)
                esr.CanBeWhitespace = CanBeWhitespace;

            rt = esr;
        }
    }
}

