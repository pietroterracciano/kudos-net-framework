using System;
using Amazon.Pinpoint;
using Amazon.Pinpoint.Model;
using Kudos.Clouding.AmazonWebServiceModule.Descriptors;

namespace Kudos.Clouding.AmazonWebServiceModule.PinpointModule.Descriptors
{
	public sealed class AWSPinpointDescriptor
		: AAWSDescriptor<AmazonPinpointClient>
	{
		internal String? ApplicationID;
    }
}

