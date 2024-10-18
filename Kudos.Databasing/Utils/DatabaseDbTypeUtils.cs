using System;
using Kudos.Databasing.Descriptors;
using Kudos.Databasing.Enums;
using System.Collections.Generic;
using System.Data;
using Kudos.Constants;

namespace Kudos.Databasing.Utils
{
	internal static class DatabaseDbTypeUtils
	{
        private static readonly Dictionary<Type, DbType>
            __d;

        static DatabaseDbTypeUtils()
        {
            __d = new Dictionary<Type, DbType>();

            __d[CType.UInt16] = DbType.UInt16;
            __d[CType.NullableUInt16] = DbType.UInt16;

            __d[CType.UInt32] = DbType.UInt32;
            __d[CType.NullableUInt32] = DbType.UInt32;

            __d[CType.UInt64] = DbType.UInt64;
            __d[CType.NullableUInt64] = DbType.UInt64;

            __d[CType.Single] = DbType.Single;
            __d[CType.NullableSingle] = DbType.Single;

            __d[CType.Double] = DbType.Double;
            __d[CType.NullableDouble] = DbType.Double;

            __d[CType.Decimal] = DbType.Decimal;
            __d[CType.NullableDecimal] = DbType.Decimal;

            __d[CType.Boolean] = DbType.Boolean;
            __d[CType.NullableBoolean] = DbType.Boolean;

            __d[CType.Byte] = DbType.Byte;
            __d[CType.NullableByte] = DbType.Byte;

            __d[CType.DateTime] = DbType.DateTime;
            __d[CType.NullableDateTime] = DbType.DateTime;
        }

    }
}

