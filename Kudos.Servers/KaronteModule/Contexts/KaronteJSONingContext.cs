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
        //private readonly KaronteJSONingService _kjsons;
        public readonly JsonSerializerOptions SerializerOptions;

        internal KaronteJSONingContext(ref KaronteContext kc) : base(ref kc)
        {
            SerializerOptions = KaronteContext.RequestService<KaronteJSONingService>().JsonSerializerOptions;
        }

        //public Task<String?> SerializeAsync(object? o) { return Task.Run(() => Serialize(o)); }
        public String? Serialize(object? o) { return JSONUtils.Serialize(o, SerializerOptions); }

        //public Task<ObjectType?> DeserializeAsync<ObjectType>(Object? o) { return Task.Run(() => Deserialize<ObjectType>(o)); }
        public ObjectType? Deserialize<ObjectType>(Object? o) { return JSONUtils.Deserialize<ObjectType>(o, SerializerOptions); }
    }
}