using System;
using System.Reflection;
using Kudos.Constants;
using Kudos.Reflection.Utils;
using Kudos.Types;
using Kudos.Utils;
using Kudos.Utils.Numerics;
using Kudos.Validations.EpikyrosiModule.Enums;

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

		internal void Validate(ref Object? o, ref MemberInfo? mi, out EEpikyrosiNotValidOn? envo)
		{
            Object?
                v = ObjectUtils.Parse(DeclaringType, ReflectionUtils.GetMemberValue(o, mi));

            if
            (
                CanBeNull != null
                && !CanBeNull.Value
                && v == null
            )
			{
                envo = EEpikyrosiNotValidOn.CanBeNull;
				return;
			}
            else if (v == null)
			{
                envo = null;
                return;
			}

            _OnValidate(ref v, out envo);
		}

		protected abstract void _OnValidate(ref Object v, out EEpikyrosiNotValidOn? eim);
	}
}

