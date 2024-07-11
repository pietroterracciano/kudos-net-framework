using System;
using Amazon.Pinpoint;
using Amazon.S3;
using Kudos.Clouds.AmazonWebServiceModule.Builders;
using Kudos.Clouds.AmazonWebServiceModule.PinpointModule;
using Kudos.Clouds.AmazonWebServiceModule.PinpointModule.Descriptors;
using Kudos.Clouds.AmazonWebServiceModule.S3Module.Descriptors;

namespace Kudos.Clouds.AmazonWebServiceModule.S3Module.Builders
{
    public sealed class AWSS3Builder
        : AAWSBuilder<AWSS3Builder, AmazonS3Client, AWSS3Descriptor, AWSS3>
    {
        public AWSS3Builder(ref AWSS3Descriptor dsc) : base(ref dsc) { }

        protected override void OnBuild(ref AWSS3Descriptor dsc, out AWSS3 bt)
        {
            bt = new AWSS3(ref dsc);
        }
    }
}

