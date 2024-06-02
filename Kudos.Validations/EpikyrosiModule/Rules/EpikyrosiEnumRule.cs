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
            //CanBeUndefined,
            CanBeInvalid;

        private Boolean _bIsExpectedCollisionValueSetted;
        private T? _eExpectedCollisionValue;
        public T? ExpectedCollisionValue { get { return _eExpectedCollisionValue; } set { _eExpectedCollisionValue = value; _bIsExpectedCollisionValueSetted = true; } }

        public EpikyrosiEnumRule()
            : base(typeof(T)) { }

        protected override void _OnValidate(ref object v, ref MemberInfo mi, out EpikyrosiNotValidResult? envr)
        {
            T? v0 = ObjectUtils.Cast<T>(v);

            if (v0 != null)
            {
                //if
                //(
                //    CanBeUndefined != null
                //    && !CanBeUndefined.Value
                //    && !EnumUtils.IsDefined<T>(v0)
                //)
                //{
                //    envr = new EpikyrosiNotValidResult(ref mi, EEpikyrosiNotValidOn.CanBeUndefined, CanBeUndefined.Value);
                //    return;
                //}
                //else
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
                    _bIsExpectedCollisionValueSetted
                    && ExpectedCollisionValue != null
                    && !EnumUtils.HasFlag(ExpectedCollisionValue, v0)
                )
                {
                    envr = new EpikyrosiNotValidResult(ref mi, EEpikyrosiNotValidOn.ExpectedCollisionValue, EnumUtils.GetKey(ExpectedCollisionValue));
                    return;
                }
            }

            envr = null;
        }
    }
}

