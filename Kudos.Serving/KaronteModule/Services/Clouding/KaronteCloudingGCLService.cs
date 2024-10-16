using System;
using Kudos.Clouding.GoogleCloudModule;
using Kudos.Clouding.GoogleCloudModule.FirebaseCloudMessagingModule;
using Kudos.Clouding.GoogleCloudModule.FirebaseCloudMessagingModule.Builders;
using Microsoft.Extensions.DependencyInjection;

namespace Kudos.Serving.KaronteModule.Services.Clouding
{
    public sealed class KaronteCloudingGCLService
        : AKaronteMetizedService
    {
        #region ... static ...

        private static String
            __fcm;

        static KaronteCloudingGCLService()
        {
            __fcm = "FirebaseCloudMessaging";
        }

        #endregion

        internal KaronteCloudingGCLService(ref IServiceCollection sc) : base(ref sc) { }

        #region FirebaseCloudMessaging

        public KaronteCloudingGCLService RegisterFirebaseCloudMessaging(String? sn, Action<GCLFirebaseCloudMessagingBuilder>? act)
        {
            if (act == null) return this;
            GCLFirebaseCloudMessagingBuilder gclfcmb = GCL.RequestFirebaseCloudMessagingBuilder();
            act.Invoke(gclfcmb);
            GCLFirebaseCloudMessaging gclfcm = gclfcmb.Build();
            _RegisterMeta(ref __fcm, ref sn, ref gclfcm);
            return this;
        }

        internal GCLFirebaseCloudMessaging RequireFirebaseCloudMessaging(String? sn)
        {
            GCLFirebaseCloudMessaging gclfcm;
            _RequireMeta(ref __fcm, ref sn, out gclfcm);
            return gclfcm;
        }

        internal GCLFirebaseCloudMessaging? GetFirebaseCloudMessaging(String? sn)
        {
            GCLFirebaseCloudMessaging? gclfcm;
            _GetMeta(ref __fcm, ref sn, out gclfcm);
            return gclfcm;
        }

        #endregion
    }
}

