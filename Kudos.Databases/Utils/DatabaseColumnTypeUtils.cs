using Kudos.Constants;
using Kudos.Databases.Enums.Columns;
using Kudos.Utils.Numerics;
using System;
using System.Collections.Generic;

namespace Kudos.Databases.Utils
{
    internal static class DatabaseColumnTypeUtils
    {
        private static readonly Int32
           __iInt8__MinValue = -128,
           __iInt8__MaxValue = 127,
           __iInt24_MinValue = -8388607,
           __iInt24_MaxValue = 8388607;

        private static readonly UInt32
           __iUInt8__MaxValue = 255,
           __iUInt24_MaxValue = UInt32Utils.NNParse(Math.Pow(2, 24)) - 1;

        private static readonly Dictionary<EDatabaseColumnType, Double>
            __dEnums2MinValues = new Dictionary<EDatabaseColumnType, Double>()
            {
                { EDatabaseColumnType.UnsignedTinyInteger, UInt16.MinValue },
                { EDatabaseColumnType.TinyInteger, __iInt8__MinValue },
                { EDatabaseColumnType.UnsignedSmallInteger, UInt16.MinValue },
                { EDatabaseColumnType.SmallInteger, Int16.MinValue },
                { EDatabaseColumnType.UnsignedMediumInteger, UInt16.MinValue },
                { EDatabaseColumnType.MediumInteger, __iInt24_MinValue },
                { EDatabaseColumnType.UnsignedInteger, UInt32.MinValue },
                { EDatabaseColumnType.Integer, Int32.MinValue },
                { EDatabaseColumnType.UnsignedBigInteger, UInt64.MinValue },
                { EDatabaseColumnType.BigInteger, Int64.MinValue },
                { EDatabaseColumnType.UnsignedDouble, Double.MinValue },
                { EDatabaseColumnType.Double, Double.MinValue }
            };

        private static readonly Dictionary<EDatabaseColumnType, Double>
            __dEnums2MaxValues = new Dictionary<EDatabaseColumnType, Double>()
            {
                { EDatabaseColumnType.UnsignedTinyInteger, __iUInt8__MaxValue },
                { EDatabaseColumnType.TinyInteger, __iInt8__MaxValue },
                { EDatabaseColumnType.UnsignedSmallInteger, UInt16.MaxValue },
                { EDatabaseColumnType.SmallInteger, Int16.MaxValue },
                { EDatabaseColumnType.UnsignedMediumInteger, __iUInt24_MaxValue },
                { EDatabaseColumnType.MediumInteger, __iInt24_MaxValue },
                { EDatabaseColumnType.UnsignedInteger, UInt32.MaxValue },
                { EDatabaseColumnType.Integer, Int32.MaxValue },
                { EDatabaseColumnType.UnsignedBigInteger, UInt64.MaxValue },
                { EDatabaseColumnType.BigInteger, Int64.MaxValue },
                { EDatabaseColumnType.UnsignedDouble, Double.MaxValue },
                { EDatabaseColumnType.Double, Double.MaxValue }
            };

        internal static Double? ToMinValue(ref EDatabaseColumnType e)
        {
            Double d;
            __dEnums2MinValues.TryGetValue(e, out d);
            return d;
        }

        internal static Double? ToMaxValue(ref EDatabaseColumnType e)
        {
            Double d;
            __dEnums2MaxValues.TryGetValue(e, out d);
            return d;
        }

        public static EDatabaseColumnType ToEnum(Type? oType)
        {
            if (oType == CType.String)
                return EDatabaseColumnType.VariableChar;
            else if (oType == CType.Int16)
                return EDatabaseColumnType.SmallInteger;
            else if (oType == CType.UInt16)
                return EDatabaseColumnType.UnsignedSmallInteger;
            else if (oType == CType.Int32)
                return EDatabaseColumnType.Integer;
            else if (oType == CType.UInt32)
                return EDatabaseColumnType.UnsignedInteger;
            else if (oType == CType.Int64)
                return EDatabaseColumnType.BigInteger;
            else if (oType == CType.UInt64)
                return EDatabaseColumnType.UnsignedBigInteger;
            else if (oType == CType.Double)
                return EDatabaseColumnType.Double;
            else if (oType == CType.Decimal)
                return EDatabaseColumnType.Double;
            else if (oType == CType.Single)
                return EDatabaseColumnType.Double;
            else if (oType == CType.Boolean)
                return EDatabaseColumnType.Boolean;
            else if (oType == CType.Object)
                return EDatabaseColumnType.Json;
            else
                return EDatabaseColumnType.Unknown;
        }
    }
}
