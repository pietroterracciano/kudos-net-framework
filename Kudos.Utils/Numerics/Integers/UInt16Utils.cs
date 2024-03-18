using System;

namespace Kudos.Utils.Numerics.Integers
{
    public static class UInt16Utils
    {
        #region public static UInt16 From

        public static ushort From(object o) { return From(UInt16NUtils.From(o)); }
        public static ushort From(Enum o) { return From(UInt16NUtils.From(o)); }
        public static ushort From(string o) { return From(UInt16NUtils.From(o)); }
        public static ushort From(ushort? o) { if (o != null) return o.Value; return 0; }
        public static ushort From(short? o) { return From(UInt16NUtils.From(o)); }
        public static ushort From(short o) { return (ushort)o; }
        public static ushort From(uint? o) { return From(UInt16NUtils.From(o)); }
        public static ushort From(uint o) { return (ushort)o; }
        public static ushort From(int? o) { return From(UInt16NUtils.From(o)); }
        public static ushort From(int o) { return (ushort)o; }
        public static ushort From(ulong? o) { return From(UInt16NUtils.From(o)); }
        public static ushort From(ulong o) { return (ushort)o; }
        public static ushort From(long? o) { return From(UInt16NUtils.From(o)); }
        public static ushort From(long o) { return (ushort)o; }
        public static ushort From(float? oSingle) { return From(UInt16NUtils.From(oSingle)); }
        public static ushort From(float oSingle) { return (ushort)oSingle; }
        public static ushort From(double? oDouble) { return From(UInt16NUtils.From(oDouble)); }
        public static ushort From(double oDouble) { return (ushort)oDouble; }
        public static ushort From(decimal? oDecimal) { return From(UInt16NUtils.From(oDecimal)); }
        public static ushort From(decimal oDecimal) { return (ushort)oDecimal; }

        #endregion
    }
}

