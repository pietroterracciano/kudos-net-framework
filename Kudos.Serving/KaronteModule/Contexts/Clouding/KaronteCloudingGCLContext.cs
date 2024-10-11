using System;
using Kudos.Clouding.AmazonWebServiceModule.PinpointModule;
using Kudos.Clouding.AmazonWebServiceModule.S3Module;
using Kudos.Clouding.GoogleCloudModule.FirebaseCloudMessagingModule;
using Kudos.Serving.KaronteModule.Services.Clouding;

namespace Kudos.Serving.KaronteModule.Contexts.Clouding
{
    public sealed class KaronteCloudingGCLContext
        : AKaronteCloudingChildContext
    {
        private readonly KaronteCloudingGCLService _kcgcls;

        internal KaronteCloudingGCLContext
            (
                ref KaronteCloudingGCLService kcgcls,
                ref KaronteCloudingContext kcc
            )
        :
            base
            (
                ref kcc
            )
        {
            _kcgcls = kcgcls;
        }

        #region FirebaseCloudMessaging

        public GCLFirebaseCloudMessaging? GetFirebaseCloudMessaging(String? sn) { return _kcgcls.GetFirebaseCloudMessaging(sn); }
        public GCLFirebaseCloudMessaging? RequireFirebaseCloudMessaging(String? sn) { return _kcgcls.RequireFirebaseCloudMessaging(sn); }

        #endregion
    }
}

