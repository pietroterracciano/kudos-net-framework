using System;
using Kudos.Clouds.AmazonWebServiceModule.PinpointModule;
using Kudos.Clouds.AmazonWebServiceModule.PinpointModule.Builders;
using Kudos.Clouds.AmazonWebServiceModule.S3Module;
using Kudos.Clouds.AmazonWebServiceModule.S3Module.Builders;
using Kudos.Servers.KaronteModule.Services;

namespace Kudos.Servers.KaronteModule.Contexts
{
    public sealed class KaronteCloudingContext
        : AKaronteChildContext
    {
        private readonly KaronteCloudingService _kcs;

        internal KaronteCloudingContext
            (
                ref KaronteCloudingService kcs,
                ref KaronteContext kc
            )
        :
            base
            (
                ref kc
            )
        {
            _kcs = kcs;
        }

        public AWSS3? GetAWSS3(String? sn)
        {
            AWSS3Builder? awss3b = _kcs.AWSS3Builders.Get<AWSS3Builder>(sn);
            return awss3b != null ? awss3b.Build() : null;
        }

        public AWSS3 RequireAWSS3(String? sn)
        {
            AWSS3? awspp = GetAWSS3(sn);
            if (awspp == null) throw new InvalidOperationException();
            return awspp;
        }

        public AWSPinpoint? GetAWSPinpoint(String? sn)
        {
            AWSPinpointBuilder? awsppb = _kcs.AWSPinpointBuilders.Get<AWSPinpointBuilder>(sn);
            return awsppb != null ? awsppb.Build() : null;
        }

        public AWSPinpoint RequireAWSPinpoint(String? sn)
        {
            AWSPinpoint? awspp = GetAWSPinpoint(sn);
            if (awspp == null) throw new InvalidOperationException();
            return awspp;
        }
    }
}