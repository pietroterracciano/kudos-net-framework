using System;
using Kudos.Validations.EpikyrosiModule.Rules;

namespace Kudos.Validations.EpikyrosiModule.Attributes
{
    public abstract class AEpikyrosiStringRuleAttribute<RuleType> : AEpikyrosiRuleAttribute
        where RuleType : AEpikyrosiStringRule
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
            RuleType er = _OnRuleCreate();

            if (_bIsMinLengthSetted)
                er.MinLength = MinLength;
            if (_bIsMaxLengthSetted)
                er.MaxLength = MaxLength;
            if (_bIsCanBeWhitespaceSetted)
                er.CanBeWhitespace = CanBeWhitespace;

            _OnParseToRule(ref er);

            rt = er;
        }

        protected abstract void _OnParseToRule(ref RuleType rt);

        protected abstract RuleType _OnRuleCreate();
    }
}

