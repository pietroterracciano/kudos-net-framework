using System;
using System.Reflection;
using Kudos.Constants;
using Kudos.Utils;
using Kudos.Validations.EpikyrosiModule.Enums;
using Kudos.Validations.EpikyrosiModule.Results;

namespace Kudos.Validations.EpikyrosiModule.Rules
{
	public sealed class
        EpikyrosiStringRule
	:
		AEpikyrosiStringRule
	{
		protected override void _OnValidate(ref String v, ref MemberInfo mi, out EpikyrosiNotValidResult? envr)
		{
			envr = null;
		}
    }
}