using System;

namespace Kudos.Utils.Numerics.Integers
{
    public static class Int32NUtils
    {
        #region public static Int32? ParseFrom()

        public static int? From(object o) { if (o != null) try { return Convert.ToInt32(o); } catch { } return null; }
        public static int? From(Enum o) { if (o != null) try { return Convert.ToInt32(o); } catch { } return null; }
        public static int? From(string o) { if (o != null) try { return Convert.ToInt32(o); } catch { } return null; }
        public static int? From(ushort? o) { if (o != null) return o.Value; return null; }
        public static int? From(short? o) { if (o != null) return o.Value; return null; }
        public static int? From(uint? o) { if (o != null) return (int?)o.Value; return null; }
        public static int? From(ulong? o) { if (o != null) return (int?)o.Value; return null; }
        public static int? From(long? o) { if (o != null) return (int?)o.Value; return null; }
        public static int? From(float? o) { if (o != null) return (int?)o.Value; return null; }
        public static int? From(double? o) { if (o != null) return (int?)o.Value; return null; }
        public static int? From(decimal? o) { if (o != null) return (int?)o.Value; return null; }
        public static int? From(bool? o) { if (o != null) return o.Value ? 1 : 0; return null; }

        #endregion
    }
}