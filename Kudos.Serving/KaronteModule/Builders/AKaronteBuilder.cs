using Kudos.Serving.KaronteModule.Interfaces;
using Microsoft.AspNetCore.Builder;

namespace Kudos.Serving.KaronteModule.Builders
{
    public abstract class AKaronteBuilder : IKaronteBuilder
    {
        public IApplicationBuilder ApplicationBuilder { get; private set; }

        internal AKaronteBuilder(ref IApplicationBuilder ab)
        {
            ApplicationBuilder = ab;
        }
    }
}