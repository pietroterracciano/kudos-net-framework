using Kudos.Constants;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Dynamic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Kudos.Utils
{
    public static class JSONUtils
{
        #region public static String? Serialize()

        public static String? Serialize(Object? o, JsonSerializerOptions? jso = null)
        {
            if (o != null)
                try { return JsonSerializer.Serialize(o, jso); } catch { }

            return null;
        }

        #endregion

        #region public static ... Deserialize...(...)

        public static T? Deserialize<T>(String? s, JsonSerializerOptions? jso = null)
        {
            return ObjectUtils.Cast<T>(Deserialize(typeof(T), s, jso));
        }
        public static Object? Deserialize(Type? t, String? s, JsonSerializerOptions? jso = null)
        {
            //if (DynamicUtils.IsNull(o) || t == null)
            //  return null;

            if (t == null || s == null)
                return null;

            //dynamic
            //    dnm = s as dynamic;

            //if (DynamicUtils.IsNull(o))
            //    return null;

            Object? o0;
            try
            {
                o0 = JsonSerializer.Deserialize(s, t, jso);
            }
            catch
            {
                o0 = null;
            }

            if (o0 == null)
                return null;

            Type 
                t0 = o0.GetType();

            if (t0 != CType.JsonElement)
                return o0;

            JsonElement 
                je = (JsonElement)o0;

            switch(je.ValueKind)
            {
                case JsonValueKind.Number:

                    UInt16 ui16; if(je.TryGetUInt16(out ui16)) return ui16;
                    Int16 i16; if (je.TryGetInt16(out i16)) return i16;
                    UInt32 ui32; if (je.TryGetUInt32(out ui32)) return ui32;
                    Int32 i32; if (je.TryGetInt32(out i32))return i32;
                    UInt64 ui64; if (je.TryGetUInt64(out ui64)) return ui64;
                    Int64 i64; if (je.TryGetInt64(out i64)) return i64;
                    Single f; if (je.TryGetSingle(out f)) return f;
                    Decimal dc; if (je.TryGetDecimal(out dc)) return dc;
                    Double d; if (je.TryGetDouble(out d)) return d;
                    break;
                case JsonValueKind.False:
                    return false;
                case JsonValueKind.True:
                    return true;
                case JsonValueKind.String:
                    try { return je.GetString(); } catch { }
                    break;
                case JsonValueKind.Null:
                    return null;
                case JsonValueKind.Array:
                    try { return je.EnumerateArray().ToArray(); } catch { }
                    break;
                //case JsonValueKind.Undefined:
                //case JsonValueKind.Object:
                //default:
                //    return new JSONDynamicObject(je);
            }

            return je;
        }

        //public static Object? Deserialize(Type? t, dynamic? jd, JsonSerializerOptions? jso)
        //{
        //    if (jd != null && t != null)
        //        try
        //        {
        //            Object? o = JsonSerializer.Deserialize(jd, t, jso);
        //            return
        //                o == null
        //                || o.GetType() != typeof(JsonElement)
        //                    ? o
        //                    : ToDynamicObject((dynamic)o);
        //        }
        //        catch
        //        {

        //        }

        //    return default(Type);
        //}

        #endregion

        #region public static ... Copy...(...)

        public static T? Copy<T>(T? o, JsonSerializerOptions? jso = null) { return ObjectUtils.Cast<T>(Copy(typeof(T), o, jso)); }
        public static Object? Copy(Type? t, Object? o, JsonSerializerOptions? jso = null)
        {
            return Deserialize(t, Serialize(o, jso), jso);
        }

        #endregion

        //#region private static dynamic ToDynamicObject()

        //private static dynamic ToDynamicObject(JsonElement je)
        //{
        //    return new JSONDynamicObject(je);
        //}

        //#endregion
    }
}