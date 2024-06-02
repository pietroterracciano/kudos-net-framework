using System;
using Kudos.Utils;
using Kudos.Validations.EpikyrosiModule.Enums;
using Kudos.Validations.EpikyrosiModule.Results;
using System.Reflection;
using Kudos.Constants;

namespace Kudos.Validations.EpikyrosiModule.Rules
{
    public sealed class
        EpikyrosiBooleanRule
    :
        AEpikyrosiRule
    {
        public Boolean? ExpectedValue;

        public EpikyrosiBooleanRule()
            : base(CType.Boolean) { }

        protected override void _OnValidate(ref object v, ref MemberInfo mi, out EpikyrosiNotValidResult? envr)
        {
            Boolean? v0 = ObjectUtils.Cast<Boolean>(v);

            if (v0 != null)
            {
                if
                (
                    ExpectedValue != null
                    && !ExpectedValue.Value
                    && v0 != ExpectedValue
                )
                {
                    envr = new EpikyrosiNotValidResult(ref mi, EEpikyrosiNotValidOn.ExpectedValue, ExpectedValue.Value);
                    return;
                }
            }

            envr = null;
        }
    }
}

