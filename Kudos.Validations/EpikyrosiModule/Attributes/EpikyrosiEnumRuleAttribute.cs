using System;
using Kudos.Validations.EpikyrosiModule.Rules;
using System.Numerics;

namespace Kudos.Validations.EpikyrosiModule.Attributes
{
    public sealed class EpikyrosiEnumRuleAttribute<T> : AEpikyrosiRuleAttribute
        where T : Enum
    {
        private Boolean _bIsCanBeInvalidSetted, _bCanBeInvalid;
        public Boolean CanBeInvalid { get { return _bCanBeInvalid; } set { _bCanBeInvalid = value; _bIsCanBeInvalidSetted = true; } }

        private Boolean _bIsCollisionSetted;
        private T? _eCollision;
        public T? Collision { get { return _eCollision; } set { _eCollision = value; _bIsCollisionSetted = true; } }

        protected override void _OnParseToRule(out AEpikyrosiRule rt)
        {
            EpikyrosiEnumRule<T> eer = new EpikyrosiEnumRule<T>();

            if (_bIsCollisionSetted)
                eer.Collision = Collision;
            if (_bIsCanBeInvalidSetted)
                eer.CanBeInvalid = CanBeInvalid;

            rt = eer;
        }
    }
}