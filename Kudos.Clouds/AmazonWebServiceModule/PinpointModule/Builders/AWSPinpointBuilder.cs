using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Amazon;
using Amazon.Pinpoint;
using Amazon.Pinpoint.Model;
using Kudos.Clouds.AmazonWebServiceModule.Builders;
using Kudos.Clouds.AmazonWebServiceModule.PinpointModule.Descriptors;

namespace Kudos.Clouds.AmazonWebServiceModule.PinpointModule.Builders
{
    public sealed class AWSPinpointBuilder
		: AAWSBuilder<AWSPinpointBuilder, AmazonPinpointClient, AWSPinpointDescriptor, AWSPinpoint>
	{
        private readonly AWSPinpointDescriptor _awsppd;

        public AWSPinpointBuilder SetApplicationID(String? s)
        {
            _awsppd.ApplicationID = s;
            return this;
        }

        internal AWSPinpointBuilder(ref AWSPinpointDescriptor dsc) : base(ref dsc) { _awsppd = dsc; }

        protected override void OnBuild(ref AWSPinpointDescriptor dsc, out AWSPinpoint bt)
        {
            bt = new AWSPinpoint(ref dsc);
        }
    }
}

