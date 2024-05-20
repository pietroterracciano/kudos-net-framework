using System;
using Kudos.Constants;
using Kudos.Utils;
using Kudos.Validations.EpikyrosiModule.Enums;

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
			CanBeEmpty,
			CanBeWhitespace;

        public EpikyrosiStringRule()
            : base(CType.String) { }

        protected override void _OnValidate(ref object v, out EEpikyrosiNotValidOn? envo)
        {
            String v0 = ObjectUtils.Cast<String>(v);
            
            if(v0 != null)
            {
                //if(
                //    CanBeEmpty != null
                //    && !CanBeEmpty.Value
                //    && v0.Length < 1
                //)
                //{
                //    envo = EEpikyrosiNotValidOn.CanBeEmpty;
                //    return;
                //}
                //else
                if
                (
                    CanBeWhitespace != null
                    && !CanBeWhitespace.Value
                    && String.IsNullOrWhiteSpace(v0)
                )
                {
                    envo = EEpikyrosiNotValidOn.CanBeWhitespace;
                    return;
                }
                else if
                (
                    MinLength != null
                    && MinLength > v0.Length
                )
                {
                    envo = EEpikyrosiNotValidOn.MinLength;
                    return;
                }
                else if
                (
                    MaxLength != null
                    && MaxLength < v0.Length
                )
                {
                    envo = EEpikyrosiNotValidOn.MaxLength;
                    return;
                }
            }

            envo = null;
        }
    }
}