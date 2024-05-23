using System;
using System.Text.Json;

namespace Kudos.Servers.KaronteModule.Options
{
	internal class KaronteJSONingOptions
	{
        public readonly JsonSerializerOptions
            JsonSerializerOptions;

        internal KaronteJSONingOptions(ref JsonSerializerOptions jso)
        {
            JsonSerializerOptions = jso;
        }
	}
}