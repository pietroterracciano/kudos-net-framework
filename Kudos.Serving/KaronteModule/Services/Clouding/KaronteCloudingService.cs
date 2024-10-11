using Microsoft.Extensions.DependencyInjection;

namespace Kudos.Serving.KaronteModule.Services.Clouding
{
    public sealed class
        KaronteCloudingService
    :
        AKaronteService
    {
        public readonly KaronteCloudingAWSService
            AWSService;

        public readonly KaronteCloudingGCLService
            GoogleCloudService;

        internal KaronteCloudingService(ref IServiceCollection sc) : base(ref sc)
        {
            AWSService = new KaronteCloudingAWSService(ref sc);
            GoogleCloudService = new KaronteCloudingGCLService(ref sc);
        }
    }
}