using Kudos.Utils;
using System;
using System.Text.Json;
using Kudos.Servers.KaronteModule.Services;

namespace Kudos.Servers.KaronteModule.Contexts
{
    public sealed class KaronteJSONingContext
        : AKaronteChildContext
    {
        //private readonly KaronteJSONingService _kjsons;
        public readonly JsonSerializerOptions SerializerOptions;

        internal
            KaronteJSONingContext
            (
                ref KaronteJSONingService kjsons,
                ref KaronteContext kc
            )
        :
            base
            (
                ref kc
            )
        {
            SerializerOptions = kjsons.JsonSerializerOptions;
        }

        //public Task<String?> SerializeAsync(object? o) { return Task.Run(() => Serialize(o)); }
        public String? Serialize(object? o) { return JSONUtils.Serialize(o, SerializerOptions); }

        //public Task<ObjectType?> DeserializeAsync<ObjectType>(Object? o) { return Task.Run(() => Deserialize<ObjectType>(o)); }
        public ObjectType? Deserialize<ObjectType>(Object? o) { return JSONUtils.Deserialize<ObjectType>(o, SerializerOptions); }
    }
}