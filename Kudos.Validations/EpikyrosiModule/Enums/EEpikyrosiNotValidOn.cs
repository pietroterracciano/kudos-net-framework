using System;
namespace Kudos.Validations.EpikyrosiModule.Enums
{
	[Flags]
	public enum EEpikyrosiNotValidOn
    {
		Object,
        MinValue,
        MaxValue,
        MinLength,
        MaxLength,
        ExpectedCollisionValue,
        ExpectedValue,
        CanBeNull,
        CanBeUndefined,
        CanBeInvalid,
        CanBeWhitespace,
        CanBeEmpty
    }
}

