﻿using System;
using Microsoft.Extensions.DependencyInjection;

namespace Kudos.Serving.KaronteModule.Services
{
	public abstract class AKaronteService
	{
		public IServiceCollection ServiceCollection { get; private set; }

		internal AKaronteService(ref IServiceCollection sc) { ServiceCollection = sc; }
	}
}

