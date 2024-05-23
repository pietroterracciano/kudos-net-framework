using Kudos.Constants;
using Kudos.Databases.Enums;
using Kudos.Utils.Numerics;
using System;
using System.Collections.Generic;

namespace Kudos.Databases.Utils
{
    internal static class DatabaseDataTypeUtils
    {
        private static readonly Int32
           __iInt8__MinValue = -128,
           __iInt8__MaxValue = 127,
           __iInt24_MinValue = -8388607,
           __iInt24_MaxValue = 8388607;

        private static readonly UInt32
           __iUInt8__MaxValue = 255,
           __iUInt24_MaxValue = UInt32Utils.NNParse(Math.Pow(2, 24)) - 1;

        private static readonly Dictionary<EDatabaseDataType, Double>
            __dEnums2MinValues = new Dictionary<EDatabaseDataType, Double>()
            {
                { EDatabaseDataType.UnsignedTinyInteger, UInt16.MinValue },
                { EDatabaseDataType.TinyInteger, __iInt8__MinValue },
                { EDatabaseDataType.UnsignedSmallInteger, UInt16.MinValue },
                { EDatabaseDataType.SmallInteger, Int16.MinValue },
                { EDatabaseDataType.UnsignedMediumInteger, UInt16.MinValue },
                { EDatabaseDataType.MediumInteger, __iInt24_MinValue },
                { EDatabaseDataType.UnsignedInteger, UInt32.MinValue },
                { EDatabaseDataType.Integer, Int32.MinValue },
                { EDatabaseDataType.UnsignedBigInteger, UInt64.MinValue },
                { EDatabaseDataType.BigInteger, Int64.MinValue },
                { EDatabaseDataType.UnsignedDouble, UInt64.MinValue },
                { EDatabaseDataType.Double, Double.MinValue }
            };

        private static readonly Dictionary<EDatabaseDataType, Double>
            __dEnums2MaxValues = new Dictionary<EDatabaseDataType, Double>()
            {
                { EDatabaseDataType.UnsignedTinyInteger, __iUInt8__MaxValue },
                { EDatabaseDataType.TinyInteger, __iInt8__MaxValue },
                { EDatabaseDataType.UnsignedSmallInteger, UInt16.MaxValue },
                { EDatabaseDataType.SmallInteger, Int16.MaxValue },
                { EDatabaseDataType.UnsignedMediumInteger, __iUInt24_MaxValue },
                { EDatabaseDataType.MediumInteger, __iInt24_MaxValue },
                { EDatabaseDataType.UnsignedInteger, UInt32.MaxValue },
                { EDatabaseDataType.Integer, Int32.MaxValue },
                { EDatabaseDataType.UnsignedBigInteger, UInt64.MaxValue },
                { EDatabaseDataType.BigInteger, Int64.MaxValue },
                { EDatabaseDataType.UnsignedDouble, Double.MaxValue },
                { EDatabaseDataType.Double, Double.MaxValue }
            };

        internal static Double? ToMinValue(ref EDatabaseDataType e)
        {
            Double d;
            __dEnums2MinValues.TryGetValue(e, out d);
            return d;
        }

        internal static Double? ToMaxValue(ref EDatabaseDataType e)
        {
            Double d;
            __dEnums2MaxValues.TryGetValue(e, out d);
            return d;
        }

        public static EDatabaseDataType ToEnum(Type? oType)
        {
            if (oType == CType.String)
                return EDatabaseDataType.VariableChar;
            else if (oType == CType.Int16)
                return EDatabaseDataType.SmallInteger;
            else if (oType == CType.UInt16)
                return EDatabaseDataType.UnsignedSmallInteger;
            else if (oType == CType.Int32)
                return EDatabaseDataType.Integer;
            else if (oType == CType.UInt32)
                return EDatabaseDataType.UnsignedInteger;
            else if (oType == CType.Int64)
                return EDatabaseDataType.BigInteger;
            else if (oType == CType.UInt64)
                return EDatabaseDataType.UnsignedBigInteger;
            else if (oType == CType.Double)
                return EDatabaseDataType.Double;
            else if (oType == CType.Decimal)
                return EDatabaseDataType.Double;
            else if (oType == CType.Single)
                return EDatabaseDataType.Double;
            else if (oType == CType.Boolean)
                return EDatabaseDataType.Boolean;
            else if (oType == CType.Object)
                return EDatabaseDataType.Json;
            else
                return EDatabaseDataType.Unknown;
        }
    }
}
