using Microsoft.AspNetCore.Builder;

namespace Kudos.Servers.KaronteModule.Interfaces
{
    public interface IKaronteBuilder
    {
        IApplicationBuilder ApplicationBuilder { get; }
    }
}
