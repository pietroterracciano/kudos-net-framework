using System;
using System.Numerics;
using Kudos.Utils;
using Kudos.Validations.EpikyrosiModule.Enums;

namespace Kudos.Validations.EpikyrosiModule.Rules
{
	public class
        EpikyrosiNumericRule<T>
    :
		AEpikyrosiRule
	where
		T
	:
        INumber<T>
    {
		public T?
			MinValue,
			MaxValue;

		public UInt16?
			MinLength,
			MaxLength;

        public EpikyrosiNumericRule()
            : base(typeof(T)) { }

        protected override void _OnValidate(ref object v, out EEpikyrosiNotValidOn? envo)
        {
            T? v0 = ObjectUtils.Cast<T>(v);

            if (v0 != null)
            {
                if
                (
                    MinValue != null
                    && MinValue > v0
                )
                {
                    envo = EEpikyrosiNotValidOn.MinValue;
                    return;
                }
                else if
                (
                    MaxValue != null
                    && MaxValue < v0
                )
                {
                    envo = EEpikyrosiNotValidOn.MaxValue;
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
                            envo = EEpikyrosiNotValidOn.MinLength;
                            return;
                        }
                        else if
                        (
                            MaxLength != null
                            && MaxLength < s.Length
                        )
                        {
                            envo = EEpikyrosiNotValidOn.MaxLength;
                            return;
                        }
                    }
                }
            }

            envo = null;
        }
    }
}

