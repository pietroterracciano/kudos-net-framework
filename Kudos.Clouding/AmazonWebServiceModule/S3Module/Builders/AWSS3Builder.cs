using System;
using Amazon.Pinpoint;
using Amazon.S3;
using Kudos.Clouding.AmazonWebServiceModule.Builders;
using Kudos.Clouding.AmazonWebServiceModule.PinpointModule;
using Kudos.Clouding.AmazonWebServiceModule.PinpointModule.Descriptors;
using Kudos.Clouding.AmazonWebServiceModule.S3Module.Descriptors;

namespace Kudos.Clouding.AmazonWebServiceModule.S3Module.Builders
{
    public sealed class AWSS3Builder
        : AAWSBuilder<AWSS3Builder, AmazonS3Client, AWSS3Descriptor, AWSS3>
    {
        internal AWSS3Builder(ref AWSS3Descriptor dsc) : base(ref dsc) { }

        protected override void OnBuild(ref AWSS3Descriptor dsc, out AWSS3 bt)
        {
            bt = new AWSS3(ref dsc);
        }
    }
}

