using System;
using FirebaseAdmin;
using FirebaseAdmin.Messaging;
using Google.Apis.Auth.OAuth2;
using Kudos.Clouding.GoogleCloudModule.Builders;

namespace Kudos.Clouding.GoogleCloudModule.FirebaseCloudMessagingModule.Builders
{
	public sealed class GCLFirebaseCloudMessagingBuilder
		: AGCLBuilder<GCLFirebaseCloudMessagingBuilder, GCLFirebaseCloudMessaging>
	{
        protected override void OnBuild(ref FirebaseApp? fa, out GCLFirebaseCloudMessaging bt)
        {
            FirebaseMessaging? fm;
            try { fm = FirebaseMessaging.GetMessaging(fa); } catch { fm = null; }
            bt = new GCLFirebaseCloudMessaging(ref fm);
        }
    }
}

