using System;
using Kudos.Clouding.GoogleCloudModule.FirebaseCloudMessagingModule.Builders;

namespace Kudos.Clouding.GoogleCloudModule
{
	public static class GCL
	{
        public static GCLFirebaseCloudMessagingBuilder RequestFirebaseCloudMessagingBuilder()
        {
            return new GCLFirebaseCloudMessagingBuilder();
        }
    }
}