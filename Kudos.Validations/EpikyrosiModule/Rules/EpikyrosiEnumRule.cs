using System;
using Kudos.Utils;
using Kudos.Validations.EpikyrosiModule.Enums;
using Kudos.Validations.EpikyrosiModule.Results;
using System.Numerics;
using System.Reflection;

namespace Kudos.Validations.EpikyrosiModule.Rules
{
    public sealed class
        EpikyrosiEnumRule<T>
    :
        AEpikyrosiRule
    where
        T
    :
        Enum
    {
        public Boolean?
            CanBeInvalid;

        private Boolean _bIsCollisionSetted;
        private T? _eCollision;
        public T? Collision { get { return _eCollision; } set { _eCollision = value; _bIsCollisionSetted = true; } }

        public EpikyrosiEnumRule()
            : base(typeof(T)) { }

        protected override void _OnValidate(ref object v, ref MemberInfo mi, out EpikyrosiNotValidResult? envr)
        {
            T? v0 = ObjectUtils.Cast<T>(v);

            if (v0 != null)
            {
                if
                (
                    CanBeInvalid != null
                    && !CanBeInvalid.Value
                    && !EnumUtils.IsValid<T>(v0)
                )
                {
                    envr = new EpikyrosiNotValidResult(ref mi, EEpikyrosiNotValidOn.CanBeInvalid, CanBeInvalid.Value);
                    return;
                }
                else if
                (
                    _bIsCollisionSetted
                    && Collision != null
                    && !EnumUtils.HasFlag(Collision, v0)
                )
                {
                    envr = new EpikyrosiNotValidResult(ref mi, EEpikyrosiNotValidOn.Collision, EnumUtils.GetKey(Collision));
                    return;
                }
            }

            envr = null;
        }
    }
}

