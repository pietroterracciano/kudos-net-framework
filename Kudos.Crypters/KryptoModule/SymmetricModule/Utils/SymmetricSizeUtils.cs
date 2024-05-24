using System;
using System.Collections.Generic;
using Kudos.Crypters.KryptoModule.SymmetricModule.Enums;

namespace Kudos.Crypters.KryptoModule.SymmetricModule.Utils
{
	internal static class SymmetricSizeUtils
	{
		private static readonly Dictionary<ESymmetricSize, Int32> __d;

		static SymmetricSizeUtils()
		{
			__d = new Dictionary<ESymmetricSize, int>(5);
			__d[ESymmetricSize._128bit] = 128;
			__d[ESymmetricSize._160bit] = 160;
			__d[ESymmetricSize._192bit] = 192;
			__d[ESymmetricSize._224bit] = 224;
			__d[ESymmetricSize._256bit] = 256;
		}

		internal static void GetInBits(ref ESymmetricSize ess, out Int32? i)
		{
			Int32 j;
			if(!__d.TryGetValue(ess, out j)) i = null;
			else i = j;
		}
	}
}

