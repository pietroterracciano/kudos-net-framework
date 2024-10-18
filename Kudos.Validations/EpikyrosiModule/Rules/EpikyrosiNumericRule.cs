using System;
using System.Numerics;
using System.Reflection;
using Kudos.Utils;
using Kudos.Utils.Numerics;
using Kudos.Validations.EpikyrosiModule.Enums;
using Kudos.Validations.EpikyrosiModule.Results;

namespace Kudos.Validations.EpikyrosiModule.Rules
{
	public sealed class
        EpikyrosiNumericRule<T>
    :
		AEpikyrosiRule
	where
		T
	:
        INumber<T>
    {
        private Boolean _bIsMinValueSetted;
        private T? _iMinValue;
        public T? MinValue { get { return _iMinValue; } set { _iMinValue = value; _bIsMinValueSetted = true; } }

        private Boolean _bIsMaxValueSetted;
        private T? _iMaxValue;
        public T? MaxValue { get { return _iMaxValue; } set { _iMaxValue = value; _bIsMaxValueSetted = true; } }

		public UInt16?
			MinLength,
			MaxLength;

        public EpikyrosiNumericRule()
            : base(NumericUtils.ParseToNType(typeof(T))) { }

        protected override void _OnValidate(ref object v, ref MemberInfo mi, out EpikyrosiNotValidResult? envr)
        {
            T? v0 = ObjectUtils.Cast<T>(v);

            if (v0 != null)
            {
                if
                (
                    _bIsMinValueSetted
                    && MinValue != null
                    && MinValue > v0
                )
                {
                    envr = new EpikyrosiNotValidResult(ref mi, EEpikyrosiNotValidOn.MinValue, MinValue);
                    return;
                }
                else if
                (
                    _bIsMaxValueSetted
                    && MaxValue != null
                    && MaxValue < v0
                )
                {
                    envr = new EpikyrosiNotValidResult(ref mi, EEpikyrosiNotValidOn.MaxValue, MaxValue);
                    return;
                }
                else if
                (
                    MinLength != null
                    || MaxLength != null
                )
                {
                    String? s = ObjectUtils.Parse<String>(v0);
                    if (s != null)
                    {
                        if
                        (
                            MinLength != null
                            && MinLength > s.Length
                        )
                        {
                            envr = new EpikyrosiNotValidResult(ref mi, EEpikyrosiNotValidOn.MinLength, MinLength);
                            return;
                        }
                        else if
                        (
                            MaxLength != null
                            && MaxLength < s.Length
                        )
                        {
                            envr = new EpikyrosiNotValidResult(ref mi, EEpikyrosiNotValidOn.MaxLength, MaxLength);
                            return;
                        }
                    }
                }
            }

            envr = null;
        }
    }
}

