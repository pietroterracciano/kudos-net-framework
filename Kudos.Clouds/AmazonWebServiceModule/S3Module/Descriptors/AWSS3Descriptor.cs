using System;
using Amazon.Pinpoint;
using Amazon.S3;
using Kudos.Clouds.AmazonWebServiceModule.Descriptors;

namespace Kudos.Clouds.AmazonWebServiceModule.S3Module.Descriptors
{
    public sealed class AWSS3Descriptor
        : AAWSDescriptor<AmazonS3Client>
    {
    }
}

