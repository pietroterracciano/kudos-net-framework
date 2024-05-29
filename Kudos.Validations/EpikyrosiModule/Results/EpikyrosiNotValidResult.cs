using System;
using Kudos.Validations.EpikyrosiModule.Enums;
using System.Diagnostics;
using System.Reflection;
using System.Text;
using Kudos.Constants;

namespace Kudos.Validations.EpikyrosiModule.Results
{
	public class EpikyrosiNotValidResult
	{
        private readonly StringBuilder _sb;
        private String _s;
        public readonly Boolean HasDeclaringMember;
        public readonly MemberInfo? DeclaringMember;
        public readonly EEpikyrosiNotValidOn On;
        public readonly Boolean HasThreshold;
        public readonly Object? Threshold;

        internal EpikyrosiNotValidResult
        (
            ref MemberInfo? dm,
            EEpikyrosiNotValidOn on,
            Object? t
        )
        {
            HasDeclaringMember = (DeclaringMember = dm) != null;
            On = on;
            HasThreshold = (Threshold = t) != null;
            _sb = new StringBuilder();
        }

        public override String ToString()
        {
            lock(_sb)
            {
                if(_s == null)
                    _s =
                        _sb
                            .Append(HasDeclaringMember ? DeclaringMember.Name : String.Empty)
                            .Append(CCharacter.DoubleDot)
                            .Append(Enum.GetName(On))
                            .Append(CCharacter.DoubleDot)
                            .Append(HasThreshold ? Threshold : String.Empty)
                            .ToString();

                return _s;
            }
        }
    }
}