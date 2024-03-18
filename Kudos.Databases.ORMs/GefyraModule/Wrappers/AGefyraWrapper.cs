using Kudos.Constants;
using Kudos.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Kudos.Databases.ORMs.GefyraModule.Wrappers
{
    internal abstract class AGefyraWrapper<ObjectType>
    {
        private static readonly HashSet<Type>
            __hsNonSerializableTypes = new HashSet<Type>()
            {
                CType.UInt16,
                CType.UInt32,
                CType.UInt64,
                CType.Int16,
                CType.Int32,
                CType.Int64,
                CType.Boolean,
                CType.String,
                CType.Char,
                CType.Single,
                CType.Decimal,
                CType.Double
            };

        internal readonly ObjectType? Wrapped;

        internal AGefyraWrapper(ref ObjectType? oWrapped)
        {
            Wrapped = oWrapped;
        }

        protected Boolean IsSerializable(ref Object oValue)
        {
            return 
                oValue != null 
                && !__hsNonSerializableTypes.Contains(oValue.GetType());
        }

        internal abstract Object PrepareValue(ref Object oObject);

        /*
        private void _PrepareValue(ref Object oValue)
        {
            switch (Type)
            {
                case EDBColumnType.Json:
                    oValue = JSONUtils.Serialize(oValue);
                    break;
                default:
                    if(IsSerializable(ref oValue))
                        oValue = JSONUtils.Serialize(oValue);

                    oValue = ObjectUtils.ChangeType(oValue, ValueType);

                    switch (Type)
                    {
                        case EDBColumnType.VariableChar:
                        case EDBColumnType.Text:
                        case EDBColumnType.MediumText:
                        case EDBColumnType.LongText:
                            if (MaxLength != null)
                                oValue = StringUtils.Truncate(oValue as String, MaxLength.Value);
                            break;
                    }
                    break;
            }

            if (oValue != null)
                return;
            else if (DefaultValue != null)
                oValue = DefaultValue;
            else if (!IsNullable)
                switch (Type)
                {
                    case EDBColumnType.Json:
                        oValue = __sDefaultNonNullableJSONValue;
                        break;
                    case EDBColumnType.UnsignedTinyInteger:
                    case EDBColumnType.TinyInteger:
                    case EDBColumnType.UnsignedSmallInteger:
                    case EDBColumnType.SmallInteger:
                    case EDBColumnType.UnsignedMediumInteger:
                    case EDBColumnType.MediumInteger:
                    case EDBColumnType.UnsignedInteger:
                    case EDBColumnType.Integer:
                    case EDBColumnType.UnsignedBigInteger:
                    case EDBColumnType.BigInteger:
                        oValue = 0;
                        break;
                    case EDBColumnType.UnsignedDouble:
                    case EDBColumnType.Double:
                        oValue = 0.0d;
                        break;
                    case EDBColumnType.Boolean:
                        oValue = false;
                        break;
                    case EDBColumnType.VariableChar:
                    case EDBColumnType.Text:
                    case EDBColumnType.MediumText:
                    case EDBColumnType.LongText:
                        oValue = String.Empty;
                        break;
                }
        }

        public Object PrepareValue(Object oValue)
        {
            //if (Type != EDBColumnType.Json)
            //{
            //    ICollection oCollection = ObjectUtils.AsCollection(oValue);

            //    if (oCollection != null)
            //    {
            //        IEnumerator oCEnumerator = oCollection.GetEnumerator();

            //        if (oCEnumerator != null)
            //        {
            //            Array
            //                oArray = ArrayUtils.CreateInstance<Object>(oCollection.Count);

            //            int i = 0;
            //            while (oCEnumerator.MoveNext())
            //            {
            //                Object oCECurrent = oCEnumerator.Current;
            //                _PrepareValue(ref oCECurrent);
            //                oArray.SetValue(oCECurrent, i++);
            //            }

            //            return oArray;
            //        }
            //    }
            //}

            _PrepareValue(ref oValue);
            return oValue;
        }
        */
    }
}
