using System;

namespace Kudos.Utils.Doubles
{
    public static class DoubleUtils
    {
        #region Ceiling

        public static Double Ceiling(Int32 oInteger0, Int32 oInteger1)
        {
            return Ceiling(ParseFrom(oInteger0), ParseFrom(oInteger1));
        }

        public static Double Ceiling(Decimal oDecimal0, Decimal oDecimal1)
        {
            return Ceiling(ParseFrom(oDecimal0), ParseFrom(oDecimal1));
        }

        public static Double Ceiling(Double oDouble0, Double oDouble1)
        {
            return
                oDouble1 != 0
                ? Math.Ceiling(oDouble0 / oDouble1)
                : 0.0d;
        }

        #endregion

        #region Object

        public static Double? NullableParseFrom(Object oObject) { if (oObject != null) try { return Convert.ToDouble(oObject); } catch { } return null; }
        public static Double ParseFrom(Object oObject) { return ParseFrom(NullableParseFrom(oObject)); }

        #endregion

        #region Int32

        public static Double? NullableParseFrom(Int32? oInteger) { if (oInteger != null) return oInteger; return null; }
        public static Double ParseFrom(Int32? oInteger) { return ParseFrom(NullableParseFrom(oInteger)); }
        public static Double ParseFrom(Int32 oInteger) { return oInteger; }

        #endregion

        #region Double?

        public static Double ParseFrom(Double? oDouble) { return oDouble != null ? oDouble.Value : 0.0d; }

        #endregion
    }
}
