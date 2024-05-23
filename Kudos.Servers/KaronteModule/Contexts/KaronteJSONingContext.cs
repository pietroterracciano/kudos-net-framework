using Kudos.Utils;
using System;
using Kudos.Servers.KaronteModule.Options;
using System.Text.Json;

namespace Kudos.Servers.KaronteModule.Contexts
{
    public sealed class KaronteJSONingContext : AKaronteChildContext
    {
        private readonly JsonSerializerOptions _jsonso;

        public KaronteJSONingContext(ref KaronteContext kc) : base(ref kc)
        {
            _jsonso = KaronteContext.GetRequiredService<KaronteJSONingOptions>().JsonSerializerOptions;
        }

        public String? Serialize(object? o)
        {
            return JSONUtils.Serialize(o, _jsonso);
        }

        public ObjectType? Deserialize<ObjectType>(dynamic? dnm)
        {
            return JSONUtils.Deserialize<ObjectType>(dnm, _jsonso);
        }
    }
}