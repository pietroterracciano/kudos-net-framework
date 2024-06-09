using System;
using System.Text;
using Amazon;

namespace Kudos.Clouds.AmazonWebServiceModule.Descriptors
{
	public abstract class AAWSDescriptor<ClientType>
    {
        internal ClientType? Client;

        internal AAWSDescriptor() { }
    }
}

