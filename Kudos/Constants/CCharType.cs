using System;
using Kudos.Enums;

namespace Kudos.Constants
{
	internal static class CCharType
	{
        internal static ECharType
            Standard = ECharType.StandardUpperCase | ECharType.StandardLowerCase,
            Numeric = ECharType.Numeric,
            StandardNumeric = Standard | Numeric;
    }
}

