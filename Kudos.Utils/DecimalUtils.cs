using System;

namespace Kudos.Utils
{
    public static class DecimalUtils
    {
        #region Ceiling

        public static Decimal Ceiling(Int32 oInteger0, Int32 oInteger1)
        {
            return Ceiling(ParseFrom(oInteger0), ParseFrom(oInteger1));
        }

        public static Decimal Ceiling(Double oDouble0, Double oDouble1)
        {
            return Ceiling(ParseFrom(oDouble0), ParseFrom(oDouble1));
        }

        public static Decimal Ceiling(Decimal oDecimal0, Decimal oDecimal1)
        {
            return
                oDecimal1 != 0
                ? Math.Ceiling(oDecimal0 / oDecimal1)
                : 0.0m;
        }

        #endregion

        #region Int32

        public static Decimal? NullableParseFrom(Int32? oInteger) { if (oInteger != null) return oInteger; return null; }
        public static Decimal ParseFrom(Int32? oInteger) { return ParseFrom(NullableParseFrom(oInteger)); }
        public static Decimal ParseFrom(Int32 oInteger) { return oInteger; }

        #endregion

        #region Double

        public static Decimal? NullableParseFrom(Double? oDouble) { if (oDouble != null) return (Decimal?)oDouble; return null; }
        public static Decimal ParseFrom(Double? oDouble) { return ParseFrom(NullableParseFrom(oDouble)); }
        public static Decimal ParseFrom(Double oDouble) { return (Decimal)oDouble; }

        #endregion

        #region Decimal?

        public static Decimal ParseFrom(Decimal? oDecimal) { return oDecimal != null ? oDecimal.Value : 0.0m; }

        #endregion
    }
}
