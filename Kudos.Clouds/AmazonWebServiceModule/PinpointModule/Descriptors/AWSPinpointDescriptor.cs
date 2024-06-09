using System;
using Amazon.Pinpoint;
using Amazon.Pinpoint.Model;
using Kudos.Clouds.AmazonWebServiceModule.Descriptors;

namespace Kudos.Clouds.AmazonWebServiceModule.PinpointModule.Descriptors
{
	public class AWSPinpointDescriptor
		: AAWSDescriptor<AmazonPinpointClient>
	{
		internal String? ApplicationID;
    }
}

