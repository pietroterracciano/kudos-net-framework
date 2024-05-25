using System;
using Microsoft.Extensions.DependencyInjection;

namespace Kudos.Servers.KaronteModule.Interfaces
{
	public interface IKaronteServiceCollection
	{
        IServiceCollection ServiceCollection { get; }
    }
}

