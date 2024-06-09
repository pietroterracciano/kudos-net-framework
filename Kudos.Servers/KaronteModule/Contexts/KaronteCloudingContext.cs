using System;
using Kudos.Clouds.AmazonWebServiceModule.PinpointModule;
using Kudos.Clouds.AmazonWebServiceModule.PinpointModule.Builders;
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