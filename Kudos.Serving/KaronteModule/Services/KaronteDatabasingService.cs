using System;
using System.Text.Json;
using Kudos.Databases.Chainers;
using Kudos.Databases.Interfaces.Chains;
using Kudos.Types;
using Microsoft.Extensions.DependencyInjection;

namespace Kudos.Serving.KaronteModule.Services
{
    public sealed class
        KaronteDatabasingService
    :
        AKaronteService
    {
        internal IBuildableDatabaseChain? BuildableDatabaseChain;

        internal KaronteDatabasingService(ref IServiceCollection sc) : base(ref sc) { }

        public KaronteDatabasingService RegisterChain(Func<IDatabaseChain, IBuildableDatabaseChain>? fnc)
        {
            if (fnc != null) BuildableDatabaseChain = fnc(DatabaseChainer.NewChain());
            return this;
        }
    }
}