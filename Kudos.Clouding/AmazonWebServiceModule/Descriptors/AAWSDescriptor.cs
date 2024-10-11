using System;
using System.Text;
using Amazon;

namespace Kudos.Clouding.AmazonWebServiceModule.Descriptors
{
	public abstract class AAWSDescriptor<ClientType>
    {
        internal ClientType? Client;

        internal AAWSDescriptor() { }
    }
}

