using Kudos.Utils;
using System;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.Text.Encodings.Web;
using Kudos.Servers.KaronteModule.Contexts.Options;

namespace Kudos.Servers.KaronteModule.Contexts
{
    public sealed class KaronteJSONingContext : AKaronteChildContext
    {
        private readonly KaronteJSONingOptionsContext _kjsonoc;

        public KaronteJSONingContext(ref KaronteContext kc) : base(ref kc)
        {
            _kjsonoc = KaronteContext.GetRequiredService<KaronteJSONingOptionsContext>();
        }

        public String? Serialize(object? o)
        {
            return JSONUtils.Serialize(o, _kjsonoc.JsonSerializerOptions);
        }

        public ObjectType? Deserialize<ObjectType>(dynamic? dnm)
        {
            return JSONUtils.Deserialize<ObjectType>(dnm, _kjsonoc.JsonSerializerOptions);
        }
    }
}