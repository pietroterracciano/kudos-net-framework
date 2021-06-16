using System;
using System.Collections.Generic;
using System.Text;

namespace Kudos.Utils.Integers
{
    public static class UInt32NUtils
    {
        #region public static UInt32? ParseFrom()

        public static UInt32? ParseFrom(Object oObject) { if (oObject != null) try { return Convert.ToUInt32(oObject); } catch { } return null; }
        public static UInt32? ParseFrom(UInt16? oInteger) { if (oInteger != null) return oInteger; return null; }
        public static UInt32? ParseFrom(UInt64? oInteger) { if (oInteger != null) return (UInt32?)oInteger; return null; }
        public static UInt32? ParseFrom(Int16? oInteger) { if (oInteger != null) return (UInt32?)oInteger; return null; }
        public static UInt32? ParseFrom(Int32? oInteger) { if (oInteger != null) return (UInt32?)oInteger; return null; }
        public static UInt32? ParseFrom(Int64? oInteger) { if (oInteger != null) return (UInt32?)oInteger; return null; }
        public static UInt32? ParseFrom(Single? oSingle) { if (oSingle != null) return (UInt32?)oSingle; return null; }
        public static UInt32? ParseFrom(Double? oDouble) { if (oDouble != null) return (UInt32?)oDouble; return null; }
        public static UInt32? ParseFrom(Decimal? oDecimal) { if (oDecimal != null) return (UInt32?)oDecimal; return null; }
        public static UInt32? ParseFrom(String oString) { if (oString != null) try { return Convert.ToUInt32(oString); } catch { } return null; }

        #endregion
    }
}