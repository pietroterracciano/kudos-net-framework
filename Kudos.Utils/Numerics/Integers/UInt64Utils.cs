using System;

namespace Kudos.Utils.Numerics.Integers
{
    public static class UInt64Utils
    {
        #region public static UInt64 From()

        public static ulong From(object o) { return From(UInt64NUtils.From(o)); }
        public static ulong From(Enum o) { return From(UInt64NUtils.From(o)); }
        public static ulong From(string o) { return From(UInt64NUtils.From(o)); }
        public static ulong From(ushort? o) { return From(UInt64NUtils.From(o)); }
        public static ulong From(ushort o) { return o; }
        public static ulong From(short? o) { return From(UInt64NUtils.From(o)); }
        public static ulong From(short o) { return (ulong)o; }
        public static ulong From(uint? o) { return From(UInt64NUtils.From(o)); }
        public static ulong From(uint o) { return o; }
        public static ulong From(int? o) { return From(UInt64NUtils.From(o)); }
        public static ulong From(int o) { return (ulong)o; }
        public static ulong From(ulong? o) { return o != null ? o.Value : 0; }
        public static ulong From(long? o) { return From(UInt64NUtils.From(o)); }
        public static ulong From(long o) { return (ulong)o; }
        public static ulong From(float? o) { return From(UInt64NUtils.From(o)); }
        public static ulong From(float o) { return (ulong)o; }
        public static ulong From(double? o) { return From(UInt64NUtils.From(o)); }
        public static ulong From(double o) { return (ulong)o; }
        public static ulong From(decimal? o) { return From(UInt64NUtils.From(o)); }
        public static ulong From(decimal o) { return (ulong)o; }

        #endregion
    }
}