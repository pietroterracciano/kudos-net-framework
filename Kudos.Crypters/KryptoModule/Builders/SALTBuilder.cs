using System;
using Kudos.Crypters.KryptoModule.Descriptors;
using Kudos.Enums;
using System.Text;

namespace Kudos.Crypters.KryptoModule.Builders
{
	public sealed class
		SALTBuilder<BuilderType, BuiltType, DescriptorType>
    where
        BuilderType
    :
        AKryptoBuilder
        <
            BuilderType,
            BuiltType,
            DescriptorType
        >
    where
        BuiltType
    :
        AKrypto<DescriptorType>
    where
        DescriptorType
    :
        AKryptoDescriptor<DescriptorType>, new()
    {
        private readonly BuilderType _bt;
        private readonly DescriptorType _dsc;
        internal SALTBuilder(ref BuilderType bt, ref DescriptorType dsc) { _bt = bt; _dsc = dsc; }

        public SALTBuilder<BuilderType, BuiltType, DescriptorType> SetLength(Int32? i)
        {
            _dsc.SALTDescriptor.Length = i;
            return this;
        }

        public SALTBuilder<BuilderType, BuiltType, DescriptorType> SetCharType(ECharType? ect)
        {
            _dsc.SALTDescriptor.CharType = ect;
            return this;
        }

        public BuilderType RequestBuilder()
        {
            return _bt;
        }
	}
}

