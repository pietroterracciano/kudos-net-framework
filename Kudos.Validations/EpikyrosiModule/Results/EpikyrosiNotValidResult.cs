using System;
using Kudos.Validations.EpikyrosiModule.Enums;
using System.Diagnostics;
using System.Reflection;

namespace Kudos.Validations.EpikyrosiModule.Results
{
	public class EpikyrosiNotValidResult
	{
        public readonly Boolean HasDeclaringMember;
        public readonly MemberInfo? DeclaringMember;
        public readonly EEpikyrosiNotValidOn On;

        internal EpikyrosiNotValidResult
        (
            ref MemberInfo? dm,
            EEpikyrosiNotValidOn on
        )
        {
            HasDeclaringMember = (DeclaringMember = dm) != null;
            On = on;
        }
    }
}