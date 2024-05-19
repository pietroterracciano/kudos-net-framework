namespace Kudos.Extensions
{
    public static class FieldExtensions
    {
        //public static Object? PrepareValue(this FieldInfo fi, Object? o)
        //{
        //    Boolean
        //        b = ObjectUtils.IsNullable(fi.FieldType);

        //    if (b)
        //    {
        //        ExtensionAttribute?
        //            ea = MemberUtils.GetAttribute<ExtensionAttribute>(fi);

        //        EInstanceMode
        //            eim;

        //        if 
        //        (
        //            ea != null
        //            && ea.InstanceMode == EInstanceMode.NotNullable
        //        )
        //            b = false;
        //    }

        //    o = ObjectUtils.ChangeType(o, fi.FieldType);

        //    if 
        //    (
        //        b  
        //        || o != null
        //    )
        //        return o;

        //    else if
        //    (
        //        fi.FieldType == CType.UInt16
        //        || fi.FieldType == CType.UInt16N
        //    )
        //        return UInt16.MinValue;
        //    else if
        //    (
        //        fi.FieldType == CType.UInt32
        //        || fi.FieldType == CType.UInt32N
        //    )
        //        return UInt32.MinValue;
        //    else if
        //    (
        //        fi.FieldType == CType.UInt64
        //        || fi.FieldType == CType.UInt64N
        //    )
        //        return UInt64.MinValue;
        //    else if
        //    (
        //        fi.FieldType == CType.Int16
        //        || fi.FieldType == CType.Int16N
        //    )
        //        return (short)0;
        //    else if
        //    (
        //        fi.FieldType == CType.Int32
        //        || fi.FieldType == CType.Int32N
        //    )
        //        return (int)0;
        //    else if
        //    (
        //       fi.FieldType == CType.Int64
        //       || fi.FieldType == CType.Int64N
        //    )
        //        return 0L;

        //    else if
        //    (
        //        fi.FieldType == CType.Single
        //        || fi.FieldType == CType.SingleN
        //    )
        //        return 0.0f;

        //    else if
        //    (
        //        fi.FieldType == CType.Double
        //        || fi.FieldType == CType.DoubleN
        //    )
        //        return 0.0d;

        //    else if
        //    (
        //        fi.FieldType == CType.Decimal
        //        || fi.FieldType == CType.DecimalN
        //    )
        //        return (decimal)0.0;

        //    else if
        //    (
        //        fi.FieldType == CType.Boolean
        //        || fi.FieldType == CType.BooleanN
        //    )
        //        return false;

        //    else if
        //    (
        //        fi.FieldType == CType.String
        //    )
        //        return String.Empty;

        //    return o;
        //}

    }
}
