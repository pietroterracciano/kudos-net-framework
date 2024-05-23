using System;
using Kudos.Utils;
using System.Text.Json;
using System.Text.Json.Serialization;
using Kudos.Enums;

namespace Kudos.Converters
{
    public class EnumJSONConverter<T> : JsonConverter<T>
        where T : Enum
    {
        private readonly EEJCWorksOn _ejcwo;

        public EnumJSONConverter(EEJCWorksOn ejcwo)
        {
            _ejcwo = ejcwo;
        }

        public override T? Read(
            ref Utf8JsonReader utf8jsonr,
            Type t,
            JsonSerializerOptions jsonso
        )
        {
            if (_ejcwo == EEJCWorksOn.Name)
            {
                String? s; try { s = utf8jsonr.GetString(); } catch { s = null; }
                return EnumUtils.Parse<T>(s);
            }
            else
            {
                Int32 i; utf8jsonr.TryGetInt32(out i);
                return EnumUtils.Parse<T>(i);
            }
        }

        public override void Write(
            Utf8JsonWriter utf8jsonw,
            T? e,
            JsonSerializerOptions jsonso
        )
        {
            if (_ejcwo == EEJCWorksOn.Name)
            {
                String? s = EnumUtils.GetKey(e);
                if (s != null) try { utf8jsonw.WriteStringValue(s); return; } catch { }
            }
            else
            {
                Int32? i = EnumUtils.GetValue(e);
                if (i != null) try { utf8jsonw.WriteNumberValue(i.Value); return; } catch { }
            }

            utf8jsonw.WriteNullValue();
        }
    }
}

