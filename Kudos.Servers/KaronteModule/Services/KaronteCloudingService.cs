using System;
using Kudos.Clouds.AmazonWebServiceModule.PinpointModule;
using Kudos.Clouds.AmazonWebServiceModule.PinpointModule.Builders;
using Kudos.Clouds.AmazonWebServiceModule.S3Module;
using Kudos.Clouds.AmazonWebServiceModule.S3Module.Builders;
using Kudos.Types;
using Microsoft.Extensions.DependencyInjection;

namespace Kudos.Servers.KaronteModule.Services
{
    public class
        KaronteCloudingService
    :
        AKaronteService
    {
        internal readonly Metas AWSPinpointBuilders;
        internal readonly Metas AWSS3Builders;

        internal KaronteCloudingService(ref IServiceCollection sc) : base(ref sc)
        {
            AWSPinpointBuilders = new Metas(StringComparison.OrdinalIgnoreCase);
            AWSS3Builders = new Metas(StringComparison.OrdinalIgnoreCase);
        }

        public KaronteCloudingService RegisterAWSPinpointBuilder(string? sn, Action<AWSPinpointBuilder>? act)
        {
            if (sn != null && act != null)
            {
                AWSPinpointBuilder awsppb = AWSPinpoint.RequestBuilder();
                act.Invoke(awsppb);
                AWSPinpointBuilders.Set(sn, awsppb);
            }

            return this;
        }

        public KaronteCloudingService RegisterAWSS3Builder(string? sn, Action<AWSS3Builder>? act)
        {
            if (sn != null && act != null)
            {
                AWSS3Builder awsppb = AWSS3.RequestBuilder();
                act.Invoke(awsppb);
                AWSS3Builders.Set(sn, awsppb);
            }

            return this;
        }
    }
}