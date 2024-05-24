using System;
using System.Text;
using Kudos.Crypters.KryptoModule.Descriptors;
using Kudos.Enums;

namespace Kudos.Crypters.Hashes.HashModule.Descriptors
{
	public class
		HashDescriptor
	:
		AKryptoDescriptor<HashDescriptor>
	{
        internal Encoding? Encoding;
        internal UInt32? SALTLength;
        internal EBinaryEncoding? BinaryEncoding;

        public override HashDescriptor Inject(ref HashDescriptor dsc)
        {
            throw new NotImplementedException();
        }
    }
}

