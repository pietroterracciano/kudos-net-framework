using System;
using System.Reflection;
using Kudos.Constants;
using Kudos.Reflection.Utils;
using Kudos.Types;
using Kudos.Utils;
using Kudos.Validations.EpikyrosiModule.Enums;
using Kudos.Validations.EpikyrosiModule.Results;

namespace Kudos.Validations.EpikyrosiModule.Rules
{
	public abstract class
		AEpikyrosiRule
	:
        TokenizedObject
	{
		public Boolean? CanBeNull;
		internal readonly Type DeclaringType;

		protected AEpikyrosiRule(Type dt)
		{
			DeclaringType = dt;
        }

		internal void Validate(ref Object? o, ref MemberInfo? mi, out EpikyrosiNotValidResult? envr)
		{
            Object?
                v = ObjectUtils.Parse(DeclaringType, ReflectionUtils.GetMemberValue(o, mi));

			if (v == null)
			{
				envr =
					CanBeNull != null
					&& !CanBeNull.Value
						? new EpikyrosiNotValidResult(ref mi, EEpikyrosiNotValidOn.CanBeNull, false)
						: null;
				return;
			}

            _OnValidate(ref v, ref mi, out envr);
		}

		protected abstract void _OnValidate(ref Object v, ref MemberInfo mi, out EpikyrosiNotValidResult? envr);
	}
}

