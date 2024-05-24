using System;
using Kudos.Enums;
using System.Text;
using Kudos.Crypters.KryptoModule.Descriptors;
using Kudos.Utils;
using Kudos.Reflection.Utils;

namespace Kudos.Crypters.KryptoModule.Builders
{
    public abstract class
        AKryptoBuilder
        <
            BuilderType,
            BuiltType,
            DescriptorType
        >
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
        private BuilderType _this;
        private readonly SALTBuilder<BuilderType, BuiltType, DescriptorType> _SALTb;
        private DescriptorType _dsc;
        internal AKryptoBuilder(ref DescriptorType dsc)
        {
            _this = this as BuilderType;
            _SALTb = new SALTBuilder<BuilderType, BuiltType, DescriptorType>(ref _this, ref dsc);
            _dsc = dsc;
        }

        public BuilderType SetEncoding(Encoding? enc)
        {
            _dsc.Encoding = enc;
            return this as BuilderType;
        }

        public BuilderType SetBinaryEncoding(EBinaryEncoding? ebe)
        {
            _dsc.BinaryEncoding = ebe;
            return this as BuilderType;
        }

        public SALTBuilder<BuilderType, BuiltType, DescriptorType> RequestSALT()
        {
            return _SALTb;
        }

        public BuiltType Build()
        {
            BuiltType bt;
            DescriptorType dsc = new DescriptorType().Inject(ref _dsc);
            OnBuild(ref dsc, out bt);
            return bt;
        }

        protected abstract void OnBuild(ref DescriptorType dsc, out BuiltType bt);
    }
}

