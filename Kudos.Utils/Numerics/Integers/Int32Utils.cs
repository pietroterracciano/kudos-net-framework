using Kudos.Utils.Numerics.Doubles;
using System;

namespace Kudos.Utils.Numerics.Integers
{
    public static class Int32Utils
    {
        private static readonly Random
            _oRandom = new Random();

        #region Random

        public static int Random(int i32Max)
        {
            return Random(i32Max, i32Max);
        }

        public static int Random(int i32Min, int i32Max)
        {
            return
                i32Min == i32Max
                    ? _oRandom.Next(i32Min + 1)
                    :
                        i32Max > i32Min
                            ? _oRandom.Next(i32Min, i32Max + 1)
                            : _oRandom.Next(i32Max, i32Min + 1)
                    ;
        }

        #endregion

        #region Ceiling

        public static int Ceiling(int oInteger0, int oInteger1)
        {
            return From(DoubleUtils.Ceiling(oInteger0, oInteger1));
        }

        public static int Ceiling(double oDouble0, double oDouble1)
        {
            return From(DoubleUtils.Ceiling(oDouble0, oDouble1));
        }

        public static int Ceiling(decimal oDecimal0, decimal oDecimal1)
        {
            return From(DecimalUtils.Ceiling(oDecimal0, oDecimal1));
        }

        #endregion

        #region public static Int32 From()

        public static int From(object o) { return From(Int32NUtils.From(o)); }
        public static int From(Enum o) { return From(Int32NUtils.From(o)); }
        public static int From(string o) { return From(Int32NUtils.From(o)); }
        public static int From(bool? o) { return From(Int32NUtils.From(o)); }
        public static int From(bool o) { return o ? 1 : 0; }
        public static int From(ushort? o) { return From(Int32NUtils.From(o)); }
        public static int From(ushort o) { return o; }
        public static int From(short? o) { return From(Int32NUtils.From(o)); }
        public static int From(short o) { return o; }
        public static int From(uint? o) { return From(Int32NUtils.From(o)); }
        public static int From(uint o) { return (int)o; }
        public static int From(int? o) { return o != null ? o.Value : 0; }
        public static int From(ulong? o) { return From(Int32NUtils.From(o)); }
        public static int From(ulong o) { return (int)o; }
        public static int From(long? o) { return From(Int32NUtils.From(o)); }
        public static int From(long o) { return (int)o; }
        public static int From(float? oSingle) { return From(Int32NUtils.From(oSingle)); }
        public static int From(float oSingle) { return (int)oSingle; }
        public static int From(double? oDouble) { return From(Int32NUtils.From(oDouble)); }
        public static int From(double oDouble) { return (int)oDouble; }
        public static int From(decimal? oDecimal) { return From(Int32NUtils.From(oDecimal)); }
        public static int From(decimal oDecimal) { return (int)oDecimal; }


        #endregion
    }
}