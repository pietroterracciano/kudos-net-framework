using Kudos.Crypters.KryptoModule.Descriptors;
using Kudos.Crypters.KryptoModule.HashModule.Enums;

namespace Kudos.Crypters.KryptoModule.HashModule.Descriptors
{
	public sealed class
		HashDescriptor
	:
		AKryptoDescriptor<HashDescriptor>
	{
        internal EHashAlgorithm? Algorithm;

        internal override void OnInject(ref HashDescriptor dsc)
        {
            Algorithm = dsc.Algorithm;
        }
    }
}