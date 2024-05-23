using System;
using System.Text.Json;

namespace Kudos.Servers.KaronteModule.Contexts.Options
{
	internal class KaronteJSONingOptionsContext
	{
		public readonly JsonSerializerOptions?
			JsonSerializerOptions;

		internal KaronteJSONingOptionsContext(ref JsonSerializerOptions jso)
		{
			JsonSerializerOptions = jso;
        }
	}
}

