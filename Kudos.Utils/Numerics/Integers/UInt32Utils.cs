using System;
using System.Collections.Generic;
using System.Text;

namespace Kudos.Utils.Numerics.Integers
{
    public static class UInt32Utils
    {
        #region public static UInt32 ParseFrom

        public static uint From(object o) { return From(UInt32NUtils.From(o)); }
        public static uint From(Enum o) { return From(UInt32NUtils.From(o)); }
        public static uint From(string o) { return From(UInt32NUtils.From(o)); }
        public static uint From(ushort? o) { return From(UInt32NUtils.From(o)); }
        public static uint From(ushort o) { return o; }
        public static uint From(short? o) { return From(UInt32NUtils.From(o)); }
        public static uint From(short o) { return (uint)o; }
        public static uint From(uint? o) { return o != null ? o.Value : 0; }
        public static uint From(int? o) { return From(UInt32NUtils.From(o)); }
        public static uint From(int o) { return (uint)o; }
        public static uint From(ulong? o) { return From(UInt32NUtils.From(o)); }
        public static uint From(ulong o) { return (uint)o; }
        public static uint From(long? o) { return From(UInt32NUtils.From(o)); }
        public static uint From(long o) { return (uint)o; }
        public static uint From(float? oSingle) { return From(UInt32NUtils.From(oSingle)); }
        public static uint From(float oSingle) { return (uint)oSingle; }
        public static uint From(double? oDouble) { return From(UInt32NUtils.From(oDouble)); }
        public static uint From(double oDouble) { return (uint)oDouble; }
        public static uint From(decimal? oDecimal) { return From(UInt32NUtils.From(oDecimal)); }
        public static uint From(decimal oDecimal) { return (uint)oDecimal; }

        #endregion
    }
}
