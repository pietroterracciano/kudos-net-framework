using Kudos.Utils;
using System;
using Kudos.Servers.KaronteModule.Options;
using System.Text.Json;

namespace Kudos.Servers.KaronteModule.Contexts
{
    public sealed class KaronteJSONingContext
        : AKaronteChildContext
    {
        private readonly KaronteJSONingService _kjsons;

        internal KaronteJSONingContext(ref KaronteContext kc) : base(ref kc)
        {
            _kjsons = KaronteContext.GetRequiredService<KaronteJSONingService>();
        }

        public String? Serialize(object? o)
        {
            return JSONUtils.Serialize(o, _kjsons.JsonSerializerOptions);
        }

        public ObjectType? Deserialize<ObjectType>(dynamic? dnm)
        {
            return JSONUtils.Deserialize<ObjectType>(dnm, _kjsons.JsonSerializerOptions);
        }
    }
}