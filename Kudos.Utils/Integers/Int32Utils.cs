using System;

namespace Kudos.Utils.Integers
{
    public static class Int32Utils
    {
        private static readonly Random
            _oRandom = new Random();

        #region Random

        public static Int32 Random(Int32 i32Max)
        {
            return Random(i32Max, i32Max);
        }

        public static Int32 Random(Int32 i32Min, Int32 i32Max)
        {
            return
                i32Min == i32Max
                    ? _oRandom.Next(i32Min + 1)
                    : (
                        i32Max > i32Min
                            ? _oRandom.Next(i32Min, i32Max + 1)
                            : _oRandom.Next(i32Max, i32Min + 1)
                    );
        }

        #endregion

        #region Ceiling

        public static Int32 Ceiling(Int32 oInteger0, Int32 oInteger1)
        {
            return ParseFrom(DoubleUtils.Ceiling(oInteger0, oInteger1));
        }

        public static Int32 Ceiling(Double oDouble0, Double oDouble1)
        {
            return ParseFrom(DoubleUtils.Ceiling(oDouble0, oDouble1));
        }

        public static Int32 Ceiling(Decimal oDecimal0, Decimal oDecimal1)
        {
            return ParseFrom(DecimalUtils.Ceiling(oDecimal0, oDecimal1));
        }

        #endregion

        #region public static Int32 ParseFrom()

        public static Int32 ParseFrom(Object oObject) { return ParseFrom(Int32NUtils.ParseFrom(oObject)); }
        public static Int32 ParseFrom(Enum oEnum) { return ParseFrom(Int32NUtils.ParseFrom(oEnum)); }
        public static Int32 ParseFrom(Int32? oInteger) { return oInteger != null ? oInteger.Value : 0; }
        public static Int32 ParseFrom(Single oSingle) { return (Int32)oSingle; }
        public static Int32 ParseFrom(Double oDouble) { return (Int32)oDouble; }
        public static Int32 ParseFrom(Decimal oDecimal) { return (Int32)oDecimal; }

        #endregion
    }
}