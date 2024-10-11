using System;
using Kudos.Serving.KaronteModule.Contexts.Crypting;
using Kudos.Serving.KaronteModule.Services.Clouding;

namespace Kudos.Serving.KaronteModule.Contexts.Clouding
{
    public sealed class KaronteCloudingContext
        : AKaronteChildContext
    {
        private readonly KaronteCloudingService _kcs;
        public readonly KaronteCloudingAWSContext AWSContext;
        public readonly KaronteCloudingGCLContext GoogleCloudContext;

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
            KaronteCloudingContext kcc = this;
            KaronteCloudingAWSService kcawss = kcs.AWSService;
            AWSContext = new KaronteCloudingAWSContext(ref kcawss, ref kcc);
            KaronteCloudingGCLService kcgcls = kcs.GoogleCloudService;
            GoogleCloudContext = new KaronteCloudingGCLContext(ref kcgcls, ref kcc);
        }
    }
}