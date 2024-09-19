using System;
using brevo_csharp.Client;

namespace Kudos.Marketing.BrevoModule.Builders
{
    public abstract class
        ABrevoBuilder
        <
            ApiAccessorType,
            BuiltType
        >
    where
        ApiAccessorType : IApiAccessor
    {
        private Configuration? _cnf;

        internal ABrevoBuilder() {}

        public ABrevoBuilder<ApiAccessorType, BuiltType> SetConfiguration(Configuration? cnf)
        {
            _cnf = cnf;
            return this ;
        }

        public BuiltType Build()
        {
            ApiAccessorType? apiat;
            try { OnApiAccessorTypeBuild(ref _cnf, out apiat); } catch { apiat = default(ApiAccessorType); }
            BuiltType bt;
            OnBuild(ref apiat, out bt);
            return bt;
        }

        protected abstract void OnApiAccessorTypeBuild(ref Configuration? cnf, out ApiAccessorType apiat);
        protected abstract void OnBuild(ref ApiAccessorType? apiat, out BuiltType bt);
    }
}

