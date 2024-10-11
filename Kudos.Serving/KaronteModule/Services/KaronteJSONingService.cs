using System;
using System.Text.Json;
using Kudos.Serving.KaronteModule.Services;
using Kudos.Utils;
using Microsoft.Extensions.DependencyInjection;

namespace Kudos.Serving.KaronteModule.Services
{
	public sealed class
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
            if (act != null)
            {
                act.Invoke(JsonSerializerOptions);
                ServiceCollection.ConfigureHttpJsonOptions((jo) => act.Invoke(jo.SerializerOptions));
            }
            return this;
        }
    }
}