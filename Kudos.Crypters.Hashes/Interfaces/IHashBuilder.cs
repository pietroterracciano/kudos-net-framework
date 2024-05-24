using Kudos.Crypters.Hashes.Enums;
using Kudos.Crypters.KryptoModule.Interfaces;

namespace Kudos.Crypters.Hashes.Interfaces
{
	public interface
		IHashBuilder
	:
		IKryptoBuilder
	{
		IHashBuilder SetAlgorithm(EHashAlgorithm eha);
	}
}

