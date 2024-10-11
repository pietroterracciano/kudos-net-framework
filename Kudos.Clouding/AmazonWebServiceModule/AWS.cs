using System;
using Kudos.Clouding.AmazonWebServiceModule.PinpointModule.Builders;
using Kudos.Clouding.AmazonWebServiceModule.PinpointModule.Descriptors;
using Kudos.Clouding.AmazonWebServiceModule.S3Module.Builders;
using Kudos.Clouding.AmazonWebServiceModule.S3Module.Descriptors;

namespace Kudos.Clouding.AmazonWebServiceModule
{
	public static class AWS
	{
        public static AWSPinpointBuilder RequestPinpointBuilder()
        {
            AWSPinpointDescriptor awsppd = new AWSPinpointDescriptor();
            return new AWSPinpointBuilder(ref awsppd);
        }

        public static AWSS3Builder RequestS3Builder()
        {
            AWSS3Descriptor awss3d = new AWSS3Descriptor();
            return new AWSS3Builder(ref awss3d);
        }
    }
}

