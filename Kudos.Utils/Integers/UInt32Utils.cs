using System;
using System.Collections.Generic;
using System.Text;

namespace Kudos.Utils.Integers
{
    public static class UInt32Utils
    {
        #region public static UInt32 ParseFrom
        public static UInt32 ParseFrom(Object oObject) { return ParseFrom(UInt32NUtils.ParseFrom(oObject)); }
        public static UInt32 ParseFrom(UInt16? oInteger) { return ParseFrom(UInt32NUtils.ParseFrom(oInteger)); }
        public static UInt32 ParseFrom(UInt16 oInteger) { return oInteger; }
        public static UInt32 ParseFrom(UInt32? oInteger) { return oInteger != null ? oInteger.Value : 0; }
        public static UInt32 ParseFrom(UInt64? oInteger) { return ParseFrom(UInt32NUtils.ParseFrom(oInteger)); }
        public static UInt32 ParseFrom(UInt64 oInteger) { return (UInt32)oInteger; }
        public static UInt32 ParseFrom(Int16? oInteger) { return ParseFrom(UInt32NUtils.ParseFrom(oInteger)); }
        public static UInt32 ParseFrom(Int16 oInteger) { return (UInt32)oInteger; }
        public static UInt32 ParseFrom(Int32? oInteger) { return ParseFrom(UInt32NUtils.ParseFrom(oInteger)); }
        public static UInt32 ParseFrom(Int32 oInteger) { return (UInt32)oInteger; }
        public static UInt32 ParseFrom(Int64? oInteger) { return ParseFrom(UInt32NUtils.ParseFrom(oInteger)); }
        public static UInt32 ParseFrom(Int64 oInteger) { return (UInt32)oInteger; }
        public static UInt32 ParseFrom(Single? oSingle) { return ParseFrom(UInt32NUtils.ParseFrom(oSingle)); }
        public static UInt32 ParseFrom(Single oSingle) { return (UInt32)oSingle; }
        public static UInt32 ParseFrom(Double? oDouble) { return ParseFrom(UInt32NUtils.ParseFrom(oDouble)); }
        public static UInt32 ParseFrom(Double oDouble) { return (UInt32)oDouble; }
        public static UInt32 ParseFrom(Decimal? oDecimal) { return ParseFrom(UInt32NUtils.ParseFrom(oDecimal)); }
        public static UInt32 ParseFrom(Decimal oDecimal) { return (UInt32)oDecimal; }
        public static UInt32 ParseFrom(String oString) { return ParseFrom(UInt32NUtils.ParseFrom(oString)); }

        #endregion
    }
}
