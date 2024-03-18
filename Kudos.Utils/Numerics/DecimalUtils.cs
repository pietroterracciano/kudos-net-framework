using System;

namespace Kudos.Utils.Numerics
{
    public static class DecimalUtils
    {
        #region Ceiling

        public static decimal Ceiling(int oInteger0, int oInteger1)
        {
            return Ceiling(ParseFrom(oInteger0), ParseFrom(oInteger1));
        }

        public static decimal Ceiling(double oDouble0, double oDouble1)
        {
            return Ceiling(ParseFrom(oDouble0), ParseFrom(oDouble1));
        }

        public static decimal Ceiling(decimal oDecimal0, decimal oDecimal1)
        {
            return
                oDecimal1 != 0
                ? Math.Ceiling(oDecimal0 / oDecimal1)
                : 0.0m;
        }

        #endregion

        #region Int32

        public static decimal? NullableParseFrom(int? oInteger) { if (oInteger != null) return oInteger; return null; }
        public static decimal ParseFrom(int? oInteger) { return ParseFrom(NullableParseFrom(oInteger)); }
        public static decimal ParseFrom(int oInteger) { return oInteger; }

        #endregion

        #region Double

        public static decimal? NullableParseFrom(double? oDouble) { if (oDouble != null) return (decimal?)oDouble; return null; }
        public static decimal ParseFrom(double? oDouble) { return ParseFrom(NullableParseFrom(oDouble)); }
        public static decimal ParseFrom(double oDouble) { return (decimal)oDouble; }

        #endregion

        #region Decimal?

        public static decimal ParseFrom(decimal? oDecimal) { return oDecimal != null ? oDecimal.Value : 0.0m; }

        #endregion
    }
}
