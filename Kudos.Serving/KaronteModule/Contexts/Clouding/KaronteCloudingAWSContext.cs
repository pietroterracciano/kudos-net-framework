using System;
using Kudos.Clouding.AmazonWebServiceModule.PinpointModule;
using Kudos.Clouding.AmazonWebServiceModule.S3Module;
using Kudos.Serving.KaronteModule.Services.Clouding;

namespace Kudos.Serving.KaronteModule.Contexts.Clouding
{
    public sealed class KaronteCloudingAWSContext
        : AKaronteCloudingChildContext
    {
        private readonly KaronteCloudingAWSService _kcawss;

        internal KaronteCloudingAWSContext
            (
                ref KaronteCloudingAWSService kcawss,
                ref KaronteCloudingContext kcc
            )
        :
            base
            (
                ref kcc
            )
        {
            _kcawss = kcawss;
        }

        #region S3

        public AWSS3? GetS3(String? sn) { return _kcawss.GetS3(sn); }
        public AWSS3? RequireS3(String? sn) { return _kcawss.RequireS3(sn); }

        #endregion

        #region Pinpoint

        public AWSPinpoint? GetPinpoint(String? sn) { return _kcawss.GetPinpoint(sn); }
        public AWSPinpoint? RequirePinpoint(String? sn) { return _kcawss.RequirePinpoint(sn); }

        #endregion
    }
}

