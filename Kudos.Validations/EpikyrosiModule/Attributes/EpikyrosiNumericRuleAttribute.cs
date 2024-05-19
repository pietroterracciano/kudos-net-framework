using System;
using System.Numerics;
using Kudos.Validations.EpikyrosiModule.Interfaces.Rules;
using Kudos.Validations.EpikyrosiModule.Rules;

namespace Kudos.Validations.EpikyrosiModule.Attributes
{
    public class EpikyrosiNumericRuleAttribute<T> : AEpikyrosiRuleAttribute
		where T : INumber<T>
	{
        internal Boolean IsMinValueSetted { get; private set; }
        private T _iMinValue;
        public T MinValue { get { return _iMinValue; } set { _iMinValue = value; IsMinValueSetted = true; } }

        internal Boolean IsMaxValueSetted { get; private set; }
        private T _iMaxValue;
        public T MaxValue { get { return _iMaxValue; } set { _iMaxValue = value; IsMaxValueSetted = true; } }

        internal Boolean IsMinLengthSetted { get; private set; }
        private UInt16 _iMinLength;
        public UInt16 MinLength { get { return _iMinLength; } set { _iMinLength = value; IsMinLengthSetted = true; } }

        internal Boolean IsMaxLengthSetted { get; private set; }
        private UInt16 _iMaxLength;
        public UInt16 MaxLength { get { return _iMaxLength; } set { _iMaxLength = value; IsMaxLengthSetted = true; } }

        protected override void _OnParseToRule(out AEpikyrosiRule rt)
        {
            EpikyrosiNumericRule<T> enr = new EpikyrosiNumericRule<T>();

            if (IsMinValueSetted)
                enr.MinValue = MinValue;
            if (IsMaxValueSetted)
                enr.MaxValue = MaxValue;
            if (IsMinLengthSetted)
                enr.MinLength = MinLength;
            if (IsMaxLengthSetted)
                enr.MaxLength = MaxLength;

            rt = enr;
        }
    }
}