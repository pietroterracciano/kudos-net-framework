using System;
using System.Runtime.ConstrainedExecution;
using Kudos.Validations.EpikyrosiModule.Rules;

namespace Kudos.Validations.EpikyrosiModule.Attributes
{
	public sealed class EpikyrosiMailRuleAttribute : AEpikyrosiStringRuleAttribute<EpikyrosiMailRule>
	{
        private Boolean _bIsCanBeInvalidSetted, _bCanBeInvalid;
        public Boolean CanBeInvalid { get { return _bCanBeInvalid; } set { _bCanBeInvalid = value; _bIsCanBeInvalidSetted = true; } }

        protected override void _OnParseToRule(ref EpikyrosiMailRule rt)
        {
            if (_bIsCanBeInvalidSetted)
                rt.CanBeInvalid = CanBeInvalid;
        }

        protected override EpikyrosiMailRule _OnRuleCreate()
        {
            return new EpikyrosiMailRule();
        }
    }
}

