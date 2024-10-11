using System;
using Kudos.Clouding.AmazonWebServiceModule;
using Kudos.Clouding.AmazonWebServiceModule.PinpointModule;
using Kudos.Clouding.AmazonWebServiceModule.PinpointModule.Builders;
using Kudos.Clouding.AmazonWebServiceModule.S3Module;
using Kudos.Clouding.AmazonWebServiceModule.S3Module.Builders;
using Kudos.Serving.KaronteModule.Services.Marketing;
using Microsoft.Extensions.DependencyInjection;

namespace Kudos.Serving.KaronteModule.Services.Clouding
{
	public sealed class KaronteCloudingAWSService
        : AKaronteMetizedService
	{
        #region ... static ...

        private static String
            __s3,
            __p;

        static KaronteCloudingAWSService()
        {
            __s3 = "S3";
            __p = "Pinpoint";
        }

        #endregion

        internal KaronteCloudingAWSService(ref IServiceCollection sc) : base(ref sc) {}

        #region S3

        public KaronteCloudingAWSService RegisterS3(String? sn, Action<AWSS3Builder>? act)
        {
            if (act == null) return this;
            AWSS3Builder awss3b = AWS.RequestS3Builder();
            act.Invoke(awss3b);
            AWSS3 awss3 = awss3b.Build();
            _RegisterMeta(ref __s3, ref sn, ref awss3);
            return this;
        }

        internal AWSS3? RequireS3(String? sn)
        {
            AWSS3? awss3;
            _RequireMeta(ref __s3, ref sn, out awss3);
            return awss3;
        }

        internal AWSS3? GetS3(String? sn)
        {
            AWSS3? awss3;
            _GetMeta(ref __s3, ref sn, out awss3);
            return awss3;
        }

        #endregion

        #region Pinpoint

        public KaronteCloudingAWSService RegisterPinpoint(String? sn, Action<AWSPinpointBuilder>? act)
        {
            if (act == null) return this;
            AWSPinpointBuilder awspb = AWS.RequestPinpointBuilder();
            act.Invoke(awspb);
            AWSPinpoint awsp = awspb.Build();
            _RegisterMeta(ref __p, ref sn, ref awsp);
            return this;
        }

        internal AWSPinpoint? RequirePinpoint(String? sn)
        {
            AWSPinpoint? awsp;
            _RequireMeta(ref __p, ref sn, out awsp);
            return awsp;
        }

        internal AWSPinpoint? GetPinpoint(String? sn)
        {
            AWSPinpoint? awsp;
            _RequireMeta(ref __p, ref sn, out awsp);
            return awsp;
        }

        #endregion
    }
}

