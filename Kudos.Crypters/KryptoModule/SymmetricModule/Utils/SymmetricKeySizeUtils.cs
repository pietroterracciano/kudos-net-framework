using System;
using System.Collections.Generic;
using Kudos.Crypters.KryptoModule.SymmetricModule.Enums;

namespace Kudos.Crypters.KryptoModule.SymmetricModule.Utils
{
	internal static class SymmetricKeySizeUtils
	{
		private static readonly Dictionary<ESymmetricKeySize, Int32> __d;

		static SymmetricKeySizeUtils()
		{
			__d = new Dictionary<ESymmetricKeySize, int>(5);
			__d[ESymmetricKeySize._128bit] = 128;
			__d[ESymmetricKeySize._160bit] = 160;
			__d[ESymmetricKeySize._192bit] = 192;
			__d[ESymmetricKeySize._224bit] = 224;
			__d[ESymmetricKeySize._256bit] = 256;
		}

		internal static void GetAsInt32(ref ESymmetricKeySize ess, out Int32? i)
		{
			Int32 j;
			if(!__d.TryGetValue(ess, out j)) i = null;
			else i = j;
		}
	}
}

