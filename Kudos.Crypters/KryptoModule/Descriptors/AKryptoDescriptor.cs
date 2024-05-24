using System;
using Kudos.Enums;
using System.Text;

namespace Kudos.Crypters.KryptoModule.Descriptors
{
    public abstract class AKryptoDescriptor<DescriptorType>
        where DescriptorType : AKryptoDescriptor<DescriptorType>, new()
    {
        internal Encoding? Encoding;
        internal SALTDescriptor SALTDescriptor;
        internal EBinaryEncoding? BinaryEncoding;

        internal AKryptoDescriptor() { SALTDescriptor = new SALTDescriptor(); }

        internal DescriptorType Inject(ref DescriptorType dsc)
        {
            Encoding = dsc.Encoding;
            SALTDescriptor.CharType = dsc.SALTDescriptor.CharType;
            SALTDescriptor.Length = dsc.SALTDescriptor.Length;
            BinaryEncoding = dsc.BinaryEncoding;
            OnInject(ref dsc);
            return this as DescriptorType;
        }

        internal abstract void OnInject(ref DescriptorType dsc);
    }
}