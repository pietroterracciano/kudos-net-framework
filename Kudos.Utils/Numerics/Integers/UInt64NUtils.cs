using System;

namespace Kudos.Utils.Numerics.Integers
{
    public static class UInt64NUtils
    {
        #region public static UInt64? From()

        public static ulong? From(object o) { if (o != null) try { return Convert.ToUInt64(o); } catch { } return null; }
        public static ulong? From(Enum o) { if (o != null) try { return Convert.ToUInt64(o); } catch { } return null; }
        public static ulong? From(string o) { if (o != null) try { return Convert.ToUInt64(o); } catch { } return null; }
        public static ulong? From(ushort? o) { if (o != null) return o; return null; }
        public static ulong? From(short? o) { if (o != null) return (ulong?)o; return null; }
        public static ulong? From(uint? o) { if (o != null) return o; return null; }
        public static ulong? From(int? o) { if (o != null) return (ulong?)o; return null; }
        public static ulong? From(long? o) { if (o != null) return (ulong?)o; return null; }
        public static ulong? From(float? o) { if (o != null) return (ulong?)o; return null; }
        public static ulong? From(double? o) { if (o != null) return (ulong?)o; return null; }
        public static ulong? From(decimal? o) { if (o != null) return (ulong?)o; return null; }

        #endregion
    }
}