using System;

namespace Kudos.Utils.Integers
{
    public static class Int32NUtils
    {
        #region public static Int32? ParseFrom()

        public static Int32? ParseFrom(Object oObject) { if (oObject != null) try { return Convert.ToInt32(oObject); } catch { } return null; }
        public static Int32? ParseFrom(Enum oEnum) { if (oEnum != null) try { return Convert.ToInt32(oEnum); } catch { } return null; }

        #endregion
    }
}