using System;
using Kudos.Clouds.AmazonWebServiceModule.PinpointModule;
using Kudos.Clouds.AmazonWebServiceModule.PinpointModule.Builders;
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

        internal KaronteCloudingService(ref IServiceCollection sc) : base(ref sc)
        {
            AWSPinpointBuilders = new Metas(StringComparison.OrdinalIgnoreCase);
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
    }
}

