using System;
using System.Numerics;
using Kudos.Validations.EpikyrosiModule.Interfaces.Rules;
using Kudos.Validations.EpikyrosiModule.Rules;

namespace Kudos.Validations.EpikyrosiModule.Attributes
{
    public class EpikyrosiNumericRuleAttribute<T> : AEpikyrosiRuleAttribute
		where T : INumber<T>
	{
        private Boolean _bIsMinValueSetted;
        private T? _iMinValue;
        public T? MinValue { get { return _iMinValue; } set { _iMinValue = value; _bIsMinValueSetted = true; } }

        private Boolean _bIsMaxValueSetted;
        private T? _iMaxValue;
        public T? MaxValue { get { return _iMaxValue; } set { _iMaxValue = value; _bIsMaxValueSetted = true; } }

        private Boolean _bIsMinLengthSetted;
        private UInt16? _iMinLength;
        public UInt16? MinLength { get { return _iMinLength; } set { _iMinLength = value; _bIsMinLengthSetted = true; } }

        private Boolean _bIsMaxLengthSetted;
        private UInt16? _iMaxLength;
        public UInt16? MaxLength { get { return _iMaxLength; } set { _iMaxLength = value; _bIsMaxLengthSetted = true; } }

        protected override void _OnParseToRule(out AEpikyrosiRule rt)
        {
            EpikyrosiNumericRule<T> enr = new EpikyrosiNumericRule<T>();

            if (_bIsMinValueSetted)
                enr.MinValue = MinValue;
            if (_bIsMaxValueSetted)
                enr.MaxValue = MaxValue;
            if (_bIsMinLengthSetted)
                enr.MinLength = MinLength;
            if (_bIsMaxLengthSetted)
                enr.MaxLength = MaxLength;

            rt = enr;
        }
    }
}