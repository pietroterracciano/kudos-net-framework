using System;
using Kudos.Constants;
using Kudos.Utils;
using Kudos.Validations.EpikyrosiModule.Enums;
using Kudos.Validations.EpikyrosiModule.Results;
using System.Reflection;
using System.ComponentModel.DataAnnotations;
using System.Net.Mail;

namespace Kudos.Validations.EpikyrosiModule.Rules
{
    public sealed class
        EpikyrosiMailRule
    :
        AEpikyrosiStringRule
    {
        public Boolean?
            CanBeInvalid;

        protected override void _OnValidate(ref String s, ref MemberInfo mi, out EpikyrosiNotValidResult? envr)
        {
            if(CanBeInvalid == null || CanBeInvalid.Value) { envr = null; return; }

            String s0 = s.Trim();

            Int32
                iLastIndexOfAt = s0.LastIndexOf(CCharacter.At),
                iLastIndexOfDot = s0.LastIndexOf(CCharacter.Dot);

            MailAddress? ma;

            if(
                iLastIndexOfAt < 0
                || iLastIndexOfDot < 0
                || iLastIndexOfAt > iLastIndexOfDot
                || (iLastIndexOfDot + 1) > (s0.Length - 1)
                || !MailAddress.TryCreate(s, out ma)
                || !s0.Equals(ma.Address, StringComparison.OrdinalIgnoreCase)
            )
            {
                envr = new EpikyrosiNotValidResult(ref mi, EEpikyrosiNotValidOn.CanBeInvalid, CanBeInvalid.Value);
                return;
            }

            envr = null;
        }
    }
}

