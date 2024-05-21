using System;
using Kudos.Validations.EpikyrosiModule.Rules;

namespace Kudos.Validations.EpikyrosiModule.Attributes
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = true, Inherited = true)]
    public abstract class AEpikyrosiRuleAttribute : Attribute
	{
        internal Boolean IsCanBeNullSetted { get; private set; }
        private Boolean _bCanBeNull;
        public Boolean CanBeNull { get { return _bCanBeNull; } set { _bCanBeNull = value; IsCanBeNullSetted = true; } }

        public String?[]? PoolNames { get; set; }

        internal void ParseToRule(out AEpikyrosiRule rt)
        {
            _OnParseToRule(out rt);
            if (IsCanBeNullSetted) rt.CanBeNull = CanBeNull;
        }

        protected abstract void _OnParseToRule(out AEpikyrosiRule rt);
    }
}