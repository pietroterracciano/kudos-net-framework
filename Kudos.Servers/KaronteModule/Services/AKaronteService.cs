using System;
using Microsoft.Extensions.DependencyInjection;

namespace Kudos.Servers.KaronteModule.Services
{
	internal abstract class AKaronteService
	{
		public IServiceCollection ServiceCollection { get; private set; }

		internal AKaronteService(ref IServiceCollection sc) { ServiceCollection = sc; }
	}
}

