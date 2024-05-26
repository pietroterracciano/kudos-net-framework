using System;
using System.Text.Json;
using Kudos.Servers.KaronteModule.Services;
using Kudos.Utils;
using Microsoft.Extensions.DependencyInjection;

namespace Kudos.Servers.KaronteModule.Options
{
	public class
        KaronteJSONingService
    :
        AKaronteService
	{
        internal JsonSerializerOptions JsonSerializerOptions;

        internal KaronteJSONingService(ref IServiceCollection sc) : base(ref sc)
        {
            JsonSerializerOptions = new JsonSerializerOptions();
        }

        public KaronteJSONingService RegisterSerializerOptions(Action<JsonSerializerOptions>? act)
        {
            if (act != null) act.Invoke(JsonSerializerOptions);
            return this;
        }
    }
}