using System;
using Amazon.Pinpoint;
using Amazon.S3;
using Kudos.Clouding.AmazonWebServiceModule.Descriptors;

namespace Kudos.Clouding.AmazonWebServiceModule.S3Module.Descriptors
{
    public sealed class AWSS3Descriptor
        : AAWSDescriptor<AmazonS3Client>
    {
    }
}

