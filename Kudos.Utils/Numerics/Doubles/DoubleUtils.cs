using System;

namespace Kudos.Utils.Numerics.Doubles
{
    public static class DoubleUtils
    {
        #region Ceiling

        public static double Ceiling(int oInteger0, int oInteger1)
        {
            return Ceiling(ParseFrom(oInteger0), ParseFrom(oInteger1));
        }

        public static double Ceiling(decimal oDecimal0, decimal oDecimal1)
        {
            return Ceiling(ParseFrom(oDecimal0), ParseFrom(oDecimal1));
        }

        public static double Ceiling(double oDouble0, double oDouble1)
        {
            return
                oDouble1 != 0
                ? Math.Ceiling(oDouble0 / oDouble1)
                : 0.0d;
        }

        #endregion

        #region Object

        public static double? NullableParseFrom(object oObject) { if (oObject != null) try { return Convert.ToDouble(oObject); } catch { } return null; }
        public static double ParseFrom(object oObject) { return ParseFrom(NullableParseFrom(oObject)); }

        #endregion

        #region Int32

        public static double? NullableParseFrom(int? oInteger) { if (oInteger != null) return oInteger; return null; }
        public static double ParseFrom(int? oInteger) { return ParseFrom(NullableParseFrom(oInteger)); }
        public static double ParseFrom(int oInteger) { return oInteger; }

        #endregion

        #region Double?

        public static double ParseFrom(double? oDouble) { return oDouble != null ? oDouble.Value : 0.0d; }

        #endregion
    }
}
