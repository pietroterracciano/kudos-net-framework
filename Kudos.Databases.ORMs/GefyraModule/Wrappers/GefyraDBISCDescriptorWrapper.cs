using Kudos.Databases.Enums.Columns;
using Kudos.Databases.Models.Schemas.Columns;
using Kudos.Utils;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kudos.Databases.ORMs.GefyraModule.Wrappers
{
    //internal class GefyraDBISCDescriptorWrapper : AGefyraWrapper<DBISCDescriptorModel>
    //{

    //    internal GefyraDBISCDescriptorWrapper(ref DBISCDescriptorModel? oWrapped) : base(ref oWrapped)
    //    {
    //    }

    //    internal override Object? PrepareValue(ref Object? oValue)
    //    {
    //        if(Wrapped == null)
    //            return null;

    //        switch (Wrapped.Type)
    //        {
    //            case EDBColumnType.Json:
    //                oValue = JSONUtils.Serialize(oValue);
    //                break;
    //            default:
    //                if (IsSerializable(ref oValue))
    //                    oValue = JSONUtils.Serialize(oValue);

    //                oValue = ObjectUtils.ChangeType(oValue, Wrapped.ValueType);

    //                switch (Wrapped.Type)
    //                {
    //                    case EDBColumnType.VariableChar:
    //                    case EDBColumnType.Text:
    //                    case EDBColumnType.MediumText:
    //                    case EDBColumnType.LongText:
    //                        if (Wrapped.MaxLength != null)
    //                            oValue = StringUtils.Truncate(oValue as String, Wrapped.MaxLength.Value);
    //                        break;
    //                }
    //                break;
    //        }

    //        if (oValue != null)
    //            return;
    //        else if (Wrapped.DefaultValue != null)
    //            oValue = Wrapped.DefaultValue;
    //        else if (!IsNullable)
    //            switch (Type)
    //            {
    //                case EDBColumnType.Json:
    //                    oValue = __sDefaultNonNullableJSONValue;
    //                    break;
    //                case EDBColumnType.UnsignedTinyInteger:
    //                case EDBColumnType.TinyInteger:
    //                case EDBColumnType.UnsignedSmallInteger:
    //                case EDBColumnType.SmallInteger:
    //                case EDBColumnType.UnsignedMediumInteger:
    //                case EDBColumnType.MediumInteger:
    //                case EDBColumnType.UnsignedInteger:
    //                case EDBColumnType.Integer:
    //                case EDBColumnType.UnsignedBigInteger:
    //                case EDBColumnType.BigInteger:
    //                    oValue = 0;
    //                    break;
    //                case EDBColumnType.UnsignedDouble:
    //                case EDBColumnType.Double:
    //                    oValue = 0.0d;
    //                    break;
    //                case EDBColumnType.Boolean:
    //                    oValue = false;
    //                    break;
    //                case EDBColumnType.VariableChar:
    //                case EDBColumnType.Text:
    //                case EDBColumnType.MediumText:
    //                case EDBColumnType.LongText:
    //                    oValue = String.Empty;
    //                    break;
    //            }
    //    }
    //}
}
