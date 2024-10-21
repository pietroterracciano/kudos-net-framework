using Kudos.Utils;
using System;
using System.Text.Json;
using Kudos.Serving.KaronteModule.Services;

namespace Kudos.Serving.KaronteModule.Contexts
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

        public T? Copy<T>(T? o) { return JSONUtils.Copy(o, SerializerOptions); }
        public Object? Copy(Object? o) { return JSONUtils.Copy(o, SerializerOptions); }

        public String? Serialize(Object? o) { return JSONUtils.Serialize(o, SerializerOptions); }

        public T? Deserialize<T>(String? s) { return JSONUtils.Deserialize<T>(s, SerializerOptions); }
        public Object? Deserialize(Type? t, String? s) { return JSONUtils.Deserialize(t, s, SerializerOptions); }
    }
}