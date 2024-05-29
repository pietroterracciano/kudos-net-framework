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
        Collision,
        CanBeNull,
        CanBeInvalid,
        CanBeWhitespace,
        CanBeEmpty
    }
}

