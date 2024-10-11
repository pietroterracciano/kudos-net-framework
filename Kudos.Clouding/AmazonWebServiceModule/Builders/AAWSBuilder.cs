using System;
using Amazon;
using Amazon.Runtime;
using Kudos.Clouding.AmazonWebServiceModule.Descriptors;

namespace Kudos.Clouding.AmazonWebServiceModule.Builders
{
	public abstract class
		AAWSBuilder
		<
			BuilderType,
            ClientType,
            DescriptorType,
			BuiltType
		>
    where
        ClientType : AmazonServiceClient
	where
        DescriptorType : AAWSDescriptor<ClientType>
	where
		BuilderType : AAWSBuilder<BuilderType, ClientType, DescriptorType, BuiltType>
    {
		private DescriptorType _dsc;

		internal AAWSBuilder(ref DescriptorType dsc) { _dsc = dsc; }

        public BuilderType SetClient(ClientType? ct)
		{
            _dsc.Client = ct;
            return this as BuilderType;
        }

        public BuiltType Build()
        {
            BuiltType bt;
            OnBuild(ref _dsc, out bt);
            return bt;
        }

        protected abstract void OnBuild(ref DescriptorType dsc, out BuiltType bt);
    }
}