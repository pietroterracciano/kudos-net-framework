using System;
using System.Numerics;

namespace Kudos.Validations.EpikyrosiModule.Interfaces.Rules
{
	public interface
		IEpikyrosiNumericRule<T>
	:
		IEpikyrosiRule
	where
		T : INumber<T>
	{
        public T? MinValue { get; set; }
		public T? MaxValue { get; set; }

        public UInt16? MinLength { get; set; }
		public UInt16? MaxLength { get; set; }
    }
}

