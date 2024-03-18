using System;

namespace Kudos.Utils.Numerics.Integers
{
    public static class UInt16NUtils
    {
        #region public static UInt16? ParseFrom()

        public static ushort? From(object o) { if (o != null) try { return Convert.ToUInt16(o); } catch { } return null; }
        public static ushort? From(Enum o) { if (o != null) try { return Convert.ToUInt16(o); } catch { } return null; }
        public static ushort? From(string o) { if (o != null) try { return Convert.ToUInt16(o); } catch { } return null; }
        public static ushort? From(short? o) { if (o != null) return (ushort?)o; return null; }
        public static ushort? From(uint? o) { if (o != null) return (ushort?)o; return null; }
        public static ushort? From(int? o) { if (o != null) return (ushort?)o; return null; }
        public static ushort? From(ulong? o) { if (o != null) return (ushort?)o; return null; }
        public static ushort? From(long? o) { if (o != null) return (ushort?)o; return null; }
        public static ushort? From(float? o) { if (o != null) return (ushort?)o; return null; }
        public static ushort? From(double? o) { if (o != null) return (ushort?)o; return null; }
        public static ushort? From(decimal? o) { if (o != null) return (ushort?)o; return null; }

        #endregion
    }
}

