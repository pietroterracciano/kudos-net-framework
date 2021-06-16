using System;

namespace Kudos.Utils
{
    public static class IntegerUtils
    {
        /*
        public static UInt16 ToUInt16(UInt16 oInteger) { return oInteger; }
        public static UInt16 ToUInt16(UInt32 oInteger) { return (UInt16)oInteger; }
        public static UInt16 ToUInt16(UInt64 oInteger) { return (UInt16)oInteger; }
        public static UInt16 ToUInt16(Int16 oInteger) { return (UInt16)oInteger; }
        public static UInt16 ToUInt16(Int32 oInteger) { return (UInt16)oInteger; }
        public static UInt16 ToUInt16(Int64 oInteger) { return (UInt16)oInteger; }
        public static UInt16 ToUInt16(Single oSingle) { return (UInt16)oSingle; }
        public static UInt16 ToUInt16(Double oDouble) { return (UInt16)oDouble; }
        public static UInt16 ToUInt16(Decimal oDecimal) { return (UInt16)oDecimal; }
        */

        #region UInt32
        public static UInt32? ToNullableUInt32(UInt16? oInteger) { if (oInteger != null) return oInteger; return null; }
        public static UInt32 ToUInt32(UInt16? oInteger) { return ToUInt32(ToNullableUInt32(oInteger)); }
        public static UInt32 ToUInt32(UInt16 oInteger) { return oInteger; }

        public static UInt32 ToUInt32(UInt32? oInteger) { return oInteger != null ? oInteger.Value : 0; }

        public static UInt32? ToNullableUInt32(UInt64? oInteger) { if (oInteger != null) return (UInt32?)oInteger; return null; }
        public static UInt32 ToUInt32(UInt64? oInteger) { return ToUInt32(ToNullableUInt32(oInteger)); }
        public static UInt32 ToUInt32(UInt64 oInteger) { return (UInt32)oInteger; }

        public static UInt32? ToNullableUInt32(Int16? oInteger) { if (oInteger != null) return (UInt32?)oInteger; return null; }
        public static UInt32 ToUInt32(Int16? oInteger) { return ToUInt32(ToNullableUInt32(oInteger)); }
        public static UInt32 ToUInt32(Int16 oInteger) { return (UInt32)oInteger; }

        public static UInt32? ToNullableUInt32(Int32? oInteger) { if (oInteger != null) return (UInt32?)oInteger; return null; }
        public static UInt32 ToUInt32(Int32? oInteger) { return ToUInt32(ToNullableUInt32(oInteger)); }
        public static UInt32 ToUInt32(Int32 oInteger) { return (UInt32)oInteger; }

        public static UInt32? ToNullableUInt32(Int64? oInteger) { if (oInteger != null) return (UInt32?)oInteger; return null; }
        public static UInt32 ToUInt32(Int64? oInteger) { return ToUInt32(ToNullableUInt32(oInteger)); }
        public static UInt32 ToUInt32(Int64 oInteger) { return (UInt32)oInteger; }

        public static UInt32? ToNullableUInt32(Single? oSingle) { if (oSingle != null) return (UInt32?)oSingle; return null; }
        public static UInt32 ToUInt32(Single? oSingle) { return ToUInt32(ToNullableUInt32(oSingle)); }
        public static UInt32 ToUInt32(Single oSingle) { return (UInt32)oSingle; }

        public static UInt32? ToNullableUInt32(Double? oDouble) { if(oDouble != null) return (UInt32?)oDouble; return null; }
        public static UInt32 ToUInt32(Double? oDouble) { return ToUInt32(ToNullableUInt32(oDouble)); }
        public static UInt32 ToUInt32(Double oDouble) { return (UInt32)oDouble; }

        public static UInt32? ToNullableUInt32(Decimal? oDecimal) { if (oDecimal != null) return (UInt32?)oDecimal; return null; }
        public static UInt32 ToUInt32(Decimal? oDecimal) { return ToUInt32(ToNullableUInt32(oDecimal)); }
        public static UInt32 ToUInt32(Decimal oDecimal) { return (UInt32)oDecimal; }

        public static UInt32? ToNullableUInt32(String oString) { if (oString != null) try { return Convert.ToUInt32(oString); } catch { } return null; }
        public static UInt32 ToUInt32(String oString) { return ToUInt32(ToNullableUInt32(oString)); }

        #endregion

        public static UInt64 ToUInt64(UInt64? oInteger) { return oInteger != null ? oInteger.Value : 0; }

        public static Int16 ToInt16(Int16? oInteger) { return oInteger != null ? oInteger.Value : (short)0; }

        #region Int64

        public static Int64 ToInt64(Int64? oInteger) { return oInteger != null ? oInteger.Value : 0; }

        #endregion
    }
}