using System;
using System.Text;
using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Http;
using Kudos.Constants;
using Kudos.Types;

namespace Kudos.Clouding.GoogleCloudModule.Builders
{
    public abstract class
        AGCLBuilder
        <
            BuilderType,
            BuiltType
        >
    where
        BuilderType : AGCLBuilder<BuilderType, BuiltType>
    {
        private static readonly StringBuilder __sc;
        private static readonly String __pid, __said;
        private static readonly Metas __m;

        static AGCLBuilder()
        {
            __sc = new StringBuilder();
            __pid = "pid";
            __m = new Metas(StringComparison.OrdinalIgnoreCase);
            __said = "said";
        }

        private BuilderType _bt;
        private AppOptions _ao;

        internal AGCLBuilder() { _ao = new AppOptions(); _bt = this as BuilderType; }

        public BuilderType SetCredential(GoogleCredential? gc)
        {
            if(gc != null) try { _ao.Credential = gc; } catch { }
            return _bt;
        }

        public BuilderType SetHttpClientFactory(HttpClientFactory? httpcf)
        {
            if (httpcf != null) try { _ao.HttpClientFactory = httpcf; } catch { }
            return _bt;
        }

        public BuilderType SetProjectId(String? s)
        {
            if (s != null) try { _ao.ProjectId = s; } catch { }
            return _bt;
        }

        public BuilderType SetServiceAccountId(String? s)
        {
            if (s != null) try { _ao.ServiceAccountId = s; } catch { }
            return _bt;
        }

        public BuiltType Build()
        {
            String? shk;
            __CalculateHashKey(ref _ao, out shk);
            FirebaseApp? fa;

            lock (__m)
            {
                if (!String.IsNullOrWhiteSpace(shk))
                {
                    fa = __m.Get<FirebaseApp>(shk);
                    if (fa == null)
                        try { fa = FirebaseApp.Create(_ao); __m.Set(shk, fa); } catch { fa = null; }
                }
                else
                    fa = null;
            }

            BuiltType bt;
            OnBuild(ref fa, out bt);
            return bt;
        }

        protected abstract void OnBuild(ref FirebaseApp? fa, out BuiltType bltt);

        private static void __CalculateHashKey(ref AppOptions? ao, out String? s)
        {
            if(ao == null) { s = null; return; }
            lock(__sc)
            {
                __sc
                    .Clear();

                if (!String.IsNullOrWhiteSpace(ao.ProjectId))
                    __sc.Append(__pid).Append(CCharacter.DoubleDot).Append(ao.ProjectId);

                if (!String.IsNullOrWhiteSpace(ao.ServiceAccountId))
                {
                    if (__sc.Length > 0) __sc.Append(CCharacter.Pipe);
                    __sc.Append(__said).Append(CCharacter.DoubleDot).Append(ao.ServiceAccountId);
                }

                s = __sc.ToString();
            }
        }
    }
}

