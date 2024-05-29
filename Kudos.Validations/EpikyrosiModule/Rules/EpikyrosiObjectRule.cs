using System;
using Kudos.Utils;
using Kudos.Validations.EpikyrosiModule.Enums;
using Kudos.Validations.EpikyrosiModule.Results;
using System.Numerics;
using System.Reflection;
using Kudos.Constants;

namespace Kudos.Validations.EpikyrosiModule.Rules
{
    public sealed class
        EpikyrosiObjectRule
    :
        AEpikyrosiRule
    {
        public EpikyrosiObjectRule()
            : base(CType.Object) { }

        protected override void _OnValidate(ref object v, ref MemberInfo mi, out EpikyrosiNotValidResult? envr)
        {
            envr = null;
        }
    }
}

