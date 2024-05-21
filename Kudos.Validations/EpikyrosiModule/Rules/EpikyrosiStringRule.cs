using System;
using System.Reflection;
using Kudos.Constants;
using Kudos.Utils;
using Kudos.Validations.EpikyrosiModule.Enums;
using Kudos.Validations.EpikyrosiModule.Results;

namespace Kudos.Validations.EpikyrosiModule.Rules
{
	public class
        EpikyrosiStringRule
	:
		AEpikyrosiRule
	{
		public UInt16?
			MinLength,
			MaxLength;

		public Boolean?
			CanBeWhitespace;

        public EpikyrosiStringRule()
            : base(CType.String) { }

        protected override void _OnValidate(ref object v, ref MemberInfo mi, out EpikyrosiNotValidResult? envr)
        {
            String v0 = ObjectUtils.Cast<String>(v);
            
            if(v0 != null)
            {
                if
                (
                    CanBeWhitespace != null
                    && !CanBeWhitespace.Value
                    && String.IsNullOrWhiteSpace(v0)
                )
                {
                    envr = new EpikyrosiNotValidResult(ref mi, EEpikyrosiNotValidOn.CanBeWhitespace, false);
                    return;
                }
                else if
                (
                    MinLength != null
                    && MinLength > v0.Length
                )
                {
                    envr = new EpikyrosiNotValidResult(ref mi, EEpikyrosiNotValidOn.MinLength, MinLength);
                    return;
                }
                else if
                (
                    MaxLength != null
                    && MaxLength < v0.Length
                )
                {
                    envr = new EpikyrosiNotValidResult(ref mi, EEpikyrosiNotValidOn.MaxLength, MaxLength);
                    return;
                }
            }

            envr = null;
        }
    }
}