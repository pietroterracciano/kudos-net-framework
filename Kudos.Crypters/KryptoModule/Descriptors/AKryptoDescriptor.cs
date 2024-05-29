using System;
using Kudos.Enums;
using System.Text;
using Kudos.Crypters.KryptoModule.Enums;

namespace Kudos.Crypters.KryptoModule.Descriptors
{
    public abstract class AKryptoDescriptor<DescriptorType>
        where DescriptorType : AKryptoDescriptor<DescriptorType>, new()
    {
        internal Encoding? Encoding;
        internal SALTDescriptor SALTDescriptor;
        internal EBinaryEncoding? BinaryEncoding;
        internal EInternalSerialization? InternalSerialization;

        internal AKryptoDescriptor() { SALTDescriptor = new SALTDescriptor(); }

        internal DescriptorType Inject(ref DescriptorType dsc)
        {
            Encoding = dsc.Encoding;
            SALTDescriptor.Length = dsc.SALTDescriptor.Length;
            BinaryEncoding = dsc.BinaryEncoding;
            InternalSerialization = dsc.InternalSerialization;
            OnInject(ref dsc);
            return this as DescriptorType;
        }

        protected abstract void OnInject(ref DescriptorType dsc);
    }
}