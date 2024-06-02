using System;
using Kudos.Validations.EpikyrosiModule.Rules;
using System.Numerics;

namespace Kudos.Validations.EpikyrosiModule.Attributes
{
    public sealed class EpikyrosiEnumRuleAttribute<T> : AEpikyrosiRuleAttribute
        where T : Enum
    {
        //private Boolean _bIsCanBeUndefinedSetted, _bCanBeUndefined;
        //public Boolean CanBeUndefined { get { return _bCanBeUndefined; } set { _bCanBeUndefined = value; _bIsCanBeUndefinedSetted = true; } }

        private Boolean _bIsCanBeInvalidSetted, _bCanBeInvalid;
        public Boolean CanBeInvalid{ get { return _bCanBeInvalid; } set { _bCanBeInvalid = value; _bIsCanBeInvalidSetted = true; } }

        private Boolean _bIsExpectedCollisionValueSetted;
        private T? _eExpectedCollisionValue;
        public T? ExpectedCollisionValue { get { return _eExpectedCollisionValue; } set { _eExpectedCollisionValue = value; _bIsExpectedCollisionValueSetted = true; } }

        protected override void _OnParseToRule(out AEpikyrosiRule rt)
        {
            EpikyrosiEnumRule<T> eer = new EpikyrosiEnumRule<T>();

            if (_bIsExpectedCollisionValueSetted)
                eer.ExpectedCollisionValue = ExpectedCollisionValue;
            //if (_bIsCanBeUndefinedSetted)
            //    eer.CanBeUndefined = CanBeUndefined;
            if (_bIsCanBeInvalidSetted)
                eer.CanBeInvalid = CanBeInvalid;

            rt = eer;
        }
    }
}