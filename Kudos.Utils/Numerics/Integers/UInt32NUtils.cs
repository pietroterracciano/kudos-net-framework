using System;

namespace Kudos.Utils.Numerics.Integers
{
    public static class UInt32NUtils
    {
        #region public static UInt32? From()

        public static uint? From(object o) { if (o != null) try { return Convert.ToUInt32(o); } catch { } return null; }
        public static uint? From(Enum o) { if (o != null) try { return Convert.ToUInt32(o); } catch { } return null; }
        public static uint? From(string o) { if (o != null) try { return Convert.ToUInt32(o); } catch { } return null; }
        public static uint? From(ushort? o) { if (o != null) return o; return null; }
        public static uint? From(short? o) { if (o != null) return (uint?)o; return null; }
        public static uint? From(int? o) { if (o != null) return (uint?)o; return null; }
        public static uint? From(ulong? o) { if (o != null) return (uint?)o; return null; }
        public static uint? From(long? o) { if (o != null) return (uint?)o; return null; }
        public static uint? From(float? o) { if (o != null) return (uint?)o; return null; }
        public static uint? From(double? o) { if (o != null) return (uint?)o; return null; }
        public static uint? From(decimal? o) { if (o != null) return (uint?)o; return null; }

        #endregion
    }
}