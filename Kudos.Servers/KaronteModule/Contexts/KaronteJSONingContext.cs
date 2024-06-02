using Kudos.Utils;
using System;
using Kudos.Servers.KaronteModule.Options;
using System.Text.Json;
using System.Threading.Tasks;

namespace Kudos.Servers.KaronteModule.Contexts
{
    public sealed class KaronteJSONingContext
        : AKaronteChildContext
    {
        private readonly KaronteJSONingService _kjsons;

        internal KaronteJSONingContext(ref KaronteContext kc) : base(ref kc)
        {
            _kjsons = KaronteContext.RequestService<KaronteJSONingService>();
        }

        //public Task<String?> SerializeAsync(object? o) { return Task.Run(() => Serialize(o)); }
        public String? Serialize(object? o) { return JSONUtils.Serialize(o, _kjsons.JsonSerializerOptions); }

        //public Task<ObjectType?> DeserializeAsync<ObjectType>(Object? o) { return Task.Run(() => Deserialize<ObjectType>(o)); }
        public ObjectType? Deserialize<ObjectType>(Object? o) { return JSONUtils.Deserialize<ObjectType>(o, _kjsons.JsonSerializerOptions); }
    }
}