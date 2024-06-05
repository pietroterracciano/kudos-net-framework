using System;
using System.Collections.Generic;
using System.Numerics;
using Kudos.Constants;
using Kudos.Reflection.Utils;

namespace Kudos.Utils.Numerics
{
	public static class NumericUtils
	{
		private static readonly Dictionary<Type, Type> __d0, __d1;

		static NumericUtils()
		{
            __d0 = new Dictionary<Type, Type>()
            {
                { CType.NullableUInt16, CType.UInt16 },
                { CType.NullableUInt32, CType.UInt32 },
                { CType.NullableUInt64, CType.UInt64 },
                { CType.NullableUInt128, CType.UInt128 },
                { CType.NullableInt16, CType.Int16 },
                { CType.NullableInt32, CType.Int32 },
                { CType.NullableInt64, CType.Int64 },
                { CType.NullableInt128, CType.Int128 },
                { CType.NullableSingle, CType.Single },
                { CType.NullableDouble, CType.Double },
                { CType.NullableDecimal, CType.Decimal }
            };

            __d1 = new Dictionary<Type, Type>()
            {
                { CType.UInt16, CType.NullableUInt16 },
                { CType.UInt32, CType.NullableUInt32 },
                { CType.UInt64, CType.NullableUInt64 },
                { CType.UInt128, CType.NullableUInt128 },
                { CType.Int16, CType.NullableInt16 },
                { CType.Int32, CType.NullableInt32 },
                { CType.Int64, CType.NullableInt64 },
                { CType.Int128, CType.NullableInt128 },
                { CType.Single, CType.NullableSingle },
                { CType.Double, CType.NullableDouble },
                { CType.Decimal, CType.NullableDecimal }
            };
        }

        public static Type? ParseToNType<T>()
            where T : INumberBase<T>
        {
            return ParseToNType(typeof(T));
        }

        public static Type? ParseToNType(Type? t)
        {
            if (t == null) return null;
            Type? t0;
            __d0.TryGetValue(t, out t0);
            if (t0 != null) t = t0;
            __d1.TryGetValue(t, out t0);
            return t0;
        }

        public static Type? ParseToNNType<T>()
            where T : INumberBase<T>
        {
            return ParseToNNType(typeof(T));
        }
        public static Type? ParseToNNType(Type? t)
        {
            if (t == null) return null;
            Type? t0;
            __d1.TryGetValue(t, out t0);
            if (t0 != null) t = t0;
            __d0.TryGetValue(t, out t0);
            return t0;
        }
    }
}

